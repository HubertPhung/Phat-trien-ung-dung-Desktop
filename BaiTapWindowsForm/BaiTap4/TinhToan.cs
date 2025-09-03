using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap4
{
    public class TinhToan
    {
        public static void ChaoHoi(string hoten,string gioitinh)
        {
            if(gioitinh.ToLower().Contains("nam"))
            {
                MessageBox.Show($"Chào ông {hoten}!");
            }
            else if (gioitinh.ToLower().Contains("nữ"))
            {
                MessageBox.Show($"Chào bà {hoten}!");
            }
        }

        public static int USCLN(int m, int n)
        {
            if (n == 0)
                return m;
            else 
                return USCLN(n, m % n);
        }
    }
}
