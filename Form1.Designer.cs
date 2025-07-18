using System.Drawing;
using System.Windows.Forms;

namespace rebellagamma
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Label labelGamma;
        private TrackBar trackBarGamma;
        private Label labelGammaValue;
        private Label labelBrightness;
        private TrackBar trackBarBrightness;
        private Label labelBrightnessValue;
        private Label labelContrast;
        private TrackBar trackBarContrast;
        private Label labelContrastValue;
        private ComboBox comboBoxMonitors;
        private Button buttonReset;
        private Panel panelGraph;
        private TextBox textBoxAR1;
        private TextBox textBoxAR2;
        private TextBox textBoxTargetAR;
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
            labelGamma = new Label();
            trackBarGamma = new TrackBar();
            labelGammaValue = new Label();
            labelBrightness = new Label();
            trackBarBrightness = new TrackBar();
            labelBrightnessValue = new Label();
            labelContrast = new Label();
            trackBarContrast = new TrackBar();
            labelContrastValue = new Label();
            comboBoxMonitors = new ComboBox();
            buttonReset = new Button();
            panelGraph = new Panel();
            label1 = new Label();
            textBoxAR1 = new TextBox();
            textBoxAR2 = new TextBox();
            textBoxTargetAR = new TextBox();
            checkBoxDT1 = new CheckBox();
            checkBoxDT2 = new CheckBox();
            checkBoxDT3 = new CheckBox();
            buttonSaveAR1 = new Button();
            buttonSaveAR2 = new Button();
            labelAR1 = new Label();
            labelAR2 = new Label();
            labelAR3 = new Label();
            buttonSetAR3 = new Button();
            buttonSaveProfile = new Button();
            buttonLoadProfile = new Button();
            textBoxProfileName = new TextBox();
            comboBoxProfiles = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            panelGraph.SuspendLayout();
            SuspendLayout();
            // 
            // labelGamma
            // 
            labelGamma.AutoSize = true;
            labelGamma.ForeColor = Color.White;
            labelGamma.Location = new Point(7, 9);
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
            trackBarGamma.Location = new Point(107, 7);
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
            // labelGammaValue
            // 
            labelGammaValue.AutoSize = true;
            labelGammaValue.ForeColor = Color.White;
            labelGammaValue.Location = new Point(77, 9);
            labelGammaValue.Name = "labelGammaValue";
            labelGammaValue.Size = new Size(28, 15);
            labelGammaValue.TabIndex = 1;
            labelGammaValue.Text = "1.00";
            labelGammaValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelBrightness
            // 
            labelBrightness.AutoSize = true;
            labelBrightness.ForeColor = Color.White;
            labelBrightness.Location = new Point(7, 39);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(62, 15);
            labelBrightness.TabIndex = 3;
            labelBrightness.Text = "Brightness";
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.BackColor = Color.Black;
            trackBarBrightness.Location = new Point(107, 37);
            trackBarBrightness.Maximum = 888;
            trackBarBrightness.Minimum = -888;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(160, 45);
            trackBarBrightness.TabIndex = 5;
            trackBarBrightness.TickStyle = TickStyle.None;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            trackBarBrightness.MouseDown += trackBar_MouseDown_RemoveFocus;
            // 
            // labelBrightnessValue
            // 
            labelBrightnessValue.AutoSize = true;
            labelBrightnessValue.ForeColor = Color.White;
            labelBrightnessValue.Location = new Point(77, 39);
            labelBrightnessValue.Name = "labelBrightnessValue";
            labelBrightnessValue.Size = new Size(13, 15);
            labelBrightnessValue.TabIndex = 4;
            labelBrightnessValue.Text = "0";
            labelBrightnessValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = Color.White;
            labelContrast.Location = new Point(7, 69);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(52, 15);
            labelContrast.TabIndex = 6;
            labelContrast.Text = "Contrast";
            // 
            // trackBarContrast
            // 
            trackBarContrast.BackColor = Color.Black;
            trackBarContrast.Location = new Point(107, 67);
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
            // labelContrastValue
            // 
            labelContrastValue.AutoSize = true;
            labelContrastValue.ForeColor = Color.White;
            labelContrastValue.Location = new Point(77, 69);
            labelContrastValue.Name = "labelContrastValue";
            labelContrastValue.Size = new Size(25, 15);
            labelContrastValue.TabIndex = 7;
            labelContrastValue.Text = "100";
            labelContrastValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBoxMonitors
            // 
            comboBoxMonitors.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMonitors.Location = new Point(275, 114);
            comboBoxMonitors.Name = "comboBoxMonitors";
            comboBoxMonitors.Size = new Size(100, 23);
            comboBoxMonitors.TabIndex = 9;
            comboBoxMonitors.SelectedIndexChanged += comboBoxMonitors_SelectedIndexChanged;
            // 
            // buttonReset
            // 
            buttonReset.ForeColor = SystemColors.ControlLightLight;
            buttonReset.Location = new Point(275, 142);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(100, 43);
            buttonReset.TabIndex = 10;
            buttonReset.Text = "⛧ Reset ⛧";
            buttonReset.Click += buttonReset_Click;
            // 
            // panelGraph
            // 
            panelGraph.BackColor = Color.Black;
            panelGraph.Controls.Add(label1);
            panelGraph.Location = new Point(275, 7);
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
            // textBoxAR1
            // 
            textBoxAR1.Location = new Point(88, 100);
            textBoxAR1.Name = "textBoxAR1";
            textBoxAR1.Size = new Size(40, 23);
            textBoxAR1.TabIndex = 12;
            textBoxAR1.KeyDown += textBoxAR1_KeyDown;
            // 
            // textBoxAR2
            // 
            textBoxAR2.Location = new Point(88, 130);
            textBoxAR2.Name = "textBoxAR2";
            textBoxAR2.Size = new Size(40, 23);
            textBoxAR2.TabIndex = 16;
            textBoxAR2.KeyDown += textBoxAR2_KeyDown;
            // 
            // textBoxTargetAR
            // 
            textBoxTargetAR.Location = new Point(88, 160);
            textBoxTargetAR.Name = "textBoxTargetAR";
            textBoxTargetAR.Size = new Size(40, 23);
            textBoxTargetAR.TabIndex = 20;
            textBoxTargetAR.KeyDown += textBoxTargetAR_KeyDown;
            // 
            // checkBoxDT1
            // 
            checkBoxDT1.ForeColor = Color.Gray;
            checkBoxDT1.Location = new Point(134, 96);
            checkBoxDT1.Name = "checkBoxDT1";
            checkBoxDT1.Size = new Size(42, 23);
            checkBoxDT1.TabIndex = 13;
            checkBoxDT1.Text = "DT";
            // 
            // checkBoxDT2
            // 
            checkBoxDT2.ForeColor = Color.Gray;
            checkBoxDT2.Location = new Point(134, 126);
            checkBoxDT2.Name = "checkBoxDT2";
            checkBoxDT2.Size = new Size(40, 23);
            checkBoxDT2.TabIndex = 17;
            checkBoxDT2.Text = "DT";
            // 
            // checkBoxDT3
            // 
            checkBoxDT3.ForeColor = Color.Gray;
            checkBoxDT3.Location = new Point(134, 156);
            checkBoxDT3.Name = "checkBoxDT3";
            checkBoxDT3.Size = new Size(40, 23);
            checkBoxDT3.TabIndex = 21;
            checkBoxDT3.Text = "DT";
            // 
            // buttonSaveAR1
            // 
            buttonSaveAR1.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR1.Location = new Point(7, 98);
            buttonSaveAR1.Name = "buttonSaveAR1";
            buttonSaveAR1.Size = new Size(75, 27);
            buttonSaveAR1.TabIndex = 14;
            buttonSaveAR1.Text = "AR1";
            buttonSaveAR1.Click += buttonSaveAR1_Click;
            // 
            // buttonSaveAR2
            // 
            buttonSaveAR2.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR2.Location = new Point(7, 128);
            buttonSaveAR2.Name = "buttonSaveAR2";
            buttonSaveAR2.Size = new Size(75, 27);
            buttonSaveAR2.TabIndex = 18;
            buttonSaveAR2.Text = "AR2";
            buttonSaveAR2.Click += buttonSaveAR2_Click;
            // 
            // labelAR1
            // 
            labelAR1.AutoSize = true;
            labelAR1.Font = new Font("Segoe UI", 6.4F);
            labelAR1.ForeColor = Color.White;
            labelAR1.Location = new Point(132, 114);
            labelAR1.Name = "labelAR1";
            labelAR1.Size = new Size(29, 12);
            labelAR1.TabIndex = 15;
            labelAR1.Text = "AR1: -";
            // 
            // labelAR2
            // 
            labelAR2.AutoSize = true;
            labelAR2.Font = new Font("Segoe UI", 6.4F);
            labelAR2.ForeColor = Color.White;
            labelAR2.Location = new Point(132, 144);
            labelAR2.Name = "labelAR2";
            labelAR2.Size = new Size(29, 12);
            labelAR2.TabIndex = 19;
            labelAR2.Text = "AR2: -";
            // 
            // labelAR3
            // 
            labelAR3.AutoSize = true;
            labelAR3.Font = new Font("Segoe UI", 6.4F);
            labelAR3.ForeColor = Color.White;
            labelAR3.Location = new Point(132, 174);
            labelAR3.Name = "labelAR3";
            labelAR3.Size = new Size(29, 12);
            labelAR3.TabIndex = 23;
            labelAR3.Text = "AR3: -";
            // 
            // buttonSetAR3
            // 
            buttonSetAR3.ForeColor = SystemColors.ControlLightLight;
            buttonSetAR3.Location = new Point(7, 158);
            buttonSetAR3.Name = "buttonSetAR3";
            buttonSetAR3.Size = new Size(75, 27);
            buttonSetAR3.TabIndex = 22;
            buttonSetAR3.Text = "Calculate";
            buttonSetAR3.Click += buttonSetAR3_Click;
            // 
            // buttonSaveProfile
            // 
            buttonSaveProfile.Font = new Font("Segoe UI", 9F);
            buttonSaveProfile.ForeColor = SystemColors.ControlLightLight;
            buttonSaveProfile.Location = new Point(7, 188);
            buttonSaveProfile.Name = "buttonSaveProfile";
            buttonSaveProfile.Size = new Size(75, 27);
            buttonSaveProfile.TabIndex = 25;
            buttonSaveProfile.Text = "Save";
            buttonSaveProfile.Click += buttonSaveProfile_Click;
            // 
            // buttonLoadProfile
            // 
            buttonLoadProfile.Font = new Font("Segoe UI", 9F);
            buttonLoadProfile.ForeColor = SystemColors.ControlLightLight;
            buttonLoadProfile.Location = new Point(194, 188);
            buttonLoadProfile.Name = "buttonLoadProfile";
            buttonLoadProfile.Size = new Size(75, 27);
            buttonLoadProfile.TabIndex = 26;
            buttonLoadProfile.Text = "Load";
            buttonLoadProfile.Click += buttonLoadProfile_Click;
            // 
            // textBoxProfileName
            // 
            textBoxProfileName.Location = new Point(88, 190);
            textBoxProfileName.Name = "textBoxProfileName";
            textBoxProfileName.Size = new Size(100, 23);
            textBoxProfileName.TabIndex = 27;
            // 
            // comboBoxProfiles
            // 
            comboBoxProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProfiles.FormattingEnabled = true;
            comboBoxProfiles.Location = new Point(275, 190);
            comboBoxProfiles.Name = "comboBoxProfiles";
            comboBoxProfiles.Size = new Size(100, 23);
            comboBoxProfiles.TabIndex = 29;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(383, 221);
            Controls.Add(panelGraph);
            Controls.Add(buttonReset);
            Controls.Add(comboBoxMonitors);
            Controls.Add(labelAR1);
            Controls.Add(comboBoxProfiles);
            Controls.Add(textBoxProfileName);
            Controls.Add(buttonLoadProfile);
            Controls.Add(buttonSaveProfile);
            Controls.Add(labelAR3);
            Controls.Add(checkBoxDT3);
            Controls.Add(labelBrightnessValue);
            Controls.Add(labelAR2);
            Controls.Add(textBoxAR1);
            Controls.Add(checkBoxDT1);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarBrightness);
            Controls.Add(labelGamma);
            Controls.Add(labelGammaValue);
            Controls.Add(trackBarGamma);
            Controls.Add(labelBrightness);
            Controls.Add(labelContrast);
            Controls.Add(labelContrastValue);
            Controls.Add(buttonSaveAR1);
            Controls.Add(textBoxAR2);
            Controls.Add(checkBoxDT2);
            Controls.Add(buttonSaveAR2);
            Controls.Add(textBoxTargetAR);
            Controls.Add(buttonSetAR3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "gamma slave extra";
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            panelGraph.ResumeLayout(false);
            panelGraph.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label1;
    }
}