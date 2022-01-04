using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckNeedMoveTask : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public float distanceStop;
    public float distanceBreak;

    public override TaskStatus OnUpdate()
    {
        if(rangeToEnemy.Value > distanceBreak)
        {
            componentManager.Value.speedMove = 0;
            return TaskStatus.Failure;
        }
        if (rangeToEnemy.Value < distanceStop)
        {
            componentManager.Value.speedMove = 0;
            return TaskStatus.Failure;
        }
        if (componentManager.Value.transform.localScale.x<0)
        {
            componentManager.Value.speedMove = -componentManager.Value.maxSpeedMove;
        }
        else if (componentManager.Value.transform.localScale.x > 0)
        {
            componentManager.Value.speedMove = componentManager.Value.maxSpeedMove;
        }
        return TaskStatus.Success;


    }
}
