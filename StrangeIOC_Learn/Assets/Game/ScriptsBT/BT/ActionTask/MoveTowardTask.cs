using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardTask :Action
{
    public SharedComponentManager componentManager;
    
    public override void OnStart()
    {
        base.OnStart();
        //componentManager.Value.entity.animatorContainer.animator.SetBool(AnimationName.MOVE, true);
        //componentManager.Value.entity.isStopMove = false;
       
        //Debug.Log("Move");
    }
    public override TaskStatus OnUpdate()
    {
        return componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputMove();
       // return TaskStatus.Running;

    }
    public override void OnEnd()
    {
        base.OnEnd();
        //componentManager.Value.entity.animatorContainer.animator.SetBool(AnimationName.MOVE, false);
        //componentManager.Value.entity.isStopMove = true;
        //Debug.Log("Stop");
    }
}
