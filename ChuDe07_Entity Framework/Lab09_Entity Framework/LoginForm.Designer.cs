namespace Lab09_Entity_Framework
{
partial class LoginForm
{
private System.ComponentModel.IContainer components = null;
private System.Windows.Forms.Label label1;
private System.Windows.Forms.TextBox txtUser;
private System.Windows.Forms.Label label2;
private System.Windows.Forms.TextBox txtPass;
private System.Windows.Forms.Button btnLogin;
private System.Windows.Forms.Button btnCancel;

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
this.label1 = new System.Windows.Forms.Label();
this.txtUser = new System.Windows.Forms.TextBox();
this.label2 = new System.Windows.Forms.Label();
this.txtPass = new System.Windows.Forms.TextBox();
this.btnLogin = new System.Windows.Forms.Button();
this.btnCancel = new System.Windows.Forms.Button();
this.SuspendLayout();
// 
// label1
// 
this.label1.AutoSize = true;
this.label1.Location = new System.Drawing.Point(12, 15);
this.label1.Name = "label1";
this.label1.Size = new System.Drawing.Size(87, 13);
this.label1.TabIndex = 0;
this.label1.Text = "Tên ðãng nh?p";
this.label1.UseCompatibleTextRendering = true;
// 
// txtUser
// 
this.txtUser.Location = new System.Drawing.Point(120, 12);
this.txtUser.Name = "txtUser";
this.txtUser.Size = new System.Drawing.Size(200, 20);
this.txtUser.TabIndex = 1;
// 
// label2
// 
this.label2.AutoSize = true;
this.label2.Location = new System.Drawing.Point(12, 44);
this.label2.Name = "label2";
this.label2.Size = new System.Drawing.Size(56, 13);
this.label2.TabIndex = 2;
this.label2.Text = "M?t kh?u";
this.label2.UseCompatibleTextRendering = true;
// 
// txtPass
// 
this.txtPass.Location = new System.Drawing.Point(120, 41);
this.txtPass.Name = "txtPass";
this.txtPass.PasswordChar = '*';
this.txtPass.Size = new System.Drawing.Size(200, 20);
this.txtPass.TabIndex = 3;
// 
// btnLogin
// 
this.btnLogin.Location = new System.Drawing.Point(164, 75);
this.btnLogin.Name = "btnLogin";
this.btnLogin.Size = new System.Drawing.Size(75, 23);
this.btnLogin.TabIndex = 4;
this.btnLogin.Text = "Ðãng nh?p";
this.btnLogin.UseCompatibleTextRendering = true;
this.btnLogin.UseVisualStyleBackColor = true;
this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
// 
// btnCancel
// 
this.btnCancel.Location = new System.Drawing.Point(245, 75);
this.btnCancel.Name = "btnCancel";
this.btnCancel.Size = new System.Drawing.Size(75, 23);
this.btnCancel.TabIndex = 5;
this.btnCancel.Text = "H?y";
this.btnCancel.UseCompatibleTextRendering = true;
this.btnCancel.UseVisualStyleBackColor = true;
this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
// 
// LoginForm
// 
this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
this.ClientSize = new System.Drawing.Size(339, 111);
this.Controls.Add(this.btnCancel);
this.Controls.Add(this.btnLogin);
this.Controls.Add(this.txtPass);
this.Controls.Add(this.label2);
this.Controls.Add(this.txtUser);
this.Controls.Add(this.label1);
this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
this.MaximizeBox = false;
this.MinimizeBox = false;
this.Name = "LoginForm";
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
this.Text = "Ðãng nh?p";
this.AcceptButton = this.btnLogin;
this.CancelButton = this.btnCancel;
this.ResumeLayout(false);
this.PerformLayout();
}
}
}
