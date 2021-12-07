using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if (controller.componentManager.entity.health.HP.Value > 0)
        {
            controller.animator.SetBool(AnimationName.MOVE, false);
            controller.animator.SetTrigger(AnimationName.IDLE);
        }

        //set anim ve idle
        //set random  idle time
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
        // khoi tao state
        //setup cac gia tri mac dinh cho state
    }
    public override void UpdateState(float deltaTime)
    {
        base.UpdateState(deltaTime);
        //update idle time
        // check xem co enemy o sigh ko

    }
}
