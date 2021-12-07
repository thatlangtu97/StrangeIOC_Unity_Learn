using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckStillInStateTask :Conditional
{
    public SharedComponentManager componentManager;
    StateMachineController stateMachine;
    public StateCheck stateToCheck;
    public override void OnStart()
    {
      stateMachine = componentManager.Value.entity.stateMachineContainer.stateMachine;
    }
    public override TaskStatus OnUpdate()
    {

        switch (stateToCheck)
        {
            case StateCheck.ATTACK:
                return stateMachine.currentState == stateMachine.attackState ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.IDLE:
                return stateMachine.currentState == stateMachine.idleState ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.DASH:
                return stateMachine.currentState == stateMachine.dashState  ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.HIT:
                return stateMachine.currentState == stateMachine.beHitState ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.IN_AIR:
                return stateMachine.currentState == stateMachine.jumpFallState  ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.KNOCKDOWN:
                return (stateMachine.currentState == stateMachine.knockDownState || stateMachine.currentState == stateMachine.getUpState) ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.MOVE:
                return stateMachine.currentState == stateMachine.moveState ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.SKILL:
                return stateMachine.currentState == stateMachine.skillState ? TaskStatus.Success : TaskStatus.Failure;
            case StateCheck.FREEZE:
                return stateMachine.currentState == stateMachine.freezeState ? TaskStatus.Success : TaskStatus.Failure;
            default:
                return TaskStatus.Failure;

        }
    }
}
public enum StateCheck
{
    MOVE, 
    IDLE,
    DASH,
    SKILL,
    ATTACK,
    KNOCKDOWN,
    HIT,
    DIE,
    IN_AIR,
    FREEZE
}
