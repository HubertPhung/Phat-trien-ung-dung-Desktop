using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class DichVuItem : UserControl
    {
        
        public DichVuItem()
        {
            InitializeComponent();
        }
        public event EventHandler onSelect = null;
        public int id { get; set; }
        public int gia { get; set; }
        public string name { get; set; }    

        public string TenDV
        {
            get { return lbName.Text; }
            set { lbName.Text = value; }
        }

        public Image AnhDV
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value;  }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }
    }
}
