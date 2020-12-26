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
	public partial class ConfigurationSelectionParam : Form
	{
		public ConfigurationSelectionParam()
		{
			InitializeComponent();
		}

		public ConfigurationParam ItemConfigurationParam { get; set; }


		private void ConfigurationSelectionParam_Load(object sender, EventArgs e)
		{
			if (ItemConfigurationParam != null)
            {
				textBoxConfName.Text = ItemConfigurationParam.ConfigurationName;
				textBoxPathToFile.Text = ItemConfigurationParam.ConfigurationPath;

				textBoxPostgreSQLServer.Text = ItemConfigurationParam.DataBaseServer;
				textBoxPostgreSQLLogin.Text = ItemConfigurationParam.DataBaseLogin;
				textBoxPostgreSQLPassword.Text = ItemConfigurationParam.DataBasePassword;
				textBoxPostgreSQLDataBase.Text = ItemConfigurationParam.DataBaseBaseName;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{


			this.Close();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{

			this.Close();
		}
	}
}
