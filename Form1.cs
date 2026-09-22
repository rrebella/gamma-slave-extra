using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using gamma2.Properties;

namespace rebellagamma
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll")]
        static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        static extern bool DeleteDC(IntPtr hdc);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Blue;
        }

        private (double gamma, int brightness, int contrast)? settingsAR1 = null;
        private (double gamma, int brightness, int contrast)? settingsAR2 = null;
        private double ar1Value, ar2Value;
        private string ConfigFilePath => Path.Combine(Application.StartupPath, "config.txt");
        private string _currentCustomImage = "";
        private string _lastDeletedProfileLine = "";
        private string _lastAction = "";
        private string _lastRenamedFrom = "";
        private string _lastRenamedTo = "";
        private List<int> _lastDeletedProfilePresets = new List<int>();
        private FileSystemWatcher _configWatcher;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;

        public Form1()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            using (MemoryStream ms = new MemoryStream(gamma2.Properties.Resources.ico))
            {
                this.Icon = new Icon(ms);
            }

            LoadMonitors();
            UpdateValueLabels();

            this.Load += Form1_Load;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = this.Icon;
            notifyIcon.Text = "gamma slave extra";
            notifyIcon.Visible = true;

            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Show", null, (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; });
            trayMenu.Items.Add("Exit", null, (s, e) => { notifyIcon.Visible = false; Application.Exit(); });

            notifyIcon.ContextMenuStrip = trayMenu;
            notifyIcon.DoubleClick += (s, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.ShowBalloonTip(1000, "gamma slave extra", "The application is running in the background.", ToolTipIcon.Info);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureConfigFileExists();
            InitializeConfigWatcher();
            SetupViews();
            SetupBindsUI();
            LoadProfiles();
            AttachDefocus(this);
            LoadSettings();
            ConsolidateLoadRows();
            ValidateConfigDuplicates();
            ToggleARControls(!_viewBonus.EnableCustomPNG);
            _proc = HookCallback;
            _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);

            if (_viewBonus.chkDefaultOnStartup.Checked && _viewBonus.cmbDefaultConfig.SelectedItem != null)
            {
                string defConfig = _viewBonus.cmbDefaultConfig.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(defConfig))
                {
                    LoadProfileByName(defConfig);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_viewBonus != null && _viewBonus.chkResetOnExit.Checked)
            {
                buttonReset_Click(null, null);
            }
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosing(e);
        }

        private void EnsureConfigFileExists()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    File.WriteAllText(ConfigFilePath, string.Empty);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No permission to write to the application folder. Move the application to a folder with write permissions (e.g., Desktop).",
                              "Write Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot create the configuration file: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void InitializeConfigWatcher()
        {
            try
            {
                _configWatcher = new FileSystemWatcher
                {
                    Path = Path.GetDirectoryName(ConfigFilePath),
                    Filter = Path.GetFileName(ConfigFilePath),
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
                };

                _configWatcher.Changed += ConfigWatcher_Changed;
                _configWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during FileSystemWatcher initialization: " + ex.Message);
            }
        }

        private void ConfigWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                LoadProfiles();
            });
        }

        private void LoadMonitors()
        {
            comboBoxMonitors.Items.Clear();
            foreach (var screen in Screen.AllScreens)
            {
                comboBoxMonitors.Items.Add(screen.DeviceName);
            }
            if (comboBoxMonitors.Items.Count > 0)
                comboBoxMonitors.SelectedIndex = 0;
        }

        private void UpdateValueLabels()
        {
            numericUpDown1.Value = (decimal)trackBarGamma.Value / 100M;
            numericUpDown2.Value = (decimal)trackBarBrightness.Value / 100M;
            numericUpDown3.Value = (decimal)trackBarContrast.Value / 100M;
        }

        private void ApplyAllSettings()
        {
            if (comboBoxMonitors.SelectedItem == null) return;

            float gamma = trackBarGamma.Value / 100.0f;
            if (gamma < 0.1f) gamma = 0.1f;

            int brightness = trackBarBrightness.Value;
            float contrast = trackBarContrast.Value / 100.0f;

            IntPtr hdc = CreateDC("DISPLAY", comboBoxMonitors.SelectedItem.ToString(), IntPtr.Zero, IntPtr.Zero);

            RAMP ramp = new RAMP()
            {
                Red = new ushort[256],
                Green = new ushort[256],
                Blue = new ushort[256]
            };

            for (int i = 0; i < 256; i++)
            {
                double normalized = i / 255.0;

                double gammaCorrected = Math.Pow(normalized, 1.0 / gamma);
                double contrastAdjusted = ((gammaCorrected - 0.5) * contrast) + 0.5;
                double brightnessAdjusted = contrastAdjusted + (brightness / 255.0);

                double val = brightnessAdjusted;
                if (val < 0) val = 0;
                if (val > 1) val = 1;

                int rampVal = (int)(val * 65535);
                if (rampVal > 65535) rampVal = 65535;
                if (rampVal < 0) rampVal = 0;

                ramp.Red[i] = ramp.Green[i] = ramp.Blue[i] = (ushort)rampVal;
            }

            SetDeviceGammaRamp(hdc, ref ramp);
            DeleteDC(hdc);
        }

        private void trackBarGamma_Scroll(object sender, EventArgs e)
        {
            ApplyAllSettings();
            UpdateValueLabels();
            panelGraph.Invalidate();
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            ApplyAllSettings();
            UpdateValueLabels();
            panelGraph.Invalidate();
        }

        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            ApplyAllSettings();
            UpdateValueLabels();
            panelGraph.Invalidate();
        }

        private void comboBoxMonitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyAllSettings();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (_viewBonus != null && _viewBonus.chkDefaultOnStartup.Checked && _viewBonus.cmbDefaultConfig.SelectedItem != null && !string.IsNullOrEmpty(_viewBonus.cmbDefaultConfig.SelectedItem.ToString()))
            {
                LoadProfileByName(_viewBonus.cmbDefaultConfig.SelectedItem.ToString());
                return;
            }

            trackBarGamma.Value = 100;
            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 100;
            ApplyAllSettings();
            UpdateValueLabels();
            panelGraph.Invalidate();
            comboBoxProfiles.SelectedIndex = -1;
            _currentCustomImage = "";
            if (_panelCustomImage != null) _panelCustomImage.Invalidate();
        }

        private void buttonSaveAR1_Click(object sender, EventArgs e)
        {
            double ar1 = (double)numericUpDownAR1.Value;
            ar1 = ApplyDTIfChecked(ar1, checkBoxDT1);
            ar1Value = ar1;
            settingsAR1 = GetCurrentSettings();
            labelAR1.Text = $"AR1: {ar1:F2} | G: {settingsAR1.Value.gamma:0.00} | B: {settingsAR1.Value.brightness} | C: {settingsAR1.Value.contrast}";
            this.ActiveControl = null;
        }

        private void buttonSaveAR2_Click(object sender, EventArgs e)
        {
            double ar2 = (double)numericUpDownAR2.Value;
            ar2 = ApplyDTIfChecked(ar2, checkBoxDT2);
            ar2Value = ar2;
            settingsAR2 = GetCurrentSettings();
            labelAR2.Text = $"AR2: {ar2:F2} | G: {settingsAR2.Value.gamma:0.00} | B: {settingsAR2.Value.brightness} | C: {settingsAR2.Value.contrast}";
            this.ActiveControl = null;
        }

        private void buttonSetAR3_Click(object sender, EventArgs e)
        {
            if (settingsAR1 == null || settingsAR2 == null)
            {
                MessageBox.Show("First, save the AR1 and AR2 settings.");
                return;
            }

            double ar3 = (double)numericUpDownTargetAR.Value;
            ar3 = ApplyDTIfChecked(ar3, checkBoxDT3);

            if (Math.Abs(ar2Value - ar1Value) < 1e-6)
            {
                MessageBox.Show("AR1 and AR2 have the same value; interpolation is not possible.");
                return;
            }

            double factor = (ar3 - ar1Value) / (ar2Value - ar1Value);

            double gamma = Lerp(settingsAR1.Value.gamma, settingsAR2.Value.gamma, factor);
            int brightness = (int)Math.Round(Lerp(settingsAR1.Value.brightness, settingsAR2.Value.brightness, factor));
            int contrast = (int)Math.Round(Lerp(settingsAR1.Value.contrast, settingsAR2.Value.contrast, factor));

            gamma = Math.Max(0.08, Math.Min(8.88, gamma));
            brightness = Math.Max(-888, Math.Min(888, brightness));
            contrast = Math.Max(8, Math.Min(888, contrast));

            trackBarGamma.Value = (int)(gamma * 100);
            trackBarBrightness.Value = brightness;
            trackBarContrast.Value = contrast;

            UpdateValueLabels();
            ApplyAllSettings();
            panelGraph.Invalidate();

            labelAR3.Text = $"AR3: {ar3:F2} | G: {gamma:0.00} | B: {brightness} | C: {contrast}";

            this.ActiveControl = null;
        }

        private (double gamma, int brightness, int contrast) GetCurrentSettings()
        {
            return (trackBarGamma.Value / 100.0, trackBarBrightness.Value, trackBarContrast.Value);
        }

        private bool TryParseAR(string text, out double value)
        {
            text = text.Replace(',', '.');
            return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private double Lerp(double start, double end, double factor)
        {
            return start + (end - start) * factor;
        }

        private double ApplyDTIfChecked(double arValue, CheckBox checkBox)
        {
            return checkBox.Checked ? ((arValue * 2) + 13) / 3 : arValue;
        }

        private void panelGraph_Paint(object sender, PaintEventArgs e)
        {
            int w = panelGraph.Width;
            int h = panelGraph.Height;
            Graphics g = e.Graphics;

            g.DrawImage(gamma2.Properties.Resources.background, new Rectangle(0, 0, w, h));

            using (SolidBrush borderBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(borderBrush, 0, 0, w, 2);
                g.FillRectangle(borderBrush, 0, h - 2, w, 2);
                g.FillRectangle(borderBrush, 0, 0, 2, h);
                g.FillRectangle(borderBrush, w - 2, 0, 2, h);
            }

            using (Pen pen = new Pen(Color.White, 2))
            {
                Point[] points = new Point[w];
                for (int x = 0; x < w; x++)
                {
                    double normalized = x / (double)(w - 1);

                    float gamma = trackBarGamma.Value / 100f;
                    if (gamma < 0.1f) gamma = 0.1f;

                    int brightness = trackBarBrightness.Value;
                    float contrast = trackBarContrast.Value / 100f;

                    double gammaCorrected = Math.Pow(normalized, 1.0 / gamma);
                    double contrastAdjusted = ((gammaCorrected - 0.5) * contrast) + 0.5;
                    double brightnessAdjusted = contrastAdjusted + (brightness / 255.0);

                    double val = brightnessAdjusted;
                    if (val < 0) val = 0;
                    if (val > 1) val = 1;

                    int y = (int)((1.0 - val) * (h - 1));
                    points[x] = new Point(x, y);
                }

                g.DrawLines(pen, points);
            }
        }

        private async void buttonSaveProfile_Click(object sender, EventArgs e)
        {
            bool wasEmpty = string.IsNullOrWhiteSpace(textBoxProfileName.Text);
            if (wasEmpty)
            {
                if (comboBoxProfiles.SelectedItem == null || string.IsNullOrWhiteSpace(comboBoxProfiles.SelectedItem.ToString()))
                {
                    return;
                }
                textBoxProfileName.Text = comboBoxProfiles.SelectedItem.ToString();
            }

            var profile = GetCurrentSettings();
            string profileName = textBoxProfileName.Text;

            if (profileName.Contains("cfg*"))
            {
                MessageBox.Show("Profile name cannot contain 'cfg*'. Please choose another name.");
                return;
            }

            var existingProfiles = GetSavedProfiles();
            string existingImage = existingProfiles.FirstOrDefault(p => p.Item1 == profileName).Item5 ?? "";
            string profileLine = $"{profileName}|{profile.gamma}|{profile.brightness}|{profile.contrast}";
            if (!string.IsNullOrEmpty(existingImage)) profileLine += $"|{existingImage}";

            try
            {
                var lines = File.Exists(ConfigFilePath) ? File.ReadAllLines(ConfigFilePath).ToList() : new List<string>();
                bool profileExists = lines.Any(line => line.Split('|')[0] == profileName);

                if (profileExists)
                {
                    if (!wasEmpty)
                    {
                        var result = MessageBox.Show("This profile name already exists. Do you want to overwrite it?",
                                                     "Confirm overwrite",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                            return;
                    }

                    lines = lines.Where(line => line.Split('|')[0] != profileName).ToList();
                }

                int cfgIndex = lines.FindIndex(l => l.Trim() == "cfg*");
                if (cfgIndex == -1)
                {
                    lines.Add(profileLine);
                }
                else
                {
                    int insertIdx = cfgIndex;
                    if (cfgIndex > 0 && string.IsNullOrWhiteSpace(lines[cfgIndex - 1])) insertIdx--;
                    lines.Insert(insertIdx, profileLine);
                }

                File.WriteAllLines(ConfigFilePath, lines);

                LoadProfiles();

                buttonSaveProfile.Text = "Saved!";
                await Task.Delay(1000);
                buttonSaveProfile.Text = "Save";
                if (wasEmpty) textBoxProfileName.Text = "";
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No write permissions in the application folder. Move the application to a folder with write permissions (e.g., Desktop).",
                              "Saving Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}");
            }
        }

        private void textBoxProfileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSaveProfile.PerformClick();
                e.SuppressKeyPress = true;
            }
        }


                private void LoadProfileByName(string selectedProfile)
        {
            var profiles = GetSavedProfiles();
            var profile = profiles.FirstOrDefault(p => p.Item1 == selectedProfile);

            if (!profile.Equals(default((string, double, int, int, string))))
            {
                if (profile.Item1 != null)
                {
                    double gamma = Math.Max(0.08, Math.Min(8.88, profile.Item2));
                    int brightness = Math.Max(-888, Math.Min(888, profile.Item3));
                    int contrast = Math.Max(8, Math.Min(888, profile.Item4));

                    trackBarGamma.Value = (int)Math.Round(gamma * 100);
                    trackBarBrightness.Value = brightness;
                    trackBarContrast.Value = contrast;

                    UpdateValueLabels();
                    ApplyAllSettings();
                    panelGraph.Invalidate();
                    
                    _currentCustomImage = profile.Item5;
                    _panelCustomImage.Invalidate();
                    if (comboBoxProfiles.SelectedItem?.ToString() != selectedProfile) comboBoxProfiles.SelectedItem = selectedProfile;
                }
            }
        }

        private async void buttonLoadProfile_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedItem == null) return;

            LoadProfileByName(comboBoxProfiles.SelectedItem.ToString());

            buttonLoadProfile.Text = "Loaded!";
            await Task.Delay(1000);
            buttonLoadProfile.Text = "Load";
        }

                private List<(string, double, int, int, string)> GetSavedProfiles()
        {
            var profiles = new List<(string, double, int, int, string)>();
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    foreach (var line in File.ReadAllLines(ConfigFilePath))
                    {
                        if (line.Trim() == "cfg*") break;

                        var parts = line.Split('|');
                        if (parts.Length >= 4)
                        {
                            if (parts[0].Contains("cfg*"))
                            {
                                MessageBox.Show("Please rename the profile that contains 'cfg*' in its name, as it conflicts with system settings.", "Invalid Profile Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            if (double.TryParse(parts[1], out double g) &&
                                int.TryParse(parts[2], out int b) &&
                                int.TryParse(parts[3], out int c))
                            {
                                string img = parts.Length >= 5 ? parts[4] : "";
                                profiles.Add((parts[0], g, b, c, img));
                            }
                        }
                    }
                }
            }
            catch { }
            return profiles;
        }

        private void LoadProfiles()
        {
            var oldDefaultSel = _viewBonus?.cmbDefaultConfig.SelectedItem;
            comboBoxProfiles.Items.Clear();
            comboBoxProfiles.Items.Add("");
            if (_viewBonus != null) {
                _viewBonus.cmbBonusProfiles.Items.Clear();
                _viewBonus.cmbBonusProfiles.Items.Add("");
                _viewBonus.cmbDefaultConfig.Items.Clear();
                _viewBonus.cmbDefaultConfig.Items.Add("");
            }
            var profiles = GetSavedProfiles();
            foreach (var profile in profiles)
            {
                comboBoxProfiles.Items.Add(profile.Item1);
                if (_viewBonus != null) {
                    _viewBonus.cmbBonusProfiles.Items.Add(profile.Item1);
                    _viewBonus.cmbDefaultConfig.Items.Add(profile.Item1);
                }
            }
            if (_viewBonus != null && oldDefaultSel != null && _viewBonus.cmbDefaultConfig.Items.Contains(oldDefaultSel)) {
                _viewBonus.cmbDefaultConfig.SelectedItem = oldDefaultSel;
            }
            if (_loadRows != null) {
                foreach (var r in _loadRows) {
                    if (r == null || r.ComboProfile == null) continue;
                    var oldSel = r.ComboProfile.SelectedItem;
                    r.ComboProfile.Items.Clear();
                    r.ComboProfile.Items.Add("");
                    foreach (var profile in profiles) {
                        r.ComboProfile.Items.Add(profile.Item1);
                    }
                    if (oldSel != null && r.ComboProfile.Items.Contains(oldSel))
                        r.ComboProfile.SelectedItem = oldSel;
                    else
                        r.ComboProfile.SelectedIndex = -1; // leave empty if nothing was selected
                }
            }
        }

        private void trackBar_Enter_RemoveFocus(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void trackBar_MouseDown_RemoveFocus(object sender, MouseEventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            if (tb == null) return;

            int thumbPos = (int)((tb.Width - 16) * (tb.Value - tb.Minimum) / (double)(tb.Maximum - tb.Minimum)) + 8;

            if (e.X < thumbPos - 8 || e.X > thumbPos + 8)
            {
                this.ActiveControl = null;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBarGamma.Value = (int)Math.Round(numericUpDown1.Value * 100M);
            ApplyAllSettings();
            panelGraph.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBarBrightness.Value = (int)Math.Round(numericUpDown2.Value * 100M);
            ApplyAllSettings();
            panelGraph.Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            trackBarContrast.Value = (int)Math.Round(numericUpDown3.Value * 100M);
            ApplyAllSettings();
            panelGraph.Invalidate();
        }

        private void numericUpDownKeyPress(object sender, KeyPressEventArgs e)
        {
            var nud = sender as NumericUpDown;
            if (nud == null) return;

            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                e.KeyChar = '.';
                if (nud.Text.Contains("."))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void numericUpDownKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void numericUpDownAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                switch (((NumericUpDown)sender).Name)
                {
                    case "numericUpDownAR1":
                        buttonSaveAR1.PerformClick();
                        break;
                    case "numericUpDownAR2":
                        buttonSaveAR2.PerformClick();
                        break;
                    case "numericUpDownTargetAR":
                        buttonSetAR3.PerformClick();
                        break;
                }
            }
        }

        // --- NEW BINDING CODE ---
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        private BindRow _refreshRow, _resetRow, _gammaRow, _brightnessRow, _contrastRow;
        private BindRow[] _loadRows = new BindRow[7];
        private bool _isLoadingSettings = false;
        
        
        private ViewBinds _viewBinds;
        private ViewLoads _viewLoads;
        private ViewBonus _viewBonus;
        
        private Panel _panelCustomImage;

        
        private void DrawTopFade(object sender, PaintEventArgs e)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Point(0, 0), new Point(0, 4), Color.White, Color.Black))
            {
                e.Graphics.FillRectangle(brush, 0, 0, ((Control)sender).Width, 4);
            }
        }

        private void panelCustomImage_Paint(object sender, PaintEventArgs e)
        {
            int w = _panelCustomImage.Width;
            int h = _panelCustomImage.Height;
            Graphics g = e.Graphics;

            string imgPath = Path.Combine(Application.StartupPath, "slave.png");
            if (!string.IsNullOrEmpty(_currentCustomImage))
            {
                string cImg = _currentCustomImage.Trim();
                if (!cImg.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) cImg += ".png";
                string customPath = Path.Combine(Application.StartupPath, cImg);
                if (File.Exists(customPath)) imgPath = customPath;
            }
            if (File.Exists(imgPath))
            {
                try
                {
                    using (Image img = Image.FromFile(imgPath))
                    {
                        g.DrawImage(img, new Rectangle(0, 0, w, h));
                    }
                }
                catch { }
            }
            
            if (_viewBonus != null)
            {
                int shadowWidth = _viewBonus.OutlineWidth;
                int softness = _viewBonus.OutlineSoftness;
                int opacity = _viewBonus.OutlineOpacity;

                if (shadowWidth > 0 && opacity > 0)
                {
                    if (shadowWidth * 2 > w) shadowWidth = w / 2;
                    if (shadowWidth * 2 > h) shadowWidth = h / 2;

                    float solidPos = 1.0f - (softness / 100f);
                    if (solidPos >= 0.999f) solidPos = 0.999f;
                    if (solidPos <= 0.001f) solidPos = 0.001f;

                    Color cSolid = Color.FromArgb(opacity, 0, 0, 0);
                    Color cTrans = Color.FromArgb(0, 0, 0, 0);

                    System.Drawing.Drawing2D.ColorBlend blend = new System.Drawing.Drawing2D.ColorBlend();
                    blend.Positions = new float[] { 0.0f, solidPos, 1.0f };
                    blend.Colors = new Color[] { cSolid, cSolid, cTrans };

                    // Top Edge
                    if (w - 2 * shadowWidth > 0)
                    {
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                            new Point(0, 0), new Point(0, shadowWidth), cSolid, cTrans))
                        {
                            brush.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;
                            brush.InterpolationColors = blend;
                            g.FillRectangle(brush, shadowWidth, 0, w - 2 * shadowWidth, shadowWidth);
                        }
                        // Bottom Edge
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                            new Point(0, h - 1), new Point(0, h - 1 - shadowWidth), cSolid, cTrans))
                        {
                            brush.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;
                            brush.InterpolationColors = blend;
                            g.FillRectangle(brush, shadowWidth, h - shadowWidth, w - 2 * shadowWidth, shadowWidth);
                        }
                    }

                    // Left Edge
                    if (h - 2 * shadowWidth > 0)
                    {
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                            new Point(0, 0), new Point(shadowWidth, 0), cSolid, cTrans))
                        {
                            brush.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;
                            brush.InterpolationColors = blend;
                            g.FillRectangle(brush, 0, shadowWidth, shadowWidth, h - 2 * shadowWidth);
                        }
                        // Right Edge
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                            new Point(w - 1, 0), new Point(w - 1 - shadowWidth, 0), cSolid, cTrans))
                        {
                            brush.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;
                            brush.InterpolationColors = blend;
                            g.FillRectangle(brush, w - shadowWidth, shadowWidth, shadowWidth, h - 2 * shadowWidth);
                        }
                    }

                    Action<int, int, int, int> drawCorner = (cx, cy, rx, ry) =>
                    {
                        using (System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            p.AddEllipse(cx - shadowWidth, cy - shadowWidth, shadowWidth * 2, shadowWidth * 2);
                            using (var pgb = new System.Drawing.Drawing2D.PathGradientBrush(p))
                            {
                                pgb.CenterPoint = new PointF(cx, cy);
                                System.Drawing.Drawing2D.ColorBlend pBlend = new System.Drawing.Drawing2D.ColorBlend();
                                pBlend.Positions = new float[] { 0.0f, solidPos, 1.0f };
                                pBlend.Colors = new Color[] { cSolid, cSolid, cTrans };
                                pgb.InterpolationColors = pBlend;
                                g.FillRectangle(pgb, rx, ry, shadowWidth, shadowWidth);
                            }
                        }
                    };

                    drawCorner(shadowWidth, shadowWidth, 0, 0); // Top-Left
                    drawCorner(w - 1 - shadowWidth, shadowWidth, w - shadowWidth, 0); // Top-Right
                    drawCorner(shadowWidth, h - 1 - shadowWidth, 0, h - shadowWidth); // Bottom-Left
                    drawCorner(w - 1 - shadowWidth, h - 1 - shadowWidth, w - shadowWidth, h - shadowWidth); // Bottom-Right
                }
            }
            
            using (SolidBrush borderBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(borderBrush, 0, 0, w, 2);
                g.FillRectangle(borderBrush, 0, h - 2, w, 2);
                g.FillRectangle(borderBrush, 0, 0, 2, h);
                g.FillRectangle(borderBrush, w - 2, 0, 2, h);
            }
        }

        private void ToggleARControls(bool showAR)
        {
            buttonSaveAR1.Visible = showAR;
            numericUpDownAR1.Visible = showAR;
            labelAR1.Visible = showAR;
            checkBoxDT1.Visible = showAR;
            buttonSaveAR2.Visible = showAR;
            numericUpDownAR2.Visible = showAR;
            labelAR2.Visible = showAR;
            checkBoxDT2.Visible = showAR;
            buttonSetAR3.Visible = showAR;
            numericUpDownTargetAR.Visible = showAR;
            labelAR3.Visible = showAR;
            checkBoxDT3.Visible = showAR;
            
            _panelCustomImage.Visible = !showAR;
        }

        private void SwitchView(Control viewToShow)
        {
            this.ActiveControl = null;
            _panelMain.Visible = false;
            _viewBinds.Visible = false;
            _viewLoads.Visible = false;
            _viewBonus.Visible = false;
            viewToShow.Visible = true;
            this.ActiveControl = null;
        }

        private void SetupViews()
        {
            
            _viewBinds = new ViewBinds { Location = new Point(0, 26), Visible = false };
            _viewLoads = new ViewLoads { Location = new Point(0, 26), Visible = false };
            _viewBonus = new ViewBonus { Location = new Point(0, 26), Visible = false };

            _viewBonus.chkDefaultOnStartup.CheckedChanged += (s, e) => SaveConfig();
            _viewBonus.cmbDefaultConfig.SelectedIndexChanged += (s, e) => SaveConfig();
            _viewBonus.chkResetOnExit.CheckedChanged += (s, e) => SaveConfig();

            _viewBonus.VignetteChanged += (s, e) => {
                if (_panelCustomImage != null) _panelCustomImage.Invalidate();
                SaveConfig();
            };
            
            _viewBonus.RenameProfileClicked += async (s, e) => {
                string sel = _viewBonus.cmbBonusProfiles.SelectedItem?.ToString();
                string newName = _viewBonus.tbBonusRename.Text.Trim();
                if (string.IsNullOrEmpty(sel) || string.IsNullOrEmpty(newName)) return;
                if (newName.Contains("cfg*")) { MessageBox.Show("Profile name cannot contain 'cfg*'."); return; }
                
                _lastAction = "rename";
                _lastRenamedFrom = sel;
                _lastRenamedTo = newName;
                var lines = File.ReadAllLines(ConfigFilePath).ToList();
                for (int i=0; i<lines.Count; i++) {
                    if (lines[i].StartsWith(sel + "|")) {
                        var parts = lines[i].Split('|');
                        parts[0] = newName;
                        lines[i] = string.Join("|", parts);
                    }
                }
                File.WriteAllLines(ConfigFilePath, lines);
                
                if (_loadRows != null) {
                    foreach (var r in _loadRows) {
                        if (r.ComboProfile != null && r.ComboProfile.SelectedItem?.ToString() == sel) {
                            r.ComboProfile.Items.Add(newName);
                            r.ComboProfile.SelectedItem = newName;
                        }
                    }
                    SaveConfig();
                }
                
                LoadProfiles();
                _viewBonus.cmbBonusProfiles.SelectedItem = newName;
                _viewBonus.tbBonusRename.Text = "";
                
                _viewBonus.btnBonusRename.Text = "Renamed!";
                await Task.Delay(1000);
                _viewBonus.btnBonusRename.Text = "Rename";
            };
            
            _viewBonus.DeleteProfileClicked += async (s, e) => {
                string sel = _viewBonus.cmbBonusProfiles.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(sel)) return;
                
                if (_viewBonus.btnBonusDelete.Text == "Delete") {
                    _viewBonus.btnBonusDelete.Text = "Sure?";
                    await Task.Delay(2000);
                    if (_viewBonus.btnBonusDelete.Text == "Sure?") _viewBonus.btnBonusDelete.Text = "Delete";
                    return;
                }
                
                if (_viewBonus.btnBonusDelete.Text == "Sure?") {
                    var lines = File.ReadAllLines(ConfigFilePath).ToList();
                    for (int i=0; i<lines.Count; i++) {
                        if (lines[i].StartsWith(sel + "|")) {
                            _lastDeletedProfileLine = lines[i];
                            lines.RemoveAt(i);
                            break;
                        }
                    }
                    File.WriteAllLines(ConfigFilePath, lines);
                    _lastAction = "delete";
                    _lastDeletedProfilePresets.Clear();
                    if (_loadRows != null) {
                        for(int j=0; j<_loadRows.Length; j++) {
                            if (_loadRows[j].ComboProfile.SelectedItem?.ToString() == sel) {
                                _lastDeletedProfilePresets.Add(j);
                            }
                        }
                    }
                    LoadProfiles();
                    
                    _viewBonus.btnBonusDelete.Text = "Deleted!";
                    await Task.Delay(1000);
                    _viewBonus.btnBonusDelete.Text = "Delete";
                }
            };
            
            _viewBonus.RevertProfileClicked += async (s, e) => {
                if (string.IsNullOrEmpty(_lastAction)) return;
                
                if (_lastAction == "delete") {
                    if (string.IsNullOrEmpty(_lastDeletedProfileLine)) return;
                    
                    var lines = File.ReadAllLines(ConfigFilePath).ToList();
                    lines.Insert(0, _lastDeletedProfileLine);
                    File.WriteAllLines(ConfigFilePath, lines);
                    LoadProfiles();
                    string restoredName = _lastDeletedProfileLine.Split('|')[0];
                    _viewBonus.cmbBonusProfiles.SelectedItem = restoredName;
                    
                    if (_loadRows != null) {
                        foreach (int j in _lastDeletedProfilePresets) {
                            if (j >= 0 && j < _loadRows.Length) {
                                _loadRows[j].ComboProfile.SelectedItem = restoredName;
                            }
                        }
                        SaveConfig();
                    }
                    _lastDeletedProfileLine = "";
                }
                else if (_lastAction == "rename") {
                    if (string.IsNullOrEmpty(_lastRenamedFrom) || string.IsNullOrEmpty(_lastRenamedTo)) return;
                    
                    var lines = File.ReadAllLines(ConfigFilePath).ToList();
                    for (int i=0; i<lines.Count; i++) {
                        if (lines[i].StartsWith(_lastRenamedTo + "|")) {
                            var parts = lines[i].Split('|');
                            parts[0] = _lastRenamedFrom;
                            lines[i] = string.Join("|", parts);
                        }
                    }
                    File.WriteAllLines(ConfigFilePath, lines);
                    
                    if (_loadRows != null) {
                        foreach (var r in _loadRows) {
                            if (r.ComboProfile != null && r.ComboProfile.SelectedItem?.ToString() == _lastRenamedTo) {
                                r.ComboProfile.Items.Add(_lastRenamedFrom);
                                r.ComboProfile.SelectedItem = _lastRenamedFrom;
                            }
                        }
                        SaveConfig();
                    }
                    LoadProfiles();
                    _viewBonus.cmbBonusProfiles.SelectedItem = _lastRenamedFrom;
                    
                    _lastRenamedFrom = "";
                    _lastRenamedTo = "";
                }
                
                _lastAction = "";
                
                _viewBonus.btnBonusRevert.Text = "Undone!";
                await Task.Delay(1000);
                _viewBonus.btnBonusRevert.Text = "Undo";
            };
            
            _viewBonus.SaveImageClicked += async (s, e) => {
                string sel = _viewBonus.cmbBonusProfiles.SelectedItem?.ToString();
                string imgName = _viewBonus.tbBonusImage.Text.Trim();
                if (string.IsNullOrEmpty(sel)) return;
                
                var lines = File.ReadAllLines(ConfigFilePath).ToList();
                for (int i=0; i<lines.Count; i++) {
                    if (lines[i].StartsWith(sel + "|")) {
                        var parts = lines[i].Split('|').ToList();
                        if (parts.Count < 5) parts.Add(imgName);
                        else parts[4] = imgName;
                        lines[i] = string.Join("|", parts);
                    }
                }
                File.WriteAllLines(ConfigFilePath, lines);
                LoadProfiles();
                _viewBonus.cmbBonusProfiles.SelectedItem = sel;
                
                _viewBonus.tbBonusImage.Text = "";
                if (comboBoxProfiles.SelectedItem?.ToString() == sel) {
                    LoadProfileByName(sel);
                }
                
                _viewBonus.btnBonusSaveImage.Text = "Saved!";
                await Task.Delay(1000);
                _viewBonus.btnBonusSaveImage.Text = "Save";
            };

            
            _viewBonus.cmbBonusProfiles.SelectedIndexChanged += (s, e) => {
                string sel = _viewBonus.cmbBonusProfiles.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(sel)) { _viewBonus.tbBonusImage.Text = ""; return; }
                var profiles = GetSavedProfiles();
                var p = profiles.FirstOrDefault(x => x.Item1 == sel);
                _viewBonus.tbBonusImage.Text = p.Item5 ?? "";
            };

            _viewBonus.BonusToggleChanged += (s, e) => {
                ToggleARControls(!_viewBonus.EnableCustomPNG);
                SaveConfig();
            };

            this.Controls.Add(_panelMain);
            this.Controls.Add(_viewBinds);
            this.Controls.Add(_viewLoads);
            this.Controls.Add(_viewBonus);
            this._viewBinds.BringToFront();
            this._viewLoads.BringToFront();
            this._viewBonus.BringToFront();
            this._panelMain.BringToFront();



            
            _panelMain.Paint += DrawTopFade;
            _viewBinds.Paint += DrawTopFade;
            _viewLoads.Paint += DrawTopFade;
            _viewBonus.Paint += DrawTopFade;

            _btnViewMain.Click += (s, e) => SwitchView(_panelMain);
            _btnViewBinds.Click += (s, e) => SwitchView(_viewBinds);
            _btnViewLoads.Click += (s, e) => SwitchView(_viewLoads);
            _btnViewBonus.Click += (s, e) => SwitchView(_viewBonus);


            

            _panelCustomImage = new Panel();
            _panelCustomImage.Location = new Point(9, 98);
            _panelCustomImage.Size = new Size(258, 86);
            _panelCustomImage.Visible = false;
            _panelCustomImage.Paint += panelCustomImage_Paint;
            _panelCustomImage.Click += (s,e) => { this.ActiveControl = null; };
            _panelMain.Controls.Add(_panelCustomImage);
            _panelCustomImage.BringToFront();
        }

        private void AttachDefocus(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (!(c is TextBox || c is NumericUpDown || c is CheckBox || c is Button || c is ComboBox || c is TrackBar))
                {
                    c.Click += (s, e) =>
                    {
                        Form f = c.FindForm();
                        if (f != null) f.ActiveControl = null;
                    };
                }
                if (c.HasChildren)
                {
                    AttachDefocus(c);
                }
            }
        }

        private void SetupBindsUI()
        {
            this.Click += (s, e) => this.ActiveControl = null;
            this.Shown += (s, e) => this.ActiveControl = null;
            this.ClientSize = new Size(382, 247); // Shrink window to original-like dimensions with tabs
            
            _gammaRow = _viewBinds.GammaRow;
            _brightnessRow = _viewBinds.BrightnessRow;
            _contrastRow = _viewBinds.ContrastRow;
            _resetRow = _viewBinds.ResetRow;
            _refreshRow = _viewBinds.RefreshRow;
            
            PopulateBindPanel(_gammaRow.PanelDown, new string[0]);
            PopulateBindPanel(_gammaRow.PanelUp, new string[0]);
            PopulateBindPanel(_brightnessRow.PanelDown, new string[0]);
            PopulateBindPanel(_brightnessRow.PanelUp, new string[0]);
            PopulateBindPanel(_contrastRow.PanelDown, new string[0]);
            PopulateBindPanel(_contrastRow.PanelUp, new string[0]);
            PopulateBindPanel(_resetRow.PanelDown, new string[0]);
            PopulateBindPanel(_refreshRow.PanelDown, new string[0]);

            _viewBinds.btnGammaLock.Click += (s, e) => { ToggleNudLocked(_viewBinds.btnGammaLock, _gammaRow.NumericValue, _gammaRow.CheckBoxActive); SaveConfig(); };
            _viewBinds.btnBrightnessLock.Click += (s, e) => { ToggleNudLocked(_viewBinds.btnBrightnessLock, _brightnessRow.NumericValue, _brightnessRow.CheckBoxActive); SaveConfig(); };
            _viewBinds.btnContrastLock.Click += (s, e) => { ToggleNudLocked(_viewBinds.btnContrastLock, _contrastRow.NumericValue, _contrastRow.CheckBoxActive); SaveConfig(); };


            for (int i=0; i<7; i++) {
                _loadRows[i] = _viewLoads.LoadRows[i];
                PopulateBindPanel(_loadRows[i].PanelDown, new string[0]);
            }

            var standardRows = new[] { _gammaRow, _brightnessRow, _contrastRow, _resetRow, _refreshRow };
            foreach (var r in standardRows) {
                if (r.CheckBoxActive != null) r.CheckBoxActive.CheckedChanged += (s, e) => SaveConfig();
                if (r.NumericValue != null) r.NumericValue.ValueChanged += (s, e) => SaveConfig();
            }
            
            if (_loadRows != null) {
                foreach (var r in _loadRows) {
                    if (r == null) continue;
                    if (r.CheckBoxActive != null) r.CheckBoxActive.CheckedChanged += (s, e) => { if (!_isConsolidating) SaveConfig(); };
                    if (r.ComboProfile != null) r.ComboProfile.SelectedIndexChanged += (s, e) => ConsolidateLoadRows();
                }
            }
            
            if (!File.ReadAllText(ConfigFilePath).Contains("cfg*"))
            {
                SaveConfig();
            }
        }

        private void PopulateBindPanel(FlowLayoutPanel pnl, string[] defaultBinds)
        {
            var btnAdd = new Button { Text = "+", Width = 23, Height = 23, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter, UseCompatibleTextRendering = true, Padding = new Padding(0, 0, 2, 1), Margin = new Padding(3, 2, 0, 3) };
            var btnSub = new Button { Text = "-", Width = 23, Height = 23, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter, UseCompatibleTextRendering = true, Padding = new Padding(0, 0, 2, 1), Margin = new Padding(2, 2, 0, 3) };
            
            pnl.Controls.Add(btnAdd);
            pnl.Controls.Add(btnSub);
            
            var btnLock = new Button { Text = "\u26BF", Width = 24, Height = 23, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter, Padding = new Padding(0), Margin = new Padding(2, 2, 0, 3) };
            pnl.Controls.Add(btnLock);
            
            btnLock.Click += (s, e) => {
                bool willLock = btnLock.Text == "\u26BF";
                SetPanelLocked(pnl, willLock);
                SaveConfig();
            };
            
            btnAdd.Click += (s, e) => {
                  if (IsPanelLocked(pnl)) return;
                  var textboxes = pnl.Controls.OfType<TextBox>().ToList();
                if (textboxes.Count < 3)
                {
                    pnl.Width += textboxes.Count == 2 ? 50 : 51; 
                    AddTextBoxToPanel(pnl, "");
                    if (IsLoadRowPanel(pnl)) ConsolidateLoadRows(); else SaveConfig();
                }
                var newCount = pnl.Controls.OfType<TextBox>().Count();
                if (newCount >= 3) { btnAdd.Visible = false; btnSub.Margin = new Padding(3, 2, 0, 3); }
                if (newCount > 1) btnSub.Visible = true;
                ReorderButtons(pnl);
            };
            
            btnSub.Click += (s, e) => {
                  if (IsPanelLocked(pnl)) return;
                  var textboxes = pnl.Controls.OfType<TextBox>().ToList();
                if (textboxes.Count > 1)
                {
                    pnl.Controls.Remove(textboxes.Last());
                    pnl.Width -= textboxes.Count == 3 ? 50 : 51;
                    btnAdd.Visible = true;
                    btnSub.Margin = new Padding(2, 2, 0, 3);
                    if (IsLoadRowPanel(pnl)) ConsolidateLoadRows(); else SaveConfig();
                }
                var newCount = pnl.Controls.OfType<TextBox>().Count();
                if (newCount <= 1) btnSub.Visible = false;
                ReorderButtons(pnl);
            };
            
            if (defaultBinds == null || defaultBinds.Length == 0) defaultBinds = new[] { "" };
            foreach (var b in defaultBinds.Take(3))
            {
                AddTextBoxToPanel(pnl, b);
                var c = pnl.Controls.OfType<TextBox>().Count();
                if (c == 2) pnl.Width += 51;
                else if (c == 3) pnl.Width += 50;
            }
            var finalCount = pnl.Controls.OfType<TextBox>().Count();
            if (finalCount >= 3) { btnAdd.Visible = false; btnSub.Margin = new Padding(3, 2, 0, 3); }
            if (finalCount <= 1) btnSub.Visible = false;
            else btnSub.Visible = true;
            ReorderButtons(pnl);
        }

        private void ReorderButtons(FlowLayoutPanel pnl)
        {
            var ba = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "+");
            var bs = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "-");
            var bl = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "\u26BF" || b.Text == "\u26CB");
            if (ba != null) pnl.Controls.SetChildIndex(ba, pnl.Controls.Count - 1);
            if (bs != null) pnl.Controls.SetChildIndex(bs, pnl.Controls.Count - 1);
            if (bl != null) pnl.Controls.SetChildIndex(bl, pnl.Controls.Count - 1);
        }
        
        private bool IsLoadRowPanel(FlowLayoutPanel pnl)
        {
            if (_loadRows == null) return false;
            foreach (var r in _loadRows) {
                if (r != null && r.PanelDown == pnl) return true;
            }
            return false;
        }

        
        private bool IsPanelLocked(FlowLayoutPanel pnl)
        {
            var btnLock = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "\u26BF" || b.Text == "\u26CB");
            return btnLock != null && btnLock.Text == "\u26CB";
        }

        private void SetNudLocked(Button btnLock, NumericUpDown nud, CheckBox chk, bool locked)
        {
            btnLock.Text = locked ? "\u26CB" : "\u26BF";
            nud.Enabled = !locked;
        }

        private void ToggleNudLocked(Button btnLock, NumericUpDown nud, CheckBox chk)
        {
            bool locked = btnLock.Text != "\u26CB";
            SetNudLocked(btnLock, nud, chk, locked);
        }

        private void SetPanelLocked(FlowLayoutPanel pnl, bool locked)
        {
            var btnLock = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "\u26BF" || b.Text == "\u26CB");
            if (btnLock != null)
            {
                btnLock.Text = locked ? "\u26CB" : "\u26BF";
                foreach(var ctrl in pnl.Controls.OfType<TextBox>()) ctrl.Enabled = !locked;
                var btnAdd = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "+");
                var btnSub = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "-");
                if (btnAdd != null) btnAdd.ForeColor = locked ? Color.Gray : Color.White;
                if (btnSub != null) btnSub.ForeColor = locked ? Color.Gray : Color.White;
                
                var standardRows = new[] { _gammaRow, _brightnessRow, _contrastRow, _resetRow, _refreshRow };
                var allRows = standardRows.Concat(_loadRows ?? new BindRow[0]).Where(r => r != null);
                var row = allRows.FirstOrDefault(r => r.PanelDown == pnl || r.PanelUp == pnl);
                if (row != null)
                {
                    if (row.PanelDown == pnl && row.LabelDown != null)
                    {
                        row.LabelDown.ForeColor = locked ? Color.Gray : Color.White;
                    }
                    else if (row.PanelUp == pnl && row.LabelUp != null)
                    {
                        row.LabelUp.ForeColor = locked ? Color.Gray : Color.White;
                    }

                    if (row.ComboProfile != null)
                    {
                        row.ComboProfile.Enabled = !locked;
                    }
                }
            }
        }

        private void AddTextBoxToPanel(FlowLayoutPanel pnl, string text)
        {
            
            if (text.Length == 1 && text[0] >= 'a' && text[0] <= 'z') text = text.ToUpper();
            if (text == "alt") text = "Alt";
            if (text == "ctrl") text = "Ctrl";
            if (text == "shift") text = "Shift";
            if (text == "win") text = "Win";
            int count = pnl.Controls.OfType<TextBox>().Count();
            var tb = new TextBox { Width = count == 2 ? 46 : 47, Text = text };
            tb.Margin = new Padding(4, 2, 0, 3);
            tb.ForeColor = Color.Black;
            tb.BackColor = Color.White;
            tb.BorderStyle = BorderStyle.Fixed3D;
            tb.Font = new Font(this.Font, FontStyle.Regular);

            tb.KeyDown += (s, e) => {
                e.SuppressKeyPress = true;
                e.Handled = true;
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) {
                    pnl.FindForm().ActiveControl = null;
                    return;
                }
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) {
                    tb.Text = "";
                    pnl.FindForm().ActiveControl = null;
                    return;
                }
                string keyName = e.KeyCode.ToString();
                switch (e.KeyCode) {
                    case Keys.ControlKey: case Keys.LControlKey: case Keys.RControlKey: keyName = "Ctrl"; break;
                    case Keys.Menu: case Keys.LMenu: case Keys.RMenu: keyName = "Alt"; break;
                    case Keys.ShiftKey: case Keys.LShiftKey: case Keys.RShiftKey: keyName = "Shift"; break;
                    case Keys.LWin: case Keys.RWin: keyName = "Win"; break;
                    case Keys.OemMinus: keyName = "-"; break;
                    case Keys.Oemplus: keyName = "="; break;
                    case Keys.OemOpenBrackets: keyName = "["; break;
                    case Keys.OemCloseBrackets: keyName = "]"; break;
                    case Keys.OemSemicolon: keyName = ";"; break;
                    case Keys.OemQuotes: keyName = "'"; break;
                    case Keys.Oemcomma: keyName = ","; break;
                    case Keys.OemPeriod: keyName = "."; break;
                    case Keys.OemQuestion: keyName = "/"; break;
                    case Keys.OemBackslash: keyName = "\\"; break;
                    case Keys.Oemtilde: keyName = "`"; break;
                    case (Keys)255: keyName = "Fn"; break;
                }
                if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) {
                    keyName = (e.KeyCode - Keys.D0).ToString();
                } else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) {
                    keyName = "NumPad" + (e.KeyCode - Keys.NumPad0).ToString();
                }
                tb.Text = keyName;
                pnl.FindForm().ActiveControl = null;
            };

            tb.TextChanged += (s, e) => { 
                if (IsBindDuplicate(pnl)) {
                    MessageBox.Show("This hotkey is already assigned to another function!", "Duplicate Hotkey", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tb.Text = "";
                    return;
                }
                if (IsLoadRowPanel(pnl)) ConsolidateLoadRows(); 
                else SaveConfig(); 
            };
            pnl.Controls.Add(tb);
            
            // Move buttons to end explicitly to preserve order
            var btnAdd = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "+");
            var btnSub = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "-");
            var btnLock = pnl.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "\u26BF" || b.Text == "\u26CB");
            
            if (btnAdd != null) pnl.Controls.SetChildIndex(btnAdd, pnl.Controls.Count - 1);
            if (btnSub != null) pnl.Controls.SetChildIndex(btnSub, pnl.Controls.Count - 1);
            if (btnLock != null) pnl.Controls.SetChildIndex(btnLock, pnl.Controls.Count - 1);
            if (IsPanelLocked(pnl)) tb.Enabled = false;
        }

        private void LoadSettings()
        {
            _isLoadingSettings = true;
            if (File.Exists(ConfigFilePath))
            {
                bool reachedSettings = false;
                foreach (var line in File.ReadAllLines(ConfigFilePath))
                {
                    if (line.Trim() == "cfg*") { reachedSettings = true; continue; }
                    if (!reachedSettings) continue;
                    
                    var parts = line.Split(new[] { ':' }, 2);
                    if (parts.Length == 2)
                    {
                        ApplySettingToUI(parts[0].Trim(), parts[1].Trim());
                    }
                }
            }
            _isLoadingSettings = false;
        }

        private void ApplySettingToUI(string key, string val)
        {
            try {
                switch (key)
                {
                    case "refresh": SetBinds(_refreshRow.PanelDown, val); break;
                    case "reset": SetBinds(_resetRow.PanelDown, val); break;
                    case "load_1_bind": SetBinds(_loadRows[0].PanelDown, val); break;
                    case "load_1_check": _loadRows[0].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_1_profile": if (_loadRows[0].ComboProfile.Items.Contains(val)) _loadRows[0].ComboProfile.SelectedItem = val; break;
                    case "load_2_bind": SetBinds(_loadRows[1].PanelDown, val); break;
                    case "load_2_check": _loadRows[1].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_2_profile": if (_loadRows[1].ComboProfile.Items.Contains(val)) _loadRows[1].ComboProfile.SelectedItem = val; break;
                    case "load_3_bind": SetBinds(_loadRows[2].PanelDown, val); break;
                    case "load_3_check": _loadRows[2].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_3_profile": if (_loadRows[2].ComboProfile.Items.Contains(val)) _loadRows[2].ComboProfile.SelectedItem = val; break;
                    case "load_4_bind": SetBinds(_loadRows[3].PanelDown, val); break;
                    case "load_4_check": _loadRows[3].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_4_profile": if (_loadRows[3].ComboProfile.Items.Contains(val)) _loadRows[3].ComboProfile.SelectedItem = val; break;
                    case "load_5_bind": SetBinds(_loadRows[4].PanelDown, val); break;
                    case "load_5_check": _loadRows[4].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_5_profile": if (_loadRows[4].ComboProfile.Items.Contains(val)) _loadRows[4].ComboProfile.SelectedItem = val; break;
                    case "load_6_bind": SetBinds(_loadRows[5].PanelDown, val); break;
                    case "load_6_check": _loadRows[5].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_6_profile": if (_loadRows[5].ComboProfile.Items.Contains(val)) _loadRows[5].ComboProfile.SelectedItem = val; break;
                    case "load_7_bind": SetBinds(_loadRows[6].PanelDown, val); break;
                    case "load_7_check": _loadRows[6].CheckBoxActive.Checked = (val == "1"); break;
                    case "load_7_profile": if (_loadRows[6].ComboProfile.Items.Contains(val)) _loadRows[6].ComboProfile.SelectedItem = val; break;
                    case "enable_custom_png": _viewBonus.EnableCustomPNG = (val == "1"); break;
                    case "outline_width": if (int.TryParse(val, out int w_val)) _viewBonus.OutlineWidth = w_val; break;
                    case "outline_softness": if (int.TryParse(val, out int s_val)) _viewBonus.OutlineSoftness = s_val; break;
                    case "outline_opacity": if (int.TryParse(val, out int o_val)) _viewBonus.OutlineOpacity = o_val; break;
                    case "default_on_startup": _viewBonus.chkDefaultOnStartup.Checked = (val == "1"); break;
                    case "reset_on_exit": _viewBonus.chkResetOnExit.Checked = (val == "1"); break;
                    case "default_config": if (_viewBonus.cmbDefaultConfig.Items.Contains(val)) _viewBonus.cmbDefaultConfig.SelectedItem = val; break;
                    case "gamma_value": if (decimal.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal gv)) _gammaRow.NumericValue.Value = gv; break;
                    case "brightness_value": if (decimal.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal bv)) _brightnessRow.NumericValue.Value = bv; break;
                    case "contrast_value": if (decimal.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cv)) _contrastRow.NumericValue.Value = cv; break;
                    case "gamma_value_locked": SetNudLocked(_viewBinds.btnGammaLock, _gammaRow.NumericValue, _gammaRow.CheckBoxActive, val == "True"); break;
                    case "brightness_value_locked": SetNudLocked(_viewBinds.btnBrightnessLock, _brightnessRow.NumericValue, _brightnessRow.CheckBoxActive, val == "True"); break;
                    case "contrast_value_locked": SetNudLocked(_viewBinds.btnContrastLock, _contrastRow.NumericValue, _contrastRow.CheckBoxActive, val == "True"); break;
                    case "gamma_down_locked": SetPanelLocked(_gammaRow.PanelDown, val == "True"); break;
                    case "gamma_up_locked": SetPanelLocked(_gammaRow.PanelUp, val == "True"); break;
                    case "brightness_down_locked": SetPanelLocked(_brightnessRow.PanelDown, val == "True"); break;
                    case "brightness_up_locked": SetPanelLocked(_brightnessRow.PanelUp, val == "True"); break;
                    case "contrast_down_locked": SetPanelLocked(_contrastRow.PanelDown, val == "True"); break;
                    case "contrast_up_locked": SetPanelLocked(_contrastRow.PanelUp, val == "True"); break;
                    case "reset_down_locked": SetPanelLocked(_resetRow.PanelDown, val == "True"); break;
                    case "refresh_down_locked": SetPanelLocked(_refreshRow.PanelDown, val == "True"); break;
                    case "load_1_locked": SetPanelLocked(_loadRows[0].PanelDown, val == "True"); break;
                    case "load_2_locked": SetPanelLocked(_loadRows[1].PanelDown, val == "True"); break;
                    case "load_3_locked": SetPanelLocked(_loadRows[2].PanelDown, val == "True"); break;
                    case "load_4_locked": SetPanelLocked(_loadRows[3].PanelDown, val == "True"); break;
                    case "load_5_locked": SetPanelLocked(_loadRows[4].PanelDown, val == "True"); break;
                    case "load_6_locked": SetPanelLocked(_loadRows[5].PanelDown, val == "True"); break;
                    case "load_7_locked": SetPanelLocked(_loadRows[6].PanelDown, val == "True"); break;
                    case "gamma_down": SetBinds(_gammaRow.PanelDown, val); break;
                    case "gamma_up": SetBinds(_gammaRow.PanelUp, val); break;
                    case "brightness_down": SetBinds(_brightnessRow.PanelDown, val); break;
                    case "brightness_up": SetBinds(_brightnessRow.PanelUp, val); break;
                    case "contrast_down": SetBinds(_contrastRow.PanelDown, val); break;
                    case "contrast_up": SetBinds(_contrastRow.PanelUp, val); break;
                    case "refresh_check": _refreshRow.CheckBoxActive.Checked = (val == "1"); break;
                    case "reset_check": _resetRow.CheckBoxActive.Checked = (val == "1"); break;
                    case "gamma_check": _gammaRow.CheckBoxActive.Checked = (val == "1"); break;
                    case "brightness_check": _brightnessRow.CheckBoxActive.Checked = (val == "1"); break;
                    case "contrast_check": _contrastRow.CheckBoxActive.Checked = (val == "1"); break;
                }
            } catch { }
        }

        private void SetBinds(FlowLayoutPanel pnl, string val)
        {
            var tbs = pnl.Controls.OfType<TextBox>().ToList();
            foreach (var tb in tbs) pnl.Controls.Remove(tb);
            
            var binds = new List<string>();
            string[] parts = val.Split('+');
            for (int i = 0; i < parts.Length; i++)
            {
                string p = parts[i].Trim();
                if (string.IsNullOrEmpty(p))
                {
                    if (i > 0 && i == parts.Length - 1 && string.IsNullOrEmpty(parts[i - 1]))
                    {
                        binds.Add("+");
                    }
                    continue;
                }
                binds.Add(p);
            }
            if (binds.Count == 0) binds.Add("");
            
            var toAdd = binds.Take(3).ToList();
            if (toAdd.Count == 1) pnl.Width = 160;
            else if (toAdd.Count == 2) pnl.Width = 211;
            else pnl.Width = 261;
            
            foreach (var b in toAdd) AddTextBoxToPanel(pnl, b);
            
            var btnAdd = pnl.Controls.OfType<Button>().First(b => b.Text == "+");
            var btnSub = pnl.Controls.OfType<Button>().First(b => b.Text == "-");
            
            int count = pnl.Controls.OfType<TextBox>().Count();
            btnAdd.Visible = count < 3;
            btnSub.Visible = count > 1;
            
            if (count >= 3) btnSub.Margin = new Padding(3, 2, 0, 3);
            else btnSub.Margin = new Padding(2, 2, 0, 3);
            
            ReorderButtons(pnl);
        }

        private bool _isConsolidating = false;
        
        private bool IsBindDuplicate(FlowLayoutPanel sourcePnl)
        {
            string currentBind = string.Join("+", sourcePnl.Controls.OfType<TextBox>().Select(tb => tb.Text.Trim()).Where(t => t.Length > 0));
            if (string.IsNullOrEmpty(currentBind)) return false;

            var standardRows = new[] { _gammaRow, _brightnessRow, _contrastRow, _resetRow, _refreshRow };
            foreach (var r in standardRows) {
                if (r == null) continue;
                if (r.PanelDown != null && r.PanelDown != sourcePnl) {
                    if (string.Join("+", r.GetDownBinds()) == currentBind) return true;
                }
                if (r.PanelUp != null && r.PanelUp != sourcePnl) {
                    if (string.Join("+", r.GetUpBinds()) == currentBind) return true;
                }
            }
            if (_loadRows != null) {
                foreach (var r in _loadRows) {
                    if (r == null) continue;
                    if (r.PanelDown != null && r.PanelDown != sourcePnl) {
                        if (string.Join("+", r.GetDownBinds()) == currentBind) return true;
                    }
                }
            }
            return false;
        }

        private void ConsolidateLoadRows()
        {
            if (_isConsolidating || _isLoadingSettings || _loadRows == null) return;
            _isConsolidating = true;
            
            bool hasGap = false;
            bool foundEmpty = false;
            for (int i = 0; i < 7; i++) {
                if (_loadRows[i] == null) continue;
                string p = _loadRows[i].ComboProfile.SelectedItem as string ?? "";
                string b = string.Join("", _loadRows[i].GetDownBinds());
                bool isEmpty = string.IsNullOrWhiteSpace(p) && string.IsNullOrWhiteSpace(b);
                
                if (isEmpty) foundEmpty = true;
                else if (foundEmpty) { hasGap = true; break; }
            }
            
            if (hasGap) {
                var activeProfiles = new System.Collections.Generic.List<string>();
                var activeBinds = new System.Collections.Generic.List<string>();
                var activeChecks = new System.Collections.Generic.List<bool>();
                var activeLocks = new System.Collections.Generic.List<bool>();
                
                for (int i = 0; i < 7; i++)
                {
                    if (_loadRows[i] == null) continue;
                    string p = _loadRows[i].ComboProfile.SelectedItem as string ?? "";
                    string b = string.Join("+", _loadRows[i].GetDownBinds());
                    bool isChecked = _loadRows[i].CheckBoxActive.Checked;
                    bool isLocked = IsPanelLocked(_loadRows[i].PanelDown);
                    
                    if (!string.IsNullOrWhiteSpace(p) || !string.IsNullOrWhiteSpace(b)) {
                        activeProfiles.Add(p);
                        activeBinds.Add(b);
                        activeChecks.Add(isChecked);
                        activeLocks.Add(isLocked);
                    }
                }
                
                for (int i = 0; i < 7; i++)
                {
                    if (_loadRows[i] == null) continue;
                    SetPanelLocked(_loadRows[i].PanelDown, false); // Unlock temporarily to allow setting binds
                    if (i < activeProfiles.Count) {
                        SetComboProfile(_loadRows[i].ComboProfile, activeProfiles[i]);
                        SetBinds(_loadRows[i].PanelDown, activeBinds[i]);
                        _loadRows[i].CheckBoxActive.Checked = activeChecks[i];
                        SetPanelLocked(_loadRows[i].PanelDown, activeLocks[i]);
                    } else {
                        SetComboProfile(_loadRows[i].ComboProfile, "");
                        SetBinds(_loadRows[i].PanelDown, "");
                        _loadRows[i].CheckBoxActive.Checked = false;
                        SetPanelLocked(_loadRows[i].PanelDown, false);
                    }
                }
            }
            
            // Visibility update
            foundEmpty = false;
            for (int i = 0; i < 7; i++)
            {
                if (_loadRows[i] == null) continue;
                string p = _loadRows[i].ComboProfile.SelectedItem as string ?? "";
                string b = string.Join("", _loadRows[i].GetDownBinds());
                bool isEmpty = string.IsNullOrWhiteSpace(p) && string.IsNullOrWhiteSpace(b);
                
                var rowPanel = _loadRows[i].CheckBoxActive.Parent;
                if (rowPanel != null && rowPanel is Panel) {
                    if (!foundEmpty) {
                        rowPanel.Visible = true;
                        if (isEmpty) {
                            foundEmpty = true;
                            _loadRows[i].CheckBoxActive.Checked = false; // Force unchecked for the newly revealed empty row
                        }
                    } else {
                        _loadRows[i].CheckBoxActive.Checked = false; // Force unchecked for hidden rows
                        SetComboProfile(_loadRows[i].ComboProfile, ""); // Clear ghost data
                        SetBinds(_loadRows[i].PanelDown, ""); // Clear ghost data
                        rowPanel.Visible = false;
                    }
                }
            }
            
            _isConsolidating = false;
            SaveConfig();
        }

        private void ValidateConfigDuplicates()
        {
            var duplicates = new System.Collections.Generic.HashSet<string>();
            var allBinds = new System.Collections.Generic.HashSet<string>();
            
            System.Action<string> checkBind = (b) => {
                if (!string.IsNullOrWhiteSpace(b)) {
                    if (allBinds.Contains(b)) duplicates.Add(b);
                    else allBinds.Add(b);
                }
            };
            
            checkBind(string.Join("+", _gammaRow.GetDownBinds()));
            checkBind(string.Join("+", _gammaRow.GetUpBinds()));
            checkBind(string.Join("+", _brightnessRow.GetDownBinds()));
            checkBind(string.Join("+", _brightnessRow.GetUpBinds()));
            checkBind(string.Join("+", _contrastRow.GetDownBinds()));
            checkBind(string.Join("+", _contrastRow.GetUpBinds()));
            checkBind(string.Join("+", _resetRow.GetDownBinds()));
            checkBind(string.Join("+", _refreshRow.GetDownBinds()));
            
            if (_loadRows != null) {
                for (int i=0; i<7; i++) {
                    if (_loadRows[i] != null) checkBind(string.Join("+", _loadRows[i].GetDownBinds()));
                }
            }
            
            if (duplicates.Count > 0) {
                MessageBox.Show("Warning! Duplicate hotkeys assigned to different functions detected in config.txt:\n\n" + string.Join("\n", duplicates) + "\n\nPlease fix them in the configuration file or application settings to avoid errors!", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SetComboProfile(ComboBox combo, string profile)
        {
            if (string.IsNullOrWhiteSpace(profile))
            {
                if (combo.Items.Count > 0) combo.SelectedIndex = 0; // Empty
            }
            else if (combo.Items.Contains(profile))
            {
                combo.SelectedItem = profile;
            }
        }

        private void SaveConfig()
        {
            if (_isLoadingSettings) return;
            
            var lines = new List<string>();
            if (File.Exists(ConfigFilePath))
            {
                foreach (var line in File.ReadAllLines(ConfigFilePath))
                {
                    if (line.Trim() == "cfg*") break;
                    lines.Add(line);
                }
                while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
                {
                    lines.RemoveAt(lines.Count - 1);
                }
            }
            
            lines.Add("");
            lines.Add("cfg*");
            lines.Add($"refresh: {string.Join(", ", _refreshRow.GetDownBinds())}");
            lines.Add($"reset: {string.Join(", ", _resetRow.GetDownBinds())}");
            lines.Add($"gamma_value: {_gammaRow.NumericValue.Value.ToString(CultureInfo.InvariantCulture)}");
            lines.Add($"brightness_value: {_brightnessRow.NumericValue.Value.ToString(CultureInfo.InvariantCulture)}");
            lines.Add($"contrast_value: {_contrastRow.NumericValue.Value.ToString(CultureInfo.InvariantCulture)}");
            lines.Add($"gamma_value_locked: {_viewBinds.btnGammaLock.Text == "\u26CB"}");
            lines.Add($"brightness_value_locked: {_viewBinds.btnBrightnessLock.Text == "\u26CB"}");
            lines.Add($"contrast_value_locked: {_viewBinds.btnContrastLock.Text == "\u26CB"}");
            
            
            var stdRows = new[] { _gammaRow, _brightnessRow, _contrastRow, _resetRow, _refreshRow };
            foreach (var r in stdRows) {
                if (r == null) continue;
                if (r.PanelDown != null) lines.Add($"{r.Name}_down_locked: {IsPanelLocked(r.PanelDown)}");
                if (r.PanelUp != null) lines.Add($"{r.Name}_up_locked: {IsPanelLocked(r.PanelUp)}");
            }
            if (_loadRows != null) {
                for (int i=0; i<7; i++) {
                    if (_loadRows[i] != null && _loadRows[i].PanelDown != null)
                        lines.Add($"load_{i+1}_locked: {IsPanelLocked(_loadRows[i].PanelDown)}");
                }
            }
            lines.Add($"gamma_down: {string.Join(", ", _gammaRow.GetDownBinds())}");
            lines.Add($"gamma_up: {string.Join(", ", _gammaRow.GetUpBinds())}");
            lines.Add($"brightness_down: {string.Join(", ", _brightnessRow.GetDownBinds())}");
            lines.Add($"brightness_up: {string.Join(", ", _brightnessRow.GetUpBinds())}");
            lines.Add($"contrast_down: {string.Join(", ", _contrastRow.GetDownBinds())}");
            lines.Add($"contrast_up: {string.Join(", ", _contrastRow.GetUpBinds())}");
            
            for (int i=0; i<7; i++) {
                if (_loadRows != null && _loadRows[i] != null) {
                    lines.Add($"load_{i+1}_bind: {string.Join("+", _loadRows[i].GetDownBinds())}");
                    lines.Add($"load_{i+1}_check: {(_loadRows[i].CheckBoxActive.Checked ? "1" : "0")}");
                    lines.Add($"load_{i+1}_profile: {_loadRows[i].ComboProfile.SelectedItem}");
                }
            }
            lines.Add($"refresh_check: {(_refreshRow.CheckBoxActive.Checked ? "1" : "0")}");
            if (_viewBonus != null) lines.Add($"enable_custom_png: {(_viewBonus.EnableCustomPNG ? "1" : "0")}");
            if (_viewBonus != null) lines.Add($"outline_width: {_viewBonus.OutlineWidth}");
            if (_viewBonus != null) lines.Add($"outline_softness: {_viewBonus.OutlineSoftness}");
            if (_viewBonus != null) lines.Add($"outline_opacity: {_viewBonus.OutlineOpacity}");
            if (_viewBonus != null) lines.Add($"default_on_startup: {(_viewBonus.chkDefaultOnStartup.Checked ? "1" : "0")}");
            if (_viewBonus != null) lines.Add($"reset_on_exit: {(_viewBonus.chkResetOnExit.Checked ? "1" : "0")}");
            if (_viewBonus != null && _viewBonus.cmbDefaultConfig.SelectedItem != null) lines.Add($"default_config: {_viewBonus.cmbDefaultConfig.SelectedItem}");
            lines.Add($"reset_check: {(_resetRow.CheckBoxActive.Checked ? "1" : "0")}");
            lines.Add($"gamma_check: {(_gammaRow.CheckBoxActive.Checked ? "1" : "0")}");
            lines.Add($"brightness_check: {(_brightnessRow.CheckBoxActive.Checked ? "1" : "0")}");
            lines.Add($"contrast_check: {(_contrastRow.CheckBoxActive.Checked ? "1" : "0")}");
            
            if (_configWatcher != null) _configWatcher.EnableRaisingEvents = false;
            File.WriteAllLines(ConfigFilePath, lines);
            if (_configWatcher != null) _configWatcher.EnableRaisingEvents = true;
        }

        private DateTime _lastRepeatTime = DateTime.MinValue;
        private DateTime _lastNewPressTime = DateTime.MinValue;
        private HashSet<string> _triggeredBinds = new HashSet<string>();

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                
                bool isKeyDown = (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN);
                bool isKeyUp = (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP);
                
                if (isKeyDown)
                {
                    bool isNewPress = _pressedKeys.Add(key);
                    
                    if (!isNewPress)
                    {
                        if ((DateTime.Now - _lastNewPressTime).TotalMilliseconds < 500) return CallNextHookEx(_hookID, nCode, wParam, lParam);
                        if ((DateTime.Now - _lastRepeatTime).TotalMilliseconds < 50) return CallNextHookEx(_hookID, nCode, wParam, lParam);
                    }
                    else
                    {
                        _lastNewPressTime = DateTime.Now;
                        _triggeredBinds.Clear();
                    }
                    
                    if (!isNewPress) _lastRepeatTime = DateTime.Now;

                    CheckBinds();
                }
                else if (isKeyUp)
                {
                    _pressedKeys.Remove(key);
                    _triggeredBinds.Clear();
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void CheckBinds()
        {
            if (_loadRows != null) {
                for (int i=0; i<7; i++) {
                    if (_loadRows[i] != null && _loadRows[i].CheckBoxActive.Checked && CheckAnyMatch(_loadRows[i].GetDownBinds())) {
                        string sel = _loadRows[i].ComboProfile.SelectedItem?.ToString();
                        if (!string.IsNullOrEmpty(sel)) {
                            this.Invoke((MethodInvoker)delegate { LoadProfileByName(sel); });
                            return;
                        }
                    }
                }
            }
            
            if (_refreshRow.CheckBoxActive.Checked && CheckAnyMatch(_refreshRow.GetDownBinds()))
            {
                this.Invoke((MethodInvoker)delegate { ApplyAllSettings(); });
                return;
            }
            if (_resetRow.CheckBoxActive.Checked && CheckAnyMatch(_resetRow.GetDownBinds()))
            {
                this.Invoke((MethodInvoker)delegate { buttonReset_Click(null, null); });
                return;
            }
            
            if (_gammaRow.CheckBoxActive.Checked)
            {
                decimal step = _gammaRow.NumericValue.Value;
                if (CheckAnyMatch(_gammaRow.GetDownBinds())) ChangeGamma(-step);
                if (CheckAnyMatch(_gammaRow.GetUpBinds())) ChangeGamma(step);
            }
            
            if (_brightnessRow.CheckBoxActive.Checked)
            {
                decimal step = _brightnessRow.NumericValue.Value;
                if (CheckAnyMatch(_brightnessRow.GetDownBinds())) ChangeBrightness(-step);
                if (CheckAnyMatch(_brightnessRow.GetUpBinds())) ChangeBrightness(step);
            }

            if (_contrastRow.CheckBoxActive.Checked)
            {
                decimal step = _contrastRow.NumericValue.Value;
                if (CheckAnyMatch(_contrastRow.GetDownBinds())) ChangeContrast(-step);
                if (CheckAnyMatch(_contrastRow.GetUpBinds())) ChangeContrast(step);
            }
        }

        private bool CheckAnyMatch(List<string> binds)
        {
            foreach (var b in binds)
            {
                if (IsBindMatch(b)) return true;
            }
            return false;
        }

        private bool IsBindMatch(string bindStr)
        {
            var keys = ParseBindString(bindStr);
            if (keys.Count == 0) return false;
            
            var normalizedPressed = new HashSet<Keys>();
            foreach (var k in _pressedKeys) normalizedPressed.Add(NormalizeKey(k));
            
            if (keys.Count != normalizedPressed.Count) return false;
            
            foreach (var k in keys)
            {
                if (!normalizedPressed.Contains(NormalizeKey(k))) return false;
            }
            
            return true;
        }

        private static List<Keys> ParseBindString(string bindStr)
        {
            var list = new List<Keys>();
            string[] parts = bindStr.Split('+');
            for (int i = 0; i < parts.Length; i++)
            {
                string p = parts[i].Trim();
                if (string.IsNullOrEmpty(p))
                {
                    if (i > 0 && i == parts.Length - 1 && string.IsNullOrEmpty(parts[i - 1]))
                    {
                        list.Add(ParseKey("+"));
                    }
                    continue;
                }
                Keys k = ParseKey(p);
                if (k != Keys.None) list.Add(k);
            }
            return list;
        }

        private static Keys ParseKey(string k)
        {
            k = k.ToLowerInvariant();
            switch (k)
            {
                case "ctrl": case "control": return Keys.ControlKey;
                case "alt": return Keys.Menu;
                case "shift": return Keys.ShiftKey;
                case "win": case "windows": return Keys.LWin;
                case "-": return Keys.OemMinus;
                case "=": case "+": return Keys.Oemplus;
                case "[": return Keys.OemOpenBrackets;
                case "]": return Keys.OemCloseBrackets;
                case ";": return Keys.OemSemicolon;
                case "'": return Keys.OemQuotes;
                case ",": return Keys.Oemcomma;
                case ".": return Keys.OemPeriod;
                case "/": return Keys.OemQuestion;
                case "\\": return Keys.OemBackslash;
                case "`": return Keys.Oemtilde;
                case "fn": return (Keys)255;
            }
            if (k.Length == 1)
            {
                char c = k[0];
                if (c >= 'a' && c <= 'z') return (Keys)((int)Keys.A + (c - 'a'));
                if (c >= '0' && c <= '9') return (Keys)((int)Keys.D0 + (c - '0'));
            }
            if (Enum.TryParse(k, true, out Keys result)) return result;
            return Keys.None;
        }

        private static Keys NormalizeKey(Keys k)
        {
            switch (k)
            {
                case Keys.LShiftKey: case Keys.RShiftKey: return Keys.ShiftKey;
                case Keys.LControlKey: case Keys.RControlKey: return Keys.ControlKey;
                case Keys.LMenu: case Keys.RMenu: return Keys.Menu;
                case Keys.LWin: case Keys.RWin: return Keys.LWin;
                default: return k;
            }
        }

        private void ChangeGamma(decimal delta)
        {
            this.Invoke((MethodInvoker)delegate {
                decimal current = (decimal)trackBarGamma.Value / 100M;
                current += delta;
                if (current < 0.1M) current = 0.1M;
                if (current > 8.88M) current = 8.88M;
                trackBarGamma.Value = (int)(current * 100);
                ApplyAllSettings();
                UpdateValueLabels();
                panelGraph.Invalidate();
            });
        }

        private void ChangeBrightness(decimal delta)
        {
            this.Invoke((MethodInvoker)delegate {
                decimal current = (decimal)trackBarBrightness.Value / 100M;
                current += delta;
                if (current < -8.88M) current = -8.88M;
                if (current > 8.88M) current = 8.88M;
                trackBarBrightness.Value = (int)(current * 100);
                ApplyAllSettings();
                UpdateValueLabels();
                panelGraph.Invalidate();
            });
        }

        private void ChangeContrast(decimal delta)
        {
            this.Invoke((MethodInvoker)delegate {
                decimal current = (decimal)trackBarContrast.Value / 100M;
                current += delta;
                if (current < 0.08M) current = 0.08M;
                if (current > 8.88M) current = 8.88M;
                trackBarContrast.Value = (int)(current * 100);
                ApplyAllSettings();
                UpdateValueLabels();
                panelGraph.Invalidate();
            });
        }

    }
}
