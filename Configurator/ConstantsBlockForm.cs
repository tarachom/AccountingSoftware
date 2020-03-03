/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     find.org.ua
*/

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
	public partial class ConstantsBlockForm : Form
	{
		public Action<string, ConfigurationConstantsBlock , bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistConstantsBlock { get; set; }

		public ConfigurationConstantsBlock ConstantsBlock;

		public string OriginalName { get; set; }
		public bool IsNew { get; set; }

		public ConstantsBlockForm()
		{
			InitializeComponent();
		}

		private void FieldForm_Load(object sender, EventArgs e)
		{
			if (ConstantsBlock == null)
			{
				ConstantsBlock = new ConfigurationConstantsBlock();
				IsNew = true;
			}
			else
			{
				OriginalName = ConstantsBlock.BlockName;

				textBoxName.Text = ConstantsBlock.BlockName;
				textBoxDesc.Text = ConstantsBlock.Desc;

				IsNew = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			string name = textBoxName.Text;
			string errorList = Configuration.ValidateConfigurationObjectName(Program.Kernel, ref name);
			textBoxName.Text = name;

			if (errorList.Length > 0)
			{
				MessageBox.Show(errorList, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsNew || OriginalName != name)
				if (CallBack_IsExistConstantsBlock(name))
				{
					MessageBox.Show("Назва блоку не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConstantsBlock.BlockName = textBoxName.Text;
			ConstantsBlock.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConstantsBlock, IsNew);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void EnumFieldForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				if (MessageBox.Show("Закрити форму?", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					this.Hide();
				}
			}
		}
	}
}