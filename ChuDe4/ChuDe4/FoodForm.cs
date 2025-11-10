using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ChuDe4
{
    public partial class FoodForm : Form
    {
        public FoodForm()
        {
            InitializeComponent();
        }

        private void dgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void LoadFood(int categoryID)
        {
            string connectionString = "server=.; database=RestaurantManagement; Integrated Security=true;";
            using (var sqlConnection = new SqlConnection(connectionString))
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlConnection.Open();

                // Get category name
                sqlCommand.CommandText = "SELECT Name FROM dbo.Category WHERE ID = @id";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = categoryID;
                var result = sqlCommand.ExecuteScalar();
                var categoryName = result != null ? result.ToString() : string.Empty;
                this.Text = "Danh sách các món ăn thuộc nhóm: " + categoryName;

                // Get foods in category
                sqlCommand.CommandText = "SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM dbo.Food WHERE FoodCategoryID = @id";
                using (var da = new SqlDataAdapter(sqlCommand))
                {
                    var dt = new DataTable("Food");
                    da.Fill(dt);
                    dgvFood.DataSource = dt;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dt = dgvFood.DataSource as DataTable;
            if (dt == null) return;

            string connectionString = "server=.; database=RestaurantManagement; Integrated Security=true;";
            using (var conn = new SqlConnection(connectionString))
            using (var da = new SqlDataAdapter("SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM dbo.Food", conn))
            using (var builder = new SqlCommandBuilder(da))
            {
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                conn.Open();

                da.FillSchema(dt, SchemaType.Source);

                var affected = da.Update(dt);
                MessageBox.Show($"Đã lưu dữ liệu món ăn. Bản ghi đã cập nhật: {affected}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow == null)
                return;

            var dt = dgvFood.DataSource as DataTable;
            if (dt == null)
                return;

            // Xóa dòng hiện tại trong DataTable, sau đó người dùng có thể nhấn Save để đẩy xuống DB
            dgvFood.Rows.RemoveAt(dgvFood.CurrentRow.Index);
        }
    }
}
