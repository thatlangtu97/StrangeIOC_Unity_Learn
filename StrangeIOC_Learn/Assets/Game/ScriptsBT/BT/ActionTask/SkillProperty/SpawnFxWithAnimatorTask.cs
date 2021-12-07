using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFxWithAnimatorTask : Action
{
    public SharedAnimator storeAnimatorValue;
    public GameObject objToSpawn;
    public Vector2 posToSpawn;
    public SharedGameObject tf;
    public float duration;

    public override void OnStart()
    {
        GameObject objSpawn = ObjectPool.Spawn(objToSpawn);
        if(tf!=null)
        {
            objSpawn.transform.SetParent(tf.Value.transform);
        }
        objSpawn.transform.position = posToSpawn;
        if(duration>0f)
        {
            ObjectPool.Recycle(objSpawn, duration);
        }
        storeAnimatorValue.Value = objSpawn.GetComponentInChildren<Animator>();
    }
}
