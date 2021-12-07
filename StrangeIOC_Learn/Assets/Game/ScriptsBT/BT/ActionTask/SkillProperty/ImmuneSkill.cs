using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class ImmuneSkill : BaseSkill
{
    public float duration;
    public Faction faction;
    public override TaskStatus OnUpdate()
    {
        if (!entity.hasImmune)
            entity.AddImmune(faction, duration);
        else
            entity.ReplaceImmune(faction, duration);

        return TaskStatus.Success;
    }
}
