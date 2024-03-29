﻿/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/


using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using AccountingSoftware;

namespace Configurator
{
	public partial class ConfigurationSelectionForm : Form
	{
		public ConfigurationSelectionForm()
		{
			InitializeComponent();

			ListConfigurationParam = new List<ConfigurationParam>();
		}

		/// <summary>
		/// Ключ конфігурації яку потрібно відкрити автоматично без вибору в списку.
		/// Ключ передається як параметр при запуску конфігуратора.
		/// </summary>
		public string AutoOpenConfigurationKey { get; set; }

		/// <summary>
		/// Шлях до списку баз баних
		/// </summary>
		private string PathToXML { get; set; }

		/// <summary>
		/// Шлях до конфігурації
		/// </summary>
		private string PathToConfXML { get; set; }

		/// <summary>
		/// Список баз даних
		/// </summary>
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

					string SelectAttribute = currentNode.GetAttribute("Select", "");
					if (!String.IsNullOrEmpty(SelectAttribute))
						ItemConfigurationParam.Select = bool.Parse(SelectAttribute);

					ItemConfigurationParam.ConfigurationKey = currentNode.SelectSingleNode("Key").Value;
					ItemConfigurationParam.ConfigurationName = currentNode.SelectSingleNode("Name").Value;
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

				XmlAttribute selectAttribute = xmlConfParamDocument.CreateAttribute("Select");
				selectAttribute.Value = ItemConfigurationParam.Select.ToString();
				configurationNode.Attributes.Append(selectAttribute);

				XmlElement nodeKey = xmlConfParamDocument.CreateElement("Key");
				nodeKey.InnerText = ItemConfigurationParam.ConfigurationKey;
				configurationNode.AppendChild(nodeKey);

				XmlElement nodeName = xmlConfParamDocument.CreateElement("Name");
				nodeName.InnerText = ItemConfigurationParam.ConfigurationName;
				configurationNode.AppendChild(nodeName);

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
			{
				listBoxConfiguration.Items.Add(ItemConfigurationParam);

				if (ItemConfigurationParam.Select)
					listBoxConfiguration.SelectedItem = ItemConfigurationParam;
			}

			if (listBoxConfiguration.SelectedIndex == -1 && listBoxConfiguration.Items.Count > 0)
				listBoxConfiguration.SelectedIndex = 0;
		}

		private void ConfigurationSelectionForm_Load(object sender, EventArgs e)
		{
			string assemblyLocation = Path.GetDirectoryName(Application.ExecutablePath);

			PathToXML = assemblyLocation + "\\ConfigurationParam.xml";
			PathToConfXML = assemblyLocation + "\\Confa.xml";

#if DEBUG
			//Конфігурація береться із папки Configurator
			//PathToConfXML = @"E:\Project\StorageAndTrade\StorageAndTrade\bin\Debug\Confa.xml";
#endif

			LoadConfigurationParamFromXML();

			if (!String.IsNullOrEmpty( AutoOpenConfigurationKey))
            {
				bool KeyFound = false;
				ConfigurationParam ItemConfigurationParamFound = new ConfigurationParam();

				foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
				{
					if (ItemConfigurationParam.ConfigurationKey == AutoOpenConfigurationKey)
                    {
						ItemConfigurationParamFound = ItemConfigurationParam;
						KeyFound = true;

						break;
					}
				}

				if (KeyFound)
				{
					Exception exception;
					bool flagOpen = OpenConfiguration(ItemConfigurationParamFound, out exception);

					if (flagOpen)
					{
						this.DialogResult = DialogResult.OK;
						this.Hide();

						return;
					}
					else
                    {
						MessageBox.Show("Помилка відкриття конфігурації: " + exception.Message);
                    }	
				}
				else
					MessageBox.Show("Помилка: Ключ конфігурації '" + AutoOpenConfigurationKey + "' не знайдено!");				
			}

			Fill_listBoxConfiguration();
		}

		private bool OpenConfiguration(ConfigurationParam itemConfigurationParam, out Exception exception )
        {
			Program.Kernel = new Kernel();

			bool flagOpen = Program.Kernel.Open(PathToConfXML,
				itemConfigurationParam.DataBaseServer,
				itemConfigurationParam.DataBaseLogin,
				itemConfigurationParam.DataBasePassword,
				itemConfigurationParam.DataBasePort,
				itemConfigurationParam.DataBaseBaseName,

				out exception);

			return flagOpen;
		}

        #region CallBack

		void CallBack_Update(ConfigurationParam itemConfigurationParam, bool isNew)
        {
			foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
				ItemConfigurationParam.Select = false;

			if (isNew)
			{
				itemConfigurationParam.Select = true;
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
						ItemConfigurationParam.DataBaseServer = itemConfigurationParam.DataBaseServer;
						ItemConfigurationParam.DataBaseLogin = itemConfigurationParam.DataBaseLogin;
						ItemConfigurationParam.DataBasePassword = itemConfigurationParam.DataBasePassword;
						ItemConfigurationParam.DataBaseBaseName = itemConfigurationParam.DataBaseBaseName;
						ItemConfigurationParam.DataBasePort = itemConfigurationParam.DataBasePort;
						ItemConfigurationParam.Select = true;

						SaveConfigurationParamFromXML();
						break;
					}
                }
			}

			//LoadConfigurationParamFromXML();
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
				if (MessageBox.Show("Видалити?", "Видалити?", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
		}

        private void buttonOpenConf_Click(object sender, EventArgs e)
        {
			if (listBoxConfiguration.SelectedItem != null)
			{
				ConfigurationParam itemConfigurationParam = (ConfigurationParam)listBoxConfiguration.SelectedItem;
				foreach (ConfigurationParam ItemConfigurationParam in ListConfigurationParam)
					ItemConfigurationParam.Select = ItemConfigurationParam.ConfigurationKey == itemConfigurationParam.ConfigurationKey;
				SaveConfigurationParamFromXML();

				Exception exception;
				bool flagOpen = OpenConfiguration(itemConfigurationParam, out exception);

				if (flagOpen)
				{
					this.DialogResult = DialogResult.OK;
					this.Hide();
				}
				else
				{
					MessageBox.Show(exception.Message);
				}
			}
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

        private void listBoxConfiguration_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
			{
				buttonOpenConf_Click(this, new EventArgs());
			}
		}
    }

    /// <summary>
    /// Клас для елементу списку баз даних
    /// </summary>
    public class ConfigurationParam
	{
		public ConfigurationParam()
		{
			DataBaseServer = "localhost";
			DataBaseLogin = "postgres";
			DataBasePort = 5432;
		}

		public string ConfigurationKey { get; set; }

		public string ConfigurationName { get; set; }

		public string DataBaseServer { get; set; }

		public int DataBasePort { get; set; }

		public string DataBaseLogin { get; set; }

		public string DataBasePassword { get; set; }

		public string DataBaseBaseName { get; set; }

		public bool Select { get; set; }

		public override string ToString()
		{
			return String.IsNullOrWhiteSpace(ConfigurationName) ? "<>" : ConfigurationName;
		}

		public ConfigurationParam Clone()
		{
			ConfigurationParam configurationParam = new ConfigurationParam();
			configurationParam.ConfigurationKey = Guid.NewGuid().ToString();
			configurationParam.ConfigurationName = ConfigurationName;
			configurationParam.DataBaseServer = DataBaseServer;
			configurationParam.DataBaseLogin = DataBaseLogin;
			configurationParam.DataBasePassword = DataBasePassword;
			configurationParam.DataBaseBaseName = DataBaseBaseName;
			configurationParam.DataBasePort = DataBasePort;

			return configurationParam;
		}
	}
}
