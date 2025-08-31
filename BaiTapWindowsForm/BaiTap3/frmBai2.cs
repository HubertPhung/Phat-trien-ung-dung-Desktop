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
    public partial class frmBai2 : Form
    {
        public frmBai2()
        {
            InitializeComponent();
        }

        private void btnXemKetQua_Click(object sender, EventArgs e)
        {
            var n = int.Parse(txtNhapN.Text);
            int kq = 0;

            if (rdTinhTong1DenN.Checked)
            {
                for(int i = 1; i <= n; i++)
                    kq += i;
            }
            else if (txtTinhNGiaiThua.Checked)
            {
                kq = 1;
                for(int i = 1; i <= n; i++)
                    kq *= i;
            }
            lblKetQua.Text = kq.ToString();
        }
    }
}
