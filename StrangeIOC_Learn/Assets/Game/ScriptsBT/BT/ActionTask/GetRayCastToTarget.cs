using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]

public class GetRayCastToTarget : Action
{
    public SharedComponentManager componentManager;
    public SharedFloat maxRange;
    public SharedVector2 target;
    public SharedVector2 result;
    public LayerMask mask;
    public Vector2 hitresult;

    public override void OnStart()
    {
        Vector2 dir = (target.Value - (Vector2)componentManager.Value.entity.centerPoint.centerPoint.position).normalized;
        //transform.right = dir;
        Debug.DrawRay((Vector2)componentManager.Value.entity.centerPoint.centerPoint.position, dir, Color.red, 5);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)componentManager.Value.entity.centerPoint.centerPoint.position, dir, maxRange.Value, mask);
        hitresult = hit.point;
        Debug.DrawLine((Vector2)componentManager.Value.entity.centerPoint.centerPoint.position, hit.point, Color.blue, 3);
        if(hit.collider != null)
        {
            result.Value = hit.point;
        }
        else
        {
            Ray2D r = new Ray2D();
            r.origin = (Vector2)componentManager.Value.entity.centerPoint.centerPoint.position;
            r.direction = dir;
            result.Value = r.GetPoint(maxRange.Value);
        }
        
    }
}
