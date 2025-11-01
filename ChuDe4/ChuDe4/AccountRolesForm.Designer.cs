namespace ChuDe4
{
 partial class AccountRolesForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvRoles;

 protected override void Dispose(bool disposing)
 {
 if (disposing && (components != null)) components.Dispose();
 base.Dispose(disposing);
 }

 private void InitializeComponent()
 {
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AllowUserToDeleteRows = false;
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoles.Location = new System.Drawing.Point(0, 0);
            this.dgvRoles.MultiSelect = false;
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.RowHeadersWidth = 51;
            this.dgvRoles.RowTemplate.Height = 24;
            this.dgvRoles.Size = new System.Drawing.Size(500, 400);
            this.dgvRoles.TabIndex = 0;
            // 
            // AccountRolesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.dgvRoles);
            this.Name = "AccountRolesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vai trò tài khoản";
            this.Load += new System.EventHandler(this.AccountRolesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.ResumeLayout(false);

 }
 }
}
