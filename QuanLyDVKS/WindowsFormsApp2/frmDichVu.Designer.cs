namespace WindowsFormsApp2
{
    partial class frmDichVu
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
            System.Windows.Forms.GroupBox gbDSDV;
            this.lvDSDV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.gbChucNang = new Guna.UI2.WinForms.Guna2GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLuuY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.txtTenDV = new System.Windows.Forms.TextBox();
            this.btnTim = new Guna.UI2.WinForms.Guna2Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiXoa1DV = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXoaNhieuDV = new System.Windows.Forms.ToolStripMenuItem();
            gbDSDV = new System.Windows.Forms.GroupBox();
            gbDSDV.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDSDV
            // 
            gbDSDV.Controls.Add(this.lvDSDV);
            gbDSDV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            gbDSDV.Location = new System.Drawing.Point(109, 260);
            gbDSDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            gbDSDV.Name = "gbDSDV";
            gbDSDV.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            gbDSDV.Size = new System.Drawing.Size(1176, 439);
            gbDSDV.TabIndex = 47;
            gbDSDV.TabStop = false;
            gbDSDV.Text = "Danh sách dịch vụ";
            // 
            // lvDSDV
            // 
            this.lvDSDV.CheckBoxes = true;
            this.lvDSDV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader8,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvDSDV.ContextMenuStrip = this.contextMenuStrip1;
            this.lvDSDV.FullRowSelect = true;
            this.lvDSDV.GridLines = true;
            this.lvDSDV.HideSelection = false;
            this.lvDSDV.Location = new System.Drawing.Point(6, 27);
            this.lvDSDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvDSDV.Name = "lvDSDV";
            this.lvDSDV.Size = new System.Drawing.Size(1164, 400);
            this.lvDSDV.TabIndex = 40;
            this.lvDSDV.UseCompatibleStateImageBehavior = false;
            this.lvDSDV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "STT";
            // 
            // columnHeader8
            // 
            this.columnHeader8.DisplayIndex = 2;
            this.columnHeader8.Text = "Hình";
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 1;
            this.columnHeader2.Text = "Mã DV";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 102;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tên Dịch Vụ";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 156;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Loại Dịch Vụ";
            this.columnHeader7.Width = 163;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Đơn giá";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 156;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ĐVT";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 118;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Lưu ý";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 150;
            // 
            // btnThem
            // 
            this.btnThem.AutoRoundedCorners = true;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(978, 74);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(128, 50);
            this.btnThem.TabIndex = 48;
            this.btnThem.Text = "Thêm";
            // 
            // guna2Button1
            // 
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(978, 157);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(128, 50);
            this.guna2Button1.TabIndex = 48;
            this.guna2Button1.Text = "Cập nhật";
            // 
            // btnXoa
            // 
            this.btnXoa.AutoRoundedCorners = true;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(783, 74);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(128, 50);
            this.btnXoa.TabIndex = 48;
            this.btnXoa.Text = "Xóa";
            // 
            // gbChucNang
            // 
            this.gbChucNang.Controls.Add(this.button1);
            this.gbChucNang.Controls.Add(this.textBox1);
            this.gbChucNang.Controls.Add(this.comboBox1);
            this.gbChucNang.Controls.Add(this.txtDonGia);
            this.gbChucNang.Controls.Add(this.txtDVT);
            this.gbChucNang.Controls.Add(this.label5);
            this.gbChucNang.Controls.Add(this.txtLuuY);
            this.gbChucNang.Controls.Add(this.label4);
            this.gbChucNang.Controls.Add(this.label3);
            this.gbChucNang.Controls.Add(this.label7);
            this.gbChucNang.Controls.Add(this.label6);
            this.gbChucNang.Controls.Add(this.label2);
            this.gbChucNang.Controls.Add(this.label1);
            this.gbChucNang.Controls.Add(this.txtMaDV);
            this.gbChucNang.Controls.Add(this.txtTenDV);
            this.gbChucNang.Controls.Add(this.btnXoa);
            this.gbChucNang.Controls.Add(this.guna2Button1);
            this.gbChucNang.Controls.Add(this.btnTim);
            this.gbChucNang.Controls.Add(this.btnThem);
            this.gbChucNang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbChucNang.Location = new System.Drawing.Point(109, 15);
            this.gbChucNang.Margin = new System.Windows.Forms.Padding(4);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(1179, 245);
            this.gbChucNang.TabIndex = 50;
            this.gbChucNang.Text = "Chức năng";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 28);
            this.button1.TabIndex = 57;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(122, 198);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 56;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(122, 157);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 28);
            this.comboBox1.TabIndex = 55;
            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(483, 63);
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(162, 27);
            this.txtDonGia.TabIndex = 54;
            this.txtDonGia.TextChanged += new System.EventHandler(this.TxtDonGia_TextChanged);
            this.txtDonGia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDonGia_KeyPress);
            this.txtDonGia.Leave += new System.EventHandler(this.TxtDonGia_Leave);
            // 
            // txtDVT
            // 
            this.txtDVT.Location = new System.Drawing.Point(483, 106);
            this.txtDVT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDVT.Name = "txtDVT";
            this.txtDVT.Size = new System.Drawing.Size(162, 27);
            this.txtDVT.TabIndex = 54;
            this.txtDVT.TextChanged += new System.EventHandler(this.TxtDVT_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(376, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 49;
            this.label5.Text = "Đơn vị tính";
            // 
            // txtLuuY
            // 
            this.txtLuuY.Location = new System.Drawing.Point(483, 158);
            this.txtLuuY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLuuY.Name = "txtLuuY";
            this.txtLuuY.Size = new System.Drawing.Size(162, 27);
            this.txtLuuY.TabIndex = 54;
            this.txtLuuY.TextChanged += new System.EventHandler(this.TxtLuuY_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(378, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 23);
            this.label4.TabIndex = 49;
            this.label4.Text = "Lưu ý";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(378, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 23);
            this.label3.TabIndex = 49;
            this.label3.Text = "Đơn giá";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(20, 198);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 23);
            this.label7.TabIndex = 49;
            this.label7.Text = "Hình";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(20, 158);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 23);
            this.label6.TabIndex = 49;
            this.label6.Text = "Loại DV";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 23);
            this.label2.TabIndex = 49;
            this.label2.Text = "Tên DV";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 49;
            this.label1.Text = "Mã DV";
            // 
            // txtMaDV
            // 
            this.txtMaDV.Location = new System.Drawing.Point(122, 59);
            this.txtMaDV.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.ReadOnly = true;
            this.txtMaDV.Size = new System.Drawing.Size(165, 27);
            this.txtMaDV.TabIndex = 51;
            this.txtMaDV.TextChanged += new System.EventHandler(this.TxtMaDV_TextChanged);
            // 
            // txtTenDV
            // 
            this.txtTenDV.Location = new System.Drawing.Point(122, 106);
            this.txtTenDV.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Size = new System.Drawing.Size(165, 27);
            this.txtTenDV.TabIndex = 52;
            this.txtTenDV.TextChanged += new System.EventHandler(this.TxtTenDV_TextChanged);
            // 
            // btnTim
            // 
            this.btnTim.AutoRoundedCorners = true;
            this.btnTim.FillColor = System.Drawing.Color.Black;
            this.btnTim.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.ForeColor = System.Drawing.Color.White;
            this.btnTim.Location = new System.Drawing.Point(783, 157);
            this.btnTim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(128, 50);
            this.btnTim.TabIndex = 48;
            this.btnTim.Text = "Tìm";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXoa1DV,
            this.tsmiXoaNhieuDV});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(196, 52);
            // 
            // tsmiXoa1DV
            // 
            this.tsmiXoa1DV.Name = "tsmiXoa1DV";
            this.tsmiXoa1DV.Size = new System.Drawing.Size(195, 24);
            this.tsmiXoa1DV.Text = "Xóa 1 dịch vụ";
            // 
            // tsmiXoaNhieuDV
            // 
            this.tsmiXoaNhieuDV.Name = "tsmiXoaNhieuDV";
            this.tsmiXoaNhieuDV.Size = new System.Drawing.Size(195, 24);
            this.tsmiXoaNhieuDV.Text = "Xóa nhiều dịch vụ";
            // 
            // frmDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1315, 814);
            this.Controls.Add(this.gbChucNang);
            this.Controls.Add(gbDSDV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDichVu";
            this.Text = "frmDichVu";
            gbDSDV.ResumeLayout(false);
            this.gbChucNang.ResumeLayout(false);
            this.gbChucNang.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            // 
            // frmDichVu
            // 
            this.Load += new System.EventHandler(this.FrmDichVu_Load);
            this.lvDSDV.SelectedIndexChanged += new System.EventHandler(this.LvDSDV_SelectedIndexChanged);
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            this.btnTim.Click += new System.EventHandler(this.BtnTim_Click);
            this.guna2Button1.Click += new System.EventHandler(this.BtnCapNhat_Click);
            this.button1.Click += new System.EventHandler(this.BtnChonHinh_Click);
            this.txtMaDV.TextChanged += new System.EventHandler(this.TxtMaDV_TextChanged);
            this.txtTenDV.TextChanged += new System.EventHandler(this.TxtTenDV_TextChanged);
            this.txtDonGia.TextChanged += new System.EventHandler(this.TxtDonGia_TextChanged);
            this.txtDonGia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDonGia_KeyPress);
            this.txtDonGia.Leave += new System.EventHandler(this.TxtDonGia_Leave);
            this.txtDVT.TextChanged += new System.EventHandler(this.TxtDVT_TextChanged);
            this.txtLuuY.TextChanged += new System.EventHandler(this.TxtLuuY_TextChanged);
            this.tsmiXoa1DV.Click += new System.EventHandler(this.DeleteOneServiceFromContext);
            this.tsmiXoaNhieuDV.Click += new System.EventHandler(this.DeleteManyServicesFromContext);
        }

        #endregion
        private System.Windows.Forms.ListView lvDSDV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        private System.Windows.Forms.TextBox txtLuuY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.TextBox txtTenDV;
        private Guna.UI2.WinForms.Guna2Button btnTim;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiXoa1DV;
        private System.Windows.Forms.ToolStripMenuItem tsmiXoaNhieuDV;
    }
}