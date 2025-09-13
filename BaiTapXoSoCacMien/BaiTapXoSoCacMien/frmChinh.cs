using BaiTapXoSoCacMien;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiTapSoXoCacMien
{
    public partial class frmChinh : Form
    {
        private readonly RssService _rss;

        public frmChinh()
        {
            InitializeComponent();
            _rss = new RssService();
        }

        private void frmChinh_Load(object sender, EventArgs e)
        {
            cboMien.Items.Clear();
            cboMien.Items.Add("Miền Bắc");
            cboMien.Items.Add("Miền Trung");
            cboMien.Items.Add("Miền Nam");
            cboMien.SelectedIndex = 0;

            dgvKetQua.Columns.Clear();
            dgvKetQua.Columns.Add("Giai", "Giải");
            dgvKetQua.Columns.Add("KetQua", "Kết quả");

            dgvKetQua.Columns[0].Width = 120;
            dgvKetQua.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvKetQua.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            dgvKetQua.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        }

        private string LayUrlTheoMien()
        {
            var key = cboMien.SelectedItem?.ToString() ?? "Miền Bắc";
            switch (key)
            {
                case "Miền Bắc":
                    return "https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";
                case "Miền Trung":
                    return "https://xskt.com.vn/rss-feed/mien-trung-xsmt.rss";
                case "Miền Nam":
                    return "https://xskt.com.vn/rss-feed/mien-nam-xsmn.rss";
                default:
                    return "https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";
            }
        }

        private async void btnLayKetQua_Click(object sender, EventArgs e)
        {
            string url = LayUrlTheoMien();
            var selectedDate = dtpNgay.Value.Date;

            try
            {
                var allResults = await _rss.GetAllLotteryResultsAsync(url);

                var availableDates = allResults
                    .Select(x =>
                    {
                        DateTime d;
                        if (TryParsePubDateToVnDate(x.NgayCongBo, out d) || TryParseDateInTitle(x.TieuDe, out d))
                            return (DateTime?)d.Date;
                        return null;
                    })
                    .Where(d => d.HasValue)
                    .Select(d => d.Value)
                    .Distinct()
                    .OrderBy(d => d)
                    .ToList();

                if (!availableDates.Any())
                {
                    MessageBox.Show("Nguồn RSS không có mục nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvKetQua.Rows.Clear();
                    return;
                }

                var min = availableDates.First();
                var max = availableDates.Last();

                if (dtpNgay.Value.Date < min)
                {
                    dtpNgay.Value = min;
                    selectedDate = min;
                }
                else if (dtpNgay.Value.Date > max)
                {
                    dtpNgay.Value = max;
                    selectedDate = max;
                }

                dtpNgay.MinDate = min;
                dtpNgay.MaxDate = max;

                Console.WriteLine($"Ngày người dùng chọn: {selectedDate:dd/MM/yyyy}");
                foreach (var kq in allResults)
                {
                    DateTime d;
                    if (TryParsePubDateToVnDate(kq.NgayCongBo, out d) || TryParseDateInTitle(kq.TieuDe, out d))
                    {
                        Console.WriteLine($"RSS -> {kq.TieuDe} | Ngày (VN): {d:dd/MM/yyyy}");
                    }
                    else
                    {
                        Console.WriteLine($"LỖI PARSE pubDate: {kq.NgayCongBo}");
                    }
                }

                var ketQuaChon = allResults.FirstOrDefault(x =>
                {
                    DateTime d;
                    if (TryParsePubDateToVnDate(x.NgayCongBo, out d)) return d.Date == selectedDate;
                    if (TryParseDateInTitle(x.TieuDe, out d)) return d.Date == selectedDate;
                    return false;
                });

                if (ketQuaChon == null)
                {
                    MessageBox.Show(
                        $"Không có dữ liệu cho ngày {selectedDate:dd/MM/yyyy}.\nKhoảng ngày có sẵn trong RSS: {min:dd/MM/yyyy} - {max:dd/MM/yyyy}.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvKetQua.Rows.Clear();
                    return;
                }

                lblTitle.Text = ketQuaChon.TieuDe;
                string cleaned = CleanDescription(ketQuaChon.NoiDung);
                HienThiKetQua(cleaned);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kết quả: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static readonly string[] Rfc1123Variants = new[]
        {
            "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'",
            "ddd, dd MMM yyyy HH':'mm':'ss zzz",
            "ddd, dd MMM yyyy HH':'mm':'ss K"
        };

        private static TimeZoneInfo GetVnTimeZone()
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); }
            catch { return TimeZoneInfo.Local; }
        }

        private static readonly TimeZoneInfo VnTz = GetVnTimeZone();

        private static bool TryParsePubDateToVnDate(string pubDate, out DateTime vnDate)
        {
            vnDate = default(DateTime);
            if (string.IsNullOrWhiteSpace(pubDate)) return false;

            DateTimeOffset dto;
            if (DateTimeOffset.TryParse(pubDate, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dto))
            {
                vnDate = TimeZoneInfo.ConvertTime(dto.UtcDateTime, VnTz).Date;
                return true;
            }

            foreach (var fmt in Rfc1123Variants)
            {
                if (DateTimeOffset.TryParseExact(pubDate, fmt, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dto))
                {
                    vnDate = TimeZoneInfo.ConvertTime(dto.UtcDateTime, VnTz).Date;
                    return true;
                }
            }
            return false;
        }

        private static bool TryParseDateInTitle(string title, out DateTime date)
        {
            date = default(DateTime);
            if (string.IsNullOrWhiteSpace(title)) return false;

            var m = Regex.Match(title, @"(?<d>\d{1,2})[-/](?<m>\d{1,2})[-/](?<y>\d{4})");
            if (m.Success)
            {
                int d = int.Parse(m.Groups["d"].Value);
                int mm = int.Parse(m.Groups["m"].Value);
                int y = int.Parse(m.Groups["y"].Value);
                date = new DateTime(y, mm, d);
                return true;
            }
            return false;
        }

        private string CleanDescription(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return string.Empty;

            html = html.Replace("<br/>", "\n").Replace("<br />", "\n").Replace("<br>", "\n");

            html = Regex.Replace(html, "<.*?>", string.Empty);

            html = WebUtility.HtmlDecode(html);

            html = html.Replace("\r\n", "\n").Replace("\r", "\n");

            var lines = html.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(l => l.Trim())
                            .Where(l => !string.IsNullOrWhiteSpace(l));

            return string.Join("\n", lines);
        }

        private void HienThiKetQua(string noiDungDaLamSach)
        {
            dgvKetQua.Rows.Clear();
            if (string.IsNullOrWhiteSpace(noiDungDaLamSach)) return;

            var danhSachTinh = Regex.Split(noiDungDaLamSach, @"(?=\[[^\]]+\])")
                                    .Where(s => !string.IsNullOrWhiteSpace(s));

            foreach (var tinh in danhSachTinh)
            {
                var layTenTinh = Regex.Match(tinh, @"\[(.*?)\]");
                string tenTinh = layTenTinh.Success ? layTenTinh.Groups[1].Value.Trim() : "Không rõ tỉnh";
                string duLieu = layTenTinh.Success ? tinh.Substring(layTenTinh.Length).Trim() : tinh.Trim();

                int dongTinh = dgvKetQua.Rows.Add(tenTinh, "");
                dgvKetQua.Rows[dongTinh].DefaultCellStyle.Font = new Font(dgvKetQua.Font, FontStyle.Bold);
                dgvKetQua.Rows[dongTinh].DefaultCellStyle.BackColor = Color.LightYellow;

                var cacGiai = Regex.Matches(duLieu, @"(?i:(?:ĐB|DB|Đặc Biệt)|[1-8])\s*:");
                var dict = new Dictionary<string, string>();

                for (int i = 0; i < cacGiai.Count; i++)
                {
                    string tenGiai = cacGiai[i].Value.TrimEnd(':').Trim();
                    int batDau = cacGiai[i].Index + cacGiai[i].Length;
                    int ketThuc = (i + 1 < cacGiai.Count) ? cacGiai[i + 1].Index : duLieu.Length;
                    string giaTri = duLieu.Substring(batDau, ketThuc - batDau).Trim();

                    var tachGiai7_8 = Regex.Match(giaTri, @"^(?<g7>\d+?)\s*8:\s*(?<g8>[\d\s\-]+)$");
                    if (tenGiai == "7" && tachGiai7_8.Success)
                    {
                        dict["Giải 7"] = tachGiai7_8.Groups["g7"].Value.Trim();
                        dict["Giải 8"] = tachGiai7_8.Groups["g8"].Value.Trim();
                    }
                    else
                    {
                        dict[tenGiai] = giaTri;
                    }
                }

                foreach (var muc in dict)
                {
                    int dong = dgvKetQua.Rows.Add(muc.Key, muc.Value);
                    var row = dgvKetQua.Rows[dong];

                    if (muc.Key.Equals("ĐB", StringComparison.OrdinalIgnoreCase) ||
                        muc.Key.Equals("DB", StringComparison.OrdinalIgnoreCase) ||
                        muc.Key.Equals("Đặc Biệt", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font(dgvKetQua.Font, FontStyle.Bold);
                    }
                }
            }
        }
    }
}
