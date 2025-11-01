namespace Lab09_Entity_Framework
{
 partial class MenuForm
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.ComboBox cbbTypeFilter;
 private System.Windows.Forms.ComboBox cbbCategoryFilter;
 private System.Windows.Forms.TextBox txtKeyword;
 private System.Windows.Forms.Button btnFilter;
 private System.Windows.Forms.Button btnPrint;
 private System.Windows.Forms.Button btnClose;
 private System.Windows.Forms.Label lblType;
 private System.Windows.Forms.Label lblCategory;
 private System.Windows.Forms.Label lblKeyword;
 private System.Windows.Forms.FlowLayoutPanel flpMenu;

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
 this.cbbTypeFilter = new System.Windows.Forms.ComboBox();
 this.cbbCategoryFilter = new System.Windows.Forms.ComboBox();
 this.txtKeyword = new System.Windows.Forms.TextBox();
 this.btnFilter = new System.Windows.Forms.Button();
 this.btnPrint = new System.Windows.Forms.Button();
 this.btnClose = new System.Windows.Forms.Button();
 this.lblType = new System.Windows.Forms.Label();
 this.lblCategory = new System.Windows.Forms.Label();
 this.lblKeyword = new System.Windows.Forms.Label();
 this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
 this.SuspendLayout();
 // 
 // Labels
 // 
 this.lblType.AutoSize = true;
 this.lblType.Location = new System.Drawing.Point(12,15);
 this.lblType.Name = "lblType";
 this.lblType.Size = new System.Drawing.Size(36,15);
 this.lblType.Text = "Lo?i:";
 this.lblType.UseCompatibleTextRendering = true;
 this.lblCategory.AutoSize = true;
 this.lblCategory.Location = new System.Drawing.Point(170,15);
 this.lblCategory.Name = "lblCategory";
 this.lblCategory.Size = new System.Drawing.Size(64,15);
 this.lblCategory.Text = "Danh m?c:";
 this.lblCategory.UseCompatibleTextRendering = true;
 this.lblKeyword.AutoSize = true;
 this.lblKeyword.Location = new System.Drawing.Point(390,15);
 this.lblKeyword.Name = "lblKeyword";
 this.lblKeyword.Size = new System.Drawing.Size(53,15);
 this.lblKeyword.Text = "T? khóa:";
 this.lblKeyword.UseCompatibleTextRendering = true;
 // 
 // cbbTypeFilter
 // 
 this.cbbTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
 this.cbbTypeFilter.Location = new System.Drawing.Point(60,12);
 this.cbbTypeFilter.Name = "cbbTypeFilter";
 this.cbbTypeFilter.Size = new System.Drawing.Size(100,21);
 this.cbbTypeFilter.TabIndex =0;
 // 
 // cbbCategoryFilter
 // 
 this.cbbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
 this.cbbCategoryFilter.Location = new System.Drawing.Point(235,12);
 this.cbbCategoryFilter.Name = "cbbCategoryFilter";
 this.cbbCategoryFilter.Size = new System.Drawing.Size(140,21);
 this.cbbCategoryFilter.TabIndex =1;
 // 
 // txtKeyword
 // 
 this.txtKeyword.Location = new System.Drawing.Point(455,12);
 this.txtKeyword.Name = "txtKeyword";
 this.txtKeyword.Size = new System.Drawing.Size(160,20);
 this.txtKeyword.TabIndex =2;
 // 
 // btnFilter
 // 
 this.btnFilter.Location = new System.Drawing.Point(625,10);
 this.btnFilter.Name = "btnFilter";
 this.btnFilter.Size = new System.Drawing.Size(60,23);
 this.btnFilter.TabIndex =3;
 this.btnFilter.Text = "L?c";
 this.btnFilter.UseCompatibleTextRendering = true;
 this.btnFilter.UseVisualStyleBackColor = true;
 this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
 // 
 // flpMenu
 // 
 this.flpMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
 | System.Windows.Forms.AnchorStyles.Left)
 | System.Windows.Forms.AnchorStyles.Right)));
 this.flpMenu.AutoScroll = true;
 this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
 this.flpMenu.WrapContents = false;
 this.flpMenu.Location = new System.Drawing.Point(12,45);
 this.flpMenu.Name = "flpMenu";
 this.flpMenu.Size = new System.Drawing.Size(760,360);
 this.flpMenu.TabIndex =4;
 this.flpMenu.BackColor = System.Drawing.Color.WhiteSmoke;
 this.flpMenu.SizeChanged += new System.EventHandler(this.flpMenu_SizeChanged);
 // 
 // btnPrint
 // 
 this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
 this.btnPrint.Location = new System.Drawing.Point(12,415);
 this.btnPrint.Name = "btnPrint";
 this.btnPrint.Size = new System.Drawing.Size(100,25);
 this.btnPrint.TabIndex =5;
 this.btnPrint.Text = "In th?c ðõn";
 this.btnPrint.UseCompatibleTextRendering = true;
 this.btnPrint.UseVisualStyleBackColor = true;
 this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
 // 
 // btnClose
 // 
 this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
 this.btnClose.Location = new System.Drawing.Point(697,415);
 this.btnClose.Name = "btnClose";
 this.btnClose.Size = new System.Drawing.Size(75,25);
 this.btnClose.TabIndex =6;
 this.btnClose.Text = "Ðóng";
 this.btnClose.UseCompatibleTextRendering = true;
 this.btnClose.UseVisualStyleBackColor = true;
 this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
 // 
 // MenuForm
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(7F,15F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,451);
 this.Controls.Add(this.btnClose);
 this.Controls.Add(this.btnPrint);
 this.Controls.Add(this.flpMenu);
 this.Controls.Add(this.btnFilter);
 this.Controls.Add(this.txtKeyword);
 this.Controls.Add(this.cbbCategoryFilter);
 this.Controls.Add(this.cbbTypeFilter);
 this.Controls.Add(this.lblKeyword);
 this.Controls.Add(this.lblCategory);
 this.Controls.Add(this.lblType);
 this.Font = new System.Drawing.Font("Segoe UI",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
 this.Name = "MenuForm";
 this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
 this.Text = "Th?c ðõn";
 this.Load += new System.EventHandler(this.MenuForm_Load);
 this.ResumeLayout(false);
 this.PerformLayout();
 }
 }
}
