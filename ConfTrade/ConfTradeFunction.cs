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
			Довідники.Номенклатура_Objest номенклатура_Objest = new Довідники.Номенклатура_Objest();
			номенклатура_Objest.New();
			номенклатура_Objest.Назва = "Товар 1";
			номенклатура_Objest.ПолнНаименование = "Товар 1 повна назва";
			номенклатура_Objest.Код = "1";
			номенклатура_Objest.Save();

			return "";
		}

		static string Run2()
		{
			//1. Знайти родителя по ключу
			//2. Рекурсивно вибрати всіх предків
			//3. Вибрати потомків
			//4. Вибрати Номенклатуру поточного родітеля

			Довідники.ГрупиНоменклатура_Список_View групиНоменклатура_Список_View = new Довідники.ГрупиНоменклатура_Список_View();
			групиНоменклатура_Список_View.QuerySelect.Where.Add(
				new Where(групиНоменклатура_Список_View.Alias["Родитель"], Comparison.EQ, new DirectoryEmptyPointer()));

			Довідники.Номенклатура_Список_View номенклатура_Список_View = new Довідники.Номенклатура_Список_View();


			return "";
		}
	}
}