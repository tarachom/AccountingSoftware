using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ConfTrade
{
	public class Function
	{
		public void @default(Stream output, HttpServerConfig.ConfObject.Command command)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();

			switch (command.Name)
			{
				case "List":
					{
						xslCompiledTransform.Load(@"../../WebServer/XSLT/default.List.xslt");

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
