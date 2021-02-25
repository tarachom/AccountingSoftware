/*
Copyright (C) 2019-2020 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/

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
	partial class Program
	{
		static void CreatSQL()
		{
			XslCompiledTransform xsltTemplateGenerator = new XslCompiledTransform();
			xsltTemplateGenerator.Load("../../SqlBuild.xslt");

			string filename = "../../result_sql.txt";
			FileMode mode = File.Exists(filename) ? FileMode.Truncate : FileMode.Create;

			XsltArgumentList xsltArgumentList = new XsltArgumentList();

			FileStream stream = new FileStream(filename, mode);

			xsltTemplateGenerator.Transform(@"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml", xsltArgumentList, stream);
		}

		static void Main(string[] args)
		{
			//CreatSQL();

			//return;

			//Console.ReadLine();


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

					string filename = "../../XSLT/Довідники." + directoryName + "_" + viewName + ".xslt";
					FileMode mode = File.Exists(filename) ? FileMode.Truncate : FileMode.Create;

					XsltArgumentList xsltArgumentList = new XsltArgumentList();
					xsltArgumentList.AddParam("DirectoryName", "", "Довідники." + directoryName);

					FileStream stream = new FileStream(filename, mode);
					xsltTemplateGenerator.Transform(nodeView.Current, xsltArgumentList, stream);
					stream.Close();

					//------------------>

					string xml = "";
					List<string> UsingEnums = new List<string>();

					XPathNodeIterator nodeFieldEnumType = nodeView.Current.Select("Fields/Field[Type = 'enum']");
					while (nodeFieldEnumType.MoveNext())
					{
						string pointerEnum = nodeFieldEnumType.Current.SelectSingleNode("Pointer").Value;

						string[] pointerEnumSplit = pointerEnum.Split(new string[] { "." }, StringSplitOptions.None);
						pointerEnum = pointerEnumSplit[1];

						if (!UsingEnums.Contains(pointerEnum))
						{
							XPathNavigator enumNode = xPathDocNavigator.SelectSingleNode("/Configuration/Enums/Enum[Name = '" + pointerEnum + "']");
							if (enumNode != null)
								xml += enumNode.OuterXml + "\n";
							else
								xml += "<error>Not found enum '" + pointerEnum + "'</error>\n";

							UsingEnums.Add(pointerEnum);
						}
					}

					if (!String.IsNullOrWhiteSpace(xml))
						xml = "<Enums>\n" + xml + "</Enums>\n";

					xsltArgumentList.AddParam("XmlHeap", "", xml);
					//<------------------

					string filename2 = "../../CSharp/Довідники." + directoryName + "_" + viewName + ".cs";
					FileMode mode2 = File.Exists(filename2) ? FileMode.Truncate : FileMode.Create;
					FileStream stream2 = new FileStream(filename2, mode2);
					xsltCodeGenerator.Transform(nodeView.Current, xsltArgumentList, stream2);
					stream2.Close();
				}
			}
		}
	}
}
