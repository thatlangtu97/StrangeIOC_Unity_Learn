using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMatchine : StateMachineController
{

    public override void UpdateState()
    {
        base.UpdateState();
        //componentManager.checkGround();
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
        if (currentState == null) return;
        base.OnInputSkill(idSkill);
        bool check = false;
        if (dictionaryStateMachine.ContainsKey(NameState.SkillState))
        {
            if (dictionaryStateMachine[NameState.SkillState].eventCollectionData.Count> idSkill)
               dictionaryStateMachine[NameState.SkillState].idState = idSkill;
            else
            {
                check = true;
            }
            
        }
        if (dictionaryStateMachine.ContainsKey(NameState.AirSkillState))
        {
            if (dictionaryStateMachine[NameState.AirSkillState].eventCollectionData.Count > idSkill)
                dictionaryStateMachine[NameState.AirSkillState].idState = idSkill;
            else
            {
                check = true;
            }
        }
        if(!check)
            currentState.OnInputSkill(idSkill);
    }
}
