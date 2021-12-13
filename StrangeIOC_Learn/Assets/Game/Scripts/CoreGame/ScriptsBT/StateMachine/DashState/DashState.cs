﻿using System.Collections;
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
            controller.componentManager.rgbody2D.velocity = new Vector2(speedDash * controller.componentManager.transform.localScale.x, 0f);
            countTime -= Time.deltaTime;
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
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);               
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
        controller.ChangeState(controller.moveState);
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
}