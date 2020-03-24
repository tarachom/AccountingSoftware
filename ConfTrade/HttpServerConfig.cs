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

					if (CommandNodes.CurrentPosition == 1)
					{
						confObjectItem.FirstCommand = command;
					}

					XPathNodeIterator ParamNodes = CommandNodes.Current.Select("Param");
					while (ParamNodes.MoveNext())
					{
						command.Get_Params.Add(ParamNodes.Current.GetAttribute("Name", ""));
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
			public Command FirstCommand { get; set; }

			public class Command
			{
				public Command(string name)
				{
					Name = name;
					Get_Params = new List<string>();
				}

				public string Name { get; }
				public List<string> Get_Params { get; }

				public CommandParamsValue GetCommandParamsValue()
				{
					return new CommandParamsValue(Name, Get_Params);
				}
			}
		}
	}

	public class CommandParamsValue
	{
		public CommandParamsValue()
		{
			Get_Params = new Dictionary<string, string>();
			Post_Params = new Dictionary<string, string>();
		}

		public CommandParamsValue(string name, List<string> get_params) : this()
		{
			Name = name;

			foreach (string get_item_param in get_params)
				Get_Params.Add(get_item_param, "");
		}

		public string Name { get; }
		public Dictionary<string, string> Get_Params { get; }
		public Dictionary<string, string> Post_Params { get; }
	}
}
