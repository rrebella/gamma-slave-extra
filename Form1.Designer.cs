using System.Drawing;
using System.Windows.Forms;

namespace rebellagamma
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.Button _btnViewMain;
        public System.Windows.Forms.Button _btnViewBinds;
        public System.Windows.Forms.Button _btnViewLoads;
        public System.Windows.Forms.Button _btnViewBonus;
        public System.Windows.Forms.Panel _panelMain;

        private Label labelGamma;
        private TrackBar trackBarGamma;
        private Label labelBrightness;
        private TrackBar trackBarBrightness;
        private Label labelContrast;
        private TrackBar trackBarContrast;
        private ComboBox comboBoxMonitors;
        private Button buttonReset;
        private Panel panelGraph;
        private NumericUpDown numericUpDownAR1;
        private NumericUpDown numericUpDownAR2;
        private NumericUpDown numericUpDownTargetAR;
        private CheckBox checkBoxDT1;
        private CheckBox checkBoxDT2;
        private CheckBox checkBoxDT3;
        private Button buttonSaveAR1;
        private Button buttonSaveAR2;
        private Label labelAR1;
        private Label labelAR2;
        private Label labelAR3;
        private Button buttonSetAR3;

        // New profile controls
        private Button buttonSaveProfile;
        private Button buttonLoadProfile;
        private TextBox textBoxProfileName;
        private ComboBox comboBoxProfiles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            _btnViewMain = new Button();
            _btnViewBinds = new Button();
            _btnViewLoads = new Button();
            _btnViewBonus = new Button();
            _panelMain = new Panel();
            numericUpDownAR1 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            panelGraph = new Panel();
            label1 = new Label();
            buttonReset = new Button();
            comboBoxMonitors = new ComboBox();
            labelAR1 = new Label();
            comboBoxProfiles = new ComboBox();
            textBoxProfileName = new TextBox();
            buttonLoadProfile = new Button();
            buttonSaveProfile = new Button();
            labelAR3 = new Label();
            checkBoxDT3 = new CheckBox();
            labelAR2 = new Label();
            checkBoxDT1 = new CheckBox();
            trackBarContrast = new TrackBar();
            trackBarBrightness = new TrackBar();
            labelGamma = new Label();
            trackBarGamma = new TrackBar();
            labelBrightness = new Label();
            labelContrast = new Label();
            buttonSaveAR1 = new Button();
            checkBoxDT2 = new CheckBox();
            buttonSaveAR2 = new Button();
            buttonSetAR3 = new Button();
            numericUpDownAR2 = new NumericUpDown();
            numericUpDownTargetAR = new NumericUpDown();
            _panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetAR).BeginInit();
            SuspendLayout();
            // 
            // _btnViewMain
            // 
            _btnViewMain.BackColor = Color.Black;
            _btnViewMain.FlatAppearance.BorderColor = Color.White;
            _btnViewMain.FlatStyle = FlatStyle.Flat;
            _btnViewMain.ForeColor = SystemColors.ControlLightLight;
            _btnViewMain.Location = new Point(0, -1);
            _btnViewMain.Name = "_btnViewMain";
            _btnViewMain.Size = new Size(96, 27);
            _btnViewMain.TabIndex = 100;
            _btnViewMain.TabStop = false;
            _btnViewMain.Text = "Main";
            _btnViewMain.UseCompatibleTextRendering = true;
            _btnViewMain.UseVisualStyleBackColor = false;
            // 
            // _btnViewBinds
            // 
            _btnViewBinds.BackColor = Color.Black;
            _btnViewBinds.FlatAppearance.BorderColor = Color.White;
            _btnViewBinds.FlatStyle = FlatStyle.Flat;
            _btnViewBinds.ForeColor = SystemColors.ControlLightLight;
            _btnViewBinds.Location = new Point(96, -1);
            _btnViewBinds.Name = "_btnViewBinds";
            _btnViewBinds.Size = new Size(95, 27);
            _btnViewBinds.TabIndex = 101;
            _btnViewBinds.TabStop = false;
            _btnViewBinds.Text = "Binds";
            _btnViewBinds.UseCompatibleTextRendering = true;
            _btnViewBinds.UseVisualStyleBackColor = false;
            // 
            // _btnViewLoads
            // 
            _btnViewLoads.BackColor = Color.Black;
            _btnViewLoads.FlatAppearance.BorderColor = Color.White;
            _btnViewLoads.FlatStyle = FlatStyle.Flat;
            _btnViewLoads.ForeColor = SystemColors.ControlLightLight;
            _btnViewLoads.Location = new Point(191, -1);
            _btnViewLoads.Name = "_btnViewLoads";
            _btnViewLoads.Size = new Size(96, 27);
            _btnViewLoads.TabIndex = 102;
            _btnViewLoads.TabStop = false;
            _btnViewLoads.Text = "Presets";
            _btnViewLoads.UseCompatibleTextRendering = true;
            _btnViewLoads.UseVisualStyleBackColor = false;
            // 
            // _btnViewBonus
            // 
            _btnViewBonus.BackColor = Color.Black;
            _btnViewBonus.FlatAppearance.BorderColor = Color.White;
            _btnViewBonus.FlatStyle = FlatStyle.Flat;
            _btnViewBonus.ForeColor = SystemColors.ControlLightLight;
            _btnViewBonus.Location = new Point(287, -1);
            _btnViewBonus.Name = "_btnViewBonus";
            _btnViewBonus.Size = new Size(95, 27);
            _btnViewBonus.TabIndex = 103;
            _btnViewBonus.TabStop = false;
            _btnViewBonus.Text = "Addons";
            _btnViewBonus.UseCompatibleTextRendering = true;
            _btnViewBonus.UseVisualStyleBackColor = false;
            // 
            // _panelMain
            // 
            _panelMain.BackColor = Color.Black;
            _panelMain.Controls.Add(numericUpDownAR1);
            _panelMain.Controls.Add(numericUpDown3);
            _panelMain.Controls.Add(numericUpDown2);
            _panelMain.Controls.Add(numericUpDown1);
            _panelMain.Controls.Add(panelGraph);
            _panelMain.Controls.Add(buttonReset);
            _panelMain.Controls.Add(comboBoxMonitors);
            _panelMain.Controls.Add(labelAR1);
            _panelMain.Controls.Add(comboBoxProfiles);
            _panelMain.Controls.Add(textBoxProfileName);
            _panelMain.Controls.Add(buttonLoadProfile);
            _panelMain.Controls.Add(buttonSaveProfile);
            _panelMain.Controls.Add(labelAR3);
            _panelMain.Controls.Add(checkBoxDT3);
            _panelMain.Controls.Add(labelAR2);
            _panelMain.Controls.Add(checkBoxDT1);
            _panelMain.Controls.Add(trackBarContrast);
            _panelMain.Controls.Add(trackBarBrightness);
            _panelMain.Controls.Add(labelGamma);
            _panelMain.Controls.Add(trackBarGamma);
            _panelMain.Controls.Add(labelBrightness);
            _panelMain.Controls.Add(labelContrast);
            _panelMain.Controls.Add(buttonSaveAR1);
            _panelMain.Controls.Add(checkBoxDT2);
            _panelMain.Controls.Add(buttonSaveAR2);
            _panelMain.Controls.Add(buttonSetAR3);
            _panelMain.Controls.Add(numericUpDownAR2);
            _panelMain.Controls.Add(numericUpDownTargetAR);
            _panelMain.Location = new Point(0, 25);
            _panelMain.Name = "_panelMain";
            _panelMain.Size = new Size(382, 220);
            _panelMain.TabIndex = 200;
            // 
            // numericUpDownAR1
            // 
            numericUpDownAR1.DecimalPlaces = 1;
            numericUpDownAR1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR1.Location = new Point(88, 101);
            numericUpDownAR1.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR1.Name = "numericUpDownAR1";
            numericUpDownAR1.Size = new Size(44, 23);
            numericUpDownAR1.TabIndex = 12;
            numericUpDownAR1.KeyDown += numericUpDownAR_KeyDown;
            numericUpDownAR1.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown3.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown3.Location = new Point(72, 69);
            numericUpDown3.Maximum = new decimal(new int[] { 888, 0, 0, 131072 });
            numericUpDown3.Minimum = new decimal(new int[] { 8, 0, 0, 131072 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(46, 23);
            numericUpDown3.TabIndex = 32;
            numericUpDown3.Value = new decimal(new int[] { 8, 0, 0, 131072 });
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            numericUpDown3.KeyDown += numericUpDownKeyDown;
            numericUpDown3.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown2.Location = new Point(72, 40);
            numericUpDown2.Maximum = new decimal(new int[] { 888, 0, 0, 131072 });
            numericUpDown2.Minimum = new decimal(new int[] { 888, 0, 0, -2147352576 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(46, 23);
            numericUpDown2.TabIndex = 31;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            numericUpDown2.KeyDown += numericUpDownKeyDown;
            numericUpDown2.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown1.Location = new Point(72, 11);
            numericUpDown1.Maximum = new decimal(new int[] { 888, 0, 0, 131072 });
            numericUpDown1.Minimum = new decimal(new int[] { 8, 0, 0, 131072 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(46, 23);
            numericUpDown1.TabIndex = 30;
            numericUpDown1.Value = new decimal(new int[] { 8, 0, 0, 131072 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            numericUpDown1.KeyDown += numericUpDownKeyDown;
            numericUpDown1.KeyPress += numericUpDownKeyPress;
            // 
            // panelGraph
            // 
            panelGraph.BackColor = Color.Black;
            panelGraph.Controls.Add(label1);
            panelGraph.Location = new Point(275, 8);
            panelGraph.Name = "panelGraph";
            panelGraph.Size = new Size(100, 100);
            panelGraph.TabIndex = 11;
            panelGraph.Paint += panelGraph_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 6F);
            label1.ForeColor = Color.FromArgb(8, 8, 8);
            label1.Location = new Point(70, 87);
            label1.Name = "label1";
            label1.Size = new Size(29, 11);
            label1.TabIndex = 30;
            label1.Text = "rebella";
            // 
            // buttonReset
            // 
            buttonReset.ForeColor = SystemColors.ControlLightLight;
            buttonReset.Location = new Point(275, 143);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(100, 43);
            buttonReset.TabIndex = 10;
            buttonReset.Text = "⛧ Reset ⛧";
            buttonReset.Click += buttonReset_Click;
            // 
            // comboBoxMonitors
            // 
            comboBoxMonitors.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMonitors.Location = new Point(275, 115);
            comboBoxMonitors.Name = "comboBoxMonitors";
            comboBoxMonitors.Size = new Size(100, 23);
            comboBoxMonitors.TabIndex = 9;
            comboBoxMonitors.SelectedIndexChanged += comboBoxMonitors_SelectedIndexChanged;
            // 
            // labelAR1
            // 
            labelAR1.AutoSize = true;
            labelAR1.Font = new Font("Segoe UI", 6.4F);
            labelAR1.ForeColor = Color.White;
            labelAR1.Location = new Point(136, 115);
            labelAR1.Name = "labelAR1";
            labelAR1.Size = new Size(29, 12);
            labelAR1.TabIndex = 15;
            labelAR1.Text = "AR1: -";
            // 
            // comboBoxProfiles
            // 
            comboBoxProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProfiles.FormattingEnabled = true;
            comboBoxProfiles.Location = new Point(275, 191);
            comboBoxProfiles.Name = "comboBoxProfiles";
            comboBoxProfiles.Size = new Size(100, 23);
            comboBoxProfiles.TabIndex = 29;
            // 
            // textBoxProfileName
            // 
            textBoxProfileName.Location = new Point(88, 191);
            textBoxProfileName.Name = "textBoxProfileName";
            textBoxProfileName.Size = new Size(100, 23);
            textBoxProfileName.TabIndex = 27;
            textBoxProfileName.KeyDown += textBoxProfileName_KeyDown;
            // 
            // buttonLoadProfile
            // 
            buttonLoadProfile.Font = new Font("Segoe UI", 9F);
            buttonLoadProfile.ForeColor = SystemColors.ControlLightLight;
            buttonLoadProfile.Location = new Point(194, 189);
            buttonLoadProfile.Name = "buttonLoadProfile";
            buttonLoadProfile.Size = new Size(75, 27);
            buttonLoadProfile.TabIndex = 26;
            buttonLoadProfile.Text = "Load";
            buttonLoadProfile.Click += buttonLoadProfile_Click;
            // 
            // buttonSaveProfile
            // 
            buttonSaveProfile.Font = new Font("Segoe UI", 9F);
            buttonSaveProfile.ForeColor = SystemColors.ControlLightLight;
            buttonSaveProfile.Location = new Point(7, 189);
            buttonSaveProfile.Name = "buttonSaveProfile";
            buttonSaveProfile.Size = new Size(75, 27);
            buttonSaveProfile.TabIndex = 25;
            buttonSaveProfile.Text = "Save";
            buttonSaveProfile.Click += buttonSaveProfile_Click;
            // 
            // labelAR3
            // 
            labelAR3.AutoSize = true;
            labelAR3.Font = new Font("Segoe UI", 6.4F);
            labelAR3.ForeColor = Color.White;
            labelAR3.Location = new Point(136, 175);
            labelAR3.Name = "labelAR3";
            labelAR3.Size = new Size(29, 12);
            labelAR3.TabIndex = 23;
            labelAR3.Text = "AR3: -";
            // 
            // checkBoxDT3
            // 
            checkBoxDT3.ForeColor = Color.Gray;
            checkBoxDT3.Location = new Point(138, 157);
            checkBoxDT3.Name = "checkBoxDT3";
            checkBoxDT3.Size = new Size(40, 23);
            checkBoxDT3.TabIndex = 21;
            checkBoxDT3.Text = "DT";
            // 
            // labelAR2
            // 
            labelAR2.AutoSize = true;
            labelAR2.Font = new Font("Segoe UI", 6.4F);
            labelAR2.ForeColor = Color.White;
            labelAR2.Location = new Point(136, 145);
            labelAR2.Name = "labelAR2";
            labelAR2.Size = new Size(29, 12);
            labelAR2.TabIndex = 19;
            labelAR2.Text = "AR2: -";
            // 
            // checkBoxDT1
            // 
            checkBoxDT1.ForeColor = Color.Gray;
            checkBoxDT1.Location = new Point(138, 97);
            checkBoxDT1.Name = "checkBoxDT1";
            checkBoxDT1.Size = new Size(42, 23);
            checkBoxDT1.TabIndex = 13;
            checkBoxDT1.Text = "DT";
            // 
            // trackBarContrast
            // 
            trackBarContrast.BackColor = Color.Black;
            trackBarContrast.Location = new Point(117, 70);
            trackBarContrast.Maximum = 888;
            trackBarContrast.Minimum = 8;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(160, 45);
            trackBarContrast.TabIndex = 8;
            trackBarContrast.TickFrequency = 10;
            trackBarContrast.TickStyle = TickStyle.None;
            trackBarContrast.Value = 100;
            trackBarContrast.Scroll += trackBarContrast_Scroll;
            trackBarContrast.MouseDown += trackBar_MouseDown_RemoveFocus;
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.BackColor = Color.Black;
            trackBarBrightness.Location = new Point(117, 41);
            trackBarBrightness.Maximum = 888;
            trackBarBrightness.Minimum = -888;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(160, 45);
            trackBarBrightness.TabIndex = 5;
            trackBarBrightness.TickStyle = TickStyle.None;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            trackBarBrightness.MouseDown += trackBar_MouseDown_RemoveFocus;
            // 
            // labelGamma
            // 
            labelGamma.AutoSize = true;
            labelGamma.ForeColor = Color.White;
            labelGamma.Location = new Point(7, 14);
            labelGamma.Name = "labelGamma";
            labelGamma.Size = new Size(49, 15);
            labelGamma.TabIndex = 0;
            labelGamma.Text = "Gamma";
            // 
            // trackBarGamma
            // 
            trackBarGamma.AutoSize = false;
            trackBarGamma.BackColor = Color.Black;
            trackBarGamma.LargeChange = 10;
            trackBarGamma.Location = new Point(117, 12);
            trackBarGamma.Maximum = 888;
            trackBarGamma.Minimum = 8;
            trackBarGamma.Name = "trackBarGamma";
            trackBarGamma.Size = new Size(160, 45);
            trackBarGamma.TabIndex = 2;
            trackBarGamma.TickFrequency = 100;
            trackBarGamma.TickStyle = TickStyle.None;
            trackBarGamma.Value = 100;
            trackBarGamma.Scroll += trackBarGamma_Scroll;
            trackBarGamma.MouseDown += trackBar_MouseDown_RemoveFocus;
            // 
            // labelBrightness
            // 
            labelBrightness.AutoSize = true;
            labelBrightness.ForeColor = Color.White;
            labelBrightness.Location = new Point(7, 43);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(62, 15);
            labelBrightness.TabIndex = 3;
            labelBrightness.Text = "Brightness";
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = Color.White;
            labelContrast.Location = new Point(7, 72);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(52, 15);
            labelContrast.TabIndex = 6;
            labelContrast.Text = "Contrast";
            // 
            // buttonSaveAR1
            // 
            buttonSaveAR1.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR1.Location = new Point(7, 99);
            buttonSaveAR1.Name = "buttonSaveAR1";
            buttonSaveAR1.Size = new Size(75, 27);
            buttonSaveAR1.TabIndex = 14;
            buttonSaveAR1.Text = "AR1";
            buttonSaveAR1.Click += buttonSaveAR1_Click;
            // 
            // checkBoxDT2
            // 
            checkBoxDT2.ForeColor = Color.Gray;
            checkBoxDT2.Location = new Point(138, 127);
            checkBoxDT2.Name = "checkBoxDT2";
            checkBoxDT2.Size = new Size(40, 23);
            checkBoxDT2.TabIndex = 17;
            checkBoxDT2.Text = "DT";
            // 
            // buttonSaveAR2
            // 
            buttonSaveAR2.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR2.Location = new Point(7, 129);
            buttonSaveAR2.Name = "buttonSaveAR2";
            buttonSaveAR2.Size = new Size(75, 27);
            buttonSaveAR2.TabIndex = 18;
            buttonSaveAR2.Text = "AR2";
            buttonSaveAR2.Click += buttonSaveAR2_Click;
            // 
            // buttonSetAR3
            // 
            buttonSetAR3.ForeColor = SystemColors.ControlLightLight;
            buttonSetAR3.Location = new Point(7, 159);
            buttonSetAR3.Name = "buttonSetAR3";
            buttonSetAR3.Size = new Size(75, 27);
            buttonSetAR3.TabIndex = 22;
            buttonSetAR3.Text = "Calculate";
            buttonSetAR3.Click += buttonSetAR3_Click;
            // 
            // numericUpDownAR2
            // 
            numericUpDownAR2.DecimalPlaces = 1;
            numericUpDownAR2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR2.Location = new Point(88, 131);
            numericUpDownAR2.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR2.Name = "numericUpDownAR2";
            numericUpDownAR2.Size = new Size(44, 23);
            numericUpDownAR2.TabIndex = 16;
            numericUpDownAR2.KeyDown += numericUpDownAR_KeyDown;
            numericUpDownAR2.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDownTargetAR
            // 
            numericUpDownTargetAR.DecimalPlaces = 1;
            numericUpDownTargetAR.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownTargetAR.Location = new Point(88, 161);
            numericUpDownTargetAR.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownTargetAR.Name = "numericUpDownTargetAR";
            numericUpDownTargetAR.Size = new Size(44, 23);
            numericUpDownTargetAR.TabIndex = 20;
            numericUpDownTargetAR.KeyDown += numericUpDownAR_KeyDown;
            numericUpDownTargetAR.KeyPress += numericUpDownKeyPress;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(382, 248);
            Controls.Add(_btnViewMain);
            Controls.Add(_btnViewBinds);
            Controls.Add(_btnViewLoads);
            Controls.Add(_btnViewBonus);
            Controls.Add(_panelMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "gamma slave extra";
            _panelMain.ResumeLayout(false);
            _panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            panelGraph.ResumeLayout(false);
            panelGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetAR).EndInit();
            ResumeLayout(false);
        }
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
    }
}