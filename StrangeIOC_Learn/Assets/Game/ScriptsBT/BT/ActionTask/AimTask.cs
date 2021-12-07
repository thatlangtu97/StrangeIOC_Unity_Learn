using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTask : Action
{
    public SharedComponentManager component;
    CharacterDirection characterDirection;
    public Vector3 offSet;
    public override void OnStart()
    {
        var newOffSet = offSet;
        characterDirection = GetComponent<CharacterDirection>();
        if (component.Value.entity.checkEnemyInSigh.enemy != null)
        {
            if(!characterDirection.isFaceRight)
            {
                newOffSet *= -1;
            }
            component.Value.entity.projectTileLauncher.target = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position+newOffSet;
        }
    }
}
