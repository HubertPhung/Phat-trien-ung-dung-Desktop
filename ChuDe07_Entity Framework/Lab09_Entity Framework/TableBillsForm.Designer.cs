namespace Lab09_Entity_Framework
{
        partial class TableBillsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstDates;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblInfo;

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
            this.lstDates = new System.Windows.Forms.ListBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lstDates
            // 
            this.lstDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDates.Location = new System.Drawing.Point(12, 12);
            this.lstDates.Name = "lstDates";
            this.lstDates.Size = new System.Drawing.Size(200, 326);
            this.lstDates.TabIndex = 0;
            this.lstDates.SelectedIndexChanged += new System.EventHandler(this.lstDates_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(218, 12);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(65, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Thông tin";
            // 
            // dgvItems
            // 
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(221, 34);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(351, 304);
            this.dgvItems.TabIndex = 2;
            // 
            // TableBillsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 350);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lstDates);
            this.Name = "TableBillsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách hóa đơn";
            this.Load += new System.EventHandler(this.TableBillsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
