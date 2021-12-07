using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateToSkillTask : Conditional
{
    public SharedComponentManager componentManager;
    public override TaskStatus OnUpdate()
    {
        if (!LevelMapConfigManager.instance.gamePlaying)
        {
            componentManager.Value.entity.isAttackComplete = false;
            componentManager.Value.entity.isSkillComplete = false;
            if (componentManager.Value.entity.stateMachineContainer.stateMachine.currentState != componentManager.Value.entity.stateMachineContainer.stateMachine.idleState)
            componentManager.Value.entity.stateMachineContainer.stateMachine.ChangeState(componentManager.Value.entity.stateMachineContainer.stateMachine.idleState);
            return TaskStatus.Failure;
        }  
        //Debug.Log(componentManager.Value.entity.stateMachineContainer.stateMachine.softBody.GetComponent<Detectable>().immuneToForce);
        //if (componentManager.Value.entity.stateMachineContainer.stateMachine.isFreezeAndAddforce)
        //    componentManager.Value.entity.stateMachineContainer.stateMachine.softBody.GetComponent<Detectable>().immuneToForce = false;
       
        //if (componentManager.Value.entity.stateMachineContainer.stateMachine.currentState == componentManager.Value.entity.stateMachineContainer.stateMachine.skillState)
        //{

        //    //componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.ForceExitState();
        //    return TaskStatus.Success; // prevent When time scale = 0 BT stops running at this node
        //}         
        return componentManager.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputSkill(0); // 0 for test
    }
}
