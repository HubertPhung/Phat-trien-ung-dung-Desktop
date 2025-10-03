    using Newtonsoft.Json;
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

            // Thêm sinh viên mới
            public void ThemSV(SinhVien sv)
            {
                DSSV.Add(sv);
            }

            // Thêm hoặc cập nhật sinh viên
            public void Them_CapNhat(SinhVien sv, string filePath = null)
            {
                var existing = DSSV.FirstOrDefault(s => s.MSSV == sv.MSSV);
                if (existing != null)
                {
                    existing.HoTenLot = sv.HoTenLot;
                    existing.Ten = sv.Ten;
                    existing.NgaySinh = sv.NgaySinh;
                    existing.GioiTinh = sv.GioiTinh;
                    existing.Lop = sv.Lop;
                    existing.SoCMND = sv.SoCMND;
                    existing.SoDT = sv.SoDT;
                    existing.DiaChi = sv.DiaChi;
                    existing.MonHocDangKy = new List<string>(sv.MonHocDangKy ?? new List<string>());
                }
                else
                {
                    ThemSV(sv);
                }

                if (!string.IsNullOrEmpty(filePath))
                    SaveToFile(filePath);
            }

            // Tìm kiếm
            public List<SinhVien> TimTheoMSSV(int mssv)
            {
                return DSSV.Where(s => s.MSSV == mssv).ToList();
            }

            public List<SinhVien> TimTheoTen(string ten)
            {
                return DSSV.Where(s => s.Ten.ToLower().Contains(ten.ToLower())).ToList();
            }

            public List<SinhVien> TimTheoLop(string lop)
            {
                return DSSV.Where(s => s.Lop.ToLower().Contains(lop.ToLower())).ToList();
            }

            // Xóa một hay nhiều sinh viên
            public void XoaSV(int mssv, string filePath = null)
            {
                var sv = DSSV.FirstOrDefault(s => s.MSSV == mssv);
                if (sv != null)
                    DSSV.Remove(sv);

                if (!string.IsNullOrEmpty(filePath))
                    SaveToFile(filePath);
            }
            public void XoaNhieuSV(List<int> mssvList, string filePath = null)
            {
                DSSV.RemoveAll(s => mssvList.Contains(s.MSSV));

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
                                       DiaChi = parts[8],
                                       MonHocDangKy = parts.Length > 9
                                                      ? parts[9].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                                                      : new List<string>()
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
                               DiaChi = (string)sv.Element("DiaChi"),
                               MonHocDangKy = ((string)sv.Element("MonHocDangKy"))?
                                              .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              .ToList() ?? new List<string>()
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
                    var lines = DSSV.Select(s =>
                    {
                        string monHoc = s.MonHocDangKy != null ? string.Join(",", s.MonHocDangKy) : "";
                        return $"{s.MSSV}#{s.HoTenLot}#{s.Ten}#{s.NgaySinh:yyyy-MM-dd}#{s.GioiTinh}#{s.Lop}#{s.SoCMND}#{s.SoDT}#{s.DiaChi}#{monHoc}";
                    });
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
                                    new XElement("DiaChi", s.DiaChi),
                                    new XElement("MonHocDangKy", s.MonHocDangKy != null ? string.Join(",", s.MonHocDangKy) : "")
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
