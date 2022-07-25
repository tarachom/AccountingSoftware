using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using AccountingSoftware;
using System.IO;


namespace Configurator
{
    public partial class UnloadingAndLoadingData : Form
    {
        public UnloadingAndLoadingData()
        {
            InitializeComponent();
        }

        public Configuration Conf { get; set; }

        private void UnloadingAndLoadingData_Load(object sender, EventArgs e)
        {

        }

        private void buttonUnloadingData_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "StorageAndTrade_Export_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xml";
            saveFileDialog.Filter = "XML|*.xml";
            saveFileDialog.Title = "Файл для вигрузки даних";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();

            if (!(saveFileDialog.ShowDialog() == DialogResult.OK))
                return;
            else
            {
                string fileExport = saveFileDialog.FileName;

                Thread thread = new Thread(new ParameterizedThreadStart(ExportData));
                thread.Start(fileExport);

                buttonUnloadingData.Enabled = false;
                richTextBoxInfo.Text = "";
            }
        }

        private void buttonLoadingData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML|*.xml";
            openFileDialog.Title = "Файл для загрузки даних";
            openFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();

            if (!(openFileDialog.ShowDialog() == DialogResult.OK))
                return;
            else
            {
                string fileImport = openFileDialog.FileName;

                Thread thread = new Thread(new ParameterizedThreadStart(ImportXSLT));
                thread.Start(fileImport);

                buttonLoadingData.Enabled = false;
                richTextBoxInfo.Text = "";
            }
        }

        #region Import

        void ImportXSLT(object fileImport)
        {
           string dir = Path.GetDirectoryName(fileImport.ToString());

            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(@"E:\Project\AccountingSoftware\Configurator\LoadingData.xslt", new XsltSettings(true, true), null);

            XsltArgumentList xsltArgumentList = new XsltArgumentList();

            FileStream fileStream = new FileStream(Path.Combine(dir, "sql_load.xml"), FileMode.Create);

            xsltCodeGnerator.Transform(fileImport.ToString(), xsltArgumentList, fileStream);

            fileStream.Close();
        }

        void Import(object fileImport)
        {
            XPathDocument xPathDocument = new XPathDocument(fileImport.ToString());
            XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();

            XPathNavigator rootNode = xPathNavigator.SelectSingleNode("root");

            XPathNodeIterator ConstantNode = rootNode.Select("Constants/Constant");
            while (ConstantNode.MoveNext())
            {
                XPathNavigator CurrentNode = ConstantNode.Current;

                string Name = CurrentNode.GetAttribute("name", "");
                string Col = CurrentNode.GetAttribute("col", "");

                XPathNavigator rowNode = CurrentNode.SelectSingleNode($"row/{Col}");

                if (rowNode != null)
                {

                }

                XPathNodeIterator TablePartNode = rootNode.Select("Constants/Constant");
                while (TablePartNode.MoveNext())
                {
                    XPathNavigator CurrentTablePartNode = TablePartNode.Current;

                    string TablePart_Name = CurrentNode.GetAttribute("name", "");
                    string TablePart_Tab = CurrentNode.GetAttribute("tab", "");

                    XPathNodeIterator rowTablePartNode = CurrentTablePartNode.Select("row");

                }
            }
        }

        #endregion

        #region Export

        void ExportData(object fileExport)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };

            XmlWriter xmlWriter = XmlWriter.Create(fileExport.ToString(), settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");

            ApendLine("КОНСТАНТИ");

            xmlWriter.WriteStartElement("Constants");
            foreach (ConfigurationConstantsBlock configurationConstantsBlock in Conf.ConstantsBlock.Values)
            {
                ApendLine(configurationConstantsBlock.BlockName);

                foreach (ConfigurationConstants configurationConstants in configurationConstantsBlock.Constants.Values)
                {
                    ApendLine(" --> Константа: " + configurationConstants.Name);

                    xmlWriter.WriteStartElement("Constant");
                    xmlWriter.WriteAttributeString("name", configurationConstants.Name);
                    xmlWriter.WriteAttributeString("col", configurationConstants.NameInTable);

                    WriteTabularPartsInfo(xmlWriter, configurationConstants.TabularParts);

                    foreach (ConfigurationObjectTablePart tablePart in configurationConstants.TabularParts.Values)
                    {
                        xmlWriter.WriteStartElement("TablePart");
                        xmlWriter.WriteAttributeString("name", tablePart.Name);
                        xmlWriter.WriteAttributeString("tab", tablePart.Table);

                        WriteQuerySelect(xmlWriter, $@"SELECT uid{GetAllFields(tablePart.Fields)} FROM {tablePart.Table}");

                        xmlWriter.WriteEndElement();
                    }

                    WriteQuerySelect(xmlWriter, $@"SELECT {configurationConstants.NameInTable} FROM tab_constants");

                    xmlWriter.WriteEndElement(); //Constant
                }
            }
            xmlWriter.WriteEndElement(); //Constants

            ApendLine("ДОВІДНИКИ");

            xmlWriter.WriteStartElement("Directories");
            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine(" --> Довідник: " + configurationDirectories.Name);

                xmlWriter.WriteStartElement("Directory");
                xmlWriter.WriteAttributeString("name", configurationDirectories.Name);
                xmlWriter.WriteAttributeString("tab", configurationDirectories.Table);

                WriteFieldsInfo(xmlWriter, configurationDirectories.Fields);
                WriteTabularPartsInfo(xmlWriter, configurationDirectories.TabularParts);

                WriteQuerySelect(xmlWriter, $@"SELECT uid{GetAllFields(configurationDirectories.Fields)} FROM {configurationDirectories.Table}");

                foreach (ConfigurationObjectTablePart tablePart in configurationDirectories.TabularParts.Values)
                {
                    xmlWriter.WriteStartElement("TablePart");
                    xmlWriter.WriteAttributeString("name", tablePart.Name);
                    xmlWriter.WriteAttributeString("tab", tablePart.Table);

                    WriteQuerySelect(xmlWriter, $@"SELECT uid, owner{GetAllFields(tablePart.Fields)} FROM {tablePart.Table}");

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement(); //Directory
            }
            xmlWriter.WriteEndElement(); //Directories

            ApendLine("ДОКУМЕНТИ");

            xmlWriter.WriteStartElement("Documents");
            foreach (ConfigurationDocuments configurationDocuments in Conf.Documents.Values)
            {
                ApendLine(" --> Документ: " + configurationDocuments.Name);

                xmlWriter.WriteStartElement("Document");
                xmlWriter.WriteAttributeString("name", configurationDocuments.Name);
                xmlWriter.WriteAttributeString("tab", configurationDocuments.Table);

                WriteFieldsInfo(xmlWriter, configurationDocuments.Fields);
                WriteTabularPartsInfo(xmlWriter, configurationDocuments.TabularParts);

                WriteQuerySelect(xmlWriter, $@"SELECT uid, spend, spend_date{GetAllFields(configurationDocuments.Fields)} FROM {configurationDocuments.Table}");

                foreach (ConfigurationObjectTablePart tablePart in configurationDocuments.TabularParts.Values)
                {
                    xmlWriter.WriteStartElement("TablePart");
                    xmlWriter.WriteAttributeString("name", tablePart.Name);
                    xmlWriter.WriteAttributeString("tab", tablePart.Table);

                    WriteQuerySelect(xmlWriter, $@"SELECT uid, owner{GetAllFields(tablePart.Fields)} FROM {tablePart.Table}");

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement(); //Document
            }
            xmlWriter.WriteEndElement(); //Documents

            ApendLine("РЕГІСТРИ ІНФОРМАЦІЇ");

            xmlWriter.WriteStartElement("RegistersInformation");
            foreach (ConfigurationRegistersInformation configurationRegistersInformation in Conf.RegistersInformation.Values)
            {
                ApendLine(" --> Регістр: " + configurationRegistersInformation.Name);

                xmlWriter.WriteStartElement("Register");
                xmlWriter.WriteAttributeString("name", configurationRegistersInformation.Name);
                xmlWriter.WriteAttributeString("tab", configurationRegistersInformation.Table);

                xmlWriter.WriteStartElement("DimensionFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersInformation.DimensionFields);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ResourcesFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersInformation.ResourcesFields);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PropertyFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersInformation.PropertyFields);
                xmlWriter.WriteEndElement();

                string query_fields = GetAllFields(configurationRegistersInformation.DimensionFields) +
                    GetAllFields(configurationRegistersInformation.ResourcesFields) +
                    GetAllFields(configurationRegistersInformation.PropertyFields);

                WriteQuerySelect(xmlWriter, $@"SELECT uid, period, owner{query_fields} FROM {configurationRegistersInformation.Table}");

                xmlWriter.WriteEndElement(); //Register
            }
            xmlWriter.WriteEndElement(); //RegistersInformation

            ApendLine("РЕГІСТРИ НАКОПИЧЕННЯ");

            xmlWriter.WriteStartElement("RegistersAccumulation");
            foreach (ConfigurationRegistersAccumulation configurationRegistersAccumulation in Conf.RegistersAccumulation.Values)
            {
                ApendLine(" --> Регістр: " + configurationRegistersAccumulation.Name);

                xmlWriter.WriteStartElement("Register");
                xmlWriter.WriteAttributeString("name", configurationRegistersAccumulation.Name);
                xmlWriter.WriteAttributeString("tab", configurationRegistersAccumulation.Table);

                xmlWriter.WriteStartElement("DimensionFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.DimensionFields);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ResourcesFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.ResourcesFields);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PropertyFields");
                WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.PropertyFields);
                xmlWriter.WriteEndElement();

                string query_fields = GetAllFields(configurationRegistersAccumulation.DimensionFields) +
                    GetAllFields(configurationRegistersAccumulation.ResourcesFields) +
                    GetAllFields(configurationRegistersAccumulation.PropertyFields);

                WriteQuerySelect(xmlWriter, $@"SELECT uid, period, income, owner{query_fields} FROM {configurationRegistersAccumulation.Table}");

                xmlWriter.WriteEndElement(); //Register
            }
            xmlWriter.WriteEndElement(); //RegistersAccumulation

            xmlWriter.WriteEndElement(); //root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            buttonUnloadingData.Invoke(new Action(() => buttonUnloadingData.Enabled = true));

            ApendLine("");
            ApendLine("Готово!");
        }

        string GetAllFields(Dictionary<string, ConfigurationObjectField> fields)
        {
            string guery_fields = "";

            foreach (ConfigurationObjectField field in fields.Values)
                guery_fields += $", {field.NameInTable}";

            return guery_fields;
        }

        void WriteFieldsInfo(XmlWriter xmlWriter, Dictionary<string, ConfigurationObjectField> fields)
        {
            xmlWriter.WriteStartElement("FieldInfo");
            foreach (ConfigurationObjectField field in fields.Values)
            {
                xmlWriter.WriteStartElement("Field");
                xmlWriter.WriteAttributeString("name", field.Name);
                xmlWriter.WriteAttributeString("col", field.NameInTable);
                xmlWriter.WriteAttributeString("type", field.Type);
                if (field.Type == "pointer" || field.Type == "enum")
                    xmlWriter.WriteAttributeString("pointer", field.Pointer);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }

        void WriteTablePartInfo(XmlWriter xmlWriter, ConfigurationObjectTablePart tablePart)
        {
            xmlWriter.WriteStartElement("TablePart");
            xmlWriter.WriteAttributeString("name", tablePart.Name);
            xmlWriter.WriteAttributeString("tab", tablePart.Table);

            WriteFieldsInfo(xmlWriter, tablePart.Fields);

            xmlWriter.WriteEndElement();
        }

        void WriteTabularPartsInfo(XmlWriter xmlWriter, Dictionary<string, ConfigurationObjectTablePart> tabularParts)
        {
            if (tabularParts.Count > 0)
            {
                xmlWriter.WriteStartElement("TabularPartsInfo");
                foreach (ConfigurationObjectTablePart tablePart in tabularParts.Values)
                    WriteTablePartInfo(xmlWriter, tablePart);
                xmlWriter.WriteEndElement(); //TabularPartsInfo
            }
        }

        void WriteQuerySelect(XmlWriter xmlWriter, string query)
        {
            string[] columnsName;
            List<object[]> listRow;

            Program.Kernel.DataBase.SelectRequest(query, null, out columnsName, out listRow);

            foreach (object[] row in listRow)
            {
                int counter = 0;

                xmlWriter.WriteStartElement("row");
                foreach (string column in columnsName)
                {
                    if (String.IsNullOrWhiteSpace(row[counter].ToString()))
                        continue;

                    xmlWriter.WriteStartElement(column);
                    xmlWriter.WriteString(row[counter].ToString());
                    xmlWriter.WriteEndElement();
                    counter++;
                }
                xmlWriter.WriteEndElement();
            }

            xmlWriter.Flush();
        }

        #endregion


        void SaveTable(string table)
        {
            string pathToUnloadBase = @"E:\ВигрузкаБази\" + table + ".xml";

            string[] columnsName;
            List<object[]> listRow;

            string query = "SELECT * FROM " + table;

            Program.Kernel.DataBase.SelectRequest(query, null, out columnsName, out listRow);

            StreamWriter sw = new StreamWriter(pathToUnloadBase);
            sw.AutoFlush = true;

            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sw.WriteLine("<ВигрузкаДаних>");
            sw.WriteLine("<Таблиця>" + table + "</Таблиця>");

            sw.WriteLine("<Колонки>");

            for (int k = 0; k < columnsName.Length; k++)
            {
                sw.Write("<Колонка>");
                sw.Write("<Назва>" + columnsName[k] + "</Назва>");
                sw.Write("<КороткаНазва>c" + k.ToString() + "</КороткаНазва>");
                sw.WriteLine("</Колонка>");
            }

            sw.WriteLine("</Колонки>");
            sw.WriteLine("<Записи>");

            foreach (object[] o in listRow)
            {
                sw.Write("<row>");

                for (int i = 0; i < o.Length; i++)
                    sw.Write("<c" + i.ToString() + ">" + o[i].ToString() + "</c" + i.ToString() + ">");

                sw.WriteLine("</row>");
            }

            sw.WriteLine("</Записи>");
            sw.WriteLine("</ВигрузкаДаних>");
            sw.Close();
        }

        void LoadingData()
        {
            ApendLine("ДОВІДНИКИ");

            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine(" --> Довідник: " + configurationDirectories.Name);

                //LoadTable(configurationDirectories.Table, configurationDirectories);
            }
        }

        void LoadTable(string table, ConfigurationDirectories configurationDirectory)
        {
            string pathToLoadBase = @"E:\ВигрузкаБази\" + table + ".xml";

            XPathDocument xPathDoc = new XPathDocument(pathToLoadBase);
            XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

            XPathNavigator rootNode = xPathDocNavigator.SelectSingleNode("/ВигрузкаДаних");

            Dictionary<string, string> columnsList = LoadColumns(rootNode);

            XPathNavigator rowsNode = rootNode.SelectSingleNode("Записи");
            LoadRows(rowsNode, table, columnsList, configurationDirectory);
        }

        Dictionary<string, string> LoadColumns(XPathNavigator rootNode)
        {
            Dictionary<string, string> columnsList = new Dictionary<string, string>();

            XPathNodeIterator columnsNodeList = rootNode.Select("Колонки/Колонка");
            while (columnsNodeList.MoveNext())
            {
                string columnNodeName = columnsNodeList.Current.SelectSingleNode("Назва").Value;
                string columnNodeAlias = columnsNodeList.Current.SelectSingleNode("КороткаНазва").Value;

                columnsList.Add(columnNodeName, columnNodeAlias);
            }

            return columnsList;
        }

        void LoadRows(XPathNavigator rootNode, string table, Dictionary<string, string> columnsList, ConfigurationDirectories configurationDirectory)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            XPathNodeIterator rowNodeList = rootNode.Select("row");
            while (rowNodeList.MoveNext())
            {
                param.Clear();

                foreach (KeyValuePair<string, string> columnItem in columnsList)
                {
                    string columnValue = rowNodeList.Current.SelectSingleNode(columnItem.Value).Value;

                    string columnType = configurationDirectory.Fields[columnItem.Key].Type;

                    object obj;

                    switch (columnType)
                    {
                        case "integer":
                        case "enum":
                            {
                                obj = int.Parse(columnValue);
                                break;
                            }
                        case "numeric":
                            {
                                obj = decimal.Parse(columnValue);
                                break;
                            }
                        case "boolean":
                            {
                                obj = bool.Parse(columnValue);
                                break;
                            }
                        case "date":
                        case "datetime":
                        case "time":
                            {
                                obj = DateTime.Parse(columnValue);
                                break;
                            }
                        case "pointer":
                        case "empty_pointer":
                            {
                                obj = Guid.Parse(columnValue);
                                break;
                            }
                        default:
                            {
                                throw new Exception("Error type");
                            }
                    }

                    param.Add(columnItem.Key, obj);
                }

                ApendLine(" --> " + table + ": " + Program.Kernel.DataBase.InsertSQL(table, param).ToString());

            }
        }

        private void ApendLine(string text)
        {
            if (richTextBoxInfo.InvokeRequired)
            {
                richTextBoxInfo.Invoke(new Action<string>(ApendLine), text);
            }
            else
            {
                richTextBoxInfo.AppendText("\n" + text);
            }
        }

    }
}
