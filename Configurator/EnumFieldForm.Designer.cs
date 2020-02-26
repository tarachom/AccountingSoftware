namespace Configurator
{
	partial class EnumFieldForm
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
			this.textBoxNameValue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxDesc
			// 
			this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDesc.Location = new System.Drawing.Point(123, 70);
			this.textBoxDesc.Multiline = true;
			this.textBoxDesc.Name = "textBoxDesc";
			this.textBoxDesc.Size = new System.Drawing.Size(399, 63);
			this.textBoxDesc.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 73);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Опис:";
			// 
			// textBoxNameValue
			// 
			this.textBoxNameValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNameValue.Location = new System.Drawing.Point(123, 44);
			this.textBoxNameValue.Name = "textBoxNameValue";
			this.textBoxNameValue.Size = new System.Drawing.Size(399, 20);
			this.textBoxNameValue.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Порядковий номер:";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(123, 18);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(399, 20);
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
			this.buttonClose.Location = new System.Drawing.Point(413, 3);
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
			this.buttonSave.Location = new System.Drawing.Point(298, 3);
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
			this.panel2.Location = new System.Drawing.Point(10, 164);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(540, 38);
			this.panel2.TabIndex = 12;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBoxName);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.textBoxNameValue);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBoxDesc);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(10, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(540, 154);
			this.panel1.TabIndex = 16;
			// 
			// EnumFieldForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(560, 212);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "EnumFieldForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Поле";
			this.Load += new System.EventHandler(this.FieldForm_Load);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDesc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxNameValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
	}
}