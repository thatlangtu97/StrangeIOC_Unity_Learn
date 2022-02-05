using UnityEngine;
[CreateAssetMenu(fileName = "EnemyDieState", menuName = "State/Enemy/EnemyDieState")]
public class EnemyDieState : State
{
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.BehaviorTree.DisableBehavior();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        coutTime = 0;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        coutTime += Time.deltaTime;
        if (coutTime > eventCollectionData[idState].durationAnimation)
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        {
            controller.gameObject.SetActive(false);
        }
    }
    public override void OnRevive()
    {
        if (coutTime > eventCollectionData[idState].durationAnimation)
        {
            base.OnRevive();
            controller.ChangeState(NameState.ReviveState, true);
        }
    }
}
