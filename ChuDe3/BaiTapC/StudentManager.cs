using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BaiTapC
{
    public class StudentManager
    {
        public List<Student> Students { get; } = new List<Student>();

        public string TextFilePath { get; }
        public string XmlFilePath { get; }
        public string JsonFilePath { get; }

        private static string MapDataPath(string relative)
        {
            // Map to project root while debugging, fallback to app base directory
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory; // bin/Debug
                var projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", ".."));
                if (Directory.Exists(projectRoot))
                {
                    return Path.Combine(projectRoot, relative);
                }
            }
            catch { }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relative);
        }

        public StudentManager(string textPath, string xmlPath, string jsonPath)
        {
            TextFilePath = MapDataPath(textPath);
            XmlFilePath = MapDataPath(xmlPath);
            JsonFilePath = MapDataPath(jsonPath);
        }

        public void Load()
        {
            Students.Clear();
            if (File.Exists(TextFilePath))
            {
                foreach (var line in File.ReadAllLines(TextFilePath, Encoding.UTF8))
                {
                    if (Student.TryParse(line, out var st)) Students.Add(st);
                }
                return;
            }
            if (File.Exists(XmlFilePath))
            {
                using (var fs = File.OpenRead(XmlFilePath))
                {
                    var xs = new XmlSerializer(typeof(List<Student>));
                    var list = (List<Student>)xs.Deserialize(fs);
                    Students.AddRange(list ?? new List<Student>());
                }
                return;
            }
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath, Encoding.UTF8);
                var list = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
                Students.AddRange(list);
            }
        }

        public void SaveAll()
        {
            SaveText();
            SaveXml();
            SaveJson();
        }

        public void SaveText()
        {
            var lines = Students.Select(s => s.ToTextLine()).ToArray();
            File.WriteAllLines(TextFilePath, lines, Encoding.UTF8);
        }

        public void SaveXml()
        {
            using (var fs = File.Create(XmlFilePath))
            {
                var xs = new XmlSerializer(typeof(List<Student>));
                xs.Serialize(fs, Students);
            }
        }

        public void SaveJson()
        {
            var json = JsonConvert.SerializeObject(Students, Formatting.Indented);
            File.WriteAllText(JsonFilePath, json, Encoding.UTF8);
        }

        public bool AddOrUpdate(Student st, out string error)
        {
            error = null;
            if (st == null) { error = "Dữ liệu không hợp lệ"; return false; }
            if (!Student.IsValidMssv(st.MSSV)) { error = "MSSV phải là 7 chữ số"; return false; }
            if (!ValidateMssvAABBCCC(st.MSSV, st.NamNhapHoc, out error)) return false;
            if (!Student.IsValidCmnd(st.SoCMND)) { error = "CMND phải có 9 chữ số"; return false; }
            if (!Student.IsValidSoDt(st.SoDT)) { error = "Số ĐT phải có 10 chữ số"; return false; }
            if (string.IsNullOrWhiteSpace(st.Ten)) { error = "Vui lòng nhập Tên"; return false; }

            var existed = Students.FirstOrDefault(x => string.Equals(x.MSSV, st.MSSV, StringComparison.OrdinalIgnoreCase));
            if (existed == null)
            {
                if (Students.Any(x => string.Equals(x.MSSV, st.MSSV, StringComparison.OrdinalIgnoreCase)))
                {
                    error = "MSSV đã tồn tại"; return false;
                }
                Students.Add(st);
            }
            else
            {
                existed.HoLot = st.HoLot;
                existed.Ten = st.Ten;
                existed.NgaySinh = st.NgaySinh;
                existed.GioiTinhNam = st.GioiTinhNam;
                existed.Lop = st.Lop;
                existed.SoCMND = st.SoCMND;
                existed.SoDT = st.SoDT;
                existed.DiaChi = st.DiaChi;
                existed.MonHocDangKy = st.MonHocDangKy ?? new List<string>();
                existed.NamNhapHoc = st.NamNhapHoc;
            }
            SaveAll();
            return true;
        }

        public int RemoveByMssv(IEnumerable<string> mssvList)
        {
            var set = new HashSet<string>(mssvList.Where(s => !string.IsNullOrWhiteSpace(s)), StringComparer.OrdinalIgnoreCase);
            var removed = Students.RemoveAll(s => set.Contains(s.MSSV));
            if (removed > 0) SaveAll();
            return removed;
        }

        public IEnumerable<Student> Search(string mssv = null, string ten = null, string lop = null)
        {
            IEnumerable<Student> q = Students;
            if (!string.IsNullOrWhiteSpace(mssv))
                q = q.Where(s => s.MSSV.IndexOf(mssv, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(ten))
                q = q.Where(s => s.Ten.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0 || s.HoLot.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0 || s.HoTenDayDu.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrWhiteSpace(lop))
                q = q.Where(s => s.Lop.IndexOf(lop, StringComparison.OrdinalIgnoreCase) >= 0);
            return q.ToList();
        }

        public static bool ValidateMssvAABBCCC(string mssv, int namNhapHoc, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(mssv) || mssv.Length != 7 || !mssv.All(char.IsDigit))
            { error = "MSSV phải gồm 7 chữ số"; return false; }

            if (mssv.Substring(2, 2) != "10")
            { error = "MSSV không đúng định dạng AABBCCC (BB phải = 10)"; return false; }

            // Xác định AA từ năm nhập học (YYYY)
            if (namNhapHoc <= 0) { error = "Chưa có năm nhập học"; return false; }
            var aa = (namNhapHoc % 100).ToString("00");

            if (mssv.Substring(0, 2) != aa)
            { error = $"Hai chữ số đầu MSSV phải là {aa} (năm nhập học {namNhapHoc})"; return false; }

            return true;
        }
    }
}
