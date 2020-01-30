using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        public string Name { get; set; }

        public string NameSpace { get; set; }

        public string Author { get; set; }

        public string PathToXmlFileConfiguration { get; set; }

        public Dictionary<string, ConfigurationConstants> Constants { get; set; }

        public Dictionary<string, ConfigurationDirectories> Directories { get; set; }

        public Dictionary<string, ConfigurationDocuments> Documents { get; set; }

        public Dictionary<string, ConfigurationEnums> Enums { get; set; }

        public Dictionary<string, ConfigurationRegisters> Registers { get; set; }

        public ConfigurationDirectories AppendDirectory(ConfigurationDirectories Directory)
        {
            Directories.Add(Directory.Name, Directory);
            return Directory;
        }

        public static void Load(string pathToConf, Configuration Conf)
        {
            XPathDocument xPathDoc = new XPathDocument(pathToConf);
            XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

            LoadConfigurationInfo(Conf, xPathDocNavigator);

            LoadDirectories(Conf, xPathDocNavigator);
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
            }
        }

        private static void LoadFields(Dictionary<string, ConfigurationObjectField> fields, XPathNavigator xPathDocNavigator)
        {
            XPathNodeIterator fieldNodes = xPathDocNavigator.Select("Fields/Field");
            while (fieldNodes.MoveNext())
            {
                string name = fieldNodes.Current.SelectSingleNode("Name").Value;
                string type = fieldNodes.Current.SelectSingleNode("Type").Value;
                string pointer = (type == "pointer") ? fieldNodes.Current.SelectSingleNode("Pointer").Value : "";
                string desc = fieldNodes.Current.SelectSingleNode("Desc").Value;

                ConfigurationObjectField ConfObjectField = new ConfigurationObjectField(name, type, pointer, desc);

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

        public static void Save(string pathToConf, Configuration Conf)
        {
            XmlDocument xmlConfDocument = new XmlDocument();
            xmlConfDocument.AppendChild(xmlConfDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

            XmlElement rootNode = xmlConfDocument.CreateElement("Configuration");
            xmlConfDocument.AppendChild(rootNode);

            SaveConfigurationInfo(Conf, xmlConfDocument, rootNode);

            SaveDirectories(Conf.Directories, xmlConfDocument, rootNode);

            xmlConfDocument.Save(pathToConf);
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
            nodeDateTimeSave.InnerText = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
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

                XmlElement nodeFieldType = xmlConfDocument.CreateElement("Type");
                nodeFieldType.InnerText = field.Value.Type;
                nodeField.AppendChild(nodeFieldType);

                if (field.Value.Type == "pointer")
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

        public static void Generation(string pathToConf, string pathToTemplate, string pathToSaveCode)
        {
            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(pathToTemplate);

            xsltCodeGnerator.Transform(pathToConf, pathToSaveCode);
        }

        public static void Comparison(string pathToSaveReport, Configuration Conf, ConfigurationInformationSchema InformationSchema)
        {
            XmlDocument xmlComparisonDocument = new XmlDocument();
            xmlComparisonDocument.AppendChild(xmlComparisonDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

            XmlElement rootNode = xmlComparisonDocument.CreateElement("Comparison");
            xmlComparisonDocument.AppendChild(rootNode);

            XmlElement CreateNode = xmlComparisonDocument.CreateElement("Create");
            rootNode.AppendChild(CreateNode);

            XmlElement AlterNode = xmlComparisonDocument.CreateElement("Alter");
            rootNode.AppendChild(AlterNode);

            Console.WriteLine(InformationSchema.Tables.Count);

            foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
            {
                Console.WriteLine(ConfDirectory.Value.Table);

                if (InformationSchema.Tables.ContainsKey(ConfDirectory.Value.Table))
                {
                    foreach (KeyValuePair<string, ConfigurationObjectField> ConfDirectoryField in ConfDirectory.Value.Fields)
                    {
                        if (InformationSchema.Tables[ConfDirectory.Value.Table].Columns.ContainsKey(ConfDirectoryField.Key))
                        {
                            ConfigurationInformationSchema_Column InformationSchemaColumn = 
                                InformationSchema.Tables[ConfDirectory.Value.Table].Columns[ConfDirectoryField.Key];

                            string confType = ConfDirectoryField.Value.Type;

                            string baseType = InformationSchemaColumn.DataType;
                            string baseType2 = InformationSchemaColumn.UdtName;

                            switch (confType)
                            {
                                case "string":
                                    {
                                        if (baseType == "text" && baseType2 == "text")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "string[]":
                                    {
                                        if (baseType == "ARRAY" && baseType2 == "_text")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "integer":
                                    {
                                        if (baseType == "integer" && baseType2 == "int4")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "integer[]":
                                    {
                                        if (baseType == "ARRAY" && baseType2 == "_int4")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "numeric":
                                    {
                                        if (baseType == "numeric" && baseType2 == "numeric")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "numeric[]":
                                    {
                                        if (baseType == "ARRAY" && baseType2 == "_numeric")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "boolean":
                                    {
                                        if (baseType == "boolean" && baseType2 == "bool")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "date":
                                    {
                                        if (baseType == "date" && baseType2 == "date")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "time":
                                    {
                                        if (baseType == "time without time zone" && baseType2 == "time")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "datetime":
                                    {
                                        if (baseType == "timestamp without time zone" && baseType2 == "timestamp")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                case "pointer":
                                    {
                                        if (baseType == "uuid" && baseType2 == "uuid")
                                        {
                                            //ok
                                        }
                                        else
                                        {
                                            //error
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //+field
                            XmlElement alterTableNode = xmlComparisonDocument.CreateElement("Table");
                            AlterNode.AppendChild(alterTableNode);

                            XmlElement alterTableNameNode = xmlComparisonDocument.CreateElement("Name");
                            alterTableNameNode.InnerText = ConfDirectory.Key;
                            alterTableNode.AppendChild(alterTableNameNode);

                            ComparisonField(ConfDirectoryField.Value, xmlComparisonDocument, alterTableNode);
                        }
                    }
                }
                else
                {
                    //+table
                    XmlElement createTableNode = xmlComparisonDocument.CreateElement("Table");
                    CreateNode.AppendChild(createTableNode);

                    XmlElement createTableNameNode = xmlComparisonDocument.CreateElement("Name");
                    createTableNameNode.InnerText = ConfDirectory.Value.Table;
                    createTableNode.AppendChild(createTableNameNode);

                    ComparisonFields(ConfDirectory.Value.Fields, xmlComparisonDocument, createTableNode);
                }
            }

            xmlComparisonDocument.Save(pathToSaveReport);
        }

        private static void ComparisonFields(Dictionary<string, ConfigurationObjectField> fields, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement nodeFields = xmlComparisonDocument.CreateElement("Fields");
            rootNode.AppendChild(nodeFields);

            foreach (KeyValuePair<string, ConfigurationObjectField> field in fields)
            {
                ComparisonField(field.Value, xmlComparisonDocument, nodeFields);
            }
        }

        private static void ComparisonField(ConfigurationObjectField field, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement nodeField = xmlComparisonDocument.CreateElement("Field");
            rootNode.AppendChild(nodeField);

            XmlElement nodeFieldName = xmlComparisonDocument.CreateElement("Name");
            nodeFieldName.InnerText = field.Name;
            nodeField.AppendChild(nodeFieldName);

            XmlElement nodeFieldType = xmlComparisonDocument.CreateElement("Type");
            nodeFieldType.InnerText = field.Type;
            nodeField.AppendChild(nodeFieldType);

            if (field.Type == "pointer")
            {
                XmlElement nodeFieldPointer = xmlComparisonDocument.CreateElement("Pointer");
                nodeFieldPointer.InnerText = field.Pointer;
                nodeField.AppendChild(nodeFieldPointer);
            }

            XmlElement nodeFieldDesc = xmlComparisonDocument.CreateElement("Desc");
            nodeFieldDesc.InnerText = field.Desc;
            nodeField.AppendChild(nodeFieldDesc);
        }
    }
}