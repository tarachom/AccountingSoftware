namespace Trade
{
	partial class Form2
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
			this.dataConfiguration = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).BeginInit();
			this.SuspendLayout();
			// 
			// dataConfiguration
			// 
			this.dataConfiguration.AllowUserToAddRows = false;
			this.dataConfiguration.AllowUserToDeleteRows = false;
			this.dataConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataConfiguration.Location = new System.Drawing.Point(12, 12);
			this.dataConfiguration.Name = "dataConfiguration";
			this.dataConfiguration.ReadOnly = true;
			this.dataConfiguration.Size = new System.Drawing.Size(776, 426);
			this.dataConfiguration.TabIndex = 0;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dataConfiguration);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataConfiguration)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataConfiguration;
	}
}