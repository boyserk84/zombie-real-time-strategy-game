using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace ZRTSModel
{
    public class ScenarioXMLReader
    {
        private XmlReader reader;

        private ScenarioXMLReader()
        { }

        public ScenarioXMLReader(Stream stream)
        {
            reader = XmlReader.Create(stream);
        }

        public ScenarioComponent GenerateScenarioFromXML()
        {
            if (reader.Read())
            {
                // Move to the XML element.
                reader.MoveToElement();
                ModelComponentFactoryFactory factoryBuilder = new ModelComponentFactoryFactory();
                while (reader.Read())
                {
                    reader.MoveToElement();
                    ModelComponent component = null; 
                }
                Console.WriteLine("XmlTextReader Properties Test");
                Console.WriteLine("===================");
                // Read this element's properties and display them on console
                Console.WriteLine("Name:" + reader.Name);
                Console.WriteLine("Base URI:" + reader.BaseURI);
                Console.WriteLine("Local Name:" + reader.LocalName);
                Console.WriteLine("Attribute Count:" + reader.AttributeCount.ToString());
                Console.WriteLine("Depth:" + reader.Depth.ToString());
                Console.WriteLine("Node Type:" + reader.NodeType.ToString());
                Console.WriteLine("Attribute Count:" + reader.Value.ToString());
            }
            return null;
        }
    }
}
