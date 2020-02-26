namespace Configurator
{
	partial class ViewForm
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
			this.panel5 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.listBoxAllFields = new System.Windows.Forms.ListBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.buttonAddAllField = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
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
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
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
			this.listBoxFields.Size = new System.Drawing.Size(250, 236);
			this.listBoxFields.TabIndex = 0;
			this.listBoxFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxFields_KeyDown);
			//this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(500, 302);
			this.panel1.TabIndex = 1;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.splitContainer2);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 30);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(500, 236);
			this.panel5.TabIndex = 2;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.listBoxFields);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.listBoxAllFields);
			this.splitContainer2.Size = new System.Drawing.Size(500, 236);
			this.splitContainer2.SplitterDistance = 250;
			this.splitContainer2.TabIndex = 0;
			// 
			// listBoxAllFields
			// 
			this.listBoxAllFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxAllFields.FormattingEnabled = true;
			this.listBoxAllFields.Location = new System.Drawing.Point(0, 0);
			this.listBoxAllFields.Name = "listBoxAllFields";
			this.listBoxAllFields.Size = new System.Drawing.Size(246, 236);
			this.listBoxAllFields.TabIndex = 1;
			this.listBoxAllFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxAllFields_KeyDown);
			this.listBoxAllFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAllFields_MouseDoubleClick);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.buttonAddAllField);
			this.panel4.Controls.Add(this.label6);
			this.panel4.Controls.Add(this.label1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(500, 30);
			this.panel4.TabIndex = 3;
			// 
			// buttonAddAllField
			// 
			this.buttonAddAllField.Location = new System.Drawing.Point(421, 4);
			this.buttonAddAllField.Name = "buttonAddAllField";
			this.buttonAddAllField.Size = new System.Drawing.Size(76, 23);
			this.buttonAddAllField.TabIndex = 8;
			this.buttonAddAllField.Text = "Додати всі";
			this.buttonAddAllField.UseVisualStyleBackColor = true;
			this.buttonAddAllField.Click += new System.EventHandler(this.buttonAddAllField_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(251, 9);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Доступні поля";
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
			this.buttonSave.Location = new System.Drawing.Point(706, 3);
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
			this.panel2.Location = new System.Drawing.Point(10, 312);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(933, 38);
			this.panel2.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonClose.Location = new System.Drawing.Point(821, 3);
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
			this.splitContainer1.Size = new System.Drawing.Size(933, 302);
			this.splitContainer1.SplitterDistance = 500;
			this.splitContainer1.TabIndex = 4;
			// 
			// textBoxDesc
			// 
			this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDesc.Location = new System.Drawing.Point(131, 83);
			this.textBoxDesc.Multiline = true;
			this.textBoxDesc.Name = "textBoxDesc";
			this.textBoxDesc.Size = new System.Drawing.Size(295, 62);
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
			this.textBoxTable.Location = new System.Drawing.Point(131, 57);
			this.textBoxTable.Name = "textBoxTable";
			this.textBoxTable.Size = new System.Drawing.Size(295, 20);
			this.textBoxTable.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Назва таблиці в базі:";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(131, 31);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(295, 20);
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
			// ViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(953, 360);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.KeyPreview = true;
			this.Name = "ViewForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Візуалізація";
			this.Load += new System.EventHandler(this.ViewForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewForm_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
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
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox listBoxAllFields;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonAddAllField;
	}
}