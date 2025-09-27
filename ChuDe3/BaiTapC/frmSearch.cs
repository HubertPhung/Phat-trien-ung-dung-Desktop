using System;
using System.Windows.Forms;

namespace BaiTapC
{
    public partial class frmSearch : Form
    {
        private System.Windows.Forms.Label lblMssv;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.TextBox txtMssv;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtLop;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        public string MSSV => txtMssv.Text.Trim();
        public string Ten => txtTen.Text.Trim();
        public string Lop => txtLop.Text.Trim();

        public frmSearch()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblMssv = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblLop = new System.Windows.Forms.Label();
            this.txtMssv = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtLop = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMssv
            // 
            this.lblMssv.AutoSize = true;
            this.lblMssv.Location = new System.Drawing.Point(20, 20);
            this.lblMssv.Name = "lblMssv";
            this.lblMssv.Size = new System.Drawing.Size(49, 17);
            this.lblMssv.TabIndex = 0;
            this.lblMssv.Text = "MSSV:";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(20, 52);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(35, 17);
            this.lblTen.TabIndex = 1;
            this.lblTen.Text = "Tên:";
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Location = new System.Drawing.Point(20, 84);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(35, 17);
            this.lblLop.TabIndex = 2;
            this.lblLop.Text = "Lớp:";
            // 
            // txtMssv
            // 
            this.txtMssv.Location = new System.Drawing.Point(90, 17);
            this.txtMssv.Name = "txtMssv";
            this.txtMssv.Size = new System.Drawing.Size(280, 22);
            this.txtMssv.TabIndex = 3;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(90, 49);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(280, 22);
            this.txtTen.TabIndex = 4;
            // 
            // txtLop
            // 
            this.txtLop.Location = new System.Drawing.Point(90, 81);
            this.txtLop.Name = "txtLop";
            this.txtLop.Size = new System.Drawing.Size(280, 22);
            this.txtLop.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(215, 120);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Tìm";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSearch
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 168);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtLop);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtMssv);
            this.Controls.Add(this.lblLop);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.lblMssv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm kiếm sinh viên";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
