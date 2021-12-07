using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckWallTask : Conditional
{
    public SharedComponentManager componentManager;
    public override TaskStatus OnUpdate()
    {
        if(componentManager.Value.entity.checkWall.isHitWall)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
