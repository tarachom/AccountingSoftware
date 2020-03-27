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
		public void Довідник_Контрагенти(Stream output, CommandParamsValue commandParamsValue)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Контрагенти/" + commandParamsValue.Name + ".xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("confobj", "", "Довідник_Контрагенти");
			xsltArgumentList.AddParam("cmd", "", commandParamsValue.Name);

			foreach (string key in commandParamsValue.Get_Params.Keys)
				xsltArgumentList.AddParam(key, "", commandParamsValue.Get_Params[key]);

			string XmlData = "<root>";

			switch (commandParamsValue.Name)
			{
				case "List":
					{
						Довідники.Контрагенти_Список_View m_1 = new Довідники.Контрагенти_Список_View();
						m_1.QuerySelect.Order.Add(m_1.Alias["Код"], SelectOrder.ASC);

						XmlData += m_1.Read();
						break;
					}

				case "Add":
					{
						Довідники.Контрагенти_Objest Контрагенти_Objest = new Довідники.Контрагенти_Objest();
						XmlData += Контрагенти_Objest.Serialize();
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

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();
						if (контрагенти_Objest.Read(new UnigueID(Uid)))
							XmlData += контрагенти_Objest.Serialize();
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}

				case "Save":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();

						if (String.IsNullOrEmpty(Uid))
						{
							контрагенти_Objest.New();
						}
						else
						{
							if (!контрагенти_Objest.Read(new UnigueID(Uid)))
							{
								XmlData += "<info>Error read Uid</info>";
								break;
							}
						}

						контрагенти_Objest.Код = commandParamsValue.Post_Params["Code"];
						контрагенти_Objest.Назва = commandParamsValue.Post_Params["Name"];

						контрагенти_Objest.Постачальник = (commandParamsValue.Post_Params.ContainsKey("Postachalnyk") &&
							commandParamsValue.Post_Params["Postachalnyk"] == "on") ? true : false;

						контрагенти_Objest.Покупець = (commandParamsValue.Post_Params.ContainsKey("Pokupec") &&
							commandParamsValue.Post_Params["Pokupec"] == "on") ? true : false;

						контрагенти_Objest.Save();

						XmlData += "<info>" + "Записано. Ід " + контрагенти_Objest.UnigueID.ToString() + "</info>";
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

						Довідники.Контрагенти_Objest контрагенти_Objest = new Довідники.Контрагенти_Objest();
						if (контрагенти_Objest.Read(new UnigueID(Uid)))
						{
							контрагенти_Objest.Delete();
							XmlData += "<info>Ok delete</info>";
						}
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}
			}

			XmlData += "</root>";

			//Console.WriteLine(XmlData);

			StringReader stringReader = new StringReader(XmlData);
			XmlReader xmlReader = XmlReader.Create(stringReader);

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
