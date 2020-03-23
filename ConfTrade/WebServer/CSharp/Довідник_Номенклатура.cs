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
		public void Довідник_Номенклатура(Stream output, HttpServerConfig.ConfObject.Command command)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Номенклатура/" + command.Name + ".xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xsltArgumentList.AddParam("ConfObject", "", "Довідник_Номенклатура");
			xsltArgumentList.AddParam("Cmd", "", command.Name);

			foreach (string key in command.Params.Keys)
				xsltArgumentList.AddParam(key, "", command.Params[key]);

			string XmlData = "<root>";

			switch (command.Name)
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
						XmlData += "";
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
