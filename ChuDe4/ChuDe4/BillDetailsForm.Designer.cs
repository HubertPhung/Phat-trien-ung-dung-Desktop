namespace ChuDe4
{
    partial class BillDetailsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBillDetails = new System.Windows.Forms.DataGridView();
            this.colFoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(332, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chi tiết hóa đơn";
            // 
            // dgvBillDetails
            // 
            this.dgvBillDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBillDetails.AutoGenerateColumns = false;
            this.dgvBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFoodName,
            this.colQuantity,
            this.colUnitPrice,
            this.colTotal});
            this.dgvBillDetails.Location = new System.Drawing.Point(12, 56);
            this.dgvBillDetails.Name = "dgvBillDetails";
            this.dgvBillDetails.RowHeadersWidth = 51;
            this.dgvBillDetails.RowTemplate.Height = 24;
            this.dgvBillDetails.Size = new System.Drawing.Size(776, 382);
            this.dgvBillDetails.TabIndex = 1;
            // 
            // colFoodName
            // 
            this.colFoodName.DataPropertyName = "FoodName";
            this.colFoodName.HeaderText = "Món";
            this.colFoodName.MinimumWidth = 6;
            this.colFoodName.Name = "FoodName";
            this.colFoodName.Width = 200;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.HeaderText = "SL";
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "Quantity";
            this.colQuantity.Width = 60;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.DataPropertyName = "UnitPrice";
            this.colUnitPrice.HeaderText = "Đơn giá";
            this.colUnitPrice.MinimumWidth = 6;
            this.colUnitPrice.Name = "UnitPrice";
            this.colUnitPrice.DefaultCellStyle.Format = "N0";
            this.colUnitPrice.Width = 120;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "Total";
            this.colTotal.HeaderText = "Thành tiền";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "Total";
            this.colTotal.DefaultCellStyle.Format = "N0";
            this.colTotal.Width = 150;
            // 
            // BillDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 450);
            this.Controls.Add(this.dgvBillDetails);
            this.Controls.Add(this.label1);
            this.Name = "BillDetailsForm";
            this.Text = "BillDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBillDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFoodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
    }
}