using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnState", menuName = "CoreGame/State/SpawnState")]
public class SpawnState : State
{
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        if(controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            //controller.ChangeState(NameState.IdleState);
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState);
                }
            }
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
        if (controller.componentManager.BehaviorTree && controller.componentManager.BehaviorTree.enabled==true)
        {
            
            controller.componentManager.BehaviorTree.EnableBehavior();
        }
    }
}
