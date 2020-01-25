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
			this.splitContainerBase = new System.Windows.Forms.SplitContainer();
			this.dataConfiguration = new System.Windows.Forms.DataGridView();
			this.treeConfiguration = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
			this.splitContainerBase.Panel1.SuspendLayout();
			this.splitContainerBase.Panel2.SuspendLayout();
			this.splitContainerBase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainerBase
			// 
			this.splitContainerBase.Location = new System.Drawing.Point(10, 10);
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
			this.splitContainerBase.Size = new System.Drawing.Size(843, 521);
			this.splitContainerBase.SplitterDistance = 234;
			this.splitContainerBase.TabIndex = 0;
			// 
			// dataConfiguration
			// 
			this.dataConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataConfiguration.Location = new System.Drawing.Point(0, 0);
			this.dataConfiguration.Name = "dataConfiguration";
			this.dataConfiguration.Size = new System.Drawing.Size(605, 521);
			this.dataConfiguration.TabIndex = 0;
			// 
			// treeConfiguration
			// 
			this.treeConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeConfiguration.Location = new System.Drawing.Point(0, 0);
			this.treeConfiguration.Name = "treeConfiguration";
			this.treeConfiguration.Size = new System.Drawing.Size(234, 521);
			this.treeConfiguration.TabIndex = 0;
			// 
			// FormConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 538);
			this.Controls.Add(this.splitContainerBase);
			this.Name = "FormConfiguration";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormConfiguration_Load);
			this.splitContainerBase.Panel1.ResumeLayout(false);
			this.splitContainerBase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
			this.splitContainerBase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerBase;
		private System.Windows.Forms.TreeView treeConfiguration;
		private System.Windows.Forms.DataGridView dataConfiguration;
	}
}

