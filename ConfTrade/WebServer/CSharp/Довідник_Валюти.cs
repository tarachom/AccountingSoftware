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
		public void Довідник_Валюти(Stream output, CommandParamsValue commandParamsValue)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Валюти/" + commandParamsValue.Name + ".xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("confobj", "", "Довідник_Валюти");
			xsltArgumentList.AddParam("cmd", "", commandParamsValue.Name);

			foreach (string key in commandParamsValue.Get_Params.Keys)
				xsltArgumentList.AddParam(key, "", commandParamsValue.Get_Params[key]);

			string XmlData = "<root>";

			switch (commandParamsValue.Name)
			{
				case "List":
					{
						TimeSpan ts = DateTime.Now.TimeOfDay;
						Console.WriteLine(ts);

						Довідники.Валюти_Список_View m_1 = new Довідники.Валюти_Список_View();
						m_1.QuerySelect.Order.Add(m_1.Alias["Код"], SelectOrder.ASC);
						m_1.QuerySelect.Order.Add(m_1.Alias["Назва"], SelectOrder.ASC);

						XmlData += m_1.Read();

						TimeSpan ts2 = DateTime.Now.TimeOfDay;
						Console.WriteLine(ts2 + " = " + (ts2 - ts) + "; " + (ts2 - ts).TotalSeconds + "; " + (ts2 - ts).Milliseconds);

						break;
					}

				case "Add":
					{
						Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();
						XmlData += валюти_Objest.Serialize();
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

						Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();
						if (валюти_Objest.Read(new UnigueID(Uid)))
							XmlData += валюти_Objest.Serialize();
						else
							XmlData += "<info>Error read Uid</info>";

						break;
					}

				case "Save":
					{
						string Uid = commandParamsValue.Get_Params["Uid"];

						Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();

						if (String.IsNullOrEmpty(Uid))
						{
							валюти_Objest.New();
						}
						else
						{
							if (!валюти_Objest.Read(new UnigueID(Uid)))
							{
								XmlData += "<info>Error read Uid</info>";
								break;
							}
						}

						валюти_Objest.Код = commandParamsValue.Post_Params["Code"];
						валюти_Objest.Назва = commandParamsValue.Post_Params["Name"];
						валюти_Objest.Save();

						XmlData += "<info>" + "Записано. Ід " + валюти_Objest.UnigueID.ToString() + "</info>";
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

						Довідники.Валюти_Objest валюти_Objest = new Довідники.Валюти_Objest();
						if (валюти_Objest.Read(new UnigueID(Uid)))
						{
							валюти_Objest.Delete();
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
