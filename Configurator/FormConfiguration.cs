/*
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
Сайт:     find.org.ua
*/

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

		//public void SaveConf()
		//{
		//	//Save
		//	Configuration.Save(Conf.PathToXmlFileConfiguration, Conf);

		//	//Comparison
		//	ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
		//	Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

		//	//Code Generation
		//	Configuration.Generation(Conf.PathToXmlFileConfiguration,
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.xslt",
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.cs");

		//	//Аналіз таблиць і полів конфігурації та бази даних
		//	Configuration.ComparisonGeneration(
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");

		//	//Create SQL
		//	Configuration.ComparisonGeneration(
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReportAnalize.xslt",
		//		@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

		//	//Read SQL
		//	List<string> SqlList = Configuration.ListComparisonSql(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

		//	//Execute
		//	foreach (string sqlText in SqlList)
		//	{
		//		Program.Kernel.DataBase.ExecuteSQL(sqlText);
		//	}

		//}

		public void LoadConstants(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationConstantsBlock> ConfConstantsBlock in Conf.ConstantsBlock)
			{
				TreeNode contantsBlockNode = rootNode.Nodes.Add(ConfConstantsBlock.Key, ConfConstantsBlock.Value.BlockName);
				contantsBlockNode.ContextMenuStrip = contextMenuStripConstantBlock;
				contantsBlockNode.SelectedImageIndex = 13;
				contantsBlockNode.ImageIndex = 13;

				foreach (KeyValuePair<string, ConfigurationConstants> ConfConstants in ConfConstantsBlock.Value.Constants)
				{
					TreeNode constantNode = contantsBlockNode.Nodes.Add(ConfConstants.Key, ConfConstants.Value.Name);
					constantNode.ContextMenuStrip = contextMenuStripConstatnt;
					constantNode.Tag = ConfConstants.Value;
					constantNode.SelectedImageIndex = 15;
					constantNode.ImageIndex = 15;
				}
			}
		}

		public void LoadDirectories(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
			{
				TreeNode directoryNode = rootNode.Nodes.Add(ConfDirectory.Key, ConfDirectory.Value.Name);
				directoryNode.Tag = "Directory=" + ConfDirectory.Key;
				directoryNode.ContextMenuStrip = contextMenuStripDirectory;
				directoryNode.SelectedImageIndex = 18;
				directoryNode.ImageIndex = 18;

				//Поля
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDirectory.Value.Fields)
				{
					string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
						" -> " + ConfFields.Value.Pointer : "";

					TreeNode fieldNode = directoryNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode directoriTabularPartsNode = directoryNode.Nodes.Add("TabularParts", "Табличні частини");
				directoriTabularPartsNode.SelectedImageIndex = 4;
				directoriTabularPartsNode.ImageIndex = 4;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in ConfDirectory.Value.TabularParts)
				{
					TreeNode directoriTablePartNode = directoriTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					directoriTablePartNode.SelectedImageIndex = 13;
					directoriTablePartNode.ImageIndex = 13;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						string info = (ConfTablePartFields.Value.Type == "pointer" || ConfTablePartFields.Value.Type == "enum") ?
							" -> " + ConfTablePartFields.Value.Pointer : "";

						TreeNode fieldNode = directoriTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name + info);
						fieldNode.SelectedImageIndex = 15;
						fieldNode.ImageIndex = 15;
					}
				}

				TreeNode directoriViewsNode = directoryNode.Nodes.Add("Views", "Візуалізації");
				directoriViewsNode.SelectedImageIndex = 8;
				directoriViewsNode.ImageIndex = 8;

				foreach (KeyValuePair<string, ConfigurationObjectView> ConfView in ConfDirectory.Value.Views)
				{
					TreeNode directoriViewNode = directoriViewsNode.Nodes.Add(ConfView.Key, ConfView.Value.Name);
					directoriViewNode.SelectedImageIndex = 9;
					directoriViewNode.ImageIndex = 9;

					//Поля
					foreach (KeyValuePair<string, string> ConfViewField in ConfView.Value.Fields)
					{
						TreeNode fieldNode = directoriViewNode.Nodes.Add(ConfViewField.Key, ConfViewField.Key);

						fieldNode.SelectedImageIndex = 15;
						fieldNode.ImageIndex = 15;
					}
				}
			}
		}

		public void LoadDocuments(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationDocuments> ConfDocuments in Conf.Documents)
			{
				TreeNode documentNode = rootNode.Nodes.Add(ConfDocuments.Key, ConfDocuments.Value.Name);
				documentNode.ContextMenuStrip = contextMenuStripDocument;
				documentNode.SelectedImageIndex = 1;
				documentNode.ImageIndex = 1;

				//Поля
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in ConfDocuments.Value.Fields)
				{
					string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
							" -> " + ConfFields.Value.Pointer : "";

					TreeNode fieldNode = documentNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode documentTabularPartsNode = documentNode.Nodes.Add("TabularParts", "Табличні частини");
				documentTabularPartsNode.SelectedImageIndex = 4;
				documentTabularPartsNode.ImageIndex = 4;

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
						fieldNode.SelectedImageIndex = 15;
						fieldNode.ImageIndex = 15;
					}
				}
			}
		}

		public void LoadEnums(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationEnums> ConfEnum in Conf.Enums)
			{
				TreeNode enumNode = rootNode.Nodes.Add(ConfEnum.Key, ConfEnum.Value.Name);
				enumNode.ContextMenuStrip = contextMenuStrip2;
				enumNode.SelectedImageIndex = 13;
				enumNode.ImageIndex = 13;

				//Поля
				foreach (KeyValuePair<string, ConfigurationEnumField> ConfEnumFields in ConfEnum.Value.Fields)
				{
					TreeNode enumFieldNode = enumNode.Nodes.Add(ConfEnumFields.Value.Value.ToString(), ConfEnumFields.Value.Name);

					enumFieldNode.SelectedImageIndex = 15;
					enumFieldNode.ImageIndex = 15;
				}
			}
		}

		public void LoadRegistersInformation(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationRegistersInformation> ConfRegistersInformation in Conf.RegistersInformation)
			{
				TreeNode registerInformationNode = rootNode.Nodes.Add(ConfRegistersInformation.Key, ConfRegistersInformation.Value.Name);
				registerInformationNode.ContextMenuStrip = contextMenuStripRegistersInformation;
				registerInformationNode.SelectedImageIndex = 13;
				registerInformationNode.ImageIndex = 13;

				TreeNode dimensionFieldsNode = registerInformationNode.Nodes.Add("DimensionFields", "Виміри");
				dimensionFieldsNode.SelectedImageIndex = 9;
				dimensionFieldsNode.ImageIndex = 9;

				//Поля вимірів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfDimensionFields in ConfRegistersInformation.Value.DimensionFields)
				{
					string info = (ConfDimensionFields.Value.Type == "pointer" || ConfDimensionFields.Value.Type == "enum") ?
						" -> " + ConfDimensionFields.Value.Pointer : "";

					TreeNode fieldNode = dimensionFieldsNode.Nodes.Add(ConfDimensionFields.Key, ConfDimensionFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode resourcesFieldsNode = registerInformationNode.Nodes.Add("ResourcesFields", "Ресурси");
				resourcesFieldsNode.SelectedImageIndex = 9;
				resourcesFieldsNode.ImageIndex = 9;

				//Поля ресурсів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfResourcesFields in ConfRegistersInformation.Value.ResourcesFields)
				{
					string info = (ConfResourcesFields.Value.Type == "pointer" || ConfResourcesFields.Value.Type == "enum") ?
						" -> " + ConfResourcesFields.Value.Pointer : "";

					TreeNode fieldNode = resourcesFieldsNode.Nodes.Add(ConfResourcesFields.Key, ConfResourcesFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode propertyFieldsNode = registerInformationNode.Nodes.Add("PropertyFields", "Поля");
				propertyFieldsNode.SelectedImageIndex = 9;
				propertyFieldsNode.ImageIndex = 9;

				//Поля реквізитів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfPropertyFields in ConfRegistersInformation.Value.PropertyFields)
				{
					string info = (ConfPropertyFields.Value.Type == "pointer" || ConfPropertyFields.Value.Type == "enum") ?
						" -> " + ConfPropertyFields.Value.Pointer : "";

					TreeNode fieldNode = propertyFieldsNode.Nodes.Add(ConfPropertyFields.Key, ConfPropertyFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}
			}
		}

		public void LoadRegistersAccumulation(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationRegistersAccumulation> ConfRegistersAccumulation in Conf.RegistersAccumulation)
			{
				TreeNode registerAccumulationNode = rootNode.Nodes.Add(ConfRegistersAccumulation.Key, ConfRegistersAccumulation.Value.Name);
				registerAccumulationNode.ContextMenuStrip = contextMenuStripRegistersAccumulation;
				registerAccumulationNode.SelectedImageIndex = 13;
				registerAccumulationNode.ImageIndex = 13;

				TreeNode dimensionFieldsNode = registerAccumulationNode.Nodes.Add("DimensionFields", "Виміри");
				dimensionFieldsNode.SelectedImageIndex = 9;
				dimensionFieldsNode.ImageIndex = 9;

				//Поля вимірів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfDimensionFields in ConfRegistersAccumulation.Value.DimensionFields)
				{
					string info = (ConfDimensionFields.Value.Type == "pointer" || ConfDimensionFields.Value.Type == "enum") ?
						" -> " + ConfDimensionFields.Value.Pointer : "";

					TreeNode fieldNode = dimensionFieldsNode.Nodes.Add(ConfDimensionFields.Key, ConfDimensionFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode resourcesFieldsNode = registerAccumulationNode.Nodes.Add("ResourcesFields", "Ресурси");
				resourcesFieldsNode.SelectedImageIndex = 9;
				resourcesFieldsNode.ImageIndex = 9;

				//Поля ресурсів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfResourcesFields in ConfRegistersAccumulation.Value.ResourcesFields)
				{
					string info = (ConfResourcesFields.Value.Type == "pointer" || ConfResourcesFields.Value.Type == "enum") ?
						" -> " + ConfResourcesFields.Value.Pointer : "";

					TreeNode fieldNode = resourcesFieldsNode.Nodes.Add(ConfResourcesFields.Key, ConfResourcesFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}

				TreeNode propertyFieldsNode = registerAccumulationNode.Nodes.Add("PropertyFields", "Поля");
				propertyFieldsNode.SelectedImageIndex = 9;
				propertyFieldsNode.ImageIndex = 9;

				//Поля реквізитів
				foreach (KeyValuePair<string, ConfigurationObjectField> ConfPropertyFields in ConfRegistersAccumulation.Value.PropertyFields)
				{
					string info = (ConfPropertyFields.Value.Type == "pointer" || ConfPropertyFields.Value.Type == "enum") ?
						" -> " + ConfPropertyFields.Value.Pointer : "";

					TreeNode fieldNode = propertyFieldsNode.Nodes.Add(ConfPropertyFields.Key, ConfPropertyFields.Value.Name + info);
					fieldNode.SelectedImageIndex = 15;
					fieldNode.ImageIndex = 15;
				}
			}
		}

		public void LoadTree()
		{
			treeConfiguration.Nodes.Clear();

			TreeNode rootNode = treeConfiguration.Nodes.Add("root", "Конфігурація");
			rootNode.SelectedImageIndex = 2;
			rootNode.ImageIndex = 2;

			TreeNode contantsNode = rootNode.Nodes.Add("Contants", "Константи");
			contantsNode.SelectedImageIndex = 3;
			contantsNode.ImageIndex = 3;

			LoadConstants(contantsNode);

			TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
			directoriesNode.SelectedImageIndex = 3;
			directoriesNode.ImageIndex = 3;

			LoadDirectories(directoriesNode);

			TreeNode documentsNode = rootNode.Nodes.Add("Documents", "Документи");
			documentsNode.SelectedImageIndex = 3;
			documentsNode.ImageIndex = 3;

			LoadDocuments(documentsNode);

			TreeNode enumsNode = rootNode.Nodes.Add("Enums", "Перелічення");
			enumsNode.SelectedImageIndex = 10;
			enumsNode.ImageIndex = 10;

			LoadEnums(enumsNode);

			TreeNode journalsNode = rootNode.Nodes.Add("Journals", "Журнали документів");
			journalsNode.SelectedImageIndex = 3;
			journalsNode.ImageIndex = 3;

			//...

			TreeNode registersInformationNode = rootNode.Nodes.Add("RegistersInformation", "Регістри відомостей");
			registersInformationNode.SelectedImageIndex = 3;
			registersInformationNode.ImageIndex = 3;

			LoadRegistersInformation(registersInformationNode);

			TreeNode registersAccumulationNode = rootNode.Nodes.Add("RegistersAccumulation", "Регістри накопичення");
			registersAccumulationNode.SelectedImageIndex = 3;
			registersAccumulationNode.ImageIndex = 3;

			LoadRegistersAccumulation(registersAccumulationNode);

			rootNode.Expand();
			contantsNode.Expand();
			directoriesNode.Expand();
			enumsNode.Expand();
			documentsNode.Expand();
			registersInformationNode.Expand();
			registersAccumulationNode.Expand();
		}

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			Program.Kernel = new Kernel();
			Program.Kernel.Open();

			Conf = Program.Kernel.Conf;

			LoadTree();
			
			DataGridViewRow dataGridViewRow = new DataGridViewRow();

			DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
			dataGridViewTextBoxCell.Value = "TExt";

			DataGridViewButtonCell dataGridViewButtonCell = new DataGridViewButtonCell();
			dataGridViewButtonCell.FlatStyle = FlatStyle.Flat;
			dataGridViewButtonCell.Value = "TExt";

			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewRow.Cells.Add(dataGridViewButtonCell);

			dataGridView1.Rows.Add(dataGridViewRow);


			ColumnHeader ch = listView1.Columns.Add("name", "Name", 150);
			ColumnHeader ch2 = listView1.Columns.Add("name2", "Name2", 150);

			for (int i = 0; i < 10; i++)
			{
				ListViewItem j = new ListViewItem(new string[] { "test", "test" }, 13);
				listView1.Items.Add(j);
			}

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

		bool CallBack_IsExistConstantsBlock(string name)
		{
			return Conf.ConstantsBlock.ContainsKey(name);
		}

		void CallBack_Update_ConstantsBlock(string originalName, ConfigurationConstantsBlock configurationConstantsBlock, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendConstantsBlock(configurationConstantsBlock);
			}
			else
			{
				if (originalName != configurationConstantsBlock.BlockName)
				{
					Conf.ConstantsBlock.Remove(originalName);
					Conf.AppendConstantsBlock(configurationConstantsBlock);
				}
				else
				{
					Conf.ConstantsBlock[originalName] = configurationConstantsBlock;
				}
			}

			LoadConstants(treeConfiguration.Nodes["root"].Nodes["Contants"]);
		}

		bool CallBack_IsExistConstants(string blockName, string name)
		{
			return Conf.ConstantsBlock[blockName].Constants.ContainsKey(name);
		}

		void CallBack_Update_Constants(string blockName, string originalName, ConfigurationConstants configurationConstants, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendConstants(blockName, configurationConstants);
			}
			else
			{
				if (blockName != configurationConstants.Block.BlockName || originalName != configurationConstants.Name)
				{
					Conf.ConstantsBlock[configurationConstants.Block.BlockName].Constants.Remove(originalName);
					Conf.AppendConstants(blockName, configurationConstants);
				}
				else
				{
					Conf.ConstantsBlock[blockName].Constants[originalName] = configurationConstants;
				}
			}

			LoadConstants(treeConfiguration.Nodes["root"].Nodes["Contants"]);
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

			LoadDirectories(treeConfiguration.Nodes["root"].Nodes["Directories"]);
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
					List<string> ListPointers = Conf.SearchForPointersEnum("Перелічення." + originalName);
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

			LoadEnums(treeConfiguration.Nodes["root"].Nodes["Enums"]);
		}

		bool CallBack_IsExistDocumentName(string name)
		{
			return Conf.Documents.ContainsKey(name);
		}

		void CallBack_Update_Document(string originalName, ConfigurationDocuments configurationDocuments, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendDocument(configurationDocuments);
			}
			else
			{
				if (originalName != configurationDocuments.Name)
				{
					List<string> ListPointers = Conf.SearchForPointers("Документи." + originalName);
					if (ListPointers.Count == 0)
					{
						Conf.Documents.Remove(originalName);
						Conf.AppendDocument(configurationDocuments);
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() +
							" вказівники на документ \"" + originalName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nПерейменувати неможливо";

						MessageBox.Show(textListPointer, "Знайденно вказівники", MessageBoxButtons.OK, MessageBoxIcon.Error);

						configurationDocuments.Name = originalName;
						Conf.Documents[originalName] = configurationDocuments;
					}
				}
				else
				{
					Conf.Documents[originalName] = configurationDocuments;
				}
			}

			LoadDocuments(treeConfiguration.Nodes["root"].Nodes["Documents"]);
		}

		bool CallBack_IsExistRegistersInformation(string name)
		{
			return Conf.RegistersInformation.ContainsKey(name);
		}

		void CallBack_Update_RegistersInformation(string originalName, ConfigurationRegistersInformation configurationRegistersInformation, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendRegistersInformation(configurationRegistersInformation);
			}
			else
			{
				if (originalName != configurationRegistersInformation.Name)
				{
					Conf.RegistersInformation.Remove(originalName);
					Conf.AppendRegistersInformation(configurationRegistersInformation);
				}
				else
				{
					Conf.RegistersInformation[originalName] = configurationRegistersInformation;
				}
			}

			LoadRegistersInformation(treeConfiguration.Nodes["root"].Nodes["RegistersInformation"]);
		}

		bool CallBack_IsExistRegistersAccumulation(string name)
		{
			return Conf.RegistersAccumulation.ContainsKey(name);
		}

		void CallBack_Update_RegistersAccumulation(string originalName, ConfigurationRegistersAccumulation configurationRegistersAccumulation, bool isNew)
		{
			if (isNew)
			{
				Conf.AppendRegistersAccumulation(configurationRegistersAccumulation);
			}
			else
			{
				if (originalName != configurationRegistersAccumulation.Name)
				{
					Conf.RegistersAccumulation.Remove(originalName);
					Conf.AppendRegistersAccumulation(configurationRegistersAccumulation);
				}
				else
				{
					Conf.RegistersAccumulation[originalName] = configurationRegistersAccumulation;
				}
			}

			LoadRegistersAccumulation(treeConfiguration.Nodes["root"].Nodes["RegistersAccumulation"]);
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
			SaveConfigurationForm saveConfigurationForm = new SaveConfigurationForm();
			saveConfigurationForm.Conf = Conf;
			saveConfigurationForm.ShowDialog();
		}

		private void openEnumItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string enumName = nodeSel.Name;

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

		private void addNewDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentForm documentForm = new DocumentForm();
			documentForm.CallBack = CallBack_Update_Document;
			documentForm.CallBack_IsExistDocumentName = CallBack_IsExistDocumentName;
			documentForm.Show();
		}

		private void openDocumentItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string documentName = nodeSel.Name;

				DocumentForm documentForm = new DocumentForm();
				documentForm.ConfDocument = Conf.Documents[documentName];
				documentForm.CallBack = CallBack_Update_Document;
				documentForm.CallBack_IsExistDocumentName = CallBack_IsExistDocumentName;
				documentForm.Show();
			}
		}

		private void addContantsBlockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ConstantsBlockForm constantsBlockForm = new ConstantsBlockForm();
			constantsBlockForm.CallBack_IsExistConstantsBlock = CallBack_IsExistConstantsBlock;
			constantsBlockForm.CallBack = CallBack_Update_ConstantsBlock;
			constantsBlockForm.Show();
		}

		private void addConstatntsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<string, ConfigurationObjectField> ConstantsAllFields = new Dictionary<string, ConfigurationObjectField>();
			foreach (ConfigurationConstantsBlock block in Conf.ConstantsBlock.Values)
			{
				foreach (ConfigurationConstants constants in block.Constants.Values) 
				{
					string fullName = block.BlockName + "." + constants.Name;
					ConstantsAllFields.Add(fullName, new ConfigurationObjectField(fullName, constants.NameInTable, constants.Type, constants.Pointer, constants.Desc));
				}
			}

			ConstantsForm constantsForm = new ConstantsForm();
			constantsForm.CallBack_IsExistConstants = CallBack_IsExistConstants;
			constantsForm.CallBack = CallBack_Update_Constants;
			constantsForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, "tab_constants", ConstantsAllFields);
			constantsForm.Show();
		}

		private void OpenConstantBlock_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string constantsBlockName = nodeSel.Name;

				ConstantsBlockForm constantsBlockForm = new ConstantsBlockForm();
				constantsBlockForm.ConstantsBlock = Conf.ConstantsBlock[constantsBlockName];
				constantsBlockForm.CallBack_IsExistConstantsBlock = CallBack_IsExistConstantsBlock;
				constantsBlockForm.CallBack = CallBack_Update_ConstantsBlock;
				constantsBlockForm.Show();
			}
		}

		private void openConstatnt_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				ConfigurationConstants configurationConstants = (ConfigurationConstants)nodeSel.Tag;

				ConstantsForm constantsForm = new ConstantsForm();
				constantsForm.Constants = configurationConstants;
				constantsForm.CallBack_IsExistConstants = CallBack_IsExistConstants;
				constantsForm.CallBack = CallBack_Update_Constants;
				constantsForm.Show();
			}
		}

		private void openItemRegistersInformation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registersInformationName = nodeSel.Name;

				RegistersInformationForm registersInformationForm = new RegistersInformationForm();
				registersInformationForm.ConfRegistersInformation = Conf.RegistersInformation[registersInformationName];
				registersInformationForm.CallBack = CallBack_Update_RegistersInformation;
				registersInformationForm.CallBack_IsExistRegistersInformation = CallBack_IsExistRegistersInformation;
				registersInformationForm.Show();
			}
		}

		private void addNewRegistersInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegistersInformationForm registersInformationForm = new RegistersInformationForm();
			registersInformationForm.CallBack = CallBack_Update_RegistersInformation;
			registersInformationForm.CallBack_IsExistRegistersInformation = CallBack_IsExistRegistersInformation;
			registersInformationForm.Show();
		}

		private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegistersAccumulationForm registersAccumulationForm = new RegistersAccumulationForm();
			registersAccumulationForm.CallBack = CallBack_Update_RegistersAccumulation;
			registersAccumulationForm.CallBack_IsExistRegistersAccumulation = CallBack_IsExistRegistersAccumulation;
			registersAccumulationForm.Show();
		}

		private void openItemRegistersAccumulation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registersAccumulationName = nodeSel.Name;

				RegistersAccumulationForm registersAccumulationForm = new RegistersAccumulationForm();
				registersAccumulationForm.ConfRegistersAccumulation = Conf.RegistersAccumulation[registersAccumulationName];
				registersAccumulationForm.CallBack = CallBack_Update_RegistersAccumulation;
				registersAccumulationForm.CallBack_IsExistRegistersAccumulation = CallBack_IsExistRegistersAccumulation;
				registersAccumulationForm.Show();
			}
		}
	}
}
