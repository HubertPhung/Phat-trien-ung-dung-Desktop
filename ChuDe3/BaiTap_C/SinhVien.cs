using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap_C
{
    public  class SinhVien
    {
        public int MSSV { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Lop { get; set; }
        public string SoCMND { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public List<string> MonHocDangKy { get; set; } = new List<string>();
        public SinhVien()
        {
            
        }

        public SinhVien(int mssv, string hoTenLot, string ten, DateTime ngaySinh, bool gioiTinh, string lop, string soCMND, string soDT, string diaChi)
        {
            this.MSSV = mssv;
            this.HoTenLot = hoTenLot;
            this.Ten = ten;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.Lop = lop;
            this.SoCMND = soCMND;
            this.SoDT = soDT;
            this.DiaChi = diaChi;
        }

        public string HoTenDayDu(string hoTenLot, string ten)
        {
            return $"{hoTenLot} {ten}".Trim();
        }

        public int NamNhapHoc(string lop)
        {
            var soLop = new string(lop.Where(char.IsDigit).ToArray());

            if (int.TryParse(soLop, out int so))
            {
                int namNhapHoc = so - 24;
                return namNhapHoc;
            }
            return 0;
        }

        public string TaoMSSV(string lop, int stt)
        {
            int nam = NamNhapHoc(lop);       
            string aa = nam.ToString("D2");  
            string bb = "10";                
            string ccc = stt.ToString("D3"); 

            return aa + bb + ccc; 
        }
    }
}
