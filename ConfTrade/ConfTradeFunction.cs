using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Довідники = ConfTrade_v1_1.Directory;

namespace ConfTrade
{
	public partial class ConfTrade
	{
		static string Run()
		{
			Довідники.Записи_Вибірка_View записи_View = new Довідники.Записи_Вибірка_View();
			return записи_View.Read();
		}

		static string Run2()
		{
			Довідники.Товари_Візуалізація3_View товари_Візуалізація3_View = new Довідники.Товари_Візуалізація3_View();
			return товари_Візуалізація3_View.Read();
		}
	}
}