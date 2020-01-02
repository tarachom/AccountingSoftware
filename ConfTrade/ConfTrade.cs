using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using WebServerTestErlang.AccountingSoftware;

//Конфігурація Торгівля
namespace ConfTrade
{
    public class ConfTrade
    {
        static void Main(string[] args)
        {
            Generation();

            //Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
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

        
    }
}
