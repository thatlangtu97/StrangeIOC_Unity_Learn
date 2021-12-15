using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckInputRotateTask : Action
{
    public SharedComponentManager componentManager;

    public override TaskStatus OnUpdate()
    {
        if (!componentManager.Value.isAttack)
        {
            componentManager.Value.Rotate();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
