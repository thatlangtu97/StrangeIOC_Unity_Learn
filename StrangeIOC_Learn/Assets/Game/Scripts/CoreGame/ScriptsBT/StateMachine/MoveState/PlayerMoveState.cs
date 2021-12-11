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

}
