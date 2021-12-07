using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckPointRunEnemy : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    CharacterDirection characterDirection;
    public float spaceCheck;
    public override void OnStart()
    {
        base.OnStart();
        if (characterDirection == null)
            characterDirection = componentManager.Value.GetComponent<CharacterDirection>();
    }
    public override TaskStatus OnUpdate()
    {
        if(Mathf.Abs( rangeToEnemy.Value) < spaceCheck)
        {
            if (!characterDirection.isFaceRight) // neu k dang quay mat ben phai thi change direction
            {
                return TaskStatus.Success;
            }
            else
            {

                return TaskStatus.Failure;
            }
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

