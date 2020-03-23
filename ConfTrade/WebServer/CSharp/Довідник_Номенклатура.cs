using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ConfTrade
{
	public partial class Function
	{
		public void Довідник_Номенклатура(Stream output, HttpServerConfig.ConfObject.Command command)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();

			switch (command.Name)
			{
				case "List":
					{
						xslCompiledTransform.Load(@"../../WebServer/Xslt/Довідник_Номенклатура.xslt");

						StringReader sr = new StringReader("<root></root>");
						XmlReader xr = XmlReader.Create(sr);

						XsltArgumentList xsltArgumentList = new XsltArgumentList();
						xslCompiledTransform.Transform(xr, xsltArgumentList, output);

						break;
					}
			}
		}
	}
}
