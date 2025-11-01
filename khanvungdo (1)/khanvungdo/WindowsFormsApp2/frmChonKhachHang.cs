using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp2.Infrastructure;

namespace WindowsFormsApp2
{
    public class frmChonKhachHang : Form
    {
        private TextBox txtSearch;
        private Button btnSearch, btnOK, btnCancel;
        private DataGridView dgv;

        public class CustomerInfo
        {
            public int Id { get; set; }
            public string TenKH { get; set; }
            public string CMND_CCCD { get; set; }
            public string SDT { get; set; }
        }

        public CustomerInfo SelectedCustomer { get; private set; }

        public frmChonKhachHang()
        {
            this.Text = "Chọn khách hàng";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 700; this.Height = 500;

            txtSearch = new TextBox { Left = 10, Top = 10, Width = 400 };
            btnSearch = new Button { Left = 420, Top = 8, Width = 80, Text = "T?m" };
            btnOK = new Button { Left = 510, Top = 8, Width = 80, Text = "Ch?n" };
            btnCancel = new Button { Left = 600, Top = 8, Width = 80, Text = "H?y" };
            dgv = new DataGridView { Left = 10, Top = 40, Width = 670, Height = 410, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            btnSearch.Click += (s, e) => LoadData(txtSearch.Text.Trim());
            btnOK.Click += (s, e) => SelectCurrent();
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            dgv.DoubleClick += (s, e) => SelectCurrent();

            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);
            this.Controls.Add(dgv);

            Load += (s, e) => LoadData(null);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmChonKhachHang
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "frmChonKhachHang";
            this.Load += new System.EventHandler(this.frmChonKhachHang_Load);
            this.ResumeLayout(false);

        }

        private void frmChonKhachHang_Load(object sender, EventArgs e)
        {

        }

        private void LoadData(string keyword)
        {
            using (var conn = Db.Open())
            using (var cmd = new SqlCommand(@"SELECT id, TenKH, CMND_CCCD, SDT FROM dbo.KhachHang
                                              WHERE (@kw IS NULL OR TenKH LIKE '%' + @kw + '%')
                                              ORDER BY TenKH", conn))
            {
                cmd.Parameters.AddWithValue("@kw", (object)keyword ?? DBNull.Value);
                var dt = new DataTable();
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                dgv.DataSource = dt;
            }
        }

        private void SelectCurrent()
        {
            if (dgv.CurrentRow == null) return;
            var row = dgv.CurrentRow;
            SelectedCustomer = new CustomerInfo
            {
                Id = Convert.ToInt32(row.Cells["id"].Value),
                TenKH = Convert.ToString(row.Cells["TenKH"].Value),
                CMND_CCCD = Convert.ToString(row.Cells["CMND_CCCD"].Value),
                SDT = Convert.ToString(row.Cells["SDT"].Value)
            };
            this.DialogResult = DialogResult.OK;
        }
    }
}
