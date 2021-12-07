using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChannelingSkillConfig :BaseSkill
{
    public float channelTime;

    public override TaskStatus OnUpdate()
    {
        channelTime -= Time.deltaTime;
        if(channelTime>0f)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
