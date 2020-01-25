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
		
			string pathToConf = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";

			Conf = new Configuration();

			Configuration.Load(pathToConf, Conf);

			TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
			
			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectori in Conf.Directories)
			{
				TreeNode directoriNode = directoriesNode.Nodes.Add(ConfDirectori.Key, ConfDirectori.Value.Name);

				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDirectori.Value.Fields) 
				{
					directoriNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name);
				}
			}

			rootNode.Expand();
			directoriesNode.Expand();
		}

        
    }
}
