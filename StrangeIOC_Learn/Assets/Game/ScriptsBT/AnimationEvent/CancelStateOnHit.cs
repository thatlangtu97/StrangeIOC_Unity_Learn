using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelStateOnHit : MonoBehaviour
{
    public StateMachineController stateMachine;
    //call thong qua animation event
    public void OnHit()
    {
        stateMachine.isHit = true;
    }
    public void OnHitDone()
    {
        stateMachine.isHit = false;
    }
}
