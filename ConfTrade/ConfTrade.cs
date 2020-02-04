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

            Conf.Tovary_View1_View tovary_View1 = new Conf.Tovary_View1_View();
            tovary_View1.QuerySelect.Limit = 2;

            Console.WriteLine(tovary_View1.Read());

            Conf.Tovary_Ceny_View tovary_Ceny_View = new Conf.Tovary_Ceny_View();
            tovary_Ceny_View.Where_owner.Value = Guid.Parse("8ada16bb-c378-4c80-9c9f-c8bf9cec6173");

            Console.WriteLine(tovary_Ceny_View.Read());

            //Configuration.ComparisonGeneration(
            //    @"D:\VS\Project\AccountingSoftware\ConfTrade\InformationSchema.xml",
            //    @"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xslt",
            //    @"D:\VS\Project\AccountingSoftware\ConfTrade\ComparisonSql.xml");

            //Comparison
            //ConfigurationInformationSchema informationSchema = Conf.Config.Kernel.DataBase.SelectInformationSchema();
            //Configuration.Comparison(@"D:\VS\Project\AccountingSoftware\ConfTrade\Comparison.xml", 
            //    Conf.Config.Kernel.Conf, 
            //    informationSchema);

            Console.ReadLine();

        }
        
        static string print_array(string[] arr)
        {
            bool is_first = true;

            string concat = "{";

            foreach (string e in arr)
            {
                if (!is_first)
                    concat += ", ";
                 else
                    is_first = false;

                concat += e;
            }

            concat += "}";

            return concat;
        }
    }
}
