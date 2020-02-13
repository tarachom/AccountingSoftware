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
		public ConfigurationObjectField configurationObjectField { get; set; }
		public string OriginalName { get; set; }
		public bool IsNew { get; set; }

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
			//comboBoxPointer.Items.Add("");

			//Список довідників
			foreach (string directoryName in Program.Kernel.Conf.Directories.Keys)
			{
				comboBoxPointer.Items.Add(directoryName);
			}

			if (configurationObjectField == null)
			{
				configurationObjectField = new ConfigurationObjectField();
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

				if (((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName == "pointer")
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

				IsNew = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			string name = textBoxName.Text;
			string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);

			if (errorList.Length > 0)
			{
				textBoxName.Text = name;
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			configurationObjectField.Name = textBoxName.Text;
			configurationObjectField.NameInTable = textBoxNameInTable.Text;
			configurationObjectField.Desc = textBoxDesc.Text;
			configurationObjectField.Type = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

			if (((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName == "pointer")
			{
				configurationObjectField.Pointer = comboBoxPointer.SelectedItem.ToString();
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
			comboBoxPointer.Enabled = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName == "pointer";
		}
	}
}
