using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.XPath;

namespace AccountingSoftware
{
    //Конфігурація
    //В цьому класі має міститися вся інформація про конфігурацію
    public class Configuration
    {
        public Configuration()
        {
            Constants = new Dictionary<string, ConfigurationConstants>();
            Directories = new Dictionary<string, ConfigurationDirectories>();
            Documents = new Dictionary<string, ConfigurationDocuments>();
            Enums = new Dictionary<string, ConfigurationEnums>();
            Registers = new Dictionary<string, ConfigurationRegisters>();
        }

        public string Name { get; set; }

        public Dictionary<string, ConfigurationConstants> Constants { get; set; }

        public Dictionary<string, ConfigurationDirectories> Directories { get; set; }

        public Dictionary<string, ConfigurationDocuments> Documents { get; set; }

        public Dictionary<string, ConfigurationEnums> Enums { get; set; }

        public Dictionary<string, ConfigurationRegisters> Registers { get; set; }

        public static void Load(string pathToConf, Configuration Conf)
        {
            XPathDocument xPathDoc = new XPathDocument(pathToConf);
            XPathNavigator xPathDocNavigator = xPathDoc.CreateNavigator();

            //Довідники
            XPathNodeIterator directoryNodes = xPathDocNavigator.Select("/Configuration/Directories/Directory");
            while (directoryNodes.MoveNext())
            {
                string nameNodeValue = directoryNodes.Current.SelectSingleNode("Name").Value;

                ConfigurationDirectories ConfObjectDirectories = new ConfigurationDirectories();
                ConfObjectDirectories.Name = nameNodeValue;

                Conf.Directories.Add(nameNodeValue, ConfObjectDirectories);

                LoadFields(ConfObjectDirectories.Fields, directoryNodes.Current);
            }
        }

        private static void LoadFields(Dictionary<string, ConfigurationObjectField> fields, XPathNavigator xPathDocNavigator)
        {
            XPathNodeIterator fieldNodes = xPathDocNavigator.Select("Fields/Field");
            while (fieldNodes.MoveNext())
            {
                string nameNodeValue = fieldNodes.Current.SelectSingleNode("Name").Value;
                string typeNodeValue = fieldNodes.Current.SelectSingleNode("Type").Value;

                ConfigurationObjectField ConfObjectField = new ConfigurationObjectField();
                ConfObjectField.Name = nameNodeValue;
                ConfObjectField.Type = typeNodeValue;

                fields.Add(nameNodeValue, ConfObjectField);
            }
        }
    }
}