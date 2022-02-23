using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class StateMachineClone : StateMachineController
{
    public StateMachineController stateMachineParent;
    public List<NameState> listStateNeedClone;
    public void SetupStateClone()
    {
        States = new List<StateClone>();

        foreach (StateClone stateClone in stateMachineParent.States)
        {
            if(listStateNeedClone.Contains(stateClone.NameState))
                States.Add(stateClone);
        }
        
        
        dictionaryStateMachine = new Dictionary<NameState, State>();
        currentState = null;
        currentNameState = NameState.UnknowState;
        foreach (StateClone tempState in States) {
            CreateStateFactory(tempState);
        }
    }

    public override void InitStateMachine()
    {
        SetupStateClone();
        base.InitStateMachine();
    }

    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (currentState != null)
        {
            currentState.OnInputAttack();
        }
    }

    public override void UpdateState()
    {
        if(stateMachineParent.currentState!=null)
            if (stateMachineParent.currentNameState != currentNameState)
            {
                ChangeState(stateMachineParent.currentNameState,stateMachineParent.currentState.idState,true);
            }
        
        base.UpdateState();
        
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
