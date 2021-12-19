using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSkillState", menuName = "State/PlayerSkillState")]
public class PlayerSkillState : State
{
    public SkillComboConfig comboNormalAttack;
    SkillConfig currentSkill;
    int idSkill=0;    
    float timeCount;
    float timeCurve = 0;
    //float durationVelocity;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.rgbody2D.gravityScale = 0;
        currentSkill = comboNormalAttack.skillDatas[idSkill];
        
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            //durationVelocity -= Time.deltaTime;
            if (timeCurve < currentSkill.durationAnimation)
            {
                Vector2 velocityAttack = new Vector2(currentSkill.curveX.Evaluate(timeCurve), currentSkill.curveY.Evaluate(timeCurve));
                Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                controller.componentManager.rgbody2D.velocity = Vector2.zero;
                controller.componentManager.rgbody2D.position += force*Time.deltaTime ;
            }
            timeCurve += Time.deltaTime;
        }
        else
        {
            controller.componentManager.rgbody2D.gravityScale = 2;
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
    }
    public void CastSkill()
    {
        currentSkill = comboNormalAttack.skillDatas[idSkill];
        controller.componentManager.Rotate();
        timeCount = currentSkill.durationAnimation;
        timeCurve = 0;
        controller.animator.SetTrigger(currentSkill.nameTrigger);
        //controller.componentManager.rgbody2D.velocity = Vector2.zero;
        //durationVelocity = comboNormalAttack.skillDatas[currentCombo].durationVelocity;
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
    //public override void OnInputAttack()
    //{
    //    base.OnInputAttack();
    //    if (timeCount < 0)
    //    {
    //        //controller.ChangeState(controller.attackState);
    //    }
    //}
}
