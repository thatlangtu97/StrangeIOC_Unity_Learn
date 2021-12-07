using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChangeToStateTask : Action
{
    public StateCheck stateToCheck;
    public SharedComponentManager componentManager;


    public override void OnStart()
    {
        StateMachineController stateMachine = componentManager.Value.entity.stateMachineContainer.stateMachine;
        switch (stateToCheck)
        {
            case StateCheck.ATTACK:
                stateMachine.ChangeState(stateMachine.attackState);
                break;
            case StateCheck.IDLE:
                stateMachine.ChangeState(stateMachine.idleState);
                break;
            case StateCheck.DASH:
                stateMachine.ChangeState(stateMachine.dashState);
                break;
            case StateCheck.HIT:
                stateMachine.ChangeState(stateMachine.beHitState);
                break;
            case StateCheck.IN_AIR:
                stateMachine.ChangeState(stateMachine.jumpFallState);
                break;
            case StateCheck.KNOCKDOWN:
                stateMachine.ChangeState(stateMachine.knockDownState);
                break;
            case StateCheck.MOVE:
                stateMachine.ChangeState(stateMachine.moveState);
                break;
            case StateCheck.SKILL:
                stateMachine.ChangeState(stateMachine.skillState);
                break;
        }
    }
}