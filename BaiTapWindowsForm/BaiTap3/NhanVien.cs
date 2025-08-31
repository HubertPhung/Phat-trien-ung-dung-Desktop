using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap3
{
    public class NhanVien
    {
        //- Các thuộc tính: MaNV, HoTen, NgaySinh, HeSoLuong, HeSoPhuCap.

        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public double HeSoLuong { get; set; }
        public double HeSoPhuCap { get; set; }

        public NhanVien()
        {
        }

        public NhanVien(string maNV, string hoTen, DateTime ngaySinh, double heSoLuong, double heSoPhuCap) { 
            this.MaNV = maNV;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.HeSoLuong = heSoLuong;
            this.HeSoPhuCap = heSoPhuCap;
        }

        public int TongLuong()
        {
            int tong  = 0;
            return tong = (int)((HeSoLuong + HeSoPhuCap) * 1150000);
        }

        public string HienThiThongTin()
        {
            return $"Mã NV: {MaNV}, Họ tên: {HoTen}, Ngày sinh: {NgaySinh.ToShortDateString()}, " +
                   $"Hệ số lương: {HeSoLuong}, Hệ số phụ cấp: {HeSoPhuCap}, Tổng lương: {TongLuong():N0} VND";
        }

    }
}
