using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapWindowsForm
{
    public class HangHoa
    {
        public string MaHang { get; set; }

        public string TenHang { get; set; }

        public string DVT { get; set; }

        public int SoLuong { get; set; }

        public int DonGia { get; set; }

        //Hàm khởi tạo không có tham số.
        public HangHoa()
        {
            
        }


        //Phương thức: HienThi (hiển thị tất cả thông tin của hàng hóa)
        public string HienThi()
        {
            return string.Format("Mã hàng: {0}, Tên hàng: {1}, Đơn vị tính: {2}, Số lượng: {3}, Đơn giá: {4}", MaHang, TenHang, DVT, SoLuong, DonGia);
        }


    }
}
