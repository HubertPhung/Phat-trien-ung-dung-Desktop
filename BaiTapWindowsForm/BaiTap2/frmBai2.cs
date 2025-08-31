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
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

      

        private void btnKetQua_Click(object sender, EventArgs e)
        {
            float soThuNhat = string.IsNullOrEmpty(txtSoThuNhat.Text) ? 0 : float.Parse(txtSoThuNhat.Text);
            float soThuHai = string.IsNullOrEmpty(txtSoThuHai.Text) ? 0 : float.Parse(txtSoThuHai.Text);

            float ketQua = 0;

            if (rdCong.Checked)
            {
                ketQua = soThuNhat + soThuHai;
            }
            else if (rdTru.Checked)
            {
                ketQua = soThuNhat - soThuHai;
            }
            else if (rdNhan.Checked)
            {
                ketQua = soThuNhat * soThuHai;
            }
            else if (rdChia.Checked)
            {
                if (soThuHai == 0)
                {
                    MessageBox.Show("Không thể chia cho 0", "Thông báo");
                    return;
                }
                ketQua = soThuNhat / soThuHai;
            }
            lblSoKetQua.Text = ketQua.ToString();
        }

       
    }
}
