using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
[TaskDescription("Returns Success if enemy in range")]
public class CheckEnemyWithinSkillRangeTask : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public SharedSkillCheck selectedSkill;
    public override TaskStatus OnUpdate()
    {
        //Debug.Log(selectedSkill);
        if (selectedSkill.Value.range >= Mathf.Abs(rangeToEnemy.Value) && selectedSkill.Value.minRange<= Mathf.Abs(rangeToEnemy.Value))
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;

    }
}
