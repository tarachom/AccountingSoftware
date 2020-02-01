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
using Conf = ConfTrade_v1_1;

namespace Trade
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();
		}

		private void товариToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form2 form2 = new Form2();
			form2.MdiParent = this;
			form2.Show();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form3 form3 = new Form3();
			form3.MdiParent = this;
			form3.Show();
		}
	}
}
