using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
[TaskCategory("MyGame")]

public class SetupAngleTrack : Action
{
    public SharedFloat xDistance;
    public SharedFloat angle;
    public float height;

    public override void OnStart()
    {
        xDistance.Value = height / Mathf.Tan(angle.Value);
        base.OnStart();
    }
}
