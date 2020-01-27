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
            OdSelect.QuerySelect.Where.Add(new Where("name", Comparison.EQ, "м."));
            OdSelect.QuerySelect.Limit = 1;
            OdSelect.Select();

            Conf.Od_Pointer OdPointer = new Conf.Od_Pointer();

            if (OdSelect.MoveNext())
                OdPointer = OdSelect.Current;

            Conf.Tovary_Objest newObj = new Conf.Tovary_Objest();
            newObj.New();
            newObj.name = "New Obj 6";
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

                Console.WriteLine(obj.name + ", " + obj.od2.UnigueID.ToString());

                //obj.code = obj.UnigueID.ToString();
                //obj.description = "description";

                //obj.field1 = "field1";
                //obj.field2 = "field2";
                //obj.field3 = "field3";
                //obj.field4 = "field4";
                //obj.field5 = "field5";
                //obj.Save();                
            }
                       

            Conf.Config.Kernel.Close();

            //Query q = new Query();

            //q.Table = "public.tovary";

            //q.Field.Add("Name", "");
            //q.Field.Add("Desc", "");
            //q.Field.Add("Code", "");

            //q.Where.Add(new Where("Name", Comparison.EQ, "Test", Comparison.AND));
            //q.Where.Add(new Where("Code", Comparison.EQ, "50", Comparison.Empty));

            //q.Order.Add("Name", SelectOrder.ASC);

            //q.Limit = 10;

            //Console.WriteLine(q.Construct());

            //Generation();

            //TestPostgres();

            Console.ReadLine();

           
        }

        static void TestPostgres()
        {
            NpgsqlConnection nCon = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=525491;Database=ConfTrade;");
            nCon.Open();

            NpgsqlCommand nCommand = new NpgsqlCommand(@"INSERT INTO public.tovary(uid, name, code, description) " +
                                                        "VALUES(@uid, @name, @code, @description)", nCon);

            nCommand.Parameters.Add(new NpgsqlParameter("uid", null));
            nCommand.Parameters.Add(new NpgsqlParameter("name", ""));
            nCommand.Parameters.Add(new NpgsqlParameter("code", "001"));
            nCommand.Parameters.Add(new NpgsqlParameter("description", "desc"));

            for (int i = 0; i < 10; i++)
            {
                nCommand.Parameters["uid"].Value = Guid.NewGuid();
                nCommand.Parameters["name"].Value = "Name " + i.ToString();

                Console.WriteLine(nCommand.ExecuteNonQuery());                
            }

            


            NpgsqlCommand nCommand2 = new NpgsqlCommand("SELECT * FROM public.tovary", nCon);

            NpgsqlDataReader reader = nCommand2.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["uid"]);
            }
            reader.Close();

            nCon.Close();
        }

        static void Generation()
        {
            XslCompiledTransform xsltCodeGnerator = new XslCompiledTransform();
            xsltCodeGnerator.Load(PathTemplate);

            xsltCodeGnerator.Transform(PathConf, @"D:\VS\Project\WebServerTestErlang\ConfTrade\CodeGeneration.cs");
        }

        public const string PathConf = @"D:\VS\Project\WebServerTestErlang\ConfTrade\Configuration.xml";
        public const string PathTemplate = @"D:\VS\Project\WebServerTestErlang\ConfTrade\CodeGeneration.xslt";

        public void Load(string path)
        {
            Configuration Conf = new Configuration();
            
            XPathDocument xpDoc = new XPathDocument(PathConf);
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Довідники
            XPathNodeIterator nodesDirectory = xpDocNavigator.Select("/Configuration/Directories/Directory");
            while (nodesDirectory.MoveNext())
            {
                XPathNavigator nameNode = nodesDirectory.Current.SelectSingleNode("Name");

                //ConfigurationObject ConfObject = new ConfigurationObject();
                //ConfObject.ConfObjectType = ConfigurationObjectType.Directory;
                //ConfObject.Fields.Add(


                //Conf.Directories.Add(nameNode.Value, ConfObject);
            }
        }

        public void TestGenericGonf()
        {
            //Directory.TovarySelect tSelect = new Directory.TovarySelect();
            //tSelect.Read();
            //int c = tSelect.DirectoryPointers.Count;

            //Directory.TovaryObjest tObject = tSelect.DirectoryPointers[0].GetDirectoryObject();
            //string code = tObject.Code;
        }
    }
}
