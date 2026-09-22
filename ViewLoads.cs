using System.Windows.Forms;

namespace rebellagamma
{
    public partial class ViewLoads : UserControl
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public BindRow[] LoadRows { get; private set; }

        public ViewLoads()
        {
            InitializeComponent();
            
            LoadRows = new BindRow[7];
            LoadRows[0] = new BindRow { Name = "load1", CheckBoxActive = chkLoad1, PanelDown = pnlLoad1, ComboProfile = cmbLoad1 };
            LoadRows[1] = new BindRow { Name = "load2", CheckBoxActive = chkLoad2, PanelDown = pnlLoad2, ComboProfile = cmbLoad2 };
            LoadRows[2] = new BindRow { Name = "load3", CheckBoxActive = chkLoad3, PanelDown = pnlLoad3, ComboProfile = cmbLoad3 };
            LoadRows[3] = new BindRow { Name = "load4", CheckBoxActive = chkLoad4, PanelDown = pnlLoad4, ComboProfile = cmbLoad4 };
            LoadRows[4] = new BindRow { Name = "load5", CheckBoxActive = chkLoad5, PanelDown = pnlLoad5, ComboProfile = cmbLoad5 };
            LoadRows[5] = new BindRow { Name = "load6", CheckBoxActive = chkLoad6, PanelDown = pnlLoad6, ComboProfile = cmbLoad6 };
            LoadRows[6] = new BindRow { Name = "load7", CheckBoxActive = chkLoad7, PanelDown = pnlLoad7, ComboProfile = cmbLoad7 };
        }
    }
}