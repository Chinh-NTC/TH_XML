namespace Bai7
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

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem listCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCustomerHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportItemHTMLToolStripMenuItem;

        private System.Windows.Forms.ComboBox cboOrder;
        private System.Windows.Forms.ComboBox cboCCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Label lblCCode;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblTotalText;
        private System.Windows.Forms.Label lblDate;

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCustomerHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportItemHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.cboOrder = new System.Windows.Forms.ComboBox();
            this.cboCCode = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.lblCCode = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listCustomerToolStripMenuItem,
            this.listItemToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // listCustomerToolStripMenuItem
            // 
            this.listCustomerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCustomerHTMLToolStripMenuItem});
            this.listCustomerToolStripMenuItem.Name = "listCustomerToolStripMenuItem";
            this.listCustomerToolStripMenuItem.Size = new System.Drawing.Size(120, 21);
            this.listCustomerToolStripMenuItem.Text = "Danh sách khách";
            this.listCustomerToolStripMenuItem.Click += new System.EventHandler(this.listCustomerToolStripMenuItem_Click);
            // 
            // exportCustomerHTMLToolStripMenuItem
            // 
            this.exportCustomerHTMLToolStripMenuItem.Name = "exportCustomerHTMLToolStripMenuItem";
            this.exportCustomerHTMLToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exportCustomerHTMLToolStripMenuItem.Text = "Chuyển đổi HTML";
            this.exportCustomerHTMLToolStripMenuItem.Click += new System.EventHandler(this.exportCustomerHTMLToolStripMenuItem_Click);
            // 
            // listItemToolStripMenuItem
            // 
            this.listItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportItemHTMLToolStripMenuItem});
            this.listItemToolStripMenuItem.Name = "listItemToolStripMenuItem";
            this.listItemToolStripMenuItem.Size = new System.Drawing.Size(135, 21);
            this.listItemToolStripMenuItem.Text = "Danh sách mặt hàng";
            this.listItemToolStripMenuItem.Click += new System.EventHandler(this.listItemToolStripMenuItem_Click);
            // 
            // exportItemHTMLToolStripMenuItem
            // 
            this.exportItemHTMLToolStripMenuItem.Name = "exportItemHTMLToolStripMenuItem";
            this.exportItemHTMLToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exportItemHTMLToolStripMenuItem.Text = "Chuyển đổi HTML";
            this.exportItemHTMLToolStripMenuItem.Click += new System.EventHandler(this.exportItemHTMLToolStripMenuItem_Click);
            // 
            // cboOrder
            // 
            this.cboOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrder.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrder.FormattingEnabled = true;
            this.cboOrder.Location = new System.Drawing.Point(95, 110);
            this.cboOrder.Name = "cboOrder";
            this.cboOrder.Size = new System.Drawing.Size(160, 25);
            this.cboOrder.TabIndex = 1;
            this.cboOrder.SelectedIndexChanged += new System.EventHandler(this.cboOrder_SelectedIndexChanged);
            // 
            // cboCCode
            // 
            this.cboCCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCode.FormattingEnabled = true;
            this.cboCCode.Location = new System.Drawing.Point(340, 110);
            this.cboCCode.Name = "cboCCode";
            this.cboCCode.Size = new System.Drawing.Size(150, 25);
            this.cboCCode.TabIndex = 2;
            this.cboCCode.SelectedIndexChanged += new System.EventHandler(this.cboCCode_SelectedIndexChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.Color.Maroon;
            this.lblCustomer.Location = new System.Drawing.Point(510, 112);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(126, 19);
            this.lblCustomer.TabIndex = 3;
            this.lblCustomer.Text = "Tên khách hàng...";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(25, 155);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 30;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(655, 230);
            this.dgvData.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Navy;
            this.lblTotal.Location = new System.Drawing.Point(115, 403);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(17, 19);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "0";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.Location = new System.Drawing.Point(25, 114);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(64, 17);
            this.lblOrderNo.TabIndex = 6;
            this.lblOrderNo.Text = "Mã Đơn:";
            // 
            // lblCCode
            // 
            this.lblCCode.AutoSize = true;
            this.lblCCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCode.Location = new System.Drawing.Point(270, 114);
            this.lblCCode.Name = "lblCCode";
            this.lblCCode.Size = new System.Drawing.Size(66, 17);
            this.lblCCode.TabIndex = 7;
            this.lblCCode.Text = "Mã Khách:";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.DarkRed;
            this.lblHeaderTitle.Location = new System.Drawing.Point(22, 43);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(142, 40);
            this.lblHeaderTitle.TabIndex = 8;
            this.lblHeaderTitle.Text = "ORDER";
            // 
            // lblTotalText
            // 
            this.lblTotalText.AutoSize = true;
            this.lblTotalText.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalText.Location = new System.Drawing.Point(25, 403);
            this.lblTotalText.Name = "lblTotalText";
            this.lblTotalText.Size = new System.Drawing.Size(76, 19);
            this.lblTotalText.TabIndex = 9;
            this.lblTotalText.Text = "Tổng:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(575, 53);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(75, 17);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "22/10/2021";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(710, 440);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTotalText);
            this.Controls.Add(this.lblHeaderTitle);
            this.Controls.Add(this.lblCCode);
            this.Controls.Add(this.lblOrderNo);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.cboCCode);
            this.Controls.Add(this.cboOrder);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý đơn hàng XML";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}