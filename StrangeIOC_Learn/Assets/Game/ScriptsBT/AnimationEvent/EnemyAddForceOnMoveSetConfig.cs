using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAddForceOnMoveSetConfig : MonoBehaviour
{
    public StateMachineController stateMachine;
    public Rigidbody2D rg;
    public Vector2 extra;
    public bool disable;
    public List<int> forceForwardOnGroundCombo = new List<int>();
    public List<int> forceOnAirCombo = new List<int>();
    
   
    [ShowInInspector]
    private int forceAdded;
    [ShowInInspector]
    public int movePower;


    public void MoveFoward()
    {
        if (stateMachine.componentManager.entity.hasDash)
        {
            stateMachine.componentManager.entity.ReplaceDash(0.5f, 0.5f, movePower * 0.1f, movePower * 0.1f);
        }
        else
        {
            stateMachine.componentManager.entity.AddDash(0.5f, 0.5f, movePower * 0.1f, movePower * 0.1f);
        }
    }

    public void SetForceOnGroundCombo(int combo)
    {
        movePower = forceForwardOnGroundCombo[combo];
    }


    public void SetForceOnAirCombo(int combo)
    {
        forceAdded = forceOnAirCombo[combo];
    }

}
