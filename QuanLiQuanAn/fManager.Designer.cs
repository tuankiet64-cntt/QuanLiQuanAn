
namespace QuanLiQuanAn
{
    partial class fManager
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AdminToolMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolScrpit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstBill = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMergeTable = new System.Windows.Forms.ComboBox();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.btnswich = new System.Windows.Forms.Button();
            this.cbSwitchTable = new System.Windows.Forms.ComboBox();
            this.numudDiscount = new System.Windows.Forms.NumericUpDown();
            this.btnMergeTable = new System.Windows.Forms.Button();
            this.btnpay = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numudFood = new System.Windows.Forms.NumericUpDown();
            this.btnadd = new System.Windows.Forms.Button();
            this.cbFood = new System.Windows.Forms.ComboBox();
            this.cbCate = new System.Windows.Forms.ComboBox();
            this.flpTable = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numudDiscount)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numudFood)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdminToolMenuStrip,
            this.infoToolScrpit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // AdminToolMenuStrip
            // 
            this.AdminToolMenuStrip.Name = "AdminToolMenuStrip";
            this.AdminToolMenuStrip.Size = new System.Drawing.Size(67, 24);
            this.AdminToolMenuStrip.Text = "Admin";
            this.AdminToolMenuStrip.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // infoToolScrpit
            // 
            this.infoToolScrpit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.infoToolScrpit.Name = "infoToolScrpit";
            this.infoToolScrpit.Size = new System.Drawing.Size(151, 24);
            this.infoToolScrpit.Text = "Thông tin tài khoản";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem5.Text = "Thông tin cá nhân";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem6.Text = "Đăng xuất";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(210, 26);
            this.toolStripMenuItem3.Text = "Thông tin cá nhân";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(210, 26);
            this.toolStripMenuItem4.Text = "Đăng xuất";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lstBill);
            this.panel2.Location = new System.Drawing.Point(649, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 353);
            this.panel2.TabIndex = 2;
            // 
            // lstBill
            // 
            this.lstBill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4});
            this.lstBill.GridLines = true;
            this.lstBill.HideSelection = false;
            this.lstBill.Location = new System.Drawing.Point(0, 0);
            this.lstBill.Name = "lstBill";
            this.lstBill.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstBill.Size = new System.Drawing.Size(435, 353);
            this.lstBill.TabIndex = 0;
            this.lstBill.UseCompatibleStateImageBehavior = false;
            this.lstBill.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên Món ăn";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Số lượng";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Đơn Giá";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thành tiền";
            this.columnHeader4.Width = 100;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cbMergeTable);
            this.panel3.Controls.Add(this.txtTotalPrice);
            this.panel3.Controls.Add(this.btnswich);
            this.panel3.Controls.Add(this.cbSwitchTable);
            this.panel3.Controls.Add(this.numudDiscount);
            this.panel3.Controls.Add(this.btnMergeTable);
            this.panel3.Controls.Add(this.btnpay);
            this.panel3.Location = new System.Drawing.Point(649, 512);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(435, 101);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Giảm giá";
            // 
            // cbMergeTable
            // 
            this.cbMergeTable.FormattingEnabled = true;
            this.cbMergeTable.Location = new System.Drawing.Point(0, 25);
            this.cbMergeTable.Name = "cbMergeTable";
            this.cbMergeTable.Size = new System.Drawing.Size(99, 28);
            this.cbMergeTable.TabIndex = 9;
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTotalPrice.Location = new System.Drawing.Point(221, 56);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(114, 28);
            this.txtTotalPrice.TabIndex = 8;
            this.txtTotalPrice.Text = "0";
            this.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnswich
            // 
            this.btnswich.Location = new System.Drawing.Point(108, 58);
            this.btnswich.Name = "btnswich";
            this.btnswich.Size = new System.Drawing.Size(99, 31);
            this.btnswich.TabIndex = 7;
            this.btnswich.Text = "Chuyển bàn";
            this.btnswich.UseVisualStyleBackColor = true;
            this.btnswich.Click += new System.EventHandler(this.btnswich_Click);
            // 
            // cbSwitchTable
            // 
            this.cbSwitchTable.FormattingEnabled = true;
            this.cbSwitchTable.Location = new System.Drawing.Point(0, 61);
            this.cbSwitchTable.Name = "cbSwitchTable";
            this.cbSwitchTable.Size = new System.Drawing.Size(99, 28);
            this.cbSwitchTable.TabIndex = 6;
            // 
            // numudDiscount
            // 
            this.numudDiscount.Location = new System.Drawing.Point(221, 25);
            this.numudDiscount.Name = "numudDiscount";
            this.numudDiscount.Size = new System.Drawing.Size(114, 27);
            this.numudDiscount.TabIndex = 5;
            // 
            // btnMergeTable
            // 
            this.btnMergeTable.Location = new System.Drawing.Point(108, 21);
            this.btnMergeTable.Name = "btnMergeTable";
            this.btnMergeTable.Size = new System.Drawing.Size(94, 31);
            this.btnMergeTable.TabIndex = 4;
            this.btnMergeTable.Text = "Gộp bàn";
            this.btnMergeTable.UseVisualStyleBackColor = true;
            this.btnMergeTable.Click += new System.EventHandler(this.btnMergeTable_Click);
            // 
            // btnpay
            // 
            this.btnpay.Location = new System.Drawing.Point(341, 21);
            this.btnpay.Name = "btnpay";
            this.btnpay.Size = new System.Drawing.Size(94, 62);
            this.btnpay.TabIndex = 3;
            this.btnpay.Text = "Thanh toán";
            this.btnpay.UseVisualStyleBackColor = true;
            this.btnpay.Click += new System.EventHandler(this.btnpay_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.numudFood);
            this.panel4.Controls.Add(this.btnadd);
            this.panel4.Controls.Add(this.cbFood);
            this.panel4.Controls.Add(this.cbCate);
            this.panel4.Location = new System.Drawing.Point(649, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(435, 95);
            this.panel4.TabIndex = 4;
            // 
            // numudFood
            // 
            this.numudFood.Location = new System.Drawing.Point(369, 36);
            this.numudFood.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numudFood.Name = "numudFood";
            this.numudFood.Size = new System.Drawing.Size(45, 27);
            this.numudFood.TabIndex = 3;
            this.numudFood.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(269, 17);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(94, 62);
            this.btnadd.TabIndex = 2;
            this.btnadd.Text = "Thêm món";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // cbFood
            // 
            this.cbFood.FormattingEnabled = true;
            this.cbFood.Location = new System.Drawing.Point(15, 51);
            this.cbFood.Name = "cbFood";
            this.cbFood.Size = new System.Drawing.Size(248, 28);
            this.cbFood.TabIndex = 1;
            // 
            // cbCate
            // 
            this.cbCate.FormattingEnabled = true;
            this.cbCate.Location = new System.Drawing.Point(15, 17);
            this.cbCate.Name = "cbCate";
            this.cbCate.Size = new System.Drawing.Size(248, 28);
            this.cbCate.TabIndex = 0;
            this.cbCate.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // flpTable
            // 
            this.flpTable.Location = new System.Drawing.Point(13, 52);
            this.flpTable.Name = "flpTable";
            this.flpTable.Size = new System.Drawing.Size(630, 561);
            this.flpTable.TabIndex = 5;
            // 
            // fManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 625);
            this.Controls.Add(this.flpTable);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lí quán ăn";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numudDiscount)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numudFood)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AdminToolMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem infoToolScrpit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lstBill;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown numudFood;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.ComboBox cbFood;
        private System.Windows.Forms.ComboBox cbCate;
        private System.Windows.Forms.FlowLayoutPanel flpTable;
        private System.Windows.Forms.Button btnswich;
        private System.Windows.Forms.ComboBox cbSwitchTable;
        private System.Windows.Forms.NumericUpDown numudDiscount;
        private System.Windows.Forms.Button btnMergeTable;
        private System.Windows.Forms.Button btnpay;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.ComboBox cbMergeTable;
        private System.Windows.Forms.Label label1;
    }
}