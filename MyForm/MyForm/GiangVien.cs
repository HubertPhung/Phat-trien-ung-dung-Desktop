using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForm
{
    public class GiangVien
    {
        public string MaSo {  get; set; }

        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public DanhSachHocPhan dsHocPhan;
        public string GioiTinh;
        public string[] NgoaiNgu;
        public string SoDT;
        public string Mail;

        public GiangVien()
        {
            dsHocPhan = new DanhSachHocPhan();
            NgoaiNgu = new string[20];
        }

        public GiangVien(string maso, string sdt, string mail, string hoten, DateTime ngaysinh, DanhSachHocPhan ds, string gt, string[] nn)
        {
            this.MaSo = maso;
            this.SoDT = sdt;
            this.Mail = mail;
            this.HoTen = hoten;
            this.NgoaiNgu = nn;
            this.dsHocPhan = ds;
            this.GioiTinh = gt;
            this.NgaySinh = ngaysinh;
        }

        public override string ToString()
        {
            string s = "Mã số: " + MaSo + "\n"
                + "Họ tên: " + HoTen + "\n"
                + "Ngày sinh: " + NgaySinh + "\n"
                + "Giới tính: " + GioiTinh + "\n"
                + "Số ĐT: " + SoDT + "\n"
                + "Mail: " + Mail + "\n";
            string sngoaingu = "Ngoại ngữ";
            foreach (string t in NgoaiNgu)
            {
                sngoaingu += t + ";";
            }
            string monDay = "Danh sách môn dạy: ";
            foreach (HocPhan hp in dsHocPhan.ds)
            {
                monDay += hp + ";";
            }
            s += "\n" + sngoaingu + "\n" + monDay;
            return s;
        }
    }
}
