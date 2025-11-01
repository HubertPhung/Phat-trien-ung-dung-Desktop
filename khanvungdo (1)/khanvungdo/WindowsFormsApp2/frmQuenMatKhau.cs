using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmQuenMatKhau : Form
    {



        private frmDangNhap parentForm;

        public frmQuenMatKhau(frmDangNhap parent)
        {
            InitializeComponent();
            this.parentForm = parent; // Lưu lại để sử dụng sau
        }

        private void lbDangNhap_Click(object sender, EventArgs e)
        {
            parentForm.Show();    
            this.Close();    
        }
    }
}
