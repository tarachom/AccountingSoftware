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
						XmlData += m_1.Read();

						break;
					}

				case "Add":
					{
						Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
						номенклатура_Objest.New();
						номенклатура_Objest.Код = "1";
						номенклатура_Objest.Назва = "Товар 1";
						номенклатура_Objest.Save();

						XmlData += "<info>" + "Ok: " + номенклатура_Objest.UnigueID.ToString() + "</info>";
						break;
					}

				case "Edit":
					{
						XmlData += "";
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

			xslCompiledTransform.Transform(xmlReader, xsltArgumentList, output);
		}
	}
}
