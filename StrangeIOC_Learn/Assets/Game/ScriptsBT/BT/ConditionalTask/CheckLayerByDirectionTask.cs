using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class CheckLayerByDirectionTask : Conditional
{
    public LayerMask layerToCheck;
    public float distance;
    public Vector2 direction;
    
    public override TaskStatus OnUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, direction, distance, layerToCheck);
        if(hit)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}
