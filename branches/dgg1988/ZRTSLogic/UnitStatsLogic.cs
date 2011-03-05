using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSLogic
{
	public class UnitStatsLogic
	{
		/// <summary>
		/// Will update the temporary stats of the Unit.
		/// </summary>
		/// <param name="unit"></param>
		public static void updateUnit(Unit unit)
		{
			updateAttackBuff(unit);
			updateSpeedBuff(unit);
		}

		private static void updateAttackBuff(Unit unit)
		{
			double baseAttack = 0.5 + 0.5 * (unit.health / unit.stats.maxHealth);
			unit.attackBuff = baseAttack;
		}

		private static void updateSpeedBuff(Unit unit)
		{
			double baseSpeed = 0.5 + 0.5 * (unit.health / unit.stats.maxHealth);
			unit.speedBuff = baseSpeed;
		}
	}
}
