namespace Lab09_Entity_Framework
{
 partial class BillsForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvBills;
 private System.Windows.Forms.DateTimePicker dtpFrom;
 private System.Windows.Forms.DateTimePicker dtpTo;
 private System.Windows.Forms.Button btnFilter;
 private System.Windows.Forms.Label lblGross;
 private System.Windows.Forms.Label lblDiscount;
 private System.Windows.Forms.Label lblNet;

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
            this.dgvBills = new System.Windows.Forms.DataGridView();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.lblGross = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBills
            // 
            this.dgvBills.AllowUserToAddRows = false;
            this.dgvBills.AllowUserToDeleteRows = false;
            this.dgvBills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBills.Location = new System.Drawing.Point(12, 41);
            this.dgvBills.MultiSelect = false;
            this.dgvBills.Name = "dgvBills";
            this.dgvBills.ReadOnly = true;
            this.dgvBills.RowHeadersWidth = 51;
            this.dgvBills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBills.Size = new System.Drawing.Size(560, 330);
            this.dgvBills.TabIndex = 0;
            this.dgvBills.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBills_CellDoubleClick);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(12, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 27);
            this.dtpFrom.TabIndex = 1;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(138, 12);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 27);
            this.dtpTo.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(264, 10);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(81, 27);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // lblGross
            // 
            this.lblGross.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGross.AutoSize = true;
            this.lblGross.Location = new System.Drawing.Point(12, 381);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(131, 25);
            this.lblGross.TabIndex = 4;
            this.lblGross.Text = "Tổng trước giảm:0";
            this.lblGross.UseCompatibleTextRendering = true;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(220, 381);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(90, 25);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Tổng giảm:0";
            this.lblDiscount.UseCompatibleTextRendering = true;
            // 
            // lblNet
            // 
            this.lblNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNet.AutoSize = true;
            this.lblNet.Location = new System.Drawing.Point(380, 381);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(77, 25);
            this.lblNet.TabIndex = 6;
            this.lblNet.Text = "Thực thu:0";
            this.lblNet.UseCompatibleTextRendering = true;
            // 
            // BillsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.dgvBills);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BillsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hóa đơn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

 }
 }
}
