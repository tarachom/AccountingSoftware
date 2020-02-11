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

            Conf.Товари_ВибіркаТовари_View v = new Conf.Товари_ВибіркаТовари_View();
            Console.WriteLine(v.Read());
            

            Console.ReadLine();
        }

        
    }
}
