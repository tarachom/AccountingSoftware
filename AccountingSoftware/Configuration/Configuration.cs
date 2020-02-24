using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace AccountingSoftware
{
	//Конфігурація
	//В цьому класі має міститися вся інформація про конфігурацію
	public class Configuration
	{
		public Configuration()
		{
			Constants = new Dictionary<string, ConfigurationConstants>();
			Directories = new Dictionary<string, ConfigurationDirectories>();
			Documents = new Dictionary<string, ConfigurationDocuments>();
			Enums = new Dictionary<string, ConfigurationEnums>();
			Registers = new Dictionary<string, ConfigurationRegisters>();

			ReservedUnigueTableName = new List<string>();
			ReservedUnigueColumnName = new Dictionary<string, List<string>>();
		}

		public string Name { get; set; }

		public string NameSpace { get; set; }

		public string Author { get; set; }

		public string PathToXmlFileConfiguration { get; set; }

		public Dictionary<string, ConfigurationConstants> Constants { get; }

		public Dictionary<string, ConfigurationDirectories> Directories { get; }

		public Dictionary<string, ConfigurationDocuments> Documents { get; }

		public Dictionary<string, ConfigurationEnums> Enums { get; }

		public Dictionary<string, ConfigurationRegisters> Registers { get; }

		private List<string> ReservedUnigueTableName { get; set; }

		private Dictionary<string, List<string>> ReservedUnigueColumnName { get; set; }

		public ConfigurationDirectories AppendDirectory(ConfigurationDirectories Directory)
		{
			Directories.Add(Directory.Name, Directory);
			return Directory;
		}

		public ConfigurationEnums AppendEnum(ConfigurationEnums Enum)
		{
			Enums.Add(Enum.Name, Enum);
			return Enum;
		}

		public List<string> SearchForPointers(string searchDirectoryName)
		{
			List<string> ListPointer = new List<string>();

			//Перевірити поля довідників та поля табличних частин чи часом вони не ссилаються на цей довідник
			foreach (ConfigurationDirectories directoryItem in Directories.Values)
			{
				//Поля довідника
				foreach (ConfigurationObjectField directoryField in directoryItem.Fields.Values)
				{
					if (directoryField.Type == "pointer")
					{
						if (directoryField.Pointer == searchDirectoryName)
						{
							//pointer
							ListPointer.Add(directoryItem.Name + "." + directoryField.Name);
						}
					}
				}

				//Табличні частини
				foreach (ConfigurationObjectTablePart directoryTablePart in directoryItem.TabularParts.Values)
				{
					//Поля табличної частини
					foreach (ConfigurationObjectField tablePartField in directoryTablePart.Fields.Values)
					{
						if (tablePartField.Type == "pointer")
						{
							if (tablePartField.Pointer == searchDirectoryName)
							{
								//pointer
								ListPointer.Add(directoryItem.Name + "." + directoryTablePart.Name + "." + tablePartField.Name);
							}
						}
					}
				}
			}

			return ListPointer;
		}

		public List<string> SearchForPointersEnum(string searchEnumName)
		{
			List<string> ListPointer = new List<string>();

			//Перевірити поля довідників та поля табличних частин чи часом вони не ссилаються на перелічення
			foreach (ConfigurationDirectories directoryItem in Directories.Values)
			{
				//Поля довідника
				foreach (ConfigurationObjectField directoryField in directoryItem.Fields.Values)
				{
					if (directoryField.Type == "enum")
					{
						if (directoryField.Pointer == searchEnumName)
						{
							//pointer
							ListPointer.Add(directoryItem.Name + "." + directoryField.Name);
						}
					}
				}

				//Табличні частини
				foreach (ConfigurationObjectTablePart directoryTablePart in directoryItem.TabularParts.Values)
				{
					//Поля табличної частини
					foreach (ConfigurationObjectField tablePartField in directoryTablePart.Fields.Values)
					{
						if (tablePartField.Type == "enum")
						{
							if (tablePartField.Pointer == searchEnumName)
							{
								//pointer
								ListPointer.Add(directoryItem.Name + "." + directoryTablePart.Name + "." + tablePartField.Name);
							}
						}
					}
				}
			}

			return ListPointer;
		}

		public static void WriteLog(string txt)
		{
			File.AppendAllText(@"D:\log.txt", txt + "\n");
		}

		public static string GetNewUnigueColumnName(Kernel Kernel, string table, Dictionary<string, ConfigurationObjectField> Fields)
		{
			string[] mas = new string[]
			{
				"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "n",
				"m", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
			};

			bool noExistInReserved = false;
			bool noExistInConf = false;
			string columnNewName = "";

			if (String.IsNullOrWhiteSpace(table)) {
				table = "0";
			}

			if (!Kernel.Conf.ReservedUnigueColumnName.ContainsKey(table))
				Kernel.Conf.ReservedUnigueColumnName.Add(table, new List<string>());

			for (int j = 0; j < mas.Length; j++)
			{
				for (int i = 1; i < 10; i++)
				{
					columnNewName = "col_" + mas[j] + i.ToString();

					if (!Kernel.Conf.ReservedUnigueColumnName[table].Contains(columnNewName))
					{
						noExistInReserved = true;
					}
					else
						continue;

					noExistInConf = true;

					foreach (ConfigurationObjectField configurationObjectField in Fields.Values)
					{
						if (configurationObjectField.NameInTable == columnNewName)
						{
							noExistInConf = false;
							break;
						}
					}

					if (noExistInReserved && noExistInConf) 
					{
						break;
					}
				}

				if (noExistInReserved && noExistInConf)
				{
					break;
				}
			}

			Kernel.Conf.ReservedUnigueColumnName[table].Add(columnNewName);

			return columnNewName;
		}

		public static string GetNewUnigueTableName(Kernel Kernel)
		{
			string[] mas = new string[] 
			{
				"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "n",
				"m", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" 
			};

			bool noExistInReserved = false;
			bool noExistInBase = false;
			bool noExistInConf = false;
			string tabNewName = "";

			for (int j = 0; j < mas.Length; j++)
			{
				for (int i = 1; i < 100; i++)
				{
					tabNewName = "tab_" + mas[j] + (i < 10 ? "0" : "") + i.ToString();

					if (!Kernel.Conf.ReservedUnigueTableName.Contains(tabNewName))
					{
						noExistInReserved = true;
					}
					else
						continue;

					if (!Kernel.DataBase.IfExistsTable(tabNewName))
					{
						noExistInBase = true;
					}
					else
						continue;

					noExistInConf = true;

					foreach (ConfigurationDirectories directoryItem in Kernel.Conf.Directories.Values)
					{
						if (directoryItem.Table == tabNewName)
						{
							noExistInConf = false;
							break;
						}

						foreach (ConfigurationObjectTablePart directoryTablePart in directoryItem.TabularParts.Values)
						{
							if (directoryTablePart.Table == tabNewName)
							{
								noExistInConf = false;
								break;
							}
						}

						if (!noExistInConf)
						{
							break;
						}
					}

					if (noExistInReserved && noExistInBase && noExistInConf)
					{
						break;
					}
				}

				if (noExistInReserved && noExistInBase && noExistInConf)
				{
					break;
				}
			}

			Kernel.Conf.ReservedUnigueTableName.Add(tabNewName);

			return tabNewName;
		}

		public static string ValidateConfigurationObjectName(Kernel Kernel, ref string configurationObjectName)
		{
			string errorList = "";

			configurationObjectName = configurationObjectName.Trim();

			if (String.IsNullOrWhiteSpace(configurationObjectName))
			{
				errorList += "Назва не задана";
				return errorList;
			}

			string allovChar = "abcdefghijklnmopqrstuvwxyz_";
			string allovNum = "0123456789";
			string allovCharCyrillic = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //"АБВГДЕЄЖЗИІЇКЛМНОПРСТУФХЦЧШЩЪЫЭЮЯЬ";
			string allovAll = allovChar + allovNum + allovCharCyrillic;

			string configurationObjectModificeName = "";

			for (int i = 0; i < configurationObjectName.Length; i++)
			{
				string checkChar = configurationObjectName.Substring(i, 1);
				string checkCharLover = checkChar.ToLower();

				if (allovAll.IndexOf(checkCharLover) >= 0)
				{
					if (i == 0 && allovNum.IndexOf(checkCharLover) >= 0)
					{
						errorList += "Назва має починатися з букви\n";
					}

					configurationObjectModificeName += checkChar;
				}
				else
					errorList += "Недопустимий символ (" + i.ToString() + "): " + "[" + checkChar + "]\n";
			}

			configurationObjectName = configurationObjectModificeName;
			return errorList;
		}

		public static void SaveInformationSchema(ConfigurationInformationSchema InformationSchema, string pathToSave)
		{
			XmlDocument xmlComparisonDocument = new XmlDocument();
			xmlComparisonDocument.AppendChild(xmlComparisonDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

			XmlElement nodeInformationSchema = xmlComparisonDocument.CreateElement("InformationSchema");
			xmlComparisonDocument.AppendChild(nodeInformationSchema);

			foreach (KeyValuePair<string, ConfigurationInformationSchema_Table> informationSchemaTable in InformationSchema.Tables)
			{
				XmlElement nodeInformationSchemaTable = xmlComparisonDocument.CreateElement("Table");
				nodeInformationSchema.AppendChild(nodeInformationSchemaTable);

				XmlElement nodeInformationSchemaTableName = xmlComparisonDocument.CreateElement("Name");
				nodeInformationSchemaTableName.InnerText = informationSchemaTable.Value.TableName;
				nodeInformationSchemaTable.AppendChild(nodeInformationSchemaTableName);

				foreach (KeyValuePair<string, ConfigurationInformationSchema_Column> informationSchemaColumn in informationSchemaTable.Value.Columns)
				{
					XmlElement nodeInformationSchemaColumn = xmlComparisonDocument.CreateElement("Column");
					nodeInformationSchemaTable.AppendChild(nodeInformationSchemaColumn);

					XmlElement nodeInformationSchemaColumnName = xmlComparisonDocument.CreateElement("Name");
					nodeInformationSchemaColumnName.InnerText = informationSchemaColumn.Value.ColumnName;
					nodeInformationSchemaColumn.AppendChild(nodeInformationSchemaColumnName);

					XmlElement nodeInformationSchemaColumnDataType = xmlComparisonDocument.CreateElement("DataType");
					nodeInformationSchemaColumnDataType.InnerText = informationSchemaColumn.Value.DataType;
					nodeInformationSchemaColumn.AppendChild(nodeInformationSchemaColumnDataType);

					XmlElement nodeInformationSchemaColumnUdtName = xmlComparisonDocument.CreateElement("UdtName");
					nodeInformationSchemaColumnUdtName.InnerText = informationSchemaColumn.Value.UdtName;
					nodeInformationSchemaColumn.AppendChild(nodeInformationSchemaColumnUdtName);
				}
			}

			xmlComparisonDocument.Save(pathToSave);
		}

		public static void Load(string pathToConf, Configuration Conf)
		{
			XPathDocument xPathDoc = new XPathDocument(pathToConf);
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			LoadConfigurationInfo(Conf, xPathDocNavigator);

			LoadDirectories(Conf, xPathDocNavigator);

			LoadEnums(Conf, xPathDocNavigator);
		}

		private static void LoadConfigurationInfo(Configuration Conf, XPathNavigator xPathDocNavigator)
		{
			XPathNavigator rootNodeConfiguration = xPathDocNavigator.SelectSingleNode("/Configuration");

			string name = rootNodeConfiguration.SelectSingleNode("Name").Value;
			Conf.Name = name;

			string nameSpace = rootNodeConfiguration.SelectSingleNode("NameSpace").Value;
			Conf.NameSpace = nameSpace;

			string author = rootNodeConfiguration.SelectSingleNode("Author").Value;
			Conf.Author = author;
		}

		private static void LoadDirectories(Configuration Conf, XPathNavigator xPathDocNavigator)
		{
			//Довідники
			XPathNodeIterator directoryNodes = xPathDocNavigator.Select("/Configuration/Directories/Directory");
			while (directoryNodes.MoveNext())
			{
				string name = directoryNodes.Current.SelectSingleNode("Name").Value;
				string table = directoryNodes.Current.SelectSingleNode("Table").Value;
				string desc = directoryNodes.Current.SelectSingleNode("Desc").Value;

				ConfigurationDirectories ConfObjectDirectories = new ConfigurationDirectories(name, table, desc);
				Conf.Directories.Add(ConfObjectDirectories.Name, ConfObjectDirectories);

				LoadFields(ConfObjectDirectories.Fields, directoryNodes.Current);

				LoadTabularParts(ConfObjectDirectories.TabularParts, directoryNodes.Current);

				LoadViews(ConfObjectDirectories.Views, directoryNodes.Current);
			}
		}

		private static void LoadFields(Dictionary<string, ConfigurationObjectField> fields, XPathNavigator xPathDocNavigator)
		{
			XPathNodeIterator fieldNodes = xPathDocNavigator.Select("Fields/Field");
			while (fieldNodes.MoveNext())
			{
				string name = fieldNodes.Current.SelectSingleNode("Name").Value;
				string nameInTable = fieldNodes.Current.SelectSingleNode("NameInTable").Value;
				string type = fieldNodes.Current.SelectSingleNode("Type").Value;
				string pointer = "";
				string desc = fieldNodes.Current.SelectSingleNode("Desc").Value;

				if (type == "pointer" || type == "enum")
				{
					pointer = fieldNodes.Current.SelectSingleNode("Pointer").Value;
				}

				ConfigurationObjectField ConfObjectField = new ConfigurationObjectField(name, nameInTable, type, pointer, desc);

				fields.Add(name, ConfObjectField);
			}
		}

		private static void LoadTabularParts(Dictionary<string, ConfigurationObjectTablePart> tabularParts, XPathNavigator xPathDocNavigator)
		{
			XPathNodeIterator tablePartNodes = xPathDocNavigator.Select("TabularParts/TablePart");
			while (tablePartNodes.MoveNext())
			{
				string name = tablePartNodes.Current.SelectSingleNode("Name").Value;
				string table = tablePartNodes.Current.SelectSingleNode("Table").Value;
				string desc = tablePartNodes.Current.SelectSingleNode("Desc").Value;

				ConfigurationObjectTablePart ConfObjectTablePart = new ConfigurationObjectTablePart(name, table, desc);

				tabularParts.Add(ConfObjectTablePart.Name, ConfObjectTablePart);

				LoadFields(ConfObjectTablePart.Fields, tablePartNodes.Current);
			}
		}

		private static void LoadViews(Dictionary<string, ConfigurationObjectView> views, XPathNavigator xPathDocNavigator)
		{
			XPathNodeIterator viewNodes = xPathDocNavigator.Select("Views/View");
			while (viewNodes.MoveNext())
			{
				string nameView = viewNodes.Current.SelectSingleNode("Name").Value;
				string tableView = viewNodes.Current.SelectSingleNode("Table").Value;
				string descView = viewNodes.Current.SelectSingleNode("Desc").Value;

				ConfigurationObjectView ConfObjectView = new ConfigurationObjectView(nameView, tableView, descView);

				views.Add(ConfObjectView.Name, ConfObjectView);

				XPathNodeIterator fieldNodes = viewNodes.Current.Select("Fields/Field");
				while (fieldNodes.MoveNext())
				{
					string nameField = fieldNodes.Current.SelectSingleNode("Name").Value;
					string nameInTableField = fieldNodes.Current.SelectSingleNode("NameInTable").Value;

					ConfObjectView.Fields.Add(nameField, nameInTableField);
				}
				
				/*
				XPathNodeIterator fieldWhere = viewNodes.Current.Select("Where/Field");
				while (fieldWhere.MoveNext())
				{
					string nameInTableField = fieldWhere.Current.SelectSingleNode("NameInTable").Value;

					ConfObjectView.Where.Add(nameInTableField);
				}
				*/
			}
		}

		public static void LoadEnums(Configuration Conf, XPathNavigator xPathDocNavigator)
		{
			//Перелічення
			XPathNodeIterator enumsNodes = xPathDocNavigator.Select("/Configuration/Enums/Enum");
			while (enumsNodes.MoveNext())
			{
				string name = enumsNodes.Current.SelectSingleNode("Name").Value;
				string desc = enumsNodes.Current.SelectSingleNode("Desc").Value;

				if (String.IsNullOrWhiteSpace(desc)) desc = "";

				ConfigurationEnums configurationEnums = new ConfigurationEnums(name, desc);
				Conf.Enums.Add(configurationEnums.Name, configurationEnums);

				XPathNodeIterator enumFieldsNodes = enumsNodes.Current.Select("Fields/Field");
				while (enumFieldsNodes.MoveNext())
				{
					string nameField = enumFieldsNodes.Current.SelectSingleNode("Name").Value;
					string valueField = enumFieldsNodes.Current.SelectSingleNode("Value").Value;

					configurationEnums.AppendField(nameField, int.Parse(valueField));
				}
			}
		}

		public static void Save(string pathToConf, Configuration Conf)
		{
			//string pathToCopyConf = CopyConfigurationFile(pathToConf);

			XmlDocument xmlConfDocument = new XmlDocument();
			xmlConfDocument.AppendChild(xmlConfDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

			XmlElement rootNode = xmlConfDocument.CreateElement("Configuration");
			xmlConfDocument.AppendChild(rootNode);

			SaveConfigurationInfo(Conf, xmlConfDocument, rootNode);

			SaveDirectories(Conf.Directories, xmlConfDocument, rootNode);

			SaveEnums(Conf.Enums, xmlConfDocument, rootNode);

			xmlConfDocument.Save(pathToConf);

			//ComparisonCopyAndNewConfigurationFile(pathToConf, pathToCopyConf);
		}

		private static void SaveConfigurationInfo(Configuration Conf, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement nodeName = xmlConfDocument.CreateElement("Name");
			nodeName.InnerText = Conf.Name;
			rootNode.AppendChild(nodeName);

			XmlElement nodeNameSpace = xmlConfDocument.CreateElement("NameSpace");
			nodeNameSpace.InnerText = Conf.NameSpace;
			rootNode.AppendChild(nodeNameSpace);

			XmlElement nodeAuthor = xmlConfDocument.CreateElement("Author");
			nodeAuthor.InnerText = Conf.Author;
			rootNode.AppendChild(nodeAuthor);

			XmlElement nodeDateTimeSave = xmlConfDocument.CreateElement("DateTimeSave");
			nodeDateTimeSave.InnerText = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
			rootNode.AppendChild(nodeDateTimeSave);
		}

		private static void SaveDirectories(Dictionary<string, ConfigurationDirectories> ConfDirectories, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement rootDirectories = xmlConfDocument.CreateElement("Directories");
			rootNode.AppendChild(rootDirectories);

			foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in ConfDirectories)
			{
				XmlElement nodeDirectory = xmlConfDocument.CreateElement("Directory");
				rootDirectories.AppendChild(nodeDirectory);

				XmlElement nodeDirectoryName = xmlConfDocument.CreateElement("Name");
				nodeDirectoryName.InnerText = ConfDirectory.Key;
				nodeDirectory.AppendChild(nodeDirectoryName);

				XmlElement nodeDirectoryTable = xmlConfDocument.CreateElement("Table");
				nodeDirectoryTable.InnerText = ConfDirectory.Value.Table;
				nodeDirectory.AppendChild(nodeDirectoryTable);

				XmlElement nodeDirectoryDesc = xmlConfDocument.CreateElement("Desc");
				nodeDirectoryDesc.InnerText = ConfDirectory.Value.Desc;
				nodeDirectory.AppendChild(nodeDirectoryDesc);

				SaveFields(ConfDirectory.Value.Fields, xmlConfDocument, nodeDirectory);

				SaveTabularParts(ConfDirectory.Value.TabularParts, xmlConfDocument, nodeDirectory);

				SaveViews(ConfDirectory.Value.Views, ConfDirectory.Value, xmlConfDocument, nodeDirectory);
			}
		}

		private static void SaveFields(Dictionary<string, ConfigurationObjectField> fields, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement nodeFields = xmlConfDocument.CreateElement("Fields");
			rootNode.AppendChild(nodeFields);

			foreach (KeyValuePair<string, ConfigurationObjectField> field in fields)
			{
				XmlElement nodeField = xmlConfDocument.CreateElement("Field");
				nodeFields.AppendChild(nodeField);

				XmlElement nodeFieldName = xmlConfDocument.CreateElement("Name");
				nodeFieldName.InnerText = field.Key;
				nodeField.AppendChild(nodeFieldName);

				XmlElement nodeFieldNameInTable = xmlConfDocument.CreateElement("NameInTable");
				nodeFieldNameInTable.InnerText = field.Value.NameInTable;
				nodeField.AppendChild(nodeFieldNameInTable);

				XmlElement nodeFieldType = xmlConfDocument.CreateElement("Type");
				nodeFieldType.InnerText = field.Value.Type;
				nodeField.AppendChild(nodeFieldType);

				if (field.Value.Type == "pointer" || field.Value.Type == "enum")
				{
					XmlElement nodeFieldPointer = xmlConfDocument.CreateElement("Pointer");
					nodeFieldPointer.InnerText = field.Value.Pointer;
					nodeField.AppendChild(nodeFieldPointer);
				}

				XmlElement nodeFieldDesc = xmlConfDocument.CreateElement("Desc");
				nodeFieldDesc.InnerText = field.Value.Desc;
				nodeField.AppendChild(nodeFieldDesc);
			}
		}

		private static void SaveTabularParts(Dictionary<string, ConfigurationObjectTablePart> tabularParts, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement nodeTabularParts = xmlConfDocument.CreateElement("TabularParts");
			rootNode.AppendChild(nodeTabularParts);

			foreach (KeyValuePair<string, ConfigurationObjectTablePart> tablePart in tabularParts)
			{
				XmlElement nodeTablePart = xmlConfDocument.CreateElement("TablePart");
				nodeTabularParts.AppendChild(nodeTablePart);

				XmlElement nodeTablePartName = xmlConfDocument.CreateElement("Name");
				nodeTablePartName.InnerText = tablePart.Key;
				nodeTablePart.AppendChild(nodeTablePartName);

				XmlElement nodeTablePartTable = xmlConfDocument.CreateElement("Table");
				nodeTablePartTable.InnerText = tablePart.Value.Table;
				nodeTablePart.AppendChild(nodeTablePartTable);

				XmlElement nodeTablePartDesc = xmlConfDocument.CreateElement("Desc");
				nodeTablePartDesc.InnerText = tablePart.Value.Desc;
				nodeTablePart.AppendChild(nodeTablePartDesc);

				SaveFields(tablePart.Value.Fields, xmlConfDocument, nodeTablePart);
			}
		}

		private static void SaveViews(Dictionary<string, ConfigurationObjectView> views, ConfigurationDirectories confDirectory, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement nodeViews = xmlConfDocument.CreateElement("Views");
			rootNode.AppendChild(nodeViews);

			foreach (KeyValuePair<string, ConfigurationObjectView> view in views)
			{
				XmlElement nodeView = xmlConfDocument.CreateElement("View");
				nodeViews.AppendChild(nodeView);

				XmlElement nodeViewName = xmlConfDocument.CreateElement("Name");
				nodeViewName.InnerText = view.Key;
				nodeView.AppendChild(nodeViewName);

				XmlElement nodeViewTable = xmlConfDocument.CreateElement("Table");
				nodeViewTable.InnerText = view.Value.Table;
				nodeView.AppendChild(nodeViewTable);

				XmlElement nodeTablePartDesc = xmlConfDocument.CreateElement("Desc");
				nodeTablePartDesc.InnerText = view.Value.Desc;
				nodeView.AppendChild(nodeTablePartDesc);

				XmlElement nodeFields = xmlConfDocument.CreateElement("Fields");
				nodeView.AppendChild(nodeFields);

				foreach (KeyValuePair<string, string> field in view.Value.Fields)
				{
					XmlElement nodeField = xmlConfDocument.CreateElement("Field");
					nodeFields.AppendChild(nodeField);

					XmlElement nodeFieldName = xmlConfDocument.CreateElement("Name");
					nodeFieldName.InnerText = field.Key;
					nodeField.AppendChild(nodeFieldName);

					XmlElement nodeFieldNameInTable = xmlConfDocument.CreateElement("NameInTable");
					nodeFieldNameInTable.InnerText = field.Value;
					nodeField.AppendChild(nodeFieldNameInTable);

					foreach (ConfigurationObjectField configurationObjectField in confDirectory.Fields.Values)
					{
						if (configurationObjectField.NameInTable == field.Value)
						{
							XmlElement nodeFieldType = xmlConfDocument.CreateElement("Type");
							nodeFieldType.InnerText = configurationObjectField.Type;
							nodeField.AppendChild(nodeFieldType);

							if (configurationObjectField.Type == "pointer" || configurationObjectField.Type == "enum")
							{
								XmlElement nodeFieldPointer = xmlConfDocument.CreateElement("Pointer");
								nodeFieldPointer.InnerText = configurationObjectField.Pointer;
								nodeField.AppendChild(nodeFieldPointer);
							}

							break;
						}
					}
				}

				/*
				XmlElement nodeWhere = xmlConfDocument.CreateElement("Where"); // ??
				nodeView.AppendChild(nodeWhere);

				foreach (string field in view.Value.Where)
				{
					XmlElement nodeField = xmlConfDocument.CreateElement("Field");
					nodeWhere.AppendChild(nodeField);

					XmlElement nodeFieldNameInTable = xmlConfDocument.CreateElement("NameInTable");
					nodeFieldNameInTable.InnerText = field;
					nodeField.AppendChild(nodeFieldNameInTable);
				}
				*/
			}
		}

		private static void SaveEnums(Dictionary<string, ConfigurationEnums> enums, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement nodeEnums = xmlConfDocument.CreateElement("Enums");
			rootNode.AppendChild(nodeEnums);

			foreach (KeyValuePair<string, ConfigurationEnums> enum_item in enums)
			{
				XmlElement nodeEnum = xmlConfDocument.CreateElement("Enum");
				nodeEnums.AppendChild(nodeEnum);

				XmlElement nodeEnumName = xmlConfDocument.CreateElement("Name");
				nodeEnumName.InnerText = enum_item.Key;
				nodeEnum.AppendChild(nodeEnumName);

				XmlElement nodeEnumDesc = xmlConfDocument.CreateElement("Desc");
				nodeEnumDesc.InnerText = enum_item.Value.Desc;
				nodeEnum.AppendChild(nodeEnumDesc);

				XmlElement nodeFields = xmlConfDocument.CreateElement("Fields");
				nodeEnum.AppendChild(nodeFields);

				foreach (KeyValuePair<string, int> field in enum_item.Value.Fields)
				{
					XmlElement nodeField = xmlConfDocument.CreateElement("Field");
					nodeFields.AppendChild(nodeField);

					XmlElement nodeFieldName = xmlConfDocument.CreateElement("Name");
					nodeFieldName.InnerText = field.Key;
					nodeField.AppendChild(nodeFieldName);

					XmlElement nodeFieldValue = xmlConfDocument.CreateElement("Value");
					nodeFieldValue.InnerText = field.Value.ToString();
					nodeField.AppendChild(nodeFieldValue);
				}
			}
		}

		private static string CopyConfigurationFile(string pathToConf) // ??
		{
			if (File.Exists(pathToConf))
			{
				string dirName = Path.GetDirectoryName(pathToConf);
				string fileNewName = Path.GetFileNameWithoutExtension(pathToConf) + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xml";
				string pathToCopyConf = Path.Combine(dirName, fileNewName);

				File.Copy(pathToConf, pathToCopyConf);

				return pathToCopyConf;
			}
			else
				throw new FileNotFoundException(pathToConf);
		}

		private static void ComparisonCopyAndNewConfigurationFile(string pathToConf, string pathToCopyConf)
		{
			//1. Пошук видалених довідників
			//..

			//2. Видалення в базі таблиць видалених довідників
			//..

			//Видалення копії конфігурації
			File.Delete(pathToCopyConf);
		}

		public static void Generation(string pathToConf, string pathToTemplate, string pathToSaveCode)
		{
			XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
			xsltCodeGnerator.Load(pathToTemplate);

			xsltCodeGnerator.Transform(pathToConf, pathToSaveCode);
		}

		public static void ComparisonGeneration(string pathToXML, string pathToTemplate, string pathToSaveCode)
		{
			XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
			xsltCodeGnerator.Load(pathToTemplate, new XsltSettings(true, true), null);

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("KeyUID", "", DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));

			System.IO.FileStream fileStream = new System.IO.FileStream(pathToSaveCode, System.IO.FileMode.Create);

			xsltCodeGnerator.Transform(pathToXML, xsltArgumentList, fileStream);

			fileStream.Close();
		}

		public static void Comparison(string pathToSave, Configuration Conf, ConfigurationInformationSchema InformationSchema)
		{

		}

		public static List<string> ListComparisonSql(string pathToXML)
		{
			List<string> slqList = new List<string>();

			XPathDocument xPathDoc = new XPathDocument(pathToXML);
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator sqlNodes = xPathDocNavigator.Select("/root/sql");
			while (sqlNodes.MoveNext())
			{
				string sqlText = sqlNodes.Current.Value;
				slqList.Add(sqlText);
			}

			return slqList;
		}
	}
}
