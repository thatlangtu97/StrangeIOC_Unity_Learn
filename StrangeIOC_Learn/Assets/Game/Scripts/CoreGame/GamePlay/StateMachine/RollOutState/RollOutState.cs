using UnityEngine;
[CreateAssetMenu(fileName = "RollOutState", menuName = "CoreGame/State/RollOutState")]
public class RollOutState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.dashCount += 1;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        //controller.componentManager.Rotate();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            Vector2 velocityAttack = new Vector2(   eventCollectionData[idState].curveX.Evaluate(timeTrigger) * controller.componentManager.transform.localScale.x,
                                                    eventCollectionData[idState].curveY.Evaluate(timeTrigger));
            controller.componentManager.rgbody2D.velocity = new Vector2(velocityAttack.x, velocityAttack.y);
//            if ((velocityAttack.x * controller.componentManager.speedMove) < 0)
//            {
//                timeTrigger = eventCollectionData[0].durationAnimation;
//            }
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
    public override void ExitState()
    {
        base.ExitState();
    }
//    public override void OnInputJump()
//    {
//        base.OnInputJump();
//        if (controller.componentManager.checkGround() == true)
//        {
//            controller.componentManager.ResetDashCount();
//            controller.componentManager.ResetAttackAirCount();
//        }
//        if (controller.componentManager.CanJump)
//            controller.ChangeState(NameState.JumpState);
//    }
//    public override void OnInputMove()
//    {
//        base.OnInputMove();
//        if (controller.componentManager.checkGround() == true && timeTrigger >= eventCollectionData[idState].durationAnimation)
//        {
//            if (controller.componentManager.speedMove != 0)
//            {
//                controller.ChangeState(NameState.MoveState);
//            }
//        }
//    }
//    public override void OnInputAttack()
//    {
//        base.OnInputAttack();
//        if (controller.componentManager.checkGround() == true)
//            controller.ChangeState(NameState.DashAttackState);
//        else
//        {
//            if (controller.componentManager.CanAttackAir)
//                controller.ChangeState(NameState.AirAttackState);
//        }
//    }
//    public override void OnInputSkill(int idSkill)
//    {
//        base.OnInputSkill(idSkill);
//        if (controller.componentManager.checkGround() == true)
//        {
//            controller.ChangeState(NameState.SkillState);
//        }
//        else
//        {
//            controller.ChangeState(NameState.AirSkillState);
//        }
//    }
}
