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
Сайт:     accounting.org.ua
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
	public partial class ViewForm : Form
	{
		public ViewForm()
		{
			InitializeComponent();
		}

		public Action<string, ConfigurationObjectView, bool> CallBack { get; set; }
		public Func<string, Boolean> CallBack_IsExistView { get; set; }

		public ConfigurationDirectories ConfDirectory { get; set; }
		public ConfigurationObjectView ConfView { get; set; }
		public bool IsNewView { get; set; }
		public string OriginalName { get; set; }

		private void ViewForm_Load(object sender, EventArgs e)
		{
			if (ConfView == null)
			{
				ConfView = new ConfigurationObjectView();

				textBoxTable.Text = ConfDirectory.Table;

				IsNewView = true;
			}
			else
			{
				OriginalName = ConfView.Name;

				textBoxName.Text = ConfView.Name;
				textBoxTable.Text = ConfView.Table;
				textBoxDesc.Text = ConfView.Desc;
				IsNewView = false;

				LoadFieldList();
			}

			LoadAllFieldList();
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

			if (IsNewView || OriginalName != name)
				if (CallBack_IsExistView(name))
				{
					MessageBox.Show("Назва візуалізації не унікальна", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

			ConfView.Name = textBoxName.Text;
			ConfView.Table = textBoxTable.Text;
			ConfView.Desc = textBoxDesc.Text;

			CallBack.Invoke(OriginalName, ConfView, IsNewView);

			this.Hide();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		void LoadFieldList()
		{
			listBoxFields.Items.Clear();

			foreach (KeyValuePair<string, string> configurationObjectView in ConfView.Fields)
			{
				listBoxFields.Items.Add(configurationObjectView);
			}
		}

		void LoadAllFieldList()
		{
			listBoxAllFields.Items.Clear();

			foreach (KeyValuePair<string, ConfigurationObjectField> configurationObjectField in ConfDirectory.Fields)
			{
				listBoxAllFields.Items.Add(
					new KeyValuePair<string, string>(
						configurationObjectField.Value.Name, configurationObjectField.Value.NameInTable));
			}
		}

		private void AddField()
		{
			if (listBoxAllFields.SelectedItem != null)
			{
				string fieldName = ((KeyValuePair<string, string>)listBoxAllFields.SelectedItem).Key;

				if (!ConfView.Fields.ContainsKey(fieldName))
					ConfView.Fields.Add(
						ConfDirectory.Fields[fieldName].Name,
						ConfDirectory.Fields[fieldName].NameInTable);

				LoadFieldList();
			}
		}

		private void listBoxAllFields_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			AddField();
		}

		private void listBoxFields_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxFields.SelectedItem != null)
			{
				if (e.KeyData == Keys.Delete)
				{
					int selectIndex = listBoxFields.SelectedIndex;

					ConfView.Fields.Remove(((KeyValuePair<string, string>)listBoxFields.SelectedItem).Key);
					LoadFieldList();

					if (selectIndex >= listBoxFields.Items.Count)
						selectIndex = listBoxFields.Items.Count - 1;

					listBoxFields.SelectedIndex = selectIndex;
				}
			}
		}

		private void listBoxAllFields_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
				AddField();
		}

		private void buttonAddAllField_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < listBoxAllFields.Items.Count; i++)
			{
				string fieldName = ((KeyValuePair<string, string>)listBoxAllFields.Items[i]).Key;

				if (!ConfView.Fields.ContainsKey(fieldName))
					ConfView.Fields.Add(
						ConfDirectory.Fields[fieldName].Name,
						ConfDirectory.Fields[fieldName].NameInTable);
			}

			LoadFieldList();
		}

		private void ViewForm_KeyDown(object sender, KeyEventArgs e)
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