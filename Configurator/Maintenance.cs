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
    public partial class Maintenance : Form
    {
        public Maintenance()
        {
            InitializeComponent();
        }

        public Configuration Conf { get; set; }
        private bool Cancel = false;
        private Thread thread;

        public string AutoCommandExecute { get; set; }

        private void Maintenance_Load(object sender, EventArgs e)
        {
            Conf = Program.Kernel.Conf;

            buttonStop.Enabled = false;

            if (AutoCommandExecute == "maintenance")
            {
                buttonStart_Click(this, null);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Cancel = false;

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            richTextBoxInfo.Text = "";

            thread = new Thread(new ThreadStart(MaintenanceTable));
            thread.Start();
        }

        void MaintenanceTable()
        {
            ApendLine("Структура бази даних");
            ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();

            ApendLine("Таблиць: " + informationSchema.Tables.Count);
            ApendLine("");

            ApendLine("Обробка таблиць:");

            foreach (ConfigurationInformationSchema_Table table in informationSchema.Tables.Values)
            {
                if (Cancel)
                    thread.Abort();

                ApendLine($" --> {table.TableName}");

                string query = $@"VACUUM FULL {table.TableName};";

                Program.Kernel.DataBase.ExecuteSQL(query);
            }

            buttonStart.Invoke(new Action(() => buttonStart.Enabled = true));
            buttonStop.Invoke(new Action(() => buttonStop.Enabled = false));

            ApendLine("");
            ApendLine("Готово!");

            if (!String.IsNullOrEmpty(AutoCommandExecute))
                this.Invoke(new Action(() => this.DialogResult = DialogResult.OK));
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

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            Cancel = true;
        }
    }
}
