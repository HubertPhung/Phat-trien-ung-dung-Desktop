using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapThietKeForm
{
    public partial class frmChinh : Form
    {
        public frmChinh()
        {
            InitializeComponent();
        }

        private void tsmiBai1_Click(object sender, EventArgs e)
        {
            var frm = new frmBai1();
            frm.ShowDialog();
        }

        private void tsmiBai2_Click(object sender, EventArgs e)
        {
            var frm = new frmBai2();
            frm.ShowDialog();
        }

        private void tsmiBai3_Click(object sender, EventArgs e)
        {
            var frm = new frmBai3();
            frm.ShowDialog();
        }

        private void tsmiBai4_Click(object sender, EventArgs e)
        {
            var frm = new frmBai4();
            frm.ShowDialog();
        }
    }
}
