using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmPhong : Form
    {
        private bool _dirty = false; // có thay đổi chưa lưu
        private bool _loading = false; // tránh gắn dirty khi đang load

        public frmPhong()
        {
            InitializeComponent();
        }

        private void AnyInputChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            _dirty = true;
        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Db.Open()) { }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối database", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToggleAll(false);
                return;
            }

            _loading = true;
            LoadPhong();
            ResetFormInputs();
            SetButtonsState(onRowSelected: false);
            _loading = false;
        }

        private void FrmPhong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dirty && MessageBox.Show("Bạn có thay đổi chưa lưu, lưu trước khi thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Thử lưu nhanh nếu có phòng chọn (cập nhật), nếu không thì bỏ qua
                if (btnCapNhat.Enabled)
                {
                    BtnCapNhat_Click(btnCapNhat, EventArgs.Empty);
                    // Nếu có lỗi sẽ vẫn đóng vì MessageBox đã báo trước; có thể mở rộng logic tại đây
                }
            }
        }

        private void ToggleAll(bool enabled)
        {
            foreach (Control c in this.Controls) c.Enabled = enabled;
        }

        private void ResetFormInputs()
        {
            cbSoPhong.SelectedIndex = -1;
            cbSoPhong.Text = string.Empty;
            txtSoNguoi.Text = string.Empty;
            txtTenKhachChu.Text = string.Empty;
            ClearErrorStyles();
            _dirty = false;
        }

        private void ClearErrorStyles()
        {
            cbSoPhong.BackColor = SystemColors.Window;
            txtSoNguoi.BackColor = SystemColors.Window;
            txtTenKhachChu.BackColor = SystemColors.Window;
        }

        private void SetButtonsState(bool onRowSelected)
        {
            btnThem.Enabled = true;
            btnCapNhat.Enabled = onRowSelected;
            btnXoa.Enabled = onRowSelected;
        }

        private bool ColumnExists(SqlConnection conn, string table, string column)
        {
            using (var cmd = new SqlCommand(@"SELECT COUNT(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@t AND COLUMN_NAME=@c", conn))
            {
                cmd.Parameters.AddWithValue("@t", table);
                cmd.Parameters.AddWithValue("@c", column);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void LoadPhong(string soPhongFilter = null, string tenKhachFilter = null)
        {
            lvDSPhong.Items.Clear();
            cbSoPhong.Items.Clear();

            using (var conn = Db.Open())
            {
                bool hasSoNguoiCol = ColumnExists(conn, "Phong", "SoNguoi");
                string sql = hasSoNguoiCol
                    ? @"SELECT p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH,
                             ISNULL(p.SoNguoi,0) AS SoNguoi,
                             ISNULL(SUM(ct.SoLuong),0) AS SoDV,
                             ISNULL(SUM(ct.ThanhTien),0) AS ThanhTien
                        FROM dbo.Phong p
                        LEFT JOIN dbo.KhachHang kh ON kh.id = p.MaKH
                        LEFT JOIN dbo.HoaDon hd ON hd.MaPhong = p.id AND hd.status = 0
                        LEFT JOIN dbo.ChiTietHD ct ON ct.MaHD = hd.id
                       GROUP BY p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH, p.SoNguoi
                       ORDER BY p.id;"
                    : @"SELECT p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH,
                             CASE WHEN p.MaKH IS NULL THEN 0 ELSE 1 END AS SoNguoi,
                             ISNULL(SUM(ct.SoLuong),0) AS SoDV,
                             ISNULL(SUM(ct.ThanhTien),0) AS ThanhTien
                        FROM dbo.Phong p
                        LEFT JOIN dbo.KhachHang kh ON kh.id = p.MaKH
                        LEFT JOIN dbo.HoaDon hd ON hd.MaPhong = p.id AND hd.status =0
                        LEFT JOIN dbo.ChiTietHD ct ON ct.MaHD = hd.id
                       GROUP BY p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH
                       ORDER BY p.id;";

                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var id = (int)rd["id"];
                        var tenPhong = rd["TenPhong"].ToString();
                        var soNguoi = Convert.ToInt32(rd["SoNguoi"]);
                        var tenKH = rd["TenKH"] == DBNull.Value ? string.Empty : rd["TenKH"].ToString();
                        var soDV = Convert.ToInt32(rd["SoDV"]);
                        var thanhTien = Convert.ToDecimal(rd["ThanhTien"]);

                        if (!string.IsNullOrWhiteSpace(soPhongFilter) && tenPhong.IndexOf(soPhongFilter, StringComparison.OrdinalIgnoreCase) < 0)
                            continue;
                        if (!string.IsNullOrWhiteSpace(tenKhachFilter) && tenKH.IndexOf(tenKhachFilter, StringComparison.OrdinalIgnoreCase) < 0)
                            continue;

                        var item = new ListViewItem(tenPhong) { Tag = id };
                        item.SubItems.Add(soNguoi.ToString());
                        item.SubItems.Add(tenKH);
                        item.SubItems.Add(soDV.ToString());
                        item.SubItems.Add(string.Format("{0:N0}", thanhTien));
                        item.SubItems.Add("Xem");
                        lvDSPhong.Items.Add(item);

                        cbSoPhong.Items.Add(tenPhong);
                    }
                }
            }

            // autosize columns roughly
            lvDSPhong.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void LvDSPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDSPhong.SelectedItems.Count == 0)
            {
                ResetFormInputs();
                SetButtonsState(false);
                return;
            }

            var it = lvDSPhong.SelectedItems[0];
            cbSoPhong.Text = it.SubItems[0].Text;
            txtSoNguoi.Text = it.SubItems[1].Text;
            txtTenKhachChu.Text = it.SubItems[2].Text;
            SetButtonsState(true);
            _dirty = false; // hiển thị dữ liệu hiện tại
        }

        private void LvDSPhong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hit = lvDSPhong.HitTest(e.Location);
            if (hit.Item == null) return;
            int colIndex = -1;
            if (hit.SubItem != null)
            {
                for (int i = 0; i < hit.Item.SubItems.Count; i++)
                    if (hit.Item.SubItems[i] == hit.SubItem) { colIndex = i; break; }
            }

            if (colIndex == 5) // Chi Tiết column
            {
                int roomId = (int)hit.Item.Tag;
                using (var f = new frmHome())
                {
                    // Try set preselected room if API exists
                    try
                    {
                        var mi = typeof(frmHome).GetMethod("SetRoom", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                        mi?.Invoke(f, new object[] { roomId });
                    }
                    catch { }
                    f.ShowDialog(this);
                }
                // refresh after closing detail
                LoadPhong();
            }
        }

        private bool ValidateInputsForAddOrUpdate(bool isAdd, out int soNguoi, out string soPhong, out string tenKhach)
        {
            ClearErrorStyles();
            soPhong = cbSoPhong.Text?.Trim();
            tenKhach = txtTenKhachChu.Text.Trim();
            soNguoi = 0;
            bool ok = true;

            if (string.IsNullOrWhiteSpace(soPhong))
            {
                cbSoPhong.BackColor = Color.MistyRose;
                MessageBox.Show("Vui lòng nhập Số Phòng.");
                ok = false;
            }

            //if (!int.TryParse(txtSoNguoi.Text.Trim(), out soNguoi) || soNguoi <=0)
            //{
            //    txtSoNguoi.BackColor = Color.MistyRose;
            //    MessageBox.Show("Số Người phải là số >0.");
            //    ok = false;
            //}

            if (!ok) return false;

            if (isAdd)
            {
                // Kiểm tra trùng số phòng
                using (var conn = Db.Open())
                using (var check = new SqlCommand("SELECT COUNT(1) FROM dbo.Phong WHERE TenPhong=@ten", conn))
                {
                    check.Parameters.AddWithValue("@ten", soPhong);
                    if ((int)check.ExecuteScalar() > 0)
                    {
                        cbSoPhong.BackColor = Color.MistyRose;
                        MessageBox.Show("Số phòng đã tồn tại.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputsForAddOrUpdate(true, out var soNguoi, out var soPhong, out var tenKhach)) return;

            try
            {
                using (var conn = Db.Open())
                {
                    int? maKh = null;
                    if (!string.IsNullOrWhiteSpace(tenKhach))
                    {
                        using (var findKh = new SqlCommand("SELECT TOP 1 id FROM dbo.KhachHang WHERE TenKH=@n", conn))
                        {
                            findKh.Parameters.AddWithValue("@n", tenKhach);
                            var obj = findKh.ExecuteScalar();
                            if (obj != null) maKh = Convert.ToInt32(obj);
                        }
                    }

                    bool hasSoNguoiCol = ColumnExists(conn, "Phong", "SoNguoi");
                    string sql = hasSoNguoiCol
                        ? "INSERT INTO dbo.Phong(TenPhong, MaKH, SoNguoi, GhiChu, status) VALUES(@Ten, @MaKH, @SoNguoi, NULL, N'Trống')"
                        : "INSERT INTO dbo.Phong(TenPhong, MaKH, GhiChu, status) VALUES(@Ten, @MaKH, NULL, N'Trống')";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ten", soPhong);
                        cmd.Parameters.AddWithValue("@MaKH", (object)maKh ?? DBNull.Value);
                        if (hasSoNguoiCol) cmd.Parameters.AddWithValue("@SoNguoi", soNguoi);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDbError(ex, "Thêm phòng");
                return;
            }

            LoadPhong();
            ResetFormInputs();
            MessageBox.Show("Thêm phòng thành công!");
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (lvDSPhong.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần cập nhật.");
                return;
            }

            if (!ValidateInputsForAddOrUpdate(false, out var soNguoi, out var soPhong, out var tenKhach)) return;

            if (MessageBox.Show("Bạn có chắc muốn cập nhật thông tin phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = Db.Open())
                {
                    int? maKh = null;
                    //if (!string.IsNullOrWhiteSpace(tenKhach))
                    //{
                    //    using (var findKh = new SqlCommand("SELECT TOP 1 id FROM dbo.KhachHang WHERE TenKH=@n", conn))
                    //    {
                    //        findKh.Parameters.AddWithValue("@n", tenKhach);
                    //        var obj = findKh.ExecuteScalar();
                    //        if (obj != null) maKh = Convert.ToInt32(obj);
                    //    }
                    //}

                    bool hasSoNguoiCol = ColumnExists(conn, "Phong", "SoNguoi");
                    string sql = hasSoNguoiCol
                        ? "UPDATE dbo.Phong SET MaKH=@MaKH, SoNguoi=@SoNguoi WHERE TenPhong=@Ten"
                        : "UPDATE dbo.Phong SET MaKH=@MaKH WHERE TenPhong=@Ten";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ten", soPhong);
                        cmd.Parameters.AddWithValue("@MaKH", (object)maKh ?? DBNull.Value);
                        if (hasSoNguoiCol) cmd.Parameters.AddWithValue("@SoNguoi", soNguoi);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDbError(ex, "Cập nhật phòng");
                return;
            }

            LoadPhong();
            ResetFormInputs();
            SetButtonsState(false);
            MessageBox.Show("Cập nhật thành công!");
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (lvDSPhong.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa.");
                return;
            }

            var soPhong = lvDSPhong.SelectedItems[0].SubItems[0].Text;
            var tenKh = lvDSPhong.SelectedItems[0].SubItems[2].Text;
            var soDV = lvDSPhong.SelectedItems[0].SubItems[3].Text;
            int soDvInt = 0; int.TryParse(soDV, out soDvInt);

            if (!string.IsNullOrWhiteSpace(tenKh))
            {
                MessageBox.Show("Không thể xóa phòng đang có khách!");
                return;
            }
            if (soDvInt > 0)
            {
                MessageBox.Show("Không thể xóa phòng đang có dịch vụ!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này? Dữ liệu không thể khôi phục!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = Db.Open())
                {
                    using (var chk = new SqlCommand(@"SELECT COUNT(1)
                                                        FROM dbo.HoaDon hd
                                                        JOIN dbo.Phong p ON p.id = hd.MaPhong
                                                        WHERE p.TenPhong=@Ten AND hd.status=0", conn))
                    {
                        chk.Parameters.AddWithValue("@Ten", soPhong);
                        if ((int)chk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Không thể xóa phòng đang có hóa đơn mở!");
                            return;
                        }
                    }

                    // Xóa dịch vụ liên quan (hóa đơn đã đóng) nếu nghiệp vụ yêu cầu - thận trọng: ở đây chỉ xóa phòng
                    using (var del = new SqlCommand("DELETE p FROM dbo.Phong p WHERE p.TenPhong=@Ten", conn))
                    {
                        del.Parameters.AddWithValue("@Ten", soPhong);
                        del.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDbError(ex, "Xóa phòng");
                return;
            }

            LoadPhong();
            ResetFormInputs();
            SetButtonsState(false);
            MessageBox.Show("Xóa phòng thành công!");
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            var soPhongFilter = cbSoPhong.Text?.Trim();
            var tenKhFilter = txtTenKhachChu.Text?.Trim();
            LoadPhong(soPhongFilter, tenKhFilter);
        }

        private void ShowDbError(Exception ex, string action)
        {
            // Có thể ghi log, tạm thời hiển thị chung
            MessageBox.Show($"{action} thất bại. Vui lòng thử lại.\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Context menu: Xóa1 phòng
        private void tsmiXoa1Phong_Click(object sender, EventArgs e)
        {
            if (lvDSPhong.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa.");
                return;
            }
            var items = new List<ListViewItem> { lvDSPhong.SelectedItems[0] };
            DeleteRoomsByItems(items);
        }

        // Context menu: Xóa nhiều phòng (dựa trên checkbox)
        private void tsmiXoaNhieuPhong_Click(object sender, EventArgs e)
        {
            var items = new List<ListViewItem>();
            foreach (ListViewItem it in lvDSPhong.Items)
            {
                if (it.Checked) items.Add(it);
            }
            if (items.Count == 0)
            {
                MessageBox.Show("Chọn các phòng (checkbox) để xóa nhiều.");
                return;
            }
            DeleteRoomsByItems(items);
        }

        private void DeleteRoomsByItems(List<ListViewItem> items)
        {
            // Lọc những phòng không đủ điều kiện xóa và thu thập danh sách id hợp lệ
            var deletable = new List<(int id, string tenPhong)>();

            // Mở kết nối1 lần để kiểm tra hóa đơn mở
            using (var conn = Db.Open())
            {
                foreach (var it in items)
                {
                    var tenPhong = it.SubItems[0].Text;
                    var tenKh = it.SubItems.Count > 2 ? it.SubItems[2].Text : string.Empty;
                    var soDvText = it.SubItems.Count > 3 ? it.SubItems[3].Text : "0";
                    int soDv = 0; int.TryParse(soDvText, out soDv);

                    if (!string.IsNullOrWhiteSpace(tenKh))
                    {
                        MessageBox.Show($"Không thể xóa phòng '{tenPhong}' vì đang có khách!");
                        continue;
                    }
                    if (soDv > 0)
                    {
                        MessageBox.Show($"Không thể xóa phòng '{tenPhong}' vì đang có dịch vụ!");
                        continue;
                    }

                    // Kiểm tra hóa đơn mở
                    using (var chk = new SqlCommand(@"SELECT COUNT(1)
                                                        FROM dbo.HoaDon hd
                                                        JOIN dbo.Phong p ON p.id = hd.MaPhong
                                                        WHERE p.TenPhong=@Ten AND hd.status=0", conn))
                    {
                        chk.Parameters.AddWithValue("@Ten", tenPhong);
                        var opened = (int)chk.ExecuteScalar() > 0;
                        if (opened)
                        {
                            MessageBox.Show($"Không thể xóa phòng '{tenPhong}' vì đang có hóa đơn mở!");
                            continue;
                        }
                    }

                    int id = 0; try { id = (int)it.Tag; } catch { id = 0; }
                    if (id == 0)
                    {
                        // fallback: tra cứu id theo tên phòng
                        using (var getId = new SqlCommand("SELECT TOP1 id FROM dbo.Phong WHERE TenPhong=@Ten", conn))
                        {
                            getId.Parameters.AddWithValue("@Ten", tenPhong);
                            var obj = getId.ExecuteScalar();
                            if (obj != null) id = Convert.ToInt32(obj);
                        }
                    }
                    if (id > 0) deletable.Add((id, tenPhong));
                }

                if (deletable.Count == 0)
                {
                    MessageBox.Show("Không có phòng nào đủ điều kiện để xóa.");
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa {deletable.Count} phòng đã chọn? Dữ liệu không thể khôi phục!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                try
                {
                    using (var del = new SqlCommand("DELETE FROM dbo.Phong WHERE id=@Id", conn))
                    {
                        del.Parameters.Add("@Id", SqlDbType.Int);
                        foreach (var r in deletable)
                        {
                            del.Parameters["@Id"].Value = r.id;
                            del.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa phòng thành công!");
                    LoadPhong();
                    ResetFormInputs();
                    SetButtonsState(false);
                }
                catch (Exception ex)
                {
                    ShowDbError(ex, "Xóa phòng");
                }
            }
        }
    }
}
