namespace Lab09_Entity_Framework
{
partial class EmployeeForm
{
private System.ComponentModel.IContainer components = null;
private System.Windows.Forms.Label lblUsername;
private System.Windows.Forms.Label lblDisplayName;
private System.Windows.Forms.Label lblEmail;
private System.Windows.Forms.Label lblTel;
private System.Windows.Forms.Label lblGroup;
private System.Windows.Forms.Label lblActive;
private System.Windows.Forms.Label lblRoles;
private System.Windows.Forms.Label lblUsernameValue;
private System.Windows.Forms.Label lblDisplayNameValue;
private System.Windows.Forms.Label lblEmailValue;
private System.Windows.Forms.Label lblTelValue;
private System.Windows.Forms.Label lblGroupValue;
private System.Windows.Forms.Label lblActiveValue;
private System.Windows.Forms.Label lblRolesValue;
private System.Windows.Forms.Button btnClose;

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
this.lblUsername = new System.Windows.Forms.Label();
this.lblDisplayName = new System.Windows.Forms.Label();
this.lblEmail = new System.Windows.Forms.Label();
this.lblTel = new System.Windows.Forms.Label();
this.lblGroup = new System.Windows.Forms.Label();
this.lblActive = new System.Windows.Forms.Label();
this.lblRoles = new System.Windows.Forms.Label();
this.lblUsernameValue = new System.Windows.Forms.Label();
this.lblDisplayNameValue = new System.Windows.Forms.Label();
this.lblEmailValue = new System.Windows.Forms.Label();
this.lblTelValue = new System.Windows.Forms.Label();
this.lblGroupValue = new System.Windows.Forms.Label();
this.lblActiveValue = new System.Windows.Forms.Label();
this.lblRolesValue = new System.Windows.Forms.Label();
this.btnClose = new System.Windows.Forms.Button();
this.SuspendLayout();
// 
// Static labels
// 
this.lblUsername.AutoSize = true;
this.lblUsername.Location = new System.Drawing.Point(12, 15);
this.lblUsername.Name = "lblUsername";
this.lblUsername.Size = new System.Drawing.Size(88, 15);
this.lblUsername.Text = "Tên ðãng nh?p:";
this.lblUsername.UseCompatibleTextRendering = true;
this.lblDisplayName.AutoSize = true;
this.lblDisplayName.Location = new System.Drawing.Point(12, 45);
this.lblDisplayName.Name = "lblDisplayName";
this.lblDisplayName.Size = new System.Drawing.Size(78, 15);
this.lblDisplayName.Text = "Tên hi?n th?:";
this.lblDisplayName.UseCompatibleTextRendering = true;
this.lblEmail.AutoSize = true;
this.lblEmail.Location = new System.Drawing.Point(12, 75);
this.lblEmail.Name = "lblEmail";
this.lblEmail.Size = new System.Drawing.Size(39, 15);
this.lblEmail.Text = "Email:";
this.lblEmail.UseCompatibleTextRendering = true;
this.lblTel.AutoSize = true;
this.lblTel.Location = new System.Drawing.Point(12, 105);
this.lblTel.Name = "lblTel";
this.lblTel.Size = new System.Drawing.Size(68, 15);
this.lblTel.Text = "Ði?n tho?i:";
this.lblTel.UseCompatibleTextRendering = true;
this.lblGroup.AutoSize = true;
this.lblGroup.Location = new System.Drawing.Point(12, 135);
this.lblGroup.Name = "lblGroup";
this.lblGroup.Size = new System.Drawing.Size(50, 15);
this.lblGroup.Text = "Nhóm:";
this.lblGroup.UseCompatibleTextRendering = true;
this.lblActive.AutoSize = true;
this.lblActive.Location = new System.Drawing.Point(12, 165);
this.lblActive.Name = "lblActive";
this.lblActive.Size = new System.Drawing.Size(64, 15);
this.lblActive.Text = "Tr?ng thái:";
this.lblActive.UseCompatibleTextRendering = true;
this.lblRoles.AutoSize = true;
this.lblRoles.Location = new System.Drawing.Point(12, 195);
this.lblRoles.Name = "lblRoles";
this.lblRoles.Size = new System.Drawing.Size(57, 15);
this.lblRoles.Text = "Vai tr?:";
this.lblRoles.UseCompatibleTextRendering = true;
// 
// Value labels
// 
int x = 120;
this.lblUsernameValue.AutoSize = true;
this.lblUsernameValue.Location = new System.Drawing.Point(x, 15);
this.lblUsernameValue.Name = "lblUsernameValue";
this.lblUsernameValue.UseCompatibleTextRendering = true;
this.lblDisplayNameValue.AutoSize = true;
this.lblDisplayNameValue.Location = new System.Drawing.Point(x, 45);
this.lblDisplayNameValue.Name = "lblDisplayNameValue";
this.lblDisplayNameValue.UseCompatibleTextRendering = true;
this.lblEmailValue.AutoSize = true;
this.lblEmailValue.Location = new System.Drawing.Point(x, 75);
this.lblEmailValue.Name = "lblEmailValue";
this.lblEmailValue.UseCompatibleTextRendering = true;
this.lblTelValue.AutoSize = true;
this.lblTelValue.Location = new System.Drawing.Point(x, 105);
this.lblTelValue.Name = "lblTelValue";
this.lblTelValue.UseCompatibleTextRendering = true;
this.lblGroupValue.AutoSize = true;
this.lblGroupValue.Location = new System.Drawing.Point(x, 135);
this.lblGroupValue.Name = "lblGroupValue";
this.lblGroupValue.UseCompatibleTextRendering = true;
this.lblActiveValue.AutoSize = true;
this.lblActiveValue.Location = new System.Drawing.Point(x, 165);
this.lblActiveValue.Name = "lblActiveValue";
this.lblActiveValue.UseCompatibleTextRendering = true;
this.lblRolesValue.AutoSize = true;
this.lblRolesValue.Location = new System.Drawing.Point(x, 195);
this.lblRolesValue.Name = "lblRolesValue";
this.lblRolesValue.MaximumSize = new System.Drawing.Size(350, 0);
this.lblRolesValue.UseCompatibleTextRendering = true;
// 
// btnClose
// 
this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
this.btnClose.Location = new System.Drawing.Point(395, 235);
this.btnClose.Name = "btnClose";
this.btnClose.Size = new System.Drawing.Size(75, 23);
this.btnClose.TabIndex = 99;
this.btnClose.Text = "Ðóng";
this.btnClose.UseCompatibleTextRendering = true;
this.btnClose.UseVisualStyleBackColor = true;
this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
// 
// EmployeeForm
// 
this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
this.ClientSize = new System.Drawing.Size(482, 270);
this.Controls.Add(this.btnClose);
this.Controls.Add(this.lblRolesValue);
this.Controls.Add(this.lblActiveValue);
this.Controls.Add(this.lblGroupValue);
this.Controls.Add(this.lblTelValue);
this.Controls.Add(this.lblEmailValue);
this.Controls.Add(this.lblDisplayNameValue);
this.Controls.Add(this.lblUsernameValue);
this.Controls.Add(this.lblRoles);
this.Controls.Add(this.lblActive);
this.Controls.Add(this.lblGroup);
this.Controls.Add(this.lblTel);
this.Controls.Add(this.lblEmail);
this.Controls.Add(this.lblDisplayName);
this.Controls.Add(this.lblUsername);
this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
this.Name = "EmployeeForm";
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
this.Text = "Thông tin nhân viên";
this.Load += new System.EventHandler(this.EmployeeForm_Load);
this.ResumeLayout(false);
this.PerformLayout();
}
}
}
