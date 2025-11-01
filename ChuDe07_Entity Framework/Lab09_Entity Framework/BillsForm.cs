using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class BillsForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public BillsForm()
        {
            InitializeComponent();
        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            LoadBills();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadBills();
        }

        private void LoadBills()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
            var query = _db.Bills
                .Where(b => b.CheckIn >= from && b.CheckIn <= to)
                .Select(b => new
                {
                    b.Id,
                    Table = b.Table.Name,
                    b.CheckIn,
                    b.CheckOut,
                    b.DiscountPercent,
                    b.IsPaid,
                    Staff = b.Staff.DisplayName,
                    Gross = b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0,
                    Discount = (b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent / 100,
                    Net = (b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) - ((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent / 100)
                })
                .OrderByDescending(b => b.CheckIn)
                .ToList();
            dgvBills.DataSource = query;

            var totalGross = query.Sum(x => x.Gross);
            var totalDiscount = query.Sum(x => x.Discount);
            var totalNet = query.Sum(x => x.Net);
            lblGross.Text = $"Tổng trước giảm: {totalGross:N0}";
            lblDiscount.Text = $"Tổng giảm: {totalDiscount:N0}";
            lblNet.Text = $"Thực thu: {totalNet:N0}";
        }

        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var billId = (int)dgvBills.Rows[e.RowIndex].Cells["Id"].Value;
            using (var dlg = new BillDetailsForm(billId))
            {
                dlg.ShowDialog(this);
            }
        }
    }
}
