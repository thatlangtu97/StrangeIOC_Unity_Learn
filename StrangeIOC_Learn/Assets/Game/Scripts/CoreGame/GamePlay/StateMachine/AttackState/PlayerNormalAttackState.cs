using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerNormalAttackState", menuName = "State/PlayerNormalAttackState")]
public class PlayerNormalAttackState : State
{
    //public AttackComboConfig comboNormalAttack;
    public int currentCombo;
    public List<AttackConfig> skillDatas= new List<AttackConfig>();
    public List<IComboEvent> EventCombo;

    float timeCount=0;
    float durationVelocity=0;
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
                //Vector2 velocityAttack = skillDatas[currentCombo].velocity;
                //Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                Vector2 velocityAttack = new Vector2( skillDatas[currentCombo].curveX.Evaluate(durationVelocity),skillDatas[currentCombo].curveY.Evaluate(durationVelocity));
                //Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
            }
            timeCount -= Time.deltaTime;
            durationVelocity -= Time.deltaTime;
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                //currentCombo = (currentCombo+1) % (skillDatas.Count);
                currentCombo += 1;
                if(currentCombo== skillDatas.Count)
                {
                    if (controller.componentManager.speedMove != 0)
                    {
                        controller.ChangeState(controller.moveState);
                    }
                    else
                    {
                        controller.ChangeState(controller.idleState);
                    }
                    return;
                }
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
    }
    public void CastSkill()
    {
        base.ResetTrigger();
        isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        timeCount = skillDatas[currentCombo].durationAnimation;
        controller.animator.SetTrigger(skillDatas[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = skillDatas[currentCombo].durationVelocity;
        controller.componentManager.isBufferAttack = false;
        //controller.stateEventTriger.Invoke(NameStateEvent.AttackStart.ToString(),0f);
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
        controller.componentManager.isBufferAttack = true;
        //if (currentCombo < skillDatas.Count-1)
        //{
        //    //if (timeCount < skillDatas[currentCombo].durationAnimation * 0.5f)
        //        controller.componentManager.isBufferAttack = true;
        //}
        
    }
}
