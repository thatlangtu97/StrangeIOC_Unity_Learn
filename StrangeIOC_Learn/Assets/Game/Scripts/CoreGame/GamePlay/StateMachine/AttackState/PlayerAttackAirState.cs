using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAttackAirState", menuName = "State/PlayerAttackAirState")]
public class PlayerAttackAirState : State
{
    //public AttackComboConfig comboNormalAttack;
    public int currentCombo;
    public List<AttackConfig> skillDatas;

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
            if (timeCount < 0)
            {
                controller.animator.SetTrigger(AnimationTriger.AIRATTACKFAIL);
            }
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
        controller.componentManager.isAttack = false;
        currentCombo = 0;
    }
    public void CastSkill()
    {
        controller.componentManager.Rotate();
        timeCount = skillDatas[currentCombo].durationAnimation;
        controller.animator.SetTrigger(skillDatas[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.rgbody2D.velocity.x, skillDatas[currentCombo].velocity.y);
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
            if(currentCombo< skillDatas.Count)
                CastSkill();
        }
    }
}
