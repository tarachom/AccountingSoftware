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

		public void S()
		{
			Configuration Conf = Program.Kernel.Conf;

			//Save
			Configuration.Save(Conf.PathToXmlFileConfiguration, Conf);

			//Comparison
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema("ConfTradeTest");
			Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

			//Code Generation
			Configuration.Generation(Conf.PathToXmlFileConfiguration,
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.cs");

			//Аналіз таблиць і полів конфігурації та бази даних
			Configuration.ComparisonGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");

			//Create SQL
			Configuration.ComparisonGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReportAnalize.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

			//Read SQL
			List<string> SqlList = Configuration.ListComparisonSql(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

			//Execute
			foreach (string sqlText in SqlList)
			{
				Program.Kernel.DataBase.ExecuteSQL(sqlText);
			}

		}

		public void LoadTree()
		{
			Configuration Conf = Program.Kernel.Conf;

			treeConfiguration.Nodes.Clear();

			TreeNode rootNode = treeConfiguration.Nodes.Add("root", "Конфігурація");
			rootNode.ImageIndex = 1;

			TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
			directoriesNode.ImageIndex = 1;

			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
			{
				TreeNode directoryNode = directoriesNode.Nodes.Add(ConfDirectory.Key, ConfDirectory.Value.Name);
				directoryNode.Tag = "Directory=" + ConfDirectory.Key;
				directoryNode.ContextMenuStrip = contextMenuStrip1;
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

					//directoriTablePartNode.Expand();
				}

				TreeNode directoriViewsNode = directoryNode.Nodes.Add("Views", "Візуалізації");
				directoriViewsNode.ImageIndex = 1;

				foreach (KeyValuePair<string, ConfigurationObjectView> ConfView in ConfDirectory.Value.Views)
				{
					TreeNode directoriViewNode = directoriViewsNode.Nodes.Add(ConfView.Key, ConfView.Value.Name);
					directoriViewNode.ImageIndex = 1;

					//Поля
					foreach (KeyValuePair<string, string> ConfViewField in ConfView.Value.Fields)
					{
						directoriViewNode.Nodes.Add(ConfViewField.Key, ConfViewField.Key).ImageIndex = 1;
					}

					//directoriTablePartNode.Expand();
				}

				//directoriTabularPartsNode.Expand();
				directoryNode.Expand();
			}

			rootNode.Expand();
			directoriesNode.Expand();
		}

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			Program.Kernel = new Kernel();
			Program.Kernel.Open();

			Configuration Conf = Program.Kernel.Conf;

			LoadTree();

			#region test

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

			#endregion

			//-------------------------------------------------------

			//dataConfiguration.Columns.Add("Name", "NAME");
			//dataConfiguration.Columns.Add("Code", "CODE");

			//DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
			//cbc.Name = "List";
			//cbc.FlatStyle = FlatStyle.Flat;
			//cbc.Items.Add("10");
			//cbc.Items.Add("30");
			//cbc.Items.Add("80");
			//cbc.Items.Add("100");
			//dataConfiguration.Columns.Add(cbc);

			//DataGridViewCheckBoxColumn cbbc = new DataGridViewCheckBoxColumn(false);
			//cbbc.Name = "Check";
			//dataConfiguration.Columns.Add(cbbc);

			//DataGridViewTextBoxColumn tbc = new DataGridViewTextBoxColumn();
			//tbc.Name = "Text";
			//dataConfiguration.Columns.Add(tbc);

			//for (int i = 0; i < 5; i++)
			//	dataConfiguration.Rows.Add(new object[] { "10", "32", "30", true, "" });
		}

		private void treeConfiguration_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag != null)
				MessageBox.Show(e.Node.Tag.ToString());
		}

		private void FormConfiguration_FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.Kernel.Close();
		}

		private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DirectoryForm directoryForm = new DirectoryForm();
			directoryForm.formConfiguration = this;

			directoryForm.Show();
		}

		private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			

			if (nodeSel != null)
				MessageBox.Show(nodeSel.Name);
			//if (e.Node.Tag != null)
			//	MessageBox.Show(e.Node.Tag.ToString());
		}

		private void addNewDirectiryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addDirectoryToolStripMenuItem_Click(sender, e);
		}

		private void copyDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void deleteDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private TreeNode nodeSel { get; set; }

		private void treeConfiguration_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			nodeSel = e.Node;
		}

		
	}
}
