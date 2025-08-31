using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap2
{
    public class TinhToan
    {
        public static void NoiChuoi(string ho, string ten, ref string s)
        {
            s = ho + " " + ten;
        }

        public static long GiaiThua(int n)
        {
            if (n < 0) return -1; // Khong hop le
            if (n == 0) return 1; // 0! = 1
            long gt = 1;
            for (int i = 1; i <= n; i++)
            {
                gt *= i;
            }
            return gt;
        }
    }
}
