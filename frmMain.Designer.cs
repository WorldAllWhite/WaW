namespace WaW
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lvwUsers = new System.Windows.Forms.ListView();
            this.chTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHostname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblUserCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbIpList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lvwUsers
            // 
            this.lvwUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTag,
            this.chUser,
            this.chIP,
            this.chHostname});
            this.lvwUsers.Location = new System.Drawing.Point(0, 44);
            this.lvwUsers.Name = "lvwUsers";
            this.lvwUsers.Size = new System.Drawing.Size(234, 541);
            this.lvwUsers.TabIndex = 0;
            this.lvwUsers.UseCompatibleStateImageBehavior = false;
            this.lvwUsers.View = System.Windows.Forms.View.Details;
            this.lvwUsers.ItemActivate += new System.EventHandler(this.lvwUsers_ItemActivate);
            // 
            // chTag
            // 
            this.chTag.DisplayIndex = 3;
            // 
            // chUser
            // 
            this.chUser.DisplayIndex = 0;
            this.chUser.Text = "用户名";
            this.chUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chUser.Width = 78;
            // 
            // chIP
            // 
            this.chIP.DisplayIndex = 1;
            this.chIP.Text = "IP";
            this.chIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chIP.Width = 78;
            // 
            // chHostname
            // 
            this.chHostname.DisplayIndex = 2;
            this.chHostname.Text = "主机名";
            this.chHostname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chHostname.Width = 78;
            // 
            // lblUserCount
            // 
            this.lblUserCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Location = new System.Drawing.Point(2, 592);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(79, 13);
            this.lblUserCount.TabIndex = 1;
            this.lblUserCount.Text = "当前用户数：";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(188, 587);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbIpList
            // 
            this.cmbIpList.BackColor = System.Drawing.SystemColors.Menu;
            this.cmbIpList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIpList.FormattingEnabled = true;
            this.cmbIpList.Location = new System.Drawing.Point(48, 12);
            this.cmbIpList.Name = "cmbIpList";
            this.cmbIpList.Size = new System.Drawing.Size(121, 21);
            this.cmbIpList.TabIndex = 3;
            this.cmbIpList.SelectedIndexChanged += new System.EventHandler(this.cmbIpList_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 612);
            this.Controls.Add(this.cmbIpList);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblUserCount);
            this.Controls.Add(this.lvwUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WaW";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwUsers;
        private System.Windows.Forms.ColumnHeader chUser;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.ColumnHeader chHostname;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ComboBox cmbIpList;
    }
}

