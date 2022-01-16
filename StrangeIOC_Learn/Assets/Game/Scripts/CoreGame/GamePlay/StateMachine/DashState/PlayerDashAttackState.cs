using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDashAttackState", menuName = "State/Player/PlayerDashAttackState")]
public class PlayerDashAttackState : State
{
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
            if (timeCount > 0)
            {
                //Vector2 velocityAttack = eventData[currentCombo].velocity;
                Vector2 velocityAttack = new Vector2(eventData[currentCombo].curveX.Evaluate(timeCount), eventData[currentCombo].curveY.Evaluate(timeCount));
                Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y);
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                currentCombo = (currentCombo + 1) % (eventData.Count);
                CastSkill();
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
        timeCount = eventData[currentCombo].durationAnimation;
        controller.animator.SetTrigger(eventData[currentCombo].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = eventData[currentCombo].durationVelocity;
        controller.componentManager.isBufferAttack = false;
    }

    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(NameState.MoveState);
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
