using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]
public class SetGravityTask : Action
{
    public SharedFloat gravitySet;
    public SharedComponentManager component;
    public override void OnStart()
    {
        Rigidbody2D rigid = component.Value.entity.rigidbodyContainer.rigidbody;
        rigid.gravityScale = gravitySet.Value;
        rigid.velocity = Vector2.zero;
    }
}
