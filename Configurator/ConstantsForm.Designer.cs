namespace Configurator
{
	partial class ConstantsForm
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
			this.textBoxDesc = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBoxEnums = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBoxPointer = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxFieldType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxBlock = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxDesc
			// 
			this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDesc.Location = new System.Drawing.Point(123, 71);
			this.textBoxDesc.Multiline = true;
			this.textBoxDesc.Name = "textBoxDesc";
			this.textBoxDesc.Size = new System.Drawing.Size(461, 63);
			this.textBoxDesc.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 74);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Опис:";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(123, 18);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(461, 20);
			this.textBoxName.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Назва:";
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonClose.Location = new System.Drawing.Point(473, 3);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(109, 32);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Закрити";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.ForeColor = System.Drawing.Color.ForestGreen;
			this.buttonSave.Location = new System.Drawing.Point(358, 3);
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
			this.panel2.Location = new System.Drawing.Point(10, 246);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(587, 38);
			this.panel2.TabIndex = 12;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.comboBoxBlock);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.comboBoxEnums);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.comboBoxPointer);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.comboBoxFieldType);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBoxName);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBoxDesc);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(10, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(587, 236);
			this.panel1.TabIndex = 16;
			// 
			// comboBoxEnums
			// 
			this.comboBoxEnums.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEnums.FormattingEnabled = true;
			this.comboBoxEnums.Location = new System.Drawing.Point(123, 195);
			this.comboBoxEnums.Name = "comboBoxEnums";
			this.comboBoxEnums.Size = new System.Drawing.Size(461, 21);
			this.comboBoxEnums.TabIndex = 25;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 198);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 24;
			this.label6.Text = "Перелічення:";
			// 
			// comboBoxPointer
			// 
			this.comboBoxPointer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPointer.FormattingEnabled = true;
			this.comboBoxPointer.Location = new System.Drawing.Point(123, 168);
			this.comboBoxPointer.Name = "comboBoxPointer";
			this.comboBoxPointer.Size = new System.Drawing.Size(461, 21);
			this.comboBoxPointer.TabIndex = 23;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 171);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Вказівник:";
			// 
			// comboBoxFieldType
			// 
			this.comboBoxFieldType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFieldType.FormattingEnabled = true;
			this.comboBoxFieldType.Location = new System.Drawing.Point(123, 140);
			this.comboBoxFieldType.Name = "comboBoxFieldType";
			this.comboBoxFieldType.Size = new System.Drawing.Size(461, 21);
			this.comboBoxFieldType.TabIndex = 21;
			this.comboBoxFieldType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFieldType_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 143);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Тип:";
			// 
			// comboBoxBlock
			// 
			this.comboBoxBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxBlock.FormattingEnabled = true;
			this.comboBoxBlock.Location = new System.Drawing.Point(123, 44);
			this.comboBoxBlock.Name = "comboBoxBlock";
			this.comboBoxBlock.Size = new System.Drawing.Size(461, 21);
			this.comboBoxBlock.TabIndex = 27;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 26;
			this.label4.Text = "Блок:";
			// 
			// ConstantsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(607, 294);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.KeyPreview = true;
			this.Name = "ConstantsForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Константа";
			this.Load += new System.EventHandler(this.FieldForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnumFieldForm_KeyDown);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDesc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBoxEnums;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox comboBoxPointer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxFieldType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxBlock;
		private System.Windows.Forms.Label label4;
	}
}