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
	public partial class ViewForm : Form
	{
		public ViewForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationObjectTablePart, bool> CallBack { get; set; }
		//public Func<string, Boolean> CallBack_IsExistTablePartName { get; set; }

		public ConfigurationDirectories ConfDirectory { get; set; }
		public ConfigurationObjectView ConfView { get; set; }
		public bool IsNewView { get; set; }
		public string OriginalName { get; set; }

		private void ViewForm_Load(object sender, EventArgs e)
		{
			if (ConfView == null)
			{
				ConfView = new ConfigurationObjectView();
				IsNewView = true;
			}
			else
			{
				OriginalName = ConfView.Name;

				textBoxName.Text = ConfView.Name;
				textBoxTable.Text = ConfView.Table;
				textBoxPrimaryField.Text = ConfView.PrimaryField;
				textBoxDesc.Text = ConfView.Desc;
				IsNewView = false;

				LoadFieldList();
			}

			LoadAllFieldList();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			//string name = textBoxName.Text;
			//string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);

			//if (errorList.Length > 0)
			//{
			//	textBoxName.Text = name;
			//	MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			//	return;
			//}

			//if (IsNewDirectoryTablePart || OriginalName != name)
			//	if (CallBack_IsExistTablePartName(name))
			//	{
			//		MessageBox.Show("Назва табличної частини не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//		return;
			//	}

			ConfView.Name = textBoxName.Text;
			ConfView.Table = textBoxTable.Text;
			ConfView.PrimaryField = textBoxPrimaryField.Text;
			ConfView.Desc = textBoxDesc.Text;

			//CallBack.Invoke(OriginalName, ConfDirectoryTablePart, IsNewDirectoryTablePart);

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

		//	LoadFieldList();
		//}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			//FieldForm fieldForm = new FieldForm();
			//fieldForm.CallBack = CallBack_Update_Field;
			//fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			//fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectoryTablePart.Table, ConfDirectoryTablePart.Fields);
			//fieldForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, string> configurationObjectView in ConfView.Fields)
			{
				listBoxFields.Items.Add(configurationObjectView.Key + " -> " + configurationObjectView.Value);
			}
		}

		void LoadAllFieldList()
		{
			listBoxAllFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectory.Fields)
			{
				listBoxAllFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			//if (listBoxFields.SelectedItem != null)
			//{
			//	FieldForm fieldForm = new FieldForm();
			//	fieldForm.configurationObjectField = ConfDirectoryTablePart.Fields[listBoxFields.SelectedItem.ToString()];
			//	fieldForm.CallBack = CallBack_Update_Field;
			//	fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			//	fieldForm.Show();
			//}
		}

		private void listBoxAllFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxAllFields.SelectedItem != null)
			{
				ConfView.Fields.Add(
					ConfDirectory.Fields[listBoxAllFields.SelectedItem.ToString()].Name,
					ConfDirectory.Fields[listBoxAllFields.SelectedItem.ToString()].NameInTable);

				LoadFieldList();
			}
		}
	}
}