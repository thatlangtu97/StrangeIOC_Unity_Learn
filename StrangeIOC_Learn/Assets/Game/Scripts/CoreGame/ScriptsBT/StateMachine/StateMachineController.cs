using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StateMachineController : MonoBehaviour
{
    [Header("State To Clone")]
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
    [Header("Current State")]
    public State currentState;
    [Header("Previous State")]
    public State previousState;

    public ComponentManager componentManager;
    public Animator animator;
    
    public void Awake()
    {
        InitStateMachine();
    }
    public void OnEnable()
    {
        ChangeState(idleState);
    }
    public void Start()
    {
       
    }
    public virtual void Update()
    {
        UpdateState();
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
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    ChangeState(freezeState);
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    ChangeState(beHitState);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    ChangeState(dieState);
        //}
    }
    public virtual void OnSpawn()
    {
    }

    public virtual void OnRevival()
    {
    }
    protected virtual void InitStates()
    {    
        CreateStateFactory(ref idleState);
        CreateStateFactory(ref dashState);
        CreateStateFactory(ref moveState);
        CreateStateFactory(ref jumpState);
        CreateStateFactory(ref jumpFallState);
        CreateStateFactory(ref attackState);
        CreateStateFactory(ref beHitState);
        CreateStateFactory(ref dieState);
        CreateStateFactory(ref knockDownState);
        CreateStateFactory(ref freezeState);
        CreateStateFactory(ref skillState);
        CreateStateFactory(ref stuntState);
    }
    protected void CreateStateFactory(ref State stateToClone)
    {
        if (stateToClone != null)
        {
            stateToClone = Instantiate(stateToClone);
            stateToClone.InitState(this);

        }

    }
    public virtual void ChangeState(State newState)
    {
        if (newState == null) return;
        if (previousState != currentState)
        {
            previousState = currentState;
        }
        if (currentState != dieState)
        {
            if (newState != currentState)
            {
                currentState.ExitState();
                currentState = newState;
                currentState.EnterState();
            }
        }
    }

    public virtual void OnInputAttack()
    {
    }

    //protected virtual void OnInputJump()
    //{
    //}

    //protected virtual void OnInputMoveLeft()
    //{
    //}

    //protected virtual void OnInputMoveRight()
    //{
    //}
    public virtual void OnInputMove()
    {
        ChangeState(moveState);
    }
    public virtual void OnInputStopMove()
    {
    }

    public virtual void OnInputSkill(int skillId)
    {
    }

    //protected virtual void OnInputDarts()
    //{
    //}

    public virtual void OnInputDash()
    {
    }

    //public virtual void OnInputTool()
    //{
    //}

    //public virtual void OnFinishCastingSkill(bool isSkill = false)
    //{
    //}

    //public virtual void UpdateState(float deltaTime)
    //{
    //}

    //public virtual void Attack()
    //{
    //}

    //public virtual void Reborn()
    //{
    //}

    //protected virtual void CheckForOpponentInTheSight()
    //{

    //}
    //public void GetUp()
    //{
    //    currentState.OnGetUp();
    //}
    
    //public virtual void KnockDown()
    //{
    //    if (currentState != null)
    //    {
    //        currentState.OnKnockDown();
    //    }


    //}
    //public virtual void Freeze(float duration)
    //{
    //    if(currentState != null)
    //    {
    //        currentState.OnFrezee(duration);
    //    }
    //}

    //public virtual void OnForceExitState()
    //{
    //    if(currentState!= null)
    //    currentState.OnForceExitState();
    //}


}

