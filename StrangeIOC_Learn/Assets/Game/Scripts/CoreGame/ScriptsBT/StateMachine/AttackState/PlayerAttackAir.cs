using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAttackAir", menuName = "State/PlayerAttackAir")]
public class PlayerAttackAir : State
{
    public AttackComboConfig comboNormalAttack;
    public int currentCombo;
    float timeCount;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        controller.componentManager.attackAirCount += 1;
        currentCombo = 0;
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount >= 0)
        {

            if (controller.componentManager.checkGround() == false)
            {
                controller.componentManager.Rotate();
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            }
            else
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
            timeCount -= Time.deltaTime;
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
                controller.animator.SetTrigger(AnimationTriger.AIRATTACKFAIL);
                controller.componentManager.Rotate();
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
                
            }
        }

    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
        currentCombo = 0;
    }
    public void CastSkill()
    {
        controller.componentManager.Rotate();
        timeCount = comboNormalAttack.skillDatas[currentCombo].durationAnimation;
        controller.animator.SetTrigger(comboNormalAttack.skillDatas[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.rgbody2D.velocity.x, comboNormalAttack.skillDatas[currentCombo].velocity.y);
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if (controller.componentManager.CanDash)
            controller.ChangeState(controller.dashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.CanJump)
            controller.ChangeState(controller.jumpState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.isAttack)
        {
            currentCombo = (currentCombo + 1);
            if(currentCombo< comboNormalAttack.skillDatas.Count)
                CastSkill();
        }
    }
}
