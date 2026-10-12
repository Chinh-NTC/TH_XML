namespace Bai12
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
            this.btnTaiDuLieu = new System.Windows.Forms.Button();
            this.btnXuatXML = new System.Windows.Forms.Button();
            this.btnXuatHTML = new System.Windows.Forms.Button();
            this.dgvDuLieu = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuLieu)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTaiDuLieu
            // 
            this.btnTaiDuLieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnTaiDuLieu.FlatAppearance.BorderSize = 0;
            this.btnTaiDuLieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiDuLieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaiDuLieu.ForeColor = System.Drawing.Color.White;
            this.btnTaiDuLieu.Location = new System.Drawing.Point(140, 70);
            this.btnTaiDuLieu.Name = "btnTaiDuLieu";
            this.btnTaiDuLieu.Size = new System.Drawing.Size(180, 45);
            this.btnTaiDuLieu.TabIndex = 0;
            this.btnTaiDuLieu.Text = "1. TẢI DỮ LIỆU SQL";
            this.btnTaiDuLieu.UseVisualStyleBackColor = false;
            this.btnTaiDuLieu.Click += new System.EventHandler(this.btnTaiDuLieu_Click);
            // 
            // btnXuatXML
            // 
            this.btnXuatXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(50)))));
            this.btnXuatXML.FlatAppearance.BorderSize = 0;
            this.btnXuatXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatXML.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatXML.ForeColor = System.Drawing.Color.White;
            this.btnXuatXML.Location = new System.Drawing.Point(340, 70);
            this.btnXuatXML.Name = "btnXuatXML";
            this.btnXuatXML.Size = new System.Drawing.Size(180, 45);
            this.btnXuatXML.TabIndex = 1;
            this.btnXuatXML.Text = "2. XUẤT BÁO CÁO XML";
            this.btnXuatXML.UseVisualStyleBackColor = false;
            this.btnXuatXML.Click += new System.EventHandler(this.btnXuatXML_Click);
            // 
            // btnXuatHTML
            // 
            this.btnXuatHTML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(177)))), ((int)(((byte)(76)))));
            this.btnXuatHTML.FlatAppearance.BorderSize = 0;
            this.btnXuatHTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatHTML.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatHTML.ForeColor = System.Drawing.Color.White;
            this.btnXuatHTML.Location = new System.Drawing.Point(540, 70);
            this.btnXuatHTML.Name = "btnXuatHTML";
            this.btnXuatHTML.Size = new System.Drawing.Size(200, 45);
            this.btnXuatHTML.TabIndex = 2;
            this.btnXuatHTML.Text = "3. XUẤT BÁO CÁO HTML";
            this.btnXuatHTML.UseVisualStyleBackColor = false;
            this.btnXuatHTML.Click += new System.EventHandler(this.btnXuatHTML_Click);
            // 
            // dgvDuLieu
            // 
            this.dgvDuLieu.AllowUserToAddRows = false;
            this.dgvDuLieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDuLieu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDuLieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuLieu.Location = new System.Drawing.Point(30, 135);
            this.dgvDuLieu.Name = "dgvDuLieu";
            this.dgvDuLieu.ReadOnly = true;
            this.dgvDuLieu.RowHeadersVisible = false;
            this.dgvDuLieu.RowHeadersWidth = 51;
            this.dgvDuLieu.Size = new System.Drawing.Size(820, 350);
            this.dgvDuLieu.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(193, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(495, 37);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "XUẤT BÁO CÁO THIẾT BỊ PHÒNG LAB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(880, 520);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvDuLieu);
            this.Controls.Add(this.btnXuatHTML);
            this.Controls.Add(this.btnXuatXML);
            this.Controls.Add(this.btnTaiDuLieu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bài Tập 12 - Quản Lý Phòng Lab";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuLieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTaiDuLieu;
        private System.Windows.Forms.Button btnXuatXML;
        private System.Windows.Forms.Button btnXuatHTML;
        private System.Windows.Forms.DataGridView dgvDuLieu;
        private System.Windows.Forms.Label lblTitle;
    }
}