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

					ItemConfigurationParam.ConfigurationKey = currentNode.SelectSingleNode("Key").Value;
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

				XmlElement nodeKey = xmlConfParamDocument.CreateElement("Key");
				nodeKey.InnerText = ItemConfigurationParam.ConfigurationKey;
				configurationNode.AppendChild(nodeKey);

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

            LoadConfigurationParamFromXML();

			Fill_listBoxConfiguration();
		}

        #region CallBack

		void CallBack_Update(ConfigurationParam itemConfigurationParam, bool isNew)
        {
			if (isNew)
			{
				ListConfigurationParam.Add(itemConfigurationParam);
				SaveConfigurationParamFromXML();
			}
			else
			{
				foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
                {
					if (ItemConfigurationParam.ConfigurationKey == itemConfigurationParam.ConfigurationKey)
					{
						ItemConfigurationParam.ConfigurationName = itemConfigurationParam.ConfigurationName;
						ItemConfigurationParam.ConfigurationPath = itemConfigurationParam.ConfigurationPath;
						ItemConfigurationParam.DataBaseServer = itemConfigurationParam.DataBaseServer;
						ItemConfigurationParam.DataBaseLogin = itemConfigurationParam.DataBaseLogin;
						ItemConfigurationParam.DataBasePassword = itemConfigurationParam.DataBasePassword;
						ItemConfigurationParam.DataBaseBaseName = itemConfigurationParam.DataBaseBaseName;
						ItemConfigurationParam.DataBasePort = itemConfigurationParam.DataBasePort;

						SaveConfigurationParamFromXML();
						break;
					}
                }
			}

			LoadConfigurationParamFromXML();
			Fill_listBoxConfiguration();
		}

		#endregion

		private void listBoxConfiguration_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxConfiguration.SelectedItem != null)
			{
				ConfigurationParam itemConfigurationParam = (ConfigurationParam)listBoxConfiguration.SelectedItem;

				ConfigurationSelectionParam configurationSelectionParamForm = new ConfigurationSelectionParam();
				configurationSelectionParamForm.IsNew = false;
				configurationSelectionParamForm.ItemConfigurationParam = itemConfigurationParam;
				configurationSelectionParamForm.CallBack_Update = CallBack_Update;
				configurationSelectionParamForm.ShowDialog();
			}
		}

		private void buttonAddConf_Click(object sender, EventArgs e)
		{
			ConfigurationSelectionParam configurationSelectionParamForm = new ConfigurationSelectionParam();
			configurationSelectionParamForm.IsNew = true;
			configurationSelectionParamForm.CallBack_Update = CallBack_Update;
			configurationSelectionParamForm.ShowDialog();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (listBoxConfiguration.SelectedItem != null)
			{
				ConfigurationParam itemConfigurationParam = (ConfigurationParam)listBoxConfiguration.SelectedItem;

				foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
				{
					if (ItemConfigurationParam.ConfigurationKey == itemConfigurationParam.ConfigurationKey)
					{
						ListConfigurationParam.Remove(ItemConfigurationParam);
						
						SaveConfigurationParamFromXML();
						Fill_listBoxConfiguration();

						break;
					}
				}
			}
		}

        private void buttonOpenConf_Click(object sender, EventArgs e)
        {

        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
			if (listBoxConfiguration.SelectedItem != null)
			{
				ConfigurationParam itemConfigurationParam = (ConfigurationParam)listBoxConfiguration.SelectedItem;

				ListConfigurationParam.Add(itemConfigurationParam.Clone());

				SaveConfigurationParamFromXML();
				Fill_listBoxConfiguration();
			}
		}
    }
}
