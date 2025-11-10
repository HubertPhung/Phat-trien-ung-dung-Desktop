using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab_Advanced_Command
{
    public partial class FoodForm : Form
    {
        private DataTable foodTable;
        public FoodForm()
        {
            InitializeComponent();
        }

        private void LoadCategory()
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            // Mở kết nối
            conn.Open();

            // Lấy dữ liệu từ CSDL đổ vào DataTable
            adapter.Fill(dt);

            // Đóng kết nối và giải phóng bộ nhớ
            conn.Close();
            conn.Dispose();

            // Đưa dữ liệu vào comboBox
            cbbCategory.DataSource = dt;

            // Hiển thị tên nhóm sản phẩm
            cbbCategory.DisplayMember = "Name";

            // Nhưng khi lấy giá trị thì lấy ID của nhóm
            cbbCategory.ValueMember = "ID";
        }
        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedIndex == -1) return;

            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Food WHERE CategoryID = @categoryID"; // đổi về CategoryID

            // Truyền tham số
            cmd.Parameters.Add("@categoryID", SqlDbType.Int);

            if(cbbCategory.SelectedValue is DataRowView)
            {
                DataRowView drv = (DataRowView)cbbCategory.SelectedValue;
                cmd.Parameters["@categoryID"].Value = (int)drv["ID"];
            }
            else
            {
                cmd.Parameters["@categoryID"].Value = (int)cbbCategory.SelectedValue;
            }

            // Tạo bộ điều hợp dữ liệu
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            foodTable = new DataTable();

            // Mở kết nối
            conn.Open();

            // Lấy dữ liệu từ CSDL đổ vào DataTable
            adapter.Fill(foodTable);

            // Đóng kết nối và giải phóng bộ nhớ
            conn.Close();
            conn.Dispose();

            // Đưa dữ liệu lên DataGridView
            dgvFoodList.DataSource = foodTable;

            // Tính số lượng mẫu tin
            lblQuantity.Text = foodTable.Rows.Count.ToString();
            lblCatName.Text = cbbCategory.Text;
        }

        private void tsmCalculateQuantity_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT @numSaleFood = SUM(Quantity) FROM BillDetails WHERE FoodID = @foodId";

            // Lấy thông tin sản phẩm được chọn
            if(dgvFoodList.SelectedRows.Count >0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];

                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                // Truyền tham số
                cmd.Parameters.Add("@foodId", SqlDbType.Int).Value = rowView["ID"]; // ID của Food
                cmd.Parameters.Add("@numSaleFood", SqlDbType.Int).Direction = ParameterDirection.Output;

                // Mở kết nối
                conn.Open();

                // Thực thi câu lệnh
                cmd.ExecuteNonQuery();

                string result = cmd.Parameters["@numSaleFood"].Value.ToString();
                MessageBox.Show("Tổng số lượng món " + rowView["Name"] + " đã bán là: " + result + " " + rowView["Unit"]);

                // Đóng kết nối
                conn.Close();
            }
            cmd.Dispose();
            conn.Dispose();
        }

        private void tsmAddFood_Click(object sender, EventArgs e)
        {
            FoodInfoForm foodForm = new FoodInfoForm();
            foodForm.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);
            foodForm.Show(this);
        }

        private void FoodForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            int index = cbbCategory.SelectedIndex;
            cbbCategory.SelectedIndex = -1;
            cbbCategory.SelectedIndex = index;
        }

        private void tsmUpdateFood_Click(object sender, EventArgs e)
        {
            if (dgvFoodList.SelectedRows.Count >0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    FoodInfoForm foodInfo = new FoodInfoForm();
                    foodInfo.DisplayFoodInfo(rowView);
                    foodInfo.FormClosed += new FormClosedEventHandler(FoodForm_FormClosed);
                    foodInfo.Show(this);
                }
            }
        }

        private void dgvFoodList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSreachByName_TextChanged(object sender, EventArgs e)
        {
            if (foodTable == null) return;

            string filterExpression = "Name LIKE '%" + txtSreachByName.Text.Replace("'", "''") + "%'"; // escape '
            string sortExpression = "Price DESC";
            DataViewRowState rowStateFillter = DataViewRowState.OriginalRows;

            DataView foodView = new DataView(foodTable,
                filterExpression, sortExpression, rowStateFillter);

            dgvFoodList.DataSource = foodView;
        }
    }
}
