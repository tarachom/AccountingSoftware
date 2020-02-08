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

		public string originalName { get; set; }

		public bool IsNew { get; set; }

		public FieldForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			foreach (FieldType fieldType in FieldType.DefaultList())
				comboBoxFieldType.Items.Add(fieldType);

			comboBoxFieldType.SelectedItem = comboBoxFieldType.Items[0];

			if (configurationObjectField == null)
			{
				configurationObjectField = new ConfigurationObjectField();
				IsNew = true;
			}
			else
			{
				originalName = configurationObjectField.Name;

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

				IsNew = false;
			}			
		}
		
		private void buttonSave_Click(object sender, EventArgs e)
		{
			configurationObjectField.Name = textBoxName.Text;
			configurationObjectField.NameInTable = textBoxNameInTable.Text;
			configurationObjectField.Desc = textBoxDesc.Text;
			configurationObjectField.Type = ((FieldType)comboBoxFieldType.SelectedItem).ConfTypeName;

			CallBack.Invoke(originalName, configurationObjectField, IsNew);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
