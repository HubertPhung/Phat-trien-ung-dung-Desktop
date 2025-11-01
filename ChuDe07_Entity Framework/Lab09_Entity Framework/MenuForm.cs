using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class MenuForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private List<MenuItemRow> _currentData = new List<MenuItemRow>();

        // Print theming
        private readonly Color _accent = Color.FromArgb(242, 168, 74); // warm orange
        private readonly string _brand = "Nhà hàng";
        private readonly string _subtitle = DateTime.Now.Year.ToString();
        private readonly string _footerNote = "Trà đá miễn phí";

        public MenuForm()
        {
            InitializeComponent();
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            // Ensure Vietnamese captions at runtime (avoid mojibake from designer encoding)
            if (lblType != null) lblType.Text = "Loại:";
            if (lblCategory != null) lblCategory.Text = "Danh mục:";
            if (lblKeyword != null) lblKeyword.Text = "Từ khóa:";
            if (btnFilter != null) btnFilter.Text = "Lọc";
            if (btnPrint != null) btnPrint.Text = "In thực đơn";
            if (btnClose != null) btnClose.Text = "Đóng";
            this.Text = "Thực đơn";

            LoadTypeFilter();
            LoadCategories();
            LoadMenu();
        }

        private void LoadTypeFilter()
        {
            cbbTypeFilter.DisplayMember = "Text";
            cbbTypeFilter.ValueMember = "Value";
            cbbTypeFilter.Items.Clear();
            cbbTypeFilter.Items.Add(new { Text = "-- Tất cả --", Value = (CategoryType?)null });
            cbbTypeFilter.Items.Add(new { Text = "Đồ ăn", Value = (CategoryType?)CategoryType.Food });
            cbbTypeFilter.Items.Add(new { Text = "Thức uống", Value = (CategoryType?)CategoryType.Drink });
            cbbTypeFilter.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            var categories = _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new { c.Id, c.Name })
            .ToList();
            categories.Insert(0, new { Id = 0, Name = "-- Tất cả nhóm --" });

            cbbCategoryFilter.DisplayMember = "Name";
            cbbCategoryFilter.ValueMember = "Id";
            cbbCategoryFilter.DataSource = categories;
        }

        private void LoadMenu()
        {
            IQueryable<Food> q = _db.Foods.AsQueryable();

            // Type filter
            var typeItem = cbbTypeFilter.SelectedItem;
            if (typeItem != null)
            {
                PropertyInfo prop = typeItem.GetType().GetProperty("Value");
                var typeVal = (CategoryType?)prop?.GetValue(typeItem, null);
                if (typeVal != null)
                {
                    q = q.Where(f => f.Category.Type == typeVal);
                }
            }

            // Category filter
            int categoryId = 0;
            if (cbbCategoryFilter.SelectedValue is int id)
                categoryId = id;
            if (categoryId > 0)
            {
                q = q.Where(f => f.FoodCategoryId == categoryId);
            }

            // Keyword filter
            var keyword = (txtKeyword.Text ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(f => f.Name.Contains(keyword) || f.Notes.Contains(keyword));
            }

            _currentData = q
            .OrderBy(f => f.Name)
            .Select(f => new MenuItemRow
            {
                Name = f.Name,
                Unit = f.Unit,
                Category = f.Category.Name,
                Price = f.Price,
                Notes = f.Notes
            })
            .ToList();

            RenderMenuCards();
        }

        private void RenderMenuCards()
        {
            flpMenu.SuspendLayout();
            flpMenu.Controls.Clear();
            foreach (var it in _currentData)
            {
                var panel = CreateCard(it);
                flpMenu.Controls.Add(panel);
            }
            flpMenu.ResumeLayout();
        }

        private Control CreateCard(MenuItemRow it)
        {
            var card = new Panel
            {
                Height = 110,
                Width = CalcCardWidth(),
                Margin = new Padding(8),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            int innerPad = 10;
            int w = card.Width - innerPad * 2;

            var lblName = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Text = it.Name,
                Location = new Point(innerPad, 8),
                Size = new Size(w, 26),
                TextAlign = ContentAlignment.MiddleCenter,
                UseCompatibleTextRendering = true,
                Tag = "name"
            };

            var lblUnit = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DimGray,
                Text = it.Unit,
                Location = new Point(innerPad, 36),
                Size = new Size(w, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                UseCompatibleTextRendering = true,
                Tag = "unit"
            };

            var lblPrice = new Label
            {
                AutoSize = false,
                Text = FormatPrice(it.Price),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(innerPad, 62),
                Size = new Size(w, 28),
                ForeColor = Color.DarkGreen,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                UseCompatibleTextRendering = true,
                Tag = "price"
            };

            card.Controls.Add(lblName);
            card.Controls.Add(lblUnit);
            card.Controls.Add(lblPrice);
            return card;
        }

        private int CalcCardWidth()
        {
            var containerW = flpMenu.ClientSize.Width;
            var colWidth = 320; // desired card width
            var columns = Math.Max(1, containerW / (colWidth + 16));
            var width = (containerW - (columns + 1) * 8) / columns; //8px margin
            return Math.Max(260, width);
        }

        private void flpMenu_SizeChanged(object sender, EventArgs e)
        {
            // Re-layout cards on resize (centered content)
            foreach (Panel p in flpMenu.Controls.OfType<Panel>())
            {
                p.Width = CalcCardWidth();
                int innerPad = 10;
                int w = p.Width - innerPad * 2;
                foreach (var lab in p.Controls.OfType<Label>())
                {
                    lab.Left = innerPad;
                    lab.Width = w;
                    lab.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadMenu();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var doc = new PrintDocument();
            doc.PrintPage += StyledPrintPage;
            using (var dlg = new PrintPreviewDialog { Document = doc, Width = 900, Height = 700 })
            {
                dlg.ShowDialog(this);
            }
        }

        // Styled two-column print like sample (no images)
        private void StyledPrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle content = e.MarginBounds;
            int padding = 24;
            int gutter = 24;

            // background panel
            using (var b = new SolidBrush(_accent))
            {
                e.Graphics.FillRectangle(b, content);
            }

            // header with extra spacing between MENU and brand
            var menuFont = new Font("Segoe UI", 38, FontStyle.Bold);
            var brandFont = new Font("Segoe UI", 20, FontStyle.Bold | FontStyle.Italic);
            var yearFont = new Font("Segoe UI", 14, FontStyle.Regular);
            var white = Brushes.White;

            int headerCenterX = content.Left + content.Width / 2;
            float y = content.Top + 24; // more top padding
            var sfCenter = new StringFormat { Alignment = StringAlignment.Center };
            e.Graphics.DrawString("MENU", menuFont, white, headerCenterX, y, sfCenter); y += 56; // more space
            e.Graphics.DrawString(_brand, brandFont, white, headerCenterX, y, sfCenter); y += 36; // more space
            e.Graphics.DrawString(_subtitle, yearFont, white, headerCenterX, y, sfCenter); y += 34;

            // inner white area for items
            Rectangle inner = new Rectangle(content.Left + padding, (int)y + 10, content.Width - padding * 2, content.Bottom - (int)y - 60);
            e.Graphics.FillRectangle(Brushes.White, inner);

            // columns
            int colWidth = (inner.Width - gutter) / 2;
            Rectangle colLeft = new Rectangle(inner.Left, inner.Top, colWidth, inner.Height);
            Rectangle colRight = new Rectangle(inner.Left + colWidth + gutter, inner.Top, colWidth, inner.Height);

            // group and draw
            var groups = _currentData
            .GroupBy(x => x.Category)
            .OrderBy(g => g.Key)
            .ToList();

            float yL = colLeft.Top + 10;
            float yR = colRight.Top + 10;
            var groupFont = new Font("Segoe UI", 12, FontStyle.Bold);
            var itemFont = new Font("Segoe UI", 10, FontStyle.Regular);
            var priceFont = new Font("Segoe UI", 10, FontStyle.Bold);
            var nameBrush = Brushes.Black;
            var priceBrush = Brushes.Black;
            int lineH = 22;

            // Local function (supports ref parameter) to draw a group
            void DrawGroup(string title, IEnumerable<MenuItemRow> rows, ref float yStart, Rectangle col)
            {
                e.Graphics.DrawString(title.ToUpperInvariant(), groupFont, nameBrush, col.Left + 10, yStart);
                yStart += lineH;
                foreach (var r in rows)
                {
                    if (yStart + lineH > col.Bottom)
                        return; // stop if overflow
                    // name centered
                    e.Graphics.DrawString(r.Name, itemFont, nameBrush, new RectangleF(col.Left, yStart, col.Width, lineH), new StringFormat { Alignment = StringAlignment.Center });
                    // right aligned price
                    string p = FormatPrice(r.Price);
                    e.Graphics.DrawString(p, priceFont, priceBrush, new RectangleF(col.Left, yStart, col.Width - 8, lineH), new StringFormat { Alignment = StringAlignment.Far });
                    yStart += lineH;
                    // unit centered italic
                    e.Graphics.DrawString(r.Unit, new Font("Segoe UI", 9, FontStyle.Italic), Brushes.DimGray,
                    new RectangleF(col.Left, yStart, col.Width, lineH - 2), new StringFormat { Alignment = StringAlignment.Center });
                    yStart += lineH - 2;
                }
                yStart += 10; // spacing after group
            }

            // fill left first then right (simple balance)
            foreach (var g in groups)
            {
                DrawGroup(g.Key, g, ref yL, colLeft);
            }
            foreach (var g in groups)
            {
                if (yL > colLeft.Bottom - 60)
                {
                    DrawGroup(g.Key, g, ref yR, colRight);
                }
            }

            // footer note
            var footerFont = new Font("Segoe UI", 11, FontStyle.Bold);
            e.Graphics.DrawString(_footerNote, footerFont, Brushes.Black, content.Left + content.Width / 2, content.Bottom - 40, sfCenter);

            e.HasMorePages = false;
        }

        private string FormatPrice(int price)
        {
            // Hiển thị dạng28K,45K ... nếu giá theo nghìn. Nếu không, hiển thị ###,### đ
            if (price % 1000 == 0)
            {
                return string.Format("{0}K", price / 1000);
            }
            return string.Format("{0:N0} đ", price);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class MenuItemRow
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public string Category { get; set; }
            public int Price { get; set; }
            public string Notes { get; set; }
        }
    }
}
