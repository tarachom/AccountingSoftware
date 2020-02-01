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



        public static void ComparisonGeneration(string pathToXML, string pathToTemplate, string pathToSaveCode)
        {
            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(pathToTemplate, new XsltSettings(true, true), null);

            xsltCodeGnerator.Transform(pathToXML, pathToSaveCode);
        }

        public static void Comparison(string pathToSaveReport, Configuration Conf, ConfigurationInformationSchema InformationSchema)
        {
            XmlDocument xmlComparisonDocument = new XmlDocument();
            xmlComparisonDocument.AppendChild(xmlComparisonDocument.CreateXmlDeclaration("1.0", "utf-8", ""));

            XmlElement rootNode = xmlComparisonDocument.CreateElement("Comparison");
            xmlComparisonDocument.AppendChild(rootNode);

            ComparisonSaveInformationSchema(InformationSchema, xmlComparisonDocument, rootNode);

            //Таблиці
            foreach (KeyValuePair<string, ConfigurationDirectories> ConfDirectory in Conf.Directories)
            {
                //Назва таблиці в базі даних
                string tableName = ConfDirectory.Value.Table;

                if (InformationSchema.Tables.ContainsKey(tableName))
                {
                    //Поля
                    foreach (KeyValuePair<string, ConfigurationObjectField> ConfDirectoryField in ConfDirectory.Value.Fields)
                    {
                        //Назва поля в базі данихConfigurationObjectField
                        string fieldName = ConfDirectoryField.Key.ToLower();

                        if (InformationSchema.Tables[tableName].Columns.ContainsKey(fieldName))
                        {
                            ConfigurationInformationSchema_Column InformationSchemaColumn = InformationSchema.Tables[tableName].Columns[fieldName];

                            string configurationFieldType = ConfDirectoryField.Value.Type;
                            string dataType = ComparisonGetDataType(ConfDirectoryField.Value.Type);

                            string informationSchemaDataType = InformationSchemaColumn.DataType;
                            string informationSchemaUdtName = InformationSchemaColumn.UdtName;

                            ComparisonTransformDataType(tableName, dataType,
                                informationSchemaDataType, informationSchemaUdtName,
                                ConfDirectoryField.Value, xmlComparisonDocument, rootNode);
                        }
                        else
                        {
                            string dataType = ComparisonGetDataType(ConfDirectoryField.Value.Type);
                            ComparisonSaveAddColumn(tableName, dataType, ConfDirectoryField.Value, xmlComparisonDocument, rootNode);
                        }
                    }
                }
                else
                {
                    ComparisonSaveCreateTable(tableName, ConfDirectory.Value.Fields, xmlComparisonDocument, rootNode);
                }

                foreach (KeyValuePair<string, ConfigurationObjectTablePart> tablePart in ConfDirectory.Value.TabularParts)
                {
                    string tablePartName = tablePart.Value.Table;

                    if (InformationSchema.Tables.ContainsKey(tablePartName))
                    {
                        foreach (KeyValuePair<string, ConfigurationObjectField> tablePartField in tablePart.Value.Fields)
                        {
                            string fieldName = tablePartField.Key.ToLower();
                            Console.WriteLine(fieldName);

                            if (InformationSchema.Tables[tablePartName].Columns.ContainsKey(fieldName))
                            {
                                ConfigurationInformationSchema_Column InformationSchemaColumn =
                                InformationSchema.Tables[tablePartName].Columns[fieldName];

                                string confType = tablePartField.Value.Type;

                                string baseType = InformationSchemaColumn.DataType;
                                string baseType2 = InformationSchemaColumn.UdtName;

                                //Перевірка типу
                            }
                            else
                            {
                                //ComparisonSaveAlterTable(tablePartName, tablePartField.Value, xmlComparisonDocument, rootNode);
                            }
                        }
                    }
                    else
                    {
                        ComparisonSaveCreateTable(tablePartName, tablePart.Value.Fields, xmlComparisonDocument, rootNode);
                    }
                }
            }

            xmlComparisonDocument.Save(pathToSaveReport);
        }

        private static void ComparisonTransformDataType(string tableName, string dataType,
            string informationSchemaDataType, string informationSchemaUdtName,
            ConfigurationObjectField field, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            string configurationFieldType = field.Type;

            if (informationSchemaDataType == "text" && informationSchemaUdtName == "text")
            {
                if (configurationFieldType == "string")
                {
                    //ok
                }
                else if (configurationFieldType == "string[]")
                {
                    
                    ComparisonSaveRenameColumn(tableName, field.Name, field.Name + "_OLD", xmlComparisonDocument, rootNode);
                    ComparisonSaveAddColumn(tableName, dataType, field, xmlComparisonDocument, rootNode);
                    ComparisonSaveCopyDataColumn(tableName, field.Name + "_OLD", field.Name, xmlComparisonDocument, rootNode);

                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "ARRAY" && informationSchemaUdtName == "_text")
            {
                if (configurationFieldType == "string[]")
                {
                    //ok
                }
                else if (configurationFieldType == "string")
                {
                    //1. Переназвати стовпчик в fieldName_OLD
                    //2. Створити новий стовпчик з назвою fieldName з типом string[]
                    //3. Скопіювати дані з fieldName_OLD в fieldName
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "integer" && informationSchemaUdtName == "int4")
            {
                if (configurationFieldType == "integer")
                {
                    //ok
                }
                else if (configurationFieldType == "integer[]")
                {
                    //1. Переназвати стовпчик в fieldName_OLD
                    //2. Створити новий стовпчик з назвою fieldName з типом string[]
                    //3. Скопіювати дані з fieldName_OLD в fieldName
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "ARRAY" && informationSchemaUdtName == "_int4")
            {
                if (configurationFieldType == "integer[]")
                {
                    //ok
                }
                else if (configurationFieldType == "integer")
                {
                    //1. Переназвати стовпчик в fieldName_OLD
                    //2. Створити новий стовпчик з назвою fieldName з типом string[]
                    //3. Скопіювати дані з fieldName_OLD в fieldName
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "numeric" && informationSchemaUdtName == "numeric")
            {
                if (configurationFieldType == "numeric")
                {
                    //ok
                }
                else if (configurationFieldType == "numeric[]")
                {
                    //1. Переназвати стовпчик в fieldName_OLD
                    //2. Створити новий стовпчик з назвою fieldName з типом string[]
                    //3. Скопіювати дані з fieldName_OLD в fieldName
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "ARRAY" && informationSchemaUdtName == "_numeric")
            {
                if (configurationFieldType == "numeric[]")
                {
                    //ok
                }
                else if (configurationFieldType == "numeric")
                {
                    //1. Переназвати стовпчик в fieldName_OLD
                    //2. Створити новий стовпчик з назвою fieldName з типом string[]
                    //3. Скопіювати дані з fieldName_OLD в fieldName
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "boolean" && informationSchemaUdtName == "bool")
            {
                if (configurationFieldType == "boolean")
                {
                    //ok
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "date" && informationSchemaUdtName == "date")
            {
                if (configurationFieldType == "date")
                {
                    //ok
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "time without time zone" && informationSchemaUdtName == "time")
            {
                if (configurationFieldType == "time")
                {
                    //ok
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "timestamp without time zone" && informationSchemaUdtName == "timestamp")
            {
                if (configurationFieldType == "datetime")
                {
                    //ok
                }
                else
                {
                    //error
                }
            }

            if (informationSchemaDataType == "uuid" && informationSchemaUdtName == "uuid")
            {
                if (configurationFieldType == "pointer")
                {
                    //ok
                }
                else
                {
                    //error
                }
            }

            
        }

        private static string ComparisonGetDataType(string configurationFieldType)
        {
            switch (configurationFieldType)
            {
                case "string": return "text";
                case "string[]": return "text[]";
                case "integer": return "integer";
                case "integer[]": return "integer[]";
                case "numeric": return "numeric";
                case "numeric[]": return "numeric[]";
                case "boolean": return "boolean";
                case "date": return "date";
                case "time": return "time without time zone";
                case "datetime": return "timestamp without time zone";
                case "pointer": return "uuid";
                default:
                    throw new Exception("Не оприділений тип даних конфігурації");
            }
        }

        private static void ComparisonFields(Dictionary<string, ConfigurationObjectField> fields, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            foreach (KeyValuePair<string, ConfigurationObjectField> field in fields)
            {
                ComparisonField(field.Value, xmlComparisonDocument, rootNode);
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

            XmlElement nodeFieldDataType = xmlComparisonDocument.CreateElement("DataType");
            nodeFieldDataType.InnerText = ComparisonGetDataType(field.Type);
            nodeField.AppendChild(nodeFieldDataType);
        }

        private static void ComparisonSaveInformationSchema(ConfigurationInformationSchema InformationSchema, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement nodeInformationSchema = xmlComparisonDocument.CreateElement("InformationSchema");
            rootNode.AppendChild(nodeInformationSchema);

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
        }

        private static void ComparisonSaveCreateTable(string tableName, Dictionary<string, ConfigurationObjectField> fields, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement createTableNode = xmlComparisonDocument.CreateElement("CreateTable");
            rootNode.AppendChild(createTableNode);

            XmlElement createTableNameNode = xmlComparisonDocument.CreateElement("Name");
            createTableNameNode.InnerText = tableName;
            createTableNode.AppendChild(createTableNameNode);

            ComparisonFields(fields, xmlComparisonDocument, createTableNode);
        }

        private static void ComparisonSaveAddColumn(string tableName, string dataType, ConfigurationObjectField field, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            //ALTER TABLE distributors ADD COLUMN address varchar(30);

            XmlElement nodeTable = xmlComparisonDocument.CreateElement("AddColumn");
            rootNode.AppendChild(nodeTable);

            XmlElement nodeTableName = xmlComparisonDocument.CreateElement("TableName");
            nodeTableName.InnerText = tableName;
            nodeTable.AppendChild(nodeTableName);

            XmlElement nodeField = xmlComparisonDocument.CreateElement("Field");
            nodeTable.AppendChild(nodeField);

            XmlElement nodeFieldName = xmlComparisonDocument.CreateElement("Name");
            nodeFieldName.InnerText = field.Name;
            nodeField.AppendChild(nodeFieldName);

            XmlElement nodeFieldType = xmlComparisonDocument.CreateElement("Type");
            nodeFieldType.InnerText = field.Type;
            nodeField.AppendChild(nodeFieldType);

            XmlElement nodeFieldDataType = xmlComparisonDocument.CreateElement("DataType");
            nodeFieldDataType.InnerText = dataType;
            nodeField.AppendChild(nodeFieldDataType);
        }

        private static void ComparisonSaveRenameColumn(string tableName, string fieldName, string newFieldName, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            //ALTER TABLE public.test RENAME uid TO uid2;

            XmlElement ColumnNode = xmlComparisonDocument.CreateElement("RenameColumn");
            rootNode.AppendChild(ColumnNode);

            XmlElement TableNameNode = xmlComparisonDocument.CreateElement("TableName");
            TableNameNode.InnerText = tableName;
            ColumnNode.AppendChild(TableNameNode);

            XmlElement FieldNode = xmlComparisonDocument.CreateElement("Field");
            ColumnNode.AppendChild(FieldNode);

            XmlElement FieldNameNode = xmlComparisonDocument.CreateElement("Name");
            FieldNameNode.InnerText = fieldName;
            FieldNode.AppendChild(FieldNameNode);

            XmlElement FieldNewNameNode = xmlComparisonDocument.CreateElement("NewName");
            FieldNewNameNode.InnerText = newFieldName;
            FieldNode.AppendChild(FieldNewNameNode);
        }

        private static void ComparisonSaveAlterColumn(string tableName, string dataType, ConfigurationObjectField field, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement alterTableNode = xmlComparisonDocument.CreateElement("AlterColumn");
            rootNode.AppendChild(alterTableNode);

            XmlElement alterTableNameNode = xmlComparisonDocument.CreateElement("TableName");
            alterTableNameNode.InnerText = tableName;
            alterTableNode.AppendChild(alterTableNameNode);

            XmlElement nodeField = xmlComparisonDocument.CreateElement("Field");
            rootNode.AppendChild(nodeField);

            XmlElement nodeFieldName = xmlComparisonDocument.CreateElement("Name");
            nodeFieldName.InnerText = field.Name;
            nodeField.AppendChild(nodeFieldName);

            XmlElement nodeFieldType = xmlComparisonDocument.CreateElement("Type");
            nodeFieldType.InnerText = field.Type;
            nodeField.AppendChild(nodeFieldType);

            XmlElement nodeFieldDataType = xmlComparisonDocument.CreateElement("DataType");
            nodeFieldType.InnerText = dataType;
            nodeField.AppendChild(nodeFieldDataType);
        }

        private static void ComparisonSaveCopyDataColumn(string tableName, string fieldNameSource, string fieldNameDestination, XmlDocument xmlComparisonDocument, XmlElement rootNode)
        {
            XmlElement ColumnNode = xmlComparisonDocument.CreateElement("CopyDataColumn");
            rootNode.AppendChild(ColumnNode);

            XmlElement TableNameNode = xmlComparisonDocument.CreateElement("TableName");
            TableNameNode.InnerText = tableName;
            ColumnNode.AppendChild(TableNameNode);

            XmlElement FieldNode = xmlComparisonDocument.CreateElement("Field");
            ColumnNode.AppendChild(FieldNode);

            XmlElement FieldNameNode = xmlComparisonDocument.CreateElement("NameSource");
            FieldNameNode.InnerText = fieldNameSource;
            FieldNode.AppendChild(FieldNameNode);

            XmlElement FieldNewNameNode = xmlComparisonDocument.CreateElement("NameDestination");
            FieldNewNameNode.InnerText = fieldNameDestination;
            FieldNode.AppendChild(FieldNewNameNode);
        }

        //ALTER TABLE public.test DROP COLUMN num;

    }
}
 