﻿using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class State : SerializedScriptableObject
{
    protected StateMachineController controller;
    public int idState;
    protected Dictionary<int,IComboEvent> idEventTrigged = new Dictionary<int, IComboEvent>();
    public float timeTrigger;
    public List<AttackConfig> eventData;
    public List<EventCollection> eventCollectionData;

    public virtual void InitState(StateMachineController controller)
    {
        this.controller = controller;
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
        timeTrigger = 0f;
        idEventTrigged = new Dictionary<int, IComboEvent>();
    }
    public virtual void ResetTrigger()
    {
        foreach (AnimatorControllerParameter p in controller.animator.parameters)
        {
            if (p.type == AnimatorControllerParameterType.Trigger)
            {
                controller.animator.ResetTrigger(p.name);
            }
        }
    }
    public virtual void ResetEvent()
    {
        idEventTrigged.Clear();
        timeTrigger = 0f;
    }
    public virtual void RecycleEvent()
    {
        foreach(IComboEvent temp in idEventTrigged.Values)
        {
            temp.Recycle();
        }
    }
    public virtual void UpdateState()
    {
        timeTrigger +=Time.deltaTime;
        if (eventCollectionData != null && eventCollectionData.Count > idState && idState >= 0)
        {
            if (eventCollectionData[idState].EventCombo != null)
            {
                foreach (IComboEvent tempComboEvent in eventCollectionData[idState].EventCombo)
                {
                    if (timeTrigger > tempComboEvent.timeTrigger && !idEventTrigged.ContainsKey(tempComboEvent.id))
                    {
                        tempComboEvent.OnEventTrigger(controller.componentManager.entity);
                        idEventTrigged.Add(tempComboEvent.id, tempComboEvent);
                    }
                }
            }
        }
    }
    public virtual void ExitState()
    {
        RecycleEvent();
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
    public virtual void OnInputSkill(int idSkill)
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


