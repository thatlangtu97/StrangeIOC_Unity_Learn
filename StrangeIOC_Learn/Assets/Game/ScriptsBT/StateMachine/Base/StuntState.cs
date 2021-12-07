using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StuntState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if (controller.allFxOfSkillChanelling != null)
        {
            controller.allFxOfSkillChanelling.SetActive((false));
        }
        controller.animator.SetBool(AnimationName.IS_ON_STUNT, true);
        controller.animator.SetTrigger(AnimationName.STUNT);
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, true);
        controller.componentManager.entity.isStopMove = true;
        controller.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }
    public override void ExitState()
    {
        //Debug.Log("exit");
        controller.animator.SetBool(AnimationName.LOCK_HIT_ANIM, false);
        controller.animator.SetBool(AnimationName.IS_ON_STUNT, false);
        controller.animator.ResetTrigger(AnimationName.HIT);
        controller.componentManager.entity.isStopMove = false;
        base.ExitState();

    }
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);

    }
    public override TaskStatus OnKnockDown()
    {
        EnterState();
        return TaskStatus.Success;
    }
    public override TaskStatus OnHit(bool isBlock)
    {
        return TaskStatus.Success;
    }
    public override TaskStatus OnInputIdle()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputSkill(int skillId)
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputMove()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnInputChangeFacing()
    {
        return TaskStatus.Failure;
    }
    public override TaskStatus OnFrezee(float duration) // ko the dong bang
    {
        return TaskStatus.Failure;
    }
}
