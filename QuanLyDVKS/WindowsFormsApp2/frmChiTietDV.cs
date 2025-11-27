using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmChiTietDV : Form
    {
        private readonly int _serviceId;
        private readonly string _tenDV;
        private readonly string _loaiDV;
        private readonly decimal _donGia;
        private readonly string _dvt;
        private readonly string _luuY;
        private readonly int _roomId;
        private int _currentBillId;
        private readonly string _imagePath;
        private bool _hasChanges;
        private readonly Action _onAddedSuccess; // callback to refresh parent

        public frmChiTietDV()
        {
            InitializeComponent();
        }

        public frmChiTietDV(int serviceId, string tenDV, string loaiDV, decimal donGia, string dvt, string luuY, int roomId, int currentBillId = 0, string imagePath = null, Action onAddedSuccess = null)
            : this()
        {
            _serviceId = serviceId;
            _tenDV = tenDV;
            _loaiDV = loaiDV;
            _donGia = donGia;
            _dvt = dvt;
            _luuY = luuY;
            _roomId = roomId;
            _currentBillId = currentBillId;
            _imagePath = imagePath;
            _onAddedSuccess = onAddedSuccess;

            // Chỉ gắn Load ở đây; các event Button/NumericUpDown đã được gắn trong Designer để tránh nhân đôi handler
            this.Load += FrmChiTietDV_Load;

            // Cấu hình giới hạn và mặc định cho số lượng
            this.nudSoLuong.Minimum = 1;
            this.nudSoLuong.Maximum = 1000;
            this.nudSoLuong.Value = 1;
        }

        private void FrmChiTietDV_Load(object sender, EventArgs e)
        {
            try
            {
                // Nhận thông tin và hiển thị
                lblTenDV.Text = _tenDV ?? "";
                lblLoaiDV.Text = _loaiDV ?? "";
                lblDonGia.Text = string.Format("{0:N0}", _donGia);
                lblDVT.Text = _dvt ?? "";
                lblLuuY.Text = string.IsNullOrWhiteSpace(_luuY) ? "" : _luuY;

                // Kiểm tra tồn tại/available (dịch vụ còn trong hệ thống)
                if (!ServiceExists(_serviceId))
                {
                    MessageBox.Show("Dịch vụ không còn tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnDat.Enabled = false;
                }

                // Load ảnh (nếu có): ưu tiên imagePath, sau đó thử theo quy ước ./Images/DichVu/{id}.jpg
                Image img = null;
                if (!string.IsNullOrWhiteSpace(_imagePath) && File.Exists(_imagePath))
                {
                    using (var fs = new FileStream(_imagePath, FileMode.Open, FileAccess.Read))
                    {
                        img = Image.FromStream(fs);
                    }
                }
                else
                {
                    var exeDir = AppDomain.CurrentDomain.BaseDirectory;
                    var guess = Path.Combine(exeDir, "Images", "DichVu", _serviceId + ".jpg");
                    if (File.Exists(guess))
                    {
                        using (var fs = new FileStream(guess, FileMode.Open, FileAccess.Read))
                        {
                            img = Image.FromStream(fs);
                        }
                    }
                }
                if (img != null)
                {
                    guna2PictureBox1.Image = img;
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ServiceExists(int id)
        {
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.DSDichVu WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var c = (int)cmd.ExecuteScalar();
                    return c > 0;
                }
            }
            catch
            {
                MessageBox.Show("Mất kết nối, không thể kiểm tra dịch vụ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void NudSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (nudSoLuong.Value < nudSoLuong.Maximum) nudSoLuong.Value += 1; e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (nudSoLuong.Value > nudSoLuong.Minimum) nudSoLuong.Value -= 1; e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                nudSoLuong.Value = Math.Min(nudSoLuong.Maximum, nudSoLuong.Value + 5); e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                nudSoLuong.Value = Math.Max(nudSoLuong.Minimum, nudSoLuong.Value - 5); e.Handled = true;
            }
        }

        private void BtnDat_Click(object sender, EventArgs e)
        {
            // Validation cuối
            if (nudSoLuong.Value < 1)
            {
                MessageBox.Show("Số lượng phải >= 1", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ServiceExists(_serviceId))
            {
                MessageBox.Show("Dịch vụ không còn tồn tại trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_roomId <= 0)
            {
                MessageBox.Show("Chưa chọn phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Đảm bảo hóa đơn hiện tại
                if (_currentBillId <= 0)
                {
                    using (var conn = Db.Open())
                    using (var cmd = new SqlCommand("USP_ThemHoaDon", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPhong", _roomId);
                        var dt = new DataTable();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Lỗi hóa đơn, vui lòng kiểm tra lại thông tin phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        _currentBillId = Convert.ToInt32(dt.Rows[0]["id"]);
                    }
                }

                // Thêm chi tiết
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_InsertChiTietHD", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idHoaDon", _currentBillId);
                    cmd.Parameters.AddWithValue("@idDichVu", _serviceId);
                    cmd.Parameters.AddWithValue("@soLuong", (int)nudSoLuong.Value);
                    // Lưu ý xem như ghi chú mặc định (nếu có)
                    cmd.Parameters.AddWithValue("@ghiChu", string.IsNullOrWhiteSpace(_luuY) ? (object)DBNull.Value : _luuY);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đã thêm dịch vụ vào hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _onAddedSuccess?.Invoke();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            if (_hasChanges)
            {
                var confirm = MessageBox.Show("Bạn có thay đổi chưa lưu. Có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Designer-wired handler to mirror previous lambda
        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            try { _hasChanges = (int)nudSoLuong.Value != 1; } catch { }
        }
    }
}
