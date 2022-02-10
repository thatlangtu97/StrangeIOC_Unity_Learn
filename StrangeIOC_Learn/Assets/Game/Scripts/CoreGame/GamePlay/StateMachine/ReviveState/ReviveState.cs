using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ReviveState", menuName = "CoreGame/State/ReviveState")]
public class ReviveState : State
{
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
        }
        else
        {
            controller.componentManager.heal = 100;
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState, 0, true);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState, 0, true);
                }
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.EnableBehavior();
    }
}
