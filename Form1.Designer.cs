using System.Drawing;
using System.Windows.Forms;

namespace rebellagamma
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            labelGamma = new Label();
            trackBarGamma = new TrackBar();
            labelBrightness = new Label();
            trackBarBrightness = new TrackBar();
            labelContrast = new Label();
            trackBarContrast = new TrackBar();
            comboBoxMonitors = new ComboBox();
            buttonReset = new Button();
            panelGraph = new Panel();
            label1 = new Label();
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
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDownAR1 = new NumericUpDown();
            numericUpDownAR2 = new NumericUpDown();
            numericUpDownTargetAR = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetAR).BeginInit();
            SuspendLayout();
            // 
            // labelGamma
            // 
            labelGamma.AutoSize = true;
            labelGamma.ForeColor = Color.White;
            labelGamma.Location = new Point(7, 13);
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
            trackBarGamma.Location = new Point(117, 11);
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
            labelBrightness.Location = new Point(7, 42);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(62, 15);
            labelBrightness.TabIndex = 3;
            labelBrightness.Text = "Brightness";
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.BackColor = Color.Black;
            trackBarBrightness.Location = new Point(117, 40);
            trackBarBrightness.Maximum = 888;
            trackBarBrightness.Minimum = -888;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(160, 45);
            trackBarBrightness.TabIndex = 5;
            trackBarBrightness.TickStyle = TickStyle.None;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            trackBarBrightness.MouseDown += trackBar_MouseDown_RemoveFocus;
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = Color.White;
            labelContrast.Location = new Point(7, 71);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(52, 15);
            labelContrast.TabIndex = 6;
            labelContrast.Text = "Contrast";
            // 
            // trackBarContrast
            // 
            trackBarContrast.BackColor = Color.Black;
            trackBarContrast.Location = new Point(117, 69);
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
            // checkBoxDT1
            // 
            checkBoxDT1.ForeColor = Color.Gray;
            checkBoxDT1.Location = new Point(138, 96);
            checkBoxDT1.Name = "checkBoxDT1";
            checkBoxDT1.Size = new Size(42, 23);
            checkBoxDT1.TabIndex = 13;
            checkBoxDT1.Text = "DT";
            // 
            // checkBoxDT2
            // 
            checkBoxDT2.ForeColor = Color.Gray;
            checkBoxDT2.Location = new Point(138, 126);
            checkBoxDT2.Name = "checkBoxDT2";
            checkBoxDT2.Size = new Size(40, 23);
            checkBoxDT2.TabIndex = 17;
            checkBoxDT2.Text = "DT";
            // 
            // checkBoxDT3
            // 
            checkBoxDT3.ForeColor = Color.Gray;
            checkBoxDT3.Location = new Point(138, 156);
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
            labelAR1.Location = new Point(136, 114);
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
            labelAR2.Location = new Point(136, 144);
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
            labelAR3.Location = new Point(136, 174);
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
            textBoxProfileName.KeyDown += textBoxProfileName_KeyDown;
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
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown1.Location = new Point(72, 10);
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
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown2.Location = new Point(72, 39);
            numericUpDown2.Maximum = new decimal(new int[] { 888, 0, 0, 131072 });
            numericUpDown2.Minimum = new decimal(new int[] { 888, 0, 0, -2147352576 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(46, 23);
            numericUpDown2.TabIndex = 31;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            numericUpDown2.KeyDown += numericUpDownKeyDown;
            numericUpDown2.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDown3
            // 
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown3.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericUpDown3.Location = new Point(72, 68);
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
            // numericUpDownAR1
            // 
            numericUpDownAR1.DecimalPlaces = 1;
            numericUpDownAR1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR1.Location = new Point(88, 100);
            numericUpDownAR1.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR1.Name = "numericUpDownAR1";
            numericUpDownAR1.Size = new Size(44, 23);
            numericUpDownAR1.TabIndex = 12;
            numericUpDownAR1.KeyDown += numericUpDownAR_KeyDown;
            numericUpDownAR1.KeyPress += numericUpDownKeyPress;
            // 
            // numericUpDownAR2
            // 
            numericUpDownAR2.DecimalPlaces = 1;
            numericUpDownAR2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR2.Location = new Point(88, 130);
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
            numericUpDownTargetAR.Location = new Point(88, 160);
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
            ClientSize = new Size(383, 221);
            Controls.Add(numericUpDownAR1);
            Controls.Add(numericUpDown3);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
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
            Controls.Add(labelAR2);
            Controls.Add(checkBoxDT1);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarBrightness);
            Controls.Add(labelGamma);
            Controls.Add(trackBarGamma);
            Controls.Add(labelBrightness);
            Controls.Add(labelContrast);
            Controls.Add(buttonSaveAR1);
            Controls.Add(checkBoxDT2);
            Controls.Add(buttonSaveAR2);
            Controls.Add(buttonSetAR3);
            Controls.Add(numericUpDownAR2);
            Controls.Add(numericUpDownTargetAR);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "gamma slave extra";
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            panelGraph.ResumeLayout(false);
            panelGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTargetAR).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
    }
}