using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware
{
	public class Kernel
	{
		public Kernel()
		{
			Conf = new Configuration();
		}

		public void Open()
		{
			DataBase = new PostgreSQL();
			DataBase.ConnectionString = "Server=localhost;User Id=postgres;Password=525491;Database=ConfTrade;";
			DataBase.Open();

			Console.WriteLine("DataBase Open");

			string pathToConfSave = @"D:\VS\Project\AccountingSoftware\ConfTrade\ConfigurationNew.xml";

			Configuration.Load(pathToConfSave, Conf);
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
