using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CalculateRangeToEnemyTask : Conditional
{
    public SharedFloat rangeToEnemy;
    public SharedComponentManager componentManager;
    public bool isUseDistance;
    float dir;
    public override TaskStatus OnUpdate()
    {
        if (componentManager.Value.entity.isEnabled)
        {
            if(componentManager.Value.entity.checkEnemyInSigh.enemy != null)
            {
                dir = componentManager.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position.x - componentManager.Value.entity.centerPoint.centerPoint.position.x;
                //if (dir < 0) dir = dir * -1;
                if (dir == 0) dir = 0.01f;
                if (isUseDistance)
                {
                    if (dir < 0) dir = dir * -1;
                    rangeToEnemy.Value = dir / dir * Vector2.Distance(componentManager.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position, componentManager.Value.entity.centerPoint.centerPoint.position);
                }

                else
                {
                    rangeToEnemy.Value = dir;

                }
            }           
        }
   
        return TaskStatus.Success;


    }
}
