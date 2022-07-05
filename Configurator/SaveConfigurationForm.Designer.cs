namespace Configurator
{
	partial class SaveConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveConfigurationForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAnalize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBoxReplacement = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.buttonAnalize);
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 509);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 38);
            this.panel2.TabIndex = 13;
            // 
            // buttonAnalize
            // 
            this.buttonAnalize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonAnalize.Enabled = false;
            this.buttonAnalize.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonAnalize.Location = new System.Drawing.Point(3, 3);
            this.buttonAnalize.Name = "buttonAnalize";
            this.buttonAnalize.Size = new System.Drawing.Size(109, 32);
            this.buttonAnalize.TabIndex = 4;
            this.buttonAnalize.Text = "Крок 1. Аналіз";
            this.buttonAnalize.UseVisualStyleBackColor = true;
            this.buttonAnalize.Click += new System.EventHandler(this.buttonAnalize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClose.Location = new System.Drawing.Point(709, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(109, 32);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Закрити";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonSave.Enabled = false;
            this.buttonSave.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonSave.Location = new System.Drawing.Point(118, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 32);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Крок 2. Виконати";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.richTextBoxInfo);
            this.panel1.Location = new System.Drawing.Point(10, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 462);
            this.panel1.TabIndex = 14;
            // 
            // richTextBoxInfo
            // 
            this.richTextBoxInfo.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxInfo.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            this.richTextBoxInfo.ReadOnly = true;
            this.richTextBoxInfo.Size = new System.Drawing.Size(824, 462);
            this.richTextBoxInfo.TabIndex = 0;
            this.richTextBoxInfo.Text = "";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.checkBoxReplacement);
            this.panel3.Location = new System.Drawing.Point(10, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 34);
            this.panel3.TabIndex = 15;
            // 
            // checkBoxReplacement
            // 
            this.checkBoxReplacement.AutoSize = true;
            this.checkBoxReplacement.Checked = true;
            this.checkBoxReplacement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReplacement.Location = new System.Drawing.Point(3, 9);
            this.checkBoxReplacement.Name = "checkBoxReplacement";
            this.checkBoxReplacement.Size = new System.Drawing.Size(308, 17);
            this.checkBoxReplacement.TabIndex = 0;
            this.checkBoxReplacement.Text = "Заміщати колонки якщо неможлива реструктуризація. ";
            this.checkBoxReplacement.UseVisualStyleBackColor = true;
            // 
            // SaveConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 557);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveConfigurationForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Збереження конфігурації";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveConfigurationForm_FormClosing);
            this.Load += new System.EventHandler(this.SaveConfigurationForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox richTextBoxInfo;
		private System.Windows.Forms.Button buttonAnalize;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.CheckBox checkBoxReplacement;
	}
}