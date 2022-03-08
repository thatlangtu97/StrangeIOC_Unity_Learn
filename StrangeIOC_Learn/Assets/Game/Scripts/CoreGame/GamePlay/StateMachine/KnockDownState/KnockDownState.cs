using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockDownState", menuName = "CoreGame/State/KnockDownState")]
public class KnockDownState : State
{
    bool isFailing = false;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        if(controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
        controller.componentManager.rgbody2D.velocity = new Vector2 (0f, 0f);
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGround() == true && timeTrigger>= eventCollectionData[idState].durationAnimation)
        {
            controller.ChangeState(NameState.KnockUpState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void OnInputDash()
    {
        if (timeTrigger >= 0.1f)
        {
            base.OnInputDash();
            controller.ChangeState(NameState.RollOutState);
        }
    }
}