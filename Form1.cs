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
        private FileSystemWatcher _configWatcher;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;

        public Form1()
        {
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureConfigFileExists();
            InitializeConfigWatcher();
            LoadProfiles();
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
            labelGammaValue.Text = (trackBarGamma.Value / 100.0).ToString("0.00");
            labelBrightnessValue.Text = trackBarBrightness.Value.ToString();
            labelContrastValue.Text = trackBarContrast.Value.ToString();
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
            trackBarGamma.Value = 100;
            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 100;
            ApplyAllSettings();
            UpdateValueLabels();
            panelGraph.Invalidate();
        }

        private void buttonSaveAR1_Click(object sender, EventArgs e)
        {
            if (TryParseAR(textBoxAR1.Text, out double ar1))
            {
                ar1 = ApplyDTIfChecked(ar1, checkBoxDT1);
                ar1Value = ar1;
                settingsAR1 = GetCurrentSettings();
                labelAR1.Text = $"AR1: {ar1:F2} | G: {settingsAR1.Value.gamma:0.00} | B: {settingsAR1.Value.brightness} | C: {settingsAR1.Value.contrast}";
            }
        }

        private void buttonSaveAR2_Click(object sender, EventArgs e)
        {
            if (TryParseAR(textBoxAR2.Text, out double ar2))
            {
                ar2 = ApplyDTIfChecked(ar2, checkBoxDT2);
                ar2Value = ar2;
                settingsAR2 = GetCurrentSettings();
                labelAR2.Text = $"AR2: {ar2:F2} | G: {settingsAR2.Value.gamma:0.00} | B: {settingsAR2.Value.brightness} | C: {settingsAR2.Value.contrast}";
            }
        }

        private void buttonSetAR3_Click(object sender, EventArgs e)
        {
            if (settingsAR1 == null || settingsAR2 == null)
            {
                MessageBox.Show("First, save the AR1 and AR2 settings.");
                return;
            }

            if (!TryParseAR(textBoxTargetAR.Text, out double ar3))
                return;

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

        private void textBoxAR1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSaveAR1.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxAR2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSaveAR2.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxTargetAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetAR3.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
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

            using (Pen borderPen = new Pen(Color.White, 2))
            {
                g.DrawRectangle(borderPen, 1, 1, w - 2, h - 2);
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
            if (string.IsNullOrWhiteSpace(textBoxProfileName.Text))
            {
                MessageBox.Show("Please enter a profile name.");
                return;
            }

            var profile = GetCurrentSettings();
            string profileLine = $"{textBoxProfileName.Text}|{profile.gamma}|{profile.brightness}|{profile.contrast}";

            try
            {
                var lines = File.Exists(ConfigFilePath) ? File.ReadAllLines(ConfigFilePath).ToList() : new List<string>();
                lines.Add(profileLine);
                File.WriteAllLines(ConfigFilePath, lines);

                LoadProfiles();

                buttonSaveProfile.Text = "Saved!";
                await Task.Delay(1000);
                buttonSaveProfile.Text = "Save";
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

        private async void buttonLoadProfile_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedItem == null) return;

            string selectedProfile = comboBoxProfiles.SelectedItem.ToString();
            var profiles = GetSavedProfiles();
            var profile = profiles.FirstOrDefault(p => p.Item1 == selectedProfile);

            if (!profile.Equals(default((string, double, int, int))))
            {
                if (profile.Item1 != null)
                {
                    double gamma = Math.Max(0.08, Math.Min(8.88, profile.Item2));
                    int brightness = Math.Max(-888, Math.Min(888, profile.Item3));
                    int contrast = Math.Max(8, Math.Min(888, profile.Item4));

                    trackBarGamma.Value = (int)(gamma * 100);
                    trackBarBrightness.Value = brightness;
                    trackBarContrast.Value = contrast;

                    UpdateValueLabels();
                    ApplyAllSettings();
                    panelGraph.Invalidate();
                }
            }

            buttonLoadProfile.Text = "Loaded!";
            await Task.Delay(1000);
            buttonLoadProfile.Text = "Load";
        }

        private List<(string, double, int, int)> GetSavedProfiles()
        {
            var profiles = new List<(string, double, int, int)>();

            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    foreach (var line in File.ReadAllLines(ConfigFilePath))
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 4)
                        {
                            string gammaStr = parts[1].Replace(',', '.');
                            string brightnessStr = parts[2].Replace(',', '.');
                            string contrastStr = parts[3].Replace(',', '.');

                            if (double.TryParse(gammaStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double gamma) &&
                                int.TryParse(brightnessStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int brightness) &&
                                int.TryParse(contrastStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int contrast))
                            {
                                profiles.Add((parts[0], gamma, brightness, contrast));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profiles: {ex.Message}");
            }

            return profiles;
        }

        private void LoadProfiles()
        {
            comboBoxProfiles.Items.Clear();
            var profiles = GetSavedProfiles();
            foreach (var profile in profiles)
            {
                comboBoxProfiles.Items.Add(profile.Item1);
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
    }
}