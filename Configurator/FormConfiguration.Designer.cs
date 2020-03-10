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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.listView1 = new System.Windows.Forms.ListView();
			this.menuStripTop = new System.Windows.Forms.MenuStrip();
			this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addContantsBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addConstatntsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewRegistersInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewDirectiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openEnumItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripDocument = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openDocumentItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripConstantBlock = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.OpenConstantBlock = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripConstatnt = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openConstatnt = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripRegistersInformation = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openItemRegistersInformation = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripRegistersAccumulation = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openItemRegistersAccumulation = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
			this.splitContainerBase.Panel1.SuspendLayout();
			this.splitContainerBase.Panel2.SuspendLayout();
			this.splitContainerBase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.menuStripTop.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.contextMenuStripDocument.SuspendLayout();
			this.contextMenuStripConstantBlock.SuspendLayout();
			this.contextMenuStripConstatnt.SuspendLayout();
			this.contextMenuStripRegistersInformation.SuspendLayout();
			this.contextMenuStripRegistersAccumulation.SuspendLayout();
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
			this.splitContainerBase.Panel2.Controls.Add(this.dataGridView1);
			this.splitContainerBase.Panel2.Controls.Add(this.listView1);
			this.splitContainerBase.Size = new System.Drawing.Size(842, 465);
			this.splitContainerBase.SplitterDistance = 265;
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
			this.treeConfiguration.Size = new System.Drawing.Size(265, 465);
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
			this.imgTreeList.Images.SetKeyName(4, "tablepartt");
			this.imgTreeList.Images.SetKeyName(5, "view");
			this.imgTreeList.Images.SetKeyName(6, "c");
			this.imgTreeList.Images.SetKeyName(7, "text_list_bullets.png");
			this.imgTreeList.Images.SetKeyName(8, "grid.png");
			this.imgTreeList.Images.SetKeyName(9, "chart_organisation.png");
			this.imgTreeList.Images.SetKeyName(10, "blog.png");
			this.imgTreeList.Images.SetKeyName(11, "blueprint_horizontal.png");
			this.imgTreeList.Images.SetKeyName(12, "css.png");
			this.imgTreeList.Images.SetKeyName(13, "field.ico");
			this.imgTreeList.Images.SetKeyName(14, "2.ico");
			this.imgTreeList.Images.SetKeyName(15, "21.ico");
			this.imgTreeList.Images.SetKeyName(16, "application_view_xp.png");
			this.imgTreeList.Images.SetKeyName(17, "3.ico");
			this.imgTreeList.Images.SetKeyName(18, "4.ico");
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
			this.dataGridView1.Location = new System.Drawing.Point(3, 231);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(534, 231);
			this.dataGridView1.TabIndex = 1;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Text = "sdfsdf";
			// 
			// listView1
			// 
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(3, 4);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(534, 221);
			this.listView1.SmallImageList = this.imgTreeList;
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// menuStripTop
			// 
			this.menuStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem});
			this.menuStripTop.Location = new System.Drawing.Point(10, 10);
			this.menuStripTop.Name = "menuStripTop";
			this.menuStripTop.Size = new System.Drawing.Size(842, 24);
			this.menuStripTop.TabIndex = 1;
			this.menuStripTop.Text = "menuStrip1";
			// 
			// ConfigToolStripMenuItem
			// 
			this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDirectoryToolStripMenuItem,
            this.addEnumToolStripMenuItem,
            this.addNewDocumentToolStripMenuItem,
            this.addContantsBlockToolStripMenuItem,
            this.addConstatntsToolStripMenuItem,
            this.addNewRegistersInformationToolStripMenuItem,
            this.addNewToolStripMenuItem,
            this.saveConfigurationToolStripMenuItem});
			this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
			this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
			this.ConfigToolStripMenuItem.Text = "Конфігурація";
			// 
			// addDirectoryToolStripMenuItem
			// 
			this.addDirectoryToolStripMenuItem.Name = "addDirectoryToolStripMenuItem";
			this.addDirectoryToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addDirectoryToolStripMenuItem.Text = "Додати новий довідник";
			this.addDirectoryToolStripMenuItem.Click += new System.EventHandler(this.addDirectoryToolStripMenuItem_Click);
			// 
			// addEnumToolStripMenuItem
			// 
			this.addEnumToolStripMenuItem.Name = "addEnumToolStripMenuItem";
			this.addEnumToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addEnumToolStripMenuItem.Text = "Додати нове перелічення";
			this.addEnumToolStripMenuItem.Click += new System.EventHandler(this.addEnumToolStripMenuItem_Click);
			// 
			// addNewDocumentToolStripMenuItem
			// 
			this.addNewDocumentToolStripMenuItem.Name = "addNewDocumentToolStripMenuItem";
			this.addNewDocumentToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addNewDocumentToolStripMenuItem.Text = "Додати новий документ";
			this.addNewDocumentToolStripMenuItem.Click += new System.EventHandler(this.addNewDocumentToolStripMenuItem_Click);
			// 
			// addContantsBlockToolStripMenuItem
			// 
			this.addContantsBlockToolStripMenuItem.Name = "addContantsBlockToolStripMenuItem";
			this.addContantsBlockToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addContantsBlockToolStripMenuItem.Text = "Додати новий блок констант";
			this.addContantsBlockToolStripMenuItem.Click += new System.EventHandler(this.addContantsBlockToolStripMenuItem_Click);
			// 
			// addConstatntsToolStripMenuItem
			// 
			this.addConstatntsToolStripMenuItem.Name = "addConstatntsToolStripMenuItem";
			this.addConstatntsToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addConstatntsToolStripMenuItem.Text = "Додати нову константу";
			this.addConstatntsToolStripMenuItem.Click += new System.EventHandler(this.addConstatntsToolStripMenuItem_Click);
			// 
			// addNewRegistersInformationToolStripMenuItem
			// 
			this.addNewRegistersInformationToolStripMenuItem.Name = "addNewRegistersInformationToolStripMenuItem";
			this.addNewRegistersInformationToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addNewRegistersInformationToolStripMenuItem.Text = "Додати новий регістер відомостей";
			this.addNewRegistersInformationToolStripMenuItem.Click += new System.EventHandler(this.addNewRegistersInformationToolStripMenuItem_Click);
			// 
			// addNewToolStripMenuItem
			// 
			this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
			this.addNewToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
			this.addNewToolStripMenuItem.Text = "Додати новий регістер накопичення";
			this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
			// 
			// saveConfigurationToolStripMenuItem
			// 
			this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
			this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
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
			this.openEnumItem.Size = new System.Drawing.Size(122, 22);
			this.openEnumItem.Text = "Відкрити";
			this.openEnumItem.Click += new System.EventHandler(this.openEnumItem_Click);
			// 
			// contextMenuStripDocument
			// 
			this.contextMenuStripDocument.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDocumentItem});
			this.contextMenuStripDocument.Name = "contextMenuStripDocument";
			this.contextMenuStripDocument.Size = new System.Drawing.Size(123, 26);
			// 
			// openDocumentItem
			// 
			this.openDocumentItem.Name = "openDocumentItem";
			this.openDocumentItem.Size = new System.Drawing.Size(122, 22);
			this.openDocumentItem.Text = "Відкрити";
			this.openDocumentItem.Click += new System.EventHandler(this.openDocumentItem_Click);
			// 
			// contextMenuStripConstantBlock
			// 
			this.contextMenuStripConstantBlock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenConstantBlock});
			this.contextMenuStripConstantBlock.Name = "contextMenuConstantBlock";
			this.contextMenuStripConstantBlock.Size = new System.Drawing.Size(123, 26);
			// 
			// OpenConstantBlock
			// 
			this.OpenConstantBlock.Name = "OpenConstantBlock";
			this.OpenConstantBlock.Size = new System.Drawing.Size(122, 22);
			this.OpenConstantBlock.Text = "Відкрити";
			this.OpenConstantBlock.Click += new System.EventHandler(this.OpenConstantBlock_Click);
			// 
			// contextMenuStripConstatnt
			// 
			this.contextMenuStripConstatnt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConstatnt});
			this.contextMenuStripConstatnt.Name = "contextMenuConstantBlock";
			this.contextMenuStripConstatnt.Size = new System.Drawing.Size(123, 26);
			// 
			// openConstatnt
			// 
			this.openConstatnt.Name = "openConstatnt";
			this.openConstatnt.Size = new System.Drawing.Size(122, 22);
			this.openConstatnt.Text = "Відкрити";
			this.openConstatnt.Click += new System.EventHandler(this.openConstatnt_Click);
			// 
			// contextMenuStripRegistersInformation
			// 
			this.contextMenuStripRegistersInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItemRegistersInformation});
			this.contextMenuStripRegistersInformation.Name = "contextMenuStripRegistersInformation";
			this.contextMenuStripRegistersInformation.Size = new System.Drawing.Size(123, 26);
			// 
			// openItemRegistersInformation
			// 
			this.openItemRegistersInformation.Name = "openItemRegistersInformation";
			this.openItemRegistersInformation.Size = new System.Drawing.Size(122, 22);
			this.openItemRegistersInformation.Text = "Відкрити";
			this.openItemRegistersInformation.Click += new System.EventHandler(this.openItemRegistersInformation_Click);
			// 
			// contextMenuStripRegistersAccumulation
			// 
			this.contextMenuStripRegistersAccumulation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItemRegistersAccumulation});
			this.contextMenuStripRegistersAccumulation.Name = "contextMenuStripRegistersInformation";
			this.contextMenuStripRegistersAccumulation.Size = new System.Drawing.Size(181, 48);
			// 
			// openItemRegistersAccumulation
			// 
			this.openItemRegistersAccumulation.Name = "openItemRegistersAccumulation";
			this.openItemRegistersAccumulation.Size = new System.Drawing.Size(180, 22);
			this.openItemRegistersAccumulation.Text = "Відкрити";
			this.openItemRegistersAccumulation.Click += new System.EventHandler(this.openItemRegistersAccumulation_Click);
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
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConfiguration_FormClosing);
			this.Load += new System.EventHandler(this.FormConfiguration_Load);
			this.splitContainerBase.Panel1.ResumeLayout(false);
			this.splitContainerBase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
			this.splitContainerBase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.menuStripTop.ResumeLayout(false);
			this.menuStripTop.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.contextMenuStripDocument.ResumeLayout(false);
			this.contextMenuStripConstantBlock.ResumeLayout(false);
			this.contextMenuStripConstatnt.ResumeLayout(false);
			this.contextMenuStripRegistersInformation.ResumeLayout(false);
			this.contextMenuStripRegistersAccumulation.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerBase;
		private System.Windows.Forms.TreeView treeConfiguration;
		private System.Windows.Forms.ImageList imgTreeList;
		private System.Windows.Forms.MenuStrip menuStripTop;
		private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
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
		private System.Windows.Forms.ToolStripMenuItem addNewDocumentToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDocument;
		private System.Windows.Forms.ToolStripMenuItem openDocumentItem;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewButtonColumn Column2;
		private System.Windows.Forms.ToolStripMenuItem addContantsBlockToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addConstatntsToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripConstantBlock;
		private System.Windows.Forms.ToolStripMenuItem OpenConstantBlock;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripConstatnt;
		private System.Windows.Forms.ToolStripMenuItem openConstatnt;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripRegistersInformation;
		private System.Windows.Forms.ToolStripMenuItem openItemRegistersInformation;
		private System.Windows.Forms.ToolStripMenuItem addNewRegistersInformationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripRegistersAccumulation;
		private System.Windows.Forms.ToolStripMenuItem openItemRegistersAccumulation;
	}
}

