using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using System.IO;
using System.Xml;

namespace ZRTSModel.TechTree
{
	/// <summary>
	/// This class will be used to define the "Tech-Tree" of the game. It will define what units and buildings the player must
	/// have in order to produce/build newer buildings and units.
	/// 
	/// Example XML: Defines a TechTree that contains a building "house" and a unit "soldier." "house" has no requirements while
	/// "soldier" requires a house.
	/// 
	/// <TechTree>
	///		<Requirements type = "building" btype = "house">
	///			<ReqList>
	///			</ReqList>
	///		</Requirements>
	/// 
	///		<Requirements type = "unit" utype = "soldier">
	///			<ReqList>
	///				<Req type = "building" btype = "house"/>
	///			</ReqList>
	///		</Requirements>
	/// </TechTree>
	/// </summary>
	public class TechTree
	{
		Dictionary<UnitStats, ReqList> unitReqs; // Requirements for units.
		Dictionary<BuildingStats, ReqList> buildingReqs; // Requirements for buildings.
	}
}
