using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSModel.Player;
using ZRTSModel.Factories;
using System.IO;
using System.Xml;

namespace ZRTSModel.TechTree
{
	/// <summary>
	/// This class will define the Buildings and Units needed by the player to produce a certain Unit or Building.
	/// 
	/// Example XML:
	/// 
	/// <ReqList>
	///		<Req rtype = "building" btype = "house"/>
	///		<Req rtype = "unit" utype = "soldier"/>
	/// </ReqList>
	/// </summary>
	public class ReqList
	{
		BuildingStats bstats = null;
		UnitStats ustats = null;
		List<UnitStats> unitReqs;
		List<BuildingStats> buildingReqs;
		public enum ReqType {Building, Unit};
		public ReqType type;

		/// <summary>
		/// Constructor that defines the requirements for a Unit.
		/// </summary>
		/// <param name="unitReqs">List of UnitStats of Units needed.</param>
		/// <param name="buildingReqs">List of BuildingStats of Buildings needed</param>
		/// <param name="ustats">UnitStats of the kind of Unit the requirements are being defined for.</param>
		public ReqList(List<UnitStats> unitReqs, List<BuildingStats> buildingReqs, UnitStats ustats)
		{
			this.unitReqs = unitReqs;
			this.buildingReqs = buildingReqs;
			this.ustats = ustats;
			type = ReqType.Unit;
		}

		/// <summary>
		/// Constructor, builds a ReqList defined by the XMl string for a Unit defined by the UnitStats. 
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="ustats"></param>
		public ReqList(string xml, UnitStats ustats)
		{
			this.unitReqs = new List<UnitStats>();
			this.buildingReqs = new List<BuildingStats>();
			this.ustats = ustats;
			type = ReqType.Unit;
			readReqXML(xml);
		}

		/// <summary>
		/// Constructor that defines the requirements for a Building.
		/// </summary>
		/// <param name="unitReqs">List of UnitStats of Units needed.</param>
		/// <param name="buildingReqs">List of BuildingStats of Buildings needed.</param>
		/// <param name="bstats">BuildingStats of the kind of Building the requirements are being defined for.</param>
		public ReqList(List<UnitStats> unitReqs, List<BuildingStats> buildingReqs, BuildingStats bstats)
		{
			this.unitReqs = unitReqs;
			this.buildingReqs = buildingReqs;
			this.bstats = bstats;
			type = ReqType.Building;
		}

		/// <summary>
		/// Contructor. Given an xml string and a BuildingStats object, this constructor will create a ReqList object for the 
		/// Building defined by bstats based on the requirements described in the xml string.
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="bstats"></param>
		public ReqList(string xml, BuildingStats bstats)
		{
			this.unitReqs = new List<UnitStats>();
			this.buildingReqs = new List<BuildingStats>();
			this.bstats = bstats;
			type = ReqType.Building;
			readReqXML(xml);
		}

		/// <summary>
		/// Given a Player, this function will check if that player meets the requirements to produce the Unit or Building.
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public bool playerMeetsReqs(Player.Player player)
		{
			return true;
		}

		private void readReqXML(string xml)
		{
			XmlReader reader = XmlReader.Create(new StringReader(xml));
			bool hasMoreReqs = true;

			while (hasMoreReqs)
			{
				hasMoreReqs = reader.ReadToFollowing("Req");

				if (hasMoreReqs)
				{
					string rtype = reader.GetAttribute("rtype");
					if (rtype.ToUpper().Equals("UNIT"))
					{
						string unitType = reader.GetAttribute("utype");

						unitReqs.Add(UnitFactory.Instance.getStats(unitType));
						Console.WriteLine(unitType);
					}
					else if (rtype.ToUpper().Equals("BUILDING"))
					{
						string buildingType = reader.GetAttribute("btype");

						buildingReqs.Add(BuildingFactory.Instance.getStats(buildingType));
						Console.WriteLine(buildingType);
					}
				}
			}
		}
	}
}