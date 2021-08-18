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

namespace Configurator
{
    public partial class UnloadingAndLoadingData : Form
    {
        public UnloadingAndLoadingData()
        {
            InitializeComponent();
        }

        public Configuration Conf { get; set; }

        private void buttonUnloadingData_Click(object sender, EventArgs e)
        {
            Conf = Program.Kernel.Conf;

            Thread thread = new Thread(new ThreadStart(UnloadingData));
            thread.Start();

            buttonUnloadingData.Enabled = false;
            richTextBoxInfo.Text = "";
        }

        void UnloadingData()
        {
            foreach (ConfigurationDirectories configurationDirectories in Conf.Directories.Values)
            {
                ApendLine("Довідник: ", configurationDirectories.Name);

                SaveTable(configurationDirectories.Table);
            }

            buttonUnloadingData.Invoke(new Action(() => buttonUnloadingData.Enabled = true));
        }

        private void SaveTable(string table)
        {
            string[] columnsName;
            List<object[]> listRow;

            string query = "SELECT * FROM " + table;
            ApendLine(query, "");
            //Program.Kernel.DataBase.SelectRequest(query, null, out columnsName, out listRow);


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
