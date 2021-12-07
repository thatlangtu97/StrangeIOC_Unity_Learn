using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGroundTask : Conditional
{
    public SharedComponentManager componentManager;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (componentManager.Value.entity.checkGround.isOnGround)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
