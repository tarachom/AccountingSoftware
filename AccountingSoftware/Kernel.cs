﻿/*
Copyright (C) 2019-2020 Tarakhomin Yuri Ivanovich
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
