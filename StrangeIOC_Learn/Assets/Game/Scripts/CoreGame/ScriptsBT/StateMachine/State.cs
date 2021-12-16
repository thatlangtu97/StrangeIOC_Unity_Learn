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
    public virtual void EnterState()
    {
        /*
        foreach (AnimatorControllerParameter p in controller.animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
            {
                controller.animator.ResetTrigger(p.name);
            }
        }
        */
    }
    public virtual void UpdateState()
    {
    }
    public virtual void ExitState()
    {
    }
    public virtual void OnInputMove()
    {
    }
    public virtual void OnInputJump()
    {
    }
    public virtual void OnInputAttack()
    {
    }
    public virtual void OnInputDash()
    {
    }
    public virtual void OnInputSkill()
    {
    }
    public virtual void OnHit()
    {
    }
    public virtual void OnRevive()
    {
    }
    /*
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

            return TaskStatus.Failure;
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
    */
}


