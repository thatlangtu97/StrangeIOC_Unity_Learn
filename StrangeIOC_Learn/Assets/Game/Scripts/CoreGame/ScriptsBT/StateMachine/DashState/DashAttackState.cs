using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashAttackState", menuName = "State/DashAttackState")]
public class DashAttackState : State
{
    public ComboSkillConfig comboNormalAttack;
    public int currentCombo;
    float timeCount;
    float durationVelocity;
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
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            durationVelocity -= Time.deltaTime;
            if (durationVelocity > 0)
            {
                Vector2 velocityAttack = comboNormalAttack.skillDatas[currentCombo].velocity;
                Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
            }
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                currentCombo = (currentCombo + 1) % (comboNormalAttack.skillDatas.Count);
                CastSkill();
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
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = comboNormalAttack.skillDatas[currentCombo].durationVelocity;
        controller.componentManager.isBufferAttack = false;
    }

    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(controller.jumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(controller.moveState);
    }

}
