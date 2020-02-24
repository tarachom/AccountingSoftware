namespace Configurator
{
	partial class FormConfiguration
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguration));
			this.splitContainerBase = new System.Windows.Forms.SplitContainer();
			this.treeConfiguration = new System.Windows.Forms.TreeView();
			this.imgTreeList = new System.Windows.Forms.ImageList(this.components);
			this.dataConfiguration = new System.Windows.Forms.DataGridView();
			this.menuStripTop = new System.Windows.Forms.MenuStrip();
			this.DirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewDirectiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openEnumItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
			this.splitContainerBase.Panel1.SuspendLayout();
			this.splitContainerBase.Panel2.SuspendLayout();
			this.splitContainerBase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).BeginInit();
			this.menuStripTop.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerBase
			// 
			this.splitContainerBase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBase.Location = new System.Drawing.Point(10, 34);
			this.splitContainerBase.Margin = new System.Windows.Forms.Padding(10);
			this.splitContainerBase.Name = "splitContainerBase";
			// 
			// splitContainerBase.Panel1
			// 
			this.splitContainerBase.Panel1.Controls.Add(this.treeConfiguration);
			// 
			// splitContainerBase.Panel2
			// 
			this.splitContainerBase.Panel2.Controls.Add(this.dataConfiguration);
			this.splitContainerBase.Size = new System.Drawing.Size(842, 465);
			this.splitContainerBase.SplitterDistance = 233;
			this.splitContainerBase.TabIndex = 0;
			// 
			// treeConfiguration
			// 
			this.treeConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeConfiguration.ImageIndex = 0;
			this.treeConfiguration.ImageList = this.imgTreeList;
			this.treeConfiguration.Location = new System.Drawing.Point(0, 0);
			this.treeConfiguration.Name = "treeConfiguration";
			this.treeConfiguration.SelectedImageIndex = 0;
			this.treeConfiguration.Size = new System.Drawing.Size(233, 465);
			this.treeConfiguration.TabIndex = 0;
			this.treeConfiguration.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeConfiguration_NodeMouseClick);
			this.treeConfiguration.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeConfiguration_NodeMouseDoubleClick);
			// 
			// imgTreeList
			// 
			this.imgTreeList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeList.ImageStream")));
			this.imgTreeList.TransparentColor = System.Drawing.Color.Transparent;
			this.imgTreeList.Images.SetKeyName(0, "1.ico");
			this.imgTreeList.Images.SetKeyName(1, "2.ico");
			this.imgTreeList.Images.SetKeyName(2, "26.ico");
			this.imgTreeList.Images.SetKeyName(3, "52.ico");
			// 
			// dataConfiguration
			// 
			this.dataConfiguration.AllowUserToAddRows = false;
			this.dataConfiguration.AllowUserToDeleteRows = false;
			this.dataConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataConfiguration.Location = new System.Drawing.Point(0, 0);
			this.dataConfiguration.Name = "dataConfiguration";
			this.dataConfiguration.Size = new System.Drawing.Size(605, 465);
			this.dataConfiguration.TabIndex = 0;
			// 
			// menuStripTop
			// 
			this.menuStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DirectoryToolStripMenuItem});
			this.menuStripTop.Location = new System.Drawing.Point(10, 10);
			this.menuStripTop.Name = "menuStripTop";
			this.menuStripTop.Size = new System.Drawing.Size(842, 24);
			this.menuStripTop.TabIndex = 1;
			this.menuStripTop.Text = "menuStrip1";
			// 
			// DirectoryToolStripMenuItem
			// 
			this.DirectoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDirectoryToolStripMenuItem,
            this.addEnumToolStripMenuItem,
            this.saveConfigurationToolStripMenuItem});
			this.DirectoryToolStripMenuItem.Name = "DirectoryToolStripMenuItem";
			this.DirectoryToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.DirectoryToolStripMenuItem.Text = "Довідники";
			// 
			// addDirectoryToolStripMenuItem
			// 
			this.addDirectoryToolStripMenuItem.Name = "addDirectoryToolStripMenuItem";
			this.addDirectoryToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.addDirectoryToolStripMenuItem.Text = "Додати новий довідник";
			this.addDirectoryToolStripMenuItem.Click += new System.EventHandler(this.addDirectoryToolStripMenuItem_Click);
			// 
			// saveConfigurationToolStripMenuItem
			// 
			this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
			this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.saveConfigurationToolStripMenuItem.Text = "SaveConfiguration";
			this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler(this.saveConfigurationToolStripMenuItem_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDirectoryToolStripMenuItem,
            this.addNewDirectiryToolStripMenuItem,
            this.copyDirectoryToolStripMenuItem,
            this.deleteDirectoryToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(151, 92);
			// 
			// openDirectoryToolStripMenuItem
			// 
			this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
			this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.openDirectoryToolStripMenuItem.Text = "Відкрити";
			this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
			// 
			// addNewDirectiryToolStripMenuItem
			// 
			this.addNewDirectiryToolStripMenuItem.Name = "addNewDirectiryToolStripMenuItem";
			this.addNewDirectiryToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.addNewDirectiryToolStripMenuItem.Text = "Додати новий";
			this.addNewDirectiryToolStripMenuItem.Click += new System.EventHandler(this.addNewDirectiryToolStripMenuItem_Click);
			// 
			// copyDirectoryToolStripMenuItem
			// 
			this.copyDirectoryToolStripMenuItem.Name = "copyDirectoryToolStripMenuItem";
			this.copyDirectoryToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.copyDirectoryToolStripMenuItem.Text = "Скопіювати";
			this.copyDirectoryToolStripMenuItem.Click += new System.EventHandler(this.copyDirectoryToolStripMenuItem_Click);
			// 
			// deleteDirectoryToolStripMenuItem
			// 
			this.deleteDirectoryToolStripMenuItem.Name = "deleteDirectoryToolStripMenuItem";
			this.deleteDirectoryToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.deleteDirectoryToolStripMenuItem.Text = "Видалити";
			this.deleteDirectoryToolStripMenuItem.Click += new System.EventHandler(this.deleteDirectoryToolStripMenuItem_Click);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openEnumItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(123, 26);
			// 
			// openEnumItem
			// 
			this.openEnumItem.Name = "openEnumItem";
			this.openEnumItem.Size = new System.Drawing.Size(180, 22);
			this.openEnumItem.Text = "Відкрити";
			this.openEnumItem.Click += new System.EventHandler(this.openEnumItem_Click);
			// 
			// addEnumToolStripMenuItem
			// 
			this.addEnumToolStripMenuItem.Name = "addEnumToolStripMenuItem";
			this.addEnumToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.addEnumToolStripMenuItem.Text = "Додати нове перелічення";
			this.addEnumToolStripMenuItem.Click += new System.EventHandler(this.addEnumToolStripMenuItem_Click);
			// 
			// FormConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 509);
			this.Controls.Add(this.splitContainerBase);
			this.Controls.Add(this.menuStripTop);
			this.MainMenuStrip = this.menuStripTop;
			this.Name = "FormConfiguration";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfiguration_FormClosing);
			this.Load += new System.EventHandler(this.FormConfiguration_Load);
			this.splitContainerBase.Panel1.ResumeLayout(false);
			this.splitContainerBase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
			this.splitContainerBase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).EndInit();
			this.menuStripTop.ResumeLayout(false);
			this.menuStripTop.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerBase;
		private System.Windows.Forms.TreeView treeConfiguration;
		private System.Windows.Forms.DataGridView dataConfiguration;
		private System.Windows.Forms.ImageList imgTreeList;
		private System.Windows.Forms.MenuStrip menuStripTop;
		private System.Windows.Forms.ToolStripMenuItem DirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addDirectoryToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addNewDirectiryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveConfigurationToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem openEnumItem;
		private System.Windows.Forms.ToolStripMenuItem addEnumToolStripMenuItem;
	}
}

