﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyNormalAttackState", menuName = "State/EnemyNormalAttackState")]

public class EnemyNormalAttackState : State
{
    //public AttackComboConfig comboNormalAttack;
    public int currentCombo;
    //public List<AttackConfig> skillDatas;
    float timeCount = 0;
    float durationVelocity = 0;
    bool isEnemyForwark;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        currentCombo = 0;
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount >= 0)
        {
            if (durationVelocity > 0 && !isEnemyForwark)
            {
                Vector2 velocityAttack = new Vector2(   eventData[currentCombo].curveX.Evaluate(durationVelocity),
                                                        eventData[currentCombo].curveY.Evaluate(durationVelocity));
                controller.componentManager.rgbody2D.position += new Vector2(   velocityAttack.x * controller.transform.localScale.x,
                                                                                velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
            }
            timeCount -= Time.deltaTime;
            durationVelocity -= Time.deltaTime;
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                currentCombo += 1;
                //if (currentCombo == skillDatas.Count)
                //{
                //    if (controller.componentManager.speedMove != 0)
                //    {
                //        controller.ChangeState(controller.moveState);
                //    }
                //    else
                //    {
                //        controller.ChangeState(controller.idleState);
                //    }
                //    return;
                //}
                CastSkill();
            }
            else
            {
                //if (controller.componentManager.speedMove != 0)
                //{
                //    controller.ChangeState(controller.moveState);
                //}
                //else
                //{
                    controller.ChangeState(NameState.IdleState);
                //}
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
    }
    public void CastSkill()
    {
        base.ResetTrigger();
        isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        timeCount = eventData[currentCombo].durationAnimation;
        controller.animator.SetTrigger(eventData[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = eventData[currentCombo].durationVelocity;
        controller.componentManager.isBufferAttack = false;
        //controller.stateEventTriger.Invoke(NameStateEvent.AttackStart.ToString(),0f);
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(NameState.MoveState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.componentManager.isBufferAttack = true;
        //if (currentCombo < skillDatas.Count-1)
        //{
        //    //if (timeCount < skillDatas[currentCombo].durationAnimation * 0.5f)
        //        controller.componentManager.isBufferAttack = true;
        //}

    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
}
