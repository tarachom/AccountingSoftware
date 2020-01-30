namespace Configurator
{
	partial class FormWorkProgress
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
			this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBoxLog
			// 
			this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxLog.Font = new System.Drawing.Font("Lucida Console", 9F);
			this.richTextBoxLog.Location = new System.Drawing.Point(5, 5);
			this.richTextBoxLog.Name = "richTextBoxLog";
			this.richTextBoxLog.Size = new System.Drawing.Size(775, 306);
			this.richTextBoxLog.TabIndex = 0;
			this.richTextBoxLog.Text = "";
			// 
			// FormWorkProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(785, 316);
			this.Controls.Add(this.richTextBoxLog);
			this.Name = "FormWorkProgress";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "FormWorkProgress";
			this.Load += new System.EventHandler(this.FormWorkProgress_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxLog;
	}
}