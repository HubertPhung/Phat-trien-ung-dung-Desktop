using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmPhong : Form
    {
        public frmPhong()
        {
            InitializeComponent();
            Load += FrmPhong_Load;

            // Wire events
            lvDSPhong.SelectedIndexChanged += LvDSPhong_SelectedIndexChanged;
            lvDSPhong.MouseDoubleClick += LvDSPhong_MouseDoubleClick;
            btnThem.Click += BtnThem_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTim.Click += BtnTim_Click;
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

            LoadPhong();
            ResetFormInputs();
            SetButtonsState(onRowSelected: false);
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
        }

        private void SetButtonsState(bool onRowSelected)
        {
            btnThem.Enabled = true;
            btnCapNhat.Enabled = onRowSelected;
            btnXoa.Enabled = onRowSelected;
        }

        private void LoadPhong(string soPhongFilter = null, string tenKhachFilter = null)
        {
            lvDSPhong.Items.Clear();
            cbSoPhong.Items.Clear();

            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"
SELECT p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH,
       CASE WHEN p.MaKH IS NULL THEN 0 ELSE 1 END AS SoNguoi,
       ISNULL(SUM(ct.SoLuong),0) AS SoDV,
       ISNULL(SUM(ct.ThanhTien),0) AS ThanhTien
FROM dbo.Phong p
LEFT JOIN dbo.KhachHang kh ON kh.id = p.MaKH
LEFT JOIN dbo.HoaDon hd ON hd.MaPhong = p.id AND hd.status = 0
LEFT JOIN dbo.ChiTietHD ct ON ct.MaHD = hd.id
GROUP BY p.id, p.TenPhong, p.status, p.MaKH, kh.TenKH
ORDER BY p.id;", conn))
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

        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Validation
            var soPhong = cbSoPhong.Text?.Trim();
            if (string.IsNullOrWhiteSpace(soPhong)) { MessageBox.Show("Vui lòng nhập Số Phòng."); return; }

            int soNguoi;
            if (!int.TryParse(txtSoNguoi.Text.Trim(), out soNguoi) || soNguoi <= 0)
            {
                MessageBox.Show("Số Người phải là số > 0.");
                return;
            }

            var tenKhach = txtTenKhachChu.Text.Trim();

            using (var conn = Db.Open())
            {
                // Duplicate check
                using (var check = new SqlCommand("SELECT COUNT(1) FROM dbo.Phong WHERE TenPhong=@ten", conn))
                {
                    check.Parameters.AddWithValue("@ten", soPhong);
                    if ((int)check.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Số phòng đã tồn tại.");
                        return;
                    }
                }

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

                using (var cmd = new SqlCommand("INSERT INTO dbo.Phong(TenPhong, MaKH, GhiChu, status) VALUES(@Ten, @MaKH, NULL, N'Trống')", conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", soPhong);
                    cmd.Parameters.AddWithValue("@MaKH", (object)maKh ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
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

            var soPhong = cbSoPhong.Text?.Trim();
            if (string.IsNullOrWhiteSpace(soPhong)) { MessageBox.Show("Vui lòng nhập Số Phòng."); return; }

            int soNguoi;
            if (!int.TryParse(txtSoNguoi.Text.Trim(), out soNguoi) || soNguoi <= 0)
            {
                MessageBox.Show("Số Người phải là số > 0.");
                return;
            }

            var tenKhach = txtTenKhachChu.Text.Trim();

            if (MessageBox.Show("Bạn có chắc muốn cập nhật thông tin phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

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

                using (var cmd = new SqlCommand("UPDATE dbo.Phong SET MaKH=@MaKH WHERE TenPhong=@Ten", conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", soPhong);
                    cmd.Parameters.AddWithValue("@MaKH", (object)maKh ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
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

            using (var conn = Db.Open())
            {
                // Ensure no open bill exists
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

                using (var del = new SqlCommand("DELETE p FROM dbo.Phong p WHERE p.TenPhong=@Ten", conn))
                {
                    del.Parameters.AddWithValue("@Ten", soPhong);
                    del.ExecuteNonQuery();
                }
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
    }
}
