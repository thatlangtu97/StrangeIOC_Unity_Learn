using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSkillState", menuName = "State/PlayerSkillState")]
public class PlayerSkillState : State
{
    public List<AttackConfig> skillDatas;
    int idSkill=0;    
    float timeCount;
    float timeCurve = 0;
    List<int> idEventTrigged = new List<int>();
    //float durationVelocity;
    public override void EnterState()
    {
        base.EnterState();

        CastSkill();
        controller.componentManager.rgbody2D.gravityScale = 0;
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            if (timeCurve < skillDatas[idSkill].durationAnimation)
            {
                Vector2 velocityAttack = new Vector2(skillDatas[idSkill].curveX.Evaluate(timeCurve), skillDatas[idSkill].curveY.Evaluate(timeCurve));
                Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                
                controller.componentManager.rgbody2D.position += force*Time.deltaTime ;
            }
            foreach(IComboEvent comboevent in skillDatas[idSkill].eventConfig.EventCombo)            
            {
                if (timeCurve > comboevent.timeTrigger && !idEventTrigged.Contains(comboevent.id))
                {
                    comboevent.OnEventTrigger(controller.componentManager.entity);
                    idEventTrigged.Add(comboevent.id);
                }
            }
            timeCurve += Time.deltaTime;
        }
        else
        {
            Debug.Log("fail down");
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
                controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
                controller.componentManager.Rotate();
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);

            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.rgbody2D.gravityScale = 2;
    }
    public void CastSkill()
    {
        idEventTrigged.Clear();
        controller.componentManager.Rotate();
        timeCurve = 0;
        timeCount = skillDatas[idSkill].durationAnimation;
        controller.animator.SetTrigger(skillDatas[idSkill].NameTrigger);
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
    
}
