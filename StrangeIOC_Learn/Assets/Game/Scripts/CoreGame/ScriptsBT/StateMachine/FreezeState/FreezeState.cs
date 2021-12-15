using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FreezeState", menuName = "State/FreezeState")]
public class FreezeState : State
{
    public float duration = 0.4f;
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.BehaviorTree.DisableBehavior();
        controller.componentManager.timeScale = 0;
        controller.animator.speed = 0f;
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
        controller.componentManager.timeScale = 1f;
        controller.animator.speed = 1f;
        
    }
}
