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
		public Func<string, Boolean> CallBack_IsExistTablePartName { get; set; }

		public ConfigurationObjectTablePart ConfDirectoryTablePart { get; set; }
		public bool IsNewDirectoryTablePart { get; set; }
		public string OriginalName { get; set; }

		private void TablePartForm_Load(object sender, EventArgs e)
		{
			if (ConfDirectoryTablePart == null)
			{
				ConfDirectoryTablePart = new ConfigurationObjectTablePart();

				textBoxTable.Text = Configuration.GetNewUnigueTableName(Program.Kernel);

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
			string name = textBoxName.Text;
			string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);
			textBoxName.Text = name;

			if (errorList.Length > 0)
			{
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsNewDirectoryTablePart || OriginalName != name)
				if (CallBack_IsExistTablePartName(name))
				{
					MessageBox.Show("Назва табличної частини не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

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

		bool CallBack_IsExistFieldName(string name)
		{
			return ConfDirectoryTablePart.Fields.ContainsKey(name);
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
			fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
			fieldForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, ConfDirectoryTablePart.Table, ConfDirectoryTablePart.Fields);
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
				fieldForm.CallBack_IsExistFieldName = CallBack_IsExistFieldName;
				fieldForm.Show();
			}
		}

		private void listBoxFields_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				if (e.KeyData == Keys.Delete)
				{
					string question = "Видалити поле";

					if (MessageBox.Show(question + " " + listBoxFields.SelectedItem.ToString() + "?", question + "?", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						int selectIndex = listBoxFields.SelectedIndex;

						ConfDirectoryTablePart.Fields.Remove(listBoxFields.SelectedItem.ToString());
						LoadFieldList();

						if (selectIndex >= listBoxFields.Items.Count)
							selectIndex = listBoxFields.Items.Count - 1;

						listBoxFields.SelectedIndex = selectIndex;
					}
				}
			}
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