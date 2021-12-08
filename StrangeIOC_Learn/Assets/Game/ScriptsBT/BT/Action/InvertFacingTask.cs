using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class InvertFacingTask : Action
{
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        componentManager.Value.OnInputChangeFacing();
        return TaskStatus.Success;
    }
}
