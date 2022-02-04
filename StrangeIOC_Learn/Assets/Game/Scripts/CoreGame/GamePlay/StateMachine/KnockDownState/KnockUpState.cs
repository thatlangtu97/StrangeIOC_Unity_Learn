using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockUpState", menuName = "State/KnockUpState")]
public class KnockUpState : State
{
    float timeCount = 0;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        timeCount = 0;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGroundBoxCast == true )
        {
            timeCount += Time.deltaTime;
            if (timeCount >= eventCollectionData[idState].durationAnimation)
            {
                controller.ChangeState(NameState.IdleState);
            }
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.EnableBehavior();
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
