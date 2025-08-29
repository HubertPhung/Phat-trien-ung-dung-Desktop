using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForm
{
    public class HocPhan
    {
        public int ID { get; set; }
        public string TenHP { get; set; }
        public int SoTC { get; set; }
        public HocPhan (string ten)
        {
            this.TenHP = ten;
        }

        public HocPhan(int iD, string tenHP, int soTC)
        {
            ID = iD;
            TenHP = tenHP;
            SoTC = soTC;
        }

        public override string ToString()
        {
            return TenHP;
        }

    }
}
