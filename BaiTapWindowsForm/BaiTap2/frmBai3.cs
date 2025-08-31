using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap2
{
    public partial class frmBai3 : Form
    {
        public frmBai3()
        {
            InitializeComponent();
        }

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            var ho = txtHo.Text;
            var ten = txtTen.Text;
            string s = "";
            var n = string.IsNullOrEmpty(txtN.Text) ? 0 : int.Parse(txtN.Text);
            if(rdNoiChuoi.Checked)
            {
                TinhToan.NoiChuoi(ho, ten, ref s);
                lblSoKetQua.Text = s;
            }
            else if(rdTinhGiaiThua.Checked)
            {
                var kq = TinhToan.GiaiThua(n);
                lblSoKetQua.Text = kq.ToString();
            }

        }
    }
}
