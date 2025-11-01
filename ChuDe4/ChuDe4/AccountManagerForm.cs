using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ChuDe4
{
 public class AccountManagerForm : Form
 {
 private ComboBox cboGroup;
 private CheckBox chkActive;
 private Button btnLoad;
 private DataGridView dgvAccounts;
 private Button btnAdd;
 private Button btnUpdate;
 private Button btnResetPwd;
 private ContextMenuStrip ctxMenu;
 private ToolStripMenuItem mnuDelete;
 private ToolStripMenuItem mnuViewRoles;

 public AccountManagerForm()
 {
 InitializeComponent();
 }

 private string ConnectionString => "server=.; database=RestaurantManagement; Integrated Security=true;";

 private void InitializeComponent()
 {
 this.Text = "Qu?n l? tài kho?n";
 this.Width =900;
 this.Height =600;
 cboGroup = new ComboBox { Location = new Point(10,10), Width =150, DropDownStyle = ComboBoxStyle.DropDownList };
 chkActive = new CheckBox { Location = new Point(180,12), Text = "Active" };
 btnLoad = new Button { Location = new Point(260,8), Text = "L?c" };
 btnLoad.Click += BtnLoad_Click;

 dgvAccounts = new DataGridView { Location = new Point(10,40), Width =860, Height =460, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
 ctxMenu = new ContextMenuStrip();
 mnuDelete = new ToolStripMenuItem("Xóa tài kho?n");
 mnuViewRoles = new ToolStripMenuItem("Xem danh sách vai tr?");
 ctxMenu.Items.AddRange(new ToolStripItem[] { mnuDelete, mnuViewRoles });
 mnuDelete.Click += MnuDelete_Click;
 mnuViewRoles.Click += MnuViewRoles_Click;
 dgvAccounts.ContextMenuStrip = ctxMenu;

 btnAdd = new Button { Location = new Point(10,510), Text = "Thêm" };
 btnUpdate = new Button { Location = new Point(90,510), Text = "C?p nh?t" };
 btnResetPwd = new Button { Location = new Point(190,510), Text = "Reset m?t kh?u" };
 btnAdd.Click += BtnAdd_Click;
 btnUpdate.Click += BtnUpdate_Click;
 btnResetPwd.Click += BtnResetPwd_Click;

 this.Controls.Add(cboGroup);
 this.Controls.Add(chkActive);
 this.Controls.Add(btnLoad);
 this.Controls.Add(dgvAccounts);
 this.Controls.Add(btnAdd);
 this.Controls.Add(btnUpdate);
 this.Controls.Add(btnResetPwd);

 LoadGroups();
 }

 private void LoadGroups()
 {
 cboGroup.Items.Clear();
 cboGroup.Items.Add("");
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "SELECT DISTINCT [Group] FROM dbo.Accounts ORDER BY [Group]";
 var da = new SqlDataAdapter(cmd);
 var dt = new DataTable();
 da.Fill(dt);
 foreach (DataRow r in dt.Rows)
 {
 cboGroup.Items.Add(r[0]?.ToString());
 }
 }
 cboGroup.SelectedIndex =0;
 }

 private void BtnLoad_Click(object sender, EventArgs e)
 {
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "SELECT ID, UserName, DisplayName, [Group], IsActive FROM dbo.Accounts WHERE (@g='' OR [Group]=@g) AND (@a IS NULL OR IsActive=@a)";
 cmd.Parameters.Add("@g", SqlDbType.NVarChar,50).Value = (cboGroup.SelectedItem?.ToString() ?? "");
 if (chkActive.CheckState == CheckState.Indeterminate)
 cmd.Parameters.AddWithValue("@a", DBNull.Value);
 else
 cmd.Parameters.Add("@a", SqlDbType.Bit).Value = chkActive.Checked;
 var da = new SqlDataAdapter(cmd);
 var dt = new DataTable();
 da.Fill(dt);
 dgvAccounts.DataSource = dt;
 }
 }

 private void BtnAdd_Click(object sender, EventArgs e)
 {
 var f = new AccountEditDialog();
 if (f.ShowDialog(this) == DialogResult.OK)
 {
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "INSERT INTO dbo.Accounts(UserName, DisplayName, Password, [Group], IsActive) VALUES (@u,@d,@p,@g,@a)";
 cmd.Parameters.Add("@u", SqlDbType.NVarChar,100).Value = f.UserName;
 cmd.Parameters.Add("@d", SqlDbType.NVarChar,200).Value = f.DisplayName;
 cmd.Parameters.Add("@p", SqlDbType.NVarChar,256).Value = f.Password;
 cmd.Parameters.Add("@g", SqlDbType.NVarChar,50).Value = f.Group;
 cmd.Parameters.Add("@a", SqlDbType.Bit).Value = f.IsActive;
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 BtnLoad_Click(null,null);
 }
 }

 private void BtnUpdate_Click(object sender, EventArgs e)
 {
 if (dgvAccounts.CurrentRow == null) return;
 var row = (dgvAccounts.CurrentRow.DataBoundItem as DataRowView)?.Row;
 if (row == null) return;
 var f = new AccountEditDialog
 {
 UserName = row["UserName"].ToString(),
 DisplayName = row["DisplayName"].ToString(),
 Group = row["Group"].ToString(),
 IsActive = Convert.ToBoolean(row["IsActive"]) 
 };
 if (f.ShowDialog(this) == DialogResult.OK)
 {
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "UPDATE dbo.Accounts SET DisplayName=@d, [Group]=@g, IsActive=@a WHERE UserName=@u";
 cmd.Parameters.Add("@u", SqlDbType.NVarChar,100).Value = f.UserName;
 cmd.Parameters.Add("@d", SqlDbType.NVarChar,200).Value = f.DisplayName;
 cmd.Parameters.Add("@g", SqlDbType.NVarChar,50).Value = f.Group;
 cmd.Parameters.Add("@a", SqlDbType.Bit).Value = f.IsActive;
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 BtnLoad_Click(null,null);
 }
 }

 private void BtnResetPwd_Click(object sender, EventArgs e)
 {
 if (dgvAccounts.CurrentRow == null) return;
 var row = (dgvAccounts.CurrentRow.DataBoundItem as DataRowView)?.Row;
 if (row == null) return;
 var userName = row["UserName"].ToString();
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "UPDATE dbo.Accounts SET Password=@p WHERE UserName=@u";
 cmd.Parameters.Add("@u", SqlDbType.NVarChar,100).Value = userName;
 cmd.Parameters.Add("@p", SqlDbType.NVarChar,256).Value = "123456"; // default
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 MessageBox.Show("Ð? reset m?t kh?u v?123456");
 }

 private void MnuDelete_Click(object sender, EventArgs e)
 {
 if (dgvAccounts.CurrentRow == null) return;
 var row = (dgvAccounts.CurrentRow.DataBoundItem as DataRowView)?.Row;
 if (row == null) return;
 int accountId = Convert.ToInt32(row["ID"]);
 using (var conn = new SqlConnection(ConnectionString))
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = "UPDATE dbo.AccountRoles SET IsActive=0 WHERE AccountID=@id";
 cmd.Parameters.Add("@id", SqlDbType.Int).Value = accountId;
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 MessageBox.Show("Ð? ðánh d?u không kích ho?t các vai tr? c?a tài kho?n.");
 }

 private void MnuViewRoles_Click(object sender, EventArgs e)
 {
 if (dgvAccounts.CurrentRow == null) return;
 var row = (dgvAccounts.CurrentRow.DataBoundItem as DataRowView)?.Row;
 if (row == null) return;
 int accountId = Convert.ToInt32(row["ID"]);
 var f = new AccountRolesForm();
 f.LoadRoles(accountId);
 f.Show(this);
 }
 }
}
