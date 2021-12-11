using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMatchine : StateMachineController
{
    public override void Update()
    {
        UpdateState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        componentManager.checkGround();
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentState.OnInputDash();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            currentState.OnInputAttack();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeState(dieState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState.OnInputJump();
        }
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        ChangeState(dashState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        componentManager.isBufferAttack = true;
        ChangeState(attackState);
    }
}
