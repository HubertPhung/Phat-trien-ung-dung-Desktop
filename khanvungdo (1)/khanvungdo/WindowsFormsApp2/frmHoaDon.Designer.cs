namespace WindowsFormsApp2
{
    partial class frmHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvHoaDon = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpLapTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbPhong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gbDSHD = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cậpNhậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2GroupBox1.SuspendLayout();
            this.gbDSHD.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvHoaDon
            // 
            this.lvHoaDon.CheckBoxes = true;
            this.lvHoaDon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvHoaDon.ContextMenuStrip = this.contextMenuStrip1;
            this.lvHoaDon.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvHoaDon.GridLines = true;
            this.lvHoaDon.HideSelection = false;
            this.lvHoaDon.Location = new System.Drawing.Point(19, 29);
            this.lvHoaDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvHoaDon.Name = "lvHoaDon";
            this.lvHoaDon.Size = new System.Drawing.Size(1169, 467);
            this.lvHoaDon.TabIndex = 12;
            this.lvHoaDon.UseCompatibleStateImageBehavior = false;
            this.lvHoaDon.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mã HD";
            this.columnHeader1.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ngày Lập HĐ";
            this.columnHeader2.Width = 168;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ngày Kết Thúc";
            this.columnHeader3.Width = 123;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Mã Phòng";
            this.columnHeader4.Width = 127;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Tổng Tiền";
            this.columnHeader5.Width = 192;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Giảm Giá";
            this.columnHeader6.Width = 138;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Thành Tiền";
            this.columnHeader7.Width = 142;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Trạng Thái";
            this.columnHeader8.Width = 218;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Lập Từ Ngày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "Đến Ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(640, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 23);
            this.label4.TabIndex = 17;
            this.label4.Text = "Phòng";
            // 
            // dtpLapTuNgay
            // 
            this.dtpLapTuNgay.AutoRoundedCorners = true;
            this.dtpLapTuNgay.BorderThickness = 2;
            this.dtpLapTuNgay.Checked = true;
            this.dtpLapTuNgay.CheckedState.FillColor = System.Drawing.Color.Black;
            this.dtpLapTuNgay.CheckedState.ForeColor = System.Drawing.Color.White;
            this.dtpLapTuNgay.FillColor = System.Drawing.Color.White;
            this.dtpLapTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpLapTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpLapTuNgay.HoverState.FillColor = System.Drawing.Color.Black;
            this.dtpLapTuNgay.Location = new System.Drawing.Point(176, 65);
            this.dtpLapTuNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpLapTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpLapTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpLapTuNgay.Name = "dtpLapTuNgay";
            this.dtpLapTuNgay.Size = new System.Drawing.Size(271, 36);
            this.dtpLapTuNgay.TabIndex = 23;
            this.dtpLapTuNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtpLapTuNgay.Value = new System.DateTime(2025, 9, 21, 0, 38, 50, 571);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.AutoRoundedCorners = true;
            this.dtpDenNgay.BorderThickness = 2;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.CheckedState.FillColor = System.Drawing.Color.Black;
            this.dtpDenNgay.CheckedState.ForeColor = System.Drawing.Color.White;
            this.dtpDenNgay.FillColor = System.Drawing.Color.White;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDenNgay.HoverState.FillColor = System.Drawing.Color.Black;
            this.dtpDenNgay.Location = new System.Drawing.Point(176, 121);
            this.dtpDenNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(271, 36);
            this.dtpDenNgay.TabIndex = 22;
            this.dtpDenNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 21, 0, 38, 50, 571);
            // 
            // cbPhong
            // 
            this.cbPhong.BackColor = System.Drawing.Color.Transparent;
            this.cbPhong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhong.FocusedColor = System.Drawing.Color.Empty;
            this.cbPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbPhong.ItemHeight = 30;
            this.cbPhong.Items.AddRange(new object[] {
            "Spa"});
            this.cbPhong.Location = new System.Drawing.Point(731, 93);
            this.cbPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbPhong.Name = "cbPhong";
            this.cbPhong.Size = new System.Drawing.Size(219, 36);
            this.cbPhong.TabIndex = 21;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.cbPhong);
            this.guna2GroupBox1.Controls.Add(this.dtpDenNgay);
            this.guna2GroupBox1.Controls.Add(this.dtpLapTuNgay);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Controls.Add(this.label2);
            this.guna2GroupBox1.Controls.Add(this.label1);
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(44, 15);
            this.guna2GroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(1208, 174);
            this.guna2GroupBox1.TabIndex = 13;
            this.guna2GroupBox1.Text = "Chức năng";
            // 
            // gbDSHD
            // 
            this.gbDSHD.Controls.Add(this.lvHoaDon);
            this.gbDSHD.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDSHD.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gbDSHD.Location = new System.Drawing.Point(44, 203);
            this.gbDSHD.Margin = new System.Windows.Forms.Padding(4);
            this.gbDSHD.Name = "gbDSHD";
            this.gbDSHD.Padding = new System.Windows.Forms.Padding(4);
            this.gbDSHD.Size = new System.Drawing.Size(1208, 513);
            this.gbDSHD.TabIndex = 14;
            this.gbDSHD.TabStop = false;
            this.gbDSHD.Text = "Danh sách hóa đơn";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem,
            this.cậpNhậpToolStripMenuItem,
            this.thêmToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 104);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.xóaToolStripMenuItem.Text = "Xóa";
            // 
            // cậpNhậpToolStripMenuItem
            // 
            this.cậpNhậpToolStripMenuItem.Name = "cậpNhậpToolStripMenuItem";
            this.cậpNhậpToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.cậpNhậpToolStripMenuItem.Text = "Cập Nhật";
            this.cậpNhậpToolStripMenuItem.Click += new System.EventHandler(this.cậpNhậpToolStripMenuItem_Click);
            // 
            // thêmToolStripMenuItem
            // 
            this.thêmToolStripMenuItem.Name = "thêmToolStripMenuItem";
            this.thêmToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.thêmToolStripMenuItem.Text = "Thêm";
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1315, 814);
            this.Controls.Add(this.gbDSHD);
            this.Controls.Add(this.guna2GroupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmHoaDon";
            this.Text = "frmHoaDon";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.gbDSHD.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvHoaDon;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpLapTuNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2ComboBox cbPhong;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.GroupBox gbDSHD;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cậpNhậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thêmToolStripMenuItem;
    }
}