using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleAirState", menuName = "CoreGame/State/IdleAirState")]
public class IdleAirState : State
{
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        controller.componentManager.vectorSpeed = Vector2.zero;
        controller.componentManager.speedMove = 0f;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.speedMove != 0)
        {
            controller.ChangeState(NameState.MoveState);
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
    public override void OnInputMove()
    {
        base.OnInputMove();
        controller.ChangeState(NameState.MoveState);
    }
    public override void OnHit()
    {
        base.OnHit();
        controller.ChangeState(NameState.HitState);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(NameState.SkillState);
    }
}
