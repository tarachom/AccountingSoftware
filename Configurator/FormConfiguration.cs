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
					string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
						" -> " + ConfFields.Value.Pointer : "";

					TreeNode fieldNode = directoryNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
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
						string info = (ConfTablePartFields.Value.Type == "pointer" || ConfTablePartFields.Value.Type == "enum") ?
							" -> " + ConfTablePartFields.Value.Pointer : "";

						TreeNode fieldNode = directoriTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name + info);
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

			TreeNode documentsNode = rootNode.Nodes.Add("Documents", "Документи");
			documentsNode.SelectedImageIndex = 3;
			documentsNode.ImageIndex = 3;

			foreach (KeyValuePair<string, ConfigurationDocuments> ConfDocuments in Conf.Documents)
			{
				TreeNode documentNode = documentsNode.Nodes.Add(ConfDocuments.Key, ConfDocuments.Value.Name);
				//documentNode.ContextMenuStrip = contextMenuStrip1;
				documentNode.SelectedImageIndex = 1;
				documentNode.ImageIndex = 1;

				//Поля
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDocuments.Value.Fields)
				{
					string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
							" -> " + ConfFields.Value.Pointer : "";

					TreeNode fieldNode = documentNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 0;
					fieldNode.ImageIndex = 0;
				}

				TreeNode documentTabularPartsNode = documentNode.Nodes.Add("TabularParts", "Табличні частини");
				documentTabularPartsNode.SelectedImageIndex = 1;
				documentTabularPartsNode.ImageIndex = 1;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in ConfDocuments.Value.TabularParts)
				{
					TreeNode documentTablePartNode = documentTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					documentTablePartNode.ImageIndex = 1;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						string info = (ConfTablePartFields.Value.Type == "pointer" || ConfTablePartFields.Value.Type == "enum") ?
							" -> " + ConfTablePartFields.Value.Pointer : "";

						TreeNode fieldNode = documentTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name + info);
						fieldNode.SelectedImageIndex = 0;
						fieldNode.ImageIndex = 0;
					}
				}
			}

			TreeNode enumsNode = rootNode.Nodes.Add("Enums", "Перелічення");
			enumsNode.SelectedImageIndex = 3;
			enumsNode.ImageIndex = 3;

			foreach (KeyValuePair<string, ConfigurationEnums> ConfEnum in Conf.Enums)
			{
				TreeNode enumNode = enumsNode.Nodes.Add(ConfEnum.Key, ConfEnum.Value.Name);
				enumNode.ContextMenuStrip = contextMenuStrip2;
				enumNode.SelectedImageIndex = 1;
				enumNode.ImageIndex = 1;

				//Поля
				foreach (KeyValuePair<string, int> ConfEnumFields in ConfEnum.Value.Fields)
				{
					TreeNode enumFieldNode = enumNode.Nodes.Add(ConfEnumFields.Value.ToString(), ConfEnumFields.Key);

					enumFieldNode.SelectedImageIndex = 0;
					enumFieldNode.ImageIndex = 0;
				}
			}

			//TreeNode journalsNode = rootNode.Nodes.Add("Journals", "Журнали документів");
			//journalsNode.SelectedImageIndex = 3;
			//journalsNode.ImageIndex = 3;

			//...

			//TreeNode registersNode = rootNode.Nodes.Add("Registers", "Регістри");
			//registersNode.SelectedImageIndex = 3;
			//registersNode.ImageIndex = 3;

			//...

			rootNode.Expand();

			directoriesNode.Expand();
			enumsNode.Expand();
			documentsNode.Expand();
		}

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			Program.Kernel = new Kernel();
			Program.Kernel.Open();

			Conf = Program.Kernel.Conf;

			LoadTree();
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
					List<string> ListPointers = Conf.SearchForPointers("Довідники." + originalName);
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

		bool CallBack_IsExistEnumName(string name)
		{
			return Conf.Enums.ContainsKey(name);
		}

		void CallBack_Update_Enum(string originalName, ConfigurationEnums configurationEnum, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendEnum(configurationEnum);
			}
			else
			{
				if (originalName != configurationEnum.Name)
				{
					List<string> ListPointers = Conf.SearchForPointersEnum(originalName);
					if (ListPointers.Count == 0)
					{
						Conf.Enums.Remove(originalName);
						Conf.AppendEnum(configurationEnum);
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() +
							" вказівники на перелічення \"" + originalName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nПерейменувати неможливо";

						MessageBox.Show(textListPointer, "Знайденно " + ListPointers.Count.ToString() +
							" вказівники на перелічення", MessageBoxButtons.OK, MessageBoxIcon.Error);

						configurationEnum.Name = originalName;
						Conf.Enums[originalName] = configurationEnum;
					}
				}
				else
				{
					Conf.Enums[originalName] = configurationEnum;
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
					List<string> ListPointers = Conf.SearchForPointers("Довідники." + directoryName);
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

		private void openEnumItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string enumName = nodeSel.Name;

				Configuration Conf = Program.Kernel.Conf;

				EnumForm enumForm = new EnumForm();
				enumForm.ConfEnums = Conf.Enums[enumName];
				enumForm.CallBack = CallBack_Update_Enum;
				enumForm.CallBack_IsExistEnums = CallBack_IsExistEnumName;
				enumForm.Show();
			}
		}

		private void addEnumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EnumForm enumForm = new EnumForm();
			enumForm.CallBack = CallBack_Update_Enum;
			enumForm.CallBack_IsExistEnums = CallBack_IsExistEnumName;
			enumForm.Show();
		}
	}
}
