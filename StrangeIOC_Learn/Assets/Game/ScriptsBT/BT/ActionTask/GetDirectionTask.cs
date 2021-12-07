using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]

public class GetDirectionTask : Action
{
    public SharedVector2 direction;
    public SharedComponentManager componentManager;
    public override void OnStart()
    {


    }
    public override TaskStatus OnUpdate()
    {
        direction.Value = componentManager.Value.entity.centerPoint.centerPoint.right;
        return TaskStatus.Success;
    }
}
