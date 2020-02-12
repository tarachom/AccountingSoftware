using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;

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

			System.IO.StringWriter stringWriter = new System.IO.StringWriter();
			stringWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			stringWriter.WriteLine("<root>");


			Довідники.Товари_ВибіркаТовари_View v = new Довідники.Товари_ВибіркаТовари_View();
			v.QuerySelect.Where.Add(new Where(v.Alias["Одиниця"], Comparison.NOTNULL, "", true));
			//v.QuerySelect.Limit = 1;
			v.QuerySelect.CreateTempTable = true;
			stringWriter.Write(v.Read());

			/**/

			//Conf.Товари_Select sel = new Conf.Товари_Select();
			//sel.QuerySelect.Table = v.QuerySelect.TempTable;
			//if (sel.SelectSingle())
			//{
			//	Conf.Товари_Objest товари_Objest = sel.Current.GetDirectoryObject();

			//	товари_Objest.Ціни_TablePart.Records.Add(new Conf.Товари_Ціни_TablePartRecord("Name", 0.35m, 0, null, DateTime.Now));
			//	товари_Objest.Ціни_TablePart.Save(false);
			//}


			Довідники.ОдиниціВиміру_Вибірка_View od = new Довідники.ОдиниціВиміру_Вибірка_View();
			od.QuerySelect.CreateTempTable = true;
			od.QuerySelect.Where.Add(new Where("uid", Comparison.IN, "SELECT DISTINCT " + v.Alias["Одиниця"] + " FROM " + v.QuerySelect.TempTable, true));
			stringWriter.Write(od.Read());

			//Довідники.Товари_ВибіркаЦіни_View ceny = new Довідники.Товари_ВибіркаЦіни_View();
			//ceny.QuerySelect.Where.Add(new Where("owner", Comparison.EQ, "(SELECT uid FROM " + v.QuerySelect.TempTable + ")", true));
			//stringWriter.Write(ceny.Read());
			/**/

			stringWriter.WriteLine("</root>");

			System.IO.File.WriteAllText(@"D:\xml.txt", stringWriter.ToString());

			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(@"D:\VS\Project\AccountingSoftware\ConfTrade\XSLTFile1.xslt");

			xslCompiledTransform.Transform(@"D:\xml.txt", @"D:\html.txt");

			Console.ReadLine();
		}


	}
}
