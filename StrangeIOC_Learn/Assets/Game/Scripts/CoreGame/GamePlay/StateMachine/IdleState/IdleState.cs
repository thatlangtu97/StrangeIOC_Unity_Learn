using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "State/IdleState")]
public class IdleState : State
{
    bool isFailing = false;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        //controller.animator.SetTrigger(AnimationTriger.IDLE);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        isFailing = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGroundBox == false)
        {
            controller.ChangeState(NameState.FallingState);
            //controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
            //Vector3 newVelocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            //if (controller.componentManager.checkWall() == true)
            //{
            //    newVelocity.x = 0;
            //}
            //controller.componentManager.rgbody2D.velocity = newVelocity;
            //isFailing = true;
        }
        else
        {
            //if (isFailing == true)
            //{
            //    EnterState();
            //}
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.ResetJumpCount();
        controller.componentManager.ResetDashCount();
        controller.componentManager.ResetAttackAirCount();
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.ChangeState(NameState.AttackState);
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        Debug.Log(controller.dictionaryStateMachine[NameState.JumpState]);
        controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(NameState.MoveState);
    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(NameState.SkillState);
    }
}
