using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml.XPath;
using AccountingSoftware;

namespace Configurator
{
	public partial class FormConfiguration : Form
	{
		public FormConfiguration()
		{
			InitializeComponent();
		}

		private Configuration Conf { get; set; }		

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			

			TreeNode rootNode = treeConfiguration.Nodes.Add("root", "Конфігурація");
			rootNode.ImageIndex = 1;

			string pathToConf = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";
			string pathToConfSave = @"D:\VS\Project\AccountingSoftware\ConfTrade\ConfigurationNew.xml";

			Conf = new Configuration();

			Configuration.Load(pathToConf, Conf);

			TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
			directoriesNode.ImageIndex = 1;

			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
			{
				TreeNode directoryNode = directoriesNode.Nodes.Add(ConfDirectory.Key, ConfDirectory.Value.Name);
				directoryNode.ImageIndex = 1;
				
				//Поля
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDirectory.Value.Fields) 
				{
					directoryNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name).ImageIndex = 1;
				}

				TreeNode directoriTabularPartsNode = directoryNode.Nodes.Add("TabularParts", "Табличні частини");
				directoriTabularPartsNode.ImageIndex = 1;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in ConfDirectory.Value.TabularParts) 
				{
					TreeNode directoriTablePartNode = directoriTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					directoriTablePartNode.ImageIndex = 1;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						directoriTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name).ImageIndex = 1;
					}
				}

				directoriNode.Expand();
			}

			rootNode.Expand();
			directoriesNode.Expand();

			//Save
			Configuration.Save(pathToConfSave, Conf);
		}

		private void treeConfiguration_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			MessageBox.Show(e.Node.Name);
		}
	}
}
