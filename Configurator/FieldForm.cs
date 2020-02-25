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
		public Action<string, ConfigurationObjectField, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistFieldName { get; set; }

		public ConfigurationObjectField configurationObjectField { get; set; }
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

			if (configurationObjectField == null)
			{
				configurationObjectField = new ConfigurationObjectField();
				textBoxNameInTable.Text = NewNameInTable;

				IsNew = true;
			}
			else
			{
				OriginalName = configurationObjectField.Name;

				textBoxName.Text = configurationObjectField.Name;
				textBoxNameInTable.Text = configurationObjectField.NameInTable;
				textBoxDesc.Text = configurationObjectField.Desc;

				for (int i = 0; i < comboBoxFieldType.Items.Count; i++)
				{
					FieldType fieldType = (FieldType)comboBoxFieldType.Items[i];
					if (fieldType.ConfTypeName == configurationObjectField.Type)
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
						if (configurationObjectField.Pointer == comboBoxPointer.Items[i].ToString())
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
						if (configurationObjectField.Pointer == comboBoxEnums.Items[i].ToString())
						{
							comboBoxEnums.SelectedItem = comboBoxEnums.Items[i];
							break;
						}
					}
				}

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

			configurationObjectField.Name = textBoxName.Text;
			configurationObjectField.NameInTable = textBoxNameInTable.Text;
			configurationObjectField.Desc = textBoxDesc.Text;
			configurationObjectField.Type = confTypeName;

			if (confTypeName == "pointer")
			{
				configurationObjectField.Pointer = comboBoxPointer.SelectedItem.ToString();
			}
			else if (confTypeName == "enum")
			{
				configurationObjectField.Pointer = comboBoxEnums.SelectedItem.ToString();
			}
			else
			{
				configurationObjectField.Pointer = "";
			}

			CallBack.Invoke(OriginalName, configurationObjectField, IsNew);

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
	}
}