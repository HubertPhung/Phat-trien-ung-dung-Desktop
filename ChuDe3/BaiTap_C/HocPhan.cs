using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap_C
{
    public class HocPhan
    {
        public string MaHP { get; set; }
        public string TenHP { get; set; }
        public int SoTinChi { get; set; }

        public HocPhan()
        {

        }
        public HocPhan(string maHP, string tenHP, int soTinChi)
        {
            this.MaHP = maHP;
            this.TenHP = tenHP;
            this.SoTinChi = soTinChi;
        }

        public override string ToString()
        {
            return TenHP + "  " + SoTinChi;
        }
    }
}
