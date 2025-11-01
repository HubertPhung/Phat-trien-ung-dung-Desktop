namespace Lab09_Entity_Framework
{
partial class TableSalesLogForm
{
private System.ComponentModel.IContainer components = null;
private System.Windows.Forms.DataGridView dgvLog;
private System.Windows.Forms.Label lblSummary;

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
this.dgvLog = new System.Windows.Forms.DataGridView();
this.lblSummary = new System.Windows.Forms.Label();
((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
this.SuspendLayout();
// 
// dgvLog
// 
this.dgvLog.AllowUserToAddRows = false;
this.dgvLog.AllowUserToDeleteRows = false;
this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
| System.Windows.Forms.AnchorStyles.Left) 
| System.Windows.Forms.AnchorStyles.Right)));
this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
this.dgvLog.Location = new System.Drawing.Point(12,12);
this.dgvLog.Name = "dgvLog";
this.dgvLog.ReadOnly = true;
this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
this.dgvLog.Size = new System.Drawing.Size(560,330);
this.dgvLog.TabIndex =0;
// 
// lblSummary
// 
this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
this.lblSummary.AutoSize = true;
this.lblSummary.Location = new System.Drawing.Point(12,351);
this.lblSummary.Name = "lblSummary";
this.lblSummary.Size = new System.Drawing.Size(63,13);
this.lblSummary.TabIndex =1;
this.lblSummary.Text = "T?ng h?p";
this.lblSummary.UseCompatibleTextRendering = true;
// 
// TableSalesLogForm
// 
this.AutoScaleDimensions = new System.Drawing.SizeF(7F,15F);
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
this.ClientSize = new System.Drawing.Size(584,381);
this.Controls.Add(this.lblSummary);
this.Controls.Add(this.dgvLog);
this.Font = new System.Drawing.Font("Segoe UI",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
this.Name = "TableSalesLogForm";
this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
this.Text = "Nh?t k? bán hàng";
this.Load += new System.EventHandler(this.TableSalesLogForm_Load);
((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
this.ResumeLayout(false);
this.PerformLayout();
}
}
}
