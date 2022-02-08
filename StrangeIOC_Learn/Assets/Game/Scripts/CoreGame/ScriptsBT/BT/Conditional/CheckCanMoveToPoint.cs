using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckCanMoveToPoint : Conditional
{
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    public SharedVector2 pointToMove;
    public float timeTarget;
    [SerializeField]
    private float timeCountTarget;

    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(pointToMove.Value, componentManager.Value.transform.position) < 0.1f)
        {
            if (componentManager.Value.transform.localScale.x < 0)
            {
                componentManager.Value.speedMove = -componentManager.Value.maxSpeedMove;
            }
            else if (componentManager.Value.transform.localScale.x > 0)
            {
                componentManager.Value.speedMove = componentManager.Value.maxSpeedMove;
            }
            

        }
//        
//        
//        
//        if (rangeToEnemy.Value > distanceBreak)
//        {
//            if (timeCountTarget <= 0)
//            {
//                componentManager.Value.speedMove = 0;
//
//                return TaskStatus.Failure;
//            }
//            else
//            {
//                timeCountTarget -= Time.deltaTime;
//            }
//        }
//        else
//        {
//            timeCountTarget = timeTarget;
//        }
//        if (rangeToEnemy.Value < distanceStop)
//        {
//            componentManager.Value.speedMove = 0;
//            return TaskStatus.Failure;
//        }
//        if (componentManager.Value.transform.localScale.x < 0)
//        {
//            componentManager.Value.speedMove = -componentManager.Value.maxSpeedMove;
//        }
//        else if (componentManager.Value.transform.localScale.x > 0)
//        {
//            componentManager.Value.speedMove = componentManager.Value.maxSpeedMove;
//        }
        return TaskStatus.Success;


    }
}