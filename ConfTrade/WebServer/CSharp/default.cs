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
		public void @default(Stream output, CommandParamsValue commandParamsValue)
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"../../WebServer/Xslt/default.xslt");

			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			xslCompiledTransform.Transform(@"../../WebServer/Xml/default.xml", xsltArgumentList, output);
		}
	}
}
