using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckEnemyInFoward : Conditional
{
    public SharedComponentManager componentManager;
    public override TaskStatus OnUpdate()
    {
        if (componentManager.Value.transform.localScale.x > 0 && (componentManager.Value.enemy.position.x > componentManager.Value.transform.position.x) ||
            componentManager.Value.transform.localScale.x < 0 && (componentManager.Value.enemy.position.x < componentManager.Value.transform.position.x)
            )
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;

    }
}
