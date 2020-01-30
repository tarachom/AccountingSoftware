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

            //Conf.Config.Kernel.DataBase.Test();
            //Console.ReadLine();

            Conf.Tovary_Select tovary_Select1 = new Conf.Tovary_Select();
            tovary_Select1.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "NEW Object"));

            if (tovary_Select1.Select())
            {
                Console.WriteLine(tovary_Select1.Count());

                Conf.Tovary_Objest tovary_Objest2 = new Conf.Tovary_Objest();

                while (tovary_Select1.MoveNext())
                {
                    Console.WriteLine(tovary_Select1.Current.UnigueID);

                    if (tovary_Objest2.Read(tovary_Select1.Current.UnigueID))
                    {
                        tovary_Objest2.num += 100;

                        if (String.IsNullOrEmpty(tovary_Objest2.code))
                            tovary_Objest2.code = "00001";

                        if (String.IsNullOrEmpty(tovary_Objest2.description))
                            tovary_Objest2.description = "description";

                        if (tovary_Objest2.od2.UnigueID.UGuid == Guid.Empty)
                        {
                            Conf.Od_Select od_Select = new Conf.Od_Select();
                            if (od_Select.SelectSingle())
                            {
                                tovary_Objest2.od2 = od_Select.Current;
                            }
                        }
                        
                        tovary_Objest2.Save();

                        tovary_Objest2.Ceny_TablePart.Read();
                        tovary_Objest2.Ceny_TablePart.Records.Add(new Conf.Tovary_Ceny_TablePartRecord("TEST22", 130));
                        tovary_Objest2.Ceny_TablePart.Save();
                    }
                }
            }

            Console.ReadLine();


            //
            Conf.Od_Objest od_Objest1 = new Conf.Od_Objest();
            
            if (od_Objest1.Read(new UnigueID("836994c1-51db-4578-a7fe-82ef173f02cc")))
            {
                Console.WriteLine(od_Objest1.Name);

                od_Objest1.New();
                od_Objest1.Save();

                Console.WriteLine(od_Objest1.UnigueID);
            }
            Console.ReadLine();




            Conf.Tovary_Select tovary_Select = new Conf.Tovary_Select();
            tovary_Select.QuerySelect.Where.Add(new Where("Name", Comparison.EQ, "Name 3"));

            foreach (KeyValuePair<string, ConfigurationObjectField> field in Conf.Config.Kernel.Conf.Directories["Tovary"].Fields)
            {
                tovary_Select.QuerySelect.Field.Add(field.Key);
            }

            tovary_Select.QuerySelect.Limit = 1;

            tovary_Select.Select();
            Console.WriteLine("Select: " + tovary_Select.Count());

            while (tovary_Select.MoveNext())
            {
                foreach (KeyValuePair<string, object> f in tovary_Select.Current.Fields)
                {
                    Console.WriteLine(f.Key + ": " + f.Value);
                }

                tovary_Select.Current.Delete();
            }

            Console.ReadLine();



            Console.WriteLine("SelectSingle: " + tovary_Select.Count());
            Console.WriteLine(tovary_Select.Current.UnigueID);

            tovary_Select.Select();
            Console.WriteLine("Select: " + tovary_Select.Count());

            while (tovary_Select.MoveNext())
            {
                Console.WriteLine(tovary_Select.Current.UnigueID);
            }

            Console.ReadLine();






            Conf.Od_Select OdSelect = new Conf.Od_Select();
            OdSelect.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "м2."));
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

            //newObj.Od_List_TablePart.Records.Add(new Conf.Tovary_Od_List_TablePartRecord(OdPointer, "м2"));
            //newObj.Od_List_TablePart.Save(false);

            newObj.Od_List_TablePart.Read();
            foreach (Conf.Tovary_Od_List_TablePartRecord tovary_Od_List_TablePartRecord in newObj.Od_List_TablePart.Records)
            {
                Conf.Od_Objest od_Objest = tovary_Od_List_TablePartRecord.Od_Pointer.GetDirectoryObject();
                Console.WriteLine(od_Objest.Name + ", " + od_Objest.FullName);

                //od_Objest.FullName = "Full: " + od_Objest.Name;
                //od_Objest.Save();

                tovary_Od_List_TablePartRecord.FullName = od_Objest.FullName;
            }

            newObj.Od_List_TablePart.Save();

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
