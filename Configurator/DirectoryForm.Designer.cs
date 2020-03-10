namespace Configurator
{
	partial class DirectoryForm
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
			this.listBoxFields = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.listBoxViews = new System.Windows.Forms.ListBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.buttonAddView = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.panel7 = new System.Windows.Forms.Panel();
			this.listBoxTabularParts = new System.Windows.Forms.ListBox();
			this.panel6 = new System.Windows.Forms.Panel();
			this.buttonAddTablePart = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.buttonAddField = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.textBoxDesc = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxTable = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxFields
			// 
			this.listBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFields.FormattingEnabled = true;
			this.listBoxFields.Location = new System.Drawing.Point(0, 0);
			this.listBoxFields.Name = "listBoxFields";
			this.listBoxFields.Size = new System.Drawing.Size(266, 236);
			this.listBoxFields.TabIndex = 0;
			this.listBoxFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxFields_KeyDown);
			this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel8);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel7);
			this.panel1.Controls.Add(this.panel6);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(266, 533);
			this.panel1.TabIndex = 1;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.listBoxViews);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel8.Location = new System.Drawing.Point(0, 426);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(266, 107);
			this.panel8.TabIndex = 10;
			// 
			// listBoxViews
			// 
			this.listBoxViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxViews.FormattingEnabled = true;
			this.listBoxViews.Location = new System.Drawing.Point(0, 0);
			this.listBoxViews.Name = "listBoxViews";
			this.listBoxViews.Size = new System.Drawing.Size(266, 107);
			this.listBoxViews.TabIndex = 0;
			this.listBoxViews.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxViews_MouseDoubleClick);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.buttonAddView);
			this.panel3.Controls.Add(this.label6);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 396);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(266, 30);
			this.panel3.TabIndex = 9;
			// 
			// buttonAddView
			// 
			this.buttonAddView.Location = new System.Drawing.Point(105, 3);
			this.buttonAddView.Name = "buttonAddView";
			this.buttonAddView.Size = new System.Drawing.Size(55, 23);
			this.buttonAddView.TabIndex = 8;
			this.buttonAddView.Text = "Додати";
			this.buttonAddView.UseVisualStyleBackColor = true;
			this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Візуалізації";
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.listBoxTabularParts);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(0, 296);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(266, 100);
			this.panel7.TabIndex = 1;
			// 
			// listBoxTabularParts
			// 
			this.listBoxTabularParts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxTabularParts.FormattingEnabled = true;
			this.listBoxTabularParts.Location = new System.Drawing.Point(0, 0);
			this.listBoxTabularParts.Name = "listBoxTabularParts";
			this.listBoxTabularParts.Size = new System.Drawing.Size(266, 100);
			this.listBoxTabularParts.TabIndex = 1;
			this.listBoxTabularParts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxTabularParts_KeyDown);
			this.listBoxTabularParts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxTabularParts_MouseDoubleClick);
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.buttonAddTablePart);
			this.panel6.Controls.Add(this.label2);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(0, 266);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(266, 30);
			this.panel6.TabIndex = 4;
			// 
			// buttonAddTablePart
			// 
			this.buttonAddTablePart.Location = new System.Drawing.Point(105, 3);
			this.buttonAddTablePart.Name = "buttonAddTablePart";
			this.buttonAddTablePart.Size = new System.Drawing.Size(55, 23);
			this.buttonAddTablePart.TabIndex = 8;
			this.buttonAddTablePart.Text = "Додати";
			this.buttonAddTablePart.UseVisualStyleBackColor = true;
			this.buttonAddTablePart.Click += new System.EventHandler(this.buttonAddTablePart_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Табличні частини";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.listBoxFields);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 30);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(266, 236);
			this.panel5.TabIndex = 2;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.buttonAddField);
			this.panel4.Controls.Add(this.label1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(266, 30);
			this.panel4.TabIndex = 3;
			// 
			// buttonAddField
			// 
			this.buttonAddField.Location = new System.Drawing.Point(44, 4);
			this.buttonAddField.Name = "buttonAddField";
			this.buttonAddField.Size = new System.Drawing.Size(55, 23);
			this.buttonAddField.TabIndex = 7;
			this.buttonAddField.Text = "Додати";
			this.buttonAddField.UseVisualStyleBackColor = true;
			this.buttonAddField.Click += new System.EventHandler(this.buttonAddField_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Поля";
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.ForeColor = System.Drawing.Color.ForestGreen;
			this.buttonSave.Location = new System.Drawing.Point(590, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(109, 32);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Зберегти";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.Controls.Add(this.buttonClose);
			this.panel2.Controls.Add(this.buttonSave);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(10, 543);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(817, 38);
			this.panel2.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonClose.Location = new System.Drawing.Point(705, 3);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(109, 32);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Закрити";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(10, 10);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.textBoxDesc);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxTable);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxName);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Size = new System.Drawing.Size(817, 533);
			this.splitContainer1.SplitterDistance = 266;
			this.splitContainer1.TabIndex = 4;
			// 
			// textBoxDesc
			// 
			this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDesc.Location = new System.Drawing.Point(85, 83);
			this.textBoxDesc.Multiline = true;
			this.textBoxDesc.Name = "textBoxDesc";
			this.textBoxDesc.Size = new System.Drawing.Size(459, 62);
			this.textBoxDesc.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 86);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Опис:";
			// 
			// textBoxTable
			// 
			this.textBoxTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTable.Location = new System.Drawing.Point(85, 57);
			this.textBoxTable.Name = "textBoxTable";
			this.textBoxTable.Size = new System.Drawing.Size(459, 20);
			this.textBoxTable.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Назва в базі:";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(85, 31);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(459, 20);
			this.textBoxName.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Назва:";
			// 
			// DirectoryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(837, 591);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.KeyPreview = true;
			this.Name = "DirectoryForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Довідник";
			this.Load += new System.EventHandler(this.DirectoryForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DirectoryForm_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel7.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxFields;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.ListBox listBoxTabularParts;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxDesc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxTable;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonAddField;
		private System.Windows.Forms.Button buttonAddTablePart;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button buttonAddView;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.ListBox listBoxViews;
	}
}