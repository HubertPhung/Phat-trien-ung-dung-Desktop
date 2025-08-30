using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapWindowsForm
{
    public partial class frmBai3 : Form
    {
        public frmBai3()
        {
            InitializeComponent();
        }

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            var TinhToan = new TinhToan();
            var a = int.Parse(txtSoA.Text);
            var b = int.Parse(txtSoB.Text);
            var n = int.Parse(txtSoN.Text);
            int tong = 0;
            if (rdTongAB.Checked == true)
            {
                TinhToan.CongHaiSo(a, b,ref tong);
            }
            else
            {
                tong = TinhToan.TongDaySo(n);
            }

            lblKetQua.Text = tong.ToString();
        }
    }
}
