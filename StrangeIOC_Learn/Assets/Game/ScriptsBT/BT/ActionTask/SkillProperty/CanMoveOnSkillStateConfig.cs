using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CanMoveOnSkillStateConfig : BaseSkill
{
    
    public override TaskStatus OnUpdate()
    {
        entity.isCanMoveOnSkillState = true;
        return TaskStatus.Success;
    }
}
