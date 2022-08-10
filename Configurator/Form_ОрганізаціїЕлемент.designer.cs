

namespace StorageAndTrade
{
    partial class Form_ОрганізаціїЕлемент
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label_Назва = new System.Windows.Forms.Label();
            this.textBox_Назва = new System.Windows.Forms.TextBox();
            this.label_Код = new System.Windows.Forms.Label();
            this.textBox_Код = new System.Windows.Forms.TextBox();
            this.label_НазваПовна = new System.Windows.Forms.Label();
            this.textBox_НазваПовна = new System.Windows.Forms.TextBox();
            this.label_НазваСкорочена = new System.Windows.Forms.Label();
            this.textBox_НазваСкорочена = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(182, 136);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(164, 27);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Закрити";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 136);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(164, 27);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Зберегти";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label_Назва
            // 
            this.label_Назва.AutoSize = true;
            this.label_Назва.Location = new System.Drawing.Point(12, 10);
            this.label_Назва.Name = "label_Назва";
            this.label_Назва.Size = new System.Drawing.Size(42, 13);
            this.label_Назва.TabIndex = 0;
            this.label_Назва.Text = "Назва:";
            // 
            // textBox_Назва
            // 
            this.textBox_Назва.Location = new System.Drawing.Point(121, 6);
            this.textBox_Назва.Name = "textBox_Назва";
            this.textBox_Назва.Size = new System.Drawing.Size(167, 20);
            this.textBox_Назва.TabIndex = 1;
            // 
            // label_Код
            // 
            this.label_Код.AutoSize = true;
            this.label_Код.Location = new System.Drawing.Point(12, 40);
            this.label_Код.Name = "label_Код";
            this.label_Код.Size = new System.Drawing.Size(29, 13);
            this.label_Код.TabIndex = 2;
            this.label_Код.Text = "Код:";
            // 
            // textBox_Код
            // 
            this.textBox_Код.Location = new System.Drawing.Point(121, 37);
            this.textBox_Код.Name = "textBox_Код";
            this.textBox_Код.Size = new System.Drawing.Size(167, 20);
            this.textBox_Код.TabIndex = 3;
            // 
            // label_НазваПовна
            // 
            this.label_НазваПовна.AutoSize = true;
            this.label_НазваПовна.Location = new System.Drawing.Point(13, 75);
            this.label_НазваПовна.Name = "label_НазваПовна";
            this.label_НазваПовна.Size = new System.Drawing.Size(74, 13);
            this.label_НазваПовна.TabIndex = 4;
            this.label_НазваПовна.Text = "НазваПовна:";
            // 
            // textBox_НазваПовна
            // 
            this.textBox_НазваПовна.Location = new System.Drawing.Point(121, 68);
            this.textBox_НазваПовна.Name = "textBox_НазваПовна";
            this.textBox_НазваПовна.Size = new System.Drawing.Size(167, 20);
            this.textBox_НазваПовна.TabIndex = 5;
            // 
            // label_НазваСкорочена
            // 
            this.label_НазваСкорочена.AutoSize = true;
            this.label_НазваСкорочена.Location = new System.Drawing.Point(13, 96);
            this.label_НазваСкорочена.Name = "label_НазваСкорочена";
            this.label_НазваСкорочена.Size = new System.Drawing.Size(96, 13);
            this.label_НазваСкорочена.TabIndex = 6;
            this.label_НазваСкорочена.Text = "НазваСкорочена:";
            // 
            // textBox_НазваСкорочена
            // 
            this.textBox_НазваСкорочена.Location = new System.Drawing.Point(121, 96);
            this.textBox_НазваСкорочена.Name = "textBox_НазваСкорочена";
            this.textBox_НазваСкорочена.Size = new System.Drawing.Size(167, 20);
            this.textBox_НазваСкорочена.TabIndex = 7;
            // 
            // Form_ОрганізаціїЕлемент
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 597);
            this.Controls.Add(this.label_Назва);
            this.Controls.Add(this.textBox_Назва);
            this.Controls.Add(this.label_Код);
            this.Controls.Add(this.textBox_Код);
            this.Controls.Add(this.label_НазваПовна);
            this.Controls.Add(this.textBox_НазваПовна);
            this.Controls.Add(this.label_НазваСкорочена);
            this.Controls.Add(this.textBox_НазваСкорочена);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Name = "Form_ОрганізаціїЕлемент";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Організації";
            this.Load += new System.EventHandler(this.Form_ОрганізаціїЕлемент_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
		
		private System.Windows.Forms.Label label_Назва;
        private System.Windows.Forms.TextBox textBox_Назва;
		
		private System.Windows.Forms.Label label_Код;
        private System.Windows.Forms.TextBox textBox_Код;
		
		private System.Windows.Forms.Label label_НазваПовна;
        private System.Windows.Forms.TextBox textBox_НазваПовна;
		
		private System.Windows.Forms.Label label_НазваСкорочена;
        private System.Windows.Forms.TextBox textBox_НазваСкорочена;
		
    }
}
	
	