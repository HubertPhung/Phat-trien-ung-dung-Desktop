using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;
using WindowsFormsApp2.Security;

namespace WindowsFormsApp2
{
    public partial class frmHoaDon : Form
    {
        private ContextMenuStrip _ctx;
        private bool _canEdit; // permission for update/delete

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // Permissions: Admin can edit/delete; staff can view
                _canEdit = UserContext.Type == 1;

                // Default date range: first day of current month -> today
                var today = DateTime.Today;
                dtpLapTuNgay.Value = new DateTime(today.Year, today.Month, 1);
                dtpDenNgay.Value = today;

                // Load combo rooms with "<Tất cả>"
                LoadPhongToCombo();

                // Load invoices with defaults
                LoadHoaDon();

                // Wire events with validation and auto reload
                dtpLapTuNgay.ValueChanged += (s, ev) =>
                {
                    if (dtpLapTuNgay.Value.Date > dtpDenNgay.Value.Date)
                    {
                        MessageBox.Show("Từ ngày không được lớn hơn Đến ngày");
                        dtpDenNgay.Value = dtpLapTuNgay.Value;
                    }
                    LoadHoaDon();
                };
                dtpDenNgay.ValueChanged += (s, ev) =>
                {
                    if (dtpDenNgay.Value.Date < dtpLapTuNgay.Value.Date)
                    {
                        MessageBox.Show("Đến ngày không được nhỏ hơn Từ ngày");
                        dtpLapTuNgay.Value = dtpDenNgay.Value;
                    }
                    LoadHoaDon();
                };
                cbPhong.SelectionChangeCommitted += (s, ev) => LoadHoaDon();

                // Double-click to update/print
                lvHoaDon.DoubleClick += (s, ev) => OpenUpdate();

                // Context menu for Update/Delete
                _ctx = new ContextMenuStrip();
                var mUpdate = new ToolStripMenuItem("Cập nhật", null, (s, ev) => OpenUpdate());
                var mDelete = new ToolStripMenuItem("Xóa", null, (s, ev) => DeleteSelected());
                _ctx.Items.Add(mUpdate);
                _ctx.Items.Add(mDelete);
                lvHoaDon.ContextMenuStrip = _ctx;
                _ctx.Opening += (s, ev) =>
                {
                    bool hasSel = lvHoaDon.SelectedItems.Count > 0;
                    mUpdate.Enabled = hasSel && _canEdit;
                    mDelete.Enabled = hasSel && _canEdit;
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu hóa đơn: " + ex.Message);
            }
        }

        private void LoadPhongToCombo()
        {
            try
            {
                cbPhong.Items.Clear();
                cbPhong.Items.Add("<Tất cả>");

                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT id, TenPhong FROM dbo.Phong ORDER BY id", conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cbPhong.Items.Add(new ComboItem
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            Text = rd["TenPhong"].ToString()
                        });
                    }
                }
                cbPhong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message);
            }
        }

        private void LoadHoaDon()
        {
            try
            {
                lvHoaDon.Items.Clear();

                var from = dtpLapTuNgay.Value.Date;
                var to = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

                int? roomId = null;
                if (cbPhong.SelectedItem is ComboItem it && it.Id > 0)
                    roomId = it.Id;

                // SỬA QUAN TRỌNG: Sử dụng stored procedure hoặc query an toàn
                string sql = @"SELECT hd.id AS MaHD, hd.NgayLapHD, hd.NgayKetThucHD, hd.MaPhong, 
                                      hd.TongTien, hd.GiamGia, hd.ThanhTien, hd.status,
                                      p.TenPhong,
                                      CASE WHEN hd.status = 1 THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END AS TrangThai
                               FROM dbo.HoaDon hd
                               INNER JOIN dbo.Phong p ON hd.MaPhong = p.id
                               WHERE hd.NgayLapHD >= @from AND hd.NgayLapHD <= @to";

                if (roomId.HasValue)
                    sql += " AND hd.MaPhong = @room";

                sql += " ORDER BY hd.NgayLapHD DESC";

                using (var conn = Db.Open())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    if (roomId.HasValue)
                        cmd.Parameters.AddWithValue("@room", roomId.Value);

                    using (var rd = cmd.ExecuteReader())
                    {
                        int count = 0;
                        while (rd.Read())
                        {
                            var item = new ListViewItem(Convert.ToString(rd["MaHD"]));
                            item.SubItems.Add(Convert.ToDateTime(rd["NgayLapHD"]).ToString("dd/MM/yyyy"));

                            // Xử lý NgayKetThucHD có thể NULL
                            object ngayKetThuc = rd["NgayKetThucHD"];
                            item.SubItems.Add(ngayKetThuc == DBNull.Value ? "" : Convert.ToDateTime(ngayKetThuc).ToString("dd/MM/yyyy"));

                            // Hiển thị tên phòng thay vì mã phòng
                            item.SubItems.Add(rd["TenPhong"].ToString());

                            // Xử lý các cột số có thể NULL
                            item.SubItems.Add(FormatCurrency(rd["TongTien"]));
                            item.SubItems.Add(FormatPercentage(rd["GiamGia"]));
                            item.SubItems.Add(FormatCurrency(rd["ThanhTien"]));
                            item.SubItems.Add(Convert.ToString(rd["TrangThai"]));

                            lvHoaDon.Items.Add(item);
                            count++;
                        }

                        if (count == 0)
                        {
                            MessageBox.Show("Không có hóa đơn nào trong khoảng thời gian này");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hóa đơn: " + ex.Message);
            }
        }

        // Hàm định dạng tiền tệ an toàn
        private string FormatCurrency(object value)
        {
            if (value == DBNull.Value || value == null)
                return "0 VND";

            try
            {
                decimal amount = Convert.ToDecimal(value);
                return string.Format("{0:N0} VND", amount);
            }
            catch
            {
                return "0 VND";
            }
        }

        // Hàm định dạng phần trăm an toàn
        private string FormatPercentage(object value)
        {
            if (value == DBNull.Value || value == null)
                return "0%";

            try
            {
                decimal percent = Convert.ToDecimal(value);
                return string.Format("{0:N0}%", percent);
            }
            catch
            {
                return "0%";
            }
        }

        private void OpenUpdate()
        {
            if (lvHoaDon.SelectedItems.Count == 0) return;
            if (!_canEdit)
            {
                MessageBox.Show("Bạn không có quyền cập nhật");
                return;
            }

            try
            {
                int id = Convert.ToInt32(lvHoaDon.SelectedItems[0].Text);
                using (var f = new frmInHoaDon(id))
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.ShowDialog(this);
                }
                LoadHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở cập nhật hóa đơn: " + ex.Message);
            }
        }

        private void DeleteSelected()
        {
            if (lvHoaDon.SelectedItems.Count == 0) return;
            if (!_canEdit)
            {
                MessageBox.Show("Bạn không có quyền xóa");
                return;
            }

            try
            {
                int id = Convert.ToInt32(lvHoaDon.SelectedItems[0].Text);

                // Kiểm tra trạng thái hóa đơn trước khi xóa
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT status FROM dbo.HoaDon WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var status = cmd.ExecuteScalar();

                    if (status != null && Convert.ToInt32(status) == 1) // Đã thanh toán
                    {
                        MessageBox.Show("Không thể xóa hóa đơn đã thanh toán", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận",
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                using (var conn = Db.Open())
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa chi tiết hóa đơn
                        using (var delCt = new SqlCommand("DELETE FROM dbo.ChiTietHD WHERE MaHD=@id", conn, tran))
                        {
                            delCt.Parameters.AddWithValue("@id", id);
                            delCt.ExecuteNonQuery();
                        }

                        // Xóa hóa đơn
                        using (var delHd = new SqlCommand("DELETE FROM dbo.HoaDon WHERE id=@id", conn, tran))
                        {
                            delHd.Parameters.AddWithValue("@id", id);
                            int affected = delHd.ExecuteNonQuery();

                            if (affected == 0)
                            {
                                tran.Rollback();
                                MessageBox.Show("Hóa đơn không tồn tại hoặc đã bị xóa");
                                return;
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Xóa hóa đơn thành công");
                        LoadHoaDon();
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { }
                        MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
            }
        }

        private void cậpNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUpdate();
        }

        // Thêm nút tải lại dữ liệu
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadHoaDon();
        }

        private class ComboItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        // Designer-wired wrappers
        private void dtpLapTuNgay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpLapTuNgay.Value.Date > dtpDenNgay.Value.Date)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn Đến ngày");
                    dtpDenNgay.Value = dtpLapTuNgay.Value;
                }
                LoadHoaDon();
            }
            catch { }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtpDenNgay.Value.Date < dtpLapTuNgay.Value.Date)
                {
                    MessageBox.Show("Đến ngày không được nhỏ hơn Từ ngày");
                    dtpLapTuNgay.Value = dtpDenNgay.Value;
                }
                LoadHoaDon();
            }
            catch { }
        }

        private void cbPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try { LoadHoaDon(); } catch { }
        }

        private void lvHoaDon_DoubleClick(object sender, EventArgs e)
        {
            try { OpenUpdate(); } catch { }
        }

        private void DeleteSelected(object sender, EventArgs e)
        {
            try { DeleteSelected(); } catch { }
        }

        private void OpenUpdate(object sender, EventArgs e)
        {
            try { OpenUpdate(); } catch { }
        }
    }
}