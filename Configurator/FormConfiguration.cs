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
Сайт:     accounting.org.ua
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

		private TreeNode nodeSel { get; set; }

		#region LoadTreeConfiguration

		public void LoadConstant(TreeNode rootNode, ConfigurationConstants confConstant)
		{
			TreeNode constantNode = rootNode.Nodes.Add(confConstant.Name, confConstant.Name);
			constantNode.ContextMenuStrip = contextMenuStripConstant;
			constantNode.Tag = confConstant;
			constantNode.SelectedImageIndex = 15;
			constantNode.ImageIndex = 15;
		}

		public void LoadConstants(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (KeyValuePair<string, ConfigurationConstantsBlock> ConfConstantsBlock in Conf.ConstantsBlock)
			{
				TreeNode contantsBlockNode = rootNode.Nodes.Add(ConfConstantsBlock.Key, ConfConstantsBlock.Value.BlockName);
				contantsBlockNode.ContextMenuStrip = contextMenuStripConstantBlock;
				contantsBlockNode.SelectedImageIndex = 13;
				contantsBlockNode.ImageIndex = 13;

				foreach (ConfigurationConstants ConfConstants in ConfConstantsBlock.Value.Constants.Values)
				{
					LoadConstant(contantsBlockNode, ConfConstants);
				}
			}
		}

		public void LoadDirectory(TreeNode rootNode, ConfigurationDirectories confDirectory)
		{
			TreeNode directoryNode = rootNode.Nodes.Add(confDirectory.Name, confDirectory.Name);
			directoryNode.Tag = "Directory=" + confDirectory.Name;
			directoryNode.ContextMenuStrip = contextMenuStripDirectory;
			directoryNode.SelectedImageIndex = 18;
			directoryNode.ImageIndex = 18;

			//Поля
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in confDirectory.Fields)
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

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in confDirectory.TabularParts)
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

			//TreeNode directoriViewsNode = directoryNode.Nodes.Add("Views", "Візуалізації");
			//directoriViewsNode.SelectedImageIndex = 8;
			//directoriViewsNode.ImageIndex = 8;

			//foreach (KeyValuePair<string, ConfigurationObjectView> ConfView in confDirectory.Views)
			//{
			//	TreeNode directoriViewNode = directoriViewsNode.Nodes.Add(ConfView.Key, ConfView.Value.Name);
			//	directoriViewNode.SelectedImageIndex = 9;
			//	directoriViewNode.ImageIndex = 9;

			//	//Поля
			//	foreach (KeyValuePair<string, string> ConfViewField in ConfView.Value.Fields)
			//	{
			//		TreeNode fieldNode = directoriViewNode.Nodes.Add(ConfViewField.Key, ConfViewField.Key);

			//		fieldNode.SelectedImageIndex = 15;
			//		fieldNode.ImageIndex = 15;
			//	}
			//}
		}

		public void LoadDirectories(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationDirectories ConfDirectory in Conf.Directories.Values)
			{
				LoadDirectory(rootNode, ConfDirectory);
			}
		}

		public void LoadDocument(TreeNode rootNode, ConfigurationDocuments confDocument)
		{
			TreeNode documentNode = rootNode.Nodes.Add(confDocument.Name, confDocument.Name);
			documentNode.ContextMenuStrip = contextMenuStripDocument;
			documentNode.SelectedImageIndex = 1;
			documentNode.ImageIndex = 1;

			//Поля
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in confDocument.Fields)
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

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in confDocument.TabularParts)
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

		public void LoadDocuments(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationDocuments ConfDocuments in Conf.Documents.Values)
			{
				LoadDocument(rootNode, ConfDocuments);
			}
		}

		public void LoadEnum(TreeNode rootNode, ConfigurationEnums confEnum)
		{
			TreeNode enumNode = rootNode.Nodes.Add(confEnum.Name, confEnum.Name);
			enumNode.ContextMenuStrip = contextMenuStripEnum;
			enumNode.SelectedImageIndex = 13;
			enumNode.ImageIndex = 13;

			//Поля
			foreach (KeyValuePair<string, ConfigurationEnumField> ConfEnumFields in confEnum.Fields)
			{
				TreeNode enumFieldNode = enumNode.Nodes.Add(ConfEnumFields.Value.Value.ToString(), ConfEnumFields.Value.Name);

				enumFieldNode.SelectedImageIndex = 15;
				enumFieldNode.ImageIndex = 15;
			}
		}

		public void LoadEnums(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationEnums ConfEnum in Conf.Enums.Values)
			{
				LoadEnum(rootNode, ConfEnum);
			}
		}

		public void LoadRegisterInformation(TreeNode rootNode, ConfigurationRegistersInformation confRegisterInformation)
		{
			TreeNode registerInformationNode = rootNode.Nodes.Add(confRegisterInformation.Name, confRegisterInformation.Name);
			registerInformationNode.ContextMenuStrip = contextMenuStripRegistersInformation;
			registerInformationNode.SelectedImageIndex = 13;
			registerInformationNode.ImageIndex = 13;

			TreeNode dimensionFieldsNode = registerInformationNode.Nodes.Add("DimensionFields", "Виміри");
			dimensionFieldsNode.SelectedImageIndex = 9;
			dimensionFieldsNode.ImageIndex = 9;

			//Поля вимірів
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfDimensionFields in confRegisterInformation.DimensionFields)
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
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfResourcesFields in confRegisterInformation.ResourcesFields)
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
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfPropertyFields in confRegisterInformation.PropertyFields)
			{
				string info = (ConfPropertyFields.Value.Type == "pointer" || ConfPropertyFields.Value.Type == "enum") ?
					" -> " + ConfPropertyFields.Value.Pointer : "";

				TreeNode fieldNode = propertyFieldsNode.Nodes.Add(ConfPropertyFields.Key, ConfPropertyFields.Value.Name + info);
				fieldNode.SelectedImageIndex = 15;
				fieldNode.ImageIndex = 15;
			}
		}

		public void LoadRegistersInformation(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationRegistersInformation ConfRegistersInformation in Conf.RegistersInformation.Values)
			{
				LoadRegisterInformation(rootNode, ConfRegistersInformation);
			}
		}

		public void LoadRegisterAccumulation(TreeNode rootNode, ConfigurationRegistersAccumulation confRegisterAccumulation)
		{
			TreeNode registerAccumulationNode = rootNode.Nodes.Add(confRegisterAccumulation.Name, confRegisterAccumulation.Name);
			registerAccumulationNode.ContextMenuStrip = contextMenuStripRegistersAccumulation;
			registerAccumulationNode.SelectedImageIndex = 13;
			registerAccumulationNode.ImageIndex = 13;

			TreeNode dimensionFieldsNode = registerAccumulationNode.Nodes.Add("DimensionFields", "Виміри");
			dimensionFieldsNode.SelectedImageIndex = 9;
			dimensionFieldsNode.ImageIndex = 9;

			//Поля вимірів
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfDimensionFields in confRegisterAccumulation.DimensionFields)
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
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfResourcesFields in confRegisterAccumulation.ResourcesFields)
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
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfPropertyFields in confRegisterAccumulation.PropertyFields)
			{
				string info = (ConfPropertyFields.Value.Type == "pointer" || ConfPropertyFields.Value.Type == "enum") ?
					" -> " + ConfPropertyFields.Value.Pointer : "";

				TreeNode fieldNode = propertyFieldsNode.Nodes.Add(ConfPropertyFields.Key, ConfPropertyFields.Value.Name + info);
				fieldNode.SelectedImageIndex = 15;
				fieldNode.ImageIndex = 15;
			}
		}

		public void LoadRegistersAccumulation(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();

			foreach (ConfigurationRegistersAccumulation ConfRegistersAccumulation in Conf.RegistersAccumulation.Values)
			{
				LoadRegisterAccumulation(rootNode, ConfRegistersAccumulation);
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

		public void LoadTreeAsync()
		{
			if (treeConfiguration.InvokeRequired)
			{
				treeConfiguration.Invoke(new Action(LoadTree));
			}
		}

		#endregion

		private void FormConfiguration_Load(object sender, EventArgs e)
		{
			ConfigurationSelectionForm configurationSelectionForm = new ConfigurationSelectionForm();
			DialogResult dialogResult = configurationSelectionForm.ShowDialog();

			if (dialogResult == DialogResult.OK)
			{
				Conf = Program.Kernel.Conf;

				Thread thread = new Thread(new ThreadStart(LoadTreeAsync));
				thread.Start();
			}
		}

		private void FormConfiguration_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Program.Kernel != null)
				Program.Kernel.Close();
		}

		private void treeConfiguration_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			nodeSel = e.Node;
		}

		#region CallBack

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

		#endregion

		#region Контекстне меню довідник

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

		private void copyDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string directoryName = nodeSel.Name;

				string directoryCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					directoryCopyName = directoryName + "_Копія_" + i.ToString();
					if (!Conf.Directories.ContainsKey(directoryCopyName))
						break;
				}

				ConfigurationDirectories confDirectoriesOriginal = Conf.Directories[directoryName];

				ConfigurationDirectories confDirectoriesCopy = new
					ConfigurationDirectories(directoryCopyName, Configuration.GetNewUnigueTableName(Program.Kernel),
					confDirectoriesOriginal.Desc);

				Conf.AppendDirectory(confDirectoriesCopy);

				foreach (ConfigurationObjectField confFieldOriginal in confDirectoriesOriginal.Fields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confDirectoriesCopy.Fields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectTablePart confTablePartOriginal in confDirectoriesOriginal.TabularParts.Values)
				{
					ConfigurationObjectTablePart confTablePartCopy = new
						ConfigurationObjectTablePart(confTablePartOriginal.Name, Configuration.GetNewUnigueTableName(Program.Kernel),
						confTablePartOriginal.Desc);

					confDirectoriesCopy.TabularParts.Add(confTablePartCopy.Name, confTablePartCopy);

					foreach (ConfigurationObjectField confTablePartFieldOriginal in confTablePartOriginal.Fields.Values)
					{
						ConfigurationObjectField confFieldCopy = new
							ConfigurationObjectField(confTablePartFieldOriginal.Name, confTablePartFieldOriginal.NameInTable,
							confTablePartFieldOriginal.Type, confTablePartFieldOriginal.Pointer, confTablePartFieldOriginal.Desc);

						confTablePartCopy.Fields.Add(confFieldCopy.Name, confFieldCopy);
					}
				}

				//foreach (ConfigurationObjectView confViewOriginal in confDirectoriesOriginal.Views.Values)
				//{
				//	ConfigurationObjectView confViewCopy = new ConfigurationObjectView(confViewOriginal.Name,
				//		confViewOriginal.Table, confViewOriginal.Desc);

				//	confDirectoriesCopy.Views.Add(confViewCopy.Name, confViewCopy);

				//	foreach (KeyValuePair<string, string> confViewField in confViewOriginal.Fields)
				//		confViewCopy.Fields.Add(confViewField.Key, confViewField.Value);
				//}

				LoadDirectory(treeConfiguration.Nodes["root"].Nodes["Directories"], confDirectoriesCopy);
			}
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
						if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
						{
							Conf.Directories.Remove(directoryName);
							treeConfiguration.Nodes["root"].Nodes["Directories"].Nodes[directoryName].Remove();
							//LoadDirectories(treeConfiguration.Nodes["root"].Nodes["Directories"]);
						}
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() + " вказівники на довідник \"" + directoryName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nВидалитити неможливо";

						MessageBox.Show(textListPointer, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void addNewDirectiryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addDirectoryToolStripMenuItem_Click(sender, e);
		}

		#endregion

		#region Контекстне меню перелічення

		private void addEnumItem_Click(object sender, EventArgs e)
		{
			addEnumToolStripMenuItem_Click(sender, e);
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

		private void copyEnumItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string enumName = nodeSel.Name;

				string enumCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					enumCopyName = enumName + "_Копія_" + i.ToString();
					if (!Conf.Enums.ContainsKey(enumCopyName))
						break;
				}

				ConfigurationEnums confEnumsOriginal = Conf.Enums[enumName];

				ConfigurationEnums confEnumsCopy = new ConfigurationEnums(enumCopyName, 0, confEnumsOriginal.Desc);
				Conf.AppendEnum(confEnumsCopy);

				foreach (ConfigurationEnumField confEnumFieldOriginal in confEnumsOriginal.Fields.Values)
				{
					confEnumsCopy.AppendField(new ConfigurationEnumField(confEnumFieldOriginal.Name,
						confEnumFieldOriginal.Value, confEnumFieldOriginal.Desc));

					if (confEnumFieldOriginal.Value > confEnumsCopy.SerialNumber)
						confEnumsCopy.SerialNumber = confEnumFieldOriginal.Value;
				}

				LoadEnum(treeConfiguration.Nodes["root"].Nodes["Enums"], confEnumsCopy);
			}
		}

		private void deleteEnumItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string enumName = nodeSel.Name;

				if (Conf.Enums.ContainsKey(enumName))
				{
					List<string> ListPointers = Conf.SearchForPointersEnum("Перелічення." + enumName);
					if (ListPointers.Count == 0)
					{
						if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
						{
							Conf.Enums.Remove(enumName);

							treeConfiguration.Nodes["root"].Nodes["Enums"].Nodes[enumName].Remove();
							//LoadEnums(treeConfiguration.Nodes["root"].Nodes["Enums"]);
						}
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() + " вказівники на перелічення \"" + enumName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nВидалитити неможливо";

						MessageBox.Show(textListPointer, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		#endregion

		#region Контекстне меню документ

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

		private void addDocumentItem_Click(object sender, EventArgs e)
		{
			addNewDocumentToolStripMenuItem_Click(sender, e);
		}

		private void copyDocumentItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string documentName = nodeSel.Name;

				string documentCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					documentCopyName = documentName + "_Копія_" + i.ToString();
					if (!Conf.Documents.ContainsKey(documentCopyName))
						break;
				}

				ConfigurationDocuments confDocumentsOriginal = Conf.Documents[documentName];

				ConfigurationDocuments confDocumentsCopy = new
					ConfigurationDocuments(documentCopyName, Configuration.GetNewUnigueTableName(Program.Kernel),
					confDocumentsOriginal.Desc);

				Conf.AppendDocument(confDocumentsCopy);

				foreach (ConfigurationObjectField confFieldOriginal in confDocumentsOriginal.Fields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confDocumentsCopy.Fields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectTablePart confTablePartOriginal in confDocumentsOriginal.TabularParts.Values)
				{
					ConfigurationObjectTablePart confTablePartCopy = new
						ConfigurationObjectTablePart(confTablePartOriginal.Name, Configuration.GetNewUnigueTableName(Program.Kernel),
						confTablePartOriginal.Desc);

					confDocumentsCopy.TabularParts.Add(confTablePartCopy.Name, confTablePartCopy);

					foreach (ConfigurationObjectField confTablePartFieldOriginal in confTablePartOriginal.Fields.Values)
					{
						ConfigurationObjectField confFieldCopy = new
							ConfigurationObjectField(confTablePartFieldOriginal.Name, confTablePartFieldOriginal.NameInTable,
							confTablePartFieldOriginal.Type, confTablePartFieldOriginal.Pointer, confTablePartFieldOriginal.Desc);

						confTablePartCopy.Fields.Add(confFieldCopy.Name, confFieldCopy);
					}
				}

				LoadDocument(treeConfiguration.Nodes["root"].Nodes["Documents"], confDocumentsCopy);
			}
		}

		private void deleteDocumentItem_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string documentName = nodeSel.Name;

				if (Conf.Documents.ContainsKey(documentName))
				{
					List<string> ListPointers = Conf.SearchForPointers("Документи." + documentName);
					if (ListPointers.Count == 0)
					{
						if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
						{
							Conf.Documents.Remove(documentName);
							treeConfiguration.Nodes["root"].Nodes["Documents"].Nodes[documentName].Remove();
							//LoadDocuments(treeConfiguration.Nodes["root"].Nodes["Documents"]);
						}
					}
					else
					{
						string textListPointer = "Знайденно " + ListPointers.Count.ToString() + " вказівники на документ \"" + documentName + "\":\n";

						foreach (string item in ListPointers)
							textListPointer += " -> " + item + "\n";

						textListPointer += "\nВидалитити неможливо";

						MessageBox.Show(textListPointer, "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		#endregion

		#region Контекстне меню блок констант

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

		private void deleteConstantBlock_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string constantsBlockName = nodeSel.Name;

				if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					Conf.ConstantsBlock.Remove(constantsBlockName);
					treeConfiguration.Nodes["root"].Nodes["Contants"].Nodes[constantsBlockName].Remove();
				}
			}
		}

		private void addConstantBlock_Click(object sender, EventArgs e)
		{
			addContantsBlockToolStripMenuItem_Click(sender, e);
		}

		private void addNewConstant_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string constantsBlockName = nodeSel.Name;
				AddNewConstants(constantsBlockName);
			}
		}

		#endregion

		#region Контекстне меню констант

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

		private void copyConstant_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				ConfigurationConstants configurationConstants = (ConfigurationConstants)nodeSel.Tag;
				ConfigurationConstantsBlock configurationConstantsBlock = configurationConstants.Block;

				string constantName = configurationConstants.Name;

				string constantCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					constantCopyName = constantName + "_Копія_" + i.ToString();
					if (!configurationConstantsBlock.Constants.ContainsKey(constantCopyName))
						break;
				}

				ConfigurationConstants configurationCopyConstants = new ConfigurationConstants(constantCopyName,
					Configuration.GetNewUnigueColumnName(Program.Kernel, "tab_constants", GetConstantsAllFields()),
					configurationConstants.Type, configurationConstantsBlock, configurationConstants.Pointer, configurationConstants.Desc);

				foreach (ConfigurationObjectTablePart confTablePartOriginal in configurationConstants.TabularParts.Values)
				{
					ConfigurationObjectTablePart confTablePartCopy = new
						ConfigurationObjectTablePart(confTablePartOriginal.Name, Configuration.GetNewUnigueTableName(Program.Kernel),
						confTablePartOriginal.Desc);

					configurationCopyConstants.TabularParts.Add(confTablePartCopy.Name, confTablePartCopy);

					foreach (ConfigurationObjectField confTablePartFieldOriginal in confTablePartOriginal.Fields.Values)
					{
						ConfigurationObjectField confFieldCopy = new
							ConfigurationObjectField(confTablePartFieldOriginal.Name, confTablePartFieldOriginal.NameInTable,
							confTablePartFieldOriginal.Type, confTablePartFieldOriginal.Pointer, confTablePartFieldOriginal.Desc);

						confTablePartCopy.Fields.Add(confFieldCopy.Name, confFieldCopy);
					}
				} 
				
				Conf.AppendConstants(configurationConstantsBlock.BlockName, configurationCopyConstants);

				LoadConstant(treeConfiguration.Nodes["root"].Nodes["Contants"].Nodes[configurationConstantsBlock.BlockName], configurationCopyConstants);
			}
		}

		private void addConstant_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				ConfigurationConstants configurationConstants = (ConfigurationConstants)nodeSel.Tag;
				ConfigurationConstantsBlock configurationConstantsBlock = configurationConstants.Block;

				AddNewConstants(configurationConstantsBlock.BlockName);
			}
		}

		private void deleteConstant_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				ConfigurationConstants configurationConstants = (ConfigurationConstants)nodeSel.Tag;
				ConfigurationConstantsBlock configurationConstantsBlock = configurationConstants.Block;

				if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					configurationConstantsBlock.Constants.Remove(configurationConstants.Name);

					treeConfiguration.Nodes["root"].Nodes["Contants"].Nodes[configurationConstantsBlock.BlockName].
						Nodes[configurationConstants.Name].Remove();
				}
			}
		}

		#endregion

		#region Контекстне меню регістер відомостей

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

		private void addItemRegistersInformation_Click(object sender, EventArgs e)
		{
			addNewRegistersInformationToolStripMenuItem_Click(sender, e);
		}

		private void copyItemRegistersInformation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registerName = nodeSel.Name;

				string registerCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					registerCopyName = registerName + "_Копія_" + i.ToString();
					if (!Conf.RegistersInformation.ContainsKey(registerCopyName))
						break;
				}

				ConfigurationRegistersInformation confRegisterOriginal = Conf.RegistersInformation[registerName];

				ConfigurationRegistersInformation confRegisterCopy = new
					ConfigurationRegistersInformation(registerCopyName, Configuration.GetNewUnigueTableName(Program.Kernel),
					confRegisterOriginal.Desc);

				Conf.AppendRegistersInformation(confRegisterCopy);

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.DimensionFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.DimensionFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.ResourcesFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.ResourcesFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.PropertyFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.PropertyFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				LoadRegisterInformation(treeConfiguration.Nodes["root"].Nodes["RegistersInformation"], confRegisterCopy);
			}
		}

		private void deleteItemRegistersInformation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registersName = nodeSel.Name;

				if (Conf.RegistersInformation.ContainsKey(registersName))
				{
					if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						Conf.RegistersInformation.Remove(registersName);
						treeConfiguration.Nodes["root"].Nodes["RegistersInformation"].Nodes[registersName].Remove();
						//LoadRegistersInformation(treeConfiguration.Nodes["root"].Nodes["RegistersInformation"]);
					}
				}
			}
		}

		#endregion

		#region Контекстне меню регістер накопичення

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

		private void addItemRegistersAccumulation_Click(object sender, EventArgs e)
		{
			addNewRegisterAccumulationToolStripMenuItem_Click(sender, e);
		}

		private void copyItemRegistersAccumulation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registerName = nodeSel.Name;

				string registerCopyName = "";
				for (int i = 1; i < 100; i++)
				{
					registerCopyName = registerName + "_Копія_" + i.ToString();
					if (!Conf.RegistersAccumulation.ContainsKey(registerCopyName))
						break;
				}

				ConfigurationRegistersAccumulation confRegisterOriginal = Conf.RegistersAccumulation[registerName];

				ConfigurationRegistersAccumulation confRegisterCopy = new
					ConfigurationRegistersAccumulation(registerCopyName, Configuration.GetNewUnigueTableName(Program.Kernel),
					confRegisterOriginal.TypeRegistersAccumulation, confRegisterOriginal.Desc);

				Conf.AppendRegistersAccumulation(confRegisterCopy);

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.DimensionFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.DimensionFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.ResourcesFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.ResourcesFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				foreach (ConfigurationObjectField confFieldOriginal in confRegisterOriginal.PropertyFields.Values)
				{
					ConfigurationObjectField confFieldCopy = new
						ConfigurationObjectField(confFieldOriginal.Name, confFieldOriginal.NameInTable,
						confFieldOriginal.Type, confFieldOriginal.Pointer, confFieldOriginal.Desc);

					confRegisterCopy.PropertyFields.Add(confFieldCopy.Name, confFieldCopy);
				}

				LoadRegisterAccumulation(treeConfiguration.Nodes["root"].Nodes["RegistersAccumulation"], confRegisterCopy);
			}
		}

		private void deleteItemRegistersAccumulation_Click(object sender, EventArgs e)
		{
			if (nodeSel != null)
			{
				string registersName = nodeSel.Name;

				if (Conf.RegistersAccumulation.ContainsKey(registersName))
				{
					if (MessageBox.Show("Видалити?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						Conf.RegistersAccumulation.Remove(registersName);
						treeConfiguration.Nodes["root"].Nodes["RegistersAccumulation"].Nodes[registersName].Remove();
						//LoadRegistersAccumulation(treeConfiguration.Nodes["root"].Nodes["RegistersAccumulation"]);
					}
				}
			}
		}

		#endregion

		#region Головне меню

		private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveConfigurationForm saveConfigurationForm = new SaveConfigurationForm();
			saveConfigurationForm.Conf = Conf;
			saveConfigurationForm.ShowDialog();
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
			AddNewConstants();
		}

		private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DirectoryForm directoryForm = new DirectoryForm();
			directoryForm.CallBack = CallBack_Update_Directory;
			directoryForm.CallBack_IsExistDirectoryName = CallBack_IsExistDirectoryName;
			directoryForm.Show();
		}

		private void addNewDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentForm documentForm = new DocumentForm();
			documentForm.CallBack = CallBack_Update_Document;
			documentForm.CallBack_IsExistDocumentName = CallBack_IsExistDocumentName;
			documentForm.Show();
		}

		private void addEnumToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EnumForm enumForm = new EnumForm();
			enumForm.CallBack = CallBack_Update_Enum;
			enumForm.CallBack_IsExistEnums = CallBack_IsExistEnumName;
			enumForm.Show();
		}

		private void addNewRegistersInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegistersInformationForm registersInformationForm = new RegistersInformationForm();
			registersInformationForm.CallBack = CallBack_Update_RegistersInformation;
			registersInformationForm.CallBack_IsExistRegistersInformation = CallBack_IsExistRegistersInformation;
			registersInformationForm.Show();
		}

		private void addNewRegisterAccumulationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegistersAccumulationForm registersAccumulationForm = new RegistersAccumulationForm();
			registersAccumulationForm.CallBack = CallBack_Update_RegistersAccumulation;
			registersAccumulationForm.CallBack_IsExistRegistersAccumulation = CallBack_IsExistRegistersAccumulation;
			registersAccumulationForm.Show();
		}

		#endregion

		#region Функції

		private void AddNewConstants(string defaultBlockName = "")
		{
			ConstantsForm constantsForm = new ConstantsForm();
			constantsForm.CallBack_IsExistConstants = CallBack_IsExistConstants;
			constantsForm.CallBack = CallBack_Update_Constants;
			constantsForm.NewNameInTable = Configuration.GetNewUnigueColumnName(Program.Kernel, "tab_constants", GetConstantsAllFields());
			constantsForm.ConstantsBlock = defaultBlockName;
			constantsForm.Show();
		}

		private Dictionary<string, ConfigurationObjectField> GetConstantsAllFields()
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

			return ConstantsAllFields;
		}


		#endregion


	}
}
