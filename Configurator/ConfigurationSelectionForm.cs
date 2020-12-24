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
	public partial class ConfigurationSelectionForm : Form
	{
		public ConfigurationSelectionForm()
		{
			InitializeComponent();
		}

		private void ConfigurationSelectionForm_Load(object sender, EventArgs e)
		{


		}

		private void listBoxConfiguration_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private void buttonAddConf_Click(object sender, EventArgs e)
		{
			ConfigurationSelectionParam configurationSelectionParamForm = new ConfigurationSelectionParam();
			configurationSelectionParamForm.ShowDialog();
		}

		private void buttonEditConf_Click(object sender, EventArgs e)
		{

		}
	}
}
