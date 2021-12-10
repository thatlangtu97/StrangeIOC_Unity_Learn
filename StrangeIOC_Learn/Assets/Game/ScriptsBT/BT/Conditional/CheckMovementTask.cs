﻿using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckMovementTask : Conditional
{
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
        if (!componentManager.Value.isAttack)
        {
                componentManager.Value.stateMachine.ChangeState(componentManager.Value.stateMachine.moveState);
        }
        else
        {
            componentManager.Value.stateMachine.ChangeState(componentManager.Value.stateMachine.idleState);
        }

    }
}
