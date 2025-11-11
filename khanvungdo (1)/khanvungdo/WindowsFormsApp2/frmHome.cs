using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;
using System.Reflection;

namespace WindowsFormsApp2
{
    public partial class frmHome : Form
    {
        private int _currentLoaiId = 0; // 0: none selected yet
        private int _currentBillId = 0;
        private int? _currentCustomerId = null;
        private int _editingOldQty = 0;
        private string _currentRoomStatus = ""; // Trống / Đang sử dụng

        public frmHome()
        {
            InitializeComponent();
            this.Load += FrmHome_Load;
            WireEvents();
            EnsureDynamicColumns();
            ConfigureChiTietGridEditing();
            ConfigureGridsUi();

            // Trạng thái UI ban đầu theo yêu cầu
            ResetDichVuList();
            ResetThongTinPhongVaKhach();
            ResetChiTietGrid();
            ToggleActions(false);
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

        private void ConfigureChiTietGridEditing()
        {
            // Cho phép chỉnh sửa SL trên lưới chi tiết hóa đơn
            dgvChiTiet.ReadOnly = false;
            foreach (DataGridViewColumn c in dgvChiTiet.Columns) c.ReadOnly = true;
            if (dgvChiTiet.Columns.Contains("Column6")) // SL
                dgvChiTiet.Columns["Column6"].ReadOnly = false;
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
        }

        private void WireEvents()
        {
            rdAnUong.CheckedChanged += (s, e) => { if (rdAnUong.Checked) { _currentLoaiId = 1; LoadDichVuTheoLoai(_currentLoaiId); } };
            rdDiLai.CheckedChanged += (s, e) => { if (rdDiLai.Checked) { _currentLoaiId = 2; LoadDichVuTheoLoai(_currentLoaiId); } };
            rdGiatLa.CheckedChanged += (s, e) => { if (rdGiatLa.Checked) { _currentLoaiId = 3; LoadDichVuTheoLoai(_currentLoaiId); } };
            rdSpa.CheckedChanged += (s, e) => { if (rdSpa.Checked) { _currentLoaiId = 4; LoadDichVuTheoLoai(_currentLoaiId); } };
            rdGiaiTri.CheckedChanged += (s, e) => { if (rdGiaiTri.Checked) { _currentLoaiId = 5; LoadDichVuTheoLoai(_currentLoaiId); } };

            btnThemDV.Click += BtnThemDV_Click;
            btnThanhToan.Click += BtnThanhToan_Click;
            btnHuyDon.Click += BtnHuyDon_Click;
            btnLuu.Click += BtnLuu_Click;
            btnThoat.Click += (s, e) => this.Close();
            nudGiamGia.ValueChanged += (s, e) => RecalcThanhTienLocal();

            dgvChiTiet.CellBeginEdit += DgvChiTiet_CellBeginEdit;
            dgvChiTiet.CellEndEdit += DgvChiTiet_CellEndEdit;
            dgvChiTiet.KeyDown += DgvChiTiet_KeyDown;
            dgvChiTiet.CellDoubleClick += DgvChiTiet_CellDoubleClick; // mở chi tiết dịch vụ từ dòng hóa đơn

            lvPhong.ItemSelectionChanged += LvPhong_ItemSelectionChanged;
            dgvDSDV.CellDoubleClick += DgvDSDV_CellDoubleClick;
        }

        private void ConfigureGridsUi()
        {
            // bật double buffering để mượt hơn
            TryEnableDoubleBuffering(lvPhong);
            TryEnableDoubleBuffering(dgvDSDV);
            TryEnableDoubleBuffering(dgvChiTiet);

            // cấu hình chọn dòng
            dgvDSDV.ReadOnly = true;
            dgvDSDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDSDV.MultiSelect = false;

            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvChiTiet.MultiSelect = false;

            // autosize cột cơ bản
            foreach (DataGridViewColumn c in dgvDSDV.Columns) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (DataGridViewColumn c in dgvChiTiet.Columns) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                MessageBox.Show("Phòng hiện đang trống.");
                return;
            }

            // Đang sử dụng -> tải thông tin khách + hóa đơn mở (nếu có)
            try
            {
                LoadKhachHangTheoPhong(roomId);
                LoadHoaDonMoTheoPhong(roomId);
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

            // đặt phòng trống lại
            if (roomId > 0)
            {
                using (var conn = Db.Open())
                using (var up = new SqlCommand("UPDATE dbo.Phong SET status=N'Trống' WHERE id=@p", conn))
                {
                    up.Parameters.AddWithValue("@p", roomId);
                    up.ExecuteNonQuery();
                }
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
                    // Thay vì xóa, có thể cập nhật status =2 (Đã hủy) nếu nghiệp vụ yêu cầu.
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
            //Ở đây có thể bổ sung cập nhật ghi chú, giảm giá tạm, ...
            MessageBox.Show("Dữ liệu đã được lưu thành công.");
        }
    }
}
