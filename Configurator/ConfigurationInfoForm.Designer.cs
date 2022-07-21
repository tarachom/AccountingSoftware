
namespace Configurator
{
    partial class ConfigurationInfoForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Назва = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_ПростірІмен = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Автор = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Назва конфігурації:";
            // 
            // textBox_Назва
            // 
            this.textBox_Назва.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Назва.Location = new System.Drawing.Point(120, 20);
            this.textBox_Назва.Name = "textBox_Назва";
            this.textBox_Назва.Size = new System.Drawing.Size(539, 20);
            this.textBox_Назва.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(671, 38);
            this.panel2.TabIndex = 4;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClose.Location = new System.Drawing.Point(559, 3);
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
            this.buttonSave.Location = new System.Drawing.Point(444, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(109, 32);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Зберегти";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_Автор);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_ПростірІмен);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_Назва);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 177);
            this.panel1.TabIndex = 4;
            // 
            // textBox_ПростірІмен
            // 
            this.textBox_ПростірІмен.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ПростірІмен.Location = new System.Drawing.Point(120, 46);
            this.textBox_ПростірІмен.Name = "textBox_ПростірІмен";
            this.textBox_ПростірІмен.Size = new System.Drawing.Size(539, 20);
            this.textBox_ПростірІмен.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Простір імен:";
            // 
            // textBox_Автор
            // 
            this.textBox_Автор.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Автор.Location = new System.Drawing.Point(120, 72);
            this.textBox_Автор.Name = "textBox_Автор";
            this.textBox_Автор.Size = new System.Drawing.Size(539, 20);
            this.textBox_Автор.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Автор:";
            // 
            // ConfigurationInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 215);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ConfigurationInfoForm";
            this.Text = "Інформація про конфігурацію";
            this.Load += new System.EventHandler(this.ConfigurationInfoForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Назва;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_ПростірІмен;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Автор;
        private System.Windows.Forms.Label label3;
    }
}