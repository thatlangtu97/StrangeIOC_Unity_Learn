using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]

public class CheckPointInForwardTask : Conditional
{
    // Start is called before the first frame update
    public SharedComponentManager componentManager;
    public SharedVector2 target;
    CharacterDirection characterDirection;
    public override void OnStart()
    {
        base.OnStart();
        if (characterDirection == null)
            characterDirection = componentManager.Value.GetComponent<CharacterDirection>();
    }
    public override TaskStatus OnUpdate()
    {
        if (componentManager.Value.entity.centerPoint.centerPoint.position.x < target.Value.x)
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
            if (characterDirection.isFaceRight)

            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
        //else
        //{
        //    return TaskStatus.Failure;
        //}

    }
}