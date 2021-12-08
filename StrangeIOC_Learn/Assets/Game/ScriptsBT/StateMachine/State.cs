using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    protected StateMachineController controller;
    public virtual void InitState(StateMachineController controller)
    {
        this.controller = controller;
    }

    public virtual void UpdateState(float deltaTime)
    {
      
    }

    public virtual void EnterState()
    {
        foreach (AnimatorControllerParameter p in controller.animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
            {
                controller.animator.ResetTrigger(p.name);
            }
        }
    }
    IEnumerator DelayResetTrigger()
    {
        
        yield return new WaitForEndOfFrame();
        foreach (var param in controller.animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                controller.animator.ResetTrigger(param.name);
            }
        }
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
        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputAttack()
    {

        return TaskStatus.Failure;
    }

    public virtual TaskStatus OnInputDash()
    {
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
        controller.ChangeState(controller.skillState);
        return TaskStatus.Success;
    }
    public void OnPlayerSkill(int skillID)
    {
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


