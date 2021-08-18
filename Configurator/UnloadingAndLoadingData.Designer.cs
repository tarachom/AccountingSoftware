
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
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.buttonUnloadingData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 108);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(883, 464);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // buttonUnloadingData
            // 
            this.buttonUnloadingData.Location = new System.Drawing.Point(12, 50);
            this.buttonUnloadingData.Name = "buttonUnloadingData";
            this.buttonUnloadingData.Size = new System.Drawing.Size(160, 23);
            this.buttonUnloadingData.TabIndex = 1;
            this.buttonUnloadingData.Text = "Вигрузити дані у файл";
            this.buttonUnloadingData.UseVisualStyleBackColor = true;
            this.buttonUnloadingData.Click += new System.EventHandler(this.buttonUnloadingData_Click);
            // 
            // UnloadingAndLoadingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 584);
            this.Controls.Add(this.buttonUnloadingData);
            this.Controls.Add(this.richTextBoxLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UnloadingAndLoadingData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вигрузка та загрузка даних";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Button buttonUnloadingData;
    }
}