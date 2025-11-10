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
    public partial class FoodInfoForm : Form
    {
        public FoodInfoForm()
        {
            InitializeComponent();
        }

        private void FoodInfoForm_Load(object sender, EventArgs e)
        {
            this.InitValues();
        }

        private void InitValues()
        {
            string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Name FROM Category";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            // Mở kết nối
            conn.Open();

            // Lấy dữ liệu từ CSDL đổ vào DataSet
            adapter.Fill(ds, "Category");

            // Hiển thị dữ liệu lên ComboBox
            cbbCatName.DataSource = ds.Tables["Category"];
            cbbCatName.DisplayMember = "Name";
            cbbCatName.ValueMember = "ID";

            // Đóng kết nối và giải phóng bộ nhớ
            conn.Close();
            conn.Dispose();
        }

        private void ResetText()
        {
            txtFoodId.ResetText();
            txtName.ResetText();
            txtUnit.ResetText();
            txtNotes.ResetText();
            cbbCatName.SelectedIndex = -1;
            // Đặt lại giá trị số
            nudPrice.Value =0;
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = conn.CreateCommand();
                // Khớp với thủ tục dbo.InsertFood(@id OUTPUT, @name, @unit, @categoryID, @price, @notes)
                cmd.CommandText = "EXECUTE InsertFood @id OUTPUT, @name, @unit, @categoryID, @price, @notes";

                // Thêm đối số vào đối tượng command
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar,1000).Value = txtName.Text;
                cmd.Parameters.Add("@unit", SqlDbType.NVarChar,100).Value = txtUnit.Text;
                cmd.Parameters.Add("@categoryID", SqlDbType.Int).Value = cbbCatName.SelectedValue;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = Convert.ToInt32(nudPrice.Value);
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar,3000).Value = txtNotes.Text;

                conn.Open();

                int numRowAffected = cmd.ExecuteNonQuery();

                if(numRowAffected >0 )
                {
                    string foodID = cmd.Parameters["@id"].Value.ToString();
                    MessageBox.Show("Successfully adding new food. Food ID = " + foodID, "Message");
                    this.ResetText();
                }
                else
                {
                    MessageBox.Show("Adding food failed");
                }

                conn.Close();
                conn.Dispose();
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message, "SQL Error");
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        public void DisplayFoodInfo(DataRowView rowView)
        {
            try
            {
                txtFoodId.Text = rowView["ID"].ToString();
                txtName.Text = rowView["Name"].ToString();
                txtNotes.Text = rowView["Notes"].ToString();
                txtUnit.Text = rowView["Unit"].ToString();
                // Gán theo Value thay vì Text để tránh lỗi định dạng
                decimal price;
                if (rowView["Price"] != DBNull.Value && decimal.TryParse(rowView["Price"].ToString(), out price))
                {
                    if (price < nudPrice.Minimum) price = nudPrice.Minimum;
                    if (price > nudPrice.Maximum) price = nudPrice.Maximum;
                    nudPrice.Value = price;
                }
                else
                {
                    nudPrice.Value =0;
                }

                // Chọn Category theo ID đúng schema (CategoryID)
                if (rowView.Row.Table.Columns.Contains("CategoryID"))
                {
                    cbbCatName.SelectedValue = rowView["CategoryID"];
                }
                else
                {
                    cbbCatName.SelectedIndex = -1;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
                this.Close();
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=.; database = RestaurantManagement; Integrated Security = true;";
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = conn.CreateCommand();
                // Khớp với thủ tục dbo.UpdateFood(@id, @name, @unit, @categoryID, @price, @notes)
                cmd.CommandText = "EXECUTE UpdateFood @id, @name, @unit, @categoryID, @price, @notes";

                // Thêm tham số và gán giá trị
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = string.IsNullOrWhiteSpace(txtFoodId.Text) ?0 : int.Parse(txtFoodId.Text);
                cmd.Parameters.Add("@name", SqlDbType.NVarChar,1000).Value = txtName.Text;
                cmd.Parameters.Add("@unit", SqlDbType.NVarChar,100).Value = txtUnit.Text;
                cmd.Parameters.Add("@categoryID", SqlDbType.Int).Value = cbbCatName.SelectedValue;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = Convert.ToInt32(nudPrice.Value);
                cmd.Parameters.Add("@notes", SqlDbType.NVarChar,3000).Value = txtNotes.Text;

                conn.Open();

                int numRowAffected = cmd.ExecuteNonQuery();

                if (numRowAffected >0)
                {
                    MessageBox.Show("Successfully updating food.", "Message");
                    this.ResetText();
                }
                else
                {
                    MessageBox.Show("Updating food failed");
                }

                conn.Close();
                conn.Dispose();
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message, "SQL Error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
