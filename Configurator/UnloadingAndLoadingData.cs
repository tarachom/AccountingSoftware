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
            Thread thread = new Thread(new ThreadStart(UnloadingData));
            thread.Start();

            buttonUnloadingData.Enabled = false;
            richTextBoxInfo.Text = "";
        }

        private void buttonLoadingData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(LoadingData));
            thread.Start();

            buttonLoadingData.Enabled = false;
            richTextBoxInfo.Text = "";
        }

        void UnloadingData()
        {
            ApendLine("ДОВІДНИКИ", "");

            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine(" --> Довідник: ", configurationDirectories.Name);

                SaveTable(configurationDirectories.Table);
            }

            ApendLine("РЕГІСТРИ", "");

            foreach (ConfigurationRegistersAccumulation configurationRegistersAccumulation in Conf.RegistersAccumulation.Values)
            {
                ApendLine(" --> Регістер накопичення: ", configurationRegistersAccumulation.Name);

                SaveTable(configurationRegistersAccumulation.Table);
            }

            buttonUnloadingData.Invoke(new Action(() => buttonUnloadingData.Enabled = true));

            ApendLine("", "");
            ApendLine("Готово!", "");
        }

        void LoadingData()
        {
            ApendLine("ДОВІДНИКИ", "");

            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine(" --> Довідник: ", configurationDirectories.Name);

                LoadTable(configurationDirectories.Table, configurationDirectories);
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

                ApendLine(" --> " + table + ": ", Program.Kernel.DataBase.InsertSQL(table, param).ToString());

            }
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

        private void ApendLine(string head, string bodySelect, string futer = "")
        {
            if (richTextBoxInfo.InvokeRequired)
            {
                richTextBoxInfo.Invoke(new Action<string, string, string>(ApendLine), head, bodySelect, futer);
            }
            else
            {
                richTextBoxInfo.AppendText(head);

                if (!String.IsNullOrEmpty(bodySelect))
                {
                    richTextBoxInfo.SelectionFont = new Font("Consolas"/*"Microsoft Sans Serif"*/, 10, FontStyle.Underline);
                    richTextBoxInfo.SelectionColor = Color.DarkBlue;
                    richTextBoxInfo.AppendText(bodySelect);
                }

                if (!String.IsNullOrEmpty(bodySelect))
                {
                    richTextBoxInfo.SelectionFont = new Font("Consolas", 10);
                    richTextBoxInfo.SelectionColor = Color.Black;
                }

                richTextBoxInfo.AppendText(" " + futer + "\n");
                richTextBoxInfo.ScrollToCaret();
            }
        }
        
    }
}
