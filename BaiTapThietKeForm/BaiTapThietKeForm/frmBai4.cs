using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapThietKeForm
{
    public partial class frmBai4 : Form
    {
        public frmBai4()
        {
            InitializeComponent();
        }

        private void frmBai4_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            int soNgauNhien;

            for (int i = 1;i <= 10; i++)
            {
                soNgauNhien = rand.Next(1, 100);
                listBox1.Items.Add(soNgauNhien);
            }
        }

        private void btnTimSo_Click(object sender, EventArgs e)
        {
            int soCanTim = int.Parse(textSo.Text);
            foreach(int item in listBox1.Items)
            {
                if(item == soCanTim)
                {
                    lblKetQua.Text = "Tìm thấy số ";
                    return;
                }   
            }
            lblKetQua.Text = "Không tìm thấy số ";
        }
    }
}
