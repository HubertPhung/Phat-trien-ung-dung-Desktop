namespace Lab09_Entity_Framework
{
 partial class ChangePasswordForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.Label label1;
 private System.Windows.Forms.TextBox txtNew;
 private System.Windows.Forms.Label label2;
 private System.Windows.Forms.TextBox txtConfirm;
 private System.Windows.Forms.Button btnSave;

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
            this.txtNew = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu mới";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(154, 12);
            this.txtNew.Name = "txtNew";
            this.txtNew.PasswordChar = '*';
            this.txtNew.Size = new System.Drawing.Size(220, 27);
            this.txtNew.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Xác nhận mật khẩu";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(154, 41);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.Size = new System.Drawing.Size(220, 27);
            this.txtConfirm.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(299, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 111);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

 }
 }
}
