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
	public partial class FieldForm : Form
	{
		//1. Оригінальна назва поля 2. Об'єкт 3. Чи новий? 4. Tag
		public Action<string, ConfigurationObjectField, bool, object> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistFieldName { get; set; }

		public ConfigurationObjectField ConfigurationObjectField { get; set; }
		public string OriginalName { get; set; }
		public bool IsNew { get; set; }
		public string NewNameInTable { get; set; }

		public FieldForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			//Типи даних
			foreach (FieldType fieldType in FieldType.DefaultList())
				comboBoxFieldType.Items.Add(fieldType);

			comboBoxFieldType.SelectedItem = comboBoxFieldType.Items[0];
			comboBoxPointer.Enabled = false;
			comboBoxEnums.Enabled = false;

			//Список довідників
			foreach (string directoryName in Program.Kernel.Conf.Directories.Keys)
			{
				comboBoxPointer.Items.Add("Довідники." + directoryName);
			}

			//Список документів
			foreach (string documentName in Program.Kernel.Conf.Documents.Keys)
			{
				comboBoxPointer.Items.Add("Документи." + documentName);
			}

			//Список перелічення
			foreach (string enumName in Program.Kernel.Conf.Enums.Keys)
			{
				comboBoxEnums.Items.Add("Перелічення." + enumName);
			}

			if (ConfigurationObjectField == null)
			{
				ConfigurationObjectField = new ConfigurationObjectField();
				textBoxNameInTable.Text = NewNameInTable;

				IsNew = true;
			}
			else
			{
				OriginalName = ConfigurationObjectField.Name;

				textBoxName.Text = ConfigurationObjectField.Name;
				textBoxNameInTable.Text = ConfigurationObjectField.NameInTable;
				textBoxDesc.Text = ConfigurationObjectField.Desc;

				for (int i = 0; i < comboBoxFieldType.Items.Count; i++)
				{
					FieldType fieldType = (FieldType)comboBoxFieldType.Items[i];
					if (fieldType.ConfTypeName == ConfigurationObjectField.Type)
					{
						comboBoxFieldType.SelectedItem = comboBoxFieldType.Items[i];
						break;
					}
				}

				string confTypeName = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

				if (confTypeName == "pointer")
				{
					for (int i = 0; i < comboBoxPointer.Items.Count; i++)
					{
						if (ConfigurationObjectField.Pointer == comboBoxPointer.Items[i].ToString())
						{
							comboBoxPointer.SelectedItem = comboBoxPointer.Items[i];
							break;
						}
					}
				}
				else if (confTypeName == "enum")
				{
					for (int i = 0; i < comboBoxEnums.Items.Count; i++)
					{
						if (ConfigurationObjectField.Pointer == comboBoxEnums.Items[i].ToString())
						{
							comboBoxEnums.SelectedItem = comboBoxEnums.Items[i];
							break;
						}
					}
				}

				checkBoxIsPresentation.Checked = ConfigurationObjectField.IsPresentation;

				IsNew = false;
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

			if (IsNew || OriginalName != name)
				if (CallBack_IsExistFieldName(name))
				{
					MessageBox.Show("Назва поля не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			string confTypeName = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

			ConfigurationObjectField.Name = textBoxName.Text;
			ConfigurationObjectField.NameInTable = textBoxNameInTable.Text;
			ConfigurationObjectField.Desc = textBoxDesc.Text;
			ConfigurationObjectField.Type = confTypeName;

			if (confTypeName == "pointer")
			{
				if (comboBoxPointer.SelectedItem != null)
					ConfigurationObjectField.Pointer = comboBoxPointer.SelectedItem.ToString();
				else
				{
					MessageBox.Show("Не заповнено поле вказівник", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			else if (confTypeName == "enum")
			{
				if (comboBoxEnums.SelectedItem != null)
					ConfigurationObjectField.Pointer = comboBoxEnums.SelectedItem.ToString();
				else
				{
					MessageBox.Show("Не заповнено поле перелічення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			else
			{
				ConfigurationObjectField.Pointer = "";
			}

			ConfigurationObjectField.IsPresentation = checkBoxIsPresentation.Checked;

			CallBack.Invoke(OriginalName, ConfigurationObjectField, IsNew, this.Tag);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void comboBoxFieldType_SelectedIndexChanged(object sender, EventArgs e)
		{
			string confTypeName = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

			comboBoxPointer.Enabled = (confTypeName == "pointer");
			comboBoxEnums.Enabled = (confTypeName == "enum");
		}

		private void FieldForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (MessageBox.Show("Закрити форму?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					this.Hide();
				}
			}
		}
	}
}