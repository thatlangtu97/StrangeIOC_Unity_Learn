using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HitState", menuName = "State/HitState")]
public class HitState : State
{
    public float duration = 0.4f;
    float coutTime = 0;
    int hit = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.BehaviorTree.DisableBehavior();
        hit = (hit + 1);
        //if (hit % 2 == 0)
        //    controller.animator.SetTrigger(AnimationTriger.HIT);
        //else
        //    controller.animator.SetTrigger(AnimationTriger.HIT2);
        if (hit % 2 == 0)
            controller.animator.Play(AnimationTriger.HIT);
        else
            controller.animator.Play(AnimationTriger.HIT2);

        coutTime = duration;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (coutTime < 0)
        {
            controller.ChangeState(controller.idleState);
            //ExitState();
        }
        coutTime -= Time.deltaTime;
    }
    public override void ExitState()
    {
        base.ExitState();
        coutTime = 0;
        controller.componentManager.BehaviorTree.EnableBehavior();
    }
    public override void OnHit()
    {
        base.OnHit();
        EnterState();
    }
}
