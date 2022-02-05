using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDieState", menuName = "State/Player/PlayerDieState")]
public class PlayerDieState : State
{
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();        
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
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
