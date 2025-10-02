using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BaiTap_C
{
    public partial class frmThongTinSV : Form
    {
        private QLSinhVien qlsv = new QLSinhVien();
        private string currentFilePath = ""; // nhớ file đã chọn

        public frmThongTinSV()
        {
            InitializeComponent();
        }

        private void frmThongTinSV_Load(object sender, EventArgs e)
        {
            ChonFileDuLieu();
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

                        // Load lên DataGridView
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

        private void ThemSV(SinhVien sv)
        {
            int rowIndex = dgvDSSV.Rows.Add();
            var row = dgvDSSV.Rows[rowIndex];
            row.Cells["colMSSV"].Value = sv.MSSV;
            row.Cells["colHoTenLot"].Value = sv.HoTenLot;
            row.Cells["colTen"].Value = sv.Ten;
            row.Cells["colNgaySinh"].Value = sv.NgaySinh.ToString("dd/MM/yyyy");
            row.Cells["colLop"].Value = sv.Lop;
            row.Cells["colCMND"].Value = sv.SoCMND;
            row.Cells["colSoDT"].Value = sv.SoDT;
            row.Cells["colDiaChi"].Value = sv.DiaChi;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(mtxtMSSV.Text.Trim(), out int mssv) || mtxtMSSV.Text.Length != 7)
            {
                MessageBox.Show("MSSV phải là 7 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtMSSV.Focus();
                return;
            }

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

            if (mtxtSoDT.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải 10 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtSoDT.Focus();
                return;
            }

            foreach (var s in qlsv.DSSV)
            {
                if (s.MSSV == mssv)
                {
                    MessageBox.Show("MSSV đã tồn tại.", "Trùng MSSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

            qlsv.ThemSV(sv);
            ThemSV(sv);
            qlsv.SaveToFile(currentFilePath); // lưu lại
            XoaDuLieuNhap();
        }

        private void dgvDSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSSV.Rows.Count)
            {
                var row = dgvDSSV.Rows[e.RowIndex];
                mtxtMSSV.Text = row.Cells["colMSSV"].Value?.ToString();
                txtHoTenLot.Text = row.Cells["colHoTenLot"].Value?.ToString();
                txtTen.Text = row.Cells["colTen"].Value?.ToString();
                dtpNgaySinh.Value = DateTime.Parse(row.Cells["colNgaySinh"].Value.ToString());
                cbLop.Text = row.Cells["colLop"].Value?.ToString();
                mtxtCMND.Text = row.Cells["colCMND"].Value?.ToString();
                mtxtSoDT.Text = row.Cells["colSoDT"].Value?.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value?.ToString();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(mtxtMSSV.Text, out int mssv)) return;

            var sv = qlsv.DSSV.FirstOrDefault(s => s.MSSV == mssv);
            if (sv != null)
            {
                sv.HoTenLot = txtHoTenLot.Text.Trim();
                sv.Ten = txtTen.Text.Trim();
                sv.NgaySinh = dtpNgaySinh.Value;
                sv.GioiTinh = rdNam.Checked;
                sv.Lop = cbLop.Text.Trim();
                sv.SoCMND = mtxtCMND.Text.Trim();
                sv.SoDT = mtxtSoDT.Text.Trim();
                sv.DiaChi = txtDiaChi.Text.Trim();
            }

            dgvDSSV.Rows.Clear();
            foreach (var s in qlsv.DSSV) ThemSV(s);

            qlsv.SaveToFile(currentFilePath);
        }

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSSV.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow row in dgvDSSV.SelectedRows)
            {
                if (row.Cells["colMSSV"].Value != null)
                {
                    int mssv = int.Parse(row.Cells["colMSSV"].Value.ToString());
                    qlsv.DSSV.RemoveAll(s => s.MSSV == mssv);
                    dgvDSSV.Rows.Remove(row);
                }
            }
            qlsv.SaveToFile(currentFilePath); 
        }

        //private void btnTimKiem_Click(object sender, EventArgs e)
        //{
        //    string keyword = txtTimKiem.Text.Trim().ToLower();

        //    var results = qlsv.DSSV.Where(s =>
        //        s.MSSV.ToString().Contains(keyword) ||
        //        s.Ten.ToLower().Contains(keyword) ||
        //        s.Lop.ToLower().Contains(keyword)
        //    ).ToList();

        //    dgvDSSV.Rows.Clear();
        //    foreach (var s in results) ThemSV(s);
        //}

        private void btnThoat_Click(object sender, EventArgs e)
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
    }
}
