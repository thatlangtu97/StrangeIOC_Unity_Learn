using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDashState", menuName = "State/Player/PlayerDashState")]
public class PlayerDashState : State
{
    float countTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        //controller.animator.SetTrigger(AnimationTriger.DASH);
        controller.componentManager.dashCount += 1;
        controller.animator.Play(eventCollectionData[idState].NameTrigger);
        controller.componentManager.Rotate();
        countTime = 0;
        
    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (countTime < eventCollectionData[idState].durationAnimation)
        {
            //Vector3 newVelocity = new Vector2(speedDash * controller.componentManager.transform.localScale.x, 0f);
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(countTime) * controller.componentManager.transform.localScale.x, eventCollectionData[idState].curveY.Evaluate(countTime));
            //Vector2 velocityAttack = new Vector2(eventData[idState].curveX.Evaluate(countTime), eventData[idState].curveY.Evaluate(countTime));
            controller.componentManager.rgbody2D.velocity = new Vector2(velocityAttack.x , velocityAttack.y);
            countTime += Time.deltaTime;
            //cancel dash
            if((velocityAttack.x * controller.componentManager.speedMove) < 0)
            {
                countTime = eventCollectionData[0].durationAnimation;
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
        countTime = eventCollectionData[idState].durationAnimation;
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
        if (controller.componentManager.checkGround() == true && countTime >= eventCollectionData[idState].durationAnimation)
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
