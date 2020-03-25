using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Константи = ConfTrade_v1_1.Константи;
using Довідники = ConfTrade_v1_1.Довідники;
using Документи = ConfTrade_v1_1.Документи;
using Перелічення = ConfTrade_v1_1.Перелічення;
using РегістриВідомостей = ConfTrade_v1_1.РегістриВідомостей;
using РегістриНакопичення = ConfTrade_v1_1.РегістриНакопичення;

namespace ConfTrade
{
	public partial class Function
	{
		public void Довідник_Номенклатура(Stream output, CommandParamsValue commandParamsValue)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Номенклатура/" + commandParamsValue.Name + ".xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("confobj", "", "Довідник_Номенклатура");
			xsltArgumentList.AddParam("cmd", "", commandParamsValue.Name);

			foreach (string key in commandParamsValue.Get_Params.Keys)
				xsltArgumentList.AddParam(key, "", commandParamsValue.Get_Params[key]);

			string XmlData = "<root>";

			switch (commandParamsValue.Name)
			{
				case "List":
					{
						Довідники.Номенклатура_Список_View m_1 = new Довідники.Номенклатура_Список_View();
						m_1.QuerySelect.Order.Add(m_1.Alias["Код"], SelectOrder.ASC);
						m_1.QuerySelect.Order.Add(m_1.Alias["Назва"], SelectOrder.ASC);

						m_1.QuerySelect.CreateTempTable = true;
						Dictionary<string, string> Alias = m_1.Alias;

						XmlData += m_1.Read();

						string TempTable = m_1.QuerySelect.TempTable;

						Довідники.Валюти_Список_View m_6 = new Довідники.Валюти_Список_View();
						m_6.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["Валюта"] + " FROM " + TempTable, true));
						XmlData += m_6.Read();

						Довідники.Test_Список_View m_7 = new Довідники.Test_Список_View();
						m_7.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + Alias["Вказівник"] + " FROM " + TempTable, true));
						XmlData += m_7.Read();

						break;
					}

				case "Add":
					{
						Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
						XmlData += номенклатура_Objest.Serialize();
						break;
					}

				case "Edit":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						if (String.IsNullOrEmpty(Uid))
						{
							XmlData += "<info>Error Uid</info>";
							break;
						}

						Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
						if (номенклатура_Objest.Read(new UnigueID(Uid)))
							XmlData += номенклатура_Objest.Serialize();
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}

				case "Save":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();

						if (String.IsNullOrEmpty(Uid))
						{
							номенклатура_Objest.New();
						}
						else
						{
							if (!номенклатура_Objest.Read(new UnigueID(Uid)))
							{
								XmlData += "<info>Error read Uid</info>";
								break;
							}
						}

						номенклатура_Objest.Код = commandParamsValue.Post_Params["Code"];
						номенклатура_Objest.Назва = commandParamsValue.Post_Params["Name"];
						номенклатура_Objest.Ціна = int.Parse(commandParamsValue.Post_Params["Cena"]);
						номенклатура_Objest.Кво = int.Parse(commandParamsValue.Post_Params["Kvo"]);
						номенклатура_Objest.ДатаСтворення = DateTime.Now;

						номенклатура_Objest.Валюта = new Довідники.Валюти_Select().FindByField("Код", "2");

						номенклатура_Objest.Save();

						XmlData += "<info>" + "Записано. Ід " + номенклатура_Objest.UnigueID.ToString() + "</info>";
						break;
					}

				case "Delete":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						if (String.IsNullOrEmpty(Uid))
						{
							XmlData += "<info>Error Uid</info>";
							break;
						}

						Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
						if (номенклатура_Objest.Read(new UnigueID(Uid)))
						{
							номенклатура_Objest.Delete();
							XmlData += "<info>Ok delete</info>";
						}
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}
			}

			XmlData += "</root>";

			StringReader stringReader = new StringReader(XmlData);
			XmlReader xmlReader = XmlReader.Create(stringReader);

			//Console.WriteLine(XmlData);

			try
			{
				xslCompiledTransform.Transform(xmlReader, xsltArgumentList, output);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
