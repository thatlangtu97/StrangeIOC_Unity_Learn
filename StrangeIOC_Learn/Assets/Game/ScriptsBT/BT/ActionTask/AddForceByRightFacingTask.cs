using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceByRightFacingTask : Action
{
    public SharedComponentManager component;
    public Vector2 force;

    public override void OnStart()
    {
        component.Value.entity.rigidbodyContainer.rigidbody.AddForce(new Vector2(transform.right.x * force.x, force.y));
    }
}
