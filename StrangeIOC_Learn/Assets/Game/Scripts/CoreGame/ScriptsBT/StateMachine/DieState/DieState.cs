using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "State/DieState")]
public class DieState : State
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
            //Destroy(controller.gameObject);
            controller.componentManager.DestroyEntity();
            controller.gameObject.SetActive(false);
        }
    }
    public override void OnRevive()
    {
        if (coutTime <= 0)
        {
            base.OnRevive();
            controller.currentState = controller.reviveState;
            controller.currentState.EnterState();
        }
    }
}
