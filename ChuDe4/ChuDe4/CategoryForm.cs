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

            this.txtName.DoubleClick += txtName_DoubleClick;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            string query = "SELECT ID, Name, [Type] FROM Category";
            sqlCommand.CommandText = query;

            sqlConnection.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            this.DisplayCategory(sqlDataReader);

            sqlConnection.Close();
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                lvCategory.Items.Add(item);

                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database=RestaurantManagement; Integrated Security=true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Category (Name, [Type]) VALUES (@name, @type)";
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar,100).Value = (txtName.Text ?? string.Empty).Trim();

                int typeVal;
                if (!int.TryParse(txtType.Text, out typeVal))
                {
                    MessageBox.Show("Giá trị lỗi không hợp lệ. Vui lòng nhập0 lại.");
                    return;
                }
                sqlCommand.Parameters.Add("@type", SqlDbType.Int).Value = typeVal;

                sqlConnection.Open();

                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowsEffected ==1)
                {
                    MessageBox.Show("Thêm nhóm món ăn thành công");

                    btnLoad.PerformClick();

                    txtName.Text = "";
                    txtType.Text = "";
                }
                else
                    MessageBox.Show("Đã có lõi xảy ra. Vui lòng thử lại");
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];

            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;

            txtType.Text = item.SubItems[2].Text;


            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE Category SET Name = @name, [Type] = @type WHERE ID = @id";
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar,100).Value = (txtName.Text ?? string.Empty).Trim();

                int typeVal;
                if (!int.TryParse(txtType.Text, out typeVal))
                {
                    MessageBox.Show("Giá trị Lỗi không hợp lệ. Vui lòng nhập lại.");
                    return;
                }
                sqlCommand.Parameters.Add("@type", SqlDbType.Int).Value = typeVal;

                int idVal;
                if (!int.TryParse(txtID.Text, out idVal))
                {
                    MessageBox.Show("ID không hợp lệ.");
                    return;
                }
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = idVal;

                sqlConnection.Open();

                int numOfRowEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowEffected ==1)
                {
                    ListViewItem item = lvCategory.SelectedItems[0];

                    item.SubItems[1].Text = txtName.Text;
                    item.SubItems[2].Text = typeVal.ToString();

                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

  
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;

                    MessageBox.Show("Cập nhật nhóm món ăn thành công");
                }
                else
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Category WHERE ID = @id";

                int idVal;
                if (!int.TryParse(txtID.Text, out idVal))
                {
                    MessageBox.Show("ID không hợp lệ.");
                    return;
                }
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = idVal;

                sqlConnection.Open();

                int numberOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if(numberOfRowsEffected ==1)
                {
                    ListViewItem item = lvCategory.SelectedItems[0];
                    lvCategory.Items.Remove(item);

                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    MessageBox.Show("Xóa nhóm món ăn thành công");
                }
                else
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
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
