using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckGroundTask : Conditional
{
    public SharedComponentManager componentManager;
    public override TaskStatus OnUpdate()
    {
        if(componentManager.Value.entity.checkGround.isOnGround)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}
