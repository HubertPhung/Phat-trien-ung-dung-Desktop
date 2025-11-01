using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ChuDe4
{
    public partial class BillCatalogForm : Form
    {
        private readonly int _tableId;
        private readonly string _tableName;
        private readonly DataAccess _db = new DataAccess();
        private DataTable _bills;

        public BillCatalogForm(int tableId, string tableName)
        {
            InitializeComponent();
            _tableId = tableId;
            _tableName = tableName;
            this.Load += BillCatalogForm_Load;
            this.lstBills.SelectedIndexChanged += LstBills_SelectedIndexChanged;
        }

        private void BillCatalogForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Danh mục hóa đơn của bàn: {_tableName}";
            LoadBillDates();
        }

        private void LoadBillDates()
        {
            string sql = @"SELECT ID, CheckIn AS [Ngày lập] FROM Bills WHERE TableID=@TableID ORDER BY CheckIn DESC";
            var prms = new[] { new SqlParameter("@TableID", _tableId) };
            _bills = _db.ExecuteQuery(sql, prms);
            lstBills.Items.Clear();
            foreach (DataRow r in _bills.Rows)
            {
                var id = Convert.ToInt32(r["ID"]);
                DateTime? checkIn = r["Ngày lập"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["Ngày lập"]);
                lstBills.Items.Add(new BillListItem(id, checkIn));
            }
            if (lstBills.Items.Count > 0) lstBills.SelectedIndex = 0;
        }

        private void LstBills_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBills.SelectedItem is BillListItem item)
            {
                int billId = item.BillId;
                lblHeader.Text = item.HeaderText;
                LoadBillItems(billId);
            }
        }

        private void LoadBillItems(int billId)
        {
            string sql = @"SELECT f.Name AS [Món], bd.Quantity AS [SL], bd.UnitPrice AS [Đơn giá], (bd.Quantity*bd.UnitPrice) AS [Thành tiền]
                            FROM BillDetails bd JOIN Food f ON f.ID = bd.FoodID WHERE bd.BillID=@BillID";
            var prms = new[] { new SqlParameter("@BillID", billId) };
            var table = _db.ExecuteQuery(sql, prms);
            dgvItems.DataSource = table;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvItems.Columns.Contains("Đơn giá")) dgvItems.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
            if (dgvItems.Columns.Contains("Thành tiền")) dgvItems.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
            long sum = 0; foreach (DataRow r in table.Rows) sum += Convert.ToInt64(r["Thành tiền"]);
            lblSummary.Text = $"Tổng: {sum:N0} đ";
        }

        private class BillListItem
        {
            public int BillId { get; }
            public DateTime? CheckIn { get; }
            public string HeaderText => CheckIn.HasValue
                ? $"Chi tiết hóa đơn ngày {CheckIn.Value:dd/MM/yyyy}"
                : "Chi tiết hóa đơn (không có ngày)";
            public BillListItem(int billId, DateTime? checkIn)
            {
                BillId = billId; CheckIn = checkIn;
            }
            public override string ToString() => CheckIn.HasValue ? CheckIn.Value.ToString("dd/MM/yyyy") : "(không có ngày)";
        }
    }
}
