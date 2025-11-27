using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmDichVu : Form
    {
        private bool _isAdmin;
        private bool _dirtyInputs;

        public frmDichVu()
        {
            InitializeComponent();
            Load += FrmDichVu_Load;
            lvDSDV.SelectedIndexChanged += LvDSDV_SelectedIndexChanged;
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTim.Click += BtnTim_Click;
            guna2Button1.Click += BtnCapNhat_Click; // Cập nhật
            button1.Click += BtnChonHinh_Click;
            txtMaDV.TextChanged += TxtMaDV_TextChanged;
            txtTenDV.TextChanged += TxtTenDV_TextChanged;
            txtDonGia.TextChanged += (s, e) => { _dirtyInputs = true; };
            txtDonGia.Leave += TxtDonGia_Leave;
            txtDonGia.KeyPress += TxtDonGia_KeyPress;
            txtDVT.TextChanged += (s, e) => { _dirtyInputs = true; };
            txtLuuY.TextChanged += (s, e) => { _dirtyInputs = true; };
            this.FormClosing += FrmDichVu_FormClosing;
        }

        private void FrmDichVu_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            _isAdmin = WindowsFormsApp2.Security.UserContext.Type == 1;
            SetButtonsByRole(canEdit: _isAdmin);

            // Kết nối CSDL
            try
            {
                using (var conn = Db.Open()) { }
            }
            catch
            {
                MessageBox.Show("Mất kết nối database", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetButtonsEnabled(false);
                return;
            }

            // cấu hình auto-complete cho ĐVT và một số ràng buộc nhập liệu
            ConfigureDvtAutocomplete();
            txtMaDV.MaxLength = 10;
            txtTenDV.MaxLength = 70;

            LoadLoaiDichVu();
            LoadDanhSachDichVu();
            ResetInputs();
            // Định dạng list
            lvDSDV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ConfigureDvtAutocomplete()
        {
            try
            {
                var units = new[] { "Lần", "Giờ", "Ngày", "Chai", "Lon", "Cái", "Kg", "Gói", "Suất", "Vé" };
                var src = new AutoCompleteStringCollection();
                src.AddRange(units);
                txtDVT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDVT.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDVT.AutoCompleteCustomSource = src;
            }
            catch { /* ignore UI config errors */ }
        }

        private class LoaiItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        private void LoadLoaiDichVu()
        {
            comboBox1.Items.Clear();
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("USP_XuatLoaiDichVu", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        comboBox1.Items.Add(new LoaiItem
                        {
                            Id = Convert.ToInt32(rd["id"]),
                            Name = rd["name"].ToString()
                        });
                    }
                }
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void LoadDanhSachDichVu(string keyword = null, int? id = null)
        {
            lvDSDV.Items.Clear();
            var sql = @"SELECT dv.id, dv.TenDV, dv.DVT, dv.DonGia, dv.LuuY, l.name AS Loai
                        FROM dbo.DSDichVu dv JOIN dbo.LoaiDichVu l ON l.id = dv.idLoaiDichVu
                        WHERE (ISNULL(@kw,'') = '' OR dv.TenDV LIKE '%' + @kw + '%')
                          AND (@id IS NULL OR dv.id = @id)
                        ORDER BY dv.TenDV";
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", (object)(keyword ?? string.Empty));
                cmd.Parameters.AddWithValue("@id", (object)id ?? DBNull.Value);
                using (var rd = cmd.ExecuteReader())
                {
                    int stt = 1;
                    while (rd.Read())
                    {
                        var item = new ListViewItem(stt.ToString());
                        item.SubItems.Add(""); // Hình - chưa lưu DB
                        item.SubItems.Add(Convert.ToString(rd["id"]));
                        item.SubItems.Add(rd["TenDV"].ToString());
                        item.SubItems.Add(rd["Loai"].ToString());
                        item.SubItems.Add(string.Format("{0:N0}", rd["DonGia"]));
                        item.SubItems.Add(rd["DVT"].ToString());
                        item.SubItems.Add(rd["LuuY"] == DBNull.Value ? string.Empty : rd["LuuY"].ToString());
                        lvDSDV.Items.Add(item);
                        stt++;
                    }
                }
            }
        }

        private void SetButtonsByRole(bool canEdit)
        {
            btnThem.Enabled = canEdit;
            guna2Button1.Enabled = false; // chỉ bật khi chọn + có quyền
            btnXoa.Enabled = false; // chỉ bật khi chọn + có quyền
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnThem.Enabled = enabled && _isAdmin;
            guna2Button1.Enabled = false;
            btnXoa.Enabled = false;
            btnTim.Enabled = enabled;
        }

        private void ResetInputs()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDVT.Clear();
            txtDonGia.Clear();
            txtLuuY.Clear();
            textBox1.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            guna2Button1.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = _isAdmin;
            _dirtyInputs = false;
        }

        private void LvDSDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDSDV.SelectedItems.Count == 0)
            {
                ResetInputs();
                return;
            }
            var it = lvDSDV.SelectedItems[0];
            txtMaDV.Text = it.SubItems[2].Text;
            txtTenDV.Text = it.SubItems[3].Text;
            var loaiText = it.SubItems[4].Text;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if ((comboBox1.Items[i] as LoaiItem)?.Name == loaiText)
                {
                    comboBox1.SelectedIndex = i; break;
                }
            }
            txtDonGia.Text = it.SubItems[5].Text.Replace(",", string.Empty);
            txtDVT.Text = it.SubItems[6].Text;
            txtLuuY.Text = it.SubItems[7].Text;

            guna2Button1.Enabled = _isAdmin; // cập nhật
            btnXoa.Enabled = _isAdmin; // xóa
            _dirtyInputs = false;
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(txtMaDV.Text.Trim(), out id))
            {
                LoadDanhSachDichVu(null, id);
            }
            else
            {
                var kw = string.IsNullOrWhiteSpace(txtTenDV.Text) ? null : txtTenDV.Text.Trim();
                LoadDanhSachDichVu(kw, null);
            }
        }

        private bool ValidateInputsForAdd(out decimal donGia)
        {
            donGia = 0;
            // Highlight và validate
            txtTenDV.BackColor = System.Drawing.SystemColors.Window;
            txtDVT.BackColor = System.Drawing.SystemColors.Window;
            txtDonGia.BackColor = System.Drawing.SystemColors.Window;

            if (string.IsNullOrWhiteSpace(txtTenDV.Text)) { txtTenDV.BackColor = System.Drawing.Color.MistyRose; MessageBox.Show("Vui lòng nhập Tên dịch vụ"); return false; }
            if (string.IsNullOrWhiteSpace(txtDVT.Text)) { txtDVT.BackColor = System.Drawing.Color.MistyRose; MessageBox.Show("Vui lòng nhập Đơn vị tính"); return false; }
            if (!decimal.TryParse(txtDonGia.Text.Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out donGia) || donGia <= 0)
            { txtDonGia.BackColor = System.Drawing.Color.MistyRose; MessageBox.Show("Đơn giá phải là số dương"); return false; }
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Chọn loại dịch vụ"); return false; }
            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!_isAdmin) { MessageBox.Show("Bạn không có quyền truy cập"); return; }
            decimal dongia;
            if (!ValidateInputsForAdd(out dongia)) return;

            var ten = txtTenDV.Text.Trim();
            var dvt = txtDVT.Text.Trim();
            var luuy = string.IsNullOrWhiteSpace(txtLuuY.Text) ? (object)DBNull.Value : txtLuuY.Text.Trim();
            var loaiId = (comboBox1.SelectedItem as LoaiItem).Id;

            using (var conn = Db.Open())
            {
                using (var chk = new SqlCommand("SELECT COUNT(1) FROM dbo.DSDichVu WHERE TenDV=@Ten AND idLoaiDichVu=@Loai", conn))
                {
                    chk.Parameters.AddWithValue("@Ten", ten);
                    chk.Parameters.AddWithValue("@Loai", loaiId);
                    if ((int)chk.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Tên dịch vụ đã tồn tại trong nhóm này.");
                        return;
                    }
                }

                using (var cmd = new SqlCommand("INSERT dbo.DSDichVu(TenDV, idLoaiDichVu, DonGia, DVT, LuuY) OUTPUT INSERTED.id VALUES(@Ten,@Loai,@Gia,@DVT,@LuuY)", conn))
                {
                    cmd.Parameters.AddWithValue("@Ten", ten);
                    cmd.Parameters.AddWithValue("@Loai", loaiId);
                    cmd.Parameters.AddWithValue("@Gia", dongia);
                    cmd.Parameters.AddWithValue("@DVT", dvt);
                    cmd.Parameters.AddWithValue("@LuuY", luuy);
                    try
                    {
                        var newIdObj = cmd.ExecuteScalar();
                        MessageBox.Show("Thêm dịch vụ mới thành công!");
                        var newId = Convert.ToInt32(newIdObj);
                        ResetInputs();
                        LoadDanhSachDichVu();
                        foreach (ListViewItem it in lvDSDV.Items)
                        {
                            if (it.SubItems[2].Text == newId.ToString())
                            {
                                it.Selected = true; it.Focused = true; it.EnsureVisible(); break;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message);
                    }
                }
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (!_isAdmin) { MessageBox.Show("Bạn không có quyền truy cập"); return; }
            int id;
            if (!int.TryParse(txtMaDV.Text.Trim(), out id))
            {
                MessageBox.Show("Phải chọn dịch vụ để cập nhật."); return;
            }
            var ten = txtTenDV.Text.Trim();
            var dvt = txtDVT.Text.Trim();
            var luuy = string.IsNullOrWhiteSpace(txtLuuY.Text) ? (object)DBNull.Value : txtLuuY.Text.Trim();
            decimal dongia;
            if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(dvt) || !decimal.TryParse(txtDonGia.Text.Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out dongia) || dongia <= 0)
            {
                MessageBox.Show("Nhập Tên, ĐVT và Đơn giá hợp lệ."); return;
            }
            if (comboBox1.SelectedItem == null) { MessageBox.Show("Chọn loại dịch vụ"); return; }
            var loaiId = (comboBox1.SelectedItem as LoaiItem).Id;

            // Kiểm tra thay đổi dữ liệu so với dòng đang chọn
            if (lvDSDV.SelectedItems.Count > 0)
            {
                var it = lvDSDV.SelectedItems[0];
                var oldTen = it.SubItems[3].Text;
                var oldLoai = it.SubItems[4].Text;
                var oldGia = it.SubItems[5].Text.Replace(",", "");
                var oldDvt = it.SubItems[6].Text;
                var oldLoaiId = (comboBox1.Items.Cast<object>().FirstOrDefault(x => (x as LoaiItem)?.Name == oldLoai) as LoaiItem)?.Id;
                if (oldTen == ten && oldDvt == dvt && oldGia == Convert.ToString(dongia, CultureInfo.InvariantCulture) && oldLoaiId == loaiId)
                {
                    MessageBox.Show("Không có thay đổi để cập nhật.");
                    return;
                }
            }

            if (MessageBox.Show("Bạn có chắc muốn cập nhật thông tin dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var conn = Db.Open())
            {
                // cảnh báo nếu dịch vụ đang được sử dụng - chỉ cần kiểm tra tồn tại một dòng
                //using (var warn = new SqlCommand("SELECT TOP11 FROM dbo.ChiTietHD WHERE MaDV=@Id", conn))
                //{
                //    warn.Parameters.AddWithValue("@Id", id);
                //    var used = warn.ExecuteScalar() != null;
                //    if (used)
                //    {
                //        MessageBox.Show("Dịch vụ đang được sử dụng. Việc thay đổi có thể ảnh hưởng đến hóa đơn hiện tại.");
                //    }
                //}

                using (var cmd = new SqlCommand(@"UPDATE dbo.DSDichVu
                                            SET TenDV=@Ten, idLoaiDichVu=@Loai, DonGia=@Gia, DVT=@DVT, LuuY=@LuuY
                                            WHERE id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Ten", ten);
                    cmd.Parameters.AddWithValue("@Loai", loaiId);
                    cmd.Parameters.AddWithValue("@Gia", dongia);
                    cmd.Parameters.AddWithValue("@DVT", dvt);
                    cmd.Parameters.AddWithValue("@LuuY", luuy);
                    try
                    {
                        var rows = cmd.ExecuteNonQuery();
                        if (rows == 0) { MessageBox.Show("Không tìm thấy DV để cập nhật."); return; }
                        MessageBox.Show("Cập nhật dịch vụ thành công!");
                        LoadDanhSachDichVu();
                        foreach (ListViewItem it in lvDSDV.Items)
                        {
                            if (it.SubItems[2].Text == id.ToString())
                            {
                                it.Selected = true; it.Focused = true; it.EnsureVisible(); break;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
                    }
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (!_isAdmin) { MessageBox.Show("Bạn không có quyền truy cập"); return; }
            var ids = new List<int>();
            foreach (ListViewItem it in lvDSDV.Items)
            {
                if (it.Checked)
                {
                    int id; if (int.TryParse(it.SubItems[2].Text, out id)) ids.Add(id);
                }
            }
            if (ids.Count == 0 && lvDSDV.SelectedItems.Count > 0)
            {
                int id; if (int.TryParse(lvDSDV.SelectedItems[0].SubItems[2].Text, out id)) ids.Add(id);
            }
            if (ids.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất một dịch vụ để xóa."); return;
            }

            using (var conn = Db.Open())
            using (var chk = new SqlCommand("SELECT COUNT(1) FROM dbo.ChiTietHD WHERE MaDV=@Id", conn))
            using (var del = new SqlCommand("DELETE dbo.DSDichVu WHERE id=@Id", conn))
            {
                chk.Parameters.Add("@Id", SqlDbType.Int);
                del.Parameters.Add("@Id", SqlDbType.Int);

                foreach (var id in ids.ToList())
                {
                    chk.Parameters["@Id"].Value = id;
                    var used = (int)chk.ExecuteScalar() > 0;
                    if (used)
                    {
                        MessageBox.Show($"Không thể xóa dịch vụ ID {id} vì đang được sử dụng trong hóa đơn.");
                        ids.Remove(id);
                    }
                }

                if (ids.Count == 0) return;

                if (MessageBox.Show($"Bạn có chắc muốn xóa {ids.Count} dịch vụ đã chọn? Dữ liệu không thể khôi phục!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                try
                {
                    foreach (var id in ids)
                    {
                        del.Parameters["@Id"].Value = id;
                        del.ExecuteNonQuery();
                    }
                    MessageBox.Show("Xóa dịch vụ thành công!");
                    ResetInputs();
                    LoadDanhSachDichVu();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void BtnChonHinh_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh dịch vụ";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                ofd.Multiselect = false;
                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    var file = ofd.FileName;
                    if (!File.Exists(file)) { MessageBox.Show("File không tồn tại"); return; }
                    var ext = Path.GetExtension(file).ToLowerInvariant();
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png") { MessageBox.Show("Định dạng file không được hỗ trợ"); return; }
                    textBox1.Text = file;
                    _dirtyInputs = true;
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Không thể load hình ảnh (Out of memory)");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể load hình ảnh: " + ex.Message);
                }
            }
        }

        private void TxtMaDV_TextChanged(object sender, EventArgs e)
        {
            if (txtMaDV.Focused)
            {
                // Chuyển hoa + giới hạn độ dài10
                var caret = txtMaDV.SelectionStart;
                var t = txtMaDV.Text.ToUpperInvariant();
                if (t.Length > 10) t = t.Substring(0, 10);
                if (t != txtMaDV.Text)
                {
                    txtMaDV.Text = t;
                    txtMaDV.SelectionStart = Math.Min(caret, txtMaDV.Text.Length);
                }
            }
        }

        private void TxtTenDV_TextChanged(object sender, EventArgs e)
        {
            _dirtyInputs = true;
            if (!txtTenDV.Focused) return;
            var caret = txtTenDV.SelectionStart;
            var text = Regex.Replace(txtTenDV.Text, @"\b\p{Ll}", m => m.Value.ToUpper());
            if (text.Length > 70) text = text.Substring(0, 70);
            if (text != txtTenDV.Text)
            {
                txtTenDV.Text = text;
                txtTenDV.SelectionStart = caret <= txtTenDV.Text.Length ? caret : txtTenDV.Text.Length;
            }
        }

        private void TxtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho số, phím điều khiển và dấu phân cách , .
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TxtDonGia_Leave(object sender, EventArgs e)
        {
            decimal v;
            var raw = txtDonGia.Text.Replace(",", "").Replace(".", "");
            if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out v))
            {
                txtDonGia.Text = string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0}", v);
            }
        }

        private void FrmDichVu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dirtyInputs)
            {
                var ans = MessageBox.Show("Bạn có thay đổi chưa lưu. Có muốn lưu trước khi đóng?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (ans == DialogResult.Cancel) { e.Cancel = true; return; }
                if (ans == DialogResult.Yes)
                {
                    if (int.TryParse(txtMaDV.Text.Trim(), out var idTmp)) BtnCapNhat_Click(sender: this, e: EventArgs.Empty);
                    else BtnThem_Click(sender: this, e: EventArgs.Empty);
                }
            }
        }
        // Designer-wired simple wrappers
        private void TxtDonGia_TextChanged(object sender, System.EventArgs e) { _dirtyInputs = true; }
        private void TxtDVT_TextChanged(object sender, System.EventArgs e) { _dirtyInputs = true; }
        private void TxtLuuY_TextChanged(object sender, System.EventArgs e) { _dirtyInputs = true; }

        // Context menu support (single and multi delete)
        private void DeleteOneServiceFromContext(object sender, EventArgs e)
        {
            if (!_isAdmin) { MessageBox.Show("Bạn không có quyền truy cập"); return; }
            if (lvDSDV.SelectedItems.Count == 0)
            { MessageBox.Show("Hãy chọn1 dịch vụ để xóa."); return; }
            int id; if (!int.TryParse(lvDSDV.SelectedItems[0].SubItems[2].Text, out id)) { MessageBox.Show("Không xác định mã dịch vụ"); return; }
            DeleteServicesByIds(new System.Collections.Generic.List<int> { id });
        }
        private void DeleteManyServicesFromContext(object sender, EventArgs e)
        {
            if (!_isAdmin) { MessageBox.Show("Bạn không có quyền truy cập"); return; }
            var ids = new System.Collections.Generic.List<int>();
            foreach (ListViewItem it in lvDSDV.Items)
                if (it.Checked) { int id; if (int.TryParse(it.SubItems[2].Text, out id)) ids.Add(id); }
            if (ids.Count == 0) { MessageBox.Show("Chọn các dịch vụ (checkbox) để xóa nhiều"); return; }
            DeleteServicesByIds(ids);
        }
        private void DeleteServicesByIds(System.Collections.Generic.List<int> ids)
        {
            using (var conn = Db.Open())
            using (var chk = new SqlCommand("SELECT COUNT(1) FROM dbo.ChiTietHD WHERE MaDV=@Id", conn))
            using (var del = new SqlCommand("DELETE dbo.DSDichVu WHERE id=@Id", conn))
            {
                chk.Parameters.Add("@Id", SqlDbType.Int);
                del.Parameters.Add("@Id", SqlDbType.Int);
                foreach (var id in ids.ToList())
                {
                    chk.Parameters["@Id"].Value = id;
                    var used = (int)chk.ExecuteScalar() > 0;
                    if (used)
                    {
                        MessageBox.Show($"Không thể xóa dịch vụ ID {id} vì đang được sử dụng trong hóa đơn.");
                        ids.Remove(id);
                    }
                }
                if (ids.Count == 0) return;
                if (MessageBox.Show($"Bạn có chắc muốn xóa {ids.Count} dịch vụ đã chọn? Dữ liệu không thể khôi phục!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                try
                {
                    foreach (var id in ids)
                    { del.Parameters["@Id"].Value = id; del.ExecuteNonQuery(); }
                    MessageBox.Show("Xóa dịch vụ thành công!");
                    ResetInputs();
                    LoadDanhSachDichVu();
                }
                catch (SqlException ex)
                { MessageBox.Show("Lỗi khi xóa: " + ex.Message); }
            }
        }
    }
}
