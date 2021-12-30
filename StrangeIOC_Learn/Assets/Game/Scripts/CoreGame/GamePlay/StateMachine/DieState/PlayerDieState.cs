using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDieState", menuName = "State/Player/PlayerDieState")]
public class PlayerDieState : State
{
    public float duration = 1f;
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();        
        controller.animator.SetTrigger(AnimationTriger.DIE);
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
        coutTime = duration;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        coutTime -= Time.deltaTime;
        if (coutTime <= 0)
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        {
            //controller.gameObject.SetActive(false);
        }
    }
    public override void OnRevive()
    {
        if (coutTime <= 0)
        {
            base.OnRevive();
            controller.ChangeState(NameState.RreviveState, true);
        }
    }
}
