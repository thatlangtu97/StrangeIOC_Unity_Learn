using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMoveState", menuName = "State/PlayerMoveState")]
public class PlayerMoveState : MeleeMoveState
{
    public override void EnterState()
    {
        base.EnterState();
        
    }
    public override void UpdateState()
    {
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
        controller.componentManager.Rotate();
        if (controller.componentManager.speedMove == 0)
        {
            controller.ChangeState(controller.idleState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.ResetJumpCount();
        controller.componentManager.ResetDashCount();
        controller.componentManager.ResetAttackAirCount();
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.ChangeState(controller.attackState);
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(controller.dashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(controller.jumpState);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        controller.ChangeState(controller.skillState);
    }

}
