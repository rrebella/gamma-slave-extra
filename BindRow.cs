using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace rebellagamma 
{ 
    public class BindRow 
    { 
        public string Name { get; set; }
        public CheckBox CheckBoxActive { get; set; }
        public NumericUpDown NumericValue { get; set; }
        public FlowLayoutPanel PanelDown { get; set; }
        public FlowLayoutPanel PanelUp { get; set; }
        public ComboBox ComboProfile { get; set; }
        public Label LabelDown { get; set; }
        public Label LabelUp { get; set; }

        public List<string> GetDownBinds() 
        { 
            if (PanelDown == null) return new List<string>(); 
            var parts = PanelDown.Controls.OfType<TextBox>().Select(tb => tb.Text.Trim()).Where(t => t.Length > 0); 
            if (!parts.Any()) return new List<string>(); 
            return new List<string> { string.Join("+", parts) }; 
        } 

        public List<string> GetUpBinds() 
        { 
            if (PanelUp == null) return new List<string>(); 
            var parts = PanelUp.Controls.OfType<TextBox>().Select(tb => tb.Text.Trim()).Where(t => t.Length > 0); 
            if (!parts.Any()) return new List<string>(); 
            return new List<string> { string.Join("+", parts) }; 
        } 
    } 
}