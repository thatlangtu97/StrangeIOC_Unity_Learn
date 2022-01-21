using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnState", menuName = "State/SpawnState")]
public class SpawnState : State
{
    float coutTime;
    public override void EnterState()
    {
        base.EnterState();
        controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        coutTime = 0;;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        coutTime += Time.deltaTime;
        if (coutTime > eventCollectionData[idState].durationAnimation)
        {
            controller.ChangeState(NameState.IdleState);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        
    }
}
