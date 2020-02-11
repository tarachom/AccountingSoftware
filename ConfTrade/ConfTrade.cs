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

			//Conf.Config.Kernel.DataBase.BeginTransaction();

			Conf.Товари_ВибіркаТовари_View v = new Conf.Товари_ВибіркаТовари_View();
			v.QuerySelect.Where.Add(new Where(v.Alias["Одиниця"], Comparison.NOTNULL, "", true));
			v.QuerySelect.Limit = 1;
			v.QuerySelect.CreateTempTable = true;
			Console.WriteLine(v.Read());

			//Conf.Товари_Select sel = new Conf.Товари_Select();
			//sel.QuerySelect.Table = v.QuerySelect.TempTable;
			//if (sel.SelectSingle())
			//{
			//	Conf.Товари_Objest товари_Objest = sel.Current.GetDirectoryObject();

			//	товари_Objest.Ціни_TablePart.Records.Add(new Conf.Товари_Ціни_TablePartRecord("Name", 0.35m, 0, null, DateTime.Now));
			//	товари_Objest.Ціни_TablePart.Save(false);
			//}

			Conf.ОдиниціВиміру_Вибірка_View od = new Conf.ОдиниціВиміру_Вибірка_View();
			od.QuerySelect.CreateTempTable = true;
			od.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + v.Alias["Одиниця"] + " FROM " + v.QuerySelect.TempTable, true));
			Console.WriteLine(od.Read());

			Conf.Товари_ВибіркаЦіни_View ceny = new Conf.Товари_ВибіркаЦіни_View();
			ceny.QuerySelect.Where.Add(new Where("owner", Comparison.EQ, "(SELECT uid FROM " + v.QuerySelect.TempTable + ")", true));
			Console.WriteLine(ceny.Read());

			//Conf.Config.Kernel.DataBase.CommitTransaction();

			Console.ReadLine();
		}


	}
}
