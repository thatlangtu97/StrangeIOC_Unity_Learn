using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemySkillState", menuName = "State/Enemy/EnemySkillState")]
public class EnemySkillState : State
{
    float timeCount;
    public override void EnterState()
    {
        base.EnterState();

        CastSkill();
        //controller.componentManager.rgbody2D.gravityScale = 0;
        controller.componentManager.rgbody2D.velocity = Vector2.zero;

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeCount < eventCollectionData[idState].durationAnimation)
        {
            timeCount += Time.fixedDeltaTime;
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeCount), eventCollectionData[idState].curveY.Evaluate(timeCount));
            Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
            controller.componentManager.rgbody2D.position += force * Time.fixedDeltaTime;
        }
        else
        {
            //controller.componentManager.rgbody2D.gravityScale = 2;
            controller.ChangeState(NameState.IdleState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        //controller.componentManager.rgbody2D.gravityScale = 2;
    }
    public void CastSkill()
    {
        ResetEvent();
        //controller.componentManager.Rotate();
        timeCount = 0;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
    }
    public override void OnInputDash()
    {
        if (timeCount < 0 && controller.componentManager.CanDash)
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
    }
    public override void OnInputJump()
    {
        if (timeCount < 0 && controller.componentManager.CanJump)
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
    }
    public override void OnInputMove()
    {
        if (timeCount < 0 && controller.componentManager.checkGround() == true)
        {
            base.OnInputMove();
            controller.ChangeState(NameState.MoveState);
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        if (timeCount < 0)
        {
            base.OnInputSkill(idSkill);
            idState = idSkill;
            EnterState();
        }
    }
}
