using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSoftware;

namespace Configurator
{
    public partial class ConfigurationInfoForm : Form
    {
        public Configuration Conf { get; set; }

        public FormConfiguration OwnerForm { get; set; }

        public ConfigurationInfoForm()
        {
            InitializeComponent();
        }

        private void ConfigurationInfoForm_Load(object sender, EventArgs e)
        {
            textBox_Назва.Text = Conf.Name;
            textBox_ПростірІмен.Text = Conf.NameSpace;
            textBox_Автор.Text = Conf.Author;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBox_ПростірІмен.Text;
            string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);
            textBox_ПростірІмен.Text = name;

            if (errorList.Length > 0)
            {
                MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Conf.Name = textBox_Назва.Text;
            Conf.NameSpace = textBox_ПростірІмен.Text;
            Conf.Author = textBox_Автор.Text;

            if (OwnerForm != null)
                OwnerForm.LoadConf();

            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
