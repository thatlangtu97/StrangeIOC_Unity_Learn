using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FallingState", menuName = "CoreGame/State/FallingState")]
public class FallingState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
        controller.componentManager.Rotate();

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGround() == false)
        {
            controller.componentManager.Rotate();
            Vector3 newVelocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            if (controller.componentManager.checkWall() == true)
            {
                newVelocity.x = 0;
            }
            controller.componentManager.rgbody2D.velocity = newVelocity;
        }
        else
        {
            if (controller.componentManager.speedMove != 0)
            {
                controller.ChangeState(NameState.MoveState);
            }
            else
            {
                controller.ChangeState(NameState.IdleState);
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if (controller.componentManager.CanDash)
            controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        if (controller.componentManager.CanJump)
        {
            controller.ChangeState(NameState.JumpState);
            base.OnInputJump();
        }
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.CanAttackAir)
            controller.ChangeState(NameState.AirAttackState);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (controller.componentManager.checkGround() == true)
        {
            controller.ChangeState(NameState.SkillState);
        }
        else
        {
            controller.ChangeState(NameState.AirSkillState);
        }
    }
}
