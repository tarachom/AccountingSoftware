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
	public partial class EnumForm : Form
	{
		public EnumForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationEnums, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistEnums { get; set; }

		public ConfigurationEnums ConfEnums { get; set; }
		public bool IsNew { get; set; }
		public string OriginalName { get; set; }

		private void TablePartForm_Load(object sender, EventArgs e)
		{
			if (ConfEnums == null)
			{
				ConfEnums = new ConfigurationEnums();
				IsNew = true;
			}
			else
			{
				OriginalName = ConfEnums.Name;

				textBoxName.Text = ConfEnums.Name;
				textBoxDesc.Text = ConfEnums.Desc;
				IsNew = false;

				LoadFieldList();
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

			ConfEnums.Name = textBoxName.Text;
			ConfEnums.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfEnums, IsNew);

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

			foreach (KeyValuePair<string, int> configurationObjectField in ConfEnums.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Key);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				//FieldForm fieldForm = new FieldForm();
				//fieldForm.configurationObjectField = ConfDirectoryTablePart.Fields[listBoxFields.SelectedItem.ToString()];
				//fieldForm.CallBack = CallBack_Update_Field;
				//fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
				//fieldForm.Show();
			}
		}
	}
}