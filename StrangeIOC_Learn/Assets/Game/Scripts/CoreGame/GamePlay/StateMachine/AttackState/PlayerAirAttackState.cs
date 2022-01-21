using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAirAttackState", menuName = "State/Player/PlayerAirAttackState")]
public class PlayerAirAttackState : State
{
    float timeCount;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        controller.componentManager.attackAirCount += 1;
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount < eventCollectionData[idState].durationAnimation)
        {

            if (controller.componentManager.checkGround() == false)
            {
                controller.componentManager.Rotate();
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            }
            //else
            //{
            //    if (controller.componentManager.speedMove != 0)
            //    {
            //        controller.ChangeState(NameState.MoveState);
            //    }
            //    else
            //    {
            //        controller.ChangeState(NameState.IdleState);
            //    }
            //}
            timeCount += Time.deltaTime;
            if (timeCount >= eventCollectionData[idState].durationAnimation)
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
                    controller.ChangeState(NameState.MoveState);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState);
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
    }
    public void CastSkill()
    {
        controller.componentManager.Rotate();
        timeCount = 0;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.rgbody2D.velocity.x, eventCollectionData[idState].curveY.Evaluate(0));
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if (controller.componentManager.CanDash)
            controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.CanJump)
            controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.isAttack)
        {
            if (idState < eventCollectionData.Count - 1)
            {
                idState = (idState + 1);
                CastSkill();
            }
        }
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
