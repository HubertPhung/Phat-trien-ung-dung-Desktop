namespace BaiTap_C
{
    partial class frmThongTinSV
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHoTenLot = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rdNam = new System.Windows.Forms.RadioButton();
            this.rdNu = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.cbLop = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clbMonHoc = new System.Windows.Forms.CheckedListBox();
            this.cmsXoaMonHoc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiXoa1Mon = new System.Windows.Forms.ToolStripMenuItem();
            this.tmstXoaNhieuMon = new System.Windows.Forms.ToolStripMenuItem();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDSSV = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMSSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTenLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCMND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsXoaSV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsiXoa1SV = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiXoaNhieuSV = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.mtxtSoDT = new System.Windows.Forms.MaskedTextBox();
            this.mtxtMSSV = new System.Windows.Forms.MaskedTextBox();
            this.mtxtCMND = new System.Windows.Forms.MaskedTextBox();
            this.cmsXoaMonHoc.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSSV)).BeginInit();
            this.cmsXoaSV.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(403, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Họ và tên lót : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày sinh : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số CMND : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Địa chỉ liên lạc :  ";
            // 
            // txtHoTenLot
            // 
            this.txtHoTenLot.Location = new System.Drawing.Point(154, 51);
            this.txtHoTenLot.Name = "txtHoTenLot";
            this.txtHoTenLot.Size = new System.Drawing.Size(211, 22);
            this.txtHoTenLot.TabIndex = 1;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(154, 89);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(210, 22);
            this.dtpNgaySinh.TabIndex = 3;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(154, 154);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(539, 22);
            this.txtDiaChi.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Giới tính : ";
            // 
            // rdNam
            // 
            this.rdNam.AutoSize = true;
            this.rdNam.Checked = true;
            this.rdNam.Location = new System.Drawing.Point(482, 20);
            this.rdNam.Name = "rdNam";
            this.rdNam.Size = new System.Drawing.Size(57, 20);
            this.rdNam.TabIndex = 3;
            this.rdNam.TabStop = true;
            this.rdNam.Text = "Nam";
            this.rdNam.UseVisualStyleBackColor = true;
            // 
            // rdNu
            // 
            this.rdNu.AutoSize = true;
            this.rdNu.Location = new System.Drawing.Point(555, 20);
            this.rdNu.Name = "rdNu";
            this.rdNu.Size = new System.Drawing.Size(45, 20);
            this.rdNu.TabIndex = 3;
            this.rdNu.Text = "Nữ";
            this.rdNu.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(403, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Lớp : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(403, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Số ĐT : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "MSSV : ";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(482, 51);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(211, 22);
            this.txtTen.TabIndex = 2;
            // 
            // cbLop
            // 
            this.cbLop.FormattingEnabled = true;
            this.cbLop.Items.AddRange(new object[] {
            "CTK44",
            "CTK45",
            "CTK46",
            "CTK47",
            "CTK48",
            "CTK49"});
            this.cbLop.Location = new System.Drawing.Point(482, 86);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(211, 24);
            this.cbLop.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Môn học đăng ký : ";
            // 
            // clbMonHoc
            // 
            this.clbMonHoc.ContextMenuStrip = this.cmsXoaMonHoc;
            this.clbMonHoc.FormattingEnabled = true;
            this.clbMonHoc.Location = new System.Drawing.Point(154, 202);
            this.clbMonHoc.Name = "clbMonHoc";
            this.clbMonHoc.Size = new System.Drawing.Size(539, 106);
            this.clbMonHoc.TabIndex = 5;
            this.clbMonHoc.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbMonHoc_ItemCheck);
            // 
            // cmsXoaMonHoc
            // 
            this.cmsXoaMonHoc.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsXoaMonHoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiXoa1Mon,
            this.tmstXoaNhieuMon});
            this.cmsXoaMonHoc.Name = "cmsXoaMonHoc";
            this.cmsXoaMonHoc.Size = new System.Drawing.Size(211, 80);
            // 
            // tsmiXoa1Mon
            // 
            this.tsmiXoa1Mon.Name = "tsmiXoa1Mon";
            this.tsmiXoa1Mon.Size = new System.Drawing.Size(206, 24);
            this.tsmiXoa1Mon.Text = "Xóa 1 môn học";
            this.tsmiXoa1Mon.Click += new System.EventHandler(this.tsmiXoa1Mon_Click);
            // 
            // tmstXoaNhieuMon
            // 
            this.tmstXoaNhieuMon.Name = "tmstXoaNhieuMon";
            this.tmstXoaNhieuMon.Size = new System.Drawing.Size(206, 24);
            this.tmstXoaNhieuMon.Text = "Xóa nhiều môn học";
            this.tmstXoaNhieuMon.Click += new System.EventHandler(this.tmstXoaNhieuMon_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Silver;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Location = new System.Drawing.Point(655, 333);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(103, 30);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click_1);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.Silver;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Location = new System.Drawing.Point(532, 333);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(103, 30);
            this.btnCapNhat.TabIndex = 6;
            this.btnCapNhat.Text = "Cập Nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.BackColor = System.Drawing.Color.Silver;
            this.btnThemMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMoi.Location = new System.Drawing.Point(406, 333);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(103, 30);
            this.btnThemMoi.TabIndex = 6;
            this.btnThemMoi.Text = "Thêm Mới";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.Silver;
            this.btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTimKiem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnTimKiem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Location = new System.Drawing.Point(279, 333);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(103, 30);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDSSV);
            this.groupBox1.Location = new System.Drawing.Point(12, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 369);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách sinh viên";
            // 
            // dgvDSSV
            // 
            this.dgvDSSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSSV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMSSV,
            this.colHoTenLot,
            this.colTen,
            this.colGioiTinh,
            this.colNgaySinh,
            this.colLop,
            this.colCMND,
            this.colSoDT,
            this.colDiaChi});
            this.dgvDSSV.ContextMenuStrip = this.cmsXoaSV;
            this.dgvDSSV.Location = new System.Drawing.Point(6, 27);
            this.dgvDSSV.Name = "dgvDSSV";
            this.dgvDSSV.RowHeadersVisible = false;
            this.dgvDSSV.RowHeadersWidth = 51;
            this.dgvDSSV.RowTemplate.Height = 24;
            this.dgvDSSV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDSSV.Size = new System.Drawing.Size(764, 336);
            this.dgvDSSV.TabIndex = 0;
            this.dgvDSSV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSSV_CellClick);
            // 
            // colChon
            // 
            this.colChon.HeaderText = "Chọn";
            this.colChon.MinimumWidth = 6;
            this.colChon.Name = "colChon";
            this.colChon.Width = 40;
            // 
            // colMSSV
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMSSV.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMSSV.HeaderText = "MSSV";
            this.colMSSV.MinimumWidth = 6;
            this.colMSSV.Name = "colMSSV";
            this.colMSSV.Width = 80;
            // 
            // colHoTenLot
            // 
            this.colHoTenLot.HeaderText = "Họ và tên lót";
            this.colHoTenLot.MinimumWidth = 6;
            this.colHoTenLot.Name = "colHoTenLot";
            this.colHoTenLot.Width = 120;
            // 
            // colTen
            // 
            this.colTen.HeaderText = "Tên";
            this.colTen.MinimumWidth = 6;
            this.colTen.Name = "colTen";
            this.colTen.Width = 60;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.HeaderText = "Giới tính";
            this.colGioiTinh.MinimumWidth = 6;
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.Width = 90;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.MinimumWidth = 6;
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.Width = 125;
            // 
            // colLop
            // 
            this.colLop.HeaderText = "Lớp";
            this.colLop.MinimumWidth = 6;
            this.colLop.Name = "colLop";
            this.colLop.Width = 90;
            // 
            // colCMND
            // 
            this.colCMND.HeaderText = "Số CMND";
            this.colCMND.MinimumWidth = 6;
            this.colCMND.Name = "colCMND";
            this.colCMND.Width = 120;
            // 
            // colSoDT
            // 
            this.colSoDT.HeaderText = "Số ĐT";
            this.colSoDT.MinimumWidth = 6;
            this.colSoDT.Name = "colSoDT";
            this.colSoDT.Width = 90;
            // 
            // colDiaChi
            // 
            this.colDiaChi.HeaderText = "Địa chỉ liên lạc";
            this.colDiaChi.MinimumWidth = 6;
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Width = 125;
            // 
            // cmsXoaSV
            // 
            this.cmsXoaSV.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsXoaSV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiXoa1SV,
            this.tmsiXoaNhieuSV});
            this.cmsXoaSV.Name = "contextMenuStrip1";
            this.cmsXoaSV.Size = new System.Drawing.Size(206, 52);
            // 
            // tmsiXoa1SV
            // 
            this.tmsiXoa1SV.Name = "tmsiXoa1SV";
            this.tmsiXoa1SV.Size = new System.Drawing.Size(205, 24);
            this.tmsiXoa1SV.Text = "Xóa 1 sinh viên";
            this.tmsiXoa1SV.Click += new System.EventHandler(this.tmsiXoa1SV_Click);
            // 
            // tmsiXoaNhieuSV
            // 
            this.tmsiXoaNhieuSV.Name = "tmsiXoaNhieuSV";
            this.tmsiXoaNhieuSV.Size = new System.Drawing.Size(205, 24);
            this.tmsiXoaNhieuSV.Text = "Xóa nhiều sinh viên";
            this.tmsiXoaNhieuSV.Click += new System.EventHandler(this.tmsiXoaNhieuSV_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BackColor = System.Drawing.Color.Silver;
            this.btnXuatFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnXuatFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnXuatFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Location = new System.Drawing.Point(154, 333);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(103, 30);
            this.btnXuatFile.TabIndex = 6;
            this.btnXuatFile.Text = "Xuất File";
            this.btnXuatFile.UseVisualStyleBackColor = false;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.BackColor = System.Drawing.Color.Silver;
            this.btnNhapFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNhapFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnNhapFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNhapFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapFile.Location = new System.Drawing.Point(30, 333);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(103, 30);
            this.btnNhapFile.TabIndex = 6;
            this.btnNhapFile.Text = "Nhập File";
            this.btnNhapFile.UseVisualStyleBackColor = false;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
            // 
            // mtxtSoDT
            // 
            this.mtxtSoDT.Location = new System.Drawing.Point(482, 123);
            this.mtxtSoDT.Mask = "0000.000.000";
            this.mtxtSoDT.Name = "mtxtSoDT";
            this.mtxtSoDT.Size = new System.Drawing.Size(209, 22);
            this.mtxtSoDT.TabIndex = 6;
            // 
            // mtxtMSSV
            // 
            this.mtxtMSSV.Location = new System.Drawing.Point(153, 21);
            this.mtxtMSSV.Mask = "0000000";
            this.mtxtMSSV.Name = "mtxtMSSV";
            this.mtxtMSSV.Size = new System.Drawing.Size(209, 22);
            this.mtxtMSSV.TabIndex = 0;
            // 
            // mtxtCMND
            // 
            this.mtxtCMND.Location = new System.Drawing.Point(153, 119);
            this.mtxtCMND.Mask = "000000000";
            this.mtxtCMND.Name = "mtxtCMND";
            this.mtxtCMND.Size = new System.Drawing.Size(209, 22);
            this.mtxtCMND.TabIndex = 5;
            // 
            // frmThongTinSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 765);
            this.Controls.Add(this.mtxtCMND);
            this.Controls.Add(this.mtxtMSSV);
            this.Controls.Add(this.mtxtSoDT);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNhapFile);
            this.Controls.Add(this.btnXuatFile);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.btnThemMoi);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.clbMonHoc);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.rdNu);
            this.Controls.Add(this.rdNam);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtHoTenLot);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "frmThongTinSV";
            this.Text = "Nhập thông tin sinh viên";
            this.Load += new System.EventHandler(this.frmThongTinSV_Load);
            this.cmsXoaMonHoc.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSSV)).EndInit();
            this.cmsXoaSV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHoTenLot;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdNam;
        private System.Windows.Forms.RadioButton rdNu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.ComboBox cbLop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckedListBox clbMonHoc;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDSSV;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.MaskedTextBox mtxtSoDT;
        private System.Windows.Forms.MaskedTextBox mtxtMSSV;
        private System.Windows.Forms.MaskedTextBox mtxtCMND;
        private System.Windows.Forms.ContextMenuStrip cmsXoaSV;
        private System.Windows.Forms.ToolStripMenuItem tmsiXoa1SV;
        private System.Windows.Forms.ToolStripMenuItem tmsiXoaNhieuSV;
        private System.Windows.Forms.ContextMenuStrip cmsXoaMonHoc;
        private System.Windows.Forms.ToolStripMenuItem tsmiXoa1Mon;
        private System.Windows.Forms.ToolStripMenuItem tmstXoaNhieuMon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMSSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTenLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCMND;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaChi;
    }
}

