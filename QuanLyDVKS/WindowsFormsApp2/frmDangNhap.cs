using BussinessLogic.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;
using WindowsFormsApp2.Security;
using WindowsFormsApp2.Services;

namespace WindowsFormsApp2
{
    public partial class frmDangNhap : Form
    {
        private int _failCount = 0;
        private Timer _unlockTimer;
        private Timer _showPwTimer;
        private CheckBox _chkShowPw;
        private CheckBox _chkRemember;
        private ProgressBar _progress;

        private string RememberPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WindowsFormsApp2", "remember.txt");

        public frmDangNhap()
        {
            InitializeComponent();
            this.Load += FrmDangNhap_Load;
            this.KeyPreview = true;
            this.KeyDown += FrmDangNhap_KeyDown;

            // Keyboard shortcuts
            txtTenTaiKhoan.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtMatKhau.Focus(); }
                else if (e.KeyCode == Keys.Escape) { ClearInputs(); txtTenTaiKhoan.Focus(); }
            };
            txtMatKhau.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnDangNhap.PerformClick(); }
                else if (e.KeyCode == Keys.Escape) { ClearInputs(); txtTenTaiKhoan.Focus(); }
            };

            // Realtime validation
            txtTenTaiKhoan.TextChanged += TxtTenTaiKhoan_TextChanged;
            txtMatKhau.TextChanged += TxtMatKhau_TextChanged;
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            // Discover optional controls
            _chkShowPw = FindCheckBoxByNameOrText("chkShowPassword", "hiển thị mật khẩu");
            _chkRemember = FindCheckBoxByNameOrText("chkRemember", "ghi nhớ đăng nhập");
            _progress = this.Controls.OfType<ProgressBar>().FirstOrDefault();
            if (_progress != null) { _progress.Visible = false; _progress.Style = ProgressBarStyle.Marquee; }

            // Timers
            _unlockTimer = new Timer { Interval = 5 * 60 * 1000 };
            _unlockTimer.Tick += (s, ev) => { _unlockTimer.Stop(); _failCount = 0; btnDangNhap.Enabled = true; };
            _showPwTimer = new Timer { Interval = 5000 };
            _showPwTimer.Tick += (s, ev) => { _showPwTimer.Stop(); if (_chkShowPw != null && _chkShowPw.Checked) _chkShowPw.Checked = false; };

            // DB connection check
            try { using (var c = Db.Open()) { } }
            catch { MessageBox.Show("Lỗi kết nối database", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); btnDangNhap.Enabled = false; }

            // Default UI
            ClearInputs();
            // Thiết lập PasswordChar cho Guna2TextBox (ẩn mật khẩu ban đầu)
            try { txtMatKhau.PasswordChar = '●'; } catch { }
            txtTenTaiKhoan.Focus();

            // Gán toggle hiển thị/ẩn mật khẩu nếu có ToggleSwitch trên form
            try
            {
                if (tsHienThiMatKhau != null)
                {
                    tsHienThiMatKhau.CheckedChanged += (s, ev) =>
                    {
                        txtMatKhau.PasswordChar = tsHienThiMatKhau.Checked ? '\0' : '●';
                    };
                }
            }
            catch { }

            if (_chkShowPw != null)
            {
                _chkShowPw.Checked = false;
                _chkShowPw.CheckedChanged += (s, ev) =>
                {
                    bool show = _chkShowPw.Checked;
                    try { txtMatKhau.PasswordChar = show ? '\0' : '●'; } catch { }
                    if (show) { _showPwTimer.Stop(); _showPwTimer.Start(); } else { _showPwTimer.Stop(); }
                };
            }

            // Load remembered username from file (optional)
            try
            {
                if (File.Exists(RememberPath))
                {
                    var content = File.ReadAllText(RememberPath).Trim();
                    var parts = content.Split('|');
                    if (parts.Length == 2)
                    {
                        bool remember = parts[0] == "1";
                        if (_chkRemember != null) _chkRemember.Checked = remember;
                        if (remember) txtTenTaiKhoan.Text = parts[1];
                    }
                }
            }
            catch { }

            if (!string.IsNullOrWhiteSpace(UserContext.TenTaiKhoan))
            {
                MessageBox.Show("Bạn đã đăng nhập");
                RedirectToMain();
                return;
            }
        }

        private CheckBox FindCheckBoxByNameOrText(string name, string contains)
        {
            var chk = this.Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (chk != null) return chk;
            foreach (Control p in this.Controls)
            {
                chk = p.Controls.OfType<CheckBox>().FirstOrDefault(c => (c.Text ?? string.Empty).IndexOf(contains, StringComparison.OrdinalIgnoreCase) >= 0);
                if (chk != null) return chk;
            }
            return null;
        }

        private void FrmDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { ClearInputs(); txtTenTaiKhoan.Focus(); }
        }

        private void ClearInputs()
        {
            txtTenTaiKhoan.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            var user = txtTenTaiKhoan.Text.Trim();
            var pass = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(user)) { MessageBox.Show("Vui lòng nhập tên đăng nhập"); txtTenTaiKhoan.Focus(); return; }
            if (string.IsNullOrWhiteSpace(pass)) { MessageBox.Show("Vui lòng nhập mật khẩu"); txtMatKhau.Focus(); return; }

            if (_failCount >= 5)
            {
                MessageBox.Show("Bạn đã thử đăng nhập quá nhiều lần. Vui lòng thử lại sau 5 phút.");
                btnDangNhap.Enabled = false;
                if (!_unlockTimer.Enabled) _unlockTimer.Start();
                return;
            }

            if (_progress != null) _progress.Visible = true;
            btnDangNhap.Enabled = false;

            try
            {
                // Use QLNhanVien to authenticate and map to NhanVien
                NhanVien nv = QLNhanVien.Authenticate(user, pass);
                if (nv != null)
                {
                    UserContext.MaTaiKhoan = nv.MaTaiKhoan;
                    UserContext.TenTaiKhoan = nv.TenTaiKhoan;
                    UserContext.Type = nv.Loai; // 1 admin, 0 staff
                    UserContext.SessionId = Guid.NewGuid().ToString("N");
                    UserContext.LoginTime = DateTime.Now;
                    try { UserContext.ClientIp = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault()?.ToString(); } catch { }

                    // Remember username to file (optional)
                    try
                    {
                        if (_chkRemember != null)
                        {
                            var dir = Path.GetDirectoryName(RememberPath);
                            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                            var content = _chkRemember.Checked ? ($"1|{user}") : "0|";
                            File.WriteAllText(RememberPath, content);
                        }
                    }
                    catch { }

                    try { System.Diagnostics.Debug.WriteLine($"User {user} đã đăng nhập thành công lúc {UserContext.LoginTime}"); } catch { }

                    _failCount = 0; _unlockTimer.Stop();
                    if (_progress != null) _progress.Visible = false;
                    RedirectToMain();
                    return;
                }

                _failCount++;
                var remain = Math.Max(0, 5 - _failCount);
                MessageBox.Show(remain > 0 ? $"Sai tên đăng nhập hoặc mật khẩu. Bạn còn {remain} lần thử" : "Sai tên đăng nhập hoặc mật khẩu.");

                int delayMs = _failCount >= 5 ? 30000 : (_failCount == 4 ? 15000 : (_failCount == 3 ? 5000 : 0));
                if (delayMs > 0) await Task.Delay(delayMs);

                txtMatKhau.Clear();
                txtMatKhau.Focus();
                if (_failCount >= 5) { btnDangNhap.Enabled = false; if (!_unlockTimer.Enabled) _unlockTimer.Start(); }
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối database. Vui lòng kiểm tra kết nối.");
            }
            finally
            {
                if (_progress != null) _progress.Visible = false;
                if (_failCount < 5) btnDangNhap.Enabled = true;
            }
        }

        private void RedirectToMain()
        {
            this.Hide();
            using (var main = new frmMain())
            {
                main.ShowDialog();
            }
            this.Close();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            var user = txtTenTaiKhoan.Text.Trim();
            if (string.IsNullOrWhiteSpace(user)) { MessageBox.Show("Vui lòng nhập tên đăng nhập trước"); txtTenTaiKhoan.Focus(); return; }
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.TaiKhoan WHERE TenTaiKhoan=@u", conn))
                { cmd.Parameters.AddWithValue("@u", user); if ((int)cmd.ExecuteScalar() == 0) { MessageBox.Show("Tên đăng nhập không tồn tại"); return; } }
            }
            catch { MessageBox.Show("Không thể kết nối database. Vui lòng kiểm tra kết nối mạng."); return; }

            var frm = new frmQuenMatKhau(this) { Tag = user };
            this.Hide();
            frm.Show();
            frm.FormClosed += (s, args) => this.Show();
        }

        private void TxtTenTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            // Allowed characters [A-Za-z0-9_]
            var caret = txtTenTaiKhoan.SelectionStart;
            var v = txtTenTaiKhoan.Text;
            var filtered = System.Text.RegularExpressions.Regex.Replace(v, @"[^A-Za-z0-9_]", "");
            if (filtered != v) { txtTenTaiKhoan.Text = filtered; txtTenTaiKhoan.SelectionStart = Math.Min(caret, filtered.Length); }


        }

        private void TxtMatKhau_TextChanged(object sender, EventArgs e)
        {
            var pwd = txtMatKhau.Text;
            bool strong = pwd.Length > 8 && System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[A-Z]") && System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[a-z]") && System.Text.RegularExpressions.Regex.IsMatch(pwd, @"\d") && System.Text.RegularExpressions.Regex.IsMatch(pwd, @"[^\da-zA-Z]");
            bool medium = !strong && pwd.Length >= 6;
            txtMatKhau.BackColor = string.IsNullOrEmpty(pwd) ? System.Drawing.Color.White : (strong ? System.Drawing.Color.Honeydew : (medium ? System.Drawing.Color.LemonChiffon : System.Drawing.Color.MistyRose));
        }
    }
}
