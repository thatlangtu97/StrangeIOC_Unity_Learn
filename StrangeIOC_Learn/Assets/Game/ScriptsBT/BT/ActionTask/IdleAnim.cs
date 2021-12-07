using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : Action
{
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();

       // componentManager.Value.entity.animatorContainer.animator.SetTrigger(AnimationName.IDLE);
    }
    public override TaskStatus OnUpdate()
    {
        if (componentManager.Value.entity.isEnabled)
        {
            componentManager.Value.entity.isStopMove = true;
            return componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputIdle();
        }
        return TaskStatus.Failure;
    }
}
