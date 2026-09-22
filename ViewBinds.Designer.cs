namespace rebellagamma
{
    partial class ViewBinds
    {
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.CheckBox chkGamma;
        public System.Windows.Forms.NumericUpDown nudGamma;
        public System.Windows.Forms.Label lblGammaDown;
        public System.Windows.Forms.Label lblGammaUp;
        public System.Windows.Forms.FlowLayoutPanel pnlGammaDown;
        public System.Windows.Forms.FlowLayoutPanel pnlGammaUp;
        public System.Windows.Forms.CheckBox chkBrightness;
        public System.Windows.Forms.NumericUpDown nudBrightness;
        public System.Windows.Forms.Label lblBrightnessDown;
        public System.Windows.Forms.Label lblBrightnessUp;
        public System.Windows.Forms.FlowLayoutPanel pnlBrightnessDown;
        public System.Windows.Forms.FlowLayoutPanel pnlBrightnessUp;
        public System.Windows.Forms.CheckBox chkContrast;
        public System.Windows.Forms.NumericUpDown nudContrast;
        public System.Windows.Forms.Label lblContrastDown;
        public System.Windows.Forms.Label lblContrastUp;
        public System.Windows.Forms.FlowLayoutPanel pnlContrastDown;
        public System.Windows.Forms.FlowLayoutPanel pnlContrastUp;
        public System.Windows.Forms.CheckBox chkReset;
        public System.Windows.Forms.FlowLayoutPanel pnlResetDown;
        public System.Windows.Forms.CheckBox chkRefresh;
        public System.Windows.Forms.FlowLayoutPanel pnlRefreshDown;

        public System.Windows.Forms.Button btnGammaLock;
        public System.Windows.Forms.Button btnBrightnessLock;
        public System.Windows.Forms.Button btnContrastLock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            chkGamma = new CheckBox();
            nudGamma = new NumericUpDown();
            lblGammaDown = new Label();
            lblGammaUp = new Label();
            pnlGammaDown = new FlowLayoutPanel();
            pnlGammaUp = new FlowLayoutPanel();
            chkBrightness = new CheckBox();
            nudBrightness = new NumericUpDown();
            lblBrightnessDown = new Label();
            lblBrightnessUp = new Label();
            pnlBrightnessDown = new FlowLayoutPanel();
            pnlBrightnessUp = new FlowLayoutPanel();
            chkContrast = new CheckBox();
            nudContrast = new NumericUpDown();
            lblContrastDown = new Label();
            lblContrastUp = new Label();
            pnlContrastDown = new FlowLayoutPanel();
            pnlContrastUp = new FlowLayoutPanel();
            chkReset = new CheckBox();
            pnlResetDown = new FlowLayoutPanel();
            chkRefresh = new CheckBox();
            pnlRefreshDown = new FlowLayoutPanel();
            btnGammaLock = new Button();
            btnBrightnessLock = new Button();
            btnContrastLock = new Button();
            ((System.ComponentModel.ISupportInitialize)nudGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudContrast).BeginInit();
            SuspendLayout();
            // 
            // chkGamma
            // 
            chkGamma.AutoSize = true;
            chkGamma.ForeColor = Color.White;
            chkGamma.Location = new Point(8, 67);
            chkGamma.Name = "chkGamma";
            chkGamma.Size = new Size(68, 19);
            chkGamma.TabIndex = 0;
            chkGamma.Text = "Gamma";
            chkGamma.UseVisualStyleBackColor = true;
            // 
            // nudGamma
            // 
            nudGamma.DecimalPlaces = 2;
            nudGamma.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudGamma.Location = new Point(8, 87);
            nudGamma.Maximum = new decimal(new int[] { 888, 0, 0, 0 });
            nudGamma.Name = "nudGamma";
            nudGamma.Size = new Size(46, 23);
            nudGamma.TabIndex = 1;
            // 
            // lblGammaDown
            // 
            lblGammaDown.AutoSize = true;
            lblGammaDown.Font = new Font("Segoe UI", 6.4F);
            lblGammaDown.ForeColor = Color.White;
            lblGammaDown.Location = new Point(144, 68);
            lblGammaDown.Name = "lblGammaDown";
            lblGammaDown.Size = new Size(27, 12);
            lblGammaDown.TabIndex = 2;
            lblGammaDown.Text = "down";
            // 
            // lblGammaUp
            // 
            lblGammaUp.AutoSize = true;
            lblGammaUp.Font = new Font("Segoe UI", 6.4F);
            lblGammaUp.ForeColor = Color.White;
            lblGammaUp.Location = new Point(144, 93);
            lblGammaUp.Name = "lblGammaUp";
            lblGammaUp.Size = new Size(15, 12);
            lblGammaUp.TabIndex = 3;
            lblGammaUp.Text = "up";
            // 
            // pnlGammaDown
            // 
            pnlGammaDown.Location = new Point(173, 62);
            pnlGammaDown.Name = "pnlGammaDown";
            pnlGammaDown.Size = new Size(160, 26);
            pnlGammaDown.TabIndex = 4;
            pnlGammaDown.WrapContents = false;
            // 
            // pnlGammaUp
            // 
            pnlGammaUp.Location = new Point(173, 87);
            pnlGammaUp.Name = "pnlGammaUp";
            pnlGammaUp.Size = new Size(160, 26);
            pnlGammaUp.TabIndex = 5;
            pnlGammaUp.WrapContents = false;
            // 
            // chkBrightness
            // 
            chkBrightness.AutoSize = true;
            chkBrightness.ForeColor = Color.White;
            chkBrightness.Location = new Point(8, 118);
            chkBrightness.Name = "chkBrightness";
            chkBrightness.Size = new Size(81, 19);
            chkBrightness.TabIndex = 6;
            chkBrightness.Text = "Brightness";
            chkBrightness.UseVisualStyleBackColor = true;
            // 
            // nudBrightness
            // 
            nudBrightness.DecimalPlaces = 2;
            nudBrightness.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudBrightness.Location = new Point(8, 139);
            nudBrightness.Maximum = new decimal(new int[] { 888, 0, 0, 0 });
            nudBrightness.Name = "nudBrightness";
            nudBrightness.Size = new Size(46, 23);
            nudBrightness.TabIndex = 7;
            // 
            // lblBrightnessDown
            // 
            lblBrightnessDown.AutoSize = true;
            lblBrightnessDown.Font = new Font("Segoe UI", 6.4F);
            lblBrightnessDown.ForeColor = Color.White;
            lblBrightnessDown.Location = new Point(144, 120);
            lblBrightnessDown.Name = "lblBrightnessDown";
            lblBrightnessDown.Size = new Size(27, 12);
            lblBrightnessDown.TabIndex = 8;
            lblBrightnessDown.Text = "down";
            // 
            // lblBrightnessUp
            // 
            lblBrightnessUp.AutoSize = true;
            lblBrightnessUp.Font = new Font("Segoe UI", 6.4F);
            lblBrightnessUp.ForeColor = Color.White;
            lblBrightnessUp.Location = new Point(144, 145);
            lblBrightnessUp.Name = "lblBrightnessUp";
            lblBrightnessUp.Size = new Size(15, 12);
            lblBrightnessUp.TabIndex = 9;
            lblBrightnessUp.Text = "up";
            // 
            // pnlBrightnessDown
            // 
            pnlBrightnessDown.Location = new Point(173, 114);
            pnlBrightnessDown.Name = "pnlBrightnessDown";
            pnlBrightnessDown.Size = new Size(160, 26);
            pnlBrightnessDown.TabIndex = 10;
            pnlBrightnessDown.WrapContents = false;
            // 
            // pnlBrightnessUp
            // 
            pnlBrightnessUp.Location = new Point(173, 139);
            pnlBrightnessUp.Name = "pnlBrightnessUp";
            pnlBrightnessUp.Size = new Size(160, 26);
            pnlBrightnessUp.TabIndex = 11;
            pnlBrightnessUp.WrapContents = false;
            // 
            // chkContrast
            // 
            chkContrast.AutoSize = true;
            chkContrast.ForeColor = Color.White;
            chkContrast.Location = new Point(8, 170);
            chkContrast.Name = "chkContrast";
            chkContrast.Size = new Size(71, 19);
            chkContrast.TabIndex = 12;
            chkContrast.Text = "Contrast";
            chkContrast.UseVisualStyleBackColor = true;
            chkContrast.CheckedChanged += chkContrast_CheckedChanged;
            // 
            // nudContrast
            // 
            nudContrast.DecimalPlaces = 2;
            nudContrast.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudContrast.Location = new Point(8, 191);
            nudContrast.Maximum = new decimal(new int[] { 888, 0, 0, 0 });
            nudContrast.Name = "nudContrast";
            nudContrast.Size = new Size(46, 23);
            nudContrast.TabIndex = 13;
            // 
            // lblContrastDown
            // 
            lblContrastDown.AutoSize = true;
            lblContrastDown.Font = new Font("Segoe UI", 6.4F);
            lblContrastDown.ForeColor = Color.White;
            lblContrastDown.Location = new Point(144, 172);
            lblContrastDown.Name = "lblContrastDown";
            lblContrastDown.Size = new Size(27, 12);
            lblContrastDown.TabIndex = 14;
            lblContrastDown.Text = "down";
            // 
            // lblContrastUp
            // 
            lblContrastUp.AutoSize = true;
            lblContrastUp.Font = new Font("Segoe UI", 6.4F);
            lblContrastUp.ForeColor = Color.White;
            lblContrastUp.Location = new Point(144, 197);
            lblContrastUp.Name = "lblContrastUp";
            lblContrastUp.Size = new Size(15, 12);
            lblContrastUp.TabIndex = 15;
            lblContrastUp.Text = "up";
            // 
            // pnlContrastDown
            // 
            pnlContrastDown.Location = new Point(173, 166);
            pnlContrastDown.Name = "pnlContrastDown";
            pnlContrastDown.Size = new Size(160, 26);
            pnlContrastDown.TabIndex = 16;
            pnlContrastDown.WrapContents = false;
            // 
            // pnlContrastUp
            // 
            pnlContrastUp.Location = new Point(173, 191);
            pnlContrastUp.Name = "pnlContrastUp";
            pnlContrastUp.Size = new Size(160, 26);
            pnlContrastUp.TabIndex = 17;
            pnlContrastUp.WrapContents = false;
            // 
            // chkReset
            // 
            chkReset.AutoSize = true;
            chkReset.ForeColor = Color.White;
            chkReset.Location = new Point(8, 13);
            chkReset.Name = "chkReset";
            chkReset.Size = new Size(159, 19);
            chkReset.TabIndex = 18;
            chkReset.Text = "Reset all values to default";
            chkReset.UseVisualStyleBackColor = true;
            // 
            // pnlResetDown
            // 
            pnlResetDown.Location = new Point(173, 8);
            pnlResetDown.Name = "pnlResetDown";
            pnlResetDown.Size = new Size(160, 26);
            pnlResetDown.TabIndex = 19;
            pnlResetDown.WrapContents = false;
            // 
            // chkRefresh
            // 
            chkRefresh.AutoSize = true;
            chkRefresh.ForeColor = Color.White;
            chkRefresh.Location = new Point(8, 40);
            chkRefresh.Name = "chkRefresh";
            chkRefresh.Size = new Size(150, 19);
            chkRefresh.TabIndex = 20;
            chkRefresh.Text = "Refresh current settings";
            chkRefresh.UseVisualStyleBackColor = true;
            // 
            // pnlRefreshDown
            // 
            pnlRefreshDown.Location = new Point(173, 35);
            pnlRefreshDown.Name = "pnlRefreshDown";
            pnlRefreshDown.Size = new Size(160, 26);
            pnlRefreshDown.TabIndex = 21;
            pnlRefreshDown.WrapContents = false;
            // 
            // btnGammaLock
            // 
            btnGammaLock.BackColor = Color.Black;
            btnGammaLock.ForeColor = Color.White;
            btnGammaLock.Location = new Point(58, 87);
            btnGammaLock.Name = "btnGammaLock";
            btnGammaLock.Size = new Size(24, 23);
            btnGammaLock.TabIndex = 30;
            btnGammaLock.Text = "⚿";
            btnGammaLock.UseVisualStyleBackColor = false;
            // 
            // btnBrightnessLock
            // 
            btnBrightnessLock.BackColor = Color.Black;
            btnBrightnessLock.ForeColor = Color.White;
            btnBrightnessLock.Location = new Point(58, 139);
            btnBrightnessLock.Name = "btnBrightnessLock";
            btnBrightnessLock.Size = new Size(24, 23);
            btnBrightnessLock.TabIndex = 31;
            btnBrightnessLock.Text = "⚿";
            btnBrightnessLock.UseVisualStyleBackColor = false;
            // 
            // btnContrastLock
            // 
            btnContrastLock.BackColor = Color.Black;
            btnContrastLock.ForeColor = Color.White;
            btnContrastLock.Location = new Point(58, 191);
            btnContrastLock.Name = "btnContrastLock";
            btnContrastLock.Size = new Size(24, 23);
            btnContrastLock.TabIndex = 32;
            btnContrastLock.Text = "⚿";
            btnContrastLock.UseVisualStyleBackColor = false;
            // 
            // ViewBinds
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(pnlContrastDown);
            Controls.Add(pnlBrightnessDown);
            Controls.Add(lblBrightnessDown);
            Controls.Add(nudBrightness);
            Controls.Add(chkBrightness);
            Controls.Add(pnlGammaUp);
            Controls.Add(pnlGammaDown);
            Controls.Add(lblGammaUp);
            Controls.Add(lblGammaDown);
            Controls.Add(nudGamma);
            Controls.Add(chkGamma);
            Controls.Add(pnlBrightnessUp);
            Controls.Add(lblBrightnessUp);
            Controls.Add(pnlContrastUp);
            Controls.Add(lblContrastUp);
            Controls.Add(lblContrastDown);
            Controls.Add(nudContrast);
            Controls.Add(chkContrast);
            Controls.Add(btnContrastLock);
            Controls.Add(btnBrightnessLock);
            Controls.Add(btnGammaLock);
            Controls.Add(pnlRefreshDown);
            Controls.Add(chkRefresh);
            Controls.Add(pnlResetDown);
            Controls.Add(chkReset);
            Name = "ViewBinds";
            Size = new Size(382, 227);
            Load += ViewBinds_Load;
            ((System.ComponentModel.ISupportInitialize)nudGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudContrast).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
