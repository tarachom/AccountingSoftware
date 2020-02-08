using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator
{
	public partial class FieldForm : Form
	{
		public FieldForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			foreach (FieldType fieldType in FieldType.DefaultList())
				comboBoxFieldType.Items.Add(fieldType);

			comboBoxFieldType.SelectedItem = comboBoxFieldType.Items[0];
		}
		
		private void buttonSave_Click(object sender, EventArgs e)
		{


			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
