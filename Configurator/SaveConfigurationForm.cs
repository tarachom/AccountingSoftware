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
using System.Xml;
using System.Xml.XPath;

namespace Configurator
{
	public partial class SaveConfigurationForm : Form
	{
		public SaveConfigurationForm()
		{
			InitializeComponent();
		}

		public Configuration Conf { get; set; }

		private void ApendLine(string head, string bodySelect, string futer = "")
		{
			richTextBoxInfo.AppendText(head);

			richTextBoxInfo.SelectionFont = new Font("Consolas"/*"Microsoft Sans Serif"*/, 10, FontStyle.Underline);
			richTextBoxInfo.SelectionColor = Color.DarkBlue;
			richTextBoxInfo.AppendText(bodySelect);

			richTextBoxInfo.SelectionFont = new Font("Consolas", 10);
			richTextBoxInfo.SelectionColor = Color.Black;
			richTextBoxInfo.AppendText(" " + futer + "\n");

			richTextBoxInfo.ScrollToCaret();
		}

		private string GetNameFromType(string Type)
		{
			switch(Type)
				{
				case "Directory":
					return "Довідник";
				case "Document":
					return "Документ";
				default:
					return "<Невідомий тип>";
			}
		}

		private void SaveConfigurationForm_Load(object sender, EventArgs e)
		{
			buttonSave.Enabled = false;

			//Конфігурація в файл
			Configuration.Save(Conf.PathToXmlFileConfiguration, Conf);

			//Схема бази даних в файл
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
			Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

			//Аналіз таблиць і полів конфігурації та бази даних
			Configuration.ComparisonGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");

			XPathDocument xPathDoc = new XPathDocument(@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator nodeNewDirectory = xPathDocNavigator.Select("/root/Control_Table[IsExist = 'no']");
			while (nodeNewDirectory.MoveNext())
			{
				XPathNavigator nodeName = nodeNewDirectory.Current.SelectSingleNode("Name");
				XPathNavigator nodeType = nodeNewDirectory.Current.SelectSingleNode("Type");
				ApendLine("Новий " + GetNameFromType(nodeType.Value) + ": ", nodeName.Value);

				InfoTableCreateFieldCreate(nodeNewDirectory.Current, "\t ");
				richTextBoxInfo.AppendText("\n");

				XPathNodeIterator nodeDirectoryTabularParts = nodeNewDirectory.Current.Select("Control_TabularParts");
				while (nodeDirectoryTabularParts.MoveNext())
				{
					XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
					ApendLine("\t Нова таблична частина: ", nodeTabularPartsName.Value);

					InfoTableCreateFieldCreate(nodeDirectoryTabularParts.Current, "\t\t ");
				}
				richTextBoxInfo.AppendText("\n");
			}

			XPathNodeIterator nodeDirectoryExist = xPathDocNavigator.Select("/root/Control_Table[IsExist = 'yes']");
			while (nodeDirectoryExist.MoveNext())
			{
				bool flag = false;
				
				XPathNodeIterator nodeDirectoryNewField = nodeDirectoryExist.Current.Select("Control_Field[IsExist = 'no']");
				if (nodeDirectoryNewField.Count > 0)
				{
					XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
					XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
					ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
					flag = true;
				}
				while (nodeDirectoryNewField.MoveNext())
				{
					XPathNavigator nodeFieldName = nodeDirectoryNewField.Current.SelectSingleNode("Name");
					ApendLine("\t Нове Поле: ", nodeFieldName.Value);
				}
				if (nodeDirectoryNewField.Count > 0)
				{
					richTextBoxInfo.AppendText("\n");
				}

				XPathNodeIterator nodeDirectoryExistField = nodeDirectoryExist.Current.Select("Control_Field[IsExist = 'yes']/Type[Coincide = 'no']");
				if (nodeDirectoryExistField.Count > 0 && flag == false)
				{
					XPathNavigator nodeName = nodeDirectoryExistField.Current.SelectSingleNode("Name");
					XPathNavigator nodeType = nodeDirectoryExistField.Current.SelectSingleNode("Type");
					ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
					flag = true;
				}
				while (nodeDirectoryExistField.MoveNext())
				{
					XPathNavigator nodeFieldName = nodeDirectoryExistField.Current.SelectSingleNode("../Name");
					XPathNavigator nodeDataType = nodeDirectoryExistField.Current.SelectSingleNode("DataType");
					XPathNavigator nodeDataTypeCreate = nodeDirectoryExistField.Current.SelectSingleNode("DataTypeCreate");

					ApendLine("\t Поле: ", nodeFieldName.Value, " -> змінений тип даних (Тип в базі: " + nodeDataType.Value + " -> Новий тип: " + nodeDataTypeCreate.Value + "). Можлива втрата даних, або колонка буде скопійована!");
				}
				if (nodeDirectoryExistField.Count > 0)
				{
					richTextBoxInfo.AppendText("\n");
				}

				XPathNodeIterator nodeDirectoryNewTabularParts = nodeDirectoryExist.Current.Select("Control_TabularParts[IsExist = 'no']");
				if (nodeDirectoryNewTabularParts.Count > 0)
				{
					if (flag == false)
					{
						XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
						XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
						ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
						flag = true;
					}
				}
				while (nodeDirectoryNewTabularParts.MoveNext())
				{
					XPathNavigator nodeTabularPartsName = nodeDirectoryNewTabularParts.Current.SelectSingleNode("Name");
					ApendLine("\t Нова таблична частина : ", nodeTabularPartsName.Value);

					InfoTableCreateFieldCreate(nodeDirectoryNewTabularParts.Current, "\t\t");
				}
				if (nodeDirectoryNewTabularParts.Count > 0)
				{
					richTextBoxInfo.AppendText("\n");
				}

				XPathNodeIterator nodeDirectoryTabularParts = nodeDirectoryExist.Current.Select("Control_TabularParts[IsExist = 'yes']");
				while (nodeDirectoryTabularParts.MoveNext())
				{
					bool flagTP = false;

					XPathNodeIterator nodeDirectoryTabularPartsNewField = nodeDirectoryTabularParts.Current.Select("Control_Field[IsExist = 'no']");
					if (nodeDirectoryTabularPartsNewField.Count > 0)
					{
						if (!flag)
						{
							XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
							XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
							ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
							flag = true;
						}

						if (!flagTP)
						{
							XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
							ApendLine("\t Таблична частина : ", nodeTabularPartsName.Value);
							flagTP = true;
						}
					}
					while (nodeDirectoryTabularPartsNewField.MoveNext())
					{
						XPathNavigator nodeFieldName = nodeDirectoryTabularPartsNewField.Current.SelectSingleNode("Name");
						XPathNavigator nodeConfType = nodeDirectoryTabularPartsNewField.Current.SelectSingleNode("FieldCreate/ConfType");

						ApendLine("\t\t Нове Поле: ", nodeFieldName.Value, "(Тип: " + nodeConfType.Value + ")");
					}
					if (nodeDirectoryTabularPartsNewField.Count > 0)
					{
						richTextBoxInfo.AppendText("\n");
					}

					XPathNodeIterator nodeDirectoryTabularPartsField = nodeDirectoryTabularParts.Current.Select("Control_Field[IsExist = 'yes']/Type[Coincide = 'no']");
					if (nodeDirectoryTabularPartsField.Count > 0)
					{
						if (flag == false)
						{
							XPathNavigator nodeName = nodeDirectoryExist.Current.SelectSingleNode("Name");
							XPathNavigator nodeType = nodeDirectoryExist.Current.SelectSingleNode("Type");
							ApendLine(GetNameFromType(nodeType.Value) + ": ", nodeName.Value);
							flag = true;
						}

						if (!flagTP)
						{
							XPathNavigator nodeTabularPartsName = nodeDirectoryTabularParts.Current.SelectSingleNode("Name");
							ApendLine("\t Таблична частина : ", nodeTabularPartsName.Value);
							flagTP = true;
						}
					}
					while (nodeDirectoryTabularPartsField.MoveNext())
					{
						XPathNavigator nodeFieldName = nodeDirectoryTabularPartsField.Current.SelectSingleNode("../Name");
						XPathNavigator nodeDataType = nodeDirectoryTabularPartsField.Current.SelectSingleNode("DataType");
						XPathNavigator nodeDataTypeCreate = nodeDirectoryTabularPartsField.Current.SelectSingleNode("DataTypeCreate");

						ApendLine("\t\t Поле: ", nodeFieldName.Value, " -> змінений тип даних (Тип в базі: " + nodeDataType.Value + " -> Новий тип: " + nodeDataTypeCreate.Value + "). Можлива втрата даних, або колонка буде скопійована!");
					}
					if (nodeDirectoryExistField.Count > 0)
					{
						richTextBoxInfo.AppendText("\n");
					}
				}
			}
		}

		private void InfoTableCreateFieldCreate(XPathNavigator xPathNavigator, string tab)
		{
			XPathNodeIterator nodeField = xPathNavigator.Select("TableCreate/FieldCreate");
			while (nodeField.MoveNext())
			{
				XPathNavigator nodeName = nodeField.Current.SelectSingleNode("Name");
				XPathNavigator nodeConfType = nodeField.Current.SelectSingleNode("ConfType");

				ApendLine(tab + "Поле: ", nodeName.Value, "(Тип: " + nodeConfType.Value + ")");
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void buttonAnalize_Click(object sender, EventArgs e)
		{
			richTextBoxInfo.AppendText("\n\n[ АНАЛІЗ ]\n\n");
			richTextBoxInfo.ScrollToCaret();

			//Конфігурація в файл
			Configuration.Save(Conf.PathToXmlFileConfiguration, Conf);

			//Схема бази даних в файл
			ConfigurationInformationSchema informationSchema = Program.Kernel.DataBase.SelectInformationSchema();
			Configuration.SaveInformationSchema(informationSchema, @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml");

			//Аналіз таблиць і полів конфігурації та бази даних
			Configuration.ComparisonGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml");

			//Create SQL
			Configuration.ComparisonGeneration(
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReport.xml",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonReportAnalize.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

			XPathDocument xPathDoc = new XPathDocument(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator nodeInfo = xPathDocNavigator.Select("/root/info");
			if (nodeInfo.Count == 0)
			{
				richTextBoxInfo.AppendText("Інформація відсутня!\n\n");
				richTextBoxInfo.ScrollToCaret();
			}
			else
				while (nodeInfo.MoveNext())
				{
					richTextBoxInfo.AppendText(nodeInfo.Current.Value + "\n");
					richTextBoxInfo.ScrollToCaret();
				}

			richTextBoxInfo.AppendText("\n[ Команди SQL ]\n\n");
			richTextBoxInfo.ScrollToCaret();

			XPathNodeIterator nodeSQL = xPathDocNavigator.Select("/root/sql");
			if (nodeSQL.Count == 0)
			{
				richTextBoxInfo.AppendText("Команди відсутні!\n\n");
				richTextBoxInfo.ScrollToCaret();
			}
			else
				while (nodeSQL.MoveNext())
				{
					richTextBoxInfo.AppendText(nodeSQL.Current.Value + "\n");
					richTextBoxInfo.ScrollToCaret();
				}

			buttonSave.Enabled = true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			//Read SQL
			List<string> SqlList = Configuration.ListComparisonSql(@"D:\VS\Project\AccountingSoftware\ConfTrade\ReportAnalize.xml");

			richTextBoxInfo.AppendText("\n[ Виконання SQL ]\n\n");
			richTextBoxInfo.ScrollToCaret();

			if (SqlList.Count == 0)
			{
				richTextBoxInfo.AppendText("Команди відсутні!\n\n");
				richTextBoxInfo.ScrollToCaret();
			}
			else
				//Execute
				foreach (string sqlText in SqlList)
				{
					int resultSQL = Program.Kernel.DataBase.ExecuteSQL(sqlText);
					richTextBoxInfo.AppendText(" -> " + sqlText + " [" + resultSQL.ToString() + "]\n");
					richTextBoxInfo.ScrollToCaret();
				}

			buttonSave.Enabled = false;

			richTextBoxInfo.AppendText("\n[ Генерування класів C# для об'єктів конфігурації ]\n\n");
			richTextBoxInfo.ScrollToCaret();

			//Code Generation
			Configuration.Generation(Conf.PathToXmlFileConfiguration,
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.xslt",
				@"D:\VS\Project\AccountingSoftware\ConfTrade\CodeGeneration.cs");

			richTextBoxInfo.AppendText("ГОТОВО!\n\n\n");
			richTextBoxInfo.ScrollToCaret();
		}
	}
}
