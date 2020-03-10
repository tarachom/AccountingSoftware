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
	public partial class ConstantsForm : Form
	{
		public Action<string, string, ConfigurationConstants, bool> CallBack { get; set; }
		public Func<string, string, Boolean> CallBack_IsExistConstants { get; set; }

		public ConfigurationConstants Constants;

		public string OriginalName { get; set; }
		public bool IsNew { get; set; }
		public string ConstantsBlock { get; set; }

		public ConstantsForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			//Блоки констант
			foreach (string constantsBlockName in Program.Kernel.Conf.ConstantsBlock.Keys)
				comboBoxBlock.Items.Add(constantsBlockName);

			if (comboBoxBlock.Items.Count > 0)
				comboBoxBlock.SelectedItem = comboBoxBlock.Items[0];

			//Типи даних
			foreach (FieldType fieldType in FieldType.DefaultList())
				comboBoxFieldType.Items.Add(fieldType);

			comboBoxFieldType.SelectedItem = comboBoxFieldType.Items[0];
			comboBoxPointer.Enabled = false;
			comboBoxEnums.Enabled = false;

			//Список довідників
			foreach (string directoryName in Program.Kernel.Conf.Directories.Keys)
				comboBoxPointer.Items.Add("Довідники." + directoryName);

			//Список документів
			foreach (string documentName in Program.Kernel.Conf.Documents.Keys)
				comboBoxPointer.Items.Add("Документи." + documentName);

			//Список перелічення
			foreach (string enumName in Program.Kernel.Conf.Enums.Keys)
				comboBoxEnums.Items.Add("Перелічення." + enumName);

			if (Constants == null)
			{
				Constants = new ConfigurationConstants();
				IsNew = true;
			}
			else
			{
				OriginalName = Constants.Name;
				ConstantsBlock = Constants.Block.BlockName;

				textBoxName.Text = Constants.Name;
				textBoxDesc.Text = Constants.Desc;

				for (int i = 0; i < comboBoxBlock.Items.Count; i++)
				{
					if (ConstantsBlock == comboBoxBlock.Items[i].ToString())
						comboBoxBlock.SelectedItem = comboBoxBlock.Items[i];
				}

				for (int i = 0; i < comboBoxFieldType.Items.Count; i++)
				{
					FieldType fieldType = (FieldType)comboBoxFieldType.Items[i];
					if (fieldType.ConfTypeName == Constants.Type)
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
						if (Constants.Pointer == comboBoxPointer.Items[i].ToString())
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
						if (Constants.Pointer == comboBoxEnums.Items[i].ToString())
						{
							comboBoxEnums.SelectedItem = comboBoxEnums.Items[i];
							break;
						}
					}
				}

				LoadTabularPartsList();

				IsNew = false;				
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			ConstantsBlock = comboBoxBlock.SelectedItem.ToString();

			string name = textBoxName.Text;
			string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);
			textBoxName.Text = name;

			if (errorList.Length > 0)
			{
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsNew || OriginalName != name || ConstantsBlock != Constants.Block.BlockName)
				if (CallBack_IsExistConstants(ConstantsBlock, name))
				{
					MessageBox.Show("Назва константи не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			string confTypeName = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

			Constants.Name = textBoxName.Text;
			Constants.Desc = textBoxDesc.Text;
			Constants.Type = confTypeName;

			if (confTypeName == "pointer")
			{
				Constants.Pointer = comboBoxPointer.SelectedItem.ToString();
			}
			else if (confTypeName == "enum")
			{
				Constants.Pointer = comboBoxEnums.SelectedItem.ToString();
			}
			else
			{
				Constants.Pointer = "";
			}

			CallBack.Invoke(ConstantsBlock, OriginalName, Constants, IsNew);

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

		private void EnumFieldForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (MessageBox.Show("Закрити форму?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					this.Hide();
				}
			}
		}

		void LoadTabularPartsList()
		{
			listBoxTabularParts.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in Constants.TabularParts)
			{
				listBoxTabularParts.Items.Add(configurationObjectTablePart.Value.Name);
			}
		}

		bool CallBack_IsExistTablePartName(string name)
		{
			return Constants.TabularParts.ContainsKey(name);
		}

		void CallBack_Update_TablePart(string originalName, ConfigurationObjectTablePart configurationObjectTablePart, bool isNew)
		{
			if (isNew)
			{
				Constants.AppendTablePart(configurationObjectTablePart);
			}
			else
			{
				if (originalName != configurationObjectTablePart.Name)
				{
					Constants.TabularParts.Remove(originalName);
					Constants.AppendTablePart(configurationObjectTablePart);
				}
				else
				{
					Constants.TabularParts[originalName] = configurationObjectTablePart;
				}
			}

			LoadTabularPartsList();
		}

		private void buttonAddTablePart_Click(object sender, EventArgs e)
		{
			TablePartForm tablePartForm = new TablePartForm();
			tablePartForm.CallBack = CallBack_Update_TablePart;
			tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;
			tablePartForm.Show();
		}

		private void listBoxTabularParts_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxTabularParts.SelectedItem != null)
			{
				TablePartForm tablePartForm = new TablePartForm();
				tablePartForm.ConfDirectoryTablePart = Constants.TabularParts[listBoxTabularParts.SelectedItem.ToString()];
				tablePartForm.CallBack = CallBack_Update_TablePart;
				tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;

				tablePartForm.Show();
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

						Constants.TabularParts.Remove(listBoxTabularParts.SelectedItem.ToString());
						LoadTabularPartsList();

						if (selectIndex >= listBoxTabularParts.Items.Count)
							selectIndex = listBoxTabularParts.Items.Count - 1;

						listBoxTabularParts.SelectedIndex = selectIndex;
					}
				}
			}
		}
	}
}