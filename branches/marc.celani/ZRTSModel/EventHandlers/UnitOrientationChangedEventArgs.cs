using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSModel.EventHandlers
{
	public class UnitOrientationChangedEventArgs
	{
		public UnitComponent unit;
		public int oldOrientation;
		public int newOrientation;

		public UnitOrientationChangedEventArgs(UnitComponent unit, int oldOrientation, int newOrientation)
		{
			this.unit = unit;
			this.oldOrientation = oldOrientation;
			this.newOrientation = newOrientation;
		}
	}
}
