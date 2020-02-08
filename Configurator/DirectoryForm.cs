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

		public FormConfiguration formConfiguration { get; set; }

		public ConfigurationDirectories ConfDirectory { get; set; }

		public bool IsNewDirectory { get; set; }

		private void DirectoryForm_Load(object sender, EventArgs e)
		{
			if (ConfDirectory == null)
			{
				ConfDirectory = new ConfigurationDirectories();

				IsNewDirectory = true;
			}
			else
			{
				textBoxName.Text = ConfDirectory.Name;
				textBoxTable.Text = ConfDirectory.Table;
				textBoxDesc.Text = ConfDirectory.Desc;

				IsNewDirectory = false;

				LoadFieldList();
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			ConfDirectory.Name = textBoxName.Text;
			ConfDirectory.Table = textBoxTable.Text;
			ConfDirectory.Desc = textBoxDesc.Text;

			if (IsNewDirectory)
			{
				Program.Kernel.Conf.AppendDirectory(ConfDirectory);
			}
			
			formConfiguration.S();
			formConfiguration.LoadTree();

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		void CallBack_Update(ConfigurationObjectField configurationObjectField, bool isNew)
		{
			if (isNew)
			{
				ConfDirectory.AppendField(configurationObjectField);
			}

			LoadFieldList();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
		
			fieldForm.CallBack = CallBack_Update;

			fieldForm.Show();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectory.Fields)
			{
				listBoxFields.Items.Add(configurationObjectField.Value.Name);
			}
		}

		private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				FieldForm fieldForm = new FieldForm();
				fieldForm.configurationObjectField = ConfDirectory.Fields[listBoxFields.SelectedItem.ToString()];
				fieldForm.CallBack = CallBack_Update;

				fieldForm.Show();
			}
		}
	}
}