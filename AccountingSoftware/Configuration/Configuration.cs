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
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Windows.Forms;

namespace AccountingSoftware
{
	/// <summary>
	/// Конфігурація.
	/// В цьому класі міститься вся інформація про конфігурацію.
	/// </summary>
	public class Configuration
	{
		public Configuration()
		{
			ConstantsBlock = new Dictionary<string, ConfigurationConstantsBlock>();
			Directories = new Dictionary<string, ConfigurationDirectories>();
			Documents = new Dictionary<string, ConfigurationDocuments>();
			Enums = new Dictionary<string, ConfigurationEnums>();
			RegistersInformation = new Dictionary<string, ConfigurationRegistersInformation>();
			RegistersResidues = new Dictionary<string, ConfigurationRegistersResidues>();

			ReservedUnigueTableName = new List<string>();
			ReservedUnigueColumnName = new Dictionary<string, List<string>>();
		}

		/// <summary>
		/// Назва конфігурації
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Простір імен для конфігурації
		/// </summary>
		public string NameSpace { get; set; }

		/// <summary>
		/// Автор конфігурації
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// Шлях до хмл файлу конфігурації
		/// </summary>
		public string PathToXmlFileConfiguration { get; set; }

		/// <summary>
		/// Блоки констант
		/// </summary>
		public Dictionary<string, ConfigurationConstantsBlock> ConstantsBlock { get; }

		/// <summary>
		/// Довідники
		/// </summary>
		public Dictionary<string, ConfigurationDirectories> Directories { get; }

		/// <summary>
		/// Документи
		/// </summary>
		public Dictionary<string, ConfigurationDocuments> Documents { get; }

		/// <summary>
		/// Перелічення
		/// </summary>
		public Dictionary<string, ConfigurationEnums> Enums { get; }

		/// <summary>
		/// Регістри відомостей
		/// </summary>
		public Dictionary<string, ConfigurationRegistersInformation> RegistersInformation { get; }

		/// <summary>
		/// Регістри залишків
		/// </summary>
		public Dictionary<string, ConfigurationRegistersResidues> RegistersResidues { get; }

		#region Private_Function

		/// <summary>
		/// Список зарезервованих назв таблиць.
		/// Довідка: коли створюється новий довідник, чи документ 
		/// для нього резервується нова унікальна назва таблиці в базі даних. 
		/// </summary>
		private List<string> ReservedUnigueTableName { get; set; }

		/// <summary>
		/// Список зарезервованих назв стовпців.
		/// Ключем виступає назва таблиці для якої резервуються стовпці.
		/// </summary>
		private Dictionary<string, List<string>> ReservedUnigueColumnName { get; set; }

		private static string[] GetEnglishAlphabet()
		{
			return new string[]
			{
				"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "n",
				"m", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
			};
		}

		private static void WriteLog(string txt)
		{
			File.AppendAllText(@"D:\log.txt", txt + "\n");
		}

		#endregion

		public void AppendConstants(string blockName, ConfigurationConstants constants)
		{
			ConstantsBlock[blockName].Constants.Add(constants.Name, constants);
			constants.Block = ConstantsBlock[blockName];
		}

		public void AppendConstantsBlock(ConfigurationConstantsBlock constantsBlock)
		{
			ConstantsBlock.Add(constantsBlock.BlockName, constantsBlock);
		}

		/// <summary>
		/// Додати довідник в список довідників
		/// </summary>
		/// <param name="Directory">Довідник</param>
		public void AppendDirectory(ConfigurationDirectories Directory)
		{
			Directories.Add(Directory.Name, Directory);
		}

		/// <summary>
		/// Додати перелічення в список перелічень
		/// </summary>
		/// <param name="Enum">Перелічення</param>
		public void AppendEnum(ConfigurationEnums Enum)
		{
			Enums.Add(Enum.Name, Enum);
		}

		/// <summary>
		/// Додати документ в список документів
		/// </summary>
		/// <param name="Document">Документ</param>
		public void AppendDocument(ConfigurationDocuments Document)
		{
			Documents.Add(Document.Name, Document);
		}

		/// <summary>
		/// Пошук ссилок довідників і документів
		/// </summary>
		/// <param name="searchName">Назва довідника або документу</param>
		/// <returns>Повертає список довідників або документів які вказують на searchName</returns>
		public List<string> SearchForPointers(string searchName)
		{
			if (searchName.IndexOf(".") > 0)
			{
				string[] searchNameSplit = searchName.Split(new string[] { "." }, StringSplitOptions.None);

				if (!(searchNameSplit[0] == "Довідники" || searchNameSplit[0] == "Документи"))
					throw new Exception("Перша частина назви має бути 'Довідники' або 'Документи'");
			}
			else
				throw new Exception("Назва для пошуку має бути 'Довідники.<Назва довідника>' або 'Документи.<Назва документу>'");

			List<string> ListPointer = new List<string>();

			//Перевірити поля довідників та поля табличних частин чи часом вони не ссилаються на цей довідник
			foreach (ConfigurationDirectories directoryItem in Directories.Values)
			{
				//Поля довідника
				foreach (ConfigurationObjectField directoryField in directoryItem.Fields.Values)
				{
					if (directoryField.Type == "pointer" && directoryField.Pointer == searchName)
						ListPointer.Add(directoryItem.Name + "." + directoryField.Name);
				}

				//Табличні частини
				foreach (ConfigurationObjectTablePart directoryTablePart in directoryItem.TabularParts.Values)
				{
					//Поля табличної частини
					foreach (ConfigurationObjectField tablePartField in directoryTablePart.Fields.Values)
					{
						if (tablePartField.Type == "pointer" && tablePartField.Pointer == searchName)
							ListPointer.Add(directoryItem.Name + "." + directoryTablePart.Name + "." + tablePartField.Name);
					}
				}
			}

			//Перевірка документів
			foreach (ConfigurationDocuments documentItem in Documents.Values)
			{
				//Поля довідника
				foreach (ConfigurationObjectField documentField in documentItem.Fields.Values)
				{
					if (documentField.Type == "pointer" && documentField.Pointer == searchName)
						ListPointer.Add(documentItem.Name + "." + documentField.Name);
				}

				//Табличні частини
				foreach (ConfigurationObjectTablePart documentTablePart in documentItem.TabularParts.Values)
				{
					//Поля табличної частини
					foreach (ConfigurationObjectField tablePartField in documentTablePart.Fields.Values)
					{
						if (tablePartField.Type == "pointer" && tablePartField.Pointer == searchName)
							ListPointer.Add(documentItem.Name + "." + documentTablePart.Name + "." + tablePartField.Name);
					}
				}
			}

			return ListPointer;
		}

		/// <summary>
		/// Пошук ссилок на перелічення
		/// </summary>
		/// <param name="searchName">Назва перелічення</param>
		/// <returns>Повертає список довідників або документів які вказують на searchName</returns>
		public List<string> SearchForPointersEnum(string searchName)
		{
			if (searchName.IndexOf(".") > 0)
			{
				string[] searchNameSplit = searchName.Split(new string[] { "." }, StringSplitOptions.None);

				if (!(searchNameSplit[0] == "Перелічення"))
					throw new Exception("Перша частина назви має бути 'Перелічення'");
			}
			else
				throw new Exception("Назва для пошуку має бути 'Перелічення.<Назва перелічення>'");

			List<string> ListPointer = new List<string>();

			//Перевірити поля довідників та поля табличних частин чи часом вони не ссилаються на перелічення
			foreach (ConfigurationDirectories directoryItem in Directories.Values)
			{
				//Поля довідника
				foreach (ConfigurationObjectField directoryField in directoryItem.Fields.Values)
				{
					if (directoryField.Type == "enum" && directoryField.Pointer == searchName)
						ListPointer.Add(directoryItem.Name + "." + directoryField.Name);
				}

				//Табличні частини
				foreach (ConfigurationObjectTablePart directoryTablePart in directoryItem.TabularParts.Values)
				{
					//Поля табличної частини
					foreach (ConfigurationObjectField tablePartField in directoryTablePart.Fields.Values)
					{
						if (tablePartField.Type == "enum" && tablePartField.Pointer == searchName)
							ListPointer.Add(directoryItem.Name + "." + directoryTablePart.Name + "." + tablePartField.Name);
					}
				}
			}

			return ListPointer;
		}

		/// <summary>
		/// Повертає унікальну назву стовпця для вказаної таблиці
		/// </summary>
		/// <param name="Kernel">Ядро</param>
		/// <param name="table">Назва таблиці для якої генерується нова назва стовпця</param>
		/// <param name="Fields">Список існуючих полів</param>
		/// <returns>Повертає унікальну назву стовпця</returns>
		public static string GetNewUnigueColumnName(Kernel Kernel, string table, Dictionary<string, ConfigurationObjectField> Fields)
		{
			string[] englishAlphabet = GetEnglishAlphabet();

			bool noExistInReserved = false;
			bool noExistInConf = false;
			string columnNewName = "";

			if (String.IsNullOrWhiteSpace(table))
			{
				table = "0";
			}

			if (!Kernel.Conf.ReservedUnigueColumnName.ContainsKey(table))
				Kernel.Conf.ReservedUnigueColumnName.Add(table, new List<string>());

			for (int j = 0; j < englishAlphabet.Length; j++)
			{
				for (int i = 1; i < 10; i++)
				{
					columnNewName = "col_" + englishAlphabet[j] + i.ToString();

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

		/// <summary>
		/// Повертає унікальну назву таблиці
		/// </summary>
		/// <param name="Kernel">Ядро</param>
		/// <returns>Повертає унікальну назву таблиці</returns>
		public static string GetNewUnigueTableName(Kernel Kernel)
		{
			string[] englishAlphabet = GetEnglishAlphabet();

			bool noExistInReserved = false;
			bool noExistInBase = false;
			bool noExistInConf = false;
			string tabNewName = "";

			for (int j = 0; j < englishAlphabet.Length; j++)
			{
				for (int i = 1; i < 100; i++)
				{
					tabNewName = "tab_" + englishAlphabet[j] + (i < 10 ? "0" : "") + i.ToString();

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

		/// <summary>
		/// Перевіряє назву обєкту конфігурації (довідники, документи, поля) на валідність
		/// </summary>
		/// <param name="Kernel">Ядро</param>
		/// <param name="configurationObjectName">Назва обєкту конфігурації</param>
		/// <returns>Повертає інформацію про помилки у вигляді стрічки</returns>
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
				}
				else
				{
					errorList += "Недопустимий символ (" + i.ToString() + "): " + "[" + checkChar + "]\n";
				}

				configurationObjectModificeName += checkChar;
			}

			configurationObjectName = configurationObjectModificeName;
			return errorList;
		}

		/// <summary>
		/// Зберігає інформацію про схему бази даних в ХМЛ файл
		/// </summary>
		/// <param name="InformationSchema">Схема</param>
		/// <param name="pathToSave">Шлях до файлу</param>
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

		/// <summary>
		/// Завантаження конфігурації
		/// </summary>
		/// <param name="pathToConf">Шлях до файлу конфігурації</param>
		/// <param name="Conf">Конфігурація</param>
		public static void Load(string pathToConf, Configuration Conf)
		{
			XPathDocument xPathDoc = new XPathDocument(pathToConf);
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			LoadConfigurationInfo(Conf, xPathDocNavigator);

			LoadConstants(Conf, xPathDocNavigator);

			LoadDirectories(Conf, xPathDocNavigator);

			LoadEnums(Conf, xPathDocNavigator);

			LoadDocuments(Conf, xPathDocNavigator);
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

		private static void LoadConstants(Configuration Conf, XPathNavigator xPathDocNavigator)
		{
			XPathNodeIterator constantsBlockNodes = xPathDocNavigator.Select("/Configuration/ConstantsBlocks/ConstantsBlock");
			while (constantsBlockNodes.MoveNext())
			{
				string blockName = constantsBlockNodes.Current.SelectSingleNode("Name").Value;
				string blockDesc = constantsBlockNodes.Current.SelectSingleNode("Desc").Value;

				ConfigurationConstantsBlock configurationConstantsBlock = new ConfigurationConstantsBlock(blockName, blockDesc);
				Conf.ConstantsBlock.Add(configurationConstantsBlock.BlockName, configurationConstantsBlock);

				XPathNodeIterator constantsNodes = constantsBlockNodes.Current.Select("Constants/Constant");
				while (constantsNodes.MoveNext())
				{
					string constName = constantsNodes.Current.SelectSingleNode("Name").Value;
					string constType = constantsNodes.Current.SelectSingleNode("Type").Value;
					string constDesc = constantsNodes.Current.SelectSingleNode("Desc").Value;

					string constPointer = "";
					if (constType == "pointer" || constType == "enum")
						constPointer = constantsNodes.Current.SelectSingleNode("Pointer").Value;

					ConfigurationConstants configurationConstants = new ConfigurationConstants(constName, constType, configurationConstantsBlock, constPointer, constDesc);
					configurationConstantsBlock.Constants.Add(configurationConstants.Name, configurationConstants);
				}
			}
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
				int serialNumber = int.Parse(enumsNodes.Current.SelectSingleNode("SerialNumber").Value);

				if (String.IsNullOrWhiteSpace(desc)) desc = "";

				ConfigurationEnums configurationEnums = new ConfigurationEnums(name, serialNumber, desc);
				Conf.Enums.Add(configurationEnums.Name, configurationEnums);

				XPathNodeIterator enumFieldsNodes = enumsNodes.Current.Select("Fields/Field");
				while (enumFieldsNodes.MoveNext())
				{
					string nameField = enumFieldsNodes.Current.SelectSingleNode("Name").Value;
					string valueField = enumFieldsNodes.Current.SelectSingleNode("Value").Value;
					string descField = enumFieldsNodes.Current.SelectSingleNode("Desc").Value;

					configurationEnums.AppendField(new ConfigurationEnumField(nameField, int.Parse(valueField), descField));
				}
			}
		}

		private static void LoadDocuments(Configuration Conf, XPathNavigator xPathDocNavigator)
		{
			//Документи
			XPathNodeIterator documentsNode = xPathDocNavigator.Select("/Configuration/Documents/Document");
			while (documentsNode.MoveNext())
			{
				string name = documentsNode.Current.SelectSingleNode("Name").Value;
				string table = documentsNode.Current.SelectSingleNode("Table").Value;
				string desc = documentsNode.Current.SelectSingleNode("Desc").Value;

				ConfigurationDocuments configurationDocuments = new ConfigurationDocuments(name, table, desc);
				Conf.Documents.Add(configurationDocuments.Name, configurationDocuments);

				LoadFields(configurationDocuments.Fields, documentsNode.Current);

				LoadTabularParts(configurationDocuments.TabularParts, documentsNode.Current);
			}
		}

		/// <summary>
		/// Збереження конфігурації
		/// </summary>
		/// <param name="pathToConf">Шлях до файлу конфігурації</param>
		/// <param name="Conf">Конфігурація</param>
		public static void Save(string pathToConf, Configuration Conf)
		{
			//string pathToCopyConf = CopyConfigurationFile(pathToConf);

			XmlDocument xmlConfDocument = new XmlDocument();
			xmlConfDocument.AppendChild(xmlConfDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

			XmlElement rootNode = xmlConfDocument.CreateElement("Configuration");
			xmlConfDocument.AppendChild(rootNode);

			SaveConfigurationInfo(Conf, xmlConfDocument, rootNode);

			SaveConstantsBlock(Conf.ConstantsBlock, xmlConfDocument, rootNode);

			SaveDirectories(Conf.Directories, xmlConfDocument, rootNode);

			SaveEnums(Conf.Enums, xmlConfDocument, rootNode);

			SaveDocuments(Conf.Documents, xmlConfDocument, rootNode);

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

		private static void SaveConstantsBlock(Dictionary<string, ConfigurationConstantsBlock> ConfConstantsBlocks, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement rootConstantsBlocks = xmlConfDocument.CreateElement("ConstantsBlocks");
			rootNode.AppendChild(rootConstantsBlocks);

			foreach (KeyValuePair<string, ConfigurationConstantsBlock> ConfConstantsBlock in ConfConstantsBlocks)
			{
				XmlElement rootConstantsBlock = xmlConfDocument.CreateElement("ConstantsBlock");
				rootConstantsBlocks.AppendChild(rootConstantsBlock);

				XmlElement nodeName = xmlConfDocument.CreateElement("Name");
				nodeName.InnerText = ConfConstantsBlock.Key;
				rootConstantsBlock.AppendChild(nodeName);

				XmlElement nodeDesc = xmlConfDocument.CreateElement("Desc");
				nodeDesc.InnerText = ConfConstantsBlock.Value.Desc;
				rootConstantsBlock.AppendChild(nodeDesc);

				SaveConstants(ConfConstantsBlock.Value.Constants, xmlConfDocument, rootConstantsBlock);
			}
		}

		private static void SaveConstants(Dictionary<string, ConfigurationConstants> ConfConstants, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement rootConstants = xmlConfDocument.CreateElement("Constants");
			rootNode.AppendChild(rootConstants);

			foreach (KeyValuePair<string, ConfigurationConstants> ConfConstant in ConfConstants)
			{
				XmlElement rootConstant = xmlConfDocument.CreateElement("Constant");
				rootConstants.AppendChild(rootConstant);

				XmlElement nodeName = xmlConfDocument.CreateElement("Name");
				nodeName.InnerText = ConfConstant.Key;
				rootConstant.AppendChild(nodeName);

				XmlElement nodeDesc = xmlConfDocument.CreateElement("Desc");
				nodeDesc.InnerText = ConfConstant.Value.Desc;
				rootConstant.AppendChild(nodeDesc);

				XmlElement nodeType = xmlConfDocument.CreateElement("Type");
				nodeType.InnerText = ConfConstant.Value.Type;
				rootConstant.AppendChild(nodeType);

				if (ConfConstant.Value.Type == "pointer" || ConfConstant.Value.Type == "enum")
				{
					XmlElement nodePointer = xmlConfDocument.CreateElement("Pointer");
					nodePointer.InnerText = ConfConstant.Value.Pointer;
					rootConstant.AppendChild(nodePointer);
				}
			}
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

				XmlElement nodeEnumSerialNumber = xmlConfDocument.CreateElement("SerialNumber");
				nodeEnumSerialNumber.InnerText = enum_item.Value.SerialNumber.ToString();
				nodeEnum.AppendChild(nodeEnumSerialNumber);

				XmlElement nodeFields = xmlConfDocument.CreateElement("Fields");
				nodeEnum.AppendChild(nodeFields);

				foreach (KeyValuePair<string, ConfigurationEnumField> field in enum_item.Value.Fields)
				{
					XmlElement nodeField = xmlConfDocument.CreateElement("Field");
					nodeFields.AppendChild(nodeField);

					XmlElement nodeFieldName = xmlConfDocument.CreateElement("Name");
					nodeFieldName.InnerText = field.Value.Name;
					nodeField.AppendChild(nodeFieldName);

					XmlElement nodeFieldValue = xmlConfDocument.CreateElement("Value");
					nodeFieldValue.InnerText = field.Value.Value.ToString();
					nodeField.AppendChild(nodeFieldValue);

					XmlElement nodeFieldDesc = xmlConfDocument.CreateElement("Desc");
					nodeFieldDesc.InnerText = field.Value.Desc;
					nodeField.AppendChild(nodeFieldDesc);
				}
			}
		}

		private static void SaveDocuments(Dictionary<string, ConfigurationDocuments> ConfDocuments, XmlDocument xmlConfDocument, XmlElement rootNode)
		{
			XmlElement rootDocuments = xmlConfDocument.CreateElement("Documents");
			rootNode.AppendChild(rootDocuments);

			foreach (KeyValuePair<string, ConfigurationDocuments> ConfDocument in ConfDocuments)
			{
				XmlElement nodeDocument = xmlConfDocument.CreateElement("Document");
				rootDocuments.AppendChild(nodeDocument);

				XmlElement nodeDocumentName = xmlConfDocument.CreateElement("Name");
				nodeDocumentName.InnerText = ConfDocument.Key;
				nodeDocument.AppendChild(nodeDocumentName);

				XmlElement nodeDocumentTable = xmlConfDocument.CreateElement("Table");
				nodeDocumentTable.InnerText = ConfDocument.Value.Table;
				nodeDocument.AppendChild(nodeDocumentTable);

				XmlElement nodeDocumentDesc = xmlConfDocument.CreateElement("Desc");
				nodeDocumentDesc.InnerText = ConfDocument.Value.Desc;
				nodeDocument.AppendChild(nodeDocumentDesc);

				SaveFields(ConfDocument.Value.Fields, xmlConfDocument, nodeDocument);

				SaveTabularParts(ConfDocument.Value.TabularParts, xmlConfDocument, nodeDocument);
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