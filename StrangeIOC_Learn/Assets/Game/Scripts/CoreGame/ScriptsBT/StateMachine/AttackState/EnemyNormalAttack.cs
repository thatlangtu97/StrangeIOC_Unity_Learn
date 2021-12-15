using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyNormalAttack", menuName = "State/EnemyNormalAttack")]
public class EnemyNormalAttack : State
{
    public override void EnterState()
    {
        base.EnterState();
        //controller.componentManager.isAttack = true;
        controller.animator.SetTrigger(AnimationTriger.ATTACK);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(controller.beHitState);
    }
    //public override TaskStatus OnHit(bool isBlock)
    //{

    //    controller.ChangeState(controller.beHitState);
    //    return TaskStatus.Success;

    //}
    //public override TaskStatus OnInputSkill(int skillId)
    //{
    //    controller.componentManager.CurrentSkill(skillId);
    //    return TaskStatus.Failure;
    //}
    //public override TaskStatus OnInputIdle()
    //{

    //    return TaskStatus.Success;
    //}

    //public override TaskStatus OnInputMove()
    //{

    //    return TaskStatus.Success;
    //}
    ////public override TaskStatus OnInputChangeFacing()
    ////{

    ////    if(controller.isCanRotate)
    ////    {
    ////        //Debug.Log("Rotate");
    ////        base.OnInputChangeFacing();
    ////        return TaskStatus.Success;
    ////    }
    ////    return TaskStatus.Failure;
    ////}

    //public TaskStatus ForceExitSkillState()
    //{
    //    controller.ChangeState(controller.idleState);
    //    return TaskStatus.Success;
    //}
}
