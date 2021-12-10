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
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnInputDash();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnInputAttack();
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
        ChangeState(attackState);
    }
}
