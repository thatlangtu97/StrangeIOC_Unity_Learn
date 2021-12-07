using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IncreaseEnemyCount : Action
{
	public override TaskStatus OnUpdate()
	{
		if (GameModeManager.instance.mode != GameMode.BossRaid)
			LevelCreator.instance.currEnemyCount++;
		else
			BossRaidLevelCreator.instance.enemyCount++;
		return TaskStatus.Success;
	}
}