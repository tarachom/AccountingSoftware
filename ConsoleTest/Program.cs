﻿using System;
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
	partial class Program
	{
		static void Main(string[] args)
		{
			XslCompiledTransform xsltTemplateGenerator = new XslCompiledTransform();
			xsltTemplateGenerator.Load("../../CreateTemplate.xslt");

			XslCompiledTransform xsltCodeGenerator = new XslCompiledTransform();
			xsltCodeGenerator.Load("../../CreateFunctionCode.xslt");

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
					xsltTemplateGenerator.Transform(nodeView.Current, xsltArgumentList, stream);
					stream.Close();

					//------------------

					string filename2 = "../../CSharp/" + directoryName + "_" + viewName + ".cs";
					FileMode mode2 = File.Exists(filename2) ? FileMode.Truncate : FileMode.Create;
					FileStream stream2 = new FileStream(filename2, mode2);
					xsltCodeGenerator.Transform(nodeView.Current, xsltArgumentList, stream2);
					stream2.Close();

				}
			}

			

			//xsltCodeGnerator.Transform(@"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml", "../../Result.xslt");

			//string genericText = File.ReadAllText("../../Result.xslt");

			//File.WriteAllText("../../Result.xslt", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + genericText.Trim());
		}
	}
}