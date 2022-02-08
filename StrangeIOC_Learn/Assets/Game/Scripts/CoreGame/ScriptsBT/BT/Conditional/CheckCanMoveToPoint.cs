using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckCanMoveToPoint : Conditional
{
    public SharedFloat rangeToEnemy;
    public float distanceBreak;

    public override TaskStatus OnUpdate()
    {
        if (rangeToEnemy.Value < distanceBreak)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
        
        
    }
//        if (Vector2.Distance(pointToMove.Value, componentManager.Value.transform.position) > 0.1f)
//        {
//            if (componentManager.Value.transform.localScale.x < 0)
//            {
//                componentManager.Value.speedMove = -componentManager.Value.maxSpeedMove;
//            }
//            else if (componentManager.Value.transform.localScale.x > 0)
//            {
//                componentManager.Value.speedMove = componentManager.Value.maxSpeedMove;
//            }
//            return TaskStatus.Success;
//        }
//        else
//        {
//            componentManager.Value.speedMove = 0f;
//            return TaskStatus.Failure;
//        }
//        


    

}