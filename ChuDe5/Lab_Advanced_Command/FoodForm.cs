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
            cmd.CommandText = "SELECT * FROM Food WHERE CategoryID = @categoryID";

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
    }
}
