using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertFacingTask : Action
{
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputChangeFacing();
        return TaskStatus.Success;
    }
}
