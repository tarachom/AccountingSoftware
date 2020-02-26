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
	public partial class DocumentForm : Form
	{
		public DocumentForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationDocuments, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistDocumentName { get; set; }

		public ConfigurationDocuments ConfDocument { get; set; }
		public string OriginalName { get; set; }
		public bool IsNewDocument { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDocument == null)
			{
				ConfDocument = new ConfigurationDocuments();
				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);

				//string newUnigueNameInTable_Name = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
				//ConfDocument.AppendField(new ConfigurationObjectField("Назва", newUnigueNameInTable_Name, "string", "", "Назва"));

				//string newUnigueNameInTable_Code = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
				//ConfDocument.AppendField(new ConfigurationObjectField("Код", newUnigueNameInTable_Code, "string", "", "Код"));

				IsNewDocument = true;

				LoadFieldList();
			}
			else
			{
				OriginalName = ConfDocument.Name;

				textBoxName.Text = ConfDocument.Name;
				textBoxTable.Text = ConfDocument.Table;
				textBoxDesc.Text = ConfDocument.Desc;

				IsNewDocument = false;

				LoadFieldList();
				LoadTabularPartsList();
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

			if (IsNewDocument || OriginalName != name)
				if (CallBack_IsExistDocumentName(name))
				{
					MessageBox.Show("Назва документу не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfDocument.Name = textBoxName.Text;
			ConfDocument.Table = textBoxTable.Text;
			ConfDocument.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfDocument, IsNewDocument);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
		
		bool CallBack_IsExistFieldName(string name)
		{
			return ConfDocument.Fields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationObjectField configurationObjectField, bool isNew)
		{
			if (isNew)
			{
				ConfDocument.AppendField(configurationObjectField);
			}
			else
			{
				if (originalName != configurationObjectField.Name)
				{
					ConfDocument.Fields.Remove(originalName);
					ConfDocument.AppendField(configurationObjectField);
				}
				else
				{
					ConfDocument.Fields[originalName] = configurationObjectField;
				}
			}

			LoadFieldList();
		}

		bool CallBack_IsExistTablePartName(string name)
		{
			return ConfDocument.TabularParts.ContainsKey(name);
		}

		void CallBack_Update_TablePart(string originalName, ConfigurationObjectTablePart configurationObjectTablePart, bool isNew)
		{
			if (isNew)
			{
				ConfDocument.AppendTablePart(configurationObjectTablePart);
			}
			else
			{
				if (originalName != configurationObjectTablePart.Name)
				{
					ConfDocument.TabularParts.Remove(originalName);
					ConfDocument.AppendTablePart(configurationObjectTablePart);
				}
				else
				{
					ConfDocument.TabularParts[originalName] = configurationObjectTablePart;
				}
			}

			LoadTabularPartsList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.CallBack = CallBack_Update_Field;
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDocument.Table, ConfDocument.Fields);
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

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDocument.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		void LoadTabularPartsList()
		{
			listBoxTabularParts.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> configurationObjectTablePart in ConfDocument.TabularParts)
			{
				listBoxTabularParts.Items.Add(configurationObjectTablePart.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.configurationObjectField = ConfDocument.Fields[listBoxFields.SelectedItem.ToString()];
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
				tablePartForm.ConfDirectoryTablePart = ConfDocument.TabularParts[listBoxTabularParts.SelectedItem.ToString()];
				tablePartForm.CallBack = CallBack_Update_TablePart;
				tablePartForm.CallBack_IsExistTablePartName = CallBack_IsExistTablePartName;

				tablePartForm.Show();
			}
		}

		private void listBoxFields_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				if (e.KeyData == Keys.Delete)
				{
					if (MessageBox.Show("Видалити поле " + listBoxFields.SelectedItem.ToString() + "?", "Видалити поле?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						int selectIndex = listBoxFields.SelectedIndex;

						ConfDocument.Fields.Remove(listBoxFields.SelectedItem.ToString());
						LoadFieldList();

						if (selectIndex >= listBoxFields.Items.Count)
							selectIndex = listBoxFields.Items.Count - 1;

						listBoxFields.SelectedIndex = selectIndex;
					}
				}
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

						ConfDocument.TabularParts.Remove(listBoxTabularParts.SelectedItem.ToString());
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