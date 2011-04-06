using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTSModel.Trigger
{
	public class WinWhenAllEnemyUnitsDead : TriggerDecorator
	{
		ConditionAllPlayerUnitsDead condition;
		PlayerLoseAction action;
		ScenarioComponent scenario;

		public WinWhenAllEnemyUnitsDead(PlayerComponent enemy, ScenarioComponent scenario)
		{
			condition = new ConditionAllPlayerUnitsDead(this,enemy);
			this.scenario = scenario;

			action = new PlayerLoseAction(this, scenario);
		}

		public override bool Eval()
		{
			//Console.WriteLine("Lose Eval");
			return CheckMyCondition();
		}

		public override bool CheckMyCondition()
		{
			//Console.WriteLine("CheckMyCondition: " + condition.CheckMyCondition());
			return condition.CheckMyCondition();
		}

		public override void PerformMyAction()
		{
			action.PerformMyAction();
		}

		public override void PerformActions()
		{
			PerformMyAction();
		}
	}

}
