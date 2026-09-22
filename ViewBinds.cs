using System;
using System.Windows.Forms;

namespace rebellagamma
{
    public partial class ViewBinds : UserControl
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow GammaRow { get; private set; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow BrightnessRow { get; private set; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow ContrastRow { get; private set; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow ResetRow { get; private set; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow RefreshRow { get; private set; }

        public ViewBinds()
        {
            InitializeComponent();
            GammaRow = new BindRow { Name = "gamma", CheckBoxActive = chkGamma, NumericValue = nudGamma, PanelDown = pnlGammaDown, PanelUp = pnlGammaUp, LabelDown = lblGammaDown, LabelUp = lblGammaUp };
            BrightnessRow = new BindRow { Name = "brightness", CheckBoxActive = chkBrightness, NumericValue = nudBrightness, PanelDown = pnlBrightnessDown, PanelUp = pnlBrightnessUp, LabelDown = lblBrightnessDown, LabelUp = lblBrightnessUp };
            ContrastRow = new BindRow { Name = "contrast", CheckBoxActive = chkContrast, NumericValue = nudContrast, PanelDown = pnlContrastDown, PanelUp = pnlContrastUp, LabelDown = lblContrastDown, LabelUp = lblContrastUp };
            ResetRow = new BindRow { Name = "reset", CheckBoxActive = chkReset, PanelDown = pnlResetDown };
            RefreshRow = new BindRow { Name = "refresh", CheckBoxActive = chkRefresh, PanelDown = pnlRefreshDown };
        }

        private void ViewBinds_Load(object sender, EventArgs e)
        {

        }

        private void chkContrast_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}