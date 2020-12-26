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

		public Action<ConfigurationParam, bool> CallBack_Update { get; set; }

		public bool IsNew { get; set; }

		public ConfigurationParam ItemConfigurationParam { get; set; }

		private void ConfigurationSelectionParam_Load(object sender, EventArgs e)
		{
			if (IsNew)
			{
				ItemConfigurationParam = new ConfigurationParam();
				ItemConfigurationParam.ConfigurationKey = Guid.NewGuid().ToString();
			}
			else
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
				else
					new Exception("ItemConfigurationParam null");
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			ItemConfigurationParam.ConfigurationName = textBoxConfName.Text;
			ItemConfigurationParam.ConfigurationPath = textBoxPathToFile.Text;

			ItemConfigurationParam.DataBaseServer = textBoxPostgreSQLServer.Text;
			ItemConfigurationParam.DataBaseLogin = textBoxPostgreSQLLogin.Text;
			ItemConfigurationParam.DataBasePassword = textBoxPostgreSQLPassword.Text;
			ItemConfigurationParam.DataBaseBaseName = textBoxPostgreSQLDataBase.Text;

			CallBack_Update(ItemConfigurationParam, IsNew);

			this.Close();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
