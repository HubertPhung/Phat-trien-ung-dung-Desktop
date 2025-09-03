using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap3
{
    public class PhepTinh
    {
        // s1 lưu họ , s2 lưu tên
        public static void TachChuoi(string hoten,out string s1,out string s2)
        {
            var arr = hoten.Split(' ');
            s2 = arr[arr.Length - 1];
            s1 = string.Join(" ", arr, 0, arr.Length - 1);
        }

        // Kiểm tra số nguyên n1 và n2 có là 2 số nguyên liên tiếp hay không
        public static bool ThuTu(int n1, int n2)
        {
            if(n2 - n1 == 1 || n1 - n2 == 1)
                return true;
            return false;
        }
    }
}
