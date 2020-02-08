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

            Conf.ТМЦ_Objest тМЦ_Objest = new Conf.ТМЦ_Objest();
            тМЦ_Objest.New();
            тМЦ_Objest.Назва = "Назва товару";
            тМЦ_Objest.Опис = "Опис товару";
            тМЦ_Objest.ДатаДобавлення = DateTime.Now;
            тМЦ_Objest.ОдиницяВиміру = new string[] { "шт.", "кг." };
            тМЦ_Objest.ОсновнаЦіна = 15.45m;
            тМЦ_Objest.Save();
            
            Console.ReadLine();
        }

        
    }
}
