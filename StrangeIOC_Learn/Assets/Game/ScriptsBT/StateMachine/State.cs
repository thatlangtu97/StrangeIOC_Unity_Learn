using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    protected StateMachineController controller;
    protected TimeCounter timeCounter;
    public virtual void InitState(StateMachineController controller)
    {
        this.controller = controller;
        timeCounter = new TimeCounter();
    }

    public virtual void UpdateState(float deltaTime)
    {
      
    }

    public virtual void EnterState()
    {
        if (controller.characterDirection != null)
            controller.characterDirection.lockDirection = false;
        if (controller.componentManager.entity.hasDash)
        {
            controller.componentManager.entity.RemoveDash();
        }
        foreach (AnimatorControllerParameter p in controller.animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
            {
                controller.animator.ResetTrigger(p.name);
                //Debug.LogError(p.name);
            }
                
        }
            
        
        //  controller.animator.ResetTrigger(AnimationName.IDLE);
    }
    IEnumerator DelayResetTrigger()
    {
       
        controller.animator.enabled = false;
        yield return new WaitForSeconds(0.05f);
        controller.animator.enabled = true;
    }
    public virtual void ExitState()
    {
        
    }

    public virtual TaskStatus OnInputMoveLeft()
    {
        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputMoveRight()
    {
        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputJump()
    {
        controller.buffer.jumpPressTime = Time.time;
        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputAttack()
    {
        if(!controller.buffer.isBuffer)
            return TaskStatus.Failure;

        if (controller.curCombo != 0)
        {
            if (controller.buffer.TimeExpire(controller.curCombo - 1))
            {
                //Debug.Log("TimeExpire: " + controller.curCombo);
                controller.buffer.timerExpired = true;
                controller.curCombo = 0;
                controller.buffer.curStoredInput = 0;

            }
        }
       

        if (controller.buffer.timerExpired)
        {
            //controller.buffer.SetTimerOnFirstAttackPress();
            controller.buffer.timerExpired = false;
            //Debug.Log("SetTimeer");
        }

       
        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputDash()
    {
        //Debug.Log("StateDash: " + controller.dashDuration);

        if (controller.componentManager.entity.health.HP.Value > 0)
            controller.ChangeState(controller.dashState);
        return TaskStatus.Success;
    }

    public virtual TaskStatus OnInputStopMove()
    {
        return TaskStatus.Failure;
    }
    public virtual TaskStatus OnHit(bool priority)
    {
        return TaskStatus.Failure;
    }
    public virtual TaskStatus OnInputChangeFacing()
    {
     
        controller.characterDirection.ChangeDirection();
        return TaskStatus.Success;
       
    }

    public virtual TaskStatus OnStunt()
    {
        controller.ChangeState(controller.stuntState);
        return TaskStatus.Success;
    }

    public virtual TaskStatus OnKnockDown()
    {
        controller.ChangeState(controller.knockDownState);

        return TaskStatus.Success;
    }
    public virtual TaskStatus OnFrezee(float duration)
    {
        if(controller.currentState != controller.freezeState)
        {
            controller.previousState = controller.currentState;
            controller.ChangeState(controller.freezeState);
            FreezeState freezeState = (FreezeState)controller.freezeState;
            freezeState.OnFrezee(duration);
        }
        return TaskStatus.Success;
    }
    public virtual TaskStatus OnGetUp()
    {
        return TaskStatus.Failure;
    }
    public virtual TaskStatus OnInputIdle()
    {
        controller.ChangeState(controller.idleState);
        return TaskStatus.Success;
    }
    public virtual TaskStatus OnInputMove()
    {
        controller.ChangeState(controller.moveState);
        return TaskStatus.Success;
    }
    public virtual TaskStatus OnInputSkill(int skillId)
    {
        //  controller.componentManager.entity.skillContainer.BehaviorTrees[skillId].ResetValuesOnRestart=true;
        
        controller.ChangeState(controller.skillState);
        return TaskStatus.Success;
    }
    public void OnPlayerSkill(int skillID)
    {
        
        controller.componentManager.entity.skillContainer.allSkill[skillID].CastSkill(controller.componentManager.entity);
        controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].DisableBehavior();
        controller.componentManager.entity.skillContainer.BehaviorTrees[skillID].EnableBehavior();
        if (skillID == 0)
            return;
        if(!controller.componentManager.entity.skillContainer.allSkill[skillID].canMoveOnSkill)
        {
            controller.ChangeState(controller.skillState);
            ((PlayerSkillState)controller.skillState).curSkillID = skillID;
        }
       
        //controller.componentManager.entity.skillContainer.allSkill[skillID].currCooldown = controller.componentManager.entity.skillContainer.allSkill[skillID].cooldown;
    }
    public virtual void OnRevive()
    {

    }

    public virtual void OnDragControl()
    {

    }

    public virtual void OnForceExitState()
    {

    }

    public virtual void OnDropDown()
    {
        
    }

    public virtual  void ForceExitState()
    {
        controller.ChangeState(controller.idleState);    
    }
}

public class TimeCounter
{
    public float elapseTime;
    public bool stop;

    public TimeCounter()
    {
        stop = true;
    }

    public void StartCount()
    {
        stop = false;
        elapseTime = 0;
    }

    public void UpdateTimeCount(float deltaTime)
    {
        if(!stop)
        {
            elapseTime += deltaTime;
        }    
    }

    public void ResetCounter()
    {
        elapseTime = 0;
        stop = true;
    }
}

