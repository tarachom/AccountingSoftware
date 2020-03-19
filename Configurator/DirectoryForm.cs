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
Сайт:     find.org.ua
*/

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

namespace Configurator
{
	public partial class DirectoryForm : Form
	{
		public DirectoryForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationDirectories, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistDirectoryName { get; set; }

		public ConfigurationDirectories ConfDirectory { get; set; }
		public string OriginalName { get; set; }
		public bool IsNewDirectory { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDirectory == null)
			{
				ConfDirectory = new ConfigurationDirectories();
				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);
				IsNewDirectory = true;

				string newUnigueNameInTable_Name = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectory.Table, ConfDirectory.Fields);
				ConfDirectory.AppendField(new ConfigurationObjectField("Назва", newUnigueNameInTable_Name, "string", "", "Назва"));

				string newUnigueNameInTable_Code = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectory.Table, ConfDirectory.Fields);
				ConfDirectory.AppendField(new ConfigurationObjectField("Код", newUnigueNameInTable_Code, "string", "", "Код"));

				ConfigurationObjectView NewView = new ConfigurationObjectView("Список", textBoxTable.Text, "Список");
				NewView.Fields.Add("Назва", newUnigueNameInTable_Name);
				NewView.Fields.Add("Код", newUnigueNameInTable_Code);
				ConfDirectory.AppendView(NewView);

				LoadFieldList();
				LoadViewsList();
			}
			else
			{
				OriginalName = ConfDirectory.Name;

				textBoxName.Text = ConfDirectory.Name;
				textBoxTable.Text = ConfDirectory.Table;
				textBoxDesc.Text = ConfDirectory.Desc;

				IsNewDirectory = false;

				LoadFieldList();
				LoadTabularPartsList();
				LoadViewsList();
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			string name = textBoxName.Text;
			string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);
			textBoxName.Text = name;

			if (errorList.Length > 0)
			{
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsNewDirectory || OriginalName != name)
				if (CallBack_IsExistDirectoryName(name))
				{
					MessageBox.Show("Назва довідника не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfDirectory.Name = textBoxName.Text;
			ConfDirectory.Table = textBoxTable.Text;
			ConfDirectory.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfDirectory, IsNewDirectory);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		bool CallBack_IsExistFieldName(string name)
		{
			return ConfDirectory.Fields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew, object Tag = null)
		{
			if (isNew)
			{
				ConfDirectory.AppendField(configurationObjectField);
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					ConfDirectory.Fields.Remove(originalName);
					ConfDirectory.AppendField(configurationObjectField);
				}
				else
				{
					ConfDirectory.Fields[originalName] = configurationObjectField;
				}
			}

			LoadFieldList();
		}

		bool CallBack_IsExistTablePartName(string name)
		{
			return ConfDirectory.TabularParts.ContainsKey(name);
		}

		void CallBack_Update_TablePart(string originalName, ConfigurationObjectTablePart configurationObjectTablePart, bool isNew)
		{
			if (isNew)
			{
				ConfDirectory.AppendTablePart(configurationObjectTablePart);
			}
			else
			{
				if (originalName != configurationObjectTablePart.Name)
				{
					ConfDirectory.TabularParts.Remove(originalName);
					ConfDirectory.AppendTablePart(configurationObjectTablePart);
				}
				else
				{
					ConfDirectory.TabularParts[originalName] = configurationObjectTablePart;
				}
			}

			LoadTabularPartsList();
		}

		bool CallBack_IsExistView(string name)
		{
			return ConfDirectory.Views.ContainsKey(name);
		}

		void CallBack_Update_View(string originalName, ConfigurationObjectView configurationObjectView, bool isNew)
		{
			if (isNew)
			{
				ConfDirectory.AppendView(configurationObjectView);
			}
			else
			{
				if (originalName != configurationObjectView.Name)
				{
					ConfDirectory.Views.Remove(originalName);
					ConfDirectory.AppendView(configurationObjectView);
				}
				else
				{
					ConfDirectory.Views[originalName] = configurationObjectView;
				}
			}

			LoadViewsList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectory.Table, ConfDirectory.Fields);
			fieldForm.Show();
		}

		private void buttonAddTablePart_Click(object sender, EventArgs e)
		{
			TablePartForm tablePartForm = new TablePartForm();
			tablePartForm.CallBack = CallBack_Update_TablePart;
			tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;
			tablePartForm.Show();
		}

		private void buttonAddView_Click(object sender, EventArgs e)
		{
			ViewForm viewForm = new ViewForm();
			viewForm.ConfDirectory = ConfDirectory;
			viewForm.CallBack = CallBack_Update_View;
			viewForm.CallBack_IsExistView = CallBack_IsExistView;
			viewForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectory.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		void LoadTabularPartsList()
		{
			listBoxTabularParts.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in ConfDirectory.TabularParts)
			{
				listBoxTabularParts.Items.Add(configurationObjectTablePart.Value.Name);
			}
		}

		void LoadViewsList()
		{
			listBoxViews.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectView> configurationObjectView in ConfDirectory.Views)
			{
				listBoxViews.Items.Add(configurationObjectView.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.ConfigurationObjectField = ConfDirectory.Fields[listBoxFields.SelectedItem.ToString()];
				fieldForm.CallBack = CallBack_Update_Field;
				fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
				fieldForm.Show();
			}
		}

		private void listBoxTabularParts_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxTabularParts.SelectedItem != null)
			{
				TablePartForm tablePartForm = new TablePartForm();
				tablePartForm.ConfDirectoryTablePart = ConfDirectory.TabularParts[listBoxTabularParts.SelectedItem.ToString()];
				tablePartForm.CallBack = CallBack_Update_TablePart;
				tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;

				tablePartForm.Show();
			}
		}

		private void listBoxViews_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxViews.SelectedItem != null)
			{
				ViewForm viewForm = new ViewForm();
				viewForm.ConfView = ConfDirectory.Views[listBoxViews.SelectedItem.ToString()];
				viewForm.ConfDirectory = ConfDirectory;
				viewForm.CallBack = CallBack_Update_View;
				viewForm.CallBack_IsExistView = CallBack_IsExistView;

				viewForm.Show();
			}
		}

		private void listBoxFields_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				if (e.KeyData == Keys.Delete)
				{
					string question = "Видалити поле";

					if (MessageBox.Show(question + " " + listBoxFields.SelectedItem.ToString() + "?", question + "?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						int selectIndex = listBoxFields.SelectedIndex;

						ConfDirectory.Fields.Remove(listBoxFields.SelectedItem.ToString());
						LoadFieldList();

						if (selectIndex >= listBoxFields.Items.Count)
							selectIndex = listBoxFields.Items.Count - 1;

						listBoxFields.SelectedIndex = selectIndex;
					}
				}
			}
		}

		private void listBoxTabularParts_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxTabularParts.SelectedItem != null)
			{
				if (e.KeyData == Keys.Delete)
				{
					string question = "Видалити табличну частину";

					if (MessageBox.Show(question + " " + listBoxTabularParts.SelectedItem.ToString() + "?", question + "?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						int selectIndex = listBoxTabularParts.SelectedIndex;

						ConfDirectory.TabularParts.Remove(listBoxTabularParts.SelectedItem.ToString());
						LoadTabularPartsList();

						if (selectIndex >= listBoxTabularParts.Items.Count)
							selectIndex = listBoxTabularParts.Items.Count - 1;

						listBoxTabularParts.SelectedIndex = selectIndex;
					}
				}
			}
		}

		private void DirectoryForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (MessageBox.Show("Закрити форму?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					this.Hide();
				}
			}
		}

		private void checkBoxHierarchical_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxHierarchical.Enabled = checkBoxHierarchical.Checked;
		}
	}
}