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

                Thread thread = new Thread(new ParameterizedThreadStart(ImportData));
                thread.Start(fileImport);

                buttonLoadingData.Enabled = false;
                richTextBoxInfo.Text = "";
            }
        }

        #region Export

        void ExportData(object fileExport)
        {
            ApendLine("Файл вигрузки: " + fileExport.ToString() + "\n\n");

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

                    //WriteTabularPartsInfo(xmlWriter, configurationConstants.TabularParts);

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

                //WriteFieldsInfo(xmlWriter, configurationDirectories.Fields);
                //WriteTabularPartsInfo(xmlWriter, configurationDirectories.TabularParts);

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

                //WriteFieldsInfo(xmlWriter, configurationDocuments.Fields);
                //WriteTabularPartsInfo(xmlWriter, configurationDocuments.TabularParts);

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

                //xmlWriter.WriteStartElement("DimensionFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersInformation.DimensionFields);
                //xmlWriter.WriteEndElement();

                //xmlWriter.WriteStartElement("ResourcesFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersInformation.ResourcesFields);
                //xmlWriter.WriteEndElement();

                //xmlWriter.WriteStartElement("PropertyFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersInformation.PropertyFields);
                //xmlWriter.WriteEndElement();

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

                //xmlWriter.WriteStartElement("DimensionFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.DimensionFields);
                //xmlWriter.WriteEndElement();

                //xmlWriter.WriteStartElement("ResourcesFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.ResourcesFields);
                //xmlWriter.WriteEndElement();

                //xmlWriter.WriteStartElement("PropertyFields");
                //WriteFieldsInfo(xmlWriter, configurationRegistersAccumulation.PropertyFields);
                //xmlWriter.WriteEndElement();

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
                    if (row[counter].GetType().Name == "DBNull")
                        continue;

                    string typeName = row[counter].GetType().Name;

                    xmlWriter.WriteStartElement(column);
                    xmlWriter.WriteAttributeString("type", typeName);

                    switch (typeName)
                    {
                        case "String[]":
                            {
                                xmlWriter.WriteRaw(ArrayToXml<string>.Convert((string[])row[counter]));
                                break;
                            }
                        case "Int32[]":
                            {
                                xmlWriter.WriteRaw(ArrayToXml<int>.Convert((int[])row[counter]));
                                break;
                            }
                        case "Decimal[]":
                            {
                                xmlWriter.WriteRaw(ArrayToXml<decimal>.Convert((decimal[])row[counter]));
                                break;
                            }
                        default:
                            {
                                xmlWriter.WriteString(row[counter].ToString());
                                break;
                            }
                    }

                    xmlWriter.WriteEndElement();
                    counter++;
                }
                xmlWriter.WriteEndElement();
            }

            xmlWriter.Flush();
        }

        #endregion

        #region Import

        void ImportData(object fileImport)
        {
            ApendLine("Файл загрузки: " + fileImport.ToString() + "\n\n");

            ApendLine("Аналіз: ");

            ApendLine(" --> Крок 1");
            string pathToXmlResultStepOne = TransformXmlDataStepOne(fileImport.ToString());

            ApendLine(" --> Крок 2");
            string pathToXmlResultStepSQL = TransformStepOneToStepSQL(fileImport.ToString(), pathToXmlResultStepOne);

            ApendLine("Виконання команд: ");

            ExecuteSqlList(pathToXmlResultStepSQL);

            ApendLine(" --> Готово!");
        }

        string TransformXmlDataStepOne(string fileImport)
        {
            string pathToTemplate = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UnloadingAndLoadingDataXML.xslt");
            string pathToDirFileImport = Path.GetDirectoryName(fileImport);
            string pathToXmlResult = Path.Combine(pathToDirFileImport, "stepone_" + Guid.NewGuid().ToString().Replace("-", "") + ".xml");

            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(pathToTemplate, new XsltSettings(false, false), null);

            XsltArgumentList xsltArgumentList = new XsltArgumentList();

            FileStream fileStream = new FileStream(pathToXmlResult, FileMode.Create);

            xsltCodeGnerator.Transform(fileImport, xsltArgumentList, fileStream);

            fileStream.Close();

            return pathToXmlResult;
        }

        string TransformStepOneToStepSQL(string fileImport, string fileStepOne)
        {
            string pathToTemplate = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UnloadingAndLoadingDataSQL.xslt");
            string pathToDirFileImport = Path.GetDirectoryName(fileImport);
            string pathToXmlResult = Path.Combine(pathToDirFileImport, "stepsql_" + Guid.NewGuid().ToString().Replace("-", "") + ".xml");

            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(pathToTemplate, new XsltSettings(false, false), null);

            XsltArgumentList xsltArgumentList = new XsltArgumentList();

            FileStream fileStream = new FileStream(pathToXmlResult, FileMode.Create);

            xsltCodeGnerator.Transform(fileStepOne, xsltArgumentList, fileStream);

            fileStream.Close();

            return pathToXmlResult;
        }

        public void ExecuteSqlList(string fileStepSQL)
        {
            XPathDocument xPathDoc = new XPathDocument(fileStepSQL);
            XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

            XPathNodeIterator rowNodes = xPathDocNavigator.Select("/root/row");
            while (rowNodes.MoveNext())
            {
                XPathNavigator sqlNode = rowNodes.Current.SelectSingleNode("sql");
                string sqlText = sqlNode.Value;

                Dictionary<string, object> param = new Dictionary<string, object>();

                XPathNodeIterator paramNodes = rowNodes.Current.Select("p");
                while (paramNodes.MoveNext())
                {
                    string paramName = paramNodes.Current.GetAttribute("name", "");
                    string paramType = paramNodes.Current.GetAttribute("type", "");

                    string paramValue = paramNodes.Current.Value;
                    object paramObj;

                    switch (paramType)
                    {
                        case "Guid":
                            {
                                paramObj = Guid.Parse(paramValue);
                                break;
                            }
                        case "Int32":
                            {
                                paramObj = int.Parse(paramValue);
                                break;
                            }
                        case "DateTime":
                            {
                                paramObj = DateTime.Parse(paramValue);
                                break;
                            }
                        case "TimeSpan":
                            {
                                paramObj = TimeSpan.Parse(paramValue);
                                break;
                            }
                        case "Boolean":
                            {
                                paramObj = Boolean.Parse(paramValue);
                                break;
                            }
                        case "Decimal":
                            {
                                paramObj = Decimal.Parse(paramValue);
                                break;
                            }
                        case "String":
                            {
                                paramObj = paramValue;
                                break;
                            }
                        case "String[]":
                            {
                                paramObj = ArrayToXml.Convert(paramNodes.Current.InnerXml);
                                break;
                            }
                        case "Int32[]":
                            {
                                string[] tmpValue = ArrayToXml.Convert(paramNodes.Current.InnerXml);
                                int[] tmpIntValue = new int[tmpValue.Length];

                                for (int i=0; i< tmpValue.Length; i++)
                                    tmpIntValue[i] = int.Parse(tmpValue[i]);

                                paramObj = tmpIntValue;
                                break;
                            }
                        case "Decimal[]":
                            {
                                string[] tmpValue = ArrayToXml.Convert(paramNodes.Current.InnerXml);
                                decimal[] tmpDecimalValue = new decimal[tmpValue.Length];

                                for (int i = 0; i < tmpValue.Length; i++)
                                    tmpDecimalValue[i] = decimal.Parse(tmpValue[i]);

                                paramObj = tmpDecimalValue;
                                break;
                            }
                        default:
                            {
                                ApendLine("Не оприділений тип: " + paramType);
                                paramObj = paramValue;
                                break;
                            }
                    }

                    param.Add(paramName, paramObj);
                }

                int result = Program.Kernel.DataBase.ExecuteSQL(sqlText, param);
                ApendInfo(".");
            }
        }

        #endregion

        private void ApendLine(string text)
        {
            if (richTextBoxInfo.InvokeRequired)
            {
                richTextBoxInfo.Invoke(new Action<string>(ApendLine), text);
            }
            else
            {
                richTextBoxInfo.AppendText("\n" + text);
                //richTextBoxInfo.ScrollToCaret();
            }
        }

        private void ApendInfo(string text)
        {
            if (richTextBoxInfo.InvokeRequired)
            {
                richTextBoxInfo.Invoke(new Action<string>(ApendInfo), text);
            }
            else
            {
                richTextBoxInfo.AppendText(text);
            }
        }

    }
}
