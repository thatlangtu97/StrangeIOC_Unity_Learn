using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMatchine : StateMachineController
{
    public override void Update()
    {
        //UpdateState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        componentManager.checkGround();
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            currentState.OnInputDash();
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

            currentState.OnInputAttack();
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    ChangeState(dieState);
        //}
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            currentState.OnInputJump();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //currentState.OnRevive();
            ChangeState(skillState);
        }

    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if (currentState != null)
        {
            currentState.OnInputDash();
        }
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (currentState != null)
        {
            currentState.OnInputAttack();
        }
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (currentState != null)
        {
            currentState.OnInputJump();
        }
    }
    public override void OnInputRevive()
    {
        base.OnInputRevive();
        currentState = reviveState;
        currentState.EnterState();
    }
}
