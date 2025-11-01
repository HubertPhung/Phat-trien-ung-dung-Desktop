using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChuDe4
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
            // Cho phép m? danh sách món t? tên lo?i (double-click)
            this.txtName.DoubleClick += txtName_DoubleClick;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // T?o chu?i k?t n?i t?i cơ s? d? li?u 
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";

            // T?o đ?i tư?ng k?t nôi
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // T?o đ?i tư?ng command
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thi?t l?p l?nh truy v?n cho đ?i tư?ng command
            string query = "SELECT ID, Name, [Type] FROM Category";
            sqlCommand.CommandText = query;

            // M? k?t n?i
            sqlConnection.Open();

            // Th?c thi l?nh b?ng phương thưc ExecuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            // G?i hàm hi?n th? d? li?u lên ListView
            this.DisplayCategory(sqlDataReader);

            // Đóng k?t n?i
            sqlConnection.Close();
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while (reader.Read())
            {
                // T?o1 d?ng trong ListView
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                // Thêm d?ng m?i vào ListView
                lvCategory.Items.Add(item);

                // Thêm các c?t c?n l?i
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // T?o đ?i tư?ng k?t n?i
            string connectionString = "server=.; database=RestaurantManagement; Integrated Security=true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                // Thi?t l?p l?nh truy v?n cho đ?i tư?ng command (dùng tham s?)
                sqlCommand.CommandText = "INSERT INTO Category (Name, [Type]) VALUES (@name, @type)";
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar,100).Value = (txtName.Text ?? string.Empty).Trim();

                int typeVal;
                if (!int.TryParse(txtType.Text, out typeVal))
                {
                    MessageBox.Show("Giá tr? 'Lo?i' không h?p l?. Vui l?ng nh?p0 ho?c1.");
                    return;
                }
                sqlCommand.Parameters.Add("@type", SqlDbType.Int).Value = typeVal;

                // M? k?t n?i t?i csdl
                sqlConnection.Open();

                // Th?c thi l?nh b?ng phương th?c ExecuteNonQuery
                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowsEffected ==1)
                {
                    MessageBox.Show("Thêm nhóm món ăn thành công");

                    // T?i l?i d? li?u
                    btnLoad.PerformClick();

                    // Xóa các ô nh?p
                    txtName.Text = "";
                    txtType.Text = "";
                }
                else
                    MessageBox.Show("Đ? có l?i x?y ra. Vui l?ng th? l?i");
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            // L?y d?ng đư?c ch?n trong Lv
            ListViewItem item = lvCategory.SelectedItems[0];

            // Hi?n th? d? li?u lên TextBox
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            // L?y đúng c?t Type (subitem[2]) và gi? d?ng s? đ? thao tác SQL
            txtType.Text = item.SubItems[2].Text;

            // Hi?n th? nút c?p nh?t và xóa
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // T?o đ?i tư?ng k?t n?i 
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                // Thi?t l?p l?nh truy v?n cho đ?i tư?ng command (dùng tham s?, s?a tên b?ng và kho?ng tr?ng)
                sqlCommand.CommandText = "UPDATE Category SET Name = @name, [Type] = @type WHERE ID = @id";
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar,100).Value = (txtName.Text ?? string.Empty).Trim();

                int typeVal;
                if (!int.TryParse(txtType.Text, out typeVal))
                {
                    MessageBox.Show("Giá tr? 'Lo?i' không h?p l?. Vui l?ng nh?p0 ho?c1.");
                    return;
                }
                sqlCommand.Parameters.Add("@type", SqlDbType.Int).Value = typeVal;

                int idVal;
                if (!int.TryParse(txtID.Text, out idVal))
                {
                    MessageBox.Show("ID không h?p l?.");
                    return;
                }
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = idVal;

                // M? k?t n?i t?i csdl
                sqlConnection.Open();

                // Th?c thi l?nh b?ng phương th?c ExecuteNonQuery
                int numOfRowEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowEffected ==1)
                {
                    // C?p nh?t l?i d? li?u trên Lv
                    ListViewItem item = lvCategory.SelectedItems[0];

                    item.SubItems[1].Text = txtName.Text;
                    item.SubItems[2].Text = typeVal.ToString();

                    // Xóa các ô nh?p
                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    // Disable các nút xóa và c?p nh?t
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;

                    MessageBox.Show("C?p nh?t nhóm món ăn thành công");
                }
                else
                    MessageBox.Show("Đ? có l?i x?y ra. Vui l?ng th? l?i");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // T?o đ?i tư?ng k?t n?i 
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                // Thi?t l?p l?nh truy v?n cho đ?i tư?ng command (dùng tham s?)
                sqlCommand.CommandText = "DELETE FROM Category WHERE ID = @id";

                int idVal;
                if (!int.TryParse(txtID.Text, out idVal))
                {
                    MessageBox.Show("ID không h?p l?.");
                    return;
                }
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = idVal;

                // M? k?t n?i csdl
                sqlConnection.Open();

                // Th?c thi l?nh b?ng phương th?c ExecuteNonQuery
                int numberOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if(numberOfRowsEffected ==1)
                {
                    // C?p nh?t d? li?u trên lv
                    ListViewItem item = lvCategory.SelectedItems[0];
                    lvCategory.Items.Remove(item);

                    // Xóa các ô nh?p
                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    // Disable các nút xóa và c?p nh?t
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    MessageBox.Show("Xóa nhóm món ăn thành công");
                }
                else
                    MessageBox.Show("Đ? có l?i x?y ra. Vui l?ng th? l?i");
            }
        }

        private void tmsDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count >0)
            {
                btnDelete.PerformClick();
            }
        }

        private void tmsViewFood_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                FoodForm foodForm = new FoodForm();
                foodForm.Show(this);
                foodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }

        // M? danh sách món ăn khi double-click vào tên lo?i
        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                FoodForm foodForm = new FoodForm();
                foodForm.Show(this);
                foodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }


    }
}
