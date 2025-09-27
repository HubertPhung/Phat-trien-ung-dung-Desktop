using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DocFileJSON
{
    public partial class frmDocFileJSON : Form
    {
        public frmDocFileJSON()
        {
            InitializeComponent();
        }

        private static List<StudentInfo> LoadJSON(string path)
        {
            // Khai báo danh sách lưu trữ
            List<StudentInfo> List = new List<StudentInfo>();

            // Đọc toàn bộ nội dung file JSON
            string json = File.ReadAllText(path);

            // Chuyển JSON thành JObject
            var array = (JObject)JsonConvert.DeserializeObject(json);

            // Lấy đối tượng "sinhvien"
            var students = array["sinhvien"].Children();

            // Duyệt mảng sinh viên
            foreach (var item in students)
            {
                string mssv = item["MSSV"].Value<string>();
                string hoten = item["Hoten"].Value<string>();
                int tuoi = item["Tuoi"].Value<int>();
                double diem = item["Diem"].Value<double>();
                bool tongiao = item["TonGiao"].Value<bool>();

                // Tạo đối tượng StudentInfo
                StudentInfo info = new StudentInfo(mssv, hoten, tuoi, diem, tongiao);

                // Thêm vào danh sách
                List.Add(info);
            }

            return List;
        }


        private void btnDoc_Click(object sender, EventArgs e)
        {
            String Str = "";
            String Path = "../../students.json";
            List<StudentInfo> List = LoadJSON(Path);
            for(int i = 0; i < List.Count; i++)
            {
                StudentInfo info = List[i];
                Str += string.Format("Sinh vien {0} co MSSV: {1}, ho ten: {2}," + " diem TB: {3}\r\n", (i + 1), info.MSSV, info.Hoten, info.Diem);
            }
            MessageBox.Show(Str);

        }

    }
}
