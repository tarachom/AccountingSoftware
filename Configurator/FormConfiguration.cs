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

		public Configuration Conf { get; set; }

		public void SaveConf()
		{
			//Save
			Configuration.Save(Conf.PathToXmlFileConfiguration, Conf);

			//Comparison
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
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
			treeConfiguration.Nodes.Clear();

			TreeNode rootNode = treeConfiguration.Nodes.Add("root", "Конфігурація");
			rootNode.SelectedImageIndex = 2; 
			rootNode.ImageIndex = 2;

			TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
			directoriesNode.SelectedImageIndex = 3;
			directoriesNode.ImageIndex = 3;

			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
			{
				TreeNode directoryNode = directoriesNode.Nodes.Add(ConfDirectory.Key, ConfDirectory.Value.Name);
				directoryNode.Tag = "Directory=" + ConfDirectory.Key;
				directoryNode.ContextMenuStrip = contextMenuStrip1;
				directoryNode.SelectedImageIndex = 1;
				directoryNode.ImageIndex = 1;

				//Поля
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDirectory.Value.Fields)
				{
					TreeNode fieldNode = directoryNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + 
						((ConfFields.Value.Type == "pointer") ? " -> " + ConfFields.Value.Pointer : ""));

					fieldNode.SelectedImageIndex = 0;
					fieldNode.ImageIndex = 0;
				}

				TreeNode directoriTabularPartsNode = directoryNode.Nodes.Add("TabularParts", "Табличні частини");
				directoriTabularPartsNode.SelectedImageIndex = 1;
				directoriTabularPartsNode.ImageIndex = 1;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in ConfDirectory.Value.TabularParts)
				{
					TreeNode directoriTablePartNode = directoriTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					directoriTablePartNode.ImageIndex = 1;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						TreeNode fieldNode = directoriTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name +
							((ConfTablePartFields.Value.Type == "pointer") ? " -> " + ConfTablePartFields.Value.Pointer : ""));

						fieldNode.SelectedImageIndex = 0;
						fieldNode.ImageIndex = 0;
					}

					//directoriTablePartNode.Expand();
				}

				TreeNode directoriViewsNode = directoryNode.Nodes.Add("Views", "Візуалізації");
				directoriViewsNode.SelectedImageIndex = 1;
				directoriViewsNode.ImageIndex = 1;

				foreach (KeyValuePair<string, ConfigurationObjectView> ConfView in ConfDirectory.Value.Views)
				{
					TreeNode directoriViewNode = directoriViewsNode.Nodes.Add(ConfView.Key, ConfView.Value.Name);
					directoriViewNode.SelectedImageIndex = 1;
					directoriViewNode.ImageIndex = 1;

					//Поля
					foreach (KeyValuePair<string, string> ConfViewField in ConfView.Value.Fields)
					{
						TreeNode fieldNode = directoriViewNode.Nodes.Add(ConfViewField.Key, ConfViewField.Key);

						fieldNode.SelectedImageIndex = 0;
						fieldNode.ImageIndex = 0;
					}

					//directoriTablePartNode.Expand();
				}

				//directoriTabularPartsNode.Expand();
				//directoryNode.Expand();
			}

			rootNode.Expand();
			directoriesNode.Expand();
		}

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			Program.Kernel = new Kernel();
			Program.Kernel.Open();

			Conf = Program.Kernel.Conf;

			LoadTree();

			//Conf.Enums["Перелічення"].AppendField("Один", 1);
			//Conf.Enums["Перелічення"].AppendField("Два", 2);
			//Conf.Enums["Перелічення"].AppendField("Три", 3);
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

		bool CallBack_IsExistDirectoryName(string name)
		{
			return Conf.Directories.ContainsKey(name);
		}

		void CallBack_Update_Directory(string originalName, ConfigurationDirectories configurationDirectories, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendDirectory(configurationDirectories);
			}
			else
			{
				if (originalName != configurationDirectories.Name)
				{
					List<string> ListPointers = Conf.SearchForPointers(originalName);
					if (ListPointers.Count == 0)
					{
						Conf.Directories.Remove(originalName);
						Conf.AppendDirectory(configurationDirectories);
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() + " вказівники на довідник \"" + originalName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nПерейменувати неможливо";

						MessageBox.Show(textListPointer, "Знайденно " + ListPointers.Count.ToString() + " вказівники на довідник", MessageBoxButtons.OK, MessageBoxIcon.Error);

						configurationDirectories.Name = originalName;
						Conf.Directories[originalName] = configurationDirectories;
					}
				}
				else
				{
					Conf.Directories[originalName] = configurationDirectories;
				}
			}

			LoadTree();
		}

		private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DirectoryForm directoryForm = new DirectoryForm();
			directoryForm.CallBack = CallBack_Update_Directory;
			directoryForm.CallBack_IsExistDirectoryName = CallBack_IsExistDirectoryName;
			directoryForm.Show();
		}

		private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string directoryName = nodeSel.Name;

				Configuration Conf = Program.Kernel.Conf;

				DirectoryForm directoryForm = new DirectoryForm();
				directoryForm.ConfDirectory = Conf.Directories[directoryName];
				directoryForm.CallBack = CallBack_Update_Directory;
				directoryForm.CallBack_IsExistDirectoryName = CallBack_IsExistDirectoryName;
				directoryForm.Show();				
			}
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
			if (nodeSel != null)
			{
				string directoryName = nodeSel.Name;

				if (Conf.Directories.ContainsKey(directoryName))
				{
					List<string> ListPointers = Conf.SearchForPointers(directoryName);
					if (ListPointers.Count == 0)
					{
						Program.Kernel.DataBase.DeleteConfigurationDirectory(Conf.Directories[directoryName]);

						Conf.Directories.Remove(directoryName);
						LoadTree();
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() + " вказівники на довідник \"" + directoryName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nВидалитити неможливо";

						MessageBox.Show(textListPointer, "Знайденно " + ListPointers.Count.ToString() + " вказівники на довідник", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private TreeNode nodeSel { get; set; }

		private void treeConfiguration_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			nodeSel = e.Node;
		}

		private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveConf();
		}
	}
}
