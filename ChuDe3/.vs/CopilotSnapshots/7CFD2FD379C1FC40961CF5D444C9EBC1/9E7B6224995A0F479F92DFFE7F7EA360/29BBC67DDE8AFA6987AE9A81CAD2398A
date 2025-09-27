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
using System.Xml.Serialization;

namespace BaiTapC
{
    public partial class frmChinh : Form
    {
        private readonly StudentManager _manager = new StudentManager("students.txt", "students.xml", "students.json");
        private readonly ContextMenuStrip _gridMenu = new ContextMenuStrip();
        private readonly ContextMenuStrip _monMenu = new ContextMenuStrip();

        public frmChinh()
        {
            InitializeComponent();
            InitContextMenus();
            LoadDataToGrid();
        }

        private void InitContextMenus()
        {
            var miDeleteSelectedRows = new ToolStripMenuItem("Xóa sinh viên đang chọn (theo dòng)");
            miDeleteSelectedRows.Click += (s, e) => DeleteSelectedRows();
            var miDeleteChecked = new ToolStripMenuItem("Xóa các sinh viên đã tick checkbox");
            miDeleteChecked.Click += (s, e) => DeleteCheckedRows();
            _gridMenu.Items.Add(miDeleteSelectedRows);
            _gridMenu.Items.Add(miDeleteChecked);
            dgvSinhVien.ContextMenuStrip = _gridMenu;
            dgvSinhVien.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hit = dgvSinhVien.HitTest(e.X, e.Y);
                    if (hit.RowIndex >= 0)
                    {
                        dgvSinhVien.ClearSelection();
                        dgvSinhVien.Rows[hit.RowIndex].Selected = true;
                    }
                }
            };

            var miAdd = new ToolStripMenuItem("Thêm môn...");
            miAdd.Click += (s, e) => AddCourse();
            var miRemoveChecked = new ToolStripMenuItem("Xóa các môn đang tick");
            miRemoveChecked.Click += (s, e) =>
            {
                var remove = grpMonHoc.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList();
                foreach (var c in remove) grpMonHoc.Controls.Remove(c);
            };
            _monMenu.Items.Add(miAdd);
            _monMenu.Items.Add(miRemoveChecked);
            grpMonHoc.ContextMenuStrip = _monMenu;
        }

        private string Prompt(string title, string message)
        {
            using (var f = new Form())
            {
                f.Text = title;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.StartPosition = FormStartPosition.CenterParent;
                f.MinimizeBox = false; f.MaximizeBox = false;
                f.ClientSize = new Size(380, 130);

                var lbl = new Label { Left = 12, Top = 12, Width = 356, Text = message };
                var txt = new TextBox { Left = 15, Top = 40, Width = 350 };
                var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 210, Top = 75, Width = 70 };
                var cancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Left = 295, Top = 75, Width = 70 };
                f.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
                f.AcceptButton = ok; f.CancelButton = cancel;
                return f.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
            }
        }

        private void AddCourse()
        {
            string input = Prompt("Thêm môn", "Nhập tên môn học:");
            if (!string.IsNullOrWhiteSpace(input))
            {
                var chk = new CheckBox { Text = input, AutoSize = true };
                int lastY = 10; int colX = 16; 
                foreach (var c in grpMonHoc.Controls.OfType<CheckBox>()) lastY = Math.Max(lastY, c.Bottom);
                chk.Location = new Point(colX, lastY + 6);
                grpMonHoc.Controls.Add(chk);
            }
        }

        private void LoadDataToGrid()
        {
            _manager.Load();
            dgvSinhVien.Rows.Clear();
            foreach (var st in _manager.Students)
            {
                dgvSinhVien.Rows.Add(false, st.MSSV, st.HoLot, st.Ten, st.NgaySinh.ToShortDateString(), st.Lop, st.SoCMND, st.SoDT, st.DiaChi);
            }
        }

        private Student CollectFromInputs()
        {
            var monHoc = grpMonHoc.Controls.OfType<CheckBox>().Where(c => c.Checked).Select(c => c.Text).ToList();
            int namNhap = 0;
            // tạm thời suy luận năm nhập học từ 2 ký tự đầu MSSV nếu có
            var mssv = txtMSSV.Text.Trim();
            if (!string.IsNullOrEmpty(mssv) && mssv.Length >= 2 && mssv.All(char.IsDigit))
            {
                var aa = int.Parse(mssv.Substring(0, 2));
                namNhap = aa >= 90 ? 1900 + aa : 2000 + aa;
            }
            return new Student
            {
                MSSV = mssv,
                HoLot = txtHoLot.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinhNam = rdoNam.Checked,
                Lop = txtLop.Text.Trim(),
                SoCMND = txtCMND.Text.Trim(),
                SoDT = mtxtSoDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                MonHocDangKy = monHoc,
                NamNhapHoc = namNhap
            };
        }

        private void FillInputsFromRow(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow) return;
            txtMSSV.Text = Convert.ToString(row.Cells[1].Value);
            txtHoLot.Text = Convert.ToString(row.Cells[2].Value);
            txtTen.Text = Convert.ToString(row.Cells[3].Value);
            DateTime d; if (DateTime.TryParse(Convert.ToString(row.Cells[4].Value), out d)) dtpNgaySinh.Value = d; else dtpNgaySinh.Value = DateTime.Today;
            txtLop.Text = Convert.ToString(row.Cells[5].Value);
            txtCMND.Text = Convert.ToString(row.Cells[6].Value);
            mtxtSoDT.Text = Convert.ToString(row.Cells[7].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells[8].Value);
        }

        private void DeleteSelectedRows()
        {
            var ids = new List<string>();
            foreach (DataGridViewRow row in dgvSinhVien.SelectedRows)
            {
                if (!row.IsNewRow)
                    ids.Add(Convert.ToString(row.Cells[1].Value));
            }
            if (ids.Count == 0) return;
            if (MessageBox.Show("Xóa các sinh viên đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _manager.RemoveByMssv(ids);
                LoadDataToGrid();
            }
        }

        private void DeleteCheckedRows()
        {
            var ids = new List<string>();
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.IsNewRow) continue;
                bool isChecked = false;
                var val = row.Cells[0].Value;
                if (val is bool) isChecked = (bool)val; else bool.TryParse(Convert.ToString(val), out isChecked);
                if (isChecked) ids.Add(Convert.ToString(row.Cells[1].Value));
            }
            if (ids.Count == 0) return;
            if (MessageBox.Show("Xóa các sinh viên đã tick?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _manager.RemoveByMssv(ids);
                LoadDataToGrid();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtMSSV.Clear();
            txtHoLot.Clear();
            txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            txtLop.Clear();
            txtCMND.Clear();
            mtxtSoDT.Clear();
            txtDiaChi.Clear();
            rdoNam.Checked = true;
            foreach (var chk in grpMonHoc.Controls.OfType<CheckBox>()) chk.Checked = false;
            txtMSSV.Focus();
        }

        private bool ValidateRequired(out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(txtMSSV.Text) || string.IsNullOrWhiteSpace(txtHoLot.Text) || string.IsNullOrWhiteSpace(txtTen.Text)
                || string.IsNullOrWhiteSpace(txtLop.Text) || string.IsNullOrWhiteSpace(txtCMND.Text) || string.IsNullOrWhiteSpace(mtxtSoDT.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                message = "Vui lòng nhập đầy đủ thông tin."; return false;
            }
            return true;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string miss;
            if (!ValidateRequired(out miss))
            {
                MessageBox.Show(miss, "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var st = CollectFromInputs();
            string error;
            if (!_manager.AddOrUpdate(st, out error))
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadDataToGrid();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmSearch())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var result = _manager.Search(dlg.MSSV, dlg.Ten, dlg.Lop);
                    ApplySearchResult(result);
                }
            }
        }

        private void ApplySearchResult(IEnumerable<Student> result)
        {
            var set = new HashSet<string>(result.Select(s => s.MSSV));
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.IsNewRow) continue;
                var id = Convert.ToString(row.Cells[1].Value);
                row.Visible = set.Contains(id);
            }
        }

        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                FillInputsFromRow(dgvSinhVien.SelectedRows[0]);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var ans = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                _manager.SaveAll();
                Close();
            }
        }

    }
}
