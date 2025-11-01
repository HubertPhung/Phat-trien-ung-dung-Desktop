namespace Lab09_Entity_Framework
{
 partial class FoodForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.ComboBox cbbCategory;
 private System.Windows.Forms.TextBox txtKeyword;
 private System.Windows.Forms.Button btnSearch;
 private System.Windows.Forms.Button btnAddFood;
 private System.Windows.Forms.Button btnEditFood;
 private System.Windows.Forms.Button btnDeleteSelected;
 private System.Windows.Forms.Button btnAddCategory;
 private System.Windows.Forms.DataGridView dgvFoods;
 private System.Windows.Forms.Label lblCategory;
 private System.Windows.Forms.Label lblKeyword;

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
            this.cbbCategory = new System.Windows.Forms.ComboBox();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.btnEditFood = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.dgvFoods = new System.Windows.Forms.DataGridView();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoods)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbCategory
            // 
            this.cbbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategory.Location = new System.Drawing.Point(101, 12);
            this.cbbCategory.Name = "cbbCategory";
            this.cbbCategory.Size = new System.Drawing.Size(160, 28);
            this.cbbCategory.TabIndex = 1;
            this.cbbCategory.SelectedIndexChanged += new System.EventHandler(this.cbbCategory_SelectedIndexChanged);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(331, 12);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(160, 27);
            this.txtKeyword.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(497, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 27);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseCompatibleTextRendering = true;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddFood
            // 
            this.btnAddFood.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFood.Location = new System.Drawing.Point(474, 384);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(75, 27);
            this.btnAddFood.TabIndex = 7;
            this.btnAddFood.Text = "Thêm";
            this.btnAddFood.UseCompatibleTextRendering = true;
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // btnEditFood
            // 
            this.btnEditFood.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditFood.Location = new System.Drawing.Point(555, 384);
            this.btnEditFood.Name = "btnEditFood";
            this.btnEditFood.Size = new System.Drawing.Size(75, 27);
            this.btnEditFood.TabIndex = 8;
            this.btnEditFood.Text = "Sửa";
            this.btnEditFood.UseCompatibleTextRendering = true;
            this.btnEditFood.UseVisualStyleBackColor = true;
            this.btnEditFood.Click += new System.EventHandler(this.btnEditFood_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelected.Location = new System.Drawing.Point(636, 384);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(75, 27);
            this.btnDeleteSelected.TabIndex = 9;
            this.btnDeleteSelected.Text = "Xóa";
            this.btnDeleteSelected.UseCompatibleTextRendering = true;
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCategory.Location = new System.Drawing.Point(613, 10);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(97, 27);
            this.btnAddCategory.TabIndex = 5;
            this.btnAddCategory.Text = "Thêm nhóm";
            this.btnAddCategory.UseCompatibleTextRendering = true;
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // dgvFoods
            // 
            this.dgvFoods.AllowUserToAddRows = false;
            this.dgvFoods.AllowUserToDeleteRows = false;
            this.dgvFoods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoods.Location = new System.Drawing.Point(12, 48);
            this.dgvFoods.Name = "dgvFoods";
            this.dgvFoods.ReadOnly = true;
            this.dgvFoods.RowHeadersWidth = 51;
            this.dgvFoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFoods.Size = new System.Drawing.Size(698, 330);
            this.dgvFoods.TabIndex = 6;
            this.dgvFoods.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFoods_CellDoubleClick);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 15);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(84, 25);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Nhóm món";
            this.lblCategory.UseCompatibleTextRendering = true;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(267, 15);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(60, 25);
            this.lblKeyword.TabIndex = 2;
            this.lblKeyword.Text = "Từ khóa";
            this.lblKeyword.UseCompatibleTextRendering = true;
            // 
            // FoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 417);
            this.Controls.Add(this.btnDeleteSelected);
            this.Controls.Add(this.btnEditFood);
            this.Controls.Add(this.btnAddFood);
            this.Controls.Add(this.dgvFoods);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.cbbCategory);
            this.Controls.Add(this.lblCategory);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FoodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý món ăn";
            this.Load += new System.EventHandler(this.FoodForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

 }
 }
}
