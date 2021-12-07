using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnemyCountTask : Action
{
    public override TaskStatus OnUpdate()
    {
       

        if (EnemySpawnControllerBrick.instance != null)
        {
            EnemySpawnControllerBrick.instance.OnEnemyDie();

        }
        else
        {
            if (GameModeManager.instance.mode != GameMode.BossRaid)
                LevelCreator.instance.OnEnemyDie();
            else
                BossRaidLevelCreator.instance.OnEnemyBossLevelDie();
        }
        return TaskStatus.Success;
    }
}
