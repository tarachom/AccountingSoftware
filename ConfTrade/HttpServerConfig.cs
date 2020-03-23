using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ConfTrade
{
	public class HttpServerConfig
	{
		public HttpServerConfig()
		{
			ConfObjects = new Dictionary<string, ConfObject>();
		}

		public Dictionary<string, ConfObject> ConfObjects { get; }

		public void ReadXml(string pathToFile)
		{
			ConfObjects.Clear();

			XPathDocument xPathDoc = new XPathDocument(pathToFile);
			XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

			XPathNodeIterator ConfObjectNodes = xPathDocNavigator.Select("/root/ConfObject");
			while (ConfObjectNodes.MoveNext())
			{
				ConfObject confObjectItem = new ConfObject(ConfObjectNodes.Current.GetAttribute("Name", ""));
				ConfObjects.Add(confObjectItem.Name, confObjectItem);

				XPathNodeIterator CommandNodes = ConfObjectNodes.Current.Select("Command");
				while (CommandNodes.MoveNext())
				{
					ConfObject.Command command = new ConfObject.Command(CommandNodes.Current.GetAttribute("Name", ""));
					confObjectItem.Commands.Add(command.Name, command);

					XPathNodeIterator ParamNodes = CommandNodes.Current.Select("Param");
					while (ParamNodes.MoveNext())
					{
						command.Params.Add(ParamNodes.Current.GetAttribute("Name", ""), "");
					}

				}

			}
		}

		public class ConfObject
		{
			public ConfObject(string name)
			{
				Name = name;
				Commands = new Dictionary<string, Command>();
			}

			public string Name { get; }

			public Dictionary<string, Command> Commands { get; }

			public class Command
			{
				public Command(string name)
				{
					Name = name;
					Params = new Dictionary<string, string>();
				}

				public string Name { get; }

				public Dictionary<string, string> Params { get; }
			}
		}
	}
}
