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
	public partial class DirectoryForm : Form
	{
		public DirectoryForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationDirectories, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistDirectoryName { get; set; }

		public ConfigurationDirectories ConfDirectory { get; set; }
		public string OriginalName { get; set; }
		public bool IsNewDirectory { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDirectory == null)
			{
				ConfDirectory = new ConfigurationDirectories();
				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);
				IsNewDirectory = true;
			}
			else
			{
				OriginalName = ConfDirectory.Name;

				textBoxName.Text = ConfDirectory.Name;
				textBoxTable.Text = ConfDirectory.Table;
				textBoxDesc.Text = ConfDirectory.Desc;

				IsNewDirectory = false;

				LoadFieldList();

				LoadTabularPartsList();

				LoadViewsList();
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

			if (IsNewDirectory || OriginalName != name)
				if (CallBack_IsExistDirectoryName(name))
				{
					MessageBox.Show("Назва довідника не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfDirectory.Name = textBoxName.Text;
			ConfDirectory.Table = textBoxTable.Text;
			ConfDirectory.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfDirectory, IsNewDirectory);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
		
		bool CallBack_IsExistFieldName(string name)
		{
			return ConfDirectory.Fields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew)
		{
			if (isNew)
			{
				ConfDirectory.AppendField(configurationObjectField);
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					ConfDirectory.Fields.Remove(originalName);
					ConfDirectory.AppendField(configurationObjectField);
				}
				else
				{
					ConfDirectory.Fields[originalName] = configurationObjectField;
				}
			}

			LoadFieldList();
		}

		bool CallBack_IsExistTablePartName(string name)
		{
			return ConfDirectory.TabularParts.ContainsKey(name);
		}

		void CallBack_Update_TablePart(string originalName, ConfigurationObjectTablePart configurationObjectTablePart, bool isNew)
		{
			if (isNew)
			{
				ConfDirectory.AppendTablePart(configurationObjectTablePart);
			}
			else
			{
				if (originalName != configurationObjectTablePart.Name)
				{
					ConfDirectory.TabularParts.Remove(originalName);
					ConfDirectory.AppendTablePart(configurationObjectTablePart);
				}
				else
				{
					ConfDirectory.TabularParts[originalName] = configurationObjectTablePart;
				}
			}

			LoadTabularPartsList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectory.Table, ConfDirectory.Fields);
			fieldForm.Show();
		}

		private void buttonAddTablePart_Click(object sender, EventArgs e)
		{
			TablePartForm tablePartForm = new TablePartForm();
			tablePartForm.CallBack = CallBack_Update_TablePart;
			tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;
			tablePartForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectory.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		void LoadTabularPartsList()
		{
			listBoxTabularParts.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in ConfDirectory.TabularParts)
			{
				listBoxTabularParts.Items.Add(configurationObjectTablePart.Value.Name);
			}
		}

		void LoadViewsList()
		{
			listBoxViews.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectView> configurationObjectView in ConfDirectory.Views)
			{
				listBoxViews.Items.Add(configurationObjectView.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.configurationObjectField = ConfDirectory.Fields[listBoxFields.SelectedItem.ToString()];
				fieldForm.CallBack = CallBack_Update_Field;
				fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
				fieldForm.Show();
			}
		}

		private void listBoxTabularParts_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxTabularParts.SelectedItem != null)
			{
				TablePartForm tablePartForm = new TablePartForm();
				tablePartForm.ConfDirectoryTablePart = ConfDirectory.TabularParts[listBoxTabularParts.SelectedItem.ToString()];
				tablePartForm.CallBack = CallBack_Update_TablePart;
				tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;

				tablePartForm.Show();
			}
		}

		private void listBoxViews_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxViews.SelectedItem != null)
			{
				ViewForm viewForm = new ViewForm();
				viewForm.ConfView = ConfDirectory.Views[listBoxViews.SelectedItem.ToString()];
				viewForm.ConfDirectory = ConfDirectory;
				//viewForm.CallBack = CallBack_Update_TablePart;
				//viewForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;

				viewForm.Show();
			}
		}

		private void buttonAddView_Click(object sender, EventArgs e)
		{
			ViewForm viewForm = new ViewForm();
			viewForm.ConfDirectory = ConfDirectory;
			viewForm.Show();
		}
	}
}