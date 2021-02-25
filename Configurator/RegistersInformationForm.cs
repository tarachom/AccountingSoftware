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
			}

			LoadTreeViewFields();
		}

		private void LoadTreeViewDimension(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField dimensionField in ConfRegistersInformation.DimensionFields.Values)
			{
				TreeNode dimensionNode = rootNode.Nodes.Add(dimensionField.Name, dimensionField.Name);
				dimensionNode.Tag = new  Tuple<ConfigurationObjectField, string>(dimensionField, "Dimension");
			}
		}

		private void LoadTreeViewResources(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField resourcesFields in ConfRegistersInformation.ResourcesFields.Values)
			{
				TreeNode resourcesNode = rootNode.Nodes.Add(resourcesFields.Name, resourcesFields.Name);
				resourcesNode.Tag = new Tuple<ConfigurationObjectField, string>(resourcesFields, "Resources");
			}
		}

		private void LoadTreeViewProperty(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationObjectField propertyFields in ConfRegistersInformation.PropertyFields.Values)
			{
				TreeNode propertyNode = rootNode.Nodes.Add(propertyFields.Name, propertyFields.Name);
				propertyNode.Tag = new Tuple<ConfigurationObjectField, string>(propertyFields, "Property");
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

		bool CallBack_IsExistFieldName(string name)
		{
			return ConfRegistersInformation.DimensionFields.ContainsKey(name) ||
				ConfRegistersInformation.ResourcesFields.ContainsKey(name) ||
				ConfRegistersInformation.PropertyFields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew, object Tag)
		{
			string flagTag = Tag.ToString();
			if (!(flagTag == "Dimension" || flagTag == "Resources" || flagTag == "Property"))
				throw new Exception("Error flag value");

			if (isNew)
			{
				if (flagTag == "Dimension")
					ConfRegistersInformation.AppendDimensionField(configurationObjectField);
				else if (flagTag == "Resources")
					ConfRegistersInformation.AppendResourcesField(configurationObjectField);
				else if (flagTag == "Property")
					ConfRegistersInformation.AppendPropertyField(configurationObjectField);					
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					if (flagTag == "Dimension")
					{
						ConfRegistersInformation.DimensionFields.Remove(originalName);
						ConfRegistersInformation.AppendDimensionField(configurationObjectField);
					}
					else if (flagTag == "Resources")
					{
						ConfRegistersInformation.ResourcesFields.Remove(originalName);
						ConfRegistersInformation.AppendResourcesField(configurationObjectField);
					}
					else if (flagTag == "Property")
					{
						ConfRegistersInformation.PropertyFields.Remove(originalName);
						ConfRegistersInformation.AppendPropertyField(configurationObjectField);
					}
				}
				else
				{
					if (flagTag == "Dimension")
						ConfRegistersInformation.DimensionFields[originalName] = configurationObjectField;
					else if (flagTag == "Resources")
						ConfRegistersInformation.ResourcesFields[originalName] = configurationObjectField;
					else if (flagTag == "Property")
						ConfRegistersInformation.PropertyFields[originalName] = configurationObjectField;					
				}
			}

			if (flagTag == "Dimension")
				LoadTreeViewDimension(treeViewFields.Nodes["root"].Nodes["Dimension"]);
			else if (flagTag == "Resources")
				LoadTreeViewResources(treeViewFields.Nodes["root"].Nodes["Resources"]);
			else if (flagTag == "Property")
				LoadTreeViewProperty(treeViewFields.Nodes["root"].Nodes["Property"]);
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

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			contextMenuStripAddField.Show(ToolStripDropDown.MousePosition);
		}

		private Dictionary<string, ConfigurationObjectField> GetAllField()
		{
			Dictionary<string, ConfigurationObjectField> allField = new Dictionary<string, ConfigurationObjectField>();

			foreach (KeyValuePair<string, ConfigurationObjectField> keyValuePair in ConfRegistersInformation.DimensionFields)
				allField.Add(keyValuePair.Key, keyValuePair.Value);

			foreach (KeyValuePair<string, ConfigurationObjectField> keyValuePair in ConfRegistersInformation.ResourcesFields)
				allField.Add(keyValuePair.Key, keyValuePair.Value);

			foreach (KeyValuePair<string, ConfigurationObjectField> keyValuePair in ConfRegistersInformation.PropertyFields)
				allField.Add(keyValuePair.Key, keyValuePair.Value);

			return allField;
		}

		private void addDimensionField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.Tag = "Dimension";
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfRegistersInformation.Table, GetAllField());
			fieldForm.Show();
		}

		private void treeViewFields_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				Tuple<ConfigurationObjectField, string> tuple = (Tuple<ConfigurationObjectField, string>)e.Node.Tag;
				ConfigurationObjectField field = tuple.Item1;
				string flagTag = tuple.Item2;

				FieldForm fieldForm = new FieldForm();
				fieldForm.Tag = flagTag;
				fieldForm.ConfigurationObjectField = field;
				fieldForm.CallBack = CallBack_Update_Field;
				fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
				fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfRegistersInformation.Table, GetAllField());
				fieldForm.Show();
			}
		}

		private void addResourcesField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.Tag = "Resources";
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfRegistersInformation.Table, GetAllField());
			fieldForm.Show();
		}

		private void addPropertyField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.Tag = "Property";
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfRegistersInformation.Table, GetAllField());
			fieldForm.Show();
		}
	}
}