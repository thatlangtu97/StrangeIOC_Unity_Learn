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
        if (Input.GetKeyDown(KeyCode.End))
        {
            ChangeState(reviveState,true);
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
        ChangeState(reviveState, true);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (currentState != null)
        {
            currentState.OnInputSkill(idSkill);
        }

    }
}
