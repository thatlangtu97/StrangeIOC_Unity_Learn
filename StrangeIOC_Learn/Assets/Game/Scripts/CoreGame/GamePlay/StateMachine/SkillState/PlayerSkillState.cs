using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSkillState", menuName = "State/Player/PlayerSkillState")]
public class PlayerSkillState : State
{
    float timeCount;
    float timeCurve = 0;
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
        if (timeCount < eventCollectionData[idState].durationAnimation )
        {
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeCount), eventCollectionData[idState].curveY.Evaluate(timeCount));
            Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
            controller.componentManager.rgbody2D.position += force * Time.fixedDeltaTime;
            timeCount += Time.deltaTime;
        }
        else
        {
            controller.componentManager.rgbody2D.gravityScale = 2;
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState);
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
        timeTrigger = 0;
        timeCount = 0;
        //if (controller.componentManager.checkGround() == true)
            controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        //else
        //    controller.animator.SetTrigger(eventData[idState].NameTriggerAir);
    }
    public override void OnInputDash()
    {
        if (timeCount >= eventCollectionData[idState].durationAnimation && controller.componentManager.CanDash)
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
    }
    public override void OnInputJump()
    {
        if (timeCount >= eventCollectionData[idState].durationAnimation && controller.componentManager.CanJump)
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
    }
    public override void OnInputMove()
    {
        if (timeCount >= eventCollectionData[idState].durationAnimation && controller.componentManager.checkGround() == true)
        {
            base.OnInputMove();
            controller.ChangeState(NameState.MoveState);
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        if (timeCount >= eventCollectionData[idState].durationAnimation)
        {
            base.OnInputSkill(idSkill);
            idState = idSkill;
            EnterState();
        }
    }
    public override void OnInputAttack()
    {
        if (timeCount >= eventCollectionData[idState].durationAnimation)
        {
            base.OnInputAttack();
            if (controller.componentManager.checkGround() == true)
            {
                controller.ChangeState(NameState.AttackState);
            }
            else
            {
                if (controller.componentManager.CanAttackAir)
                    controller.ChangeState(NameState.AirAttackState);
            }
        }
    }
}
