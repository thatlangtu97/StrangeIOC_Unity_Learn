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
    public float distance;
    public override TaskStatus OnUpdate()
    {
        float space = componentManager.Value.enemy.position.x - componentManager.Value.transform.position.x;
        rangeToEnemy.Value = Mathf.Abs(space);
        if (rangeToEnemy.Value < distance)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
        //if (dir == 0) dir = 0.05f;
        //if (isUseDistance)
        //{
        //    rangeToEnemy.Value = dir;
        //}
        //else
        //{
        //    rangeToEnemy.Value = dir;

        //}
        //return TaskStatus.Success;


    }
}
