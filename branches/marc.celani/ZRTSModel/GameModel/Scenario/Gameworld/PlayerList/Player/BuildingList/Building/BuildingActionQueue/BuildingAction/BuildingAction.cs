using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel
{
	class BuildingAction : ModelComponent
	{
		public abstract bool Work();
		public override void Accept(ModelComponentVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
