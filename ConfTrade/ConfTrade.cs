using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using AccountingSoftware;
using Conf = ConfTrade_v1_1;
using Npgsql;

//Конфігурація Торгівля
namespace ConfTrade
{
    public class ConfTrade
    {
        static void Main(string[] args)
        {
            Conf.Config.Kernel = new Kernel();
            Conf.Config.Kernel.Open();

            //Conf.Od_Objest newOd = new Conf.Od_Objest();
            //newOd.New();
            //newOd.Name = "м.";
            //newOd.Save();

            Conf.Od_Select OdSelect = new Conf.Od_Select();
            OdSelect.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "кг."));
            OdSelect.QuerySelect.Limit = 1;
            OdSelect.Select();

            Conf.Od_Pointer OdPointer = new Conf.Od_Pointer();

            if (OdSelect.MoveNext())
                OdPointer = OdSelect.Current;

            Conf.Tovary_Objest newObj = new Conf.Tovary_Objest();
            newObj.New();
            newObj.name = "New Obj 7";
            newObj.count = 1001;
            newObj.num = 11.1113m;
            newObj.od2 = OdPointer;
            newObj.Save();

            Conf.Tovary_Select s = new Conf.Tovary_Select();
            //s.QuerySelect.Limit = 10;
            //s.QuerySelect.Field.Add("name");
            //s.QuerySelect.Field.Add("code");
            //s.QuerySelect.Field.Add("field1");

            s.Select();

            while (s.MoveNext())
            {
                //Console.WriteLine(s.Current.UnigueID.ToString());

                Conf.Tovary_Objest obj = s.Current.GetDirectoryObject();

                Console.WriteLine(obj.name + ", " + obj.od2.UnigueID.ToString() + ", " + obj.count.ToString() + ", " + (obj.od2.UnigueID.UGuid == Guid.Empty ? "1" : "0"));

                //obj.code = obj.UnigueID.ToString();
                //obj.description = "description";

                //obj.field1 = "field1";
                //obj.field2 = "field2";
                //obj.field3 = "field3";
                //obj.field4 = "field4";
                //obj.field5 = "field5";
                //obj.count = -11;
                //obj.Save();                
            }
                       

            Conf.Config.Kernel.Close();

            Console.ReadLine();
        }
    }
}