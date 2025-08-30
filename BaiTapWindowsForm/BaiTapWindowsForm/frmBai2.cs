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
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void cbbTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stt = cbbTenHang.SelectedIndex;
            switch(stt)
            {
                case 0:
                    txtDonGia.Text = "100000";
                    break;
                case 1:
                    txtDonGia.Text = "2000000";
                    break;
                case 2:
                    txtDonGia.Text = "150000";
                    break;
                default:
                    txtDonGia.Text = "0";
                    break;
            }
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            int tinhTien = int.Parse(txtSoLuong.Text) * int.Parse(txtDonGia.Text);
            if (rdChuyenKhoan.Checked == true)
            {
                tinhTien -= (int)(tinhTien * 0.05);
            }
            lblSoTien.Text = tinhTien.ToString();
        }
    }
}
