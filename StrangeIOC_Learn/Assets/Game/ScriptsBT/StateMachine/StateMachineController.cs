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

    public Transform centerPoint;

    
    public BehaviorTree behaviorTree;
    public void Awake()
    {
        InitStateMachine();
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
            currentState.UpdateState(Time.deltaTime);
            
        }

    }
    public virtual void OnSpawn()
    {
    }

    public virtual void OnRevival()
    {
    }

    public virtual void DisableOnDead()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnHit(bool isBlock, bool isNotKnock)
    {
        if(isBlock)
        {
            animator.SetTrigger(AnimationName.BLOCK);
            return;
        }

        if(isNotKnock)
        {
            return;
        }

        if ((beHitState != null && currentState != null))
        {
            currentState.OnHit(isBlock);
        }else
        {
            animator.SetTrigger(AnimationName.HIT);
        }
    }

    protected virtual void EnterStateOnSpawn()
    {
        //  currentState.EnterState();
    }
    protected virtual void InitStates()
    {    
        CreateStateFactory(ref idleState);
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
       if(currentState!=dieState )
       {
            if(currentState != null)
                currentState.ExitState();

            if (newState == null)
                newState = idleState;
            if (newState != currentState)
            {
                currentState = newState;
                currentState.EnterState();
            }
       }
    }
    protected virtual void OnInputAttack()
    {
    }

    protected virtual void OnInputJump()
    {
    }

    protected virtual void OnInputMoveLeft()
    {
    }

    protected virtual void OnInputMoveRight()
    {
    }

    protected virtual void OnInputStopMove()
    {
    }

    protected virtual void OnInputSkill(int skillId)
    {
    }

    protected virtual void OnInputDarts()
    {
    }

    protected virtual void OnInputDash()
    {
    }

    public virtual void OnInputTool()
    {
    }

    public virtual void OnFinishCastingSkill(bool isSkill = false)
    {
    }

    public virtual void UpdateState(float deltaTime)
    {
    }

    public virtual void Attack()
    {
    }

    public virtual void Reborn()
    {
    }

    protected virtual void CheckForOpponentInTheSight()
    {

    }
    public void GetUp()
    {
        currentState.OnGetUp();
    }
    
    public virtual void KnockDown()
    {
        if (currentState != null)
        {
            currentState.OnKnockDown();
        }


    }
    public virtual void Freeze(float duration)
    {
        if(currentState != null)
        {
            currentState.OnFrezee(duration);
        }
    }

    public virtual void OnForceExitState()
    {
        if(currentState!= null)
        currentState.OnForceExitState();
    }


}

