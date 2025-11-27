namespace WindowsFormsApp2
{
    partial class frmDoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lvDoanhThu = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbKhoangTG = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnIn = new Guna.UI2.WinForms.Guna2Button();
            this.btnXem = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txtTongDoanhThu = new System.Windows.Forms.TextBox();
            this.txtTienGiam = new System.Windows.Forms.TextBox();
            this.txtTongTienDichVu = new System.Windows.Forms.TextBox();
            this.txtTongDichVu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.chartDanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbKhoangTG.SuspendLayout();
            this.guna2GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // lvDoanhThu
            // 
            this.lvDoanhThu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvDoanhThu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDoanhThu.FullRowSelect = true;
            this.lvDoanhThu.GridLines = true;
            this.lvDoanhThu.HideSelection = false;
            this.lvDoanhThu.Location = new System.Drawing.Point(581, 52);
            this.lvDoanhThu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvDoanhThu.Name = "lvDoanhThu";
            this.lvDoanhThu.Size = new System.Drawing.Size(707, 242);
            this.lvDoanhThu.TabIndex = 11;
            this.lvDoanhThu.UseCompatibleStateImageBehavior = false;
            this.lvDoanhThu.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mã dịch vụ";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tên dịch vụ";
            this.columnHeader2.Width = 148;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Số lượng";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DVT";
            this.columnHeader4.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Thành tiền";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ghi Chú";
            this.columnHeader6.Width = 200;
            // 
            // gbKhoangTG
            // 
            this.gbKhoangTG.Controls.Add(this.dtpDenNgay);
            this.gbKhoangTG.Controls.Add(this.dtpTuNgay);
            this.gbKhoangTG.Controls.Add(this.btnIn);
            this.gbKhoangTG.Controls.Add(this.btnXem);
            this.gbKhoangTG.Controls.Add(this.label2);
            this.gbKhoangTG.Controls.Add(this.label1);
            this.gbKhoangTG.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbKhoangTG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbKhoangTG.Location = new System.Drawing.Point(76, 52);
            this.gbKhoangTG.Margin = new System.Windows.Forms.Padding(4);
            this.gbKhoangTG.Name = "gbKhoangTG";
            this.gbKhoangTG.Size = new System.Drawing.Size(431, 242);
            this.gbKhoangTG.TabIndex = 12;
            this.gbKhoangTG.Text = "Chọn khoảng thời gian";
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
            this.dtpDenNgay.Location = new System.Drawing.Point(129, 121);
            this.dtpDenNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(271, 36);
            this.dtpDenNgay.TabIndex = 17;
            this.dtpDenNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 21, 0, 38, 50, 571);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.AutoRoundedCorners = true;
            this.dtpTuNgay.BorderThickness = 2;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.CheckedState.FillColor = System.Drawing.Color.Black;
            this.dtpTuNgay.CheckedState.ForeColor = System.Drawing.Color.White;
            this.dtpTuNgay.FillColor = System.Drawing.Color.White;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTuNgay.HoverState.FillColor = System.Drawing.Color.Black;
            this.dtpTuNgay.Location = new System.Drawing.Point(129, 79);
            this.dtpTuNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(271, 36);
            this.dtpTuNgay.TabIndex = 18;
            this.dtpTuNgay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 21, 0, 38, 50, 571);
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.guna2DateTimePicker2_ValueChanged);
            // 
            // btnIn
            // 
            this.btnIn.AutoRoundedCorners = true;
            this.btnIn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnIn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(241, 172);
            this.btnIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(112, 50);
            this.btnIn.TabIndex = 19;
            this.btnIn.Text = "In";
            // 
            // btnXem
            // 
            this.btnXem.AutoRoundedCorners = true;
            this.btnXem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(57, 172);
            this.btnXem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(117, 50);
            this.btnXem.TabIndex = 20;
            this.btnXem.Text = "Xem";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 128);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Từ ngày:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.Controls.Add(this.txtTongDoanhThu);
            this.guna2GroupBox2.Controls.Add(this.txtTienGiam);
            this.guna2GroupBox2.Controls.Add(this.txtTongTienDichVu);
            this.guna2GroupBox2.Controls.Add(this.txtTongDichVu);
            this.guna2GroupBox2.Controls.Add(this.label6);
            this.guna2GroupBox2.Controls.Add(this.label9);
            this.guna2GroupBox2.Controls.Add(this.label8);
            this.guna2GroupBox2.Controls.Add(this.label7);
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox2.Location = new System.Drawing.Point(76, 377);
            this.guna2GroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(431, 241);
            this.guna2GroupBox2.TabIndex = 13;
            this.guna2GroupBox2.Text = "Thống kê";
            // 
            // txtTongDoanhThu
            // 
            this.txtTongDoanhThu.Location = new System.Drawing.Point(197, 182);
            this.txtTongDoanhThu.Margin = new System.Windows.Forms.Padding(4);
            this.txtTongDoanhThu.Name = "txtTongDoanhThu";
            this.txtTongDoanhThu.ReadOnly = true;
            this.txtTongDoanhThu.Size = new System.Drawing.Size(215, 27);
            this.txtTongDoanhThu.TabIndex = 15;
            // 
            // txtTienGiam
            // 
            this.txtTienGiam.Location = new System.Drawing.Point(197, 148);
            this.txtTienGiam.Margin = new System.Windows.Forms.Padding(4);
            this.txtTienGiam.Name = "txtTienGiam";
            this.txtTienGiam.ReadOnly = true;
            this.txtTienGiam.Size = new System.Drawing.Size(215, 27);
            this.txtTienGiam.TabIndex = 14;
            // 
            // txtTongTienDichVu
            // 
            this.txtTongTienDichVu.Location = new System.Drawing.Point(197, 113);
            this.txtTongTienDichVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtTongTienDichVu.Name = "txtTongTienDichVu";
            this.txtTongTienDichVu.ReadOnly = true;
            this.txtTongTienDichVu.Size = new System.Drawing.Size(215, 27);
            this.txtTongTienDichVu.TabIndex = 13;
            // 
            // txtTongDichVu
            // 
            this.txtTongDichVu.Location = new System.Drawing.Point(197, 79);
            this.txtTongDichVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtTongDichVu.Name = "txtTongDichVu";
            this.txtTongDichVu.ReadOnly = true;
            this.txtTongDichVu.Size = new System.Drawing.Size(215, 27);
            this.txtTongDichVu.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 186);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Tổng doanh thu:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 151);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 23);
            this.label9.TabIndex = 10;
            this.label9.Text = "Tiền giảm:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 117);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 23);
            this.label8.TabIndex = 9;
            this.label8.Text = "Tổng tiền dịch vụ:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tổng dịch vụ:";
            // 
            // chartDanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDanhThu.Legends.Add(legend1);
            this.chartDanhThu.Location = new System.Drawing.Point(581, 377);
            this.chartDanhThu.Name = "chartDanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDanhThu.Series.Add(series1);
            this.chartDanhThu.Size = new System.Drawing.Size(707, 237);
            this.chartDanhThu.TabIndex = 14;
            this.chartDanhThu.Text = "chart1";
            // 
            // frmDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1315, 814);
            this.Controls.Add(this.chartDanhThu);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.gbKhoangTG);
            this.Controls.Add(this.lvDoanhThu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDoanhThu";
            this.Text = "frmDoanhThu";
            this.Load += new System.EventHandler(this.frmDoanhThu_Load);
            this.gbKhoangTG.ResumeLayout(false);
            this.gbKhoangTG.PerformLayout();
            this.guna2GroupBox2.ResumeLayout(false);
            this.guna2GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDanhThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvDoanhThu;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private Guna.UI2.WinForms.Guna2GroupBox gbKhoangTG;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private Guna.UI2.WinForms.Guna2Button btnIn;
        private Guna.UI2.WinForms.Guna2Button btnXem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private System.Windows.Forms.TextBox txtTongDoanhThu;
        private System.Windows.Forms.TextBox txtTienGiam;
        private System.Windows.Forms.TextBox txtTongTienDichVu;
        private System.Windows.Forms.TextBox txtTongDichVu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDanhThu;
    }
}