using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckInputRotateTask : Action
{
    public SharedComponentManager componentManager;

    public override TaskStatus OnUpdate()
    {
        if (!componentManager.Value.isAttack)
        {
            if (componentManager.Value.speedMove == 1f)
            {
                componentManager.Value.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (componentManager.Value.speedMove == -1f)
            {
                componentManager.Value.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
        //else
        //{
        //    componentManager.Value.stateMachine.ChangeState(componentManager.Value.stateMachine.idleState);
        //}



    }
}
