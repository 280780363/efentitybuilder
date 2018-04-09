namespace Generator
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cbxlTables = new System.Windows.Forms.CheckedListBox();
            this.cbxAll = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑实体模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑数据库上下文模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑类型映射ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑查询语句ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtContextPrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxGenerateContext = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxlTables
            // 
            this.cbxlTables.FormattingEnabled = true;
            this.cbxlTables.Location = new System.Drawing.Point(7, 111);
            this.cbxlTables.Name = "cbxlTables";
            this.cbxlTables.Size = new System.Drawing.Size(323, 388);
            this.cbxlTables.TabIndex = 0;
            // 
            // cbxAll
            // 
            this.cbxAll.AutoSize = true;
            this.cbxAll.Location = new System.Drawing.Point(10, 91);
            this.cbxAll.Name = "cbxAll";
            this.cbxAll.Size = new System.Drawing.Size(48, 16);
            this.cbxAll.TabIndex = 1;
            this.cbxAll.Text = "全选";
            this.cbxAll.UseVisualStyleBackColor = true;
            this.cbxAll.CheckedChanged += new System.EventHandler(this.cbxAll_CheckedChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(341, 111);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(341, 388);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑实体模板ToolStripMenuItem,
            this.编辑数据库上下文模板ToolStripMenuItem,
            this.编辑类型映射ToolStripMenuItem,
            this.编辑查询语句ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 编辑实体模板ToolStripMenuItem
            // 
            this.编辑实体模板ToolStripMenuItem.Name = "编辑实体模板ToolStripMenuItem";
            this.编辑实体模板ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.编辑实体模板ToolStripMenuItem.Text = "编辑实体模板";
            this.编辑实体模板ToolStripMenuItem.Click += new System.EventHandler(this.编辑实体模板ToolStripMenuItem_Click);
            // 
            // 编辑数据库上下文模板ToolStripMenuItem
            // 
            this.编辑数据库上下文模板ToolStripMenuItem.Name = "编辑数据库上下文模板ToolStripMenuItem";
            this.编辑数据库上下文模板ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.编辑数据库上下文模板ToolStripMenuItem.Text = "编辑数据库上下文模板";
            this.编辑数据库上下文模板ToolStripMenuItem.Click += new System.EventHandler(this.编辑数据库上下文模板ToolStripMenuItem_Click);
            // 
            // 编辑类型映射ToolStripMenuItem
            // 
            this.编辑类型映射ToolStripMenuItem.Name = "编辑类型映射ToolStripMenuItem";
            this.编辑类型映射ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.编辑类型映射ToolStripMenuItem.Text = "编辑类型映射";
            this.编辑类型映射ToolStripMenuItem.Click += new System.EventHandler(this.编辑类型映射ToolStripMenuItem_Click);
            // 
            // 编辑查询语句ToolStripMenuItem
            // 
            this.编辑查询语句ToolStripMenuItem.Name = "编辑查询语句ToolStripMenuItem";
            this.编辑查询语句ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.编辑查询语句ToolStripMenuItem.Text = "编辑查询语句";
            this.编辑查询语句ToolStripMenuItem.Click += new System.EventHandler(this.编辑查询语句ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "保存目录";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(82, 30);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(279, 21);
            this.txtSavePath.TabIndex = 5;
            this.txtSavePath.Text = "Entities";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(367, 29);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(76, 48);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "开始生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtContextPrefix
            // 
            this.txtContextPrefix.Location = new System.Drawing.Point(176, 56);
            this.txtContextPrefix.Name = "txtContextPrefix";
            this.txtContextPrefix.Size = new System.Drawing.Size(124, 21);
            this.txtContextPrefix.TabIndex = 5;
            this.txtContextPrefix.Text = "Your";
            this.txtContextPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "DbContext";
            // 
            // cbxGenerateContext
            // 
            this.cbxGenerateContext.AutoSize = true;
            this.cbxGenerateContext.Checked = true;
            this.cbxGenerateContext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGenerateContext.Location = new System.Drawing.Point(25, 58);
            this.cbxGenerateContext.Name = "cbxGenerateContext";
            this.cbxGenerateContext.Size = new System.Drawing.Size(102, 16);
            this.cbxGenerateContext.TabIndex = 1;
            this.cbxGenerateContext.Text = "创建DbContext";
            this.cbxGenerateContext.UseVisualStyleBackColor = true;
            this.cbxGenerateContext.CheckedChanged += new System.EventHandler(this.cbxGenerateContext_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 503);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtContextPrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.cbxGenerateContext);
            this.Controls.Add(this.cbxAll);
            this.Controls.Add(this.cbxlTables);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EFCore实体构建工具";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cbxlTables;
        private System.Windows.Forms.CheckBox cbxAll;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑实体模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑数据库上下文模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑类型映射ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑查询语句ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtContextPrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxGenerateContext;
    }
}