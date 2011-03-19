using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace ZRTSModel
{
    public class ScenarioXMLWriter : ModelComponentVisitor
    {
        private XmlWriter output;
        private int numTabs;

        private ScenarioXMLWriter()
        { }

        public ScenarioXMLWriter(Stream stream)
        {
            output = XmlWriter.Create(stream);
        }

        public void BeginWrite()
        {
            output.WriteStartDocument();
        }

        public void EndWrite()
        {
            output.WriteEndDocument();
            output.Flush();
        }

        public void Visit(Sand sand)
        {
            output.WriteStartElement("Sand");
            VisitChildren(sand);
            output.WriteEndElement();
        }

        public void Visit(Mountain mountain)
        {
            output.WriteStartElement("Mountain");
            VisitChildren(mountain);
            output.WriteEndElement();
        }

        public void Visit(Grass grass)
        {
            output.WriteStartElement("Grass");
            VisitChildren(grass);
            output.WriteEndElement();
        }

        public void Visit(MapGold gold)
        {
            output.WriteStartElement("Gold");
            VisitChildren(gold);
            output.WriteEndElement();
        }

        public void Visit(MapMetal metal)
        {
            output.WriteStartElement("Metal");
            VisitChildren(metal);
            output.WriteEndElement();
        }

        public void Visit(MapWood wood)
        {
            output.WriteStartElement("Wood");
            VisitChildren(wood);
            output.WriteEndElement();
        }

        public void Visit(MapResource mapResource)
        {
            output.WriteStartElement("MapResource");
            VisitChildren(mapResource);
            output.WriteEndElement();
        }

        public void Visit(CellComponent cell)
        {
            output.WriteStartElement("Cell");
            VisitChildren(cell);
            output.WriteEndElement();
        }

        public void Visit(Map map)
        {
            output.WriteStartElement("Map");
            output.WriteElementString("width", map.GetWidth().ToString());
            output.WriteElementString("height", map.GetHeight().ToString());
            VisitChildren(map);
            output.WriteEndElement();
        }

        public void Visit(Gameworld gameworld)
        {
            output.WriteStartElement("Gameworld");
            VisitChildren(gameworld);
            output.WriteEndElement();
        }

        public void Visit(ScenarioComponent scenario)
        {
            output.WriteStartElement("Scenario");
            VisitChildren(scenario);
            output.WriteEndElement();
        }

        public void Visit(PlayerList list)
        {
            output.WriteStartElement("PlayerList");
            VisitChildren(list);
            output.WriteEndElement();
        }

        public void Visit(PlayerComponent player)
        {
            output.WriteStartElement("Player");
            output.WriteElementString("name", player.GetName());
            VisitChildren(player);
            output.WriteEndElement();
        }

        public void Visit(BuildingList list)
        {
            output.WriteStartElement("BuildingList");
            VisitChildren(list);
            output.WriteEndElement();
        }

        public void Visit(PlayerResources resources)
        {
            output.WriteStartElement("PlayerResources");
            VisitChildren(resources);
            output.WriteEndElement();
        }

        public void Visit(UnitList list)
        {
            output.WriteStartElement("UnitList");
            VisitChildren(list);
            output.WriteEndElement();
        }

        public void Visit(UnitComponent unit)
        {
            output.WriteStartElement("Unit");
            output.WriteElementString("type", unit.Type);
            VisitChildren(unit);
            output.WriteEndElement();
        }

        public void Visit(ActionQueue queue)
        {
            // We do not output the action queue, or any of its children.
        }

        public void Visit(ModelComponent component)
        {
            // Do nothing.
        }

        private void VisitChildren(ModelComponent component)
        {
            foreach (ModelComponent child in component.GetChildren())
            {
                child.Accept(this);
            }
        }
    }
}
