using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSModel.GameWorld;

namespace ZRTSLogic
{
	/// <summary>
	/// This class will control updates to the visiblity map (Cell.explored)
	/// 
	/// Daniel Gephardt
	/// </summary>
	public class VisibilityMapLogic
	{
		GameWorld gw;
		Map map;

		public VisibilityMapLogic(GameWorld gw)
		{
			this.gw = gw;
			this.map = gw.map;
		}

		/// <summary>
		/// This method will update the visibility map to show that all Cells in a Units visibilityRange have been explored.
		/// </summary>
		/// <param name="unit"></param>
		public void updateVisMap(Unit unit)
		{
			byte range = (byte)unit.stats.visibilityRange;
			byte offset = (byte)(range / 2);

			int xStart = (short)unit.x - offset;
			int xEnd = (short)unit.x + offset;
			int yStart = (short)unit.y - offset;
			int yEnd = (short)unit.y + offset;

			// Make sure that our bounds are valid. (Assumes that no Unit has a visibility range longer than the map.)
			if (xStart < 0)
			{
				xStart = 0;
			}
			else if (xEnd >= map.width)
			{
				xEnd = map.width;
			}

			if (yStart < 0)
			{
				yStart = 0;
			}
			else if (yEnd >= map.height)
			{
				yEnd = map.height;
			}

			// Set all cell explored flags to true.
			for (int i = xStart; i < xEnd; i++)
			{
				for (int j = yStart; j < yEnd; j++)
				{
					map.getCell(i, j).explored = true;
				}
			}
		}
	}
}
