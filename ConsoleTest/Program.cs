using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.IO;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
			xsltCodeGnerator.Load("../../CreateTemplate.xslt");

			XPathDocument xPathDoc = new XPathDocument(@"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml");
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator nodeDirectory = xPathDocNavigator.Select("/Configuration/Directories/Directory");
			while (nodeDirectory.MoveNext())
			{
				string directoryName = nodeDirectory.Current.SelectSingleNode("Name").Value;

				XPathNodeIterator nodeView = nodeDirectory.Current.Select("Views/View");
				while (nodeView.MoveNext())
				{
					string viewName = nodeView.Current.SelectSingleNode("Name").Value;

					Console.WriteLine(directoryName + "_" + viewName);

					string filename = "../../XSLT/" + directoryName + "_" + viewName + ".xslt";
					FileMode mode = File.Exists(filename) ? FileMode.Truncate : FileMode.Create;

					XsltArgumentList xsltArgumentList = new XsltArgumentList();
					xsltArgumentList.AddParam("DirectoryName", "", directoryName);

					FileStream stream = new FileStream(filename, mode);
					xsltCodeGnerator.Transform(nodeView.Current, xsltArgumentList, stream);
					stream.Close();
				}
			}

			//xsltCodeGnerator.Transform(@"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml", "../../Result.xslt");

			//string genericText = File.ReadAllText("../../Result.xslt");

			//File.WriteAllText("../../Result.xslt", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + genericText.Trim());
		}
	}
}
