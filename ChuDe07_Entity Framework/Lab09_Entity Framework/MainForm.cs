using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class MainForm : Form
    {
        private int? _selectedTableId;
        private int? _currentBillId;
        private System.Windows.Forms.Button btnEmployees;

        public MainForm()
        {
            InitializeComponent();
            InitializeEmployeeButton();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Show admin menu only for admins
            if (AppSession.CurrentUser == null)
            {
                if (tsmiQuanTri != null)
                    tsmiQuanTri.Visible = false;
            }
            else
            {
                if (tsmiQuanTri != null)
                    tsmiQuanTri.Visible = AppSession.IsAdmin();
            }

            SetupFoodListView();
            ShowCategoriesd();
            LoadTableButtons();
        }

        private void SetupFoodListView()
        {

            lvwFood.BeginUpdate();
            try
            {
                lvwFood.View = View.Details;
                lvwFood.Columns.Clear();
                lvwFood.Columns.Add("ID", 60, HorizontalAlignment.Left);
                lvwFood.Columns.Add("Tên món", 150, HorizontalAlignment.Left);
                lvwFood.Columns.Add("ĐVT", 70, HorizontalAlignment.Left);
                lvwFood.Columns.Add("Loại", 140, HorizontalAlignment.Left);
                lvwFood.Columns.Add("Giá", 90, HorizontalAlignment.Right);
                lvwFood.Columns.Add("Ghi chú", 220, HorizontalAlignment.Left);
            }
            finally
            {
                lvwFood.EndUpdate();
            }
        }

        private void accountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open current user's account info; if no session, open login
            if (AppSession.CurrentUser == null)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog(this) != DialogResult.OK)
                        return;
                }
            }

            Account user = AppSession.CurrentUser;
            if (user == null)
                return;

            // For simplicity, open editable form. You can add read-only mode later for staff.
            using (var dlg = new UpdateAccountForm(user.Id))
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnReloadCategory_Click(object sender, EventArgs e)
        {
            ShowCategoriesd();
        }

        private List<Category> GetCategories()
        {
            var dbContext = new RestaurantContext();
            return dbContext.Categories.OrderBy(x => x.Name).ToList();
        }

        private void ShowCategoriesd()
        {
            tvwCategory.Nodes.Clear();

            var cateMap = new Dictionary<CategoryType, string>()
            {
                [CategoryType.Food] = "Đồ ăn",
                [CategoryType.Drink] = "Thức uống"
            };

            TreeNode rootNode = tvwCategory.Nodes.Add("Tất cả");

            List<Category> categories = GetCategories();

            foreach (KeyValuePair<CategoryType, string> cateType in cateMap)
            {
                TreeNode childNode = rootNode.Nodes.Add(cateType.Key.ToString(), cateType.Value);
                childNode.Tag = cateType.Key;

                foreach (Category category in categories)
                {
                    if (category.Type != cateType.Key)
                        continue;

                    TreeNode grandChildNode = childNode.Nodes.Add(category.Id.ToString(), category.Name);
                    grandChildNode.Tag = category;
                }
            }
            tvwCategory.ExpandAll();
            tvwCategory.SelectedNode = rootNode;
        }

        private List<FoodModel> GetFoodByCategory(int? categoryID)
        {
            var dbContext = new RestaurantContext();

            IQueryable<Food> foods = dbContext.Foods.AsQueryable();

            if (categoryID != null)
            {
                foods = foods.Where(f => f.FoodCategoryId == categoryID);
            }

            return foods
                .OrderBy(x => x.Name)
                .Select(x => new FoodModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    unit = x.Unit,
                    Price = x.Price,
                    Notes = x.Notes,
                    CategoryName = x.Category.Name
                })
                .ToList();
        }

        private List<FoodModel> GetFoodByCategoryType(CategoryType? cateType)
        {
            var dbContext = new RestaurantContext();

            return dbContext.Foods
                .Where(x => x.Category.Type == cateType)
                .OrderBy(x => x.Name)
                .Select(x => new FoodModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    unit = x.Unit,
                    Price = x.Price,
                    Notes = x.Notes,
                    CategoryName = x.Category.Name
                })
                .ToList();
        }

        private void ShowFoodsForNode(TreeNode node)
        {
            lvwFood.Items.Clear();

            if (node == null)
                return;

            List<FoodModel> foods;
            if (node.Level == 1)
            {
                var categoryType = (CategoryType)node.Tag;
                foods = GetFoodByCategoryType(categoryType);
            }
            else
            {
                var category = node.Tag as Category;
                foods = GetFoodByCategory(category?.Id);
            }

            ShowFoodsOnListView(foods);
        }

        private void ShowFoodsOnListView(List<FoodModel> foods)
        {
            lvwFood.BeginUpdate();
            try
            {
                foreach (FoodModel food in foods)
                {
                    var item = new ListViewItem(food.Id.ToString());
                    item.SubItems.Add(food.Name);
                    item.SubItems.Add(food.unit);
                    item.SubItems.Add(food.CategoryName);
                    item.SubItems.Add(food.Price.ToString("##,###"));
                    item.SubItems.Add(food.Notes);
                    lvwFood.Items.Add(item);
                }
            }
            finally
            {
                lvwFood.EndUpdate();
            }
        }

        private void tvwCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowFoodsForNode(e.Node);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            var dialog = new UpdateCategoryForm();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowCategoriesd();
            }
        }

        private void tvwCategory_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Level != 2 || e.Node.Tag == null)
                return;

            var category = e.Node.Tag as Category;
            var dialog = new UpdateCategoryForm(category.Id);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowCategoriesd();
            }
        }

        private void btnReloadFood_Click(object sender, EventArgs e)
        {
            ShowFoodsForNode(tvwCategory.SelectedNode);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwFood.SelectedItems.Count == 0)
                return;

            var dbContext = new RestaurantContext();
            var selectedFoodId = int.Parse(lvwFood.SelectedItems[0].Text);
            Food selectedFood = dbContext.Foods.Find(selectedFoodId);

            if (selectedFood != null)
            {
                dbContext.Foods.Remove(selectedFood);
                dbContext.SaveChanges();

                lvwFood.Items.Remove(lvwFood.SelectedItems[0]);
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            var dialog = new UpdateFoodForm();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ShowFoodsForNode(tvwCategory.SelectedNode);
            }
        }

        private void lvwFood_DoubleClick(object sender, EventArgs e)
        {
            if (lvwFood.SelectedItems.Count == 0)
                return;


            if (_currentBillId != null)
            {
                var foodId = int.Parse(lvwFood.SelectedItems[0].Text);
                var input = InputBox("Thêm món", "Nhập số lượng:", "1");
                if (string.IsNullOrWhiteSpace(input))
                    return;
                if (!int.TryParse(input, out var qty) || qty <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AddFoodToBill(foodId, qty);
                return;
            }

            // fallback: no bill selected -> inform user
            MessageBox.Show("Vui lòng chọn bàn trước khi thêm món", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void tsmiVaiTro_Click_1(object sender, EventArgs e)
        {
            using (var dlg = new RoleForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void tsmiHoaDon_Click(object sender, EventArgs e)
        {
            using (var dlg = new BillsForm())
            {
                dlg.ShowDialog(this);
            }
        }



        private void tsmiThucAn_Click(object sender, EventArgs e)
        {
            using (var dlg = new FoodForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void tsmiBan_Click(object sender, EventArgs e)
        {
            using (var dlg = new TableForm())
            {
                dlg.ShowDialog(this);
            }

            LoadTableButtons();
        }

        private void tsmiAdmin_Accounts_Click(object sender, EventArgs e)
        {
            using (var dlg = new AccountForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void tsmThongTinNhanVien_Click(object sender, EventArgs e)
        {
            using (var dlg = new EmployeeForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnRefreshTables_Click(object sender, EventArgs e)
        {
            LoadTableButtons();
        }

        private void LoadTableButtons()
        {
            flpTables.Controls.Clear();
            using (var db = new RestaurantContext())
            {
                var tables = db.Tables.OrderBy(t => t.Name).ToList();
                var openBills = db.Bills.Where(b => !b.IsPaid).ToList();
                foreach (DiningTable t in tables)
                {
                    var hasOpen = openBills.Any(b => b.TableId == t.Id);
                    var btn = new Button
                    {
                        Width = 120,
                        Height = 70,
                        Margin = new Padding(6),
                        Tag = t.Id,
                        Text = t.Name + (hasOpen ? "\n(Đang dùng)" : "\n(Trống)"),
                        BackColor = hasOpen ? Color.LightSalmon : (t.IsActive ? Color.LightGreen : Color.LightGray)
                    };
                    btn.Click += TableButton_Click;
                    flpTables.Controls.Add(btn);
                }
            }
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn))
                return;
            _selectedTableId = (int)btn.Tag;
            lblSelectedTable.Text = $"Bàn: {btn.Text.Split('\n')[0]}";
            EnsureBillForSelectedTable();

            LoadTableButtons();
            LoadBillItems();
        }

        private void EnsureBillForSelectedTable()
        {
            if (_selectedTableId == null)
                return;
            using (var db = new RestaurantContext())
            {
                Bill bill = db.Bills.Where(b => b.TableId == _selectedTableId && !b.IsPaid)
                    .OrderByDescending(b => b.CheckIn).FirstOrDefault();
                if (bill == null)
                {
                    if (MessageBox.Show("Tạo hóa đơn mới cho bàn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    bill = new Bill
                    {
                        TableId = _selectedTableId.Value,
                        CheckIn = DateTime.Now,
                        DiscountPercent = 0,
                        IsPaid = false,
                        StaffId = AppSession.CurrentUser?.Id
                    };
                    db.Bills.Add(bill);
                    db.SaveChanges();
                }
                _currentBillId = bill.Id;
            }
        }

        private void LoadBillItems()
        {
            if (_currentBillId == null)
            {
                dgvBillItems.DataSource = null;
                return;
            }
            using (var db = new RestaurantContext())
            {
                var items = db.BillDetails.Where(d => d.BillId == _currentBillId)
                    .Select(d => new { d.Id, Food = d.Food.Name, d.Quantity, d.UnitPrice, Total = d.Quantity * d.UnitPrice, d.Notes })
                    .ToList();
                dgvBillItems.DataSource = items;
            }
        }

        // Add a food to the current bill (re-using UpdateFoodForm selection or a simple input)
        private void AddFoodToBill(int foodId, int quantity)
        {
            if (_currentBillId == null)
                return;
            using (var db = new RestaurantContext())
            {
                Food food = db.Foods.Find(foodId);
                if (food == null)
                    return;
                var detail = new BillDetail
                {
                    BillId = _currentBillId.Value,
                    FoodId = food.Id,
                    Quantity = quantity,
                    UnitPrice = food.Price
                };
                db.BillDetails.Add(detail);
                db.SaveChanges();
            }
            LoadBillItems();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (_currentBillId == null)
                return;
            using (var db = new RestaurantContext())
            {
                Bill bill = db.Bills.Find(_currentBillId.Value);
                if (bill == null)
                    return;
                bill.IsPaid = true;
                bill.CheckOut = DateTime.Now;
                db.SaveChanges();
            }
            LoadTableButtons();
            LoadBillItems();
        }

        private void btnTransferTable_Click(object sender, EventArgs e)
        {
            if (_currentBillId == null)
                return;
            var input = InputBox("Chuyển bàn", "Nhập ID bàn đích:", "");
            if (string.IsNullOrWhiteSpace(input))
                return;
            if (!int.TryParse(input, out var targetTableId))
                return;
            using (var db = new RestaurantContext())
            {
                Bill bill = db.Bills.Find(_currentBillId.Value);
                if (bill == null)
                    return;
                bill.TableId = targetTableId;
                db.SaveChanges();
            }
            LoadTableButtons();
        }

        private void btnMergeTable_Click(object sender, EventArgs e)
        {
            if (_currentBillId == null)
                return;
            var input = InputBox("Nhập bàn", "Nhập ID bàn để nhập hóa đơn vào bàn hiện tại:", "");
            if (string.IsNullOrWhiteSpace(input))
                return;
            if (!int.TryParse(input, out var fromTableId))
                return;
            using (var db = new RestaurantContext())
            {
                Bill fromBill = db.Bills.Where(b => b.TableId == fromTableId && !b.IsPaid)
                    .OrderByDescending(b => b.CheckIn).FirstOrDefault();
                if (fromBill == null)
                {
                    MessageBox.Show("Bàn nguồn không có hóa đơn mở");
                    return;
                }
                Bill toBill = db.Bills.Find(_currentBillId.Value);
                if (toBill == null)
                    return;
                var details = db.BillDetails.Where(d => d.BillId == fromBill.Id).ToList();
                foreach (BillDetail d in details)
                {
                    d.BillId = toBill.Id;
                }
                db.Bills.Remove(fromBill);
                db.SaveChanges();
            }
            LoadBillItems();
            LoadTableButtons();
        }

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            if (_currentBillId == null)
                return;
            var doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            using (var dlg = new PrintPreviewDialog { Document = doc, Width = 800, Height = 600 })
            {
                dlg.ShowDialog(this);
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentBillId == null)
                return;
            using (var db = new RestaurantContext())
            {
                Bill bill = db.Bills.Find(_currentBillId.Value);
                var items = db.BillDetails.Where(d => d.BillId == _currentBillId.Value)
                    .Select(d => new { d.Food.Name, d.Quantity, d.UnitPrice, Total = d.Quantity * d.UnitPrice })
                    .ToList();
                float y = 20;
                Graphics g = e.Graphics;
                var font = new Font("Segoe UI", 10);
                g.DrawString($"Hóa đơn #{bill.Id} - Bàn {bill.TableId}", font, Brushes.Black, 20, y);
                y += 25;
                foreach (var it in items)
                {
                    g.DrawString($"{it.Name} x{it.Quantity} = {it.Total:N0}", font, Brushes.Black, 20, y);
                    y += 20;
                }
                var gross = items.Sum(i => i.Total);
                var discount = gross * bill.DiscountPercent / 100;
                var net = gross - discount;
                y += 10;
                g.DrawString($"Tổng: {gross:N0}", font, Brushes.Black, 20, y);
                y += 20;
                g.DrawString($"Giảm: {discount:N0}", font, Brushes.Black, 20, y);
                y += 20;
                g.DrawString($"Thực thu: {net:N0}", font, Brushes.Black, 20, y);
            }
        }

        // Local InputBox helper (no external references required)
        private string InputBox(string title, string prompt, string defaultValue)
        {
            using (var form = new Form())
            using (var label = new Label())
            using (var textBox = new TextBox())
            using (var buttonOk = new Button())
            using (var buttonCancel = new Button())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(360, 130);
                form.MinimizeBox = false;
                form.MaximizeBox = false;

                label.AutoSize = true;
                label.Text = prompt;
                label.Location = new Point(12, 15);
                label.Size = new Size(330, 20);
                label.UseCompatibleTextRendering = true;

                textBox.Size = new Size(330, 23);
                textBox.Location = new Point(12, 40);
                textBox.Text = defaultValue ?? string.Empty;

                buttonOk.Text = "OK";
                buttonOk.DialogResult = DialogResult.OK;
                buttonOk.Location = new Point(186, 80);
                buttonOk.Size = new Size(75, 25);

                buttonCancel.Text = "Hủy";
                buttonCancel.DialogResult = DialogResult.Cancel;
                buttonCancel.Location = new Point(267, 80);
                buttonCancel.Size = new Size(75, 25);
                buttonCancel.UseCompatibleTextRendering = true;

                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                DialogResult result = form.ShowDialog(this);
                return result == DialogResult.OK ? textBox.Text : null;
            }
        }

        private void InitializeEmployeeButton()
        {
            btnEmployees = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(12, 370),
                Name = "btnEmployees",
                Size = new System.Drawing.Size(120, 23),
                TabIndex = 999,
                Text = "Nhân viên",
                UseVisualStyleBackColor = true
            };
            btnEmployees.Click += new System.EventHandler(BtnEmployees_Click);
            Controls.Add(btnEmployees);
        }

        private void BtnEmployees_Click(object sender, EventArgs e)
        {
            using (var dlg = new EmployeeForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void tsmiMenu_Click(object sender, EventArgs e)
        {
            using (var dlg = new MenuForm())
            {
                dlg.ShowDialog(this);
            }
        }

    }
}
