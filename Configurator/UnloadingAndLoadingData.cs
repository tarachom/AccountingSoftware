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
            Conf = Program.Kernel.Conf;
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
            Thread thread = new Thread(new ThreadStart(LoadingData));
            thread.Start();

            buttonLoadingData.Enabled = false;
            richTextBoxInfo.Text = "";
        }

        void ExportData(object fileExport)
        {
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };

            XmlWriter xmlWriter = XmlWriter.Create(fileExport.ToString(), settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");

            ApendLine("ДОВІДНИКИ");

            xmlWriter.WriteStartElement("Directories");
            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine(" --> Довідник: " + configurationDirectories.Name);

                xmlWriter.WriteStartElement("Directory");
                xmlWriter.WriteAttributeString("name", configurationDirectories.Name);
                xmlWriter.WriteAttributeString("tab", configurationDirectories.Table);

                string all_fields;

                WriteFields(xmlWriter, configurationDirectories.Fields, out all_fields);
                WriteQuerySelect(xmlWriter, $@"SELECT uid{all_fields} FROM {configurationDirectories.Table}");

                xmlWriter.WriteStartElement("TabularParts");
                foreach (ConfigurationObjectTablePart tablePart in configurationDirectories.TabularParts.Values)
                {
                    WriteTablePart(xmlWriter, tablePart, out all_fields);
                    WriteQuerySelect(xmlWriter, $@"SELECT uid, owner{all_fields} FROM {tablePart.Table}");
                }
                xmlWriter.WriteEndElement(); //TabularParts
                xmlWriter.WriteEndElement(); //Directory
            }
            xmlWriter.WriteEndElement();

            ApendLine("\nРЕГІСТРИ");

            foreach (ConfigurationRegistersAccumulation configurationRegistersAccumulation in Conf.RegistersAccumulation.Values)
            {
                ApendLine(" --> Регістер накопичення: " + configurationRegistersAccumulation.Name);

                //SaveTable(configurationRegistersAccumulation.Table);
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            buttonUnloadingData.Invoke(new Action(() => buttonUnloadingData.Enabled = true));

            ApendLine("");
            ApendLine("Готово!");
        }

        void WriteFields(XmlWriter xmlWriter, Dictionary<string, ConfigurationObjectField> fields, out string guery_all_fields)
        {
            guery_all_fields = "";

            xmlWriter.WriteStartElement("Fields");
            foreach (ConfigurationObjectField field in fields.Values)
            {
                guery_all_fields += $", {field.NameInTable}";

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

        void WriteTablePart(XmlWriter xmlWriter, ConfigurationObjectTablePart tablePart, out string guery_all_fields)
        {
            xmlWriter.WriteStartElement("TablePart");
            xmlWriter.WriteAttributeString("name", tablePart.Name);
            xmlWriter.WriteAttributeString("tab", tablePart.Table);

            WriteFields(xmlWriter, tablePart.Fields, out guery_all_fields);

            xmlWriter.WriteEndElement();
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
