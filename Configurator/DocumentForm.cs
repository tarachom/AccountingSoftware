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
	public partial class DocumentForm : Form
	{
		public DocumentForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationDocuments, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistDocumentName { get; set; }

		public ConfigurationDocuments ConfDocument { get; set; }
		public string OriginalName { get; set; }
		public bool IsNewDocument { get; set; }
		public Dictionary<string, ConfigurationRegistersAccumulation> RegistersAccumulation { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDocument == null)
			{
				ConfDocument = new ConfigurationDocuments();
				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);

				string newUnigueNameInTable_Назва = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
				ConfDocument.AppendField(new ConfigurationObjectField("Назва", newUnigueNameInTable_Назва, "string", "", "Назва"));

				string newUnigueNameInTable_ДатаДок = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
				ConfDocument.AppendField(new ConfigurationObjectField("ДатаДок", newUnigueNameInTable_ДатаДок, "datetime", "", "ДатаДок"));

				string newUnigueNameInTable_НомерДок = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
				ConfDocument.AppendField(new ConfigurationObjectField("НомерДок", newUnigueNameInTable_НомерДок, "string", "", "НомерДок"));

				IsNewDocument = true;
			}
			else
			{
				OriginalName = ConfDocument.Name;

				textBoxName.Text = ConfDocument.Name;
				textBoxTable.Text = ConfDocument.Table;
				textBoxDesc.Text = ConfDocument.Desc;

				textBoxTriggersAfterSave.Text = ConfDocument.TriggerFunctions.AfterSave;
				textBoxTriggersBeforeSave.Text = ConfDocument.TriggerFunctions.BeforeSave;
				textBoxTriggersBeforeDelete.Text = ConfDocument.TriggerFunctions.BeforeDelete;

				textBoxSpend.Text = ConfDocument.SpendFunctions.Spend;
				textBoxClearSpend.Text = ConfDocument.SpendFunctions.ClearSpend;

				IsNewDocument = false;
			}

			LoadFieldList();
			LoadTabularPartsList();
			LoadRegisters();
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

			if (IsNewDocument || OriginalName != name)
				if (CallBack_IsExistDocumentName(name))
				{
					MessageBox.Show("Назва документу не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfDocument.Name = textBoxName.Text;
			ConfDocument.Table = textBoxTable.Text;
			ConfDocument.Desc = textBoxDesc.Text;

			//Доступні регістри накопичення
			ConfDocument.AllowRegisterAccumulation.Clear();
			foreach (string register in checkedListBoxRegisters.CheckedItems)
				ConfDocument.AllowRegisterAccumulation.Add(register);

			//Тригери
			ConfDocument.TriggerFunctions.BeforeSave = textBoxTriggersBeforeSave.Text;
			ConfDocument.TriggerFunctions.AfterSave = textBoxTriggersAfterSave.Text;
			ConfDocument.TriggerFunctions.BeforeDelete = textBoxTriggersBeforeDelete.Text;

			//Функції проведення
			ConfDocument.SpendFunctions.Spend = textBoxSpend.Text;
			ConfDocument.SpendFunctions.ClearSpend = textBoxClearSpend.Text;

			CallBack.Invoke(OriginalName, ConfDocument, IsNewDocument);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
		
		bool CallBack_IsExistFieldName(string name)
		{
			return ConfDocument.Fields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew, object Tag = null)
		{
			if (isNew)
			{
				ConfDocument.AppendField(configurationObjectField);
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					ConfDocument.Fields.Remove(originalName);
					ConfDocument.AppendField(configurationObjectField);
				}
				else
				{
					ConfDocument.Fields[originalName] = configurationObjectField;
				}
			}

			LoadFieldList();
		}

		bool CallBack_IsExistTablePartName(string name)
		{
			return ConfDocument.TabularParts.ContainsKey(name);
		}

		void CallBack_Update_TablePart(string originalName, ConfigurationObjectTablePart configurationObjectTablePart, bool isNew)
		{
			if (isNew)
			{
				ConfDocument.AppendTablePart(configurationObjectTablePart);
			}
			else
			{
				if (originalName != configurationObjectTablePart.Name)
				{
					ConfDocument.TabularParts.Remove(originalName);
					ConfDocument.AppendTablePart(configurationObjectTablePart);
				}
				else
				{
					ConfDocument.TabularParts[originalName] = configurationObjectTablePart;
				}
			}

			LoadTabularPartsList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
			fieldForm.Show();
		}

		private void buttonAddTablePart_Click(object sender, EventArgs e)
		{
			TablePartForm tablePartForm = new TablePartForm();
			tablePartForm.CallBack = CallBack_Update_TablePart;
			tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;
			tablePartForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDocument.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		void LoadTabularPartsList()
		{
			listBoxTabularParts.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in ConfDocument.TabularParts)
			{
				listBoxTabularParts.Items.Add(configurationObjectTablePart.Value.Name);
			}
		}

		void LoadRegisters()
        {
			checkedListBoxRegisters.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationRegistersAccumulation> configurationRegistersAccumulation in RegistersAccumulation)
			{
				checkedListBoxRegisters.Items.Add(
					configurationRegistersAccumulation.Value.Name,
					ConfDocument.AllowRegisterAccumulation.Contains(configurationRegistersAccumulation.Value.Name)
				);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.ConfigurationObjectField = ConfDocument.Fields[listBoxFields.SelectedItem.ToString()];
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
				tablePartForm.ConfDirectoryTablePart = ConfDocument.TabularParts[listBoxTabularParts.SelectedItem.ToString()];
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
					if (MessageBox.Show("Видалити поле " + listBoxFields.SelectedItem.ToString() + "?", "Видалити поле?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						int selectIndex = listBoxFields.SelectedIndex;

						ConfDocument.Fields.Remove(listBoxFields.SelectedItem.ToString());
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

						ConfDocument.TabularParts.Remove(listBoxTabularParts.SelectedItem.ToString());
						LoadTabularPartsList();

						if (selectIndex >= listBoxTabularParts.Items.Count)
							selectIndex = listBoxTabularParts.Items.Count - 1;

						listBoxTabularParts.SelectedIndex = selectIndex;
					}
				}
			}
		}

		private void DocumentForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (MessageBox.Show("Закрити форму?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					this.Hide();
				}
			}
		}

		private void RewriteConfDictionaryFields()
		{
			//Новий словник полів
			Dictionary<string, ConfigurationObjectField> newFields = new Dictionary<string, ConfigurationObjectField>();

			//Прохід по полях в списку і копіювання в новий словник
			foreach (string field in listBoxFields.Items)
				newFields.Add(field, ConfDocument.Fields[field]);

			//Очищення словника полів конфігурації
			ConfDocument.Fields.Clear();

			//Копіювання з нового словника в словник конфігурації
			foreach (KeyValuePair<string, ConfigurationObjectField> item in newFields)
				ConfDocument.Fields.Add(item.Key, item.Value);
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