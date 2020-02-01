namespace Trade
{
	partial class Form1
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.довідникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.товариToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.додатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.довідникиToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// довідникиToolStripMenuItem
			// 
			this.довідникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.товариToolStripMenuItem});
			this.довідникиToolStripMenuItem.Name = "довідникиToolStripMenuItem";
			this.довідникиToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.довідникиToolStripMenuItem.Text = "Довідники";
			// 
			// товариToolStripMenuItem
			// 
			this.товариToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиToolStripMenuItem});
			this.товариToolStripMenuItem.Name = "товариToolStripMenuItem";
			this.товариToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.товариToolStripMenuItem.Text = "Товари";
			this.товариToolStripMenuItem.Click += new System.EventHandler(this.товариToolStripMenuItem_Click);
			// 
			// додатиToolStripMenuItem
			// 
			this.додатиToolStripMenuItem.Name = "додатиToolStripMenuItem";
			this.додатиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.додатиToolStripMenuItem.Text = "Додати";
			this.додатиToolStripMenuItem.Click += new System.EventHandler(this.додатиToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.menuStrip1);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem довідникиToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem товариToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem додатиToolStripMenuItem;
	}
}

