namespace BaiTapSoXoCacMien
{
    partial class frmChinh
    {
    
        private System.ComponentModel.IContainer components = null;

 
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

   
        private void InitializeComponent()
        {
            this.cboMien = new System.Windows.Forms.ComboBox();
            this.btnLayKetQua = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMien
            // 
            this.cboMien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMien.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cboMien.FormattingEnabled = true;
            this.cboMien.Location = new System.Drawing.Point(20, 20);
            this.cboMien.Name = "cboMien";
            this.cboMien.Size = new System.Drawing.Size(180, 36);
            this.cboMien.TabIndex = 0;
            // 
            // btnLayKetQua
            // 
            this.btnLayKetQua.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLayKetQua.Location = new System.Drawing.Point(220, 20);
            this.btnLayKetQua.Name = "btnLayKetQua";
            this.btnLayKetQua.Size = new System.Drawing.Size(140, 36);
            this.btnLayKetQua.TabIndex = 1;
            this.btnLayKetQua.Text = "Lấy Kết Quả";
            this.btnLayKetQua.UseVisualStyleBackColor = true;
            this.btnLayKetQua.Click += new System.EventHandler(this.btnLayKetQua_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(20, 70);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(799, 32);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "KẾT QUẢ XỔ SỐ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(20, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ngày công bố:";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKetQua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(20, 150);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.RowTemplate.Height = 30;
            this.dgvKetQua.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQua.Size = new System.Drawing.Size(799, 363);
            this.dgvKetQua.TabIndex = 6;
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(167, 112);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpNgay.TabIndex = 7;
            // 
            // frmChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 539);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLayKetQua);
            this.Controls.Add(this.cboMien);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmChinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ứng dụng Xổ Số 3 Miền";
            this.Load += new System.EventHandler(this.frmChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMien;
        private System.Windows.Forms.Button btnLayKetQua;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.DateTimePicker dtpNgay;
    }
}
