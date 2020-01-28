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

            Conf.Od_Objest newOd = new Conf.Od_Objest();
            newOd.New();
            newOd.Name = "м2.";
            newOd.Save();

            //Conf.Od_Select OdSelect = new Conf.Od_Select();
            //OdSelect.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "кг."));
            //OdSelect.QuerySelect.Limit = 1;
            //OdSelect.Select();

            //Conf.Od_Pointer OdPointer = new Conf.Od_Pointer();

            //if (OdSelect.MoveNext())
            //    OdPointer = OdSelect.Current;

            Conf.Tovary_Objest newObj = new Conf.Tovary_Objest();
            newObj.New();
            newObj.name = "New Obj 777";
            newObj.count = 1001;
            newObj.num = 11.1113m;
            newObj.od2 = newOd.GetDirectoryPointer();
            newObj.datetime_add = DateTime.Now;
            newObj.time_add = DateTime.Now.TimeOfDay;
            newObj.text_arr = new string[] { "test1", "text2", "text3" };
            newObj.int_arr = new int[] { 10, 20, 30, 40, 50 };
            newObj.numeric_arr = new decimal[] { 10.2m, 11.3554m };
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

                Console.WriteLine(obj.name + ", " + obj.od2.UnigueID.ToString() + ", " + obj.count.ToString() +
                    ", " + (obj.od2.UnigueID.UGuid == Guid.Empty ? "1" : "0") +
                    ", " + obj.isupdate +
                    //", " + obj.date_add +
                    ", " + obj.time_add +
                    ", " + obj.datetime_add +
                    ", " + print_array(obj.text_arr));

                //obj.text_arr = new string[] { };
                //obj.int_arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 9, 10 };
                //obj.Save();

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
