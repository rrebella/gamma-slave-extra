namespace rebellagamma
{
    partial class ViewBonus
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.CheckBox chkBonusAR;
        public System.Windows.Forms.Label lblShadowWidth;
        public System.Windows.Forms.TrackBar tbShadowWidth;
        public System.Windows.Forms.NumericUpDown numShadowWidth;
        
        public System.Windows.Forms.Label lblShadowSoftness;
        public System.Windows.Forms.TrackBar tbShadowSoftness;
        public System.Windows.Forms.NumericUpDown numShadowSoftness;

        public System.Windows.Forms.Label lblShadowOpacity;
        public System.Windows.Forms.TrackBar tbShadowOpacity;
        public System.Windows.Forms.NumericUpDown numShadowOpacity;
                public System.Windows.Forms.Label lblBonusInfo;
        public System.Windows.Forms.ComboBox cmbBonusProfiles;
        public System.Windows.Forms.TextBox tbBonusRename;
        public System.Windows.Forms.Button btnBonusRename;
        public System.Windows.Forms.Button btnBonusDelete;
        public System.Windows.Forms.Button btnBonusRevert;
        
        public System.Windows.Forms.Label lblBonusImage;
        public System.Windows.Forms.TextBox tbBonusImage;
        public System.Windows.Forms.Button btnBonusSaveImage;

        public System.Windows.Forms.CheckBox chkDefaultOnStartup;
        public System.Windows.Forms.ComboBox cmbDefaultConfig;
        public System.Windows.Forms.CheckBox chkResetOnExit;

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
            chkBonusAR = new CheckBox();
            lblShadowWidth = new Label();
            tbShadowWidth = new TrackBar();
            numShadowWidth = new NumericUpDown();
            lblShadowSoftness = new Label();
            tbShadowSoftness = new TrackBar();
            numShadowSoftness = new NumericUpDown();
            lblShadowOpacity = new Label();
            tbShadowOpacity = new TrackBar();
            numShadowOpacity = new NumericUpDown();
            lblBonusInfo = new Label();
            cmbBonusProfiles = new ComboBox();
            tbBonusRename = new TextBox();
            btnBonusRename = new Button();
            btnBonusDelete = new Button();
            btnBonusRevert = new Button();
            lblBonusImage = new Label();
            tbBonusImage = new TextBox();
            btnBonusSaveImage = new Button();
            chkDefaultOnStartup = new CheckBox();
            cmbDefaultConfig = new ComboBox();
            chkResetOnExit = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)tbShadowWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShadowWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbShadowSoftness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShadowSoftness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbShadowOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShadowOpacity).BeginInit();
            SuspendLayout();
            // 
            // chkBonusAR
            // 
            chkBonusAR.AutoSize = true;
            chkBonusAR.ForeColor = Color.White;
            chkBonusAR.Location = new Point(127, 8);
            chkBonusAR.Name = "chkBonusAR";
            chkBonusAR.Size = new Size(243, 19);
            chkBonusAR.TabIndex = 1;
            chkBonusAR.Text = "Enable custom .png (hides AR calculator)";
            chkBonusAR.UseVisualStyleBackColor = true;
            // 
            // lblShadowWidth
            // 
            lblShadowWidth.AutoSize = true;
            lblShadowWidth.ForeColor = Color.White;
            lblShadowWidth.Location = new Point(124, 31);
            lblShadowWidth.Name = "lblShadowWidth";
            lblShadowWidth.Size = new Size(79, 15);
            lblShadowWidth.TabIndex = 2;
            lblShadowWidth.Text = "Outline width";
            // 
            // tbShadowWidth
            // 
            tbShadowWidth.Location = new Point(202, 30);
            tbShadowWidth.Maximum = 9;
            tbShadowWidth.Name = "tbShadowWidth";
            tbShadowWidth.Size = new Size(125, 45);
            tbShadowWidth.TabIndex = 3;
            tbShadowWidth.TickStyle = TickStyle.None;
            tbShadowWidth.Value = 6;
            // 
            // numShadowWidth
            // 
            numShadowWidth.BackColor = Color.White;
            numShadowWidth.ForeColor = Color.Black;
            numShadowWidth.Location = new Point(329, 29);
            numShadowWidth.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            numShadowWidth.Name = "numShadowWidth";
            numShadowWidth.Size = new Size(46, 23);
            numShadowWidth.TabIndex = 4;
            numShadowWidth.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // lblShadowSoftness
            // 
            lblShadowSoftness.AutoSize = true;
            lblShadowSoftness.ForeColor = Color.White;
            lblShadowSoftness.Location = new Point(124, 58);
            lblShadowSoftness.Name = "lblShadowSoftness";
            lblShadowSoftness.Size = new Size(77, 15);
            lblShadowSoftness.TabIndex = 5;
            lblShadowSoftness.Text = "Softness fade";
            // 
            // tbShadowSoftness
            // 
            tbShadowSoftness.Location = new Point(202, 57);
            tbShadowSoftness.Maximum = 100;
            tbShadowSoftness.Minimum = 50;
            tbShadowSoftness.Name = "tbShadowSoftness";
            tbShadowSoftness.Size = new Size(125, 45);
            tbShadowSoftness.TabIndex = 6;
            tbShadowSoftness.TickFrequency = 5;
            tbShadowSoftness.TickStyle = TickStyle.None;
            tbShadowSoftness.Value = 75;
            // 
            // numShadowSoftness
            // 
            numShadowSoftness.BackColor = Color.White;
            numShadowSoftness.ForeColor = Color.Black;
            numShadowSoftness.Location = new Point(329, 56);
            numShadowSoftness.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numShadowSoftness.Name = "numShadowSoftness";
            numShadowSoftness.Size = new Size(46, 23);
            numShadowSoftness.TabIndex = 7;
            numShadowSoftness.Value = new decimal(new int[] { 75, 0, 0, 0 });
            // 
            // lblShadowOpacity
            // 
            lblShadowOpacity.AutoSize = true;
            lblShadowOpacity.ForeColor = Color.White;
            lblShadowOpacity.Location = new Point(124, 85);
            lblShadowOpacity.Name = "lblShadowOpacity";
            lblShadowOpacity.Size = new Size(77, 15);
            lblShadowOpacity.TabIndex = 8;
            lblShadowOpacity.Text = "Black opacity";
            // 
            // tbShadowOpacity
            // 
            tbShadowOpacity.Location = new Point(202, 84);
            tbShadowOpacity.Maximum = 255;
            tbShadowOpacity.Name = "tbShadowOpacity";
            tbShadowOpacity.Size = new Size(125, 45);
            tbShadowOpacity.TabIndex = 9;
            tbShadowOpacity.TickFrequency = 15;
            tbShadowOpacity.TickStyle = TickStyle.None;
            tbShadowOpacity.Value = 255;
            // 
            // numShadowOpacity
            // 
            numShadowOpacity.BackColor = Color.White;
            numShadowOpacity.ForeColor = Color.Black;
            numShadowOpacity.Location = new Point(329, 83);
            numShadowOpacity.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numShadowOpacity.Name = "numShadowOpacity";
            numShadowOpacity.Size = new Size(46, 23);
            numShadowOpacity.TabIndex = 10;
            numShadowOpacity.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // lblBonusInfo
            // 
            lblBonusInfo.AutoSize = true;
            lblBonusInfo.ForeColor = Color.DarkGray;
            lblBonusInfo.Location = new Point(4, 6);
            lblBonusInfo.Name = "lblBonusInfo";
            lblBonusInfo.Size = new Size(105, 150);
            lblBonusInfo.TabIndex = 11;
            lblBonusInfo.Text = "Place slave.png in\r\nthe app folder to\r\ndisplay an image\r\n(optimal: 258x86).\r\nFrame settings are\r\non the right side.\r\nYou can also link\r\nanother .png file\r\nto a chosen config\r\nin the area below.";
            // 
            // cmbBonusProfiles
            // 
            cmbBonusProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBonusProfiles.Location = new Point(7, 159);
            cmbBonusProfiles.Name = "cmbBonusProfiles";
            cmbBonusProfiles.Size = new Size(105, 23);
            cmbBonusProfiles.TabIndex = 13;
            // 
            // tbBonusRename
            // 
            tbBonusRename.Location = new Point(119, 159);
            tbBonusRename.Name = "tbBonusRename";
            tbBonusRename.Size = new Size(96, 23);
            tbBonusRename.TabIndex = 14;
            // 
            // btnBonusRename
            // 
            btnBonusRename.ForeColor = Color.White;
            btnBonusRename.Location = new Point(221, 157);
            btnBonusRename.Name = "btnBonusRename";
            btnBonusRename.Size = new Size(75, 27);
            btnBonusRename.TabIndex = 15;
            btnBonusRename.Text = "Rename";
            btnBonusRename.Click += btnBonusRename_Click;
            // 
            // btnBonusDelete
            // 
            btnBonusDelete.ForeColor = Color.White;
            btnBonusDelete.Location = new Point(301, 157);
            btnBonusDelete.Name = "btnBonusDelete";
            btnBonusDelete.Size = new Size(75, 27);
            btnBonusDelete.TabIndex = 16;
            btnBonusDelete.Text = "Delete";
            // 
            // btnBonusRevert
            // 
            btnBonusRevert.ForeColor = Color.White;
            btnBonusRevert.Location = new Point(301, 188);
            btnBonusRevert.Name = "btnBonusRevert";
            btnBonusRevert.Size = new Size(75, 27);
            btnBonusRevert.TabIndex = 17;
            btnBonusRevert.Text = "Undo";
            // 
            // lblBonusImage
            // 
            lblBonusImage.AutoSize = true;
            lblBonusImage.ForeColor = Color.White;
            lblBonusImage.Location = new Point(4, 193);
            lblBonusImage.Name = "lblBonusImage";
            lblBonusImage.Size = new Size(112, 15);
            lblBonusImage.TabIndex = 18;
            lblBonusImage.Text = "Custom .png name:";
            // 
            // tbBonusImage
            // 
            tbBonusImage.Location = new Point(119, 190);
            tbBonusImage.Name = "tbBonusImage";
            tbBonusImage.Size = new Size(96, 23);
            tbBonusImage.TabIndex = 19;
            // 
            // btnBonusSaveImage
            // 
            btnBonusSaveImage.ForeColor = Color.White;
            btnBonusSaveImage.Location = new Point(221, 188);
            btnBonusSaveImage.Name = "btnBonusSaveImage";
            btnBonusSaveImage.Size = new Size(75, 27);
            btnBonusSaveImage.TabIndex = 20;
            btnBonusSaveImage.Text = "Save";
            // 
            // chkDefaultOnStartup
            // 
            chkDefaultOnStartup.AutoSize = true;
            chkDefaultOnStartup.ForeColor = Color.White;
            chkDefaultOnStartup.Location = new Point(128, 132);
            chkDefaultOnStartup.Name = "chkDefaultOnStartup";
            chkDefaultOnStartup.Size = new Size(148, 19);
            chkDefaultOnStartup.TabIndex = 21;
            chkDefaultOnStartup.Text = "Use config as a default:";
            chkDefaultOnStartup.UseVisualStyleBackColor = true;
            // 
            // cmbDefaultConfig
            // 
            cmbDefaultConfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultConfig.Location = new Point(275, 128);
            cmbDefaultConfig.Name = "cmbDefaultConfig";
            cmbDefaultConfig.Size = new Size(100, 23);
            cmbDefaultConfig.TabIndex = 22;
            // 
            // chkResetOnExit
            // 
            chkResetOnExit.AutoSize = true;
            chkResetOnExit.ForeColor = Color.White;
            chkResetOnExit.Location = new Point(128, 107);
            chkResetOnExit.Name = "chkResetOnExit";
            chkResetOnExit.Size = new Size(190, 19);
            chkResetOnExit.TabIndex = 23;
            chkResetOnExit.Text = "Reset to default settings on exit";
            chkResetOnExit.UseVisualStyleBackColor = true;
            // 
            // ViewBonus
            // 
            BackColor = Color.Black;
            Controls.Add(tbBonusImage);
            Controls.Add(cmbDefaultConfig);
            Controls.Add(chkDefaultOnStartup);
            Controls.Add(cmbBonusProfiles);
            Controls.Add(tbBonusRename);
            Controls.Add(lblShadowOpacity);
            Controls.Add(numShadowOpacity);
            Controls.Add(lblShadowSoftness);
            Controls.Add(numShadowSoftness);
            Controls.Add(lblShadowWidth);
            Controls.Add(numShadowWidth);
            Controls.Add(btnBonusRename);
            Controls.Add(btnBonusDelete);
            Controls.Add(btnBonusRevert);
            Controls.Add(lblBonusImage);
            Controls.Add(btnBonusSaveImage);
            Controls.Add(chkBonusAR);
            Controls.Add(lblBonusInfo);
            Controls.Add(chkResetOnExit);
            Controls.Add(tbShadowOpacity);
            Controls.Add(tbShadowSoftness);
            Controls.Add(tbShadowWidth);
            Name = "ViewBonus";
            Size = new Size(385, 224);
            ((System.ComponentModel.ISupportInitialize)tbShadowWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShadowWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbShadowSoftness).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShadowSoftness).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbShadowOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShadowOpacity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
