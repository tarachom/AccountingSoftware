<?xml version="1.0" encoding="utf-8"?>
<!--
/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text" indent="yes"/>
	
    <xsl:param name="ConstructorType" />
	<xsl:param name="ConfObjectName" />
	<xsl:param name="Form" /> 
	
    <xsl:template match="/">
		
		<xsl:choose>
			<xsl:when test="$ConstructorType = 'Directory'">
				<xsl:choose>
					<xsl:when test="$Form = 'DirectoryFormListDesigner'">
						<xsl:call-template name="DirectoryFormListDesigner" />
					</xsl:when>
				    <xsl:when test="$Form = 'DirectoryFormList'">
						<xsl:call-template name="DirectoryFormList" />
					</xsl:when>
				    <xsl:when test="$Form = 'DirectoryFormElementDesigner'">
						<xsl:call-template name="DirectoryFormElementDesigner" />
					</xsl:when>
				    <xsl:when test="$Form = 'DirectoryFormElement'">
						<xsl:call-template name="DirectoryFormElement" />
					</xsl:when>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="ConstructorType = 'Document'">

			</xsl:when>
		</xsl:choose>
		
    </xsl:template>

	<xsl:template name="DirectoryFormListDesigner">
		<xsl:variable name="Directory" select="Configuration/Directories/Directory[Name = $ConfObjectName]" />
		
namespace <xsl:value-of select="Configuration/NameSpace"/>
{
    partial class Form_<xsl:value-of select="$Directory/Name"/>
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing &amp;&amp; (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_<xsl:value-of select="$Directory/Name"/>));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewRecords = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 29);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonEdit,
            this.toolStripButtonRefresh,
            this.toolStripButtonCopy,
            this.toolStripButtonDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(921, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(66, 22);
            this.toolStripButtonAdd.Text = "Додати";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(87, 22);
            this.toolStripButtonEdit.Text = "Редагувати";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonRefresh.Text = "Обновити";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(85, 22);
            this.toolStripButtonCopy.Text = "Копіювати";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonDelete.Text = "Видалити";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewRecords);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(921, 378);
            this.panel2.TabIndex = 2;
            // 
            // dataGridViewRecords
            // 
            this.dataGridViewRecords.AllowUserToAddRows = false;
            this.dataGridViewRecords.AllowUserToDeleteRows = false;
            this.dataGridViewRecords.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRecords.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRecords.Name = "dataGridViewRecords";
            this.dataGridViewRecords.ReadOnly = true;
            this.dataGridViewRecords.RowHeadersVisible = false;
            this.dataGridViewRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRecords.Size = new System.Drawing.Size(921, 378);
            this.dataGridViewRecords.TabIndex = 0;
            this.dataGridViewRecords.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRecords_CellDoubleClick);
            // 
            // Form_<xsl:value-of select="$Directory/Name"/>
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 407);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_<xsl:value-of select="$Directory/Name"/>";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<xsl:value-of select="$Directory/Name"/>";
            this.Load += new System.EventHandler(this.Form_<xsl:value-of select="$Directory/Name"/>_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
    }
}
		
	</xsl:template>

    <xsl:template name="DirectoryFormList">
	   <xsl:variable name="Directory" select="Configuration/Directories/Directory[Name = $ConfObjectName]" />

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AccountingSoftware;
using Конфа = <xsl:value-of select="Configuration/NameSpace"/>;
using Константи = <xsl:value-of select="Configuration/NameSpace"/>.Константи;
using Довідники = <xsl:value-of select="Configuration/NameSpace"/>.Довідники;

namespace StorageAndTrade
{
    public partial class Form_<xsl:value-of select="$Directory/Name"/> : Form
    {
        public Form_<xsl:value-of select="$Directory/Name"/>()
        {
            InitializeComponent();

			RecordsBindingList = new BindingList&lt;Записи&gt;();
			dataGridViewRecords.DataSource = RecordsBindingList;
			
			//dataGridViewRecords.Columns["Image"].Width = 30;
			//dataGridViewRecords.Columns["Image"].HeaderText = "";

			dataGridViewRecords.Columns["ID"].Visible = false;
			dataGridViewRecords.Columns["Назва"].Width = 300;
			dataGridViewRecords.Columns["Код"].Width = 50;
		}

		public DirectoryPointer DirectoryPointerItem { get; set; }

        private void Form_<xsl:value-of select="$Directory/Name"/>_Load(object sender, EventArgs e)
        {
			LoadRecords();
		}

		private BindingList&lt;Записи&gt; RecordsBindingList { get; set; }

		public void LoadRecords()
		{
			int selectRow = 0;

			RecordsBindingList.Clear();
			dataGridViewRecords.Rows.Clear();

			Довідники.<xsl:value-of select="$Directory/Name"/>_Select <xsl:value-of select="$Directory/Name"/>_Select = new Довідники.<xsl:value-of select="$Directory/Name"/>_Select();
			<xsl:value-of select="$Directory/Name"/>_Select.QuerySelect.Field.Add(Довідники.<xsl:value-of select="$Directory/Name"/>_Const.Назва);
			<xsl:value-of select="$Directory/Name"/>_Select.QuerySelect.Field.Add(Довідники.<xsl:value-of select="$Directory/Name"/>_Const.Код);

			//ORDER
			<xsl:value-of select="$Directory/Name"/>_Select.QuerySelect.Order.Add(Довідники.<xsl:value-of select="$Directory/Name"/>_Const.Назва, SelectOrder.ASC);

			<xsl:value-of select="$Directory/Name"/>_Select.Select();
			while (<xsl:value-of select="$Directory/Name"/>_Select.MoveNext())
			{
				Довідники.<xsl:value-of select="$Directory/Name"/>_Pointer cur = <xsl:value-of select="$Directory/Name"/>_Select.Current;

				RecordsBindingList.Add(new Записи
				{
					ID = cur.UnigueID.ToString(),
					Назва = cur.Fields[Довідники.<xsl:value-of select="$Directory/Name"/>_Const.Назва].ToString(),
					Код = cur.Fields[Довідники.<xsl:value-of select="$Directory/Name"/>_Const.Код].ToString()
				});

				if (DirectoryPointerItem != null)
					if (cur.UnigueID.ToString() == DirectoryPointerItem.UnigueID.ToString())
						selectRow = RecordsBindingList.Count - 1;
			}

			if (selectRow != 0 &amp;&amp; selectRow &lt; dataGridViewRecords.Rows.Count)
			{
                dataGridViewRecords.FirstDisplayedScrollingRowIndex = selectRow;
                dataGridViewRecords.Rows[selectRow].Selected = true;
                dataGridViewRecords.Rows[0].Selected = false;
            }
		}

		private class Записи
		{
			public Записи() { /* Image = Properties.Resources.doc_text_image; */ }
			public Bitmap Image { get; set; }
			public string ID { get; set; }
			public string Назва { get; set; }
			public string Код { get; set; }
		}

        private void dataGridViewRecords_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
			if (e.RowIndex >= 0 &amp;&amp; e.RowIndex &lt; dataGridViewRecords.RowCount)
			{
				string Uid = dataGridViewRecords.Rows[e.RowIndex].Cells["ID"].Value.ToString();

				if (DirectoryPointerItem != null)
				{
					DirectoryPointerItem = new Довідники.<xsl:value-of select="$Directory/Name"/>_Pointer(new UnigueID(Uid));
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					toolStripButtonEdit_Click(this, null);
				}
			}
		}

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
			Form_<xsl:value-of select="$Directory/Name"/>Елемент form_<xsl:value-of select="$Directory/Name"/>Елемент = new Form_<xsl:value-of select="$Directory/Name"/>Елемент();
			form_<xsl:value-of select="$Directory/Name"/>Елемент.MdiParent = this.MdiParent;
			form_<xsl:value-of select="$Directory/Name"/>Елемент.IsNew = true;
			form_<xsl:value-of select="$Directory/Name"/>Елемент.OwnerForm = this;
			if (DirectoryPointerItem != null &amp;&amp; this.MdiParent == null)
				form_<xsl:value-of select="$Directory/Name"/>Елемент.ShowDialog();
			else
				form_<xsl:value-of select="$Directory/Name"/>Елемент.Show();
		}

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
			if (dataGridViewRecords.SelectedRows.Count > 0)
			{
				int RowIndex = dataGridViewRecords.SelectedRows[0].Index;

				Form_<xsl:value-of select="$Directory/Name"/>Елемент form_<xsl:value-of select="$Directory/Name"/>Елемент = new Form_<xsl:value-of select="$Directory/Name"/>Елемент();
				form_<xsl:value-of select="$Directory/Name"/>Елемент.MdiParent = this.MdiParent;
				form_<xsl:value-of select="$Directory/Name"/>Елемент.IsNew = false;
				form_<xsl:value-of select="$Directory/Name"/>Елемент.OwnerForm = this;
				form_<xsl:value-of select="$Directory/Name"/>Елемент.Uid = dataGridViewRecords.Rows[RowIndex].Cells["ID"].Value.ToString();
				if (DirectoryPointerItem != null &amp;&amp; this.MdiParent == null)
					form_<xsl:value-of select="$Directory/Name"/>Елемент.ShowDialog();
				else
					form_<xsl:value-of select="$Directory/Name"/>Елемент.Show();
			}			
		}

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
			LoadRecords();
		}

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
			if (dataGridViewRecords.SelectedRows.Count != 0 &amp;&amp;
				MessageBox.Show("Копіювати записи?", "Повідомлення", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				for (int i = 0; i &lt; dataGridViewRecords.SelectedRows.Count; i++)
				{
					DataGridViewRow row = dataGridViewRecords.SelectedRows[i];
					string uid = row.Cells["ID"].Value.ToString();

                    Довідники.<xsl:value-of select="$Directory/Name"/>_Objest <xsl:value-of select="$Directory/Name"/>_Objest = new Довідники.<xsl:value-of select="$Directory/Name"/>_Objest();
                    if (<xsl:value-of select="$Directory/Name"/>_Objest.Read(new UnigueID(uid)))
                    {
						Довідники.<xsl:value-of select="$Directory/Name"/>_Objest <xsl:value-of select="$Directory/Name"/>_Objest_Новий = <xsl:value-of select="$Directory/Name"/>_Objest.Copy();
						<xsl:value-of select="$Directory/Name"/>_Objest_Новий.Назва = <xsl:value-of select="$Directory/Name"/>_Objest_Новий.Назва + " - Копія";
						//<xsl:value-of select="$Directory/Name"/>_Objest_Новий.Код = (++Константи.НумераціяДовідників.<xsl:value-of select="$Directory/Name"/>_Const).ToString("D6");
						<xsl:value-of select="$Directory/Name"/>_Objest_Новий.Save();
					}
                    else
                    {
                        MessageBox.Show("Error read");
                        break;
                    }
                }

				LoadRecords();
			}
		}

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
			if (dataGridViewRecords.SelectedRows.Count != 0 &amp;&amp;
				MessageBox.Show("Видалити записи?", "Повідомлення", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				for (int i = 0; i &lt; dataGridViewRecords.SelectedRows.Count; i++)
				{
					DataGridViewRow row = dataGridViewRecords.SelectedRows[i];
					string uid = row.Cells["ID"].Value.ToString();

                    Довідники.<xsl:value-of select="$Directory/Name"/>_Objest <xsl:value-of select="$Directory/Name"/>_Objest = new Довідники.<xsl:value-of select="$Directory/Name"/>_Objest();
                    if (<xsl:value-of select="$Directory/Name"/>_Objest.Read(new UnigueID(uid)))
                    {
						<xsl:value-of select="$Directory/Name"/>_Objest.Delete();
                    }
                    else
                    {
                        MessageBox.Show("Error read");
                        break;
                    }
                }

				LoadRecords();
			}
		}
    }
}
	
    </xsl:template>

	<xsl:template name="DirectoryFormElementDesigner">
		<xsl:variable name="Directory" select="Configuration/Directories/Directory[Name = $ConfObjectName]" />

namespace StorageAndTrade
{
    partial class Form_<xsl:value-of select="$Directory/Name"/>Елемент
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing &amp;&amp; (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_<xsl:value-of select="$Directory/Name"/>Елемент));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Код = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(230, 51);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(164, 27);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Закрити";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(60, 51);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(164, 27);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Зберегти";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(60, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(489, 20);
            this.textBoxName.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Назва:";
            // 
            // textBox_Код
            // 
            this.textBox_Код.Location = new System.Drawing.Point(591, 12);
            this.textBox_Код.Name = "textBox_Код";
            this.textBox_Код.Size = new System.Drawing.Size(167, 20);
            this.textBox_Код.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(556, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Код:";
            // 
            // Form_<xsl:value-of select="$Directory/Name"/>Елемент
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 97);
            this.Controls.Add(this.textBox_Код);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_<xsl:value-of select="$Directory/Name"/>Елемент";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<xsl:value-of select="$Directory/Name"/>";
            this.Load += new System.EventHandler(this.Form_<xsl:value-of select="$Directory/Name"/>Елемент_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Код;
        private System.Windows.Forms.Label label1;
    }
}
	
	</xsl:template>

	<xsl:template name="DirectoryFormElement">
		<xsl:variable name="Directory" select="Configuration/Directories/Directory[Name = $ConfObjectName]" />

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AccountingSoftware;
using Конфа = <xsl:value-of select="Configuration/NameSpace"/>;
using Константи = <xsl:value-of select="Configuration/NameSpace"/>.Константи;
using Довідники = <xsl:value-of select="Configuration/NameSpace"/>.Довідники;
using Перелічення = <xsl:value-of select="Configuration/NameSpace"/>.Перелічення;

namespace StorageAndTrade
{
    public partial class Form_<xsl:value-of select="$Directory/Name"/>Елемент : Form
    {
        public Form_<xsl:value-of select="$Directory/Name"/>Елемент()
        {
            InitializeComponent();
        }

        public Form_<xsl:value-of select="$Directory/Name"/> OwnerForm { get; set; }
        
        public Nullable&lt;bool&gt; IsNew { get; set; }

        public string Uid { get; set; }

        private Довідники.<xsl:value-of select="$Directory/Name"/>_Objest <xsl:value-of select="$Directory/Name"/>_Objest { get; set; }

		private void Form_<xsl:value-of select="$Directory/Name"/>Елемент_Load(object sender, EventArgs e)
        {
			if (IsNew.HasValue)
			{
				<xsl:value-of select="$Directory/Name"/>_Objest = new Довідники.<xsl:value-of select="$Directory/Name"/>_Objest();

				if (IsNew.Value)
				{
					this.Text += " - Новий";
					//textBox_Код.Text = <xsl:value-of select="$Directory/Name"/>_Objest.Код = (++Константи.НумераціяДовідників.<xsl:value-of select="$Directory/Name"/>_Const).ToString("D6");
				}
				else
				{
					if (<xsl:value-of select="$Directory/Name"/>_Objest.Read(new UnigueID(Uid)))
					{
						this.Text += " - Редагування";

						textBoxName.Text = <xsl:value-of select="$Directory/Name"/>_Objest.Назва;
						textBox_Код.Text = <xsl:value-of select="$Directory/Name"/>_Objest.Код;
					}
					else
						MessageBox.Show("Error read");
				}
			}
		}

        private void buttonSave_Click(object sender, EventArgs e)
        {
			if (IsNew.HasValue)
			{
				if (IsNew.Value)
					<xsl:value-of select="$Directory/Name"/>_Objest.New();

				try
				{
					<xsl:value-of select="$Directory/Name"/>_Objest.Назва = textBoxName.Text;
					<xsl:value-of select="$Directory/Name"/>_Objest.Код = textBox_Код.Text;
					<xsl:value-of select="$Directory/Name"/>_Objest.Save();
				}
				catch (Exception exp)
				{
					MessageBox.Show(exp.Message);
					return;
				}

				if (OwnerForm != null)
					OwnerForm.LoadRecords();

				this.Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
	
	</xsl:template>

</xsl:stylesheet>
