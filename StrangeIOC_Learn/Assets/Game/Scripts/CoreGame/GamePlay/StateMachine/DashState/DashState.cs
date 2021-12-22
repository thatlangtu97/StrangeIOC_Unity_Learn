using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashState", menuName = "State/DashState")]
public class DashState : State
{
    public float duration = 0.4f;
    public float speedDash = 10f;
    float countTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        //controller.animator.SetTrigger(AnimationTriger.DASH);
        controller.componentManager.dashCount += 1;
        controller.animator.Play(AnimationTriger.DASH);
        controller.componentManager.Rotate();
        countTime = duration;
        
    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (countTime >= 0)
        {
            Vector3 newVelocity = new Vector2(speedDash * controller.componentManager.transform.localScale.x, 0f);
            controller.componentManager.rgbody2D.velocity = new Vector2(speedDash * controller.componentManager.transform.localScale.x, 0f);
            countTime -= Time.deltaTime;
            //cancel dash
            if((newVelocity.x * controller.componentManager.speedMove) < 0)
            {
                countTime = -1f;
            }
            //if(newVelocity.x > 0 && controller.componentManager.speedMove<0 || newVelocity.x < 0 && controller.componentManager.speedMove > 0)
            //{
            //    countTime = -1f;
            //    //ExitState();
            //    //if (controller.componentManager.checkGround() == true)
            //    //{
            //    //    if (controller.componentManager.speedMove != 0)
            //    //    {
            //    //        controller.ChangeState(controller.moveState);
            //    //    }
            //    //    else
            //    //    {
            //    //        controller.ChangeState(controller.idleState);
            //    //    }
            //    //}
            //    //else
            //    //{
                    
            //    //    controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
            //    //    controller.componentManager.Rotate();
            //    //    newVelocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            //    //    if (controller.componentManager.checkWall() == true)
            //    //    {
            //    //        newVelocity.x = 0;
            //    //    }
            //    //    controller.componentManager.rgbody2D.velocity = newVelocity;

            //    //}
            //}
        }
        else
        {
            if (controller.componentManager.checkGround() == true)
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
            else
            {
                controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
                controller.componentManager.Rotate();
                Vector3 newVelocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
                if (controller.componentManager.checkWall() == true)
                {
                    newVelocity.x = 0;
                }
                controller.componentManager.rgbody2D.velocity = newVelocity;
                             
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        countTime = -1;
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.checkGround() == true)
        {
            controller.componentManager.ResetDashCount();
            controller.componentManager.ResetAttackAirCount();
        }
        if (controller.componentManager.CanJump)
            controller.ChangeState(controller.jumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        if (controller.componentManager.checkGround() == true && countTime<0f)
        {
            if (controller.componentManager.speedMove != 0)
            {
                controller.ChangeState(controller.moveState);
            }
        }
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.checkGround() == true)
            controller.ChangeState(controller.dashAttack);
        else
        {
            if(controller.componentManager.CanAttackAir)
                controller.ChangeState(controller.airAttackState);
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(controller.skillState);
    }
}
