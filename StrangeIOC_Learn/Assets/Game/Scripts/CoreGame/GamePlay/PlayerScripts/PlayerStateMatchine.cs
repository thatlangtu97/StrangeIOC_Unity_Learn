using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMatchine : StateMachineController
{

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
            ChangeState(NameState.DieState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState.OnInputJump();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            ChangeState(NameState.ReviveState, true);
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
        ChangeState(NameState.ReviveState, true);
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (currentState != null)
        {
            if (dictionaryStateMachine.ContainsKey(NameState.SkillState))
            {
                dictionaryStateMachine[NameState.SkillState].idState = idSkill;
            }
            if (dictionaryStateMachine.ContainsKey(NameState.AirSkillState))
            {
                dictionaryStateMachine[NameState.AirSkillState].idState = idSkill;
            }
            currentState.OnInputSkill(idSkill);
        }

    }
}
