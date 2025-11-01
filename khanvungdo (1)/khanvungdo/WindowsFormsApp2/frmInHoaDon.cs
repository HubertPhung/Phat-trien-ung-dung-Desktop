using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public partial class frmInHoaDon : Form
    {
        private readonly int _billId;
        private DataTable _dsChiTiet = new DataTable();
        private decimal _tong = 0m, _giam = 0m, _thanh = 0m;
        private PrintDocument _printDoc;
        private string _tenKS = "Khách sạn ABC", _diaChiKS = "Địa chỉ ...", _mst = "MST: ...";
        private int _roomId = 0;
        private bool _isPaid = false;

        public frmInHoaDon()
        {
            InitializeComponent();
            WireButtons();
            dgvChiTiet.CellDoubleClick += DgvChiTiet_CellDoubleClick;
        }

        public frmInHoaDon(int billId) : this()
        {
            _billId = billId;
            Load += (s, e) => { LoadEnterpriseInfo(); LoadData(); };
        }

        private void WireButtons()
        {
            btnDong.Click += BtnDong_Click;
            btnIn.Click += (s, e) => PrintInvoiceWithOptions();
        }

        private void LoadEnterpriseInfo()
        {
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("SELECT TOP 1 TenKS, DiaChi, MST FROM dbo.ThongTinDoanhNghiep", conn))
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        _tenKS = Convert.ToString(rd["TenKS"]);
                        _diaChiKS = Convert.ToString(rd["DiaChi"]);
                        _mst = "MST: " + Convert.ToString(rd["MST"]);
                    }
                }
            }
            catch { /* fallback defaults */ }
        }

        private void LoadData()
        {
            if (_billId <= 0)
            {
                MessageBox.Show("Hóa đơn không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand(@"SELECT hd.id, hd.NgayLapHD, hd.NgayKetThucHD, hd.GiamGia, hd.TongTien, hd.ThanhTien, hd.MaPhong, hd.status,
                                                     p.TenPhong, kh.TenKH, kh.SDT
                                              FROM dbo.HoaDon hd
                                              JOIN dbo.Phong p ON p.id = hd.MaPhong
                                              LEFT JOIN dbo.KhachHang kh ON kh.id = p.MaKH
                                              WHERE hd.id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _billId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read())
                        {
                            MessageBox.Show("Hóa đơn không tồn tại trong hệ thống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        lblSoHD.Text = $"Số HĐ: {rd["id"]}";
                        lblNgayLap.Text = $"Ngày lập: {Convert.ToDateTime(rd["NgayLapHD"]).ToString("dd/MM/yyyy HH:mm")}";
                        label1.Text = rd["NgayKetThucHD"] == DBNull.Value ? "" : $"Ngày kết thúc: {Convert.ToDateTime(rd["NgayKetThucHD"]).ToString("dd/MM/yyyy HH:mm")}";
                        lblKhachHang.Text = $"Khách hàng: {rd["TenKH"]}";
                        lblSDT.Text = $"SĐT: {rd["SDT"]}";
                        lblSoPhong.Text = $"Số phòng: {rd["TenPhong"]}";
                        _roomId = Convert.ToInt32(rd["MaPhong"]);
                        _isPaid = Convert.ToInt32(rd["status"]) == 1;
                        _tong = Convert.ToDecimal(rd["TongTien"]);
                        _giam = Convert.ToDecimal(rd["GiamGia"]);
                        _thanh = Convert.ToDecimal(rd["ThanhTien"]);
                        lblTongCong.Text = $"Tổng cộng: {_tong:N0} VND";
                        lblGiamGia.Text = $"Giảm giá: {_giam:N0}%";
                        lblThanhTien.Text = $"THÀNH TIỀN: {_thanh:N0} VND";
                    }
                }

                _dsChiTiet = new DataTable();
                _dsChiTiet.Columns.Add("TenDV", typeof(string));
                _dsChiTiet.Columns.Add("DVT", typeof(string));
                _dsChiTiet.Columns.Add("SoLuong", typeof(int));
                _dsChiTiet.Columns.Add("DonGia", typeof(decimal));
                _dsChiTiet.Columns.Add("ThanhTien", typeof(decimal));

                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_XuatDS_IDHoaDon", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@billID", _billId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var row = _dsChiTiet.NewRow();
                            row["TenDV"] = rd["TenDV"].ToString();
                            row["DVT"] = rd["DVT"].ToString();
                            row["SoLuong"] = Convert.ToInt32(rd["SoLuong"]);
                            row["DonGia"] = Convert.ToDecimal(rd["DonGia"]);
                            row["ThanhTien"] = Convert.ToDecimal(rd["ThanhTien"]);
                            _dsChiTiet.Rows.Add(row);
                        }
                    }
                }

                dgvChiTiet.AutoGenerateColumns = true;
                dgvChiTiet.DataSource = _dsChiTiet;
                dgvChiTiet.ReadOnly = true;

                if (_isPaid)
                {
                    MessageBox.Show("Hóa đơn đã thanh toán, không thể chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dgvChiTiet.Rows[e.RowIndex];
            var ten = Convert.ToString(r.Cells["TenDV"].Value);
            var dvt = Convert.ToString(r.Cells["DVT"].Value);
            var dongia = Convert.ToDecimal(r.Cells["DonGia"].Value);

            int dvId = 0;
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand("SELECT TOP 1 id, (SELECT name FROM LoaiDichVu WHERE id = idLoaiDichVu) AS Loai, LuuY FROM DSDichVu WHERE TenDV=@Ten AND DVT=@DVT AND DonGia=@Gia", conn))
            {
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@DVT", dvt);
                cmd.Parameters.AddWithValue("@Gia", dongia);
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        dvId = Convert.ToInt32(rd["id"]);
                        var loai = Convert.ToString(rd["Loai"]);
                        var luuy = rd["LuuY"] == DBNull.Value ? null : Convert.ToString(rd["LuuY"]);
                        var f = new frmChiTietDV(dvId, ten, loai, dongia, dvt, luuy, _roomId, _billId, null, null)
                        {
                            StartPosition = FormStartPosition.CenterParent
                        };
                        // View only
                        try
                        {
                            var prop = typeof(frmChiTietDV).GetField("btnDat", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                            if (prop?.GetValue(f) is Control btn) btn.Enabled = false;
                        }
                        catch { }
                        f.ShowDialog(this);
                    }
                }
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            if (_isPaid)
            {
                this.Close();
                return;
            }
            var confirm = MessageBox.Show("Bạn có chắc muốn đóng hóa đơn này? Hóa đơn sẽ được chuyển sang trạng thái đã thanh toán.",
                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            try
            {
                using (var conn = Db.Open())
                using (var cmd = new SqlCommand("USP_DienHoaDon", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@billID", _billId);
                    cmd.Parameters.AddWithValue("@giamgia", _giam);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Đã đóng hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var t = new Timer { Interval = 3000 };
                t.Tick += (s, ev) => { t.Stop(); this.DialogResult = DialogResult.OK; this.Close(); };
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đóng hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintInvoiceWithOptions()
        {
            if (_printDoc == null)
            {
                _printDoc = new PrintDocument();
                _printDoc.DocumentName = $"HoaDon_{_billId}";
                _printDoc.PrintPage += PrintDoc_PrintPage;
            }

            using (var pd = new PrintDialog { Document = _printDoc, UseEXDialog = true, AllowSomePages = false })
            {
                if (pd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        using (var preview = new PrintPreviewDialog { Document = _printDoc, Width = 1200, Height = 800 })
                        {
                            preview.ShowDialog(this);
                        }

                        _printDoc.Print();
                        MessageBox.Show("In hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi máy in, vui lòng kiểm tra kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float left = e.MarginBounds.Left;
            float top = e.MarginBounds.Top;
            float right = e.MarginBounds.Right;
            var g = e.Graphics;

            using (var fontTitle = new Font("Arial", 16, FontStyle.Bold))
            using (var font = new Font("Arial", 10))
            using (var fontBold = new Font("Arial", 10, FontStyle.Bold))
            {
                // Header doanh nghiệp
                g.DrawString(_tenKS, fontBold, Brushes.Black, left, top);
                g.DrawString(_diaChiKS, font, Brushes.Black, left, top + 16);
                g.DrawString(_mst, font, Brushes.Black, left, top + 32);

                // Tiêu đề
                var title = "HÓA ĐƠN THANH TOÁN";
                var sizeTitle = g.MeasureString(title, fontTitle);
                g.DrawString(title, fontTitle, Brushes.Black, left + (e.MarginBounds.Width - sizeTitle.Width) / 2, top + 50);
                float y = top + 50 + sizeTitle.Height + 10;

                // Thông tin hóa đơn
                g.DrawString(lblSoHD.Text, font, Brushes.Black, left, y);
                g.DrawString(lblNgayLap.Text, font, Brushes.Black, right - 280, y);
                y += 20;
                g.DrawString(lblKhachHang.Text, font, Brushes.Black, left, y);
                g.DrawString(lblSoPhong.Text, font, Brushes.Black, right - 280, y);
                y += 20;
                g.DrawString(lblSDT.Text, font, Brushes.Black, left, y);
                if (!string.IsNullOrWhiteSpace(label1.Text)) g.DrawString(label1.Text, font, Brushes.Black, right - 280, y);
                y += 30;

                // Bảng chi tiết
                float colTen = left;
                float colDVT = left + 250;
                float colSL = left + 350;
                float colGia = left + 420;
                float colTT = left + 520;
                g.DrawString("Tên dịch vụ", fontBold, Brushes.Black, colTen, y);
                g.DrawString("DVT", fontBold, Brushes.Black, colDVT, y);
                g.DrawString("SL", fontBold, Brushes.Black, colSL, y);
                g.DrawString("Đơn giá", fontBold, Brushes.Black, colGia, y);
                g.DrawString("Thành tiền", fontBold, Brushes.Black, colTT, y);
                y += 20;
                g.DrawLine(Pens.Black, left, y, right, y);
                y += 5;

                foreach (DataRow r in _dsChiTiet.Rows)
                {
                    g.DrawString(Convert.ToString(r["TenDV"]), font, Brushes.Black, colTen, y);
                    g.DrawString(Convert.ToString(r["DVT"]), font, Brushes.Black, colDVT, y);
                    g.DrawString(Convert.ToString(r["SoLuong"]), font, Brushes.Black, colSL, y);
                    g.DrawString(string.Format("{0:N0}", r["DonGia"]), font, Brushes.Black, colGia, y);
                    g.DrawString(string.Format("{0:N0}", r["ThanhTien"]), font, Brushes.Black, colTT, y);
                    y += 20;
                }

                y += 10;
                g.DrawLine(Pens.Black, left, y, right, y);
                y += 10;

                g.DrawString(lblTongCong.Text, font, Brushes.Black, left, y); y += 20;
                g.DrawString(lblGiamGia.Text, font, Brushes.Black, left, y); y += 20;
                g.DrawString(lblThanhTien.Text, fontBold, Brushes.Red, left, y);

                // Footer cảm ơn
                y += 40;
                g.DrawString("Cảm ơn quý khách đã sử dụng dịch vụ!", font, Brushes.Black, left, y);
            }
        }
    }
}
