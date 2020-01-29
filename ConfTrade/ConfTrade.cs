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
            //newOd.Name = "м2.";
            //newOd.Save();

            Conf.Od_Select OdSelect = new Conf.Od_Select();
            OdSelect.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "кг."));
            OdSelect.QuerySelect.Limit = 1;
            OdSelect.Select();

            Conf.Od_Pointer OdPointer = null;
            if (OdSelect.MoveNext())
                OdPointer = OdSelect.Current;
            else
            {
                Console.WriteLine("Not found");
                Console.ReadLine();
            }


            //if (OdSelect.MoveNext())
            //    OdPointer = OdSelect.Current;

            //Conf.Tovary_Objest newObj = new Conf.Tovary_Objest();
            //newObj.New();
            //newObj.name = "New Obj 88";
            //newObj.count = 1001;
            //newObj.num = 11.1113m;
            //newObj.od2 = OdPointer;
            //newObj.datetime_add = DateTime.Now;
            //newObj.time_add = DateTime.Now.TimeOfDay;
            //newObj.text_arr = new string[] { "test1", "text2", "text3" };
            //newObj.int_arr = new int[] { 10, 20, 30, 40, 50 };
            //newObj.numeric_arr = new decimal[] { 10.2m, 11.3554m };
            //newObj.Save();

            Conf.Tovary_Select objSelect = new Conf.Tovary_Select();
            objSelect.QuerySelect.Where.Add(new Where("uid", Comparison.EQ, Guid.Parse("8ada16bb-c378-4c80-9c9f-c8bf9cec6173")));
            objSelect.Select();

            Conf.Tovary_Objest newObj = null;

            if (objSelect.MoveNext())
                newObj = objSelect.Current.GetDirectoryObject();
            else
            {
                Console.WriteLine("Not found");
                Console.ReadLine();
            }

            //newObj.Od_List_TablePart.Records.Add(new Conf.Tovary_Od_List_TablePartRecord(OdPointer, "кілограм"));
            //nwObj.Od_List_TablePart.Save(false);

            newObj.Od_List_TablePart.Read();
            foreach (Conf.Tovary_Od_List_TablePartRecord tovary_Od_List_TablePartRecord in newObj.Od_List_TablePart.Records)
            {
                Conf.Od_Objest od_Objest = tovary_Od_List_TablePartRecord.od_pointer.GetDirectoryObject();
                Console.WriteLine(od_Objest.Name);
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    newObj.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord());
            //    newObj.Ceny_TablePart.Records[i].name = "Name" + i.ToString();
            //}

            //newObj.Ceny_TablePart.Save();

            //newObj.CenyPostach_TablePart.Records[0].od = new Conf.Od_Pointer();

            //newObj.Ceny_TablePart.Read();
            //foreach (Conf.Tovary_Ceny_TablePartRecord record in newObj.Ceny_TablePart.Records)
            //{
            //    Console.WriteLine(record.name);
            //}

            //for (int i = 0; i < newObj.Ceny_TablePart.Records.Count; i++)
            //{
            //    newObj.Ceny_TablePart.Records[i].name = "NewName " + i.ToString();
            //    newObj.Ceny_TablePart.Records[i].cena = 14.55m + 1.43m;
            //}



            //newObj.Ceny_TablePart.Read();
            //newObj.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord("dfasd", 10));
            //newObj.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord("dfgsdfdsfasg", 10.232m));
            //newObj.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord("sdfasd", 10));
            //newObj.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord("sdfasdfasd", 10.232m));
            //newObj.Ceny_TablePart.Save();

            //newObj.Ceny_TablePart.Read();
            //foreach (Conf.Tovary_Ceny_TablePartRecord record in newObj.Ceny_TablePart.Records)
            //{
            //    Console.WriteLine("name = " + record.name + ", cena = " + record.cena.ToString());
            //}

            //newObj.Ceny_TablePart.Clear();

            Console.ReadLine();

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
