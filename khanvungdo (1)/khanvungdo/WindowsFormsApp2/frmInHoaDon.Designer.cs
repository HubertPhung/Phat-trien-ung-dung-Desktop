namespace WindowsFormsApp2
{
    partial class frmInHoaDon
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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

        /// <summary>
        /// Required method for Designer support
        /// do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSoHD = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(52, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HÓA ĐƠN THANH TOÁN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoHD
            // 
            this.lblSoHD.AutoSize = true;
            this.lblSoHD.Location = new System.Drawing.Point(30, 60);
            this.lblSoHD.Name = "lblSoHD";
            this.lblSoHD.Size = new System.Drawing.Size(93, 16);
            this.lblSoHD.TabIndex = 1;
            this.lblSoHD.Text = "Số HĐ: HD001";
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(400, 60);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(132, 16);
            this.lblNgayLap.TabIndex = 2;
            this.lblNgayLap.Text = "Ngày lập: 22/09/2025";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(30, 90);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(169, 16);
            this.lblKhachHang.TabIndex = 3;
            this.lblKhachHang.Text = "Khách hàng: Nguyễn Văn A";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(400, 120);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(116, 16);
            this.lblSDT.TabIndex = 4;
            this.lblSDT.Text = "SĐT: 0901 234 567";
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Location = new System.Drawing.Point(30, 120);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(92, 16);
            this.lblSoPhong.TabIndex = 5;
            this.lblSoPhong.Text = "Số phòng: 203";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Location = new System.Drawing.Point(33, 169);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 62;
            this.dgvChiTiet.Size = new System.Drawing.Size(497, 200);
            this.dgvChiTiet.TabIndex = 6;
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblThanhTien.ForeColor = System.Drawing.Color.Red;
            this.lblThanhTien.Location = new System.Drawing.Point(262, 392);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(270, 25);
            this.lblThanhTien.TabIndex = 7;
            this.lblThanhTien.Text = "THÀNH TIỀN: 50.000 VND";
            this.lblThanhTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Location = new System.Drawing.Point(32, 466);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(450, 40);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú: Cảm ơn quý khách đã sử dụng dịch vụ!";
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(200, 520);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(120, 35);
            this.btnIn.TabIndex = 11;
            this.btnIn.Text = "In hóa đơn";
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(360, 520);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 12;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ngày kết thúc: 22/09/2025";
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Location = new System.Drawing.Point(32, 415);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(86, 16);
            this.lblGiamGia.TabIndex = 5;
            this.lblGiamGia.Text = "Giảm giá: 0%";
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Location = new System.Drawing.Point(32, 392);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(113, 16);
            this.lblTongCong.TabIndex = 5;
            this.lblTongCong.Text = "Tổng cộng: 50000";
            // 
            // frmInHoaDon
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(590, 580);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.lblThanhTien);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.lblTongCong);
            this.Controls.Add(this.lblGiamGia);
            this.Controls.Add(this.lblSoPhong);
            this.Controls.Add(this.lblSDT);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.lblNgayLap);
            this.Controls.Add(this.lblSoHD);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInHoaDon";
            this.Text = "In Hóa Đơn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSoHD;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblTongCong;
    }
}