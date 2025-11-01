using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class BillDetailsForm : Form
    {
        private readonly int _billId;
        private readonly RestaurantContext _db = new RestaurantContext();

        public BillDetailsForm(int billId)
        {
            InitializeComponent();
            _billId = billId;
        }

        private void BillDetailsForm_Load(object sender, EventArgs e)
        {
            Bill bill = _db.Bills.Find(_billId);
            if (bill == null)
                return;
            lblBill.Text = $"Hóa đơn #{bill.Id} - Bàn: {bill.Table.Name}";
            var items = _db.BillDetails
                .Where(d => d.BillId == _billId)
                .Select(d => new
                {
                    d.Id,
                    Food = d.Food.Name,
                    d.Quantity,
                    d.UnitPrice,
                    Total = d.Quantity * d.UnitPrice,
                    d.Notes
                })
                .ToList();
            dgvItems.DataSource = items;
            lblTotal.Text = $"Tổng: {items.Sum(i => i.Total):N0}";
        }
    }
}
