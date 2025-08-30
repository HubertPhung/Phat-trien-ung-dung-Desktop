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
    public partial class frmBai1 : Form
    {
        public frmBai1()
        {
            InitializeComponent();
        }

        private void HienThiThongTin(int index)
        {
            switch (index)
            {
                case 0:
                    cbbMaThietBi.SelectedIndex = 0;
                    cbbTenThietBi.SelectedIndex = 0;
                    txtNuocSX.Text = "Mỹ";
                    txtDonGia.Text = "1500000";
                    break;
                case 1:
                    cbbMaThietBi.SelectedIndex = 1;
                    cbbTenThietBi.SelectedIndex = 1;
                    txtNuocSX.Text = "Mỹ";
                    txtDonGia.Text = "1200000";
                    break;
                case 2:
                    cbbMaThietBi.SelectedIndex = 2;
                    cbbTenThietBi.SelectedIndex = 2;
                    txtNuocSX.Text = "Nhật Bản";
                    txtDonGia.Text = "2000000";
                    break;
                case 3:
                    cbbMaThietBi.SelectedIndex = 3;
                    cbbTenThietBi.SelectedIndex = 3;
                    txtNuocSX.Text = "Mỹ";
                    txtDonGia.Text = "800000";
                    break;
                case 4:
                    cbbMaThietBi.SelectedIndex = 4;
                    cbbTenThietBi.SelectedIndex = 4;
                    txtNuocSX.Text = "Trung Quốc";
                    txtDonGia.Text = "700000";
                    break;
                case 5:
                    cbbMaThietBi.SelectedIndex = 5;
                    cbbTenThietBi.SelectedIndex = 5;
                    txtNuocSX.Text = "Đài Loan";
                    txtDonGia.Text = "300000";
                    break;
            }
        }

        private void cbbMaThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThiThongTin(cbbMaThietBi.SelectedIndex);
        }

        private void cbbTenThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThiThongTin(cbbTenThietBi.SelectedIndex);
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            string ma = cbbMaThietBi.Text;
            string ten = cbbTenThietBi.Text;
            string nuoc = txtNuocSX.Text;
            int donGia = int.Parse(txtDonGia.Text);
            int soLuong = int.Parse(txtSoLuong.Text);
            int tongTien = 0;

            ThietBi tb = new ThietBi(ma, ten, nuoc, donGia, soLuong);
            tongTien = tb.ThanhTien();
            lblSoTien.Text = tongTien.ToString();
        }
    }
}
