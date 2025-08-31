using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap3
{
    public partial class frmBai1 : Form
    {
        public frmBai1()
        {
            InitializeComponent();
        }
       
        

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            double heSoLuong = double.Parse(txtHeSoLuong.Text);
            double heSoPhuCap = double.Parse(txtHeSoPhuCap.Text);

            NhanVien nv = new NhanVien(maNV, hoTen, ngaySinh, heSoLuong, heSoPhuCap);
            lblSoTien.Text =  nv.TongLuong().ToString();
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = dtpNgaySinh.Value;
            double heSoLuong = double.Parse(txtHeSoLuong.Text);
            double heSoPhuCap = double.Parse(txtHeSoPhuCap.Text);

            NhanVien nv = new NhanVien(maNV, hoTen, ngaySinh, heSoLuong, heSoPhuCap);
            var frm = new HoaDon(nv);
            frm.ShowDialog();

        }
    }
}
