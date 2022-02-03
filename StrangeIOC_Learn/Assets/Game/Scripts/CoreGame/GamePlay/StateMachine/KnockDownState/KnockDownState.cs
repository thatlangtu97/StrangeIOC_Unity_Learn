using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockDownState", menuName = "State/KnockDownState")]
public class KnockDownState : State
{
    bool isFailing = false;
    float timeCount = 0;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        timeCount = 0;
        if(controller.componentManager.BehaviorTree)
        controller.componentManager.BehaviorTree.DisableBehavior();
        //controller.animator.SetTrigger(AnimationTriger.IDLE);
        controller.componentManager.rgbody2D.velocity = new Vector2 (0f, /*controller.componentManager.rgbody2D.velocity.y*/ 0f);
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        //isFailing = false;
        //controller.componentManager.ResetJumpCount();
        //controller.componentManager.ResetDashCount();
        //controller.componentManager.ResetAttackAirCount();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGroundBoxCast == true && timeCount>= eventCollectionData[idState].durationAnimation)
        {
            controller.ChangeState(NameState.KnockUpState);
        }
        timeCount += Time.deltaTime;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    //public override void OnInputAttack()
    //{
    //    base.OnInputAttack();
    //    controller.ChangeState(NameState.AttackState);
    //}
    //public override void OnInputDash()
    //{
    //    base.OnInputDash();
    //    controller.ChangeState(NameState.DashState);
    //}
    //public override void OnInputJump()
    //{
    //    base.OnInputJump();
    //    controller.ChangeState(NameState.JumpState);
    //}
    //public override void OnInputMove()
    //{
    //    base.OnInputMove();
    //    controller.ChangeState(NameState.MoveState);
    //}
    //public override void OnHit()
    //{
    //    base.OnHit();
    //    controller.ChangeState(NameState.HitState);
    //}
    //public override void OnInputSkill(int idSkill)
    //{
    //    base.OnInputSkill(idSkill);
    //    controller.ChangeState(NameState.SkillState);
    //}
}