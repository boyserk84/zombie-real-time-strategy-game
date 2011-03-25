using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSLogic;
using ZRTSModel;
using ZRTSModel.GameModel;
namespace ZRTSLogic.Action
{
    /// <summary>
    /// This class will represent a simple attack where the unit will attack the target as long as the target is alive and inside
    /// the unit's range.
    /// </summary>
    public class SimpleAttackUnitAction : EntityAction
    {
        private UnitComponent unit;
        private UnitComponent target;

        private int ticksSinceLastAttk = 0;
        public SimpleAttackUnitAction(UnitComponent unit, UnitComponent target)
        {
            this.unit = unit;
            this.target = target;
        }

        private bool targetIsInRange()
        {
            float distance = (float)Math.Sqrt(Math.Pow((double)(unit.PointLocation.X - target.PointLocation.X), 2.0) + Math.Pow((double)(unit.PointLocation.Y - target.PointLocation.Y), 2.0));
            return distance <= unit.AttackRange;
        }

		private void updateUnitOrientation()
		{
			PointF directionVector = new PointF((target.PointLocation.X - unit.PointLocation.X), (target.PointLocation.Y - unit.PointLocation.Y));
            unit.Orientation = (int)Math.Atan2((double)directionVector.Y, (double)directionVector.X);
		}

        public override bool Work()
        {
            bool cannotAttack = target.CurrentHealth <= 0;
            if (!cannotAttack)
            {
                if (targetIsInRange())
                {
                    if (ticksSinceLastAttk % (unit.AttackTicks * 10) == 0)
                    {
                        updateUnitOrientation();
                        ticksSinceLastAttk = 0;
                        target.CurrentHealth -= unit.Attack;
                        if (target.CurrentHealth <= 0)
                        {
                            cannotAttack = true;
                        }
                    }
                    ticksSinceLastAttk++;
                }
                else
                {
                    cannotAttack = true;
                }
            }
            return cannotAttack;
        }
    }
}
