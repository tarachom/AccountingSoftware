using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;

//Конфігурація Торгівля
namespace ConfTrade
{
	public class ConfTrade
	{
		static void Main(string[] args)
		{
			Conf.Config.Kernel = new Kernel();
			Conf.Config.Kernel.Open();

			/*
			using (Conf.Товари_Select s = new Conf.Товари_Select())
			{
				s.Select();

				while (s.MoveNext())
				{
					Conf.Товари_Objest o = s.Current.GetDirectoryObject();
					o.Назва = "Товар " + new Random().Next(1000).ToString();
					o.Код = "Код " + new Random().Next(1000).ToString();
					o.Save();
				}
			}


			Conf.ОдиниціВиміру_Objest одиниціВиміру_Objest = new Conf.ОдиниціВиміру_Objest();
			одиниціВиміру_Objest.New();
			одиниціВиміру_Objest.Назва = "Назва 2";
			одиниціВиміру_Objest.Код = "12";
			одиниціВиміру_Objest.Save();
			*/

			Conf.Товари_ВибіркаТовари_View v = new Conf.Товари_ВибіркаТовари_View();
			//v.QuerySelect.Where.Add(new Where(v.AliasRevers["Одиниця"], Comparison.EQ, " NOT NULL ", true));
			v.QuerySelect.Limit = 5;
			v.QuerySelect.CreateTempTable = true;
			Console.WriteLine(v.Read());
			Console.WriteLine(v.QuerySelect.TempTable);

			Conf.ОдиниціВиміру_Вибірка_View od = new Conf.ОдиниціВиміру_Вибірка_View();
			od.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT distinct od2 FROM " + v.QuerySelect.TempTable, true));

			Console.WriteLine(od.Read());

			Console.ReadLine();
		}


	}
}
