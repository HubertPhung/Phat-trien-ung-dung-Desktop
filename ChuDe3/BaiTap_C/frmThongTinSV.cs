using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BaiTap_C
{
    public partial class frmThongTinSV : Form
    {
        private QLSinhVien qlsv = new QLSinhVien();
        private QLHocPhan qlhp = new QLHocPhan();
        private string currentFilePath = ""; 
        private string currentHPFilePath = "HocPhan.txt";
        public frmThongTinSV()
        {
            InitializeComponent();
        }
        private void frmThongTinSV_Load(object sender, EventArgs e)
        {
            //ChonFileDuLieu();

            try
            {
                var exeDir = AppDomain.CurrentDomain.BaseDirectory;
                var projectHPPath = Path.GetFullPath(Path.Combine(exeDir, @"..\\..\\HocPhan.txt"));
                var outputHPPath = Path.Combine(exeDir, "HocPhan.txt");

                currentHPFilePath = File.Exists(projectHPPath) ? projectHPPath : outputHPPath;

                qlhp.DocFileTXT(currentHPFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc file Học Phần: " + ex.Message);
            }

            clbMonHoc.Items.Clear();
            foreach (var hp in qlhp.DSHP)
            {
                clbMonHoc.Items.Add(hp);
            }
        }

        private void ChonFileDuLieu()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu sinh viên";
                ofd.Filter = "Text file (*.txt)|*.txt|JSON file (*.json)|*.json|XML file (*.xml)|*.xml|Tất cả (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = ofd.FileName;
                    string ext = System.IO.Path.GetExtension(currentFilePath).ToLower();

                    try
                    {
                        if (ext == ".txt")
                            qlsv.DocFileTXT(currentFilePath);
                        else if (ext == ".json")
                            qlsv.DocFileJSON(currentFilePath);
                        else if (ext == ".xml")
                            qlsv.DocFileXML(currentFilePath);
                        else
                        {
                            MessageBox.Show("Định dạng file không hỗ trợ!");
                            return;
                        }

                        dgvDSSV.Rows.Clear();
                        foreach (var sv in qlsv.DSSV)
                            ThemSV(sv);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể đọc file: " + ex.Message);
                    }
                }
            }
        }
        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            ChonFileDuLieu();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách sinh viên";
                sfd.Filter = "Text file (*.txt)|*.txt|JSON file (*.json)|*.json|XML file (*.xml)|*.xml";
                sfd.FileName = "DanhSachSV";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentFilePath = sfd.FileName;
                        qlsv.SaveToFile(currentFilePath);
                        MessageBox.Show("Xuất file thành công!",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message,
                                        "Lỗi",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ThemSV(SinhVien sv)
        {
            int rowIndex = dgvDSSV.Rows.Add();
            var row = dgvDSSV.Rows[rowIndex];
            row.Cells["colMSSV"].Value = sv.MSSV;
            row.Cells["colHoTenLot"].Value = sv.HoTenLot;
            row.Cells["colTen"].Value = sv.Ten;
            row.Cells["colGioiTinh"].Value = sv.GioiTinh ? "Nam" : "Nữ";
            row.Cells["colNgaySinh"].Value = sv.NgaySinh.ToString("dd/MM/yyyy");
            row.Cells["colLop"].Value = sv.Lop;
            row.Cells["colCMND"].Value = sv.SoCMND;
            row.Cells["colSoDT"].Value = sv.SoDT;
            row.Cells["colDiaChi"].Value = sv.DiaChi;
        }

        private bool ValidateMSSVTheoLop(out int mssv)
        {
            mssv = 0;
            string mssvStr = mtxtMSSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(mssvStr) || mssvStr.Length != 7 || !int.TryParse(mssvStr, out mssv))
            {
                MessageBox.Show("MSSV phải là 7 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtMSSV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbLop.Text))
            {
                MessageBox.Show("Vui lòng chọn/nhập Lớp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLop.Focus();
                return false;
            }

            var tmpSv = new SinhVien();
            int aaNumber = tmpSv.NamNhapHoc(cbLop.Text.Trim());
            if (aaNumber <= 0 || aaNumber > 99)
            {
                MessageBox.Show("Không xác định được năm nhập học từ Lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string aaExpected = aaNumber.ToString("D2");

            if (mssvStr.Substring(2, 2) != "10")
            {
                MessageBox.Show("2 số giữa của MSSV (BB) phải là 10.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtMSSV.Focus();
                return false;
            }

            if (mssvStr.Substring(0, 2) != aaExpected)
            {
                MessageBox.Show($"2 số đầu của MSSV (AA) phải là {aaExpected} theo năm nhập học của lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtMSSV.Focus();
                return false;
            }

            return true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cbLop.Text))
            {
                MessageBox.Show("Vui lòng chọn/nhập Lớp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLop.Focus();
                return;
            }

            if (mtxtCMND.Text.Length != 9)
            {
                MessageBox.Show("CMND phải 9 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtCMND.Focus();
                return;
            }

            if (mtxtSoDT.Text.Length != 12)
            {
                MessageBox.Show("Số điện thoại phải 10 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtSoDT.Focus();
                return;
            }

            if (!ValidateMSSVTheoLop(out int mssv))
                return;

            if (qlsv.DSSV.Any(s => s.MSSV == mssv))
            {
                MessageBox.Show("MSSV đã tồn tại, vui lòng nhập số khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtMSSV.Focus();
                return;
            }

            var sv = new SinhVien(mssv,
                                  txtHoTenLot.Text.Trim(),
                                  txtTen.Text.Trim(),
                                  dtpNgaySinh.Value,
                                  rdNam.Checked,
                                  cbLop.Text.Trim(),
                                  mtxtCMND.Text.Trim(),
                                  mtxtSoDT.Text.Trim(),
                                  txtDiaChi.Text.Trim());

            sv.MonHocDangKy.Clear();
            foreach (var item in clbMonHoc.CheckedItems)
            {
                if (item is HocPhan hp)
                    sv.MonHocDangKy.Add(hp.MaHP);
            }

            qlsv.Them_CapNhat(sv, currentFilePath);
            dgvDSSV.Rows.Clear();
            foreach (var s in qlsv.DSSV) ThemSV(s);

            XoaDuLieuNhap();
        }

        private void dgvDSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSSV.Rows.Count)
            {
                var row = dgvDSSV.Rows[e.RowIndex];

                if (row.Cells["colMSSV"].Value != null)
                    mtxtMSSV.Text = row.Cells["colMSSV"].Value.ToString();

                if (row.Cells["colHoTenLot"].Value != null)
                    txtHoTenLot.Text = row.Cells["colHoTenLot"].Value.ToString();

                if (row.Cells["colTen"].Value != null)
                    txtTen.Text = row.Cells["colTen"].Value.ToString();

                if (row.Cells["colNgaySinh"].Value != null && DateTime.TryParse(row.Cells["colNgaySinh"].Value.ToString(), out DateTime ns))
                    dtpNgaySinh.Value = ns;

                if (row.Cells["colLop"].Value != null)
                    cbLop.Text = row.Cells["colLop"].Value.ToString();

                if (row.Cells["colCMND"].Value != null)
                    mtxtCMND.Text = row.Cells["colCMND"].Value.ToString();

                if (row.Cells["colSoDT"].Value != null)
                    mtxtSoDT.Text = row.Cells["colSoDT"].Value.ToString();

                if (row.Cells["colDiaChi"].Value != null)
                    txtDiaChi.Text = row.Cells["colDiaChi"].Value.ToString();

                string gt = row.Cells["colGioiTinh"].Value?.ToString();
                if (gt == "Nam") rdNam.Checked = true;
                else if (gt == "Nữ") rdNu.Checked = true;

                CapNhatHocPhan();
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void XoaDuLieuNhap()
        {
            mtxtMSSV.Clear();
            txtHoTenLot.Clear();
            txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            rdNam.Checked = true;
            cbLop.SelectedIndex = -1;
            mtxtCMND.Clear();
            mtxtSoDT.Clear();
            txtDiaChi.Clear();
            if (clbMonHoc.Items.Count > 0)
            {
                for (int i = 0; i < clbMonHoc.Items.Count; i++)
                    clbMonHoc.SetItemChecked(i, false);
            }
            mtxtMSSV.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<SinhVien> ketQua = new List<SinhVien>();

            if (!string.IsNullOrWhiteSpace(mtxtMSSV.Text))
            {
                int mssv;
                if (int.TryParse(mtxtMSSV.Text.Trim(), out mssv))
                {
                    ketQua = qlsv.TimTheoMSSV(mssv);
                }
                else
                {
                    MessageBox.Show("MSSV phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtTen.Text))
            {
                ketQua = qlsv.TimTheoTen(txtTen.Text.Trim());
            }
            else if (!string.IsNullOrWhiteSpace(cbLop.Text))
            {
                ketQua = qlsv.TimTheoLop(cbLop.Text.Trim());
            }
            else
            {
                ketQua = qlsv.DSSV.ToList();
            }

            dgvDSSV.Rows.Clear();
            foreach (var sv in ketQua)
            {
                ThemSV(sv);
            }
        }

        #region Cập nhật
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateMSSVTheoLop(out int mssv)) return;

            int originalMssv = -1;
            if (dgvDSSV.SelectedRows.Count > 0)
            {
                int.TryParse(dgvDSSV.SelectedRows[0].Cells["colMSSV"].Value?.ToString(), out originalMssv);
            }

            if (originalMssv != mssv && qlsv.DSSV.Any(s => s.MSSV == mssv))
            {
                MessageBox.Show("MSSV đã tồn tại, không thể cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sv = new SinhVien(mssv,
                                   txtHoTenLot.Text.Trim(),
                                   txtTen.Text.Trim(),
                                   dtpNgaySinh.Value,
                                   rdNam.Checked,
                                   cbLop.Text.Trim(),
                                   mtxtCMND.Text.Trim(),
                                   mtxtSoDT.Text.Trim(),
                                   txtDiaChi.Text.Trim());

            sv.MonHocDangKy.Clear();
            foreach (var item in clbMonHoc.CheckedItems)
            {
                if (item is HocPhan hp)
                    sv.MonHocDangKy.Add(hp.MaHP);
            }

            qlsv.Them_CapNhat(sv, currentFilePath);

            dgvDSSV.Rows.Clear();
            foreach (var s in qlsv.DSSV) ThemSV(s);

            XoaDuLieuNhap();
        }

        private void CapNhatHocPhan()
        {
            if (dgvDSSV.SelectedRows.Count == 0) return;

            var row = dgvDSSV.SelectedRows[0];
            string mssv = row.Cells["colMSSV"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(mssv)) return;

            var sv = qlsv.DSSV.FirstOrDefault(s => s.MSSV.ToString() == mssv);
            if (sv == null) return;

            for (int i = 0; i < clbMonHoc.Items.Count; i++)
            {
                clbMonHoc.SetItemChecked(
                    i,
                    clbMonHoc.Items[i] is HocPhan hp && sv.MonHocDangKy?.Contains(hp.MaHP) == true
                );
            }
        }
        #endregion

        #region Xóa môn, xóa sinh viên
        private void clbMonHoc_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (dgvDSSV.SelectedRows.Count == 0) return;

            var row = dgvDSSV.SelectedRows[0];
            string mssv = row.Cells["colMSSV"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(mssv)) return;

            var sv = qlsv.DSSV.FirstOrDefault(s => s.MSSV.ToString() == mssv);
            if (sv == null) return;

            var hp = clbMonHoc.Items[e.Index] as HocPhan;
            if (hp == null) return;

            if (e.NewValue == CheckState.Checked)
            {
                if (!sv.MonHocDangKy.Contains(hp.MaHP))
                    sv.MonHocDangKy.Add(hp.MaHP);
            }
            else
            {
                sv.MonHocDangKy.Remove(hp.MaHP);
            }

            if (!string.IsNullOrEmpty(currentFilePath))
                qlsv.SaveToFile(currentFilePath);
        }

        private void tsmiXoa1Mon_Click(object sender, EventArgs e)
        {
            if (clbMonHoc.SelectedItem is HocPhan hp)
            {
                qlhp.XoaHP(hp.MaHP);
                clbMonHoc.Items.Remove(hp);

                if (!string.IsNullOrEmpty(currentHPFilePath))
                {
                    qlhp.SaveToFile(currentHPFilePath);
                }
            }
        }

        private void tmstXoaNhieuMon_Click(object sender, EventArgs e)
        {
            List<string> maHPs = new List<string>();

            foreach (var item in clbMonHoc.CheckedItems)
            {
                if (item is HocPhan hp)
                {
                    maHPs.Add(hp.MaHP);
                }
            }

            if (maHPs.Count > 0)
            {
                qlhp.XoaNhieuHP(maHPs);

                var itemsToRemove = clbMonHoc.CheckedItems.Cast<object>().ToList();
                foreach (var i in itemsToRemove)
                {
                    clbMonHoc.Items.Remove(i);
                }

                if (!string.IsNullOrEmpty(currentHPFilePath))
                {
                    qlhp.SaveToFile(currentHPFilePath);
                }
            }
        }

        private void tmsiXoa1SV_Click(object sender, EventArgs e)
        {
            if (dgvDSSV.SelectedRows.Count == 0) return;

            var row = dgvDSSV.SelectedRows[0];
            if (int.TryParse(row.Cells["colMSSV"].Value?.ToString(), out int mssv))
            {
                qlsv.XoaSV(mssv, currentFilePath);
                dgvDSSV.Rows.Remove(row);
                MessageBox.Show($"Đã xóa sinh viên {mssv}", "Thông báo");
            }
        }

        private void tmsiXoaNhieuSV_Click(object sender, EventArgs e)
        {
            dgvDSSV.EndEdit();

            var mssvList = dgvDSSV.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Where(r => r.Cells["colChon"].Value is bool b && b)
                .Select(r =>
                {
                    int id;
                    return int.TryParse(Convert.ToString(r.Cells["colMSSV"].Value), out id) ? (int?)id : null;
                })
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            if (mssvList.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn sinh viên nào để xóa!", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa {mssvList.Count} sinh viên đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            qlsv.XoaNhieuSV(mssvList, currentFilePath);

            dgvDSSV.Rows.Clear();
            foreach (var sv in qlsv.DSSV)
                ThemSV(sv);

            MessageBox.Show("Đã xóa các sinh viên được chọn!", "Thông báo");
        }
        #endregion 
    }
}
