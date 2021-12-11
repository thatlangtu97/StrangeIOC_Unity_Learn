using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JumpState", menuName = "State/JumpState")]
public class JumpState : State
{

    public float forceJump=3f;
    public float duration = 0.05f;
    float countTimeBufferJump = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(AnimationTriger.JUMP);
        controller.componentManager.Rotate();
        if (controller.componentManager.speedMove != 0)
        {
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.maxSpeedMove * controller.componentManager.transform.localScale.x, forceJump);
        }
        else
        {
            controller.componentManager.rgbody2D.velocity = new Vector2(0f, forceJump);
        }
            countTimeBufferJump = duration;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGround() == false)
        {
            //if (controller.componentManager.speedMove < 0 && controller.transform.localScale.x < 0 ||
            //   controller.componentManager.speedMove > 0 && controller.transform.localScale.x > 0)
            //{
            //    controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            //}
            //else
            //{
            //    controller.componentManager.rgbody2D.velocity = new Vector2(0f, controller.componentManager.rgbody2D.velocity.y);
            //}
            controller.componentManager.Rotate();
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
        }
        else
        {
            if (countTimeBufferJump < 0)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(controller.moveState);
                }
                else
                {
                    controller.ChangeState(controller.idleState);
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
        controller.ChangeState(controller.dashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(controller.jumpState);
        EnterState();
    }
}
