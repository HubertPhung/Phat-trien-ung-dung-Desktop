using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmHome : Form
    {
        private int _currentLoaiId = 0; // 0: none selected yet
        private int _currentBillId = 0;
        private int? _currentCustomerId = null;
        private int _editingOldQty = 0;
        private string _currentRoomStatus = ""; // Trống / Đang sử dụng
        private bool _addingService = false; // chặn double-add khi bấm nhanh hoặc event trùng
        private bool _syncingNud = false; // tránh loop khi cập nhật số lượng
        private bool _syncingGiamGia = false; // tránh ghi DB khi đang đồng bộ UI


        public frmHome()
        {
            InitializeComponent();
            // ĐÃ có wiring Load trong Designer, tránh gắn thêm tại đây để không bị chạy2 lần
            // this.Load += FrmHome_Load; // remove to avoid double Load

            // Events and UI configs are now wired in Designer to avoid duplication
            EnsureDynamicColumns();

            // Keep only double buffering via helper
            TryEnableDoubleBuffering(lvPhong);
            TryEnableDoubleBuffering(dgvDSDV);
            TryEnableDoubleBuffering(dgvChiTiet);

            // Trạng thái UI ban đầu theo yêu cầu
            ResetDichVuList();
            ResetThongTinPhongVaKhach();
            ResetChiTietGrid();
            ToggleActions(false);
        }

        // Helper: kiểm tra cột có tồn tại không
        private bool ColumnExists(SqlConnection conn, string table, string column)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@t AND COLUMN_NAME=@c", conn))
            {
                cmd.Parameters.AddWithValue("@t", table);
                cmd.Parameters.AddWithValue("@c", column);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Cập nhật textbox Số người theo DB (đồng bộ frmPhong)
        private void UpdateSoNguoiDisplay(int roomId)
        {
            try
            {
                using (var conn = Db.Open())
                {
                    int soNguoi = 0;
                    if (ColumnExists(conn, "Phong", "SoNguoi"))
                    {
                        using (var cmd = new SqlCommand("SELECT ISNULL(SoNguoi,0) FROM dbo.Phong WHERE id=@p", conn))
                        {
                            cmd.Parameters.AddWithValue("@p", roomId);
                            var obj = cmd.ExecuteScalar();
                            if (obj != null) soNguoi = Convert.ToInt32(obj);
                        }
                    }
                    else
                    {
                        using (var cmd = new SqlCommand("SELECT CASE WHEN MaKH IS NULL THEN 0 ELSE 1 END FROM dbo.Phong WHERE id=@p", conn))
                        {
                            cmd.Parameters.AddWithValue("@p", roomId);
                            var obj = cmd.ExecuteScalar();
                            if (obj != null) soNguoi = Convert.ToInt32(obj);
                        }
                    }
                    txtSoNguoi.Text = soNguoi.ToString();
                }
            }
            catch { txtSoNguoi.Text = "0"; }
        }

        // Cho phép form khác chọn trước phòng
        public void SetRoom(int roomId)
        {
            if (roomId <= 0) return;
            // nếu đã load danh sách phòng thì chọn ngay; nếu chưa sẽ chọn sau khi load
            SelectRoomInList(roomId);
        }

        private void EnsureDynamicColumns()
        {
            // Thêm cột IdDVẩn cho dgvDSDV nếu chưa có để lưu mã dịch vụ
            if (dgvDSDV.Columns["IdDV"] == null)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "IdDV",
                    HeaderText = "IdDV",
                    Visible = false
                };
                dgvDSDV.Columns.Add(col);
            }
        }


        private void FrmHome_Load(object sender, EventArgs e)
        {
            // 1) Kết nối CSDL
            try
            {
                using (var conn = Db.Open()) { }
            }
            catch
            {
                MessageBox.Show("Mất kết nối database. Vui lòng kiểm tra kết nối.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToggleAll(false);
                return;
            }

            // 2) Hiển thị danh sách phòng bằng USP_XemPhong
            try
            {
                LoadDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách phòng: " + ex.Message);
            }

            // 3) Bỏ chọn loại dịch vụ mặc định
            try
            {
                rdAnUong.Checked = false;
                rdDiLai.Checked = false;
                rdGiatLa.Checked = false;
                rdSpa.Checked = false;
                rdGiaiTri.Checked = false;
            }
            catch { }

            // 4) default số người
            txtSoNguoi.Text = "0";
        }


        private void TryEnableDoubleBuffering(object control)
        {
            try
            {
                var pi = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi?.SetValue(control, true, null);
            }
            catch { }
        }

        private void ResetDichVuList()
        {
            dgvDSDV.Rows.Clear();
        }

        private void ResetThongTinPhongVaKhach()
        {
            txtKhachHang.Clear();
            txtCMND.Clear();
            txtSoDT.Clear();
            txtSoPhong.Text = string.Empty;
            txtNgayLap.Text = string.Empty;
            txtGhiChu.Clear();
            txtTongDV.Clear();
            txtTongTien.Clear();
            txtThanhTien.Clear();
            txtSoNguoi.Text = "0";
            _currentBillId = 0;
            _currentCustomerId = null;
            _currentRoomStatus = string.Empty;
        }

        private void ResetChiTietGrid()
        {
            dgvChiTiet.Rows.Clear();
        }

        private void ToggleActions(bool enabled)
        {
            btnThemDV.Enabled = enabled;
            btnThanhToan.Enabled = enabled;
            btnLuu.Enabled = enabled;
            btnHuyDon.Enabled = enabled;
        }

        private void ToggleAll(bool enabled)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = enabled;
            }
        }

        private void LoadDanhSachPhong()
        {
            lvPhong.Items.Clear();
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_XemPhong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var id = Convert.ToInt32(rd["id"]);
                        var ten = rd["TenPhong"].ToString();
                        var status = rd["status"].ToString();
                        if (string.IsNullOrWhiteSpace(status)) status = "Trống"; // mặc định Trống
                        int imageIndex = string.Equals(status, "Trống", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                        var item = new ListViewItem(ten, imageIndex)
                        {
                            Tag = new { Id = id, Ten = ten, Status = status }
                        };
                        lvPhong.Items.Add(item);
                    }
                }
            }
        }

        private void SelectRoomInList(int roomId)
        {
            foreach (ListViewItem it in lvPhong.Items)
            {
                var tag = it.Tag;
                if (tag == null) continue;
                try
                {
                    var t = tag.GetType();
                    int id = (int)t.GetProperty("Id").GetValue(tag, null);
                    if (id == roomId)
                    {
                        it.Selected = true; it.Focused = true; it.EnsureVisible();
                        break;
                    }
                }
                catch { }
            }
        }

        private void LvPhong_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected) return;
            var tag = e.Item.Tag;
            int roomId = 0; string tenPhong = e.Item.Text; string status = "Trống";
            if (tag != null)
            {
                var t = tag.GetType();
                roomId = (int)t.GetProperty("Id").GetValue(tag, null);
                tenPhong = (string)t.GetProperty("Ten").GetValue(tag, null);
                status = (string)t.GetProperty("Status").GetValue(tag, null);
            }
            _currentRoomStatus = status;

            txtSoPhong.Text = roomId.ToString();
            if (string.Equals(status, "Trống", StringComparison.OrdinalIgnoreCase))
            {
                ResetThongTinPhongVaKhach();
                txtSoPhong.Text = roomId.ToString();
                ResetChiTietGrid();
                ToggleActions(false);
                // cập nhật số người theo DB (trống =>0)
                UpdateSoNguoiDisplay(roomId);
                MessageBox.Show("Phòng hiện đang trống.");
                return;
            }

            // Đang sử dụng -> tải thông tin khách + hóa đơn mở (nếu có)
            try
            {
                LoadKhachHangTheoPhong(roomId);
                LoadHoaDonMoTheoPhong(roomId);
                UpdateSoNguoiDisplay(roomId); // đồng bộ số người
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin phòng: " + ex.Message);
            }
        }

        private void LoadKhachHangTheoPhong(int roomId)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"SELECT TOP(1) kh.id, kh.TenKH, kh.CMND_CCCD, kh.SDT
                                            FROM dbo.Phong p LEFT JOIN dbo.KhachHang kh ON kh.id = p.MaKH
                                            WHERE p.id=@p", conn))
            {
                cmd.Parameters.AddWithValue("@p", roomId);
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read() && rd["TenKH"] != DBNull.Value)
                    {
                        _currentCustomerId = Convert.ToInt32(rd["id"]);
                        txtKhachHang.Text = rd["TenKH"].ToString();
                        txtCMND.Text = rd["CMND_CCCD"].ToString();
                        txtSoDT.Text = rd["SDT"].ToString();
                    }
                    else
                    {
                        _currentCustomerId = null;
                        txtKhachHang.Clear(); txtCMND.Clear(); txtSoDT.Clear();
                    }
                }
            }
        }

        private void LoadHoaDonMoTheoPhong(int roomId)
        {
            _currentBillId = 0;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_XuatHoaDonTheoPhong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@phongID", roomId);
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        _currentBillId = Convert.ToInt32(rd["id"]);
                        var ngayLap = Convert.ToDateTime(rd["NgayLapHD"]);
                        txtNgayLap.Text = ngayLap.ToString("dd/MM/yyyy HH:mm");
                    }
                }
            }

            if (_currentBillId <= 0)
            {
                ResetChiTietGrid();
                ToggleActions(false);
                MessageBox.Show("Không có hóa đơn mở cho phòng này.");
                return;
            }

            // Đồng bộ giảm giá từ DB về UI trước khi tính toán
            LoadCurrentBillDiscount();
            RefreshBillUI();
            ToggleActions(true);
        }

        private void LoadDichVuTheoLoai(int loaiId)
        {
            _currentLoaiId = loaiId;
            dgvDSDV.Rows.Clear();
            int count = 0;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_XuatDichVuTheoLoai", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", loaiId);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int idDV = 0;
                        try { idDV = Convert.ToInt32(rd["id"]); } catch { /* nếu SP không trả id */ }
                        var ten = rd["TenDV"].ToString();
                        var dvt = rd["DVT"].ToString();
                        var gia = rd["DonGia"]; // giữ object
                        // Thứ tự cột hiện tại: Hinh, Ten, DVT, Gia, IdDV (ấn)
                        dgvDSDV.Rows.Add(null, ten, dvt, gia, idDV);
                        count++;
                    }
                }
            }
            if (count == 0)
            {
                MessageBox.Show("Chưa có dịch vụ trong nhóm này");
            }

            // Reset chọn/số lượng khi đổi loại
            if (dgvDSDV.CurrentRow != null) dgvDSDV.ClearSelection();
            nudSoLuong.Value = 0;
        }

        private string GetLoaiTenById(int id)
        {
            switch (id)
            {
                case 1: return "Ăn uống";
                case 2: return "Đi lại";
                case 3: return "Giặt là";
                case 4: return "Spa";
                case 5: return "Giải trí";
                default: return string.Empty;
            }
        }

        private void DgvDSDV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kiểm tra điều kiện trước khi mở chi tiết
            int roomId;
            if (!int.TryParse(txtSoPhong.Text, out roomId))
            {
                MessageBox.Show("Vui lòng chọn phòng trước.");
                return;
            }
            if (string.Equals(_currentRoomStatus, "Trống", StringComparison.OrdinalIgnoreCase))
            {
                // Cố gắng mở hóa đơn sẽ tự set trạng thái phòng
                if (!EnsureBill())
                {
                    MessageBox.Show("Phòng đang trống, không thể thêm dịch vụ.");
                    return;
                }
            }
            if (_currentBillId == 0 && !EnsureBill())
            {
                MessageBox.Show("Phòng chưa có hóa đơn mở.");
                return;
            }

            var row = dgvDSDV.Rows[e.RowIndex];
            var ten = Convert.ToString(row.Cells["Ten"].Value);
            var dvt = Convert.ToString(row.Cells["DVT"].Value);
            decimal gia; decimal.TryParse(Convert.ToString(row.Cells["Gia"].Value), out gia);
            int serviceId = 0;
            if (row.Cells["IdDV"] != null)
                int.TryParse(Convert.ToString(row.Cells["IdDV"].Value), out serviceId);
            if (serviceId == 0)
                serviceId = GetServiceIdByUnique(ten, dvt, gia, _currentLoaiId == 0 ? 1 : _currentLoaiId);

            // Lấy tên loại chính xác cho dịch vụ (nếu có thể)
            var loaiTen = GetLoaiTenByServiceId(serviceId) ?? GetLoaiTenById(_currentLoaiId);

            using (var f = new frmChiTietDV(serviceId, ten, loaiTen, gia, dvt, null, roomId, _currentBillId, null, () => RefreshBillUI()))
            {
                f.ShowDialog(this);
            }
        }

        private bool EnsureRoomExists(int roomId)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.Phong WHERE id=@p", conn))
            {
                cmd.Parameters.AddWithValue("@p", roomId);
                var count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Phòng không tồn tại.");
                    return false;
                }
                return true;
            }
        }

        // Cập nhật icon/trạng thái phòng trên list ngay lập tức
        private void UpdateRoomStatusInList(int roomId, string status)
        {
            if (roomId <= 0) return;
            foreach (ListViewItem it in lvPhong.Items)
            {
                var tag = it.Tag;
                if (tag == null) continue;
                try
                {
                    var t = tag.GetType();
                    int id = (int)t.GetProperty("Id").GetValue(tag, null);
                    if (id != roomId) continue;
                    int imageIndex = string.Equals(status, "Trống", System.StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                    it.ImageIndex = imageIndex;
                    // thay tag để phản ánh trạng thái mới
                    var ten = it.Text;
                    it.Tag = new { Id = roomId, Ten = ten, Status = status };
                    break;
                }
                catch { }
            }
            // làm mới hiển thị
            try { lvPhong.Invalidate(); } catch { }
        }

        private bool EnsureBill()
        {
            int roomId;
            if (!int.TryParse(txtSoPhong.Text, out roomId))
            {
                MessageBox.Show("Nhập ID phòng (số).", "Thông báo");
                return false;
            }
            if (!EnsureRoomExists(roomId)) return false;

            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_ThemHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPhong", roomId);
                var dt = new DataTable();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    _currentBillId = Convert.ToInt32(dt.Rows[0]["id"]);
                    txtNgayLap.Text = Convert.ToDateTime(dt.Rows[0]["NgayLapHD"]).ToString("dd/MM/yyyy HH:mm");

                    // Đánh dấu phòng đang sử dụng
                    using (var up = new SqlCommand("UPDATE dbo.Phong SET status=N'Đang sử dụng' WHERE id=@p", conn))
                    {
                        up.Parameters.AddWithValue("@p", roomId);
                        up.ExecuteNonQuery();
                    }
                    _currentRoomStatus = "Đang sử dụng";
                    UpdateRoomStatusInList(roomId, _currentRoomStatus);
                    UpdateSoNguoiDisplay(roomId); // đảm bảo đồng bộ hiển thị

                    // đồng bộ giảm giá từ DB
                    LoadCurrentBillDiscount();
                    RefreshBillUI();
                    ToggleActions(true);
                    return true;
                }
            }
            return false;
        }

        private void RefreshBillUI()
        {
            if (_currentBillId <= 0) return;

            dgvChiTiet.Rows.Clear();
            int totalQty = 0;
            decimal totalMoney = 0;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_XuatChiTietHD_HoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@billID", _currentBillId);
                using (var rd = cmd.ExecuteReader())
                {
                    int stt = 1;
                    while (rd.Read())
                    {
                        string ten = rd["TenDV"].ToString();
                        string dvt = rd["DVT"].ToString();
                        int sl = Convert.ToInt32(rd["SoLuong"]);
                        decimal dongia = Convert.ToDecimal(rd["DonGia"]);
                        decimal thanhtien = rd["ThanhTien"] == DBNull.Value ? (sl * dongia) : Convert.ToDecimal(rd["ThanhTien"]);
                        totalQty += sl;
                        totalMoney += thanhtien;
                        int idService = Convert.ToInt32(rd["MaDV"]);
                        string ghiChu = rd["GhiChu"] == DBNull.Value ? string.Empty : rd["GhiChu"].ToString();
                        dgvChiTiet.Rows.Add(stt++, idService, ten, DateTime.Now.ToString("dd/MM/yyyy"), dongia, sl, thanhtien, ghiChu);
                    }
                }
            }
            txtTongDV.Text = totalQty.ToString(); // tổng số dịch vụ (SL cộng dồn)
            txtTongTien.Text = string.Format("{0:N0}", totalMoney);
            RecalcThanhTienLocal();
        }

        private int GetCurrentQtyInBillForService(int dvId)
        {
            int qty = 0;
            try
            {
                foreach (DataGridViewRow r in dgvChiTiet.Rows)
                {
                    if (r.IsNewRow) continue;
                    int id; if (!int.TryParse(Convert.ToString(r.Cells["Column2"].Value), out id)) continue;
                    if (id != dvId) continue;
                    int q; if (int.TryParse(Convert.ToString(r.Cells["Column6"].Value), out q)) qty += q;
                }
            }
            catch { }
            return qty;
        }

        private void UpdateNudSoLuongForServiceId(int dvId)
        {
            try
            {
                int curr = GetCurrentQtyInBillForService(dvId);
                if (curr <= 0) curr = 1;
                var min = (int)nudSoLuong.Minimum;
                var max = (int)nudSoLuong.Maximum;
                curr = Math.Max(min, Math.Min(max, curr));
                nudSoLuong.Value = curr;
            }
            catch
            {
                try { nudSoLuong.Value = 1; } catch { }
            }
        }

        private void DgvDSDV_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDSDV.CurrentRow == null) return;
                var row = dgvDSDV.CurrentRow;
                int dvId = 0;
                if (row.Cells["IdDV"] != null)
                    int.TryParse(Convert.ToString(row.Cells["IdDV"].Value), out dvId);
                if (dvId == 0)
                {
                    var ten = Convert.ToString(row.Cells["Ten"].Value);
                    var dvt = Convert.ToString(row.Cells["DVT"].Value);
                    decimal gia; if (!decimal.TryParse(Convert.ToString(row.Cells["Gia"].Value), out gia)) { nudSoLuong.Value = 1; return; }
                    dvId = GetServiceIdByUnique(ten, dvt, gia, _currentLoaiId == 0 ? 1 : _currentLoaiId);
                }
                if (dvId > 0) UpdateNudSoLuongForServiceId(dvId);
                else nudSoLuong.Value = 1;
            }
            catch { }
        }
        private int GetServiceIdByUnique(string ten, string dvt, decimal gia, int loai)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("SELECT TOP(1) id FROM DSDichVu WHERE TenDV=@Ten AND DVT=@DVT AND DonGia=@Gia AND idLoaiDichVu=@Loai", conn))
            {
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@DVT", dvt);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@Loai", loai);
                var obj = cmd.ExecuteScalar();
                return obj == null ? 0 : Convert.ToInt32(obj);
            }
        }

        private string GetLoaiTenByServiceId(int serviceId)
        {
            if (serviceId <= 0) return null;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"SELECT TOP(1) l.name
                                             FROM DSDichVu dv
                                             JOIN LoaiDichVu l ON l.id = dv.idLoaiDichVu
                                             WHERE dv.id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", serviceId);
                var obj = cmd.ExecuteScalar();
                return obj == null ? null : Convert.ToString(obj);
            }
        }

        private void RecalcThanhTienLocal()
        {
            decimal tong = 0;
            // loại bỏ mọi ký tự không phải số
            var raw = Regex.Replace(txtTongTien.Text ?? string.Empty, @"[^0-9]", "");
            if (!string.IsNullOrEmpty(raw)) decimal.TryParse(raw, out tong);
            var giam = (decimal)nudGiamGia.Value; // % giảm
            var thanh = Math.Max(0, tong * (1 - (giam / 100m)));
            txtThanhTien.Text = string.Format("{0:N0}", thanh);
        }

        private void RdAnUong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAnUong.Checked) { _currentLoaiId = 1; LoadDichVuTheoLoai(_currentLoaiId); }
        }
        private void RdDiLai_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDiLai.Checked) { _currentLoaiId = 2; LoadDichVuTheoLoai(_currentLoaiId); }
        }
        private void RdGiatLa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdGiatLa.Checked) { _currentLoaiId = 3; LoadDichVuTheoLoai(_currentLoaiId); }
        }
        private void RdSpa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSpa.Checked) { _currentLoaiId = 4; LoadDichVuTheoLoai(_currentLoaiId); }
        }
        private void RdGiaiTri_CheckedChanged(object sender, EventArgs e)
        {
            if (rdGiaiTri.Checked) { _currentLoaiId = 5; LoadDichVuTheoLoai(_currentLoaiId); }
        }
        private void NudGiamGia_ValueChanged(object sender, EventArgs e)
        {
            // Tính lại tiền trên UI
            RecalcThanhTienLocal();

            // Nếu đang đồng bộ từ DB -> không ghi DB
            if (_syncingGiamGia) return;

            // Lưu giảm giá ngay vào hóa đơn để frmHoaDon thấy được sau khi reload
            if (_currentBillId > 0)
            {
                try
                {
                    using (var conn = Db.Open())
                    using (var cmd = new SqlCommand("UPDATE dbo.HoaDon SET GiamGia = @g WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@g", nudGiamGia.Value);
                        cmd.Parameters.AddWithValue("@id", _currentBillId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { /* ignore and keep UI only */ }
            }
        }
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvChiTiet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTiet.Columns["Column6"].Index)
            {
                int.TryParse(Convert.ToString(dgvChiTiet.Rows[e.RowIndex].Cells["Column6"].Value), out _editingOldQty);
            }
        }

        private void DgvChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentBillId == 0) return;
            if (e.ColumnIndex != dgvChiTiet.Columns["Column6"].Index) return;

            var row = dgvChiTiet.Rows[e.RowIndex];
            int newQty;
            if (!int.TryParse(Convert.ToString(row.Cells["Column6"].Value), out newQty) || newQty <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ.");
                row.Cells["Column6"].Value = _editingOldQty;
                return;
            }
            var dvIdObj = row.Cells["Column2"].Value;
            int dvId;
            if (dvIdObj == null || !int.TryParse(dvIdObj.ToString(), out dvId) || dvId == 0)
            {
                MessageBox.Show("Không xác định được dịch vụ.");
                return;
            }

            int delta = newQty - _editingOldQty;
            if (delta == 0) return;

            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                cmd.Parameters.AddWithValue("@idDichVu", dvId);
                cmd.Parameters.AddWithValue("@soLuong", delta);
                cmd.Parameters.AddWithValue("@ghiChu", (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            RefreshBillUI();
        }

        private void DgvChiTiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentBillId == 0) return;
            if (e.KeyCode != Keys.Delete) return;
            if (dgvChiTiet.CurrentRow == null) return;

            var row = dgvChiTiet.CurrentRow;
            int dvId;
            if (!int.TryParse(Convert.ToString(row.Cells["Column2"].Value), out dvId)) return;
            int qty; int.TryParse(Convert.ToString(row.Cells["Column6"].Value), out qty);

            if (MessageBox.Show("Xóa dịch vụ khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                cmd.Parameters.AddWithValue("@idDichVu", dvId);
                cmd.Parameters.AddWithValue("@soLuong", -Math.Abs(qty));
                cmd.Parameters.AddWithValue("@ghiChu", (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            RefreshBillUI();
        }

        private void DgvChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_currentBillId == 0) return;

            var row = dgvChiTiet.Rows[e.RowIndex];
            int serviceId; if (!int.TryParse(Convert.ToString(row.Cells["Column2"].Value), out serviceId)) return;
            var ten = Convert.ToString(row.Cells["Column3"].Value);
            decimal donGia = 0; decimal.TryParse(Convert.ToString(row.Cells["Column5"].Value), out donGia);
            var dvt = ""; // không có cột DVT trong lưới -> lấy từ DB nếu cần
            var loai = GetLoaiTenByServiceId(serviceId) ?? GetLoaiTenById(_currentLoaiId);

            int roomId; int.TryParse(txtSoPhong.Text, out roomId);
            using (var f = new frmChiTietDV(serviceId, ten, loai, donGia, dvt, null, roomId, _currentBillId, null, () => RefreshBillUI()))
            {
                f.ShowDialog(this);
            }
        }

        private void BtnThemDV_Click(object sender, EventArgs e)
        {
            // Chặn double-trigger do click nhanh hoặc bị wire event trùng
            if (_addingService) return;
            _addingService = true;
            try
            {
                // Kiểm tra điều kiện
                int roomId;
                if (!int.TryParse(txtSoPhong.Text, out roomId))
                {
                    MessageBox.Show("Vui lòng chọn phòng.");
                    return;
                }
                if (string.Equals(_currentRoomStatus, "Trống", StringComparison.OrdinalIgnoreCase))
                {
                    if (!EnsureBill())
                    {
                        MessageBox.Show("Phòng đang trống, không thể thêm dịch vụ.");
                        return;
                    }
                }

                if (_currentBillId == 0 && !EnsureBill())
                {
                    MessageBox.Show("Phòng chưa có hóa đơn mở.");
                    return;
                }

                if (dgvDSDV.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ.");
                    return;
                }

                var row = dgvDSDV.CurrentRow;
                var ten = Convert.ToString(row.Cells["Ten"].Value);
                var dvt = Convert.ToString(row.Cells["DVT"].Value);
                decimal gia;
                if (!decimal.TryParse(Convert.ToString(row.Cells["Gia"].Value), out gia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ.");
                    return;
                }

                var qty = (int)nudSoLuong.Value;
                if (qty <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
                    return;
                }

                var ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim();
                int dvId = 0;
                if (row.Cells["IdDV"] != null)
                    int.TryParse(Convert.ToString(row.Cells["IdDV"].Value), out dvId);
                if (dvId == 0)
                    dvId = GetServiceIdByUnique(ten, dvt, gia, _currentLoaiId == 0 ? 1 : _currentLoaiId);
                if (dvId == 0)
                {
                    MessageBox.Show("Không tìm thấy dịch vụ.");
                    return;
                }

                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                    cmd.Parameters.AddWithValue("@idDichVu", dvId);
                    cmd.Parameters.AddWithValue("@soLuong", qty);
                    cmd.Parameters.AddWithValue("@ghiChu", ghiChu);
                    cmd.ExecuteNonQuery();
                }

                RefreshBillUI();
            }
            finally
            {
                _addingService = false;
            }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (_currentBillId == 0)
            {
                MessageBox.Show("Chưa có hóa đơn.");
                return;
            }
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dịch vụ để thanh toán.");
                return;
            }

            var giam = (decimal)nudGiamGia.Value;
            int roomId; int.TryParse(txtSoPhong.Text, out roomId);
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_DienHoaDon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@billID", _currentBillId);
                cmd.Parameters.AddWithValue("@giamgia", giam);
                cmd.ExecuteNonQuery();
            }

            // đặt phòng trống lại (DB + UI)
            if (roomId > 0)
            {
                using (var conn = Db.Open())
                {
                    using (var up = new SqlCommand("UPDATE dbo.Phong SET status=N'Trống' WHERE id=@p", conn))
                    {
                        up.Parameters.AddWithValue("@p", roomId);
                        up.ExecuteNonQuery();
                    }
                    // nếu có cột SoNguoi thì trả về0 để đồng bộ frmPhong
                    try
                    {
                        if (ColumnExists(conn, "Phong", "SoNguoi"))
                        {
                            using (var up2 = new SqlCommand("UPDATE dbo.Phong SET SoNguoi =0 WHERE id=@p", conn))
                            {
                                up2.Parameters.AddWithValue("@p", roomId);
                                up2.ExecuteNonQuery();
                            }
                        }
                    }
                    catch { }
                }
                _currentRoomStatus = "Trống";
                UpdateRoomStatusInList(roomId, _currentRoomStatus);
                UpdateSoNguoiDisplay(roomId);
            }

            MessageBox.Show("Thanh toán thành công. Hóa đơn đã được lưu.");

            // Reset sau thanh toán
            _currentBillId = 0;
            ResetChiTietGrid();
            txtTongDV.Clear(); txtTongTien.Clear(); txtThanhTien.Clear();
            ToggleActions(false);
        }

        private void BtnHuyDon_Click(object sender, EventArgs e)
        {
            if (_currentBillId == 0)
            {
                MessageBox.Show("Không có hóa đơn để hủy.");
                return;
            }
            if (MessageBox.Show("Hủy hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int roomId; int.TryParse(txtSoPhong.Text, out roomId);

            using (var conn = Db.Open())
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    using (var cmd = new SqlCommand("DELETE dbo.ChiTietHD WHERE MaHD=@id", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", _currentBillId);
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new SqlCommand("DELETE dbo.HoaDon WHERE id=@id AND status=0", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", _currentBillId);
                        cmd.ExecuteNonQuery();
                    }
                    if (roomId > 0)
                    {
                        using (var cmd = new SqlCommand("UPDATE dbo.Phong SET status=N'Trống' WHERE id=@p", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@p", roomId);
                            cmd.ExecuteNonQuery();
                        }
                        // đặt SoNguoi=0 nếu có cột
                        try
                        {
                            if (ColumnExists(conn, "Phong", "SoNguoi"))
                            {
                                using (var cmd = new SqlCommand("UPDATE dbo.Phong SET SoNguoi=0 WHERE id=@p", conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@p", roomId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch { }
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    MessageBox.Show("Hủy hóa đơn thất bại. Vui lòng thử lại.");
                    return;
                }
            }

            _currentBillId = 0;
            _currentRoomStatus = "Trống";
            if (roomId > 0) UpdateRoomStatusInList(roomId, _currentRoomStatus);
            UpdateSoNguoiDisplay(roomId);

            ResetChiTietGrid();
            ResetThongTinPhongVaKhach();
            ToggleActions(false);
            MessageBox.Show("Hóa đơn và dịch vụ đã được hủy.");
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            int roomId;
            if (!int.TryParse(txtSoPhong.Text, out roomId))
            {
                MessageBox.Show("Phòng chưa được chọn, không thể lưu.");
                return;
            }
            if (_currentCustomerId == null)
            {
                MessageBox.Show("Thông tin khách hàng chưa đầy đủ, vui lòng kiểm tra lại.");
                return;
            }
            if (_currentBillId == 0 && !EnsureBill())
            {
                MessageBox.Show("Không thể tạo/lấy hóa đơn.");
                return;
            }

            // Lưu giảm giá hiện tại vào hóa đơn (đồng bộ với frmHoaDon)
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("UPDATE dbo.HoaDon SET GiamGia=@g WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@g", nudGiamGia.Value);
                    cmd.Parameters.AddWithValue("@id", _currentBillId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }

            // Lưu xong, phòng đang sử dụng -> cập nhật icon ngay (trường hợp vừa tạo hóa đơn)
            _currentRoomStatus = "Đang sử dụng";
            UpdateRoomStatusInList(roomId, _currentRoomStatus);
            UpdateSoNguoiDisplay(roomId);

            MessageBox.Show("Dữ liệu đã được lưu thành công.");
        }

        // Cho phép frmHome nhận khách hàng từ frmKhachHang và ràng buộc vào phòng hiện tại
        private void ApplyCustomerToCurrentRoom(int customerId)
        {
            // Gán vào UI và DB phòng hiện tại nếu có
            int roomId; if (!int.TryParse(txtSoPhong.Text, out roomId) || roomId <= 0) return;

            try
            {
                using (var conn = Db.Open())
                {
                    using (var cmd = new SqlCommand("UPDATE dbo.Phong SET MaKH=@kh WHERE id=@p", conn))
                    {
                        cmd.Parameters.AddWithValue("@kh", customerId);
                        cmd.Parameters.AddWithValue("@p", roomId);
                        cmd.ExecuteNonQuery();
                    }
                    // nếu có cột SoNguoi, đảm bảo tối thiểu là1
                    try
                    {
                        if (ColumnExists(conn, "Phong", "SoNguoi"))
                        {
                            using (var up = new SqlCommand("UPDATE dbo.Phong SET SoNguoi = CASE WHEN ISNULL(SoNguoi,0) <1 THEN1 ELSE SoNguoi END WHERE id=@p", conn))
                            {
                                up.Parameters.AddWithValue("@p", roomId);
                                up.ExecuteNonQuery();
                            }
                        }
                    }
                    catch { }
                }
                UpdateSoNguoiDisplay(roomId);
            }
            catch { }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                using (var f = new frmKhachHang())
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    if (f.ShowDialog(this) == DialogResult.OK && f.SelectedCustomerId.HasValue)
                    {
                        _currentCustomerId = f.SelectedCustomerId.Value;
                        txtKhachHang.Text = f.SelectedCustomerName ?? string.Empty;
                        // cập nhật DB phòng hiện tại -> MaKH = KH vừa chọn
                        ApplyCustomerToCurrentRoom(_currentCustomerId.Value);
                        // nạp thêm SDT/CMND
                        try
                        {
                            using (var conn = Infrastructure.Db.Open())
                            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT TOP(1) CMND_CBCD, SDT FROM dbo.KhachHang WHERE id=@id", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", _currentCustomerId.Value);
                                using (var rd = cmd.ExecuteReader())
                                {
                                    if (rd.Read())
                                    {
                                        txtCMND.Text = Convert.ToString(rd["CMND_CBCD"] ?? "");
                                        txtSoDT.Text = Convert.ToString(rd["SDT"] ?? "");
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở/nhận khách hàng: " + ex.Message);
            }
        }

        // Context menu handler: Xóa dịch vụ đang chọn trong dgvChiTiet
        private void TsmiXoaDV_Click(object sender, EventArgs e)
        {
            if (_currentBillId == 0)
            {
                MessageBox.Show("Chưa có hóa đơn.");
                return;
            }
            if (dgvChiTiet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.");
                return;
            }

            var row = dgvChiTiet.CurrentRow;
            int dvId;
            if (!int.TryParse(Convert.ToString(row.Cells["Column2"].Value), out dvId))
            {
                MessageBox.Show("Không xác định được mã dịch vụ.");
                return;
            }
            int qty; int.TryParse(Convert.ToString(row.Cells["Column6"].Value), out qty);
            if (qty <= 0) qty = 1;

            if (MessageBox.Show("Xóa dịch vụ khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                    cmd.Parameters.AddWithValue("@idDichVu", dvId);
                    cmd.Parameters.AddWithValue("@soLuong", -Math.Abs(qty));
                    cmd.Parameters.AddWithValue("@ghiChu", (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                RefreshBillUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message);
            }
        }

        private void DgvChiTiet_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvChiTiet.CurrentRow == null) return;
                var row = dgvChiTiet.CurrentRow;
                int serviceId; if (!int.TryParse(Convert.ToString(row.Cells["Column2"].Value), out serviceId)) return;
                int qty; if (!int.TryParse(Convert.ToString(row.Cells["Column6"].Value), out qty) || qty <= 0) qty = 1;

                // Cập nhật nudSoLuong theo số lượng hiện có
                var min = (int)nudSoLuong.Minimum; var max = (int)nudSoLuong.Maximum;
                qty = Math.Max(min, Math.Min(max, qty));
                nudSoLuong.Value = qty;

                // Đồng bộ chọn bên danh sách dịch vụ nếu có IdDVẩn trùng
                foreach (DataGridViewRow dvRow in dgvDSDV.Rows)
                {
                    if (dvRow.Cells["IdDV"] != null)
                    {
                        int idTmp; if (int.TryParse(Convert.ToString(dvRow.Cells["IdDV"].Value), out idTmp) && idTmp == serviceId)
                        {
                            dvRow.Selected = true; dgvDSDV.CurrentCell = dvRow.Cells["Ten"]; dgvDSDV.FirstDisplayedScrollingRowIndex = dvRow.Index; break;
                        }
                    }
                }
            }
            catch { }
        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (_syncingNud) return; // đang đồng bộ từ code
            if (_currentBillId == 0) return; // chưa có hóa đơn

            // Xác định dịch vụ đang chọn: ưu tiên dòng chi tiết, nếu không lấy từ danh sách dịch vụ
            int dvId = 0;
            if (dgvChiTiet.CurrentRow != null)
            {
                int.TryParse(Convert.ToString(dgvChiTiet.CurrentRow.Cells["Column2"].Value), out dvId);
            }
            if (dvId == 0 && dgvDSDV.CurrentRow != null)
            {
                var r = dgvDSDV.CurrentRow;
                if (r.Cells["IdDV"] != null)
                    int.TryParse(Convert.ToString(r.Cells["IdDV"].Value), out dvId);
                if (dvId == 0)
                {
                    var ten = Convert.ToString(r.Cells["Ten"].Value);
                    var dvt = Convert.ToString(r.Cells["DVT"].Value);
                    decimal gia; if (!decimal.TryParse(Convert.ToString(r.Cells["Gia"].Value), out gia)) return;
                    dvId = GetServiceIdByUnique(ten, dvt, gia, _currentLoaiId == 0 ? 1 : _currentLoaiId);
                }
            }
            if (dvId == 0) return; // không xác định được dịch vụ

            int currentQty = GetCurrentQtyInBillForService(dvId);
            int newQty = (int)nudSoLuong.Value;

            // Nếu dịch vụ chưa có trong hóa đơn (currentQty=0) thì không cập nhật mà chờ người dùng bấm Thêm
            if (currentQty == 0) return;

            // Nếu mới về0 -> hỏi xóa
            if (newQty <= 0)
            {
                if (MessageBox.Show("Số lượng về0. Xóa dịch vụ khỏi hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = Db.Open())
                        using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                            cmd.Parameters.AddWithValue("@idDichVu", dvId);
                            cmd.Parameters.AddWithValue("@soLuong", -currentQty); // xóa toàn bộ
                            cmd.Parameters.AddWithValue("@ghiChu", (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                        RefreshBillUI();
                        _syncingNud = true; nudSoLuong.Value = 1; _syncingNud = false; // reset về1 cho thao tác thêm mới
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật số lượng: " + ex.Message);
                    }
                }
                else
                {
                    // phục hồi giá trị cũ
                    _syncingNud = true; nudSoLuong.Value = currentQty; _syncingNud = false;
                }
                return;
            }

            int delta = newQty - currentQty;
            if (delta == 0) return;

            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                    cmd.Parameters.AddWithValue("@idDichVu", dvId);
                    cmd.Parameters.AddWithValue("@soLuong", delta);
                    cmd.Parameters.AddWithValue("@ghiChu", (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                RefreshBillUI();
                // Đồng bộ lại numeric với giá trị mới chính thức (phòng trường hợp làm tròn)
                int latest = GetCurrentQtyInBillForService(dvId);
                _syncingNud = true; nudSoLuong.Value = latest > 0 ? latest : 1; _syncingNud = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật số lượng: " + ex.Message);
                _syncingNud = true; nudSoLuong.Value = currentQty; _syncingNud = false; // revert
            }
        }

        // Tải giảm giá hiện tại của hóa đơn từ DB về NumericUpDown
        private void LoadCurrentBillDiscount()
        {
            if (_currentBillId <= 0) return;
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT ISNULL(GiamGia,0) FROM dbo.HoaDon WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _currentBillId);
                    var obj = cmd.ExecuteScalar();
                    decimal val = 0;
                    if (obj != null && obj != DBNull.Value)
                        decimal.TryParse(Convert.ToString(obj), out val);

                    // clamp theo range của control
                    if (val < nudGiamGia.Minimum) val = nudGiamGia.Minimum;
                    if (val > nudGiamGia.Maximum) val = nudGiamGia.Maximum;

                    _syncingGiamGia = true;
                    nudGiamGia.Value = val;
                    _syncingGiamGia = false;
                }
            }
            catch { }
        }
    }
}
