using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BaiTapC
{
    public class Student
    {
        public string MSSV { get; set; }
        public string HoLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinhNam { get; set; } 
        public string Lop { get; set; }
        public string SoCMND { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public List<string> MonHocDangKy { get; set; } = new List<string>();
        public int NamNhapHoc { get; set; } // YYYY

        public string HoTenDayDu => string.Concat(HoLot ?? string.Empty, (string.IsNullOrWhiteSpace(HoLot) ? string.Empty : " "), Ten ?? string.Empty).Trim();

        public static bool IsValidMssv(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.All(char.IsDigit) && value.Length == 7;
        }

        public static bool IsValidCmnd(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.All(char.IsDigit) && (value.Length == 9);
        }

        public static bool IsValidSoDt(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var digits = new string(value.Where(char.IsDigit).ToArray());
            return digits.Length == 10;
        }

        public string ToTextLine()
        {
            var monhoc = string.Join(";", MonHocDangKy ?? new List<string>());
            return string.Join("|", new[]
            {
                MSSV ?? string.Empty,
                HoLot ?? string.Empty,
                Ten ?? string.Empty,
                NgaySinh.ToString("yyyy-MM-dd"),
                GioiTinhNam ? "1" : "0",
                Lop ?? string.Empty,
                SoCMND ?? string.Empty,
                SoDT ?? string.Empty,
                DiaChi ?? string.Empty,
                monhoc,
                NamNhapHoc > 0 ? NamNhapHoc.ToString(CultureInfo.InvariantCulture) : string.Empty
            });
        }

        public static bool TryParse(string line, out Student student)
        {
            student = null;
            if (string.IsNullOrWhiteSpace(line)) return false;
            var parts = line.Split('|');
            if (parts.Length < 10) return false;
            DateTime d;
            if (!DateTime.TryParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                if (!DateTime.TryParse(parts[3], out d)) return false;
            }
            int namNhap = 0;
            if (parts.Length >= 11)
            {
                int.TryParse(parts[10], NumberStyles.Integer, CultureInfo.InvariantCulture, out namNhap);
            }
            if (namNhap == 0 && !string.IsNullOrEmpty(parts[0]) && parts[0].Length >= 2 && parts[0].All(char.IsDigit))
            {
                var aa = int.Parse(parts[0].Substring(0, 2));
                namNhap = aa >= 90 ? 1900 + aa : 2000 + aa; // suy lu?n
            }
            student = new Student
            {
                MSSV = parts[0],
                HoLot = parts[1],
                Ten = parts[2],
                NgaySinh = d,
                GioiTinhNam = parts[4] == "1" || parts[4].Equals("Nam", StringComparison.OrdinalIgnoreCase),
                Lop = parts[5],
                SoCMND = parts[6],
                SoDT = parts[7],
                DiaChi = parts[8],
                MonHocDangKy = (parts[9] ?? string.Empty).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                NamNhapHoc = namNhap
            };
            return true;
        }
    }
}
