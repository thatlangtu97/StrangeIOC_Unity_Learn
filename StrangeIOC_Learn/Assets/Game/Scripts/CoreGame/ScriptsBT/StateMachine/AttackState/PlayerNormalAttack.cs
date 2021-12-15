using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerNormalAttack", menuName = "State/PlayerNormalAttack")]
public class PlayerNormalAttack : State
{
    //public AttackComboConfig comboNormalAttack;
    public List<AttackConfig> skillDatas= new List<AttackConfig>();
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
            durationVelocity-= Time.deltaTime;
            if (durationVelocity > 0)
            {
                Vector2 velocityAttack = skillDatas[currentCombo].velocity;
                Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
            }
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                currentCombo = (currentCombo+1) % (skillDatas.Count);
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
        timeCount = skillDatas[currentCombo].durationAnimation;
        controller.animator.SetTrigger(skillDatas[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = skillDatas[currentCombo].durationVelocity;
        controller.componentManager.isBufferAttack = false;
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
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(controller.moveState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if(currentCombo != skillDatas.Count-1)
        controller.componentManager.isBufferAttack = true;
        //controller.ChangeState(controller.attackState);
    }
}
