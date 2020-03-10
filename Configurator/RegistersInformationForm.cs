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
	public partial class RegistersInformationForm : Form
	{
		public RegistersInformationForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationRegistersInformation, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistRegistersInformation { get; set; }

		public ConfigurationRegistersInformation ConfRegistersInformation { get; set; }
		public bool IsNew { get; set; }
		public string OriginalName { get; set; }

		private void TablePartForm_Load(object sender, EventArgs e)
		{
			if (ConfRegistersInformation == null)
			{
				ConfRegistersInformation = new ConfigurationRegistersInformation();

				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);

				IsNew = true;
			}
			else
			{
				OriginalName = ConfRegistersInformation.Name;

				textBoxName.Text = ConfRegistersInformation.Name;
				textBoxTable.Text = ConfRegistersInformation.Table;
				textBoxDesc.Text = ConfRegistersInformation.Desc;

				IsNew = false;

				LoadTreeViewFields();
			}
		}

		private void LoadTreeViewDimension(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField dimensionField in ConfRegistersInformation.DimensionFields.Values)
			{
				TreeNode dimensionNode = rootNode.Nodes.Add(dimensionField.Name, dimensionField.Name);
			}
		}

		private void LoadTreeViewResources(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField resourcesFields in ConfRegistersInformation.ResourcesFields.Values)
			{
				TreeNode resourcesNode = rootNode.Nodes.Add(resourcesFields.Name, resourcesFields.Name);
			}
		}

		private void LoadTreeViewProperty(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField propertyFields in ConfRegistersInformation.PropertyFields.Values)
			{
				TreeNode propertyNode = rootNode.Nodes.Add(propertyFields.Name, propertyFields.Name);
			}
		}

		private void LoadTreeViewFields()
		{
			TreeNode rootNode = treeViewFields.Nodes.Add("root", "Поля");

			TreeNode dimensionNode = rootNode.Nodes.Add("Dimension", "Виміри");
			LoadTreeViewDimension(dimensionNode);

			TreeNode resourcesNode = rootNode.Nodes.Add("Resources", "Ресурси");
			LoadTreeViewResources(resourcesNode);

			TreeNode propertyNode = rootNode.Nodes.Add("Property", "Поля");
			LoadTreeViewProperty(propertyNode);

			rootNode.ExpandAll();
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
				if (CallBack_IsExistRegistersInformation(name))
				{
					MessageBox.Show("Назва регістру не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfRegistersInformation.Name = textBoxName.Text;
			ConfRegistersInformation.Table = textBoxTable.Text;
			ConfRegistersInformation.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfRegistersInformation, IsNew);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		//bool CallBack_IsExistFieldName(string name)
		//{
		//	return ConfDirectoryTablePart.Fields.ContainsKey(name);
		//}

		//void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew)
		//{
		//	if (isNew)
		//	{
		//		ConfDirectoryTablePart.AppendField(configurationObjectField);
		//	}
		//	else
		//	{
		//		if (originalName != configurationObjectField.Name)
		//		{
		//			ConfDirectoryTablePart.Fields.Remove(originalName);
		//			ConfDirectoryTablePart.AppendField(configurationObjectField);
		//		}
		//		else
		//		{
		//			ConfDirectoryTablePart.Fields[originalName] = configurationObjectField;
		//		}
		//	}
		//}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			//FieldForm fieldForm = new FieldForm();
			//fieldForm.CallBack = CallBack_Update_Field;
			//fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			//fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectoryTablePart.Table, ConfDirectoryTablePart.Fields);
			//fieldForm.Show();
		}

		private void TablePartForm_KeyDown(object sender, KeyEventArgs e)
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