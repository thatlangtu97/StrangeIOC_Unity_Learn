using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    public Dictionary<NameState, State> dictionaryStateMachine = new Dictionary<NameState, State>();
    [Header("Current State")]
    public State currentState;
    public NameState currentNameState;
    public List<StateClone> States;
    public void SetupState()
    {
        foreach (StateClone tempState in States) {
            CreateStateFactory(tempState);
        }
    }
    public ComponentManager componentManager;
    public Animator animator;
    public virtual void Update()
    {
    }
    public virtual void InitStateMachine()
    {
        SetupState();
        if (dictionaryStateMachine.ContainsKey(NameState.SpawnState))
        {
            ChangeState(NameState.SpawnState, true);
        }
        else
        {
            ChangeState(NameState.IdleState, true);
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
        if ( !dictionaryStateMachine.ContainsKey(nameState) ) return;
        State newState = dictionaryStateMachine[nameState];
        if (!forceChange)
        {
            if (currentNameState != NameState.DieState && currentNameState != NameState.ReviveState)
            {
                if (nameState != currentNameState)
                {
                    if (currentState != null)
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
            if (currentState != null)
            {
                currentState.ExitState();
            }
            currentState = newState;
            currentNameState = nameState;
            currentState.EnterState();
        }
    }
    public virtual void OnInputAttack()
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
}

[System.Serializable]
public struct StateClone
{
    public NameState NameState;
    public State StateToClone;
}
public enum NameState
{
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
}