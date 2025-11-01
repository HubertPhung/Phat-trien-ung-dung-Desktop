namespace ChuDe4
{
    partial class FoodForm
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
            this.dgvFood = new System.Windows.Forms.DataGridView();
            this.colMaMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFood)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFood
            // 
            this.dgvFood.AutoGenerateColumns = false;
            this.dgvFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFood.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaMon,
            this.colTenMon,
            this.colDonViTinh,
            this.colMaNhom,
            this.colDonGia,
            this.colGhiChu});
            this.dgvFood.Location = new System.Drawing.Point(0, 0);
            this.dgvFood.Name = "dgvFood";
            this.dgvFood.RowHeadersWidth = 51;
            this.dgvFood.RowTemplate.Height = 24;
            this.dgvFood.Size = new System.Drawing.Size(805, 320);
            this.dgvFood.TabIndex = 0;
            this.dgvFood.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFood_CellContentClick);
            // 
            // colMaMon
            // 
            this.colMaMon.DataPropertyName = "ID";
            this.colMaMon.HeaderText = "Mã món";
            this.colMaMon.MinimumWidth = 6;
            this.colMaMon.Name = "colMaMon";
            this.colMaMon.Width = 125;
            // 
            // colTenMon
            // 
            this.colTenMon.DataPropertyName = "Name";
            this.colTenMon.HeaderText = "Tên món";
            this.colTenMon.MinimumWidth = 6;
            this.colTenMon.Name = "colTenMon";
            this.colTenMon.Width = 125;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "Unit";
            this.colDonViTinh.HeaderText = "Đơn vị tính";
            this.colDonViTinh.MinimumWidth = 6;
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.Width = 125;
            // 
            // colMaNhom
            // 
            this.colMaNhom.DataPropertyName = "FoodCategoryID";
            this.colMaNhom.HeaderText = "Mã nhóm";
            this.colMaNhom.MinimumWidth = 6;
            this.colMaNhom.Name = "colMaNhom";
            this.colMaNhom.Width = 125;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "Price";
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.MinimumWidth = 6;
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Width = 125;
            // 
            // colGhiChu
            // 
            this.colGhiChu.DataPropertyName = "Notes";
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 6;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Width = 125;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(586, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(692, 326);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 360);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvFood);
            this.Name = "FoodForm";
            this.Text = "Danh sách món ăn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFood)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNhom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}