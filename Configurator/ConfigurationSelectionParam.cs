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
				}
				else
					new Exception("ItemConfigurationParam null");
			}

			textBoxPostgreSQLServer.Text = ItemConfigurationParam.DataBaseServer;
			textBoxPostgreSQLLogin.Text = ItemConfigurationParam.DataBaseLogin;
			textBoxPostgreSQLPassword.Text = ItemConfigurationParam.DataBasePassword;
			textBoxPostgreSQLPort.Text = ItemConfigurationParam.DataBasePort.ToString();
			textBoxPostgreSQLDataBase.Text = ItemConfigurationParam.DataBaseBaseName;
		}

		private void UpdateItemConfigurationParam()
		{
			ItemConfigurationParam.ConfigurationName = textBoxConfName.Text;
			ItemConfigurationParam.ConfigurationPath = textBoxPathToFile.Text;

			ItemConfigurationParam.DataBaseServer = textBoxPostgreSQLServer.Text;
			ItemConfigurationParam.DataBaseLogin = textBoxPostgreSQLLogin.Text;
			ItemConfigurationParam.DataBasePassword = textBoxPostgreSQLPassword.Text;
			ItemConfigurationParam.DataBasePort = int.Parse(textBoxPostgreSQLPort.Text);
			ItemConfigurationParam.DataBaseBaseName = textBoxPostgreSQLDataBase.Text;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			UpdateItemConfigurationParam();

			CallBack_Update(ItemConfigurationParam, IsNew);

			this.Close();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			Exception exception = null;

			UpdateItemConfigurationParam();

			Program.Kernel = new Kernel();

			bool flag =  Program.Kernel.CreateDatabaseIfNotExist(
				ItemConfigurationParam.DataBaseServer,
				ItemConfigurationParam.DataBaseLogin,
				ItemConfigurationParam.DataBasePassword,
				ItemConfigurationParam.DataBasePort,
				ItemConfigurationParam.DataBaseBaseName, out exception);

			MessageBox.Show(flag.ToString());
		}

		private void buttonTryConnect_Click(object sender, EventArgs e)
		{
			Exception exception = null;

			UpdateItemConfigurationParam();

			Program.Kernel = new Kernel();

			bool flag = Program.Kernel.TryConnectToServer(
				ItemConfigurationParam.DataBaseServer,
				ItemConfigurationParam.DataBaseLogin,
				ItemConfigurationParam.DataBasePassword,
				ItemConfigurationParam.DataBasePort, out exception);

			MessageBox.Show(flag.ToString());
		}
	}
}
