// AccountManager.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ChuDe4
{
    public partial class AccountManager : Form
    {
        private DataAccess db = new DataAccess();

        public AccountManager()
        {
            InitializeComponent();

            // Wire up events
            this.Load += AccountManager_Load;
            this.dgvAccounts.SelectionChanged += dgvAccounts_SelectionChanged;
            this.btnAdd.Click += btnAdd_Click;
            this.btnUpdate.Click += btnUpdate_Click;
            this.btnResetPassword.Click += btnResetPassword_Click;
            this.tsmDelete.Click += tsmDelete_Click;
            this.tsmViewRoles.Click += tsmViewRoles_Click;
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            LoadFilterComboBoxes();
            LoadAccounts();
        }

        private void LoadFilterComboBoxes()
        {
            // Load Account Groups
            cboAccountGroup.Items.Add("Tất cả");
            string query = "SELECT DISTINCT [Group] FROM Accounts WHERE [Group] IS NOT NULL";
            DataTable groupTable = db.ExecuteQuery(query);
            foreach (DataRow row in groupTable.Rows)
            {
                cboAccountGroup.Items.Add(row["Group"].ToString());
            }
            cboAccountGroup.SelectedIndex = 0;

            // Load Status
            cboStatus.Items.Add("Ngưng hoạt động"); // value = 0
            cboStatus.Items.Add("Hoạt động"); // value = 1
            cboStatus.Items.Add("Tất cả"); // value = 2
            cboStatus.SelectedIndex = 1;

            // Tự động tải lại khi thay đổi lựa chọn
            cboAccountGroup.SelectedIndexChanged += (s, ev) => LoadAccounts();
            cboStatus.SelectedIndexChanged += (s, ev) => LoadAccounts();
        }

        private void LoadAccounts()
        {
            string query = "SELECT ID, UserName AS [Tên đăng nhập], DisplayName AS [Tên hiển thị], [Group] AS [Nhóm], IsActive AS [Kích hoạt] FROM Accounts WHERE 1=1";
            var parameters = new System.Collections.Generic.List<SqlParameter>();

            if (cboAccountGroup.SelectedIndex > 0)
            {
                query += " AND [Group] = @Group";
                parameters.Add(new SqlParameter("@Group", cboAccountGroup.SelectedItem.ToString()));
            }

            if (cboStatus.SelectedIndex != 2)
            {
                query += " AND IsActive = @IsActive";
                parameters.Add(new SqlParameter("@IsActive", cboStatus.SelectedIndex));
            }

            dgvAccounts.DataSource = db.ExecuteQuery(query, parameters.ToArray());
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tránh tự động chọn dòng đầu tiên
            dgvAccounts.ClearSelection();
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                var row = dgvAccounts.SelectedRows[0];
                txtUsername.Text = Convert.ToString(row.Cells["Tên đăng nhập"].Value);
                txtFullName.Text = Convert.ToString(row.Cells["Tên hiển thị"].Value);
                // Lấy thêm các trường khác nếu có trên form (email, tel)
                var isActiveVal = row.Cells["Kích hoạt"].Value;
                bool isActive = false;
                if (isActiveVal is bool) isActive = (bool)isActiveVal;
                else if (isActiveVal != null) bool.TryParse(isActiveVal.ToString(), out isActive);
                chkActive.Checked = isActive;

                txtUsername.Enabled = false; // Không cho sửa tên đăng nhập (khóa chính logic)
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
            }
            else
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtUsername.Text = "";
            txtFullName.Text = "";
            chkActive.Checked = false;
            txtUsername.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            dgvAccounts.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string defaultPassword = "123"; // TODO: Nên mã hóa mật khẩu này

            string query = "INSERT INTO Accounts (UserName, DisplayName, Password, [Group], IsActive) VALUES (@UserName, @DisplayName, @Password, @Group, @IsActive)";
            SqlParameter[] parameters = {
                new SqlParameter("@UserName", txtUsername.Text),
                new SqlParameter("@DisplayName", txtFullName.Text),
                new SqlParameter("@Password", defaultPassword),
                new SqlParameter("@Group", "Staff"), // Mặc định là Staff
                new SqlParameter("@IsActive", chkActive.Checked)
            };

            try
            {
                if (db.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccounts();
                    ClearForm();
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) 
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0) return;

            int accountId = Convert.ToInt32(((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row["ID"]);

            string query = "UPDATE Accounts SET DisplayName = @DisplayName, IsActive = @IsActive WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@DisplayName", txtFullName.Text),
                new SqlParameter("@IsActive", chkActive.Checked),
                new SqlParameter("@ID", accountId)
            };

            if (db.ExecuteNonQuery(query, parameters) > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccounts();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0) return;

            var drv = (DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem;
            int accountId = Convert.ToInt32(drv.Row["ID"]);
            string username = Convert.ToString(drv.Row["Tên đăng nhập"]);
            string defaultPassword = "123"; 

            if (MessageBox.Show($"Bạn có chắc muốn đặt lại mật khẩu cho '{username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "UPDATE Accounts SET Password = @Password WHERE ID = @ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@Password", defaultPassword),
                    new SqlParameter("@ID", accountId)
                };

                if (db.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show($"Mật khẩu của '{username}' đã được đặt lại thành '{defaultPassword}'.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // --- Context Menu Events ---

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0) return;
            int accountId = Convert.ToInt32(((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row["ID"]);
            string username = Convert.ToString(((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row["Tên đăng nhập"]);
            if (MessageBox.Show($"Thao tác này sẽ ngưng kích hoạt toàn bộ vai trò của tài khoản '{username}'. Bạn có chắc chắn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "UPDATE AccountRoles SET IsActive =0 WHERE AccountID = @AccountID";
                SqlParameter[] parameters = { new SqlParameter("@AccountID", accountId) };
                int rowsAffected = db.ExecuteNonQuery(query, parameters);
                MessageBox.Show($"Đã ngưng kích hoạt {rowsAffected} vai trò của tài khoản '{username}'.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsmViewRoles_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                int accountId = Convert.ToInt32(((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row["ID"]);
                string username = Convert.ToString(((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row["Tên đăng nhập"]);
                var rolesForm = new AccountRolesForm(accountId, username);
                rolesForm.ShowDialog();
            }
        }
    }
}
