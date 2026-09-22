using System;
using System.Windows.Forms;

namespace rebellagamma
{
    public partial class ViewBonus : UserControl
    {
        public event EventHandler? BonusToggleChanged;
        public event EventHandler? VignetteChanged;

        public event EventHandler? RenameProfileClicked;
        public event EventHandler? DeleteProfileClicked;
        public event EventHandler? RevertProfileClicked;
        public event EventHandler? SaveImageClicked;

        private bool _syncing = false;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool EnableCustomPNG
        {
            get => chkBonusAR.Checked;
            set => chkBonusAR.Checked = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int OutlineWidth
        {
            get => tbShadowWidth.Value;
            set { SetWidth(value); }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int OutlineSoftness
        {
            get => tbShadowSoftness.Value;
            set { SetSoftness(value); }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int OutlineOpacity
        {
            get => tbShadowOpacity.Value;
            set { SetOpacity(value); }
        }

        private void SetWidth(int value)
        {
            _syncing = true;
            if (value < 0) value = 0; if (value > tbShadowWidth.Maximum) value = tbShadowWidth.Maximum;
            tbShadowWidth.Value = value;
            numShadowWidth.Value = value;
            _syncing = false;
        }

        private void SetSoftness(int value)
        {
            _syncing = true;
            if (value < 0) value = 0; if (value > tbShadowSoftness.Maximum) value = tbShadowSoftness.Maximum;
            tbShadowSoftness.Value = value;
            numShadowSoftness.Value = value;
            _syncing = false;
        }

        private void SetOpacity(int value)
        {
            _syncing = true;
            if (value < 0) value = 0; if (value > tbShadowOpacity.Maximum) value = tbShadowOpacity.Maximum;
            tbShadowOpacity.Value = value;
            numShadowOpacity.Value = value;
            _syncing = false;
        }

        private void UpdateARControlsState()
        {
            bool isEnabled = chkBonusAR.Checked;
            tbShadowWidth.Enabled = isEnabled;
            numShadowWidth.Enabled = isEnabled;
            tbShadowSoftness.Enabled = isEnabled;
            numShadowSoftness.Enabled = isEnabled;
            tbShadowOpacity.Enabled = isEnabled;
            numShadowOpacity.Enabled = isEnabled;
            
            lblShadowWidth.Enabled = true;
            lblShadowSoftness.Enabled = true;
            lblShadowOpacity.Enabled = true;

            Color lblColor = isEnabled ? Color.White : Color.Gray;
            lblShadowWidth.ForeColor = lblColor;
            lblShadowSoftness.ForeColor = lblColor;
            lblShadowOpacity.ForeColor = lblColor;
        }

        public ViewBonus()
        {
            InitializeComponent();
            chkBonusAR.CheckedChanged += (s, e) => {
                UpdateARControlsState();
                BonusToggleChanged?.Invoke(this, EventArgs.Empty);
            };
            UpdateARControlsState();

            tbShadowWidth.ValueChanged += (s, e) => { if (!_syncing) { numShadowWidth.Value = tbShadowWidth.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };
            numShadowWidth.ValueChanged += (s, e) => { if (!_syncing) { tbShadowWidth.Value = (int)numShadowWidth.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };

            tbShadowSoftness.ValueChanged += (s, e) => { if (!_syncing) { numShadowSoftness.Value = tbShadowSoftness.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };
            numShadowSoftness.ValueChanged += (s, e) => { if (!_syncing) { tbShadowSoftness.Value = (int)numShadowSoftness.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };

            tbShadowOpacity.ValueChanged += (s, e) => { if (!_syncing) { numShadowOpacity.Value = tbShadowOpacity.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };
            numShadowOpacity.ValueChanged += (s, e) => { if (!_syncing) { tbShadowOpacity.Value = (int)numShadowOpacity.Value; VignetteChanged?.Invoke(this, EventArgs.Empty); } };

            btnBonusRename.Click += (s, e) => RenameProfileClicked?.Invoke(this, EventArgs.Empty);
            btnBonusDelete.Click += (s, e) => DeleteProfileClicked?.Invoke(this, EventArgs.Empty);
            btnBonusRevert.Click += (s, e) => RevertProfileClicked?.Invoke(this, EventArgs.Empty);
            btnBonusSaveImage.Click += (s, e) => SaveImageClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnBonusRename_Click(object sender, EventArgs e)
        {

        }
    }
}
