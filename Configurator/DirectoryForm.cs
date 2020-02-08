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
				//...

				IsNewDirectory = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			//...

			if (IsNewDirectory)
			{
				ConfDirectory.Name = textBoxName.Text;
				ConfDirectory.Table = textBoxTable.Text;
				ConfDirectory.Desc = textBoxDesc.Text;

				Program.Kernel.Conf.AppendDirectory(ConfDirectory);

				formConfiguration.S();
				formConfiguration.LoadTree();
			}

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			FieldForm fieldForm = new FieldForm();
			fieldForm.Show();
		}
	}
}