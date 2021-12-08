using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class MoveTowardTask :Action
{
    public SharedComponentManager componentManager;
    
    public override void OnStart()
    {
        base.OnStart();
        componentManager.Value.stateMachine.ChangeState(componentManager.Value.stateMachine.moveState);
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
        //return componentManager.Value.stateMachine.currentState.OnInputMove();
    }
    public override void OnEnd()
    {
        base.OnEnd();
    }
}
