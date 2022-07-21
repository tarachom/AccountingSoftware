
namespace Configurator
{
    partial class UnloadingAndLoadingData
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
            this.buttonUnloadingData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.buttonLoadingData = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUnloadingData
            // 
            this.buttonUnloadingData.Location = new System.Drawing.Point(12, 12);
            this.buttonUnloadingData.Name = "buttonUnloadingData";
            this.buttonUnloadingData.Size = new System.Drawing.Size(80, 30);
            this.buttonUnloadingData.TabIndex = 1;
            this.buttonUnloadingData.Text = "Вигрузити";
            this.buttonUnloadingData.UseVisualStyleBackColor = true;
            this.buttonUnloadingData.Click += new System.EventHandler(this.buttonUnloadingData_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.richTextBoxInfo);
            this.panel1.Location = new System.Drawing.Point(12, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 519);
            this.panel1.TabIndex = 2;
            // 
            // richTextBoxInfo
            // 
            this.richTextBoxInfo.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxInfo.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            this.richTextBoxInfo.ReadOnly = true;
            this.richTextBoxInfo.Size = new System.Drawing.Size(883, 519);
            this.richTextBoxInfo.TabIndex = 1;
            this.richTextBoxInfo.Text = "";
            // 
            // buttonLoadingData
            // 
            this.buttonLoadingData.Location = new System.Drawing.Point(98, 12);
            this.buttonLoadingData.Name = "buttonLoadingData";
            this.buttonLoadingData.Size = new System.Drawing.Size(80, 30);
            this.buttonLoadingData.TabIndex = 3;
            this.buttonLoadingData.Text = "Загрузити";
            this.buttonLoadingData.UseVisualStyleBackColor = true;
            this.buttonLoadingData.Click += new System.EventHandler(this.buttonLoadingData_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(250, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(69, 30);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Зупинити";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // UnloadingAndLoadingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 584);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonLoadingData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonUnloadingData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UnloadingAndLoadingData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вигрузка та загрузка даних";
            this.Load += new System.EventHandler(this.UnloadingAndLoadingData_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonUnloadingData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        private System.Windows.Forms.Button buttonLoadingData;
        private System.Windows.Forms.Button buttonStop;
    }
}