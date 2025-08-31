namespace BaiTap3
{
    partial class frmBai2
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
            this.txtNhapN = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTinhNGiaiThua = new System.Windows.Forms.RadioButton();
            this.rdTinhTong1DenN = new System.Windows.Forms.RadioButton();
            this.btnXemKetQua = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập một số nguyên dương N: ";
            // 
            // txtNhapN
            // 
            this.txtNhapN.Location = new System.Drawing.Point(221, 57);
            this.txtNhapN.Name = "txtNhapN";
            this.txtNhapN.Size = new System.Drawing.Size(163, 22);
            this.txtNhapN.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTinhNGiaiThua);
            this.groupBox1.Controls.Add(this.rdTinhTong1DenN);
            this.groupBox1.Location = new System.Drawing.Point(149, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn công việc";
            // 
            // txtTinhNGiaiThua
            // 
            this.txtTinhNGiaiThua.AutoSize = true;
            this.txtTinhNGiaiThua.Location = new System.Drawing.Point(27, 59);
            this.txtTinhNGiaiThua.Name = "txtTinhNGiaiThua";
            this.txtTinhNGiaiThua.Size = new System.Drawing.Size(144, 20);
            this.txtTinhNGiaiThua.TabIndex = 3;
            this.txtTinhNGiaiThua.TabStop = true;
            this.txtTinhNGiaiThua.Text = "Tính N giai thừa (N!)";
            this.txtTinhNGiaiThua.UseVisualStyleBackColor = true;
            // 
            // rdTinhTong1DenN
            // 
            this.rdTinhTong1DenN.AutoSize = true;
            this.rdTinhTong1DenN.Location = new System.Drawing.Point(27, 33);
            this.rdTinhTong1DenN.Name = "rdTinhTong1DenN";
            this.rdTinhTong1DenN.Size = new System.Drawing.Size(140, 20);
            this.rdTinhTong1DenN.TabIndex = 3;
            this.rdTinhTong1DenN.TabStop = true;
            this.rdTinhTong1DenN.Text = "Tính tổng 1+2+...+N";
            this.rdTinhTong1DenN.UseVisualStyleBackColor = true;
            // 
            // btnXemKetQua
            // 
            this.btnXemKetQua.Location = new System.Drawing.Point(186, 225);
            this.btnXemKetQua.Name = "btnXemKetQua";
            this.btnXemKetQua.Size = new System.Drawing.Size(122, 41);
            this.btnXemKetQua.TabIndex = 3;
            this.btnXemKetQua.Text = "Xem kết quả";
            this.btnXemKetQua.UseVisualStyleBackColor = true;
            this.btnXemKetQua.Click += new System.EventHandler(this.btnXemKetQua_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(145, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kết quả là: ";
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKetQua.Location = new System.Drawing.Point(245, 294);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(18, 20);
            this.lblKetQua.TabIndex = 4;
            this.lblKetQua.Text = "0";
            // 
            // frmBai2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 351);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnXemKetQua);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNhapN);
            this.Controls.Add(this.label1);
            this.Name = "frmBai2";
            this.Text = "frmBai2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNhapN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton txtTinhNGiaiThua;
        private System.Windows.Forms.RadioButton rdTinhTong1DenN;
        private System.Windows.Forms.Button btnXemKetQua;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblKetQua;
    }
}