namespace BaiTap2
{
    partial class frmBai3
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
            this.lblSoKetQua = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnKetQua = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdTinhGiaiThua = new System.Windows.Forms.RadioButton();
            this.rdNoiChuoi = new System.Windows.Forms.RadioButton();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtN = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSoKetQua
            // 
            this.lblSoKetQua.AutoSize = true;
            this.lblSoKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoKetQua.Location = new System.Drawing.Point(233, 312);
            this.lblSoKetQua.Name = "lblSoKetQua";
            this.lblSoKetQua.Size = new System.Drawing.Size(18, 20);
            this.lblSoKetQua.TabIndex = 11;
            this.lblSoKetQua.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(133, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Kết quả là: ";
            // 
            // btnKetQua
            // 
            this.btnKetQua.Location = new System.Drawing.Point(128, 253);
            this.btnKetQua.Name = "btnKetQua";
            this.btnKetQua.Size = new System.Drawing.Size(113, 39);
            this.btnKetQua.TabIndex = 3;
            this.btnKetQua.Text = "Xem kết quả";
            this.btnKetQua.UseVisualStyleBackColor = true;
            this.btnKetQua.Click += new System.EventHandler(this.btnKetQua_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdTinhGiaiThua);
            this.groupBox1.Controls.Add(this.rdNoiChuoi);
            this.groupBox1.Location = new System.Drawing.Point(128, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 84);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn";
            // 
            // rdTinhGiaiThua
            // 
            this.rdTinhGiaiThua.AutoSize = true;
            this.rdTinhGiaiThua.Location = new System.Drawing.Point(15, 47);
            this.rdTinhGiaiThua.Name = "rdTinhGiaiThua";
            this.rdTinhGiaiThua.Size = new System.Drawing.Size(117, 20);
            this.rdTinhGiaiThua.TabIndex = 3;
            this.rdTinhGiaiThua.TabStop = true;
            this.rdTinhGiaiThua.Text = "Tính giai thừa n";
            this.rdTinhGiaiThua.UseVisualStyleBackColor = true;
            // 
            // rdNoiChuoi
            // 
            this.rdNoiChuoi.AutoSize = true;
            this.rdNoiChuoi.Checked = true;
            this.rdNoiChuoi.Location = new System.Drawing.Point(15, 21);
            this.rdNoiChuoi.Name = "rdNoiChuoi";
            this.rdNoiChuoi.Size = new System.Drawing.Size(84, 20);
            this.rdNoiChuoi.TabIndex = 3;
            this.rdNoiChuoi.TabStop = true;
            this.rdNoiChuoi.Text = "Nối chuỗi";
            this.rdNoiChuoi.UseVisualStyleBackColor = true;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(128, 84);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(147, 22);
            this.txtTen.TabIndex = 1;
            // 
            // txtHo
            // 
            this.txtHo.Location = new System.Drawing.Point(128, 49);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(147, 22);
            this.txtHo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nhập tên: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nhập họ: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nhập n: ";
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(128, 124);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(147, 22);
            this.txtN.TabIndex = 2;
            // 
            // frmBai3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 389);
            this.Controls.Add(this.lblSoKetQua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnKetQua);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtN);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtHo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBai3";
            this.Text = "Bài 3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSoKetQua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnKetQua;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdTinhGiaiThua;
        private System.Windows.Forms.RadioButton rdNoiChuoi;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtN;
    }
}