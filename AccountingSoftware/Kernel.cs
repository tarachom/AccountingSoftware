using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	public class Kernel
	{
		public Kernel()
		{
			
		}

		public void Open()
		{
			DataBase = new PostgreSQL();
			DataBase.Open("Server=localhost;User Id=postgres;Password=525491;Database=ConfTrade;");

			Console.WriteLine("DataBase Open");

			Conf = new Configuration();
			Conf.PathToXmlFileConfiguration = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";
			Configuration.Load(Conf.PathToXmlFileConfiguration, Conf);

			Console.WriteLine("Configuration Load");
		}

		public void Close()
		{
			DataBase.Close();

			Console.WriteLine("DataBase Close");

			Conf = null;

			Console.WriteLine("Configuration Close");
		}

		public Configuration Conf { get; set; }

		public IDataBase DataBase { get; set; }
	}
}
