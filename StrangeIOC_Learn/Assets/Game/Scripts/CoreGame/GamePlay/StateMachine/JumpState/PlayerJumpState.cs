using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerJumpState", menuName = "State/Player/PlayerJumpState")]
public class PlayerJumpState : State
{
    public float forceJump=7f;
    public float duration = 0.05f;
    float countTimeBufferJump = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.JUMP);
        controller.componentManager.Rotate();
        countTimeBufferJump = duration;
        controller.componentManager.jumpCount += 1;
        if (controller.componentManager.speedMove != 0)
        {
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.maxSpeedMove * controller.componentManager.transform.localScale.x, forceJump);
        }
        else
        {
            controller.componentManager.rgbody2D.velocity = new Vector2(0f, forceJump);
        }
        
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
            if (countTimeBufferJump < 0)
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
        countTimeBufferJump -= Time.deltaTime;
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if(controller.componentManager.CanDash)
            controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if(controller.componentManager.CanJump)
            EnterState();
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
        controller.ChangeState(NameState.SkillState);
    }
}
