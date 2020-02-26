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
			textBoxName.Text = name;

			if (errorList.Length > 0)
			{
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsNew || OriginalName != name)
				if (CallBack_IsExistEnums(name))
				{
					MessageBox.Show("Назва перелічення не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		bool CallBack_IsExistField(string name)
		{
			return ConfEnums.Fields.ContainsKey(name);
		}

		void CallBack_Update_Field(string originalName, ConfigurationEnumField configurationEnumField, bool isNew)
		{
			if (isNew)
			{
				ConfEnums.AppendField(configurationEnumField);
			}
			else
			{
				if (originalName != configurationEnumField.Name)
				{
					ConfEnums.Fields.Remove(originalName);
					ConfEnums.AppendField(configurationEnumField);
				}
				else
				{
					ConfEnums.Fields[originalName] = configurationEnumField;
				}
			}

			LoadFieldList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			ConfEnums.SerialNumber += 1;

			EnumFieldForm enumFieldForm = new EnumFieldForm();
			enumFieldForm.SerialNumber = ConfEnums.SerialNumber;
			enumFieldForm.CallBack_IsExistField = CallBack_IsExistField;
			enumFieldForm.CallBack = CallBack_Update_Field;
			enumFieldForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationEnumField> configurationObjectField in ConfEnums.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Key);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				EnumFieldForm enumFieldForm = new EnumFieldForm();
				enumFieldForm.Field = ConfEnums.Fields[listBoxFields.SelectedItem.ToString()];
				enumFieldForm.CallBack_IsExistField = CallBack_IsExistField;
				enumFieldForm.CallBack = CallBack_Update_Field;
				enumFieldForm.Show();
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

						ConfEnums.Fields.Remove(listBoxFields.SelectedItem.ToString());
						LoadFieldList();

						if (selectIndex >= listBoxFields.Items.Count)
							selectIndex = listBoxFields.Items.Count - 1;

						listBoxFields.SelectedIndex = selectIndex;
					}
				}
			}
		}

		private void EnumForm_KeyDown(object sender, KeyEventArgs e)
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