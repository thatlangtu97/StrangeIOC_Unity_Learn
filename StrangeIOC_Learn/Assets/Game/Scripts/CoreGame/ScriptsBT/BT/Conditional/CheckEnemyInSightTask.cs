using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
[TaskDescription("Returns Success if enemy in range")]
public class CheckEnemyInSightTask : Conditional
{
    public SharedComponentManager componentManager;
    public override void OnAwake()
    {
        base.OnAwake();
    }
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {

        if (componentManager.Value.enemy != null)
        {
            return TaskStatus.Success;
        }
        else
        {
            componentManager.Value.enemy = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.stateMachine.transform;
            return TaskStatus.Failure;
        }

        //if (componentManager != null)
        //{
        //    if (componentManager.Value != null)
        //    {
        //        if (!componentManager.Value.hasCheckEnemyInSigh)
        //            return TaskStatus.Failure;
        //        if (componentManager.Value.enemy != null)
        //        {
        //            return TaskStatus.Success;
        //        }
        //        else
        //        {
        //            return TaskStatus.Failure;
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Value null");
        //        return TaskStatus.Failure;
        //    }
        //}
        //else
        //{
        //    Debug.Log("componentManager null");
        //    return TaskStatus.Failure;
        //}

    }
}
