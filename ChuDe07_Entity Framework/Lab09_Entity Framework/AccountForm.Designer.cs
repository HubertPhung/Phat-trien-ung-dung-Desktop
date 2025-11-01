namespace Lab09_Entity_Framework
{
 partial class AccountForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvAccounts;
 private System.Windows.Forms.ComboBox cbbRoleFilter;
 private System.Windows.Forms.TextBox txtKeyword;
 private System.Windows.Forms.Button btnSearch;
 private System.Windows.Forms.Button btnAdd;
 private System.Windows.Forms.Button btnEdit;
 private System.Windows.Forms.Button btnResetPassword;
 private System.Windows.Forms.Button btnChangePassword;
 private System.Windows.Forms.ContextMenuStrip ctxMenu;
 private System.Windows.Forms.ToolStripMenuItem ctxDeleteAccount;
 private System.Windows.Forms.ToolStripMenuItem ctxViewRoles;

 protected override void Dispose(bool disposing)
 {
 if (disposing && (components != null))
 {
 components.Dispose();
 }
 base.Dispose(disposing);
 }

 private void InitializeComponent()
 {
            this.components = new System.ComponentModel.Container();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxDeleteAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxViewRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.cbbRoleFilter = new System.Windows.Forms.ComboBox();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.AllowUserToDeleteRows = false;
            this.dgvAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.ContextMenuStrip = this.ctxMenu;
            this.dgvAccounts.Location = new System.Drawing.Point(12, 48);
            this.dgvAccounts.MultiSelect = false;
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersWidth = 51;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(637, 330);
            this.dgvAccounts.TabIndex = 0;
            // 
            // ctxMenu
            // 
            this.ctxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxDeleteAccount,
            this.ctxViewRoles});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(225, 52);
            // 
            // ctxDeleteAccount
            // 
            this.ctxDeleteAccount.Name = "ctxDeleteAccount";
            this.ctxDeleteAccount.Size = new System.Drawing.Size(224, 24);
            this.ctxDeleteAccount.Text = "Xóa tài khoản";
            this.ctxDeleteAccount.Click += new System.EventHandler(this.ctxDeleteAccount_Click);
            // 
            // ctxViewRoles
            // 
            this.ctxViewRoles.Name = "ctxViewRoles";
            this.ctxViewRoles.Size = new System.Drawing.Size(224, 24);
            this.ctxViewRoles.Text = "Xem danh sách vai trò";
            this.ctxViewRoles.Click += new System.EventHandler(this.ctxViewRoles_Click);
            // 
            // cbbRoleFilter
            // 
            this.cbbRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRoleFilter.Location = new System.Drawing.Point(12, 12);
            this.cbbRoleFilter.Name = "cbbRoleFilter";
            this.cbbRoleFilter.Size = new System.Drawing.Size(180, 21);
            this.cbbRoleFilter.TabIndex = 1;
            this.cbbRoleFilter.SelectedIndexChanged += new System.EventHandler(this.cbbRoleFilter_SelectedIndexChanged);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(198, 12);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(180, 20);
            this.txtKeyword.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(384, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(484, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(540, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(50, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetPassword.Location = new System.Drawing.Point(596, 10);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(53, 23);
            this.btnResetPassword.TabIndex = 6;
            this.btnResetPassword.Text = "Reset";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.Location = new System.Drawing.Point(549, 381);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(100, 23);
            this.btnChangePassword.TabIndex = 7;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 411);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnResetPassword);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.cbbRoleFilter);
            this.Controls.Add(this.dgvAccounts);
            this.Name = "AccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý tài khoản";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

 }
 }
}
