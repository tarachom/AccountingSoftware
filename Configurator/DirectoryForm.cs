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

				LoadFieldList();
			}
			else
			{
				OriginalName = ConfDirectory.Name;

				textBoxName.Text = ConfDirectory.Name;
				textBoxTable.Text = ConfDirectory.Table;
				textBoxDesc.Text = ConfDirectory.Desc;

				textBoxTriggersAfterSave.Text = ConfDirectory.TriggerFunctions.AfterSave;
				textBoxTriggersBeforeSave.Text = ConfDirectory.TriggerFunctions.BeforeSave;
				textBoxTriggersBeforeDelete.Text = ConfDirectory.TriggerFunctions.BeforeDelete;

				IsNewDirectory = false;

				LoadFieldList();
				LoadTabularPartsList();
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

			//Тригери
			ConfDirectory.TriggerFunctions.BeforeSave = textBoxTriggersBeforeSave.Text;
			ConfDirectory.TriggerFunctions.AfterSave = textBoxTriggersAfterSave.Text;
			ConfDirectory.TriggerFunctions.BeforeDelete = textBoxTriggersBeforeDelete.Text;

			CallBack.Invoke(OriginalName, ConfDirectory, IsNewDirectory);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void checkBoxHierarchical_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxHierarchical.Enabled = checkBoxHierarchical.Checked;
		}

		#region Callback

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

		#endregion

		#region buttonAdd (Field, TabularParts, Views)

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

		#endregion

		#region Load List (Field, TabularParts, Views)

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

		#endregion

		#region Mouse & KeyDown

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

		#endregion

		#region Контекстне меню для поля

		private void copyFiled_Click(object sender, EventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				string fieldName = listBoxFields.SelectedItem.ToString();

				string fieldCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					fieldCopyName = fieldName + "_Копія_" + i.ToString();
					if (!ConfDirectory.Fields.ContainsKey(fieldCopyName))
						break;
				}

				ConfigurationObjectField ConfFieldOriginal = ConfDirectory.Fields[fieldName];

				ConfigurationObjectField ConfFieldCopy = new ConfigurationObjectField(fieldCopyName,
					Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectory.Table, ConfDirectory.Fields),
					ConfFieldOriginal.Type, ConfFieldOriginal.Pointer, ConfFieldOriginal.Desc);

				ConfDirectory.AppendField(ConfFieldCopy);

				LoadFieldList();
			}
			else
			{
				if (listBoxFields.Items.Count > 0)
					MessageBox.Show("Виберіть елемент", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		#endregion

		#region Контекстне меню для таблиної частини

		private void copyTablePart_Click(object sender, EventArgs e)
		{
			if (listBoxTabularParts.SelectedItem != null)
			{
				string tablePartName = listBoxTabularParts.SelectedItem.ToString();

				string tablePartCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					tablePartCopyName = tablePartName + "_Копія_" + i.ToString();
					if (!ConfDirectory.TabularParts.ContainsKey(tablePartCopyName))
						break;
				}

				ConfigurationObjectTablePart ConfTablePartOriginal = ConfDirectory.TabularParts[tablePartName];

				ConfigurationObjectTablePart ConfTablePartCopy = new ConfigurationObjectTablePart(tablePartCopyName,
					Configuration.GetNewUnigueTableName(Program.Kernel), ConfTablePartOriginal.Desc);

				ConfDirectory.AppendTablePart(ConfTablePartCopy);

				foreach (ConfigurationObjectField ConfFieldOriginal in ConfTablePartOriginal.Fields.Values) 
				{
					ConfigurationObjectField ConfFieldCopy = new ConfigurationObjectField(ConfFieldOriginal.Name,
					ConfFieldOriginal.NameInTable, ConfFieldOriginal.Type, ConfFieldOriginal.Pointer, ConfFieldOriginal.Desc);

					ConfTablePartCopy.AppendField(ConfFieldCopy);
				}

				LoadTabularPartsList();
			}
			else
			{
				if (listBoxTabularParts.Items.Count > 0)
					MessageBox.Show("Виберіть елемент", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}


        #endregion

		private void RewriteConfDictionaryFields()
        {
			//Новий словник полів
			Dictionary<string, ConfigurationObjectField> newFields = new Dictionary<string, ConfigurationObjectField>();

			//Прохід по полях в списку і копіювання в новий словник
			foreach (string field in listBoxFields.Items)
				newFields.Add(field, ConfDirectory.Fields[field]);

			//Очищення словника полів конфігурації
			ConfDirectory.Fields.Clear();

			//Копіювання з нового словника в словник конфігурації
			foreach (KeyValuePair<string, ConfigurationObjectField> item in newFields)
				ConfDirectory.Fields.Add(item.Key, item.Value);
		}

		private void buttonUp_Click(object sender, EventArgs e)
        {
			if (listBoxFields.SelectedItem != null && listBoxFields.SelectedIndex > 0)
            {
				object selectedItem = listBoxFields.SelectedItem;
				int selectIndex = listBoxFields.SelectedIndex;

				listBoxFields.Items.RemoveAt(selectIndex);
				listBoxFields.Items.Insert(selectIndex - 1, selectedItem);

				listBoxFields.SelectedItem = selectedItem;

				RewriteConfDictionaryFields();
			}
		}

        private void buttonDown_Click(object sender, EventArgs e)
        {
			if (listBoxFields.SelectedItem != null && listBoxFields.SelectedIndex < listBoxFields.Items.Count - 1)
			{
				object selectedItem = listBoxFields.SelectedItem;
				int selectIndex = listBoxFields.SelectedIndex;

				listBoxFields.Items.RemoveAt(selectIndex);
				listBoxFields.Items.Insert(selectIndex + 1, selectedItem);

				listBoxFields.SelectedItem = selectedItem;

				RewriteConfDictionaryFields();
			}
		}
    }
}
