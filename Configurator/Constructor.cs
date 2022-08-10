using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AccountingSoftware;

namespace Configurator
{
    public partial class Constructor : Form
    {
        public Constructor()
        {
            InitializeComponent();
        }

        public Configuration Conf { get; set; }
		public ConfigurationDirectories ConfigurationDirectories { get; set; }
		public ConfigurationDocuments ConfigurationDocuments { get; set; }
		public string ConfObjectName { get; set; }
		public enum ConstructorTypeBuild
		{
			Directory,
			Document
		}
		public ConstructorTypeBuild ConstructorType { get; set; }

		#region Tree

		public void LoadDirectory(TreeNode rootNode, ConfigurationDirectories confDirectory)
		{
			TreeNode directoryNode = rootNode.Nodes.Add(confDirectory.Name, confDirectory.Name);
			directoryNode.Checked = true;
			directoryNode.Tag = confDirectory;

			//Поля
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in confDirectory.Fields)
			{
				string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
					" -> " + ConfFields.Value.Pointer : "";

				TreeNode fieldNode = directoryNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
				fieldNode.Checked = true;
				fieldNode.Tag = ConfFields.Value;
			}

			if (confDirectory.TabularParts.Count > 0)
			{
				TreeNode directoriTabularPartsNode = directoryNode.Nodes.Add("TabularParts", "Табличні частини");
				directoriTabularPartsNode.Checked = true;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in confDirectory.TabularParts)
				{
					TreeNode directoriTablePartNode = directoriTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					directoriTablePartNode.Checked = true;
					directoriTablePartNode.Tag = ConfTablePart.Value;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						string info = (ConfTablePartFields.Value.Type == "pointer" || ConfTablePartFields.Value.Type == "enum") ?
							" -> " + ConfTablePartFields.Value.Pointer : "";

						TreeNode fieldNode = directoriTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name + info);
						fieldNode.Checked = true;
						fieldNode.Tag = ConfTablePartFields.Value;
					}
				}
			}
		}

		public void LoadDirectories(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();
			LoadDirectory(rootNode, Conf.Directories[ConfObjectName]);
		}

		public void LoadDocument(TreeNode rootNode, ConfigurationDocuments confDocument)
		{
			TreeNode documentNode = rootNode.Nodes.Add(confDocument.Name, confDocument.Name);
			documentNode.Checked = true;
			documentNode.Tag = confDocument;

			//Поля
			foreach (KeyValuePair<string, ConfigurationObjectField> ConfFields in confDocument.Fields)
			{
				string info = (ConfFields.Value.Type == "pointer" || ConfFields.Value.Type == "enum") ?
						" -> " + ConfFields.Value.Pointer : "";

				TreeNode fieldNode = documentNode.Nodes.Add(ConfFields.Key, ConfFields.Value.Name + info);
				fieldNode.Checked = true;
				fieldNode.Tag = ConfFields.Value;
			}

			if (confDocument.TabularParts.Count > 0)
			{
				TreeNode documentTabularPartsNode = documentNode.Nodes.Add("TabularParts", "Табличні частини");
				documentTabularPartsNode.Checked = true;

				foreach (KeyValuePair<string, ConfigurationObjectTablePart> ConfTablePart in confDocument.TabularParts)
				{
					TreeNode documentTablePartNode = documentTabularPartsNode.Nodes.Add(ConfTablePart.Key, ConfTablePart.Value.Name);
					documentTablePartNode.Checked = true;
					documentTablePartNode.Tag = ConfTablePart.Value;

					//Поля
					foreach (KeyValuePair<string, ConfigurationObjectField> ConfTablePartFields in ConfTablePart.Value.Fields)
					{
						string info = (ConfTablePartFields.Value.Type == "pointer" || ConfTablePartFields.Value.Type == "enum") ?
							" -> " + ConfTablePartFields.Value.Pointer : "";

						TreeNode fieldNode = documentTablePartNode.Nodes.Add(ConfTablePartFields.Key, ConfTablePartFields.Value.Name + info);
						fieldNode.Checked = true;
						fieldNode.Tag = ConfTablePartFields.Value;
					}
				}
			}
		}

		public void LoadDocuments(TreeNode rootNode)
		{
			rootNode.Nodes.Clear();
			LoadDocument(rootNode, Conf.Documents[ConfObjectName]);
		}

		public void LoadTree()
		{
			treeConfiguration.Nodes.Clear();

			TreeNode rootNode = treeConfiguration.Nodes.Add("root", "Конфігурація");
			rootNode.Checked = true;

			switch (ConstructorType)
            {
				case ConstructorTypeBuild.Directory:
                    {
						TreeNode directoriesNode = rootNode.Nodes.Add("Directories", "Довідники");
						directoriesNode.Checked = true;

						LoadDirectories(directoriesNode);

						directoriesNode.ExpandAll();

						break;
                    }
				case ConstructorTypeBuild.Document:
					{
						TreeNode documentsNode = rootNode.Nodes.Add("Documents", "Документи");
						documentsNode.Checked = true;

						LoadDocuments(documentsNode);

						documentsNode.ExpandAll();

						break;
					}
			}

			rootNode.Expand();
		}

		#endregion

		private void Constructor_Load(object sender, EventArgs e)
        {
			LoadTree();
		}

        private void treeConfiguration_AfterCheck(object sender, TreeViewEventArgs e)
        {
			TreeNode nodeCheck = e.Node;

			RecursionNodeCheck(nodeCheck.Checked, nodeCheck.Nodes);
		}

		private void RecursionNodeCheck(bool check, TreeNodeCollection nodeCheck)
        {
			foreach (TreeNode nodeChild in nodeCheck)
			{
				nodeChild.Checked = check;
				RecursionNodeCheck(check, nodeChild.Nodes);
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			switch (ConstructorType)
			{
				case ConstructorTypeBuild.Directory:
					{
						ConfigurationDirectories = Conf.Directories[ConfObjectName].Copy();
						break;
					}
				case ConstructorTypeBuild.Document:
					{
						ConfigurationDocuments = Conf.Documents[ConfObjectName].Copy();
						break;
					}
			}

			NodeWork(treeConfiguration.Nodes);
		}

		private void NodeWork(TreeNodeCollection nodeCollect)
        {
			foreach (TreeNode nodeChild in nodeCollect)
			{
				if (nodeChild.Tag != null)
				{
					string nodeTagTypeObject = nodeChild.Tag.GetType().Name;

					Console.WriteLine(nodeChild.Name + " - " + nodeTagTypeObject);

					if (nodeTagTypeObject == "ConfigurationDirectories")
					{
						if (!nodeChild.Checked)
							ConfigurationDirectories = null;
						else
							foreach (TreeNode nodeChildField in nodeChild.Nodes)
							{
								if (nodeChildField.Tag != null)
								{
									nodeTagTypeObject = nodeChildField.Tag.GetType().Name;

									if (nodeTagTypeObject == "ConfigurationObjectField")
										if (!nodeChildField.Checked)
											ConfigurationDirectories.Fields.Remove(nodeChildField.Name);
								}
								else
									NodeWork(nodeChildField.Nodes);
							}
					}
					else if (nodeTagTypeObject == "ConfigurationDocuments")
					{
						if (!nodeChild.Checked)
							ConfigurationDocuments = null;
						else
							foreach (TreeNode nodeChildField in nodeChild.Nodes)
							{
								if (nodeChildField.Tag != null)
								{
									nodeTagTypeObject = nodeChildField.Tag.GetType().Name;

									if (nodeTagTypeObject == "ConfigurationObjectField")
										if (!nodeChildField.Checked)
											ConfigurationDocuments.Fields.Remove(nodeChildField.Name);
								}
								else
									NodeWork(nodeChildField.Nodes);
							}
					}
					else if (nodeTagTypeObject == "ConfigurationObjectTablePart")
					{
						if (!nodeChild.Checked)
						{
							switch (ConstructorType)
							{
								case ConstructorTypeBuild.Directory:
									{
										ConfigurationDirectories.TabularParts.Remove(nodeChild.Name);
										break;
									}
								case ConstructorTypeBuild.Document:
									{
										ConfigurationDocuments.TabularParts.Remove(nodeChild.Name);
										break;
									}
							}
						}
						else
						{
							foreach (TreeNode nodeChildField in nodeChild.Nodes)
							{
								if (nodeChildField.Tag != null)
								{
									nodeTagTypeObject = nodeChildField.Tag.GetType().Name;

									if (nodeTagTypeObject == "ConfigurationObjectField")
										if (!nodeChildField.Checked)
											switch (ConstructorType)
											{
												case ConstructorTypeBuild.Directory:
													{
														ConfigurationDirectories.TabularParts[nodeChild.Name].Fields.Remove(nodeChildField.Name);
														break;
													}
												case ConstructorTypeBuild.Document:
													{
														ConfigurationDocuments.TabularParts[nodeChild.Name].Fields.Remove(nodeChildField.Name);
														break;
													}
											}
								}
							}
						}
					}
				}
				else
					NodeWork(nodeChild.Nodes);
			}
		}

	}
}
