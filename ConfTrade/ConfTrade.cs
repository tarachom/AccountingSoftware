using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using WebServerTestErlang.AccountingSoftware;

using Npgsql;

//Конфігурація Торгівля
namespace ConfTrade
{
    public class ConfTrade
    {
        static void Main(string[] args)
        {
            Generation();

            //TestPostgres();

            //Console.ReadLine();
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

                ConfigurationObject ConfObject = new ConfigurationObject();
                ConfObject.ConfObjectType = ConfigurationObjectType.Directory;
                //ConfObject.Fields.Add(


                Conf.Directories.Add(nameNode.Value, ConfObject);
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
