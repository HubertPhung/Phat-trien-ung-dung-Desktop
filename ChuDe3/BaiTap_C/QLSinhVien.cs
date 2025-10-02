using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BaiTap_C
{
    public class QLSinhVien
    {
        public List<SinhVien> DSSV { get; private set; }

        public QLSinhVien()
        {
            DSSV = new List<SinhVien>();
        }

        public void ThemSV(SinhVien sv)
        {
            DSSV.Add(sv);
        }

        // a. Thêm hoặc cập nhật sinh viên
        public void Them_CapNhat(SinhVien sv, string filePath = null)
        {
            var existing = DSSV.FirstOrDefault(s => s.MSSV == sv.MSSV);
            if (existing != null)
            {
                // Cập nhật
                existing.HoTenLot = sv.HoTenLot;
                existing.Ten = sv.Ten;
                existing.NgaySinh = sv.NgaySinh;
                existing.GioiTinh = sv.GioiTinh;
                existing.Lop = sv.Lop;
                existing.SoCMND = sv.SoCMND;
                existing.SoDT = sv.SoDT;
                existing.DiaChi = sv.DiaChi;
            }
            else
            {
                ThemSV(sv);
            }

            if (!string.IsNullOrEmpty(filePath))
                SaveToFile(filePath);
        }

        // b. Tìm kiếm
        public List<SinhVien> TimTheoTen(string ten)
        {
            List<SinhVien> ketQua = new List<SinhVien>();
            foreach (SinhVien sv in DSSV)
            {
                if (sv.Ten != null &&
                    sv.Ten.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    ketQua.Add(sv);
                }
            }
            return ketQua;
        }

        public List<SinhVien> TimTheoLop(string lop)
        {
            List<SinhVien> ketQua = new List<SinhVien>();
            foreach (SinhVien sv in DSSV)
            {
                if (sv.Lop != null &&
                    sv.Lop.Equals(lop, StringComparison.OrdinalIgnoreCase))
                {
                    ketQua.Add(sv);
                }
            }
            return ketQua;
        }

        public SinhVien TimTheoMSSV(int mssv)
        {
            foreach (SinhVien sv in DSSV)
            {
                if (sv.MSSV == mssv)
                    return sv;
            }
            return null;
        }


        // c. Xóa
        public void XoaTheoMSSV(int mssv, string filePath = null)
        {
            DSSV.RemoveAll(s => s.MSSV == mssv);
            if (!string.IsNullOrEmpty(filePath))
                SaveToFile(filePath);
        }

        public void XoaTheoLop(string lop, string filePath = null)
        {
            DSSV.RemoveAll(s => s.Lop.Equals(lop, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(filePath))
                SaveToFile(filePath);
        }

        // ==== Các hàm đọc file ====

        public void DocFileTXT(string filename)
        {
            DSSV = File.ReadLines(filename)
                       .Select(line => line.Split('#'))
                       .Where(parts => parts.Length >= 9)
                       .Select(parts => new SinhVien
                       {
                           MSSV = int.Parse(parts[0]),
                           HoTenLot = parts[1],
                           Ten = parts[2],
                           NgaySinh = DateTime.Parse(parts[3]),
                           GioiTinh = bool.Parse(parts[4]),
                           Lop = parts[5],
                           SoCMND = parts[6],
                           SoDT = parts[7],
                           DiaChi = parts[8]
                       })
                       .ToList();
        }

        public void DocFileJSON(string filename)
        {
            string json = File.ReadAllText(filename);
            DSSV = JsonConvert.DeserializeObject<List<SinhVien>>(json) ?? new List<SinhVien>();
        }

        public void DocFileXML(string filename)
        {
            var xdoc = XDocument.Load(filename);

            DSSV = xdoc.Descendants("SinhVien")
                       .Select(sv => new SinhVien
                       {
                           MSSV = (int)sv.Element("MSSV"),
                           HoTenLot = (string)sv.Element("HoTenLot"),
                           Ten = (string)sv.Element("Ten"),
                           NgaySinh = DateTime.Parse((string)sv.Element("NgaySinh")),
                           GioiTinh = bool.Parse((string)sv.Element("GioiTinh")),
                           Lop = (string)sv.Element("Lop"),
                           SoCMND = (string)sv.Element("SoCMND"),
                           SoDT = (string)sv.Element("SoDT"),
                           DiaChi = (string)sv.Element("DiaChi")
                       })
                       .ToList();
        }

        // ==== Lưu file (tùy định dạng) ====
        public void SaveToFile(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();

            if (ext == ".json")
            {
                string json = JsonConvert.SerializeObject(DSSV, Formatting.Indented);
                File.WriteAllText(filename, json);
            }
            else if (ext == ".txt")
            {
                var lines = DSSV.Select(s => $"{s.MSSV}#{s.HoTenLot}#{s.Ten}#{s.NgaySinh:yyyy-MM-dd}#{s.GioiTinh}#{s.Lop}#{s.SoCMND}#{s.SoDT}#{s.DiaChi}");
                File.WriteAllLines(filename, lines);
            }
            else if (ext == ".xml")
            {
                var xdoc = new XDocument(
                    new XElement("DanhSachSinhVien",
                        DSSV.Select(s =>
                            new XElement("SinhVien",
                                new XElement("MSSV", s.MSSV),
                                new XElement("HoTenLot", s.HoTenLot),
                                new XElement("Ten", s.Ten),
                                new XElement("NgaySinh", s.NgaySinh.ToString("yyyy-MM-dd")),
                                new XElement("GioiTinh", s.GioiTinh),
                                new XElement("Lop", s.Lop),
                                new XElement("SoCMND", s.SoCMND),
                                new XElement("SoDT", s.SoDT),
                                new XElement("DiaChi", s.DiaChi)
                            )
                        )
                    )
                );
                xdoc.Save(filename);
            }
            else
            {
                throw new Exception("Định dạng file không được hỗ trợ!");
            }
        }
    }
}
