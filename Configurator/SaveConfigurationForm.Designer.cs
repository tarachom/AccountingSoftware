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
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.Controls.Add(this.buttonClose);
			this.panel2.Controls.Add(this.buttonSave);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(10, 402);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(780, 38);
			this.panel2.TabIndex = 13;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonClose.Location = new System.Drawing.Point(668, 3);
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
			this.buttonSave.Location = new System.Drawing.Point(553, 3);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(109, 32);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Зберегти";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.richTextBoxInfo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(10, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(780, 392);
			this.panel1.TabIndex = 14;
			// 
			// richTextBoxInfo
			// 
			this.richTextBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.richTextBoxInfo.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxInfo.Name = "richTextBoxInfo";
			this.richTextBoxInfo.Size = new System.Drawing.Size(780, 392);
			this.richTextBoxInfo.TabIndex = 0;
			this.richTextBoxInfo.Text = "";
			// 
			// SaveConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "SaveConfigurationForm";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SaveConfigurationForm";
			this.Load += new System.EventHandler(this.SaveConfigurationForm_Load);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox richTextBoxInfo;
	}
}