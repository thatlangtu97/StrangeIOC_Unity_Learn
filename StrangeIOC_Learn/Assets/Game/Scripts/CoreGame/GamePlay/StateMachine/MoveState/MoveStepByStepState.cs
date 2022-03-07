using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MoveStepByStepState", menuName = "CoreGame/State/MoveStepByStepState")]
public class MoveStepByStepState : State
{
    bool isFailing = false;
    private float countTimeMovement;
    
    public float timeByStep=0.2f;
//    public float timeStop=0.2f;
    public AnimationCurve curveX;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        isFailing = false;
        controller.componentManager.ResetJumpCount();
        controller.componentManager.ResetDashCount();
        controller.componentManager.ResetAttackAirCount();
        countTimeMovement = 0;
    }
    public override void UpdateState()
    {
        if (countTimeMovement <= timeByStep)
        {
            
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove * curveX.Evaluate(countTimeMovement),
                controller.componentManager.rgbody2D.velocity.y);
            
        }
        else
        {
            countTimeMovement = 0;
            
        }
        countTimeMovement += Time.deltaTime;
        controller.componentManager.Rotate();
        if (controller.componentManager.checkGround() == false)
        {
            controller.ChangeState(NameState.FallingState);
        }
        else
        {
            if (controller.componentManager.speedMove == 0)
            {
                controller.ChangeState(NameState.IdleState);
            }
            else
            {
                if (isFailing == true)
                {
                    EnterState();
                }
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.ChangeState(NameState.AttackState);
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
