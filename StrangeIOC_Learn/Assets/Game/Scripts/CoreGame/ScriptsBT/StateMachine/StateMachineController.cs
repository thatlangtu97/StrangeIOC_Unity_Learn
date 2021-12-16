using System;
using UnityEngine;

[Serializable]
public class StateMachineController : MonoBehaviour
{
    [Header("State To Clone")]
    public State spawnState;
    public State idleState;
    public State moveState;
    public State jumpState;
    public State attackState;
    public State skillState;
    public State jumpFallState;
    public State doubleJumpState;
    public State knockDownState;
    public State airAttackState;
    public State dashState;
    public State dieState;
    public State dashAttack;
    public State beHitState;   
    public State getUpState; 
    public State freezeState;
    public State stuntState;
    public State reviveState;
    [Header("Current State")]
    public State currentState;
    [Header("Previous State")]
    public State previousState;

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
        InitStates();
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
        CreateStateFactory(ref spawnState); 
        CreateStateFactory(ref idleState);
        CreateStateFactory(ref dashState);
        CreateStateFactory(ref dashAttack);
        CreateStateFactory(ref moveState);
        CreateStateFactory(ref jumpState);
        CreateStateFactory(ref jumpFallState);
        CreateStateFactory(ref attackState);
        CreateStateFactory(ref airAttackState);
        CreateStateFactory(ref beHitState);
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
}

