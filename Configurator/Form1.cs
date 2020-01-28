using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

			//string pathToConf = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";
			string pathToConfSave = @"D:\VS\Project\AccountingSoftware\ConfTrade\ConfigurationNew.xml";

			Conf = new Configuration();

			Configuration.Load(pathToConfSave, Conf);

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

					directoriTablePartNode.Expand();
				}

				directoriTabularPartsNode.Expand();
				directoryNode.Expand();
			}

			rootNode.Expand();
			directoriesNode.Expand();

			/*
			ConfigurationDirectories TmcDirectory = new ConfigurationDirectories();
			TmcDirectory.Name = "TMC3";
			TmcDirectory.Desc = "TMC 2";

			ConfigurationObjectField TmcDirectoryField1 = new ConfigurationObjectField();
			TmcDirectoryField1.Name = "Code";
			TmcDirectoryField1.Type = "string";
			TmcDirectoryField1.Desc = "Code";

			ConfigurationObjectField TmcDirectoryField2 = new ConfigurationObjectField();
			TmcDirectoryField2.Name = "Name";
			TmcDirectoryField2.Type = "string";
			TmcDirectoryField2.Desc = "Name";

			TmcDirectory.Fields.Add(TmcDirectoryField1.Name, TmcDirectoryField1);
			TmcDirectory.Fields.Add(TmcDirectoryField2.Name, TmcDirectoryField2);
			*/


			//ConfigurationDirectories tmc6 = Conf.AppendDirectory(new ConfigurationDirectories("TMC6", "Desc"));
			//ConfigurationObjectField fieldName = tmc6.AppendField(new ConfigurationObjectField("Name", "Desc", "string"));
			//tmc6.AppendField(new ConfigurationObjectField("Code", "Desc", "string"));


			//Conf.Directories["TMC6"].TabularParts.Add("Od", new ConfigurationObjectTablePart("Od"));
			//Conf.Directories["TMC6"].TabularParts["Od"].Fields.Add("Name", new ConfigurationObjectField("Name"));

			//Conf.Directories["Tovary"].TabularParts.Add("Od", new ConfigurationObjectTablePart("Od"));
			//Conf.Directories["Tovary"].TabularParts["Od"].Fields.Add("Name", new ConfigurationObjectField("Name"));

			//Save
			Configuration.Save(pathToConfSave, Conf);

			Configuration.Generation(pathToConfSave,
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.cs");

			//-------------------------------------------------------

			dataConfiguration.Columns.Add("Name", "NAME");
			dataConfiguration.Columns.Add("Code", "CODE");

			DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
			cbc.Name = "List";
			cbc.FlatStyle = FlatStyle.Flat;
			cbc.Items.Add("10");
			cbc.Items.Add("30");
			cbc.Items.Add("80");
			cbc.Items.Add("100");
			dataConfiguration.Columns.Add(cbc);

			DataGridViewCheckBoxColumn cbbc = new DataGridViewCheckBoxColumn(false);
			cbbc.Name = "Check";
			dataConfiguration.Columns.Add(cbbc);

			DataGridViewTextBoxColumn tbc = new DataGridViewTextBoxColumn();
			tbc.Name = "Text";
			dataConfiguration.Columns.Add(tbc);

			for (int i = 0; i < 5; i++)
				dataConfiguration.Rows.Add(new object[] { "10", "32", "30", true, "" });
		}

		private void treeConfiguration_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{			
			
		}
	}
}
