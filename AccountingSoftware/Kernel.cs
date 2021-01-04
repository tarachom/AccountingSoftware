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
Сайт:     find.org.ua
*/

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
			DataBase.Open("Server=localhost;User Id=postgres;Password=525491;Database=ConfTradeTest;");

			Console.WriteLine("DataBase Open");

			Conf = new Configuration();
			Conf.PathToXmlFileConfiguration = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";
			Configuration.Load(Conf.PathToXmlFileConfiguration, Conf);

			Console.WriteLine("Configuration Load");
		}

		public bool TryConnectToServer(string Server, string UserId, string Password, int Port, out Exception exception)
		{
			DataBase = new PostgreSQL();
			return DataBase.TryConnectToServer(Server, UserId, Password, Port, out exception);
		}

		public bool CreateDatabaseIfNotExist(string Server, string UserId, string Password, int Port, string Database, out Exception exception)
		{
			DataBase = new PostgreSQL();
			return DataBase.CreateDatabaseIfNotExist(Server, UserId, Password, Port, Database, out exception);
		}

		public void Open2(string connectionString)
		{
			DataBase = new PostgreSQL();
			DataBase.Open(connectionString);

			Console.WriteLine("DataBase Open");

			Conf = new Configuration();
			Conf.PathToXmlFileConfiguration = @"D:\VS\Project\AccountingSoftware\ConfTrade\Configuration.xml";
			Configuration.Load(Conf.PathToXmlFileConfiguration, Conf);

			Console.WriteLine("Configuration Load");
		}

		public void Close()
		{
			DataBase.Close();
			Conf = null;
		}

		public Configuration Conf { get; set; }

		public IDataBase DataBase { get; set; }
	}
}
