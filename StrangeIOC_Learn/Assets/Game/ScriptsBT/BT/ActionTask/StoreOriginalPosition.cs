using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOriginalPosition : Action
{
    // Start is called before the first frame update
    public SharedVector2 storedValue;
    public override void OnStart()
    {
        //  storedValue.SetValue(transform.position);
        storedValue.Value = transform.position;
    }

}
