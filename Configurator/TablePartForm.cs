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
	public partial class TablePartForm : Form
	{
		public TablePartForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationObjectTablePart, bool> CallBack { get; set; }
		public ConfigurationObjectTablePart ConfDirectoryTablePart { get; set; }
		public bool IsNewDirectoryTablePart { get; set; }
		public string OriginalName { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDirectoryTablePart == null)
			{
				ConfDirectoryTablePart = new ConfigurationObjectTablePart();
				IsNewDirectoryTablePart = true;
			}
			else
			{
				OriginalName = ConfDirectoryTablePart.Name;

				textBoxName.Text = ConfDirectoryTablePart.Name;
				textBoxTable.Text = ConfDirectoryTablePart.Table;
				textBoxDesc.Text = ConfDirectoryTablePart.Desc;
				IsNewDirectoryTablePart = false;

				LoadFieldList();
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			ConfDirectoryTablePart.Name = textBoxName.Text;
			ConfDirectoryTablePart.Table = textBoxTable.Text;
			ConfDirectoryTablePart.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfDirectoryTablePart, IsNewDirectoryTablePart);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew)
		{
			if (isNew)
			{
				ConfDirectoryTablePart.AppendField(configurationObjectField);
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					ConfDirectoryTablePart.Fields.Remove(originalName);
					ConfDirectoryTablePart.AppendField(configurationObjectField);
				}
				else
				{
					ConfDirectoryTablePart.Fields[originalName] = configurationObjectField;
				}
			}

			LoadFieldList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectoryTablePart.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.configurationObjectField = ConfDirectoryTablePart.Fields[listBoxFields.SelectedItem.ToString()];
				fieldForm.CallBack = CallBack_Update_Field;

				fieldForm.Show();
			}
		}
	}
}