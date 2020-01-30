using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator
{
	public partial class FormWorkProgress : Form
	{
		public FormWorkProgress()
		{
			InitializeComponent();
		}

		public void AppendLog(string line)
		{
			richTextBoxLog.AppendText(line + "\n") ;
		}

		private void FormWorkProgress_Load(object sender, EventArgs e)
		{

		}
	}
}
