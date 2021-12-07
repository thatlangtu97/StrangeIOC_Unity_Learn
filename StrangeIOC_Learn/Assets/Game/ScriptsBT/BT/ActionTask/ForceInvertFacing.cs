using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInvertFacing : Action
{
    CharacterDirection characterDirection;
    public float cooldown;
    private float lastTimeUsed = 0f;
    public override void OnStart()
    {
        if(characterDirection==null)
        {
            characterDirection = GetComponent<CharacterDirection>();
        }

        if (Time.timeSinceLevelLoad - lastTimeUsed > cooldown)
        {
            characterDirection.ForceChangeDirection();
            lastTimeUsed = Time.timeSinceLevelLoad;
        }
        
    }
}
