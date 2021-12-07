using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class SetCooldownForSkill : Action
{
    public SharedSkillCheck skillNeedToCooldown;
    public override TaskStatus OnUpdate()
    {
        skillNeedToCooldown.Value.lastTimeUsed = Time.timeSinceLevelLoad;
        return TaskStatus.Success;
    }
}
