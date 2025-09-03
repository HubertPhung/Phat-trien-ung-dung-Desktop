using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap4
{
    public class SanPham
    {
        public string MaSanPham { get; set; }

        public string TenSanPham { get; set; }

        public string LoaiSanPham { get; set; }

        public DateTime NgaySanXuat { get; set; }

        public SanPham()
        {
            
        }

        //NamHetHan: lấy năm hết hạn sử dụng, biết rằng mỗi sản phẩm có hạn sử
        //dụng 03 năm.

        public int NamHetHan()
        {
            return NgaySanXuat.Year + 3;
        }

        //HienThi: hiển thị tất cả thông tin của sản phẩm
        public string HienThi()
        {
            return $"Mã sản phẩm: {MaSanPham}\nTên sản phẩm: {TenSanPham}\nLoại sản phẩm: {LoaiSanPham}\nNgày sản xuất: {NgaySanXuat:dd/MM/yyyy}\nNăm hết hạn: {NamHetHan()}";
        }

        
    }
}
