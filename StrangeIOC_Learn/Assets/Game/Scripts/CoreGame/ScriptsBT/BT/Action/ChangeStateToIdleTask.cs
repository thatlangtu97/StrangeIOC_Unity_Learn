using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class ChangeStateToIdleTask : Action
{
    public SharedComponentManager componentManager;
    StateMachineController stateMachine;
    public override void OnStart()
    {
        base.OnStart();

        if (!componentManager.Value.isAttack)
        {
            stateMachine = componentManager.Value.stateMachine;
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}
