using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CanJumpOnSkillStateConfig : BaseSkill
{
    public override TaskStatus OnUpdate()
    {
        
       // base.CastSkill(entity);
        entity.isCanInputJumpOnSkillState = true;
        return TaskStatus.Success;
    }
}
