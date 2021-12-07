using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnMoveSetConfig : MonoBehaviour
{
    public StateMachineController stateMachine;
    public Rigidbody2D rg;
    public Vector2 extra;
    public bool disable;
    public List<int> forceForwardOnGroundCombo = new List<int>();
    public List<int> forceOnAirCombo = new List<int>();
    public int forceDown;
    public int forceUp;
    [ShowInInspector]
    private int forceAdded;
    [ShowInInspector]
    private int movePower;
    private float duration = 0.5f;
    

    public void AddForceForward()
    {
        rg.AddForce(Vector2.right * forceAdded);
    }

    public void AddForceUp()
    {
        stateMachine.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
        rg.AddForce(Vector2.up * forceAdded);
        //Debug.Log(forceAdded);
        
    }

    public void AddForceDown()
    {
        stateMachine.componentManager.entity.rigidbodyContainer.rigidbody.velocity = Vector2.zero;
        if (forceDown != 0)
        {
            rg.gravityScale = 3.5f;
            rg.AddForce(-Vector2.up * forceDown);
        }
    }

    public void MoveForward()
    {   
        if (stateMachine.currentState != stateMachine.attackState)
            return;

        if (stateMachine.componentManager.entity.hasDash)
        {
            stateMachine.componentManager.entity.ReplaceDash(duration, duration, movePower * 0.1f, movePower * 0.1f);
        }
        else
        {
            stateMachine.componentManager.entity.AddDash(duration, duration, movePower * 0.1f, movePower * 0.1f);
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

    public void SetForceUp()
    {
        forceAdded = forceUp;
    }
}
