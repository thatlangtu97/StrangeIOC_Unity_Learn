using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnState", menuName = "State/SpawnState")]
public class SpawnState : State
{
    public float duration;
    float coutTime;
    public override void EnterState()
    {
        
        controller.animator.SetTrigger(AnimationTriger.SPAWN);
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
        controller.ChangeState(NameState.IdleState);

    }
}
