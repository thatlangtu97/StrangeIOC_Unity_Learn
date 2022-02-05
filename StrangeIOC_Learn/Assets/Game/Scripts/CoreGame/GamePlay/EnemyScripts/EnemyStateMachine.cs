using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineController
{
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (currentState != null)
        {
            currentState.OnInputAttack();
        }
    }
    public override void OnInputAttack(int idState)
    {
        base.OnInputAttack();
        if (currentState != null)
        {
            currentState.OnInputAttack();
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (currentState != null)
        {
            if (dictionaryStateMachine.ContainsKey(NameState.SkillState))
            {
                dictionaryStateMachine[NameState.SkillState].idState = idSkill;
                currentState.OnInputSkill(idSkill);
            }
        }
    }
}
