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
	public partial class EnumFieldForm : Form
	{
		public Action<string, ConfigurationEnumField, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistField { get; set; }

		public ConfigurationEnumField Field;

		public string OriginalName { get; set; }
		public bool IsNew { get; set; }

		public EnumFieldForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			if (Field == null)
			{
				Field = new ConfigurationEnumField();
				IsNew = true;
			}
			else
			{
				OriginalName = Field.Name;

				textBoxName.Text = Field.Name;
				textBoxNameValue.Text = Field.Value.ToString();
				textBoxDesc.Text = Field.Desc;

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
				if (CallBack_IsExistField(name))
				{
					MessageBox.Show("Назва поля не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			Field.Name = textBoxName.Text;
			Field.Value = int.Parse(textBoxNameValue.Text);
			Field.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, Field, IsNew);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}