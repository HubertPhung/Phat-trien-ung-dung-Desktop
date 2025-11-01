// BillDetailsForm.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ChuDe4
{
    public partial class BillDetailsForm : Form
    {
        private readonly int _billId;
        private DataAccess db = new DataAccess();

        public BillDetailsForm(int billId)
        {
            InitializeComponent();
            this.Load += BillDetailsForm_Load;
            _billId = billId;
        }

        private void BillDetailsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Chi tiết Hóa đơn #" + _billId;
            label1.Text = "Các món trong hóa đơn #" + _billId;
            LoadBillDetails();
        }

        private void LoadBillDetails()
        {
            string query = @"
                SELECT
                    f.Name AS FoodName,
                    bd.Quantity,
                    bd.UnitPrice,
                    (bd.Quantity * bd.UnitPrice) AS Total
                FROM dbo.BillDetails bd
                JOIN dbo.Food f ON bd.FoodID = f.ID
                WHERE bd.BillID = @BillID";

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@BillID", _billId) };

            dgvBillDetails.DataSource = db.ExecuteQuery(query, parameters);

            // Cấu hình cột
            if (dgvBillDetails.Columns.Contains("FoodName")) dgvBillDetails.Columns["FoodName"].HeaderText = "Tên món";
            if (dgvBillDetails.Columns.Contains("Quantity")) dgvBillDetails.Columns["Quantity"].HeaderText = "Số lượng";
            if (dgvBillDetails.Columns.Contains("UnitPrice")) dgvBillDetails.Columns["UnitPrice"].HeaderText = "Đơn giá";
            if (dgvBillDetails.Columns.Contains("Total")) dgvBillDetails.Columns["Total"].HeaderText = "Thành tiền";

            if (dgvBillDetails.Columns.Contains("UnitPrice")) dgvBillDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            if (dgvBillDetails.Columns.Contains("Total")) dgvBillDetails.Columns["Total"].DefaultCellStyle.Format = "N0";

            dgvBillDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}