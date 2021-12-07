using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]

public class CheckEnemyLeft : Conditional
{
    public SharedComponentManager componentManager;
    public SharedInt EnemyCount;

    public override TaskStatus OnUpdate()
    {
        if(LevelCreator.instance.currEnemyCount <= EnemyCount.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}
