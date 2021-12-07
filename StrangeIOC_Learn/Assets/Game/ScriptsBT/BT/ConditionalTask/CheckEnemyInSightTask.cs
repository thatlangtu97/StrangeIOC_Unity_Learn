using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
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
        if (!componentManager.Value.entity.hasCheckEnemyInSigh)
            return TaskStatus.Failure;
        if (componentManager.Value.entity.checkEnemyInSigh.enemy!=null)
        {

            return TaskStatus.Success;
        }
        else
        {
   
            return TaskStatus.Failure;
        }

    }
}
