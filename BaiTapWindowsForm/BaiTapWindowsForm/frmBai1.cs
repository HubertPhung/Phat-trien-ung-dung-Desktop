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
    public partial class frmBai1 : Form
    {
        public frmBai1()
        {
            InitializeComponent();
        }

        private void frmBai1_Load(object sender, EventArgs e)
        {
            HangHoa hh = new HangHoa();
            hh.MaHang = "MH01";
            hh.TenHang = "Bút bi";
            hh.DVT = "Cây";
            hh.SoLuong = 100;
            hh.DonGia = 5000;

            // Hiển thị thông tin hàng hóa ra lblThongBao
            lblThongBao.Text = hh.HienThi();
        }
    }
}
