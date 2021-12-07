using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Patrol State", menuName = "State/Enemy/Spider/Move")]
public class SpiderMoveState : MeleeEnemyMoveState
{
    public override TaskStatus OnInputMove()
    {
        return TaskStatus.Success;
    }
    public override void EnterState()
    {
        controller.animator.SetBool(AnimationName.MOVE, true);
        controller.componentManager.entity.isStopMove = false;
    }
    public override TaskStatus OnHit(bool isBlock)
    {
        return TaskStatus.Success;
    }
    public override TaskStatus OnKnockDown()
    {
        return TaskStatus.Success;
    }

}
