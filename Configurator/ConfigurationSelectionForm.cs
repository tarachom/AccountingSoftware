using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Configurator
{
	public partial class ConfigurationSelectionForm : Form
	{
		public ConfigurationSelectionForm()
		{
			InitializeComponent();

			ListConfigurationParam = new List<ConfigurationParam>();
			
		}

		private string PathToXML { get; set; }

		private List<ConfigurationParam> ListConfigurationParam { get; set; }

		private void LoadConfigurationParamFromXML()
        {
			ListConfigurationParam.Clear();

			if (File.Exists(PathToXML))
            {
				XPathDocument xPathDoc = new XPathDocument(PathToXML);
				XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

				XPathNodeIterator ConfigurationParamNodes = xPathDocNavigator.Select("/root/Configuration");
				while (ConfigurationParamNodes.MoveNext())
                {
					ConfigurationParam ItemConfigurationParam = new ConfigurationParam();

					XPathNavigator currentNode = ConfigurationParamNodes.Current;

					ItemConfigurationParam.ConfigurationName = currentNode.SelectSingleNode("Name").Value;
					ItemConfigurationParam.ConfigurationPath = currentNode.SelectSingleNode("Path").Value;
					ItemConfigurationParam.DataBaseServer = currentNode.SelectSingleNode("Server").Value;
					ItemConfigurationParam.DataBasePort = int.Parse(currentNode.SelectSingleNode("Port").Value);
					ItemConfigurationParam.DataBaseLogin = currentNode.SelectSingleNode("Login").Value;
					ItemConfigurationParam.DataBasePassword = currentNode.SelectSingleNode("Password").Value;
					ItemConfigurationParam.DataBaseBaseName = currentNode.SelectSingleNode("BaseName").Value;

					ListConfigurationParam.Add(ItemConfigurationParam);
				}
			}
        }

		private void SaveConfigurationParamFromXML()
        {
			XmlDocument xmlConfParamDocument = new XmlDocument();
			xmlConfParamDocument.AppendChild(xmlConfParamDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

			XmlElement rootNode = xmlConfParamDocument.CreateElement("root");
			xmlConfParamDocument.AppendChild(rootNode);

			foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
			{
				XmlElement configurationNode = xmlConfParamDocument.CreateElement("Configuration");
				rootNode.AppendChild(configurationNode);

				XmlElement nodeName = xmlConfParamDocument.CreateElement("Name");
				nodeName.InnerText = ItemConfigurationParam.ConfigurationName;
				configurationNode.AppendChild(nodeName);

				XmlElement nodePath = xmlConfParamDocument.CreateElement("Path");
				nodePath.InnerText = ItemConfigurationParam.ConfigurationPath;
				configurationNode.AppendChild(nodePath);

				XmlElement nodeServer = xmlConfParamDocument.CreateElement("Server");
				nodeServer.InnerText = ItemConfigurationParam.DataBaseServer;
				configurationNode.AppendChild(nodeServer);

				XmlElement nodePort = xmlConfParamDocument.CreateElement("Port");
				nodePort.InnerText = ItemConfigurationParam.DataBasePort.ToString();
				configurationNode.AppendChild(nodePort);

				XmlElement nodeLogin = xmlConfParamDocument.CreateElement("Login");
				nodeLogin.InnerText = ItemConfigurationParam.DataBaseLogin;
				configurationNode.AppendChild(nodeLogin);

				XmlElement nodePassword = xmlConfParamDocument.CreateElement("Password");
				nodePassword.InnerText = ItemConfigurationParam.DataBasePassword;
				configurationNode.AppendChild(nodePassword);

				XmlElement nodeBaseName = xmlConfParamDocument.CreateElement("BaseName");
				nodeBaseName.InnerText = ItemConfigurationParam.DataBaseBaseName;
				configurationNode.AppendChild(nodeBaseName);
			}

			xmlConfParamDocument.Save(PathToXML);
		}

		private void Fill_listBoxConfiguration()
		{
			listBoxConfiguration.Items.Clear();
			
			foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
				listBoxConfiguration.Items.Add(ItemConfigurationParam);
		}

		private void ConfigurationSelectionForm_Load(object sender, EventArgs e)
		{
			PathToXML = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ConfigurationParam.xml";

			ConfigurationParam itemConfigurationParam = new ConfigurationParam();
			itemConfigurationParam.ConfigurationName = "First";
			itemConfigurationParam.ConfigurationPath = @"E:\Project\AccountingSoftwareCloneVS\AccountingSoftware\ConfTrade\Configuration.xml";
			itemConfigurationParam.DataBaseServer = "localhost";
			itemConfigurationParam.DataBasePort = 5433;
			itemConfigurationParam.DataBaseLogin = "postgres";
			itemConfigurationParam.DataBasePassword = "1";
			itemConfigurationParam.DataBaseBaseName = "ConfTradeTest";

			ListConfigurationParam.Add(itemConfigurationParam);

			SaveConfigurationParamFromXML();

			LoadConfigurationParamFromXML();

			Fill_listBoxConfiguration();
		}

		private void listBoxConfiguration_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxConfiguration.SelectedItem != null)
			{


				//EnumFieldForm enumFieldForm = new EnumFieldForm();
				//enumFieldForm.Field = ConfEnums.Fields[listBoxFields.SelectedItem.ToString()];
				//enumFieldForm.CallBack_IsExistField = CallBack_IsExistField;
				//enumFieldForm.CallBack = CallBack_Update_Field;
				//enumFieldForm.Show();
			}
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

	class ConfigurationParam
	{
		public string ConfigurationName { get; set; }

		public string ConfigurationPath { get; set; }

		public string DataBaseServer { get; set; }

		public int DataBasePort { get; set; }

		public string DataBaseLogin { get; set; }

		public string DataBasePassword { get; set; }

		public string DataBaseBaseName { get; set; }

        public override string ToString()
        {
            return ConfigurationName;
        }
    }
}
