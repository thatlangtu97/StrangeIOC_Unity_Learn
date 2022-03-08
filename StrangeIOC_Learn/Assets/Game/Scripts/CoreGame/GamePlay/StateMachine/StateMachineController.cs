using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
public class StateMachineController : MonoBehaviour
{
    [DictionaryDrawerSettings(KeyLabel = "CustomKeyName", ValueLabel = "CustomValueLabel")]
    public Dictionary<NameState, State> dictionaryStateMachine = new Dictionary<NameState, State>();
    [BoxGroup("Current State")]
    public State currentState;
    [BoxGroup("Current State")]
    public NameState currentNameState;
    [LabelText("STATE TO CLONE")]
    public List<StateClone> States;
    public void SetupState()
    {
        dictionaryStateMachine = new Dictionary<NameState, State>();
        currentState = null;
        currentNameState = NameState.UnknowState;
        foreach (StateClone tempState in States) {
            CreateStateFactory(tempState);
        }
    }
    public ComponentManager componentManager;
    public Animator animator;
    public virtual void InitStateMachine()
    {
        SetupState();
        if (dictionaryStateMachine.ContainsKey(NameState.SpawnState))
        {
            ChangeState(NameState.SpawnState, 0, true);
        }
        else
        {
            ChangeState(NameState.IdleState, 0, true);
        }
    }
    public virtual void UpdateState()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
    public virtual void OnSpawn()
    {
    }
    public virtual void OnRevival()
    {
    }
    protected void CreateStateFactory(StateClone stateClone)
    {
        State state = Instantiate(stateClone.StateToClone);
        state.InitState(this);
        if (!dictionaryStateMachine.ContainsKey(stateClone.NameState))
        {
            dictionaryStateMachine.Add(stateClone.NameState, state);
        }
        else
        {
            dictionaryStateMachine[stateClone.NameState] = state;
        }
    }
    public virtual void ChangeState(NameState nameState, bool forceChange = false)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        if (!forceChange)
        {
            if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
            {
                if (nameState != currentNameState)
                {
                    if (currentState)
                    {
                        currentState.ExitState();
                    }
                    currentState = newState;
                    currentNameState = nameState;
                    currentState.EnterState();
                }
            }
        }
        else
        {
            if (currentState)
            {
                currentState.ExitState();
            }
            currentState = newState;
            currentNameState = nameState;
            currentState.EnterState();
        }
    }
    public virtual void ChangeState(NameState nameState, int idState, bool forceChange = false)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        newState.idState = idState;
        if (!forceChange)
        {
            if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
            {
                if (nameState != currentNameState)
                {
                    if (currentState)
                    {
                        currentState.ExitState();
                    }
                    currentState = newState;
                    currentNameState = nameState;
                    currentState.EnterState();
                }
            }
        }
        else
        {
            if (currentState)
            {
                currentState.ExitState();
            }
            currentState = newState;
            currentNameState = nameState;
            currentState.EnterState();
        }
    }
    public void SetIdState(NameState nameState, int idState)
    {
        if (!dictionaryStateMachine.ContainsKey(nameState)) return;
        State newState = dictionaryStateMachine[nameState];
        newState.idState = idState;
    }
    public virtual void OnInputState(NameState nameState,int idState)
    {
    }
    public virtual void OnInputAttack()
    {
    }
    public virtual void OnInputAttack(int idState)
    {

    }
    public virtual void OnInputJump()
    {
    }
    public virtual void OnInputMove()
    {
    }
    public virtual void OnInputDash()
    {
    }
    public virtual void OnInputRevive()
    {
    }
    public virtual void OnInputSkill(int idSkill)
    {
    }
    public virtual void OnHit(Action action)
    {
        if(componentManager.HasImmune(Immune.HIT)) 
            return;
        if (action != null)
        {
            action.Invoke();
        }
        ChangeState(NameState.HitState,true);
    }
    public virtual void OnKnockDown(Action action)
    {
        
        if(componentManager.HasImmune(Immune.KNOCK))
        {
            return;
        }
        ChangeState(NameState.KnockDownState, true);
        if (action != null)
        {
            action.Invoke();
        }
    }
    public virtual void InvokeAction(Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }
}

[System.Serializable]
public struct StateClone
{
    public NameState NameState;
    public State StateToClone;
}
public enum NameState
{
    UnknowState,
    SpawnState,
    IdleState,
    MoveState,
    JumpState,
    DashState,
    DieState,
    ReviveState,
    AttackState,
    AirAttackState,
    DashAttackState,
    SkillState,
    KnockDownState,
    HitState,
    GetUpState,
    FreezeState,
    StuntState,
    AirSkillState,
    FallingState,
    KnockUpState,
    RollOutState,

}