using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess.OL;

namespace RestaurantManagementProject
{
    public partial class FormDatBan : Form
    {
        // ====== Business Logic ======
        private readonly BanBL _banBL = new BanBL();
        private readonly FoodBL _foodBL = new FoodBL();
        private readonly CategoryBL _catBL = new CategoryBL();
        private readonly BillsBL _billsBL = new BillsBL();
        private readonly BillDetailsBL _detailsBL = new BillDetailsBL();

        // ====== Trạng thái trong Form ======
        private bool _loadingCbb = false;

        private List<Ban> _allTables = new List<Ban>();
        private List<Food> _allFoods = new List<Food>();
        private List<Category> _allCats = new List<Category>();

        private int _selectedTableId = 0;
        private Ban _selectedBan = null;

        private int _currentBillId = 0;          // Bill đang mở (Status=0) cho bàn đã chọn
        private string _currentUser = "system";  // Tuỳ bạn đưa AccountName thực vào đây

        public FormDatBan()
        {
            InitializeComponent();

            InitFoodsGrid();          // DataGridView cho danh sách món (có SL)
            InitBillItemsListView();  // ListView hiển thị món theo bàn

            LoadTablesToFLP();
            LoadCategoriesToCbb();
            LoadFoodsToGrid();        // mặc định: tất cả
            RefreshTongTien();
        }

        public FormDatBan(string accountName) : this()
        {
            if (!string.IsNullOrWhiteSpace(accountName))
                _currentUser = accountName;
        }

        // ====== Khởi tạo DataGridView (dgvDSMonAn) ======
        private void InitFoodsGrid()
        {
            dgvDSMonAn.AutoGenerateColumns = false;
            dgvDSMonAn.AllowUserToAddRows = false;
            dgvDSMonAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDSMonAn.MultiSelect = false;
            dgvDSMonAn.RowHeadersVisible = false;
            dgvDSMonAn.Columns.Clear();

            var colChk = new DataGridViewCheckBoxColumn { Name = "Chon", HeaderText = "", Width = 32 };

            var colName = new DataGridViewTextBoxColumn { Name = "TenMon", HeaderText = "Tên món", ReadOnly = true, Width = 200 };

            var colUnit = new DataGridViewTextBoxColumn { Name = "DVT", HeaderText = "ĐVT", ReadOnly = true, Width = 80 };

            var colPrice = new DataGridViewTextBoxColumn
            {
                Name = "Gia",
                HeaderText = "Giá",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            // Ghi chú (NEW)
            var colNotes = new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                ReadOnly = true,
                Width = 200
            };

            var colQty = new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "SL",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            var colId = new DataGridViewTextBoxColumn { Name = "FoodID", HeaderText = "FoodID", Visible = false };

            dgvDSMonAn.Columns.AddRange(colChk, colName, colUnit, colPrice, colNotes, colQty, colId);

            // Validate SL
            dgvDSMonAn.CellValidating += (s, e) =>
            {
                if (dgvDSMonAn.Columns[e.ColumnIndex].Name == "SoLuong")
                {
                    var value = e.FormattedValue?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(value) || !int.TryParse(value, out int qty) || qty < 1)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Số lượng phải là số nguyên ≥ 1.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            };

            // Default SL = 1
            dgvDSMonAn.RowsAdded += (s, e) =>
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    var row = dgvDSMonAn.Rows[e.RowIndex + i];
                    if (row.Cells["SoLuong"].Value == null)
                        row.Cells["SoLuong"].Value = 1;
                }
            };
        }

        // ====== Khởi tạo ListView món theo bàn ======
        private void InitBillItemsListView()
        {
            lvMonAnTheoBan.View = View.Details;
            lvMonAnTheoBan.FullRowSelect = true;
            lvMonAnTheoBan.GridLines = true;
            lvMonAnTheoBan.CheckBoxes = false;
            lvMonAnTheoBan.Columns.Clear();

            lvMonAnTheoBan.Columns.Add("Tên món", 200);                      // index 0
            lvMonAnTheoBan.Columns.Add("ĐVT", 80);                           // index 1
            lvMonAnTheoBan.Columns.Add("Giá", 100, HorizontalAlignment.Right); // index 2
            lvMonAnTheoBan.Columns.Add("Ghi chú", 200);                      // index 3
            lvMonAnTheoBan.Columns.Add("Số lượng", 80, HorizontalAlignment.Center); // index 4
        }

        // ====== Load bàn ra FlowLayoutPanel ======
        private void LoadTablesToFLP()
        {
            flpDSBan.Controls.Clear();
            _allTables = _banBL.GetAll();

            foreach (var t in _allTables)
            {
                var btn = new Button();
                btn.Text = t.Name ?? ("Bàn " + t.ID);
                btn.Tag = t; // giữ cả object
                btn.Width = 90;
                btn.Height = 60;
                btn.Margin = new Padding(6);

                // màu theo trạng thái
                btn.BackColor = (t.Status == 1)
                    ? Color.LightSalmon   // đang dùng
                    : Color.LightGreen;   // trống

                btn.Click += OnTableButtonClick;
                flpDSBan.Controls.Add(btn);
            }
        }

        private void OnTableButtonClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            _selectedBan = btn?.Tag as Ban;
            if (_selectedBan == null) return;

            _selectedTableId = _selectedBan.ID;
            lblSoBan.Text = _selectedBan.Name ?? ("Bàn " + _selectedBan.ID);

            // tìm bill đang mở (Status=false/0) cho bàn này
            var openBill = _billsBL.GetOpenByTable(_selectedTableId)
                                   .OrderByDescending(b => b.ID)
                                   .FirstOrDefault();

            _currentBillId = openBill?.ID ?? 0;

            // load chi tiết bill nếu có
            LoadBillDetailsToListView();
            RefreshTongTien();
        }

        private void LoadBillDetailsToListView()
        {
            lvMonAnTheoBan.Items.Clear();
            if (_currentBillId == 0) return;

            var allDetails = _detailsBL.GetByBill(_currentBillId);
            if (_allFoods.Count == 0) _allFoods = _foodBL.GetAll();

            foreach (var d in allDetails)
            {
                var food = _allFoods.FirstOrDefault(f => f.ID == d.FoodID);
                if (food == null) continue;

                var it = new ListViewItem(food.Name);
                it.SubItems.Add(food.Unit);
                it.SubItems.Add(food.Price.ToString());
                it.SubItems.Add(food.Notes ?? "");
                it.SubItems.Add(d.Quantity.ToString());
                it.Tag = food.ID;
                lvMonAnTheoBan.Items.Add(it);
            }
        }

        // ====== Load Category vào ComboBox (có "Tất cả") ======
        private void LoadCategoriesToCbb()
        {
            _loadingCbb = true;

            _allCats = _catBL.GetAll();
            var src = new List<Category>();
            src.Add(new Category { ID = 0, Name = "Tất cả", Type = 0 }); // hàng UI
            src.AddRange(_allCats);

            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";
            cbbCategory.DataSource = src;

            cbbCategory.SelectedValue = 0;

            _loadingCbb = false;
        }

        // ====== Load Food theo Category vào DataGridView ======
        private void LoadFoodsToGrid(int categoryId = 0)
        {
            dgvDSMonAn.Rows.Clear();
            _allFoods = _foodBL.GetAll();

            var foods = (categoryId == 0) ? _allFoods
                : _allFoods.Where(f => f.FoodCategoryID == categoryId).ToList();

            foreach (var f in foods)
            {
                // Chọn, Tên, ĐVT, Giá, Ghi chú, SL=1, FoodID
                dgvDSMonAn.Rows.Add(false, f.Name, f.Unit, f.Price, f.Notes ?? "", 1, f.ID);
            }
        }

        // ====== Thêm món từ dgvDSMonAn (đã tick) sang bill của bàn ======
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == 0)
            {
                MessageBox.Show("Hãy chọn bàn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // nếu chưa có bill đang mở → tạo bill mới
            if (_currentBillId == 0)
            {
                var newBill = new Bills
                {
                    ID = 0,
                    Name = $"Bill_Ban_{_selectedTableId}_{DateTime.Now:yyyyMMddHHmmss}",
                    TableID = _selectedTableId,
                    Amount = 0,
                    Discount = null,
                    Tax = null,
                    Status = false, // đang mở
                    CheckoutDate = null,
                    Account = _currentUser
                };
                _currentBillId = _billsBL.Insert(newBill);

                // set bàn đang dùng
                if (_selectedBan != null)
                {
                    _selectedBan.Status = 1;
                    _banBL.Update(_selectedBan); // update bàn: Status=1
                    LoadTablesToFLP(); // refresh màu
                }
            }

            bool any = false;

            // Duyệt các dòng được tick
            foreach (DataGridViewRow row in dgvDSMonAn.Rows)
            {
                bool chon = row.Cells["Chon"].Value is bool b && b;
                if (!chon) continue;
                any = true;

                int foodId = Convert.ToInt32(row.Cells["FoodID"].Value);
                int price = Convert.ToInt32(row.Cells["Gia"].Value);
                int qty = 1;

                var cellQty = row.Cells["SoLuong"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(cellQty) && int.TryParse(cellQty, out int q) && q > 0)
                    qty = q;

                // Ghi DB (BillDetails)
                var detail = new BillDetails
                {
                    ID = 0,
                    InvoiceID = _currentBillId,
                    FoodID = foodId,
                    Quantity = qty
                };
                _detailsBL.Insert(detail); // insert

                // Cập nhật UI (lvMonAnTheoBan)
                var food = _allFoods.FirstOrDefault(f => f.ID == foodId);
                if (food != null)
                {
                    var line = new ListViewItem(food.Name);
                    line.SubItems.Add(food.Unit);
                    line.SubItems.Add(price.ToString());
                    line.SubItems.Add(food.Notes ?? "");
                    line.SubItems.Add(qty.ToString());
                    line.Tag = food.ID;
                    lvMonAnTheoBan.Items.Add(line);
                }
                // Bỏ chọn + reset SL tránh add trùng
                row.Cells["Chon"].Value = false;
                row.Cells["SoLuong"].Value = 1;
            }

            if (!any)
            {
                MessageBox.Show("Hãy tick chọn món cần thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RefreshTongTien();
        }

        // ====== Tính tổng tiền từ lvMonAnTheoBan ======
        private void RefreshTongTien()
        {
            long total = 0;
            foreach (ListViewItem it in lvMonAnTheoBan.Items)
            {
                int price = 0;
                int qty = 0;

                // Giá ở SubItems[2], Số lượng ở SubItems[4] (vì có cột Ghi chú ở index 3)
                int.TryParse(it.SubItems[2].Text, out price);
                int.TryParse(it.SubItems[4].Text, out qty);

                total += (long)price * qty;
            }
            txtTongTien.Text = total.ToString("#,0");
        }

        // ====== CBB Category thay đổi ======
        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingCbb) return;
            if (cbbCategory.SelectedValue == null) return;

            int catId;
            if (!int.TryParse(cbbCategory.SelectedValue.ToString(), out catId))
                catId = 0;

            LoadFoodsToGrid(catId);
        }

        // ====== Reset CBB về "Tất cả" ======
        private void btnResetCBB_Click(object sender, EventArgs e)
        {
            _loadingCbb = true;
            cbbCategory.SelectedValue = 0;
            _loadingCbb = false;
            LoadFoodsToGrid(0);
        }

        // ====== Thanh toán ======
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == 0 || _currentBillId == 0)
            {
                MessageBox.Show("Chưa có bill đang mở cho bàn này.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form InHoaDon (modal). Truyền vào BillId và thông tin bàn.
            using (var f = new InHoaDon(_currentBillId, _selectedTableId, _selectedBan?.Name ?? ("Bàn " + _selectedTableId)))
            {
                var dialogResult = f.ShowDialog(this);

                // Nếu trong form đã hoàn thành (đã cập nhật DB)
                if (f.DaHoanThanh)
                {
                    // Dọn UI & refresh
                    _currentBillId = 0;
                    lvMonAnTheoBan.Items.Clear();
                    RefreshTongTien();
                    LoadTablesToFLP();
                    MessageBox.Show("Thanh toán thành công.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ====== (Optional) Double-click tăng SL trong bill ======
        private void lvMonAnTheoBan_DoubleClick(object sender, EventArgs e)
        {
            if (lvMonAnTheoBan.SelectedItems.Count == 0 || _currentBillId == 0) return;

            var it = lvMonAnTheoBan.SelectedItems[0];
            if (it.Tag == null) return;

            int foodId = (int)it.Tag;

            // SL ở SubItems[4]
            int currentQty = int.Parse(it.SubItems[4].Text);
            int newQty = currentQty + 1;
            it.SubItems[4].Text = newQty.ToString();

            // cập nhật DB (tìm detail trước)
            var detail = _detailsBL.GetByBill(_currentBillId)
                .Where(d => d.FoodID == foodId)
                .OrderByDescending(d => d.ID).FirstOrDefault();

            if (detail != null)
            {
                detail.Quantity = newQty;
                _detailsBL.Update(detail);
            }

            RefreshTongTien();
        }

        // ====== Xoá món khỏi bill (context menu) ======
        private void cmsXoa_Click(object sender, EventArgs e)
        {
            if (lvMonAnTheoBan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần xoá.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xoá món đã chọn?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Nếu chưa có bill mở, chỉ cần xoá khỏi UI
            if (_currentBillId == 0)
            {
                foreach (ListViewItem it in lvMonAnTheoBan.SelectedItems)
                    lvMonAnTheoBan.Items.Remove(it);

                RefreshTongTien();
                return;
            }

            // Lấy toàn bộ detail của bill đang mở để đối chiếu và xoá
            var billDetails = _detailsBL.GetByBill(_currentBillId).ToList();

            foreach (ListViewItem it in lvMonAnTheoBan.SelectedItems)
            {
                if (it.Tag == null) continue;
                int foodId = (int)it.Tag;

                // SL ở SubItems[4]
                int qty = 1;
                int.TryParse(it.SubItems[4].Text, out qty);

                // Tìm đúng bản ghi BillDetails để xoá:
                var toDelete = billDetails
                    .Where(d => d.FoodID == foodId && d.Quantity == qty)
                    .OrderByDescending(d => d.ID)
                    .FirstOrDefault()
                    ?? billDetails
                        .Where(d => d.FoodID == foodId)
                        .OrderByDescending(d => d.ID)
                        .FirstOrDefault();

                if (toDelete != null)
                {
                    _detailsBL.Delete(new BillDetails
                    {
                        ID = toDelete.ID,
                        InvoiceID = toDelete.InvoiceID,
                        FoodID = toDelete.FoodID,
                        Quantity = toDelete.Quantity
                    });

                    billDetails.Remove(toDelete);
                }

                lvMonAnTheoBan.Items.Remove(it);
            }

            RefreshTongTien();
        }

        // ====== Handler trống do Designer sinh ra (nếu còn wire trong Designer) ======
        private void flpDSBan_Paint(object sender, PaintEventArgs e) { }
        private void lvMonAnTheoBan_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblSoBan_Click(object sender, EventArgs e) { }
        private void lvDSMonAn_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void btnInHoaDon_Click(object sender, EventArgs e) { /* in hoá đơn: tuỳ bạn triển khai */ }

        private void dgvDSMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            using (var f = new frmFood())
            {
                var dialog = f.ShowDialog(this);

                if (dialog == DialogResult.OK)
                {
                    if (f.FoodsChanged)
                        LoadFoodsToGrid();     // món có đổi → reload grid

                    if (f.TablesChanged)
                        LoadTablesToFLP();     // bàn có đổi → reload layout bàn
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?",
                             "Đăng xuất", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                var login = new frmLogin();
                login.Show();
            }
        }
    }
}
