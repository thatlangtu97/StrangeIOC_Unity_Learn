using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpState", menuName = "CoreGame/State/JumpState")]
public class JumpState : State
{
    float countTimeBufferJump = 0;
    public float timeBuffer = 0.15f;
    bool bufferJump = false;

    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.Rotate();
        countTimeBufferJump = eventCollectionData[idState].durationAnimation;
        controller.componentManager.jumpCount += 1;
        Vector2 velocity = new Vector2(0f, eventCollectionData[idState].curveY.Evaluate(0));
        if (controller.componentManager.speedMove != 0)
        {
            velocity.x = controller.componentManager.maxSpeedMove * controller.componentManager.transform.localScale.x;
        }
        controller.componentManager.rgbody2D.velocity = velocity;
        bufferJump = false;
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
            if (bufferJump && controller.componentManager.CanJump)
            {
                if (countTimeBufferJump < timeBuffer)
                {
                    EnterState();
                }
            }
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
        if (controller.componentManager.CanDash)
            controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        bufferJump = true;
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
