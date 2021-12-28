using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachineController : MonoBehaviour
{

    //[Header("State To Clone")]
    //public State spawnState;
    //public State idleState;
    //public State moveState;
    //public State jumpState;
    //public State dashState;
    //public State dieState;
    //public State reviveState;

    //public State attackState;
    //public State airAttackState;
    //public State dashAttack;
    //public State skillState;

    //public State knockDownState;
    //public State hitState;
    //public State getUpState;
    //public State freezeState;
    //public State stuntState;

    //[HideInInspector]
    public State spawnState, idleState, moveState, jumpState, dashState, dieState, reviveState, attackState, airAttackState, dashAttack, skillState, knockDownState, hitState, getUpState, freezeState, stuntState;
    [Header("Current State")]
    public State currentState;
    [Header("Previous State")]
    public State previousState;
    //[Header("State To Clone")]
    public List<StateClone> States;
    public enum NameState
    {
      SpawnState,
      IdleState,
      MoveState,
      JumpState,
      DashState,
      DieState,
      RreviveState,
      AttackState,
      AirAttackState,
      DashAttack,
      SkillState,
      KnockDownState,
      HitState,
      GetUpState,
      FreezeState,
      StuntState,
    }
    [System.Serializable]
    public struct StateClone {
        public NameState NameState;
        public State StateToClone;
    }
    public void SetupState()
    {
        foreach (StateClone tempState in States) {
            switch (tempState.NameState)
            {
                case NameState.SpawnState:
                    CreateStateFactory(ref spawnState, tempState.StateToClone);
                    break;
                case NameState.IdleState:
                    CreateStateFactory(ref idleState, tempState.StateToClone);
                    break;
                case NameState.MoveState:
                    CreateStateFactory(ref moveState, tempState.StateToClone);
                    break;
                case NameState.JumpState:
                    CreateStateFactory(ref jumpState, tempState.StateToClone);
                    break;
                case NameState.DashState:
                    CreateStateFactory(ref dashState, tempState.StateToClone);
                    break;
                case NameState.DieState:
                    CreateStateFactory(ref dieState, tempState.StateToClone);
                    break;
                case NameState.RreviveState:
                    CreateStateFactory(ref reviveState, tempState.StateToClone);
                    break;
                case NameState.AttackState:
                    CreateStateFactory(ref attackState, tempState.StateToClone);
                    break;
                case NameState.AirAttackState:
                    CreateStateFactory(ref airAttackState, tempState.StateToClone);
                    break;
                case NameState.DashAttack:
                    CreateStateFactory(ref dashAttack, tempState.StateToClone);
                    break;
                case NameState.SkillState:
                    CreateStateFactory(ref skillState, tempState.StateToClone);
                    break;
                case NameState.KnockDownState:
                    CreateStateFactory(ref knockDownState, tempState.StateToClone);
                    break;
                case NameState.HitState:
                    CreateStateFactory(ref hitState, tempState.StateToClone);
                    break;
                case NameState.GetUpState:
                    CreateStateFactory(ref getUpState, tempState.StateToClone);
                    break;
                case NameState.FreezeState:
                    CreateStateFactory(ref freezeState, tempState.StateToClone);
                    break;
                case NameState.StuntState:
                    CreateStateFactory(ref stuntState, tempState.StateToClone);
                    break;
            }
        
        }
    }

    public ComponentManager componentManager;
    public Animator animator;

    public void Awake()
    {
        //InitStateMachine();
    }
    public void Start()
    {
        //ChangeState(idleState);
    }
    public virtual void Update()
    {
        //UpdateState();
    }
    public virtual void InitStateMachine()
    {
        SetupState();
        //InitStates();
        currentState = idleState;
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

    protected virtual void InitStates()
    {
        //CreateStateFactory(ref spawnState); 
        CreateStateFactory(ref idleState);
        CreateStateFactory(ref dashState);
        CreateStateFactory(ref dashAttack);
        CreateStateFactory(ref moveState);
        CreateStateFactory(ref jumpState);
        //CreateStateFactory(ref jumpFallState);
        CreateStateFactory(ref attackState);
        CreateStateFactory(ref airAttackState);
        CreateStateFactory(ref hitState);
        CreateStateFactory(ref dieState);
        CreateStateFactory(ref knockDownState);
        CreateStateFactory(ref freezeState);
        CreateStateFactory(ref skillState);
        CreateStateFactory(ref stuntState);
        CreateStateFactory(ref reviveState);
    }
    protected void CreateStateFactory(ref State stateToClone)
    {
        if (stateToClone != null)
        {
            stateToClone = Instantiate(stateToClone);
            stateToClone.InitState(this);
        }
    }
    protected void CreateStateFactory(ref State stateToClone , State stateClone)
    {
        stateToClone = Instantiate(stateClone);
        stateToClone.InitState(this);
    }
    public virtual void ChangeState(State newState, bool forceChange = false)
    {
        if (newState == null) return;
        if (previousState != currentState)
        {
            previousState = currentState;
        }
        if (!forceChange)
        {
            if (currentState != dieState && currentState != reviveState)
            {
                if (newState != currentState)
                {
                    if (currentState != null)
                    {
                        currentState.ExitState();
                    }
                    currentState = newState;
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

