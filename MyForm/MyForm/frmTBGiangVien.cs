using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyForm
{
    public partial class frmTBGiangVien : Form
    {
        public frmTBGiangVien()
        {
            InitializeComponent();
        }

        private void frmTBGiangVien_Load(object sender, EventArgs e)
        {

        }

        public void SetText(string s)
        {
            this.lbThongBao.Text = s;
        }
    }
}
