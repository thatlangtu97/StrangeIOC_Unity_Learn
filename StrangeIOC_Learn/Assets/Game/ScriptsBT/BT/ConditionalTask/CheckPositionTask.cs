using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositionTask : Conditional
{
    public SharedVector2 positionToCheck;
    public SharedComponentManager component;
    public override TaskStatus OnUpdate()
    {
        //Debug.Log(component.Value.entity.centerPoint.centerPoint.position+" -"+ positionToCheck.Value + Mathf.Abs(Vector2.Distance(component.Value.entity.centerPoint.centerPoint.position, positionToCheck.Value)));
        if(Mathf.Abs( Vector2.Distance(component.Value.entity.centerPoint.centerPoint.position, positionToCheck.Value))<=0.3f)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
