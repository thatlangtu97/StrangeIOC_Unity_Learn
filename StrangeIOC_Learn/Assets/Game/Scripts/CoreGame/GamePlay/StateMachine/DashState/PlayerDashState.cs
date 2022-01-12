using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDashState", menuName = "State/Player/PlayerDashState")]
public class PlayerDashState : State
{
    public float duration = 0.4f;
    public float speedDash = 10f;
    float countTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        //controller.animator.SetTrigger(AnimationTriger.DASH);
        controller.componentManager.dashCount += 1;
        controller.animator.Play(AnimationTriger.DASH);
        controller.componentManager.Rotate();
        countTime = duration;
        
    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (countTime >= 0)
        {
            Vector3 newVelocity = new Vector2(speedDash * controller.componentManager.transform.localScale.x, 0f);

            Vector2 velocityAttack = new Vector2(eventData[idState].curveX.Evaluate(countTime), eventData[idState].curveY.Evaluate(countTime));
            controller.componentManager.rgbody2D.velocity = new Vector2(velocityAttack.x * controller.componentManager.transform.localScale.x, 0f);
            countTime -= Time.deltaTime;
            //cancel dash
            if((newVelocity.x * controller.componentManager.speedMove) < 0)
            {
                countTime = -1f;
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
                controller.animator.SetTrigger(AnimationTriger.JUMPFAIL);
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
        countTime = -1;
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.checkGround() == true)
        {
            controller.componentManager.ResetDashCount();
            controller.componentManager.ResetAttackAirCount();
        }
        if (controller.componentManager.CanJump)
            controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        if (controller.componentManager.checkGround() == true && countTime<0f)
        {
            if (controller.componentManager.speedMove != 0)
            {
                controller.ChangeState(NameState.MoveState);
            }
        }
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.checkGround() == true)
            controller.ChangeState(NameState.DashAttackState);
        else
        {
            if(controller.componentManager.CanAttackAir)
                controller.ChangeState(NameState.AirAttackState);
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(NameState.SkillState);
    }
}
