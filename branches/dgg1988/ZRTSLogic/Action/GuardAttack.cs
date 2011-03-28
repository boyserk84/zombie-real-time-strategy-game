using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;
using ZRTSModel.GameWorld;
namespace ZRTSLogic.Action
{
	public class GuardAttack : ActionCommand
	{
		MoveAction moveAction = null;
		AttackAction attackAction = null;
		Entity target; // target Entity of the AttackAction
		GameWorld gw;
		Unit unit; // Unit performing the AttackAction
		const float MAX_DIST = 10.0f;
		bool targetDead = false;
		public GuardAttack(Unit unit, Entity target, GameWorld gw)
		{
			this.unit = unit;
			this.target = target;
			this.gw = gw;
			this.actionType = ActionType.GuardAttack;
		}

		public override bool work()
		{
			State targetState = target.getState();

			if (!targetDead && targetState.getPrimaryState() == State.PrimaryState.Dead || targetState.getPrimaryState() == State.PrimaryState.Remove)
			{
				targetDead = true;
			}

			float dis = EntityLocController.findDistance(unit.x, unit.y, target.x, target.y); 

			if (targetDead || dis > MAX_DIST)
			{
				if (moveAction == null)
				{
					moveAction = new MoveAction(unit.getGuardCell().Xcoord, unit.getGuardCell().Ycoord, gw, unit);
				}

				return moveAction.work();
			}
			else
			{
				if (attackAction == null)
				{
					attackAction = new AttackAction(unit, target, gw);
				}
				attackAction.work();
			}


			return false;
		}
	}
}
