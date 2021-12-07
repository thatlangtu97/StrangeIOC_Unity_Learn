using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleGameObjectTask : Action
{
    public SharedGameObject recycle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override TaskStatus OnUpdate()
    {
        if(recycle.Value != null)
        {
            ObjectPool.Recycle(recycle.Value);
        }       
        return TaskStatus.Success;
    }
}
