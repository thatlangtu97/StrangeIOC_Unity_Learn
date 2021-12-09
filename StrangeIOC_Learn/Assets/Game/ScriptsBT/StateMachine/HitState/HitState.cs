using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HitState", menuName = "State/HitState")]
public class HitState : State
{
    float duration = 0.4f;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.BehaviorTree.DisableBehavior();
        controller.animator.SetTrigger(AnimationTriger.HIT);
        duration = 0.4f;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            //controller.componentManager.BehaviorTree.EnableBehavior();
            //controller.ChangeState(controller.idleState);
            ExitState();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        {
            controller.componentManager.BehaviorTree.EnableBehavior();
        }
    }

}
