using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAttackState", menuName = "State/Player/PlayerAttackState")]
public class PlayerAttackState : State
{
    //public int idState;
    float timeCount=0;
    bool isEnemyForwark;
    public float timeBuffer = 0.15f;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        idState = 0;
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount < eventCollectionData[idState].durationAnimation)
        {
            isEnemyForwark = controller.componentManager.checkEnemyForwark();
            if (!isEnemyForwark)
            {
                Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeCount), eventCollectionData[idState].curveY.Evaluate(timeCount));
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.deltaTime;
                controller.componentManager.rgbody2D.velocity = Vector2.zero;
            }
            
            if (controller.componentManager.isBufferAttack == true && (timeCount + timeBuffer) > eventCollectionData[idState].durationAnimation) {
                timeCount += timeBuffer;
            }
            else
            {
                timeCount += Time.deltaTime;
                if ((timeCount + timeBuffer) > eventCollectionData[idState].durationAnimation)
                {
                    if (controller.componentManager.checkGround() == false)
                    {
                        controller.ChangeState(NameState.FallingState);
                    }
                }
            }

            
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                idState += 1;
                if(idState== eventCollectionData.Count)
                {
                    if (controller.componentManager.speedMove != 0)
                    {
                        controller.ChangeState(NameState.MoveState);
                    }
                    else
                    {
                        controller.ChangeState(NameState.IdleState);
                    }
                    return;
                }
                CastSkill();
            }
            else
            {
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
                    controller.ChangeState(NameState.FallingState);
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
        ResetEvent();
        isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        timeCount = 0;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        controller.componentManager.isBufferAttack = false;

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
        if (idState >= eventCollectionData.Count) return;
        if (timeCount >= eventCollectionData[idState].durationAnimation)
            controller.ChangeState(NameState.MoveState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.componentManager.isBufferAttack = true;
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (controller.componentManager.checkGround() == true)
        {
            controller.ChangeState(NameState.SkillState);
        }
        else
        {
            controller.ChangeState(NameState.AirSkillState);
        }
    }
}
