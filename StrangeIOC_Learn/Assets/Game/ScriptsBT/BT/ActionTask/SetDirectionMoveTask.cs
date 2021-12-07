using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class SetDirectionMoveTask : Action
{

    public Vector2 direction;
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        
        
    }
    public override TaskStatus OnUpdate()
    {
        componentManager.Value.entity.moveByDirection.direction = direction;
        return componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputMove();
    }
}
