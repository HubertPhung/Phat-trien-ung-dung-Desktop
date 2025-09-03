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
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

        private void btnXepLoai_Click(object sender, EventArgs e)
        {
            var diemLT = float.Parse(txtNhapDiemLT.Text);
            var diemTH = float.Parse(txtNhapDiemTH.Text);
            var diemTB = (diemLT + diemTH) / 2;

            if (diemLT < 0 || diemLT > 10 || diemTH < 0 || diemTH > 10)
            {
                MessageBox.Show("Điểm không hợp lệ. Vui lòng nhập lại!");
                return;
            }
            
            if(diemLT < 5 || diemTH < 5)
                lblKetQua.Text = "Yếu";
            else if (diemTB < 7 )
                lblKetQua.Text = "Trung bình";
            else if (diemTB < 8)
                lblKetQua.Text = "Khá";
            else if (diemTB < 9)
                lblKetQua.Text = "Giỏi";
            else 
                lblKetQua.Text = "Xuất sắc";
        }
    }
}
