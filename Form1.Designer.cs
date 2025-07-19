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
        private Label labelContrast;
        private ComboBox comboBoxMonitors;
        private Button buttonReset;
        private Panel panelGraph;
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
            labelContrast = new Label();
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
            numericUpDownAR1 = new NumericUpDown();
            numericUpDownAR2 = new NumericUpDown();
            numericUpDownAR3 = new NumericUpDown();
            trackBarBrightness = new TrackBar();
            trackBarContrast = new TrackBar();
            labelDisplay = new Label();
            numericUpDownGamma = new NumericUpDown();
            numericUpDownBrightness = new NumericUpDown();
            numericUpDownContrast = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            panelGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownContrast).BeginInit();
            SuspendLayout();
            // 
            // labelGamma
            // 
            labelGamma.AutoSize = true;
            labelGamma.ForeColor = Color.White;
            labelGamma.Location = new Point(7, 7);
            labelGamma.Name = "labelGamma";
            labelGamma.Size = new Size(48, 15);
            labelGamma.TabIndex = 0;
            labelGamma.Text = "gamma";
            // 
            // trackBarGamma
            // 
            trackBarGamma.AutoSize = false;
            trackBarGamma.BackColor = Color.Black;
            trackBarGamma.LargeChange = 10;
            trackBarGamma.Location = new Point(73, 27);
            trackBarGamma.Maximum = 888;
            trackBarGamma.Minimum = 8;
            trackBarGamma.Name = "trackBarGamma";
            trackBarGamma.Size = new Size(197, 27);
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
            labelBrightness.Location = new Point(7, 52);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(62, 15);
            labelBrightness.TabIndex = 3;
            labelBrightness.Text = "brightness";
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = Color.White;
            labelContrast.Location = new Point(7, 95);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(50, 15);
            labelContrast.TabIndex = 6;
            labelContrast.Text = "contrast";
            // 
            // comboBoxMonitors
            // 
            comboBoxMonitors.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMonitors.Location = new Point(275, 135);
            comboBoxMonitors.Name = "comboBoxMonitors";
            comboBoxMonitors.Size = new Size(100, 23);
            comboBoxMonitors.TabIndex = 9;
            comboBoxMonitors.SelectedIndexChanged += comboBoxMonitors_SelectedIndexChanged;
            // 
            // buttonReset
            // 
            buttonReset.ForeColor = SystemColors.ControlLightLight;
            buttonReset.Location = new Point(275, 165);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(100, 69);
            buttonReset.TabIndex = 10;
            buttonReset.Text = "⛧ reset ⛧";
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
            checkBoxDT1.Location = new Point(134, 145);
            checkBoxDT1.Name = "checkBoxDT1";
            checkBoxDT1.Size = new Size(42, 23);
            checkBoxDT1.TabIndex = 13;
            checkBoxDT1.Text = "DT";
            // 
            // checkBoxDT2
            // 
            checkBoxDT2.ForeColor = Color.Gray;
            checkBoxDT2.Location = new Point(134, 175);
            checkBoxDT2.Name = "checkBoxDT2";
            checkBoxDT2.Size = new Size(40, 23);
            checkBoxDT2.TabIndex = 17;
            checkBoxDT2.Text = "DT";
            // 
            // checkBoxDT3
            // 
            checkBoxDT3.ForeColor = Color.Gray;
            checkBoxDT3.Location = new Point(134, 205);
            checkBoxDT3.Name = "checkBoxDT3";
            checkBoxDT3.Size = new Size(40, 23);
            checkBoxDT3.TabIndex = 21;
            checkBoxDT3.Text = "DT";
            // 
            // buttonSaveAR1
            // 
            buttonSaveAR1.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR1.Location = new Point(7, 147);
            buttonSaveAR1.Name = "buttonSaveAR1";
            buttonSaveAR1.Size = new Size(70, 27);
            buttonSaveAR1.TabIndex = 14;
            buttonSaveAR1.Text = "save ar1";
            buttonSaveAR1.Click += buttonSaveAR1_Click;
            // 
            // buttonSaveAR2
            // 
            buttonSaveAR2.ForeColor = SystemColors.ControlLightLight;
            buttonSaveAR2.Location = new Point(7, 177);
            buttonSaveAR2.Name = "buttonSaveAR2";
            buttonSaveAR2.Size = new Size(70, 27);
            buttonSaveAR2.TabIndex = 18;
            buttonSaveAR2.Text = "save ar2";
            buttonSaveAR2.Click += buttonSaveAR2_Click;
            // 
            // labelAR1
            // 
            labelAR1.AutoSize = true;
            labelAR1.Font = new Font("Segoe UI", 6.4F);
            labelAR1.ForeColor = Color.White;
            labelAR1.Location = new Point(132, 163);
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
            labelAR2.Location = new Point(132, 193);
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
            labelAR3.Location = new Point(132, 223);
            labelAR3.Name = "labelAR3";
            labelAR3.Size = new Size(29, 12);
            labelAR3.TabIndex = 23;
            labelAR3.Text = "AR3: -";
            // 
            // buttonSetAR3
            // 
            buttonSetAR3.ForeColor = SystemColors.ControlLightLight;
            buttonSetAR3.Location = new Point(7, 207);
            buttonSetAR3.Name = "buttonSetAR3";
            buttonSetAR3.Size = new Size(70, 27);
            buttonSetAR3.TabIndex = 22;
            buttonSetAR3.Text = "calculate";
            buttonSetAR3.Click += buttonSetAR3_Click;
            // 
            // buttonSaveProfile
            // 
            buttonSaveProfile.Font = new Font("Segoe UI", 9F);
            buttonSaveProfile.ForeColor = SystemColors.ControlLightLight;
            buttonSaveProfile.Location = new Point(7, 237);
            buttonSaveProfile.Name = "buttonSaveProfile";
            buttonSaveProfile.Size = new Size(70, 27);
            buttonSaveProfile.TabIndex = 25;
            buttonSaveProfile.Text = "save cfg";
            buttonSaveProfile.Click += buttonSaveProfile_Click;
            // 
            // buttonLoadProfile
            // 
            buttonLoadProfile.Font = new Font("Segoe UI", 9F);
            buttonLoadProfile.ForeColor = SystemColors.ControlLightLight;
            buttonLoadProfile.Location = new Point(194, 237);
            buttonLoadProfile.Name = "buttonLoadProfile";
            buttonLoadProfile.Size = new Size(75, 27);
            buttonLoadProfile.TabIndex = 26;
            buttonLoadProfile.Text = "load cfg";
            buttonLoadProfile.Click += buttonLoadProfile_Click;
            // 
            // textBoxProfileName
            // 
            textBoxProfileName.Location = new Point(83, 239);
            textBoxProfileName.Name = "textBoxProfileName";
            textBoxProfileName.Size = new Size(105, 23);
            textBoxProfileName.TabIndex = 27;
            // 
            // comboBoxProfiles
            // 
            comboBoxProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProfiles.FormattingEnabled = true;
            comboBoxProfiles.Location = new Point(275, 239);
            comboBoxProfiles.Name = "comboBoxProfiles";
            comboBoxProfiles.Size = new Size(100, 23);
            comboBoxProfiles.TabIndex = 29;
            // 
            // numericUpDownAR1
            // 
            numericUpDownAR1.DecimalPlaces = 1;
            numericUpDownAR1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR1.Location = new Point(83, 149);
            numericUpDownAR1.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR1.Name = "numericUpDownAR1";
            numericUpDownAR1.Size = new Size(45, 23);
            numericUpDownAR1.TabIndex = 30;
            // 
            // numericUpDownAR2
            // 
            numericUpDownAR2.DecimalPlaces = 1;
            numericUpDownAR2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR2.Location = new Point(83, 179);
            numericUpDownAR2.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR2.Name = "numericUpDownAR2";
            numericUpDownAR2.Size = new Size(45, 23);
            numericUpDownAR2.TabIndex = 31;
            // 
            // numericUpDownAR3
            // 
            numericUpDownAR3.DecimalPlaces = 1;
            numericUpDownAR3.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownAR3.Location = new Point(83, 209);
            numericUpDownAR3.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownAR3.Name = "numericUpDownAR3";
            numericUpDownAR3.Size = new Size(45, 23);
            numericUpDownAR3.TabIndex = 32;
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.AutoSize = false;
            trackBarBrightness.BackColor = Color.Black;
            trackBarBrightness.LargeChange = 10;
            trackBarBrightness.Location = new Point(73, 72);
            trackBarBrightness.Maximum = 888;
            trackBarBrightness.Minimum = -888;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(197, 27);
            trackBarBrightness.TabIndex = 33;
            trackBarBrightness.TickFrequency = 100;
            trackBarBrightness.TickStyle = TickStyle.None;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            // 
            // trackBarContrast
            // 
            trackBarContrast.AutoSize = false;
            trackBarContrast.BackColor = Color.Black;
            trackBarContrast.LargeChange = 10;
            trackBarContrast.Location = new Point(73, 115);
            trackBarContrast.Maximum = 888;
            trackBarContrast.Minimum = 8;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(197, 27);
            trackBarContrast.TabIndex = 34;
            trackBarContrast.TickFrequency = 100;
            trackBarContrast.TickStyle = TickStyle.None;
            trackBarContrast.Value = 100;
            trackBarContrast.Scroll += trackBarContrast_Scroll;
            // 
            // labelDisplay
            // 
            labelDisplay.AutoSize = true;
            labelDisplay.ForeColor = Color.White;
            labelDisplay.Location = new Point(273, 117);
            labelDisplay.Name = "labelDisplay";
            labelDisplay.Size = new Size(81, 15);
            labelDisplay.TabIndex = 35;
            labelDisplay.Text = "target display:";
            // 
            // numericUpDownGamma
            // 
            numericUpDownGamma.DecimalPlaces = 2;
            numericUpDownGamma.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            numericUpDownGamma.Location = new Point(10, 26);
            numericUpDownGamma.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            numericUpDownGamma.Name = "numericUpDownGamma";
            numericUpDownGamma.Size = new Size(59, 23);
            numericUpDownGamma.TabIndex = 36;
            numericUpDownGamma.ValueChanged += numericUpDownGamma_ValueChanged;
            // 
            // numericUpDownBrightness
            // 
            numericUpDownBrightness.Location = new Point(10, 70);
            numericUpDownBrightness.Maximum = new decimal(new int[] { 888, 0, 0, 0 });
            numericUpDownBrightness.Minimum = new decimal(new int[] { 888, 0, 0, int.MinValue });
            numericUpDownBrightness.Name = "numericUpDownBrightness";
            numericUpDownBrightness.Size = new Size(59, 23);
            numericUpDownBrightness.TabIndex = 37;
            numericUpDownBrightness.ValueChanged += numericUpDownBrightness_ValueChanged;
            // 
            // numericUpDownContrast
            // 
            numericUpDownContrast.Location = new Point(10, 113);
            numericUpDownContrast.Maximum = new decimal(new int[] { 888, 0, 0, 0 });
            numericUpDownContrast.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownContrast.Name = "numericUpDownContrast";
            numericUpDownContrast.Size = new Size(59, 23);
            numericUpDownContrast.TabIndex = 38;
            numericUpDownContrast.Value = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownContrast.ValueChanged += numericUpDownContrast_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(383, 270);
            Controls.Add(numericUpDownContrast);
            Controls.Add(numericUpDownBrightness);
            Controls.Add(numericUpDownGamma);
            Controls.Add(labelDisplay);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarBrightness);
            Controls.Add(numericUpDownAR3);
            Controls.Add(numericUpDownAR2);
            Controls.Add(numericUpDownAR1);
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
            Controls.Add(labelGamma);
            Controls.Add(trackBarGamma);
            Controls.Add(labelBrightness);
            Controls.Add(labelContrast);
            Controls.Add(buttonSaveAR1);
            Controls.Add(checkBoxDT2);
            Controls.Add(buttonSaveAR2);
            Controls.Add(buttonSetAR3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "gamma slave extra";
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            panelGraph.ResumeLayout(false);
            panelGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAR3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownContrast).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label1;
        private NumericUpDown numericUpDownAR1;
        private NumericUpDown numericUpDownAR2;
        private NumericUpDown numericUpDownAR3;
        private TrackBar trackBarBrightness;
        private TrackBar trackBarContrast;
        private Label labelDisplay;
        private NumericUpDown numericUpDownGamma;
        private NumericUpDown numericUpDownBrightness;
        private NumericUpDown numericUpDownContrast;
    }
}