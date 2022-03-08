using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockUpState", menuName = "CoreGame/State/KnockUpState")]
public class KnockUpState : State
{
    float timeCount = 0;
    public override void InitState(StateMachineController controller)
    {
        base.InitState(controller);
    }
    public override void EnterState()
    {
        base.EnterState();
        timeCount = 0;
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controller.componentManager.checkGround() == true )
        {
            timeCount += Time.deltaTime;
            if (timeCount >= eventCollectionData[idState].durationAnimation)
            {
                controller.ChangeState(NameState.IdleState);
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.EnableBehavior();
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.RollOutState);
    }
}
