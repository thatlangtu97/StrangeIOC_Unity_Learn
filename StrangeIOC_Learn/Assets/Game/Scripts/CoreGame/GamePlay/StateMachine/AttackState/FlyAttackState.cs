using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlyAttackState", menuName = "CoreGame/State/FlyAttackState")]
public class FlyAttackState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        CastSkill();

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            controller.componentManager.Rotate(); 
            //controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
            controller.componentManager.rgbody2D.velocity = Vector2.zero;
        }
        else
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

    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
    }
    public void CastSkill()
    {
        ResetEvent();
        controller.componentManager.Rotate();
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
