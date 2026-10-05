namespace Bai11
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.dgvNguon = new System.Windows.Forms.DataGridView();
            this.dgvDich = new System.Windows.Forms.DataGridView();
            this.lblLog = new System.Windows.Forms.Label();
            this.lblNguon = new System.Windows.Forms.Label();
            this.lblDich = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDich)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXuat
            // 
            this.btnXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnXuat.FlatAppearance.BorderSize = 0;
            this.btnXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuat.ForeColor = System.Drawing.Color.White;
            this.btnXuat.Location = new System.Drawing.Point(126, 20);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(220, 45);
            this.btnXuat.TabIndex = 0;
            this.btnXuat.Text = "1. Xuất MySQL -> XML";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(177)))), ((int)(((byte)(76)))));
            this.btnNhap.FlatAppearance.BorderSize = 0;
            this.btnNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNhap.ForeColor = System.Drawing.Color.White;
            this.btnNhap.Location = new System.Drawing.Point(622, 20);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(220, 45);
            this.btnNhap.TabIndex = 1;
            this.btnNhap.Text = "2. Nhập XML -> SQL Server";
            this.btnNhap.UseVisualStyleBackColor = false;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtLog.Location = new System.Drawing.Point(30, 395);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(910, 130);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // dgvNguon
            // 
            this.dgvNguon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNguon.BackgroundColor = System.Drawing.Color.White;
            this.dgvNguon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNguon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNguon.Location = new System.Drawing.Point(30, 105);
            this.dgvNguon.Name = "dgvNguon";
            this.dgvNguon.RowHeadersVisible = false;
            this.dgvNguon.RowHeadersWidth = 51;
            this.dgvNguon.Size = new System.Drawing.Size(440, 250);
            this.dgvNguon.TabIndex = 3;
            // 
            // dgvDich
            // 
            this.dgvDich.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDich.BackgroundColor = System.Drawing.Color.White;
            this.dgvDich.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDich.Location = new System.Drawing.Point(500, 105);
            this.dgvDich.Name = "dgvDich";
            this.dgvDich.RowHeadersVisible = false;
            this.dgvDich.RowHeadersWidth = 51;
            this.dgvDich.Size = new System.Drawing.Size(440, 250);
            this.dgvDich.TabIndex = 4;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLog.ForeColor = System.Drawing.Color.DimGray;
            this.lblLog.Location = new System.Drawing.Point(27, 375);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(130, 20);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Nhật ký đồng bộ:";
            // 
            // lblNguon
            // 
            this.lblNguon.AutoSize = true;
            this.lblNguon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNguon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblNguon.Location = new System.Drawing.Point(27, 80);
            this.lblNguon.Name = "lblNguon";
            this.lblNguon.Size = new System.Drawing.Size(218, 23);
            this.lblNguon.TabIndex = 5;
            this.lblNguon.Text = "DỮ LIỆU NGUỒN (MySQL)";
            // 
            // lblDich
            // 
            this.lblDich.AutoSize = true;
            this.lblDich.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(177)))), ((int)(((byte)(76)))));
            this.lblDich.Location = new System.Drawing.Point(497, 80);
            this.lblDich.Name = "lblDich";
            this.lblDich.Size = new System.Drawing.Size(229, 23);
            this.lblDich.TabIndex = 6;
            this.lblDich.Text = "DỮ LIỆU ĐÍCH (SQL Server)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(970, 550);
            this.Controls.Add(this.lblDich);
            this.Controls.Add(this.lblNguon);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.dgvDich);
            this.Controls.Add(this.dgvNguon);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnXuat);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bài Tập 11 - Đồng Bộ Dữ Liệu Qua XML";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnNhap;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.DataGridView dgvNguon;
        private System.Windows.Forms.DataGridView dgvDich;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblNguon;
        private System.Windows.Forms.Label lblDich;
    }
}