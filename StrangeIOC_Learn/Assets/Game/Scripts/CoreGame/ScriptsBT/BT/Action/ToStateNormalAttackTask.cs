using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class ToStateNormalAttackTask : Conditional
{
    public SharedComponentManager componentManager;
    StateMachineController stateMachine;
   /// public SharedFloat rangeToEnemy;
    public override void OnStart()
    {
        base.OnStart();
        stateMachine = componentManager.Value.stateMachine;
        stateMachine.ChangeState(stateMachine.attackState);
    }
    //public override TaskStatus OnUpdate()
    //{
    //    if(rangeToEnemy.Value <= 0.5f && rangeToEnemy.Value >= -0.5f)
    //    {
    //        componentManager.Value.isAttack = true;
    //        stateMachine.ChangeState(stateMachine.attackState);
    //        return TaskStatus.Success;
    //    }
    //    else
    //    {
    //        return TaskStatus.Failure;
    //    }
    //}
}
