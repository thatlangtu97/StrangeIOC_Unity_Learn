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
        base.UpdateState();
        if (controller.componentManager.speedMove == 1f)
        {
            controller.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (controller.componentManager.speedMove == -1f)
        {
            controller.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (controller.componentManager.speedMove == 0)
        {
            controller.ChangeState(controller.idleState);

        }
    }

}
