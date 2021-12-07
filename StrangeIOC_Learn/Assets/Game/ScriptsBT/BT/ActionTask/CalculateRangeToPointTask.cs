using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]

public class CalculateRangeToPointTask : Conditional
{
    public SharedFloat rangeToEnemy;
    public SharedVector2 targetPoint;
    public SharedComponentManager componentManager;
    public override TaskStatus OnUpdate()
    {
        float dir = targetPoint.Value.x - componentManager.Value.entity.centerPoint.centerPoint.position.x;
        if (dir == 0) dir = 0.01f;
        rangeToEnemy.Value = dir / Mathf.Abs(dir) * Vector2.Distance(targetPoint.Value, componentManager.Value.transform.position);
        return TaskStatus.Success;


    }
}
