using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDieState", menuName = "State/PlayerDieState")]
public class PlayerDieState : State
{
    public float duration = 1f;
    float coutTime = 0;
    public override void EnterState()
    {
        base.EnterState();
        
        controller.animator.SetTrigger(AnimationTriger.DIE);
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
}
