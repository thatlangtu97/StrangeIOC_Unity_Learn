using UnityEngine;
[CreateAssetMenu(fileName = "EnemyDieState", menuName = "State/Enemy/EnemyDieState")]
public class EnemyDieState : State
{
    public float duration = 1f;
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.BehaviorTree.DisableBehavior();
        controller.animator.SetTrigger(AnimationTriger.DIE);
        //controller.componentManager.timeScale = 0;
        //controller.animator.speed = 0f;
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
            controller.componentManager.DestroyEntity();
            controller.gameObject.SetActive(false);
            Destroy(controller.gameObject);
        }
    }
    public override void OnRevive()
    {
        if (coutTime <= 0)
        {
            base.OnRevive();
            controller.ChangeState(NameState.ReviveState, true);
        }
    }
}
