using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap4
{
    public partial class frmBai3 : Form
    {
        public frmBai3()
        {
            InitializeComponent();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            var hoTen = txtHoTen.Text;
            var gioiTinh = txtGioiTinh.Text;

            TinhToan.ChaoHoi(hoTen, gioiTinh);
        }

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            var m = int.Parse(txtSoM.Text);
            var n = int.Parse(txtSoN.Text);

            lblKetQua.Text =  TinhToan.USCLN(m, n).ToString();
        }
    }
}
