using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if (controller.attackSoundController != null)
        {
            controller.attackSoundController.StopSound();
        }
        // controller.componentManager.entity.isStopMove = false;
    }
    public override void ExitState()
    {
        base.ExitState();
        //controller.componentManager.entity.isStopMove = true;
    }
}
