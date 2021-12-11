using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CalculateRangeToEnemyTask : Conditional
{
    public SharedFloat rangeToEnemy;
    public SharedComponentManager componentManager;
    public bool isUseDistance;
    public float dir;
    public override TaskStatus OnUpdate()
    {
                dir = componentManager.Value.enemy.position.x - componentManager.Value.transform.position.x;
                if (dir == 0) dir = 0.05f;
                if (isUseDistance)
                {
                    rangeToEnemy.Value = dir;
                }
                else
                {
                    rangeToEnemy.Value = dir;

                }
        return TaskStatus.Success;


    }
}
