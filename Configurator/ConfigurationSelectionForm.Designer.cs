namespace Configurator
{
	partial class ConfigurationSelectionForm
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
            this.listBoxConfiguration = new System.Windows.Forms.ListBox();
            this.buttonAddConf = new System.Windows.Forms.Button();
            this.buttonOpenConf = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxConfiguration
            // 
            this.listBoxConfiguration.FormattingEnabled = true;
            this.listBoxConfiguration.Location = new System.Drawing.Point(12, 12);
            this.listBoxConfiguration.Name = "listBoxConfiguration";
            this.listBoxConfiguration.Size = new System.Drawing.Size(502, 277);
            this.listBoxConfiguration.TabIndex = 0;
            this.listBoxConfiguration.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxConfiguration_MouseDoubleClick);
            // 
            // buttonAddConf
            // 
            this.buttonAddConf.Location = new System.Drawing.Point(520, 135);
            this.buttonAddConf.Name = "buttonAddConf";
            this.buttonAddConf.Size = new System.Drawing.Size(97, 30);
            this.buttonAddConf.TabIndex = 1;
            this.buttonAddConf.Text = "Додати";
            this.buttonAddConf.UseVisualStyleBackColor = true;
            this.buttonAddConf.Click += new System.EventHandler(this.buttonAddConf_Click);
            // 
            // buttonOpenConf
            // 
            this.buttonOpenConf.Location = new System.Drawing.Point(520, 12);
            this.buttonOpenConf.Name = "buttonOpenConf";
            this.buttonOpenConf.Size = new System.Drawing.Size(97, 30);
            this.buttonOpenConf.TabIndex = 3;
            this.buttonOpenConf.Text = "Відкрити";
            this.buttonOpenConf.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(520, 171);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(97, 30);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Видалити";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // ConfigurationSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 300);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonOpenConf);
            this.Controls.Add(this.buttonAddConf);
            this.Controls.Add(this.listBoxConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вибір конфігурації";
            this.Load += new System.EventHandler(this.ConfigurationSelectionForm_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxConfiguration;
		private System.Windows.Forms.Button buttonAddConf;
		private System.Windows.Forms.Button buttonOpenConf;
        private System.Windows.Forms.Button buttonDelete;
    }
}