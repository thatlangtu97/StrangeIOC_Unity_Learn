using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FreezeState", menuName = "CoreGame/State/FreezeState")]
public class FreezeState : State
{
    public float duration = 0.4f;
    public override void EnterState()
    {
        base.EnterState();
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
        controller.animator.speed = 0f;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            controller.ChangeState(NameState.IdleState);
            //ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.EnableBehavior();
        controller.animator.speed = 1f;
    }
}
