namespace WindowsFormsApp2
{
    partial class frmHome
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Phòng 101", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.HighlightText, new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "hello", System.Drawing.SystemColors.HotTrack, System.Drawing.Color.White, new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Phòng 102"}, 0, System.Drawing.Color.Empty, System.Drawing.SystemColors.HighlightText, null);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Phòng 103", 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Phòng 104", 1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Phòng 201", 0);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Phòng 202", 0);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Phòng 203", 1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Phòng 204", 0);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Phòng V301", 0);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Phòng V302", 1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Phòng 303", 1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Phòng 304", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.gbThongTinPhong = new System.Windows.Forms.GroupBox();
            this.btnThemKH = new System.Windows.Forms.Button();
            this.txtSoDT = new System.Windows.Forms.TextBox();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvChiTiet = new Guna.UI2.WinForms.Guna2DataGridView();
            this.gbChiTietDV = new System.Windows.Forms.GroupBox();
            this.gbDSDichVu = new System.Windows.Forms.GroupBox();
            this.dgvDSDV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Hinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnThemDV = new Guna.UI2.WinForms.Guna2Button();
            this.nudSoLuong = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbLoaiDV = new System.Windows.Forms.GroupBox();
            this.rdGiatLa = new System.Windows.Forms.RadioButton();
            this.rdGiaiTri = new System.Windows.Forms.RadioButton();
            this.rdDiLai = new System.Windows.Forms.RadioButton();
            this.rdSpa = new System.Windows.Forms.RadioButton();
            this.rdAnUong = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvPhong = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gbTongKetHD = new System.Windows.Forms.GroupBox();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuyDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnThanhToan = new Guna.UI2.WinForms.Guna2Button();
            this.label15 = new System.Windows.Forms.Label();
            this.nudGiamGia = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtTongDV = new System.Windows.Forms.TextBox();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtNgayLap = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoPhong = new Guna.UI2.WinForms.Guna2TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbThongTinPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.gbChiTietDV.SuspendLayout();
            this.gbDSDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.gbLoaiDV.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbTongKetHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).BeginInit();
            this.SuspendLayout();
            // 
            // gbThongTinPhong
            // 
            this.gbThongTinPhong.Controls.Add(this.btnThemKH);
            this.gbThongTinPhong.Controls.Add(this.txtSoPhong);
            this.gbThongTinPhong.Controls.Add(this.txtSoDT);
            this.gbThongTinPhong.Controls.Add(this.txtCMND);
            this.gbThongTinPhong.Controls.Add(this.txtKhachHang);
            this.gbThongTinPhong.Controls.Add(this.label8);
            this.gbThongTinPhong.Controls.Add(this.label7);
            this.gbThongTinPhong.Controls.Add(this.label6);
            this.gbThongTinPhong.Controls.Add(this.label4);
            this.gbThongTinPhong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbThongTinPhong.Location = new System.Drawing.Point(602, 25);
            this.gbThongTinPhong.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbThongTinPhong.Name = "gbThongTinPhong";
            this.gbThongTinPhong.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbThongTinPhong.Size = new System.Drawing.Size(757, 144);
            this.gbThongTinPhong.TabIndex = 1;
            this.gbThongTinPhong.TabStop = false;
            this.gbThongTinPhong.Text = "Thông tin phòng";
            // 
            // btnThemKH
            // 
            this.btnThemKH.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemKH.Location = new System.Drawing.Point(654, 34);
            this.btnThemKH.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnThemKH.Name = "btnThemKH";
            this.btnThemKH.Size = new System.Drawing.Size(33, 26);
            this.btnThemKH.TabIndex = 2;
            this.btnThemKH.Text = "...";
            this.btnThemKH.UseVisualStyleBackColor = true;
            // 
            // txtSoDT
            // 
            this.txtSoDT.Location = new System.Drawing.Point(400, 103);
            this.txtSoDT.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtSoDT.Name = "txtSoDT";
            this.txtSoDT.ReadOnly = true;
            this.txtSoDT.Size = new System.Drawing.Size(209, 30);
            this.txtSoDT.TabIndex = 1;
            // 
            // txtCMND
            // 
            this.txtCMND.Location = new System.Drawing.Point(400, 68);
            this.txtCMND.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.ReadOnly = true;
            this.txtCMND.Size = new System.Drawing.Size(209, 30);
            this.txtCMND.TabIndex = 1;
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Location = new System.Drawing.Point(400, 30);
            this.txtKhachHang.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(209, 30);
            this.txtKhachHang.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(233, 103);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "Số điện thoại";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(233, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "CMND/CCCD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(233, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tên khách hàng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số phòng";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvChiTiet.ColumnHeadersHeight = 25;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvChiTiet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvChiTiet.Location = new System.Drawing.Point(7, 30);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(748, 346);
            this.dgvChiTiet.TabIndex = 3;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvChiTiet.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvChiTiet.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvChiTiet.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.Height = 25;
            this.dgvChiTiet.ThemeStyle.ReadOnly = true;
            this.dgvChiTiet.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChiTiet.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvChiTiet.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvChiTiet.ThemeStyle.RowsStyle.Height = 24;
            this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // gbChiTietDV
            // 
            this.gbChiTietDV.Controls.Add(this.dgvChiTiet);
            this.gbChiTietDV.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbChiTietDV.Location = new System.Drawing.Point(595, 193);
            this.gbChiTietDV.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbChiTietDV.Name = "gbChiTietDV";
            this.gbChiTietDV.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbChiTietDV.Size = new System.Drawing.Size(764, 380);
            this.gbChiTietDV.TabIndex = 4;
            this.gbChiTietDV.TabStop = false;
            this.gbChiTietDV.Text = "Chi tiết dịch vụ";
            // 
            // gbDSDichVu
            // 
            this.gbDSDichVu.Controls.Add(this.dgvDSDV);
            this.gbDSDichVu.Controls.Add(this.btnThemDV);
            this.gbDSDichVu.Controls.Add(this.nudSoLuong);
            this.gbDSDichVu.Controls.Add(this.label1);
            this.gbDSDichVu.Controls.Add(this.label2);
            this.gbDSDichVu.Controls.Add(this.gbLoaiDV);
            this.gbDSDichVu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDSDichVu.Location = new System.Drawing.Point(268, 25);
            this.gbDSDichVu.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbDSDichVu.Name = "gbDSDichVu";
            this.gbDSDichVu.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbDSDichVu.Size = new System.Drawing.Size(326, 548);
            this.gbDSDichVu.TabIndex = 5;
            this.gbDSDichVu.TabStop = false;
            this.gbDSDichVu.Text = "Danh sách dịch vụ";
            // 
            // dgvDSDV
            // 
            this.dgvDSDV.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSDV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSDV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvDSDV.ColumnHeadersHeight = 25;
            this.dgvDSDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDSDV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hinh,
            this.Ten,
            this.DVT,
            this.Gia});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSDV.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvDSDV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDV.Location = new System.Drawing.Point(11, 172);
            this.dgvDSDV.Name = "dgvDSDV";
            this.dgvDSDV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSDV.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvDSDV.RowHeadersVisible = false;
            this.dgvDSDV.RowHeadersWidth = 51;
            this.dgvDSDV.RowTemplate.Height = 24;
            this.dgvDSDV.Size = new System.Drawing.Size(307, 288);
            this.dgvDSDV.TabIndex = 14;
            this.dgvDSDV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDV.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSDV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvDSDV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSDV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dgvDSDV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDSDV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSDV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDSDV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDSDV.ThemeStyle.HeaderStyle.Height = 25;
            this.dgvDSDV.ThemeStyle.ReadOnly = false;
            this.dgvDSDV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSDV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDSDV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSDV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSDV.ThemeStyle.RowsStyle.Height = 24;
            this.dgvDSDV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSDV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Hinh
            // 
            this.Hinh.HeaderText = "Hình";
            this.Hinh.MinimumWidth = 6;
            this.Hinh.Name = "Hinh";
            // 
            // Ten
            // 
            this.Ten.HeaderText = "Tên";
            this.Ten.MinimumWidth = 6;
            this.Ten.Name = "Ten";
            // 
            // DVT
            // 
            this.DVT.HeaderText = "DVT";
            this.DVT.MinimumWidth = 6;
            this.DVT.Name = "DVT";
            // 
            // Gia
            // 
            this.Gia.HeaderText = "Giá";
            this.Gia.MinimumWidth = 6;
            this.Gia.Name = "Gia";
            // 
            // btnThemDV
            // 
            this.btnThemDV.AutoRoundedCorners = true;
            this.btnThemDV.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnThemDV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDV.ForeColor = System.Drawing.Color.White;
            this.btnThemDV.Location = new System.Drawing.Point(143, 483);
            this.btnThemDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(157, 50);
            this.btnThemDV.TabIndex = 13;
            this.btnThemDV.Text = "Thêm dịch vụ";
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.nudSoLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoLuong.Location = new System.Drawing.Point(25, 504);
            this.nudSoLuong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(81, 29);
            this.nudSoLuong.TabIndex = 12;
            this.nudSoLuong.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 477);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số lượng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dịch vụ";
            // 
            // gbLoaiDV
            // 
            this.gbLoaiDV.Controls.Add(this.rdGiatLa);
            this.gbLoaiDV.Controls.Add(this.rdGiaiTri);
            this.gbLoaiDV.Controls.Add(this.rdDiLai);
            this.gbLoaiDV.Controls.Add(this.rdSpa);
            this.gbLoaiDV.Controls.Add(this.rdAnUong);
            this.gbLoaiDV.Location = new System.Drawing.Point(12, 30);
            this.gbLoaiDV.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbLoaiDV.Name = "gbLoaiDV";
            this.gbLoaiDV.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbLoaiDV.Size = new System.Drawing.Size(306, 114);
            this.gbLoaiDV.TabIndex = 4;
            this.gbLoaiDV.TabStop = false;
            this.gbLoaiDV.Text = "Loại dịch vụ";
            // 
            // rdGiatLa
            // 
            this.rdGiatLa.AutoSize = true;
            this.rdGiatLa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdGiatLa.Location = new System.Drawing.Point(131, 71);
            this.rdGiatLa.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdGiatLa.Name = "rdGiatLa";
            this.rdGiatLa.Size = new System.Drawing.Size(80, 27);
            this.rdGiatLa.TabIndex = 0;
            this.rdGiatLa.Text = "Giặt là";
            this.rdGiatLa.UseVisualStyleBackColor = true;
            // 
            // rdGiaiTri
            // 
            this.rdGiaiTri.AutoSize = true;
            this.rdGiaiTri.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdGiaiTri.Location = new System.Drawing.Point(207, 32);
            this.rdGiaiTri.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdGiaiTri.Name = "rdGiaiTri";
            this.rdGiaiTri.Size = new System.Drawing.Size(81, 27);
            this.rdGiaiTri.TabIndex = 0;
            this.rdGiaiTri.Text = "Giải trí";
            this.rdGiaiTri.UseVisualStyleBackColor = true;
            // 
            // rdDiLai
            // 
            this.rdDiLai.AutoSize = true;
            this.rdDiLai.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDiLai.Location = new System.Drawing.Point(12, 78);
            this.rdDiLai.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdDiLai.Name = "rdDiLai";
            this.rdDiLai.Size = new System.Drawing.Size(69, 27);
            this.rdDiLai.TabIndex = 0;
            this.rdDiLai.Text = "Đi lại";
            this.rdDiLai.UseVisualStyleBackColor = true;
            // 
            // rdSpa
            // 
            this.rdSpa.AutoSize = true;
            this.rdSpa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSpa.Location = new System.Drawing.Point(131, 31);
            this.rdSpa.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdSpa.Name = "rdSpa";
            this.rdSpa.Size = new System.Drawing.Size(59, 27);
            this.rdSpa.TabIndex = 0;
            this.rdSpa.Text = "Spa";
            this.rdSpa.UseVisualStyleBackColor = true;
            // 
            // rdAnUong
            // 
            this.rdAnUong.AutoSize = true;
            this.rdAnUong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAnUong.Location = new System.Drawing.Point(13, 32);
            this.rdAnUong.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rdAnUong.Name = "rdAnUong";
            this.rdAnUong.Size = new System.Drawing.Size(97, 27);
            this.rdAnUong.TabIndex = 0;
            this.rdAnUong.Text = "Ăn uống";
            this.rdAnUong.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvPhong);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(237, 777);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách phòng";
            // 
            // lvPhong
            // 
            this.lvPhong.BackColor = System.Drawing.Color.White;
            this.lvPhong.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPhong.HideSelection = false;
            this.lvPhong.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.lvPhong.LargeImageList = this.imageList1;
            this.lvPhong.Location = new System.Drawing.Point(3, 25);
            this.lvPhong.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvPhong.Name = "lvPhong";
            this.lvPhong.Size = new System.Drawing.Size(231, 750);
            this.lvPhong.TabIndex = 2;
            this.lvPhong.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 300;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "hotel (1).png");
            this.imageList1.Images.SetKeyName(1, "hotel.png");
            this.imageList1.Images.SetKeyName(2, "hotel (4).png");
            // 
            // gbTongKetHD
            // 
            this.gbTongKetHD.Controls.Add(this.btnThoat);
            this.gbTongKetHD.Controls.Add(this.btnHuyDon);
            this.gbTongKetHD.Controls.Add(this.btnLuu);
            this.gbTongKetHD.Controls.Add(this.btnThanhToan);
            this.gbTongKetHD.Controls.Add(this.label15);
            this.gbTongKetHD.Controls.Add(this.nudGiamGia);
            this.gbTongKetHD.Controls.Add(this.txtGhiChu);
            this.gbTongKetHD.Controls.Add(this.txtTongDV);
            this.gbTongKetHD.Controls.Add(this.txtThanhTien);
            this.gbTongKetHD.Controls.Add(this.txtTongTien);
            this.gbTongKetHD.Controls.Add(this.txtNgayLap);
            this.gbTongKetHD.Controls.Add(this.label13);
            this.gbTongKetHD.Controls.Add(this.label11);
            this.gbTongKetHD.Controls.Add(this.label3);
            this.gbTongKetHD.Controls.Add(this.label12);
            this.gbTongKetHD.Controls.Add(this.label5);
            this.gbTongKetHD.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTongKetHD.Location = new System.Drawing.Point(268, 582);
            this.gbTongKetHD.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbTongKetHD.Name = "gbTongKetHD";
            this.gbTongKetHD.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gbTongKetHD.Size = new System.Drawing.Size(1091, 220);
            this.gbTongKetHD.TabIndex = 7;
            this.gbTongKetHD.TabStop = false;
            this.gbTongKetHD.Text = "Tổng kết và hóa đơn";
            // 
            // btnThoat
            // 
            this.btnThoat.AutoRoundedCorners = true;
            this.btnThoat.CustomizableEdges.TopRight = false;
            this.btnThoat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(824, 162);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(128, 50);
            this.btnThoat.TabIndex = 13;
            this.btnThoat.Text = "Thoát";
            // 
            // btnHuyDon
            // 
            this.btnHuyDon.AutoRoundedCorners = true;
            this.btnHuyDon.CustomizableEdges.TopRight = false;
            this.btnHuyDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnHuyDon.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyDon.ForeColor = System.Drawing.Color.White;
            this.btnHuyDon.Location = new System.Drawing.Point(659, 162);
            this.btnHuyDon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuyDon.Name = "btnHuyDon";
            this.btnHuyDon.Size = new System.Drawing.Size(128, 50);
            this.btnHuyDon.TabIndex = 13;
            this.btnHuyDon.Text = "Hủy đơn";
            // 
            // btnLuu
            // 
            this.btnLuu.AutoRoundedCorners = true;
            this.btnLuu.CustomizableEdges.TopRight = false;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(491, 165);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(128, 50);
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Lưu";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.AutoRoundedCorners = true;
            this.btnThanhToan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(167, 162);
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(219, 50);
            this.btnThanhToan.TabIndex = 13;
            this.btnThanhToan.Text = "Thanh toán";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(461, 79);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 23);
            this.label15.TabIndex = 0;
            this.label15.Text = "Giảm giá %";
            // 
            // nudGiamGia
            // 
            this.nudGiamGia.BackColor = System.Drawing.Color.Transparent;
            this.nudGiamGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudGiamGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudGiamGia.Location = new System.Drawing.Point(596, 74);
            this.nudGiamGia.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudGiamGia.Name = "nudGiamGia";
            this.nudGiamGia.Size = new System.Drawing.Size(237, 30);
            this.nudGiamGia.TabIndex = 12;
            this.nudGiamGia.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(178, 81);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(208, 30);
            this.txtGhiChu.TabIndex = 1;
            // 
            // txtTongDV
            // 
            this.txtTongDV.Location = new System.Drawing.Point(178, 118);
            this.txtTongDV.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTongDV.Name = "txtTongDV";
            this.txtTongDV.Size = new System.Drawing.Size(208, 30);
            this.txtTongDV.TabIndex = 1;
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Location = new System.Drawing.Point(596, 113);
            this.txtThanhTien.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Size = new System.Drawing.Size(239, 30);
            this.txtThanhTien.TabIndex = 1;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(596, 34);
            this.txtTongTien.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(239, 30);
            this.txtTongTien.TabIndex = 1;
            // 
            // txtNgayLap
            // 
            this.txtNgayLap.Location = new System.Drawing.Point(178, 42);
            this.txtNgayLap.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtNgayLap.Name = "txtNgayLap";
            this.txtNgayLap.Size = new System.Drawing.Size(208, 30);
            this.txtNgayLap.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(45, 81);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 23);
            this.label13.TabIndex = 0;
            this.label13.Text = "Ghi chú";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(31, 121);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "Tổng dịch vụ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(461, 116);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Thành tiền";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(461, 42);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "Tổng tiền";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(45, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ngày lập";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoPhong.DefaultText = "";
            this.txtSoPhong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoPhong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoPhong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoPhong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoPhong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoPhong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoPhong.Location = new System.Drawing.Point(21, 71);
            this.txtSoPhong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtSoPhong.PlaceholderText = "";
            this.txtSoPhong.SelectedText = "";
            this.txtSoPhong.Size = new System.Drawing.Size(157, 31);
            this.txtSoPhong.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "STT";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Mã DV";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "TênDV";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Ngày SD";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Đơn giá";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SL";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Thành tiền";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Ghi chú";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1372, 814);
            this.Controls.Add(this.gbTongKetHD);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbDSDichVu);
            this.Controls.Add(this.gbChiTietDV);
            this.Controls.Add(this.gbThongTinPhong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmHome";
            this.Text = "frmHome";
            this.gbThongTinPhong.ResumeLayout(false);
            this.gbThongTinPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.gbChiTietDV.ResumeLayout(false);
            this.gbDSDichVu.ResumeLayout(false);
            this.gbDSDichVu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.gbLoaiDV.ResumeLayout(false);
            this.gbLoaiDV.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbTongKetHD.ResumeLayout(false);
            this.gbTongKetHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbThongTinPhong;
        private System.Windows.Forms.Button btnThemKH;
        private System.Windows.Forms.TextBox txtSoDT;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvChiTiet;
        private System.Windows.Forms.GroupBox gbChiTietDV;
        private System.Windows.Forms.GroupBox gbDSDichVu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbLoaiDV;
        private System.Windows.Forms.RadioButton rdGiatLa;
        private System.Windows.Forms.RadioButton rdGiaiTri;
        private System.Windows.Forms.RadioButton rdDiLai;
        private System.Windows.Forms.RadioButton rdSpa;
        private System.Windows.Forms.RadioButton rdAnUong;
        private Guna.UI2.WinForms.Guna2NumericUpDown nudSoLuong;
        private Guna.UI2.WinForms.Guna2Button btnThemDV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbTongKetHD;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtTongDV;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtNgayLap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private Guna.UI2.WinForms.Guna2Button btnThanhToan;
        private Guna.UI2.WinForms.Guna2NumericUpDown nudGiamGia;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
        private Guna.UI2.WinForms.Guna2Button btnHuyDon;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvPhong;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDSDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn DVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gia;
        private Guna.UI2.WinForms.Guna2TextBox txtSoPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}