namespace Lab09_Entity_Framework
{
 partial class TableForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvTables;
 private System.Windows.Forms.Button btnReload;
 private System.Windows.Forms.Button btnAdd;
 private System.Windows.Forms.Button btnEdit;
 private System.Windows.Forms.Button btnDelete;
 private System.Windows.Forms.Button btnViewCurrentBill;
 private System.Windows.Forms.ContextMenuStrip ctxMenu;
 private System.Windows.Forms.ToolStripMenuItem mnuDelete;
 private System.Windows.Forms.ToolStripMenuItem mnuViewBills;
 private System.Windows.Forms.ToolStripMenuItem mnuViewSalesLog;

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
            this.components = new System.ComponentModel.Container();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewBills = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSalesLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnViewCurrentBill = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTables
            // 
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.ContextMenuStrip = this.ctxMenu;
            this.dgvTables.Location = new System.Drawing.Point(12, 12);
            this.dgvTables.MultiSelect = false;
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.RowHeadersWidth = 51;
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(560, 330);
            this.dgvTables.TabIndex = 0;
            // 
            // ctxMenu
            // 
            this.ctxMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete,
            this.mnuViewBills,
            this.mnuViewSalesLog});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(237, 76);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(236, 24);
            this.mnuDelete.Text = "Xóa bàn";
            // 
            // mnuViewBills
            // 
            this.mnuViewBills.Name = "mnuViewBills";
            this.mnuViewBills.Size = new System.Drawing.Size(236, 24);
            this.mnuViewBills.Text = "Xem danh mục hóa đơn";
            // 
            // mnuViewSalesLog
            // 
            this.mnuViewSalesLog.Name = "mnuViewSalesLog";
            this.mnuViewSalesLog.Size = new System.Drawing.Size(236, 24);
            this.mnuViewSalesLog.Text = "Xem nhật ký bán hàng";
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.Location = new System.Drawing.Point(12, 348);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 27);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseCompatibleTextRendering = true;
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(93, 348);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseCompatibleTextRendering = true;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(174, 348);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 27);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseCompatibleTextRendering = true;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(255, 348);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 27);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseCompatibleTextRendering = true;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnViewCurrentBill
            // 
            this.btnViewCurrentBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewCurrentBill.Location = new System.Drawing.Point(482, 348);
            this.btnViewCurrentBill.Name = "btnViewCurrentBill";
            this.btnViewCurrentBill.Size = new System.Drawing.Size(90, 27);
            this.btnViewCurrentBill.TabIndex = 5;
            this.btnViewCurrentBill.Text = "HĐ hiện tại";
            this.btnViewCurrentBill.UseCompatibleTextRendering = true;
            this.btnViewCurrentBill.UseVisualStyleBackColor = true;
            this.btnViewCurrentBill.Click += new System.EventHandler(this.btnViewCurrentBill_Click);
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 381);
            this.Controls.Add(this.btnViewCurrentBill);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dgvTables);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "TableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý bàn";
            this.Load += new System.EventHandler(this.TableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

 }
 }
}
