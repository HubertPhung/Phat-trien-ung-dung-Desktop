using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap2
{
    public class ThietBi
    {
        public string MaThietBi { get; set; }

        public string TenThietBi { get; set; }

        public string NuocSanXuat { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        public ThietBi()
        {
            
        }

        public ThietBi(string maThietBi, string tenThietBi, string nuocSanXuat, int donGia, int soLuong)
        {
            MaThietBi = maThietBi;
            TenThietBi = tenThietBi;
            NuocSanXuat = nuocSanXuat;
            DonGia = donGia;
            SoLuong = soLuong;
        }

        public int ThanhTien()
        {
            return DonGia * SoLuong;
        }

        public override string ToString()
        {
            return $"Mã thiết bị: {MaThietBi}, Tên thiết bị: {TenThietBi}, Nước sản xuất: {NuocSanXuat}, Đơn giá: {DonGia}, Số lượng: {SoLuong}, Thành tiền: {ThanhTien()}";
        }
    }
}
