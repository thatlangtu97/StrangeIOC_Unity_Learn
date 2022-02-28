using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "CoreGame/State/DieState")]
public class DieState : State
{
    public override void EnterState()
    {
        base.EnterState();
        if(controller.componentManager.BehaviorTree)
            controller.componentManager.BehaviorTree.DisableBehavior();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.DestroyEntity();
        
        //controller.gameObject.SetActive(false);
            //Destroy(controller.gameObject);
    }
    public override void OnRevive()
    {
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            base.OnRevive();
            controller.ChangeState(NameState.ReviveState, true);
        }
    }
}
