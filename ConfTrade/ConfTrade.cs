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


            Conf.Tovary_Objest tovary_Objest = new Conf.Tovary_Objest();

            for (int i = 0; i < 10; i++)
            {
                tovary_Objest.New();
                tovary_Objest.Name = new string[] { "test", "test2", "test3" };
                tovary_Objest.Save();
            }

            Console.ReadLine();
        }

        
    }
}
