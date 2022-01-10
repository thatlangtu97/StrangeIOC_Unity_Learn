using UnityEngine;
[CreateAssetMenu(fileName = "PlayerAttackState", menuName = "State/Player/PlayerAttackState")]
public class PlayerAttackState : State
{
    //public int idState;
    float timeCount=0;
    float durationVelocity=0;
    bool isEnemyForwark;
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
        if (timeCount >= 0)
        {
            if (durationVelocity > 0 && !isEnemyForwark)
            {
                Vector2 velocityAttack = new Vector2( eventData[idState].curveX.Evaluate(durationVelocity), eventData[idState].curveY.Evaluate(durationVelocity));
                controller.componentManager.rgbody2D.position += new Vector2(velocityAttack.x * controller.transform.localScale.x, velocityAttack.y * controller.transform.localScale.y) * Time.fixedDeltaTime;
            }
            timeCount -= Time.deltaTime;
            durationVelocity -= Time.deltaTime;
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                idState += 1;
                if(idState== eventData.Count)
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
    }
    public void CastSkill()
    {
        base.ResetTrigger();
        isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        timeCount = eventData[idState].durationAnimation;
        controller.animator.SetTrigger(eventData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        durationVelocity = eventData[idState].durationVelocity;
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
        controller.ChangeState(NameState.SkillState);
    }
}
