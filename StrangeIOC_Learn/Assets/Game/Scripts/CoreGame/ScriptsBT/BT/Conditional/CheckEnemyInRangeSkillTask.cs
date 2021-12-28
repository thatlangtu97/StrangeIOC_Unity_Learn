using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]

public class CheckEnemyInRangeSkillTask : Conditional
{
    public SharedFloat rangeToEnemy;
    public SharedComponentManager componentManager;
    public float range;
    public override TaskStatus OnUpdate()
    {
        if(rangeToEnemy.Value <range )
        {
            //if (componentManager.Value.isAttack)
            //{
            //    componentManager.Value.NextSkill(componentManager.Value.nextIdSkill);
            //}
            return TaskStatus.Success;
        }
        else
        {

            return TaskStatus.Failure;
        }
        


    }
}
