using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckSkillCooldownTask : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public SharedSkillCheck selectedSkill;
    public override TaskStatus OnUpdate()
    {
        float cooldownAfterReduce = selectedSkill.Value.cooldown;
        if(selectedSkill.Value.canNotReduce==false)
        {
            cooldownAfterReduce = selectedSkill.Value.cooldown * (1f - componentManager.Value.entity.skillContainer.cooldownReduction);
        }
        if (Time.timeSinceLevelLoad- selectedSkill.Value.lastTimeUsed  >= Mathf.Clamp(  cooldownAfterReduce , 0.1f, Mathf.Infinity) ||selectedSkill.Value.lastTimeUsed==0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;

    }
}
