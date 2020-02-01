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
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			Conf.Tovary_Select tovary_Select = new Conf.Tovary_Select();
			tovary_Select.QuerySelect.Field.Add("name");
			tovary_Select.QuerySelect.Field.Add("code");
			tovary_Select.QuerySelect.Field.Add("description");
			tovary_Select.QuerySelect.Field.Add("od2");

			tovary_Select.Select();

			foreach (string field in tovary_Select.QuerySelect.Field)
			{
				dataConfiguration.Columns.Add(field, field);
			}

			while (tovary_Select.MoveNext())
			{
				dataConfiguration.Rows.Add(new object[] {
					tovary_Select.Current.Fields["name"],
					tovary_Select.Current.Fields["code"],
					tovary_Select.Current.Fields["description"],
					tovary_Select.Current.Fields["od2"]
				});
			}		

			/*
			dataConfiguration.Columns.Add("Name", "NAME");
			dataConfiguration.Columns.Add("Code", "CODE");

			DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
			cbc.Name = "List";
			cbc.FlatStyle = FlatStyle.Flat;
			cbc.Items.Add("10");
			cbc.Items.Add("30");
			cbc.Items.Add("80");
			cbc.Items.Add("100");
			dataConfiguration.Columns.Add(cbc);

			DataGridViewCheckBoxColumn cbbc = new DataGridViewCheckBoxColumn(false);
			cbbc.Name = "Check";
			dataConfiguration.Columns.Add(cbbc);

			DataGridViewTextBoxColumn tbc = new DataGridViewTextBoxColumn();
			tbc.Name = "Text";
			dataConfiguration.Columns.Add(tbc);

			for (int i = 0; i < 5; i++)
				dataConfiguration.Rows.Add(new object[] { "10", "32", "30", true, "" });
			*/
		}
	}
}
