using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CalculateRangeToEnemyTask : Action
{
    public SharedFloat rangeToEnemy;
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
        float space = componentManager.Value.enemy.position.x - componentManager.Value.transform.position.x;
        rangeToEnemy.Value = Mathf.Abs(space);
    }
}
