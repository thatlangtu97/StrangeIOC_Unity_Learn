using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class SetupComponentManager : Action
{
    public SharedComponentManager componentManager;
    public override void OnAwake()
    {
        //Debug.Log(gameObject.GetComponent<ComponentManager>());
        //componentManager.Value = gameObject.GetComponent<ComponentManager>();
        //Debug.Log(componentManager);
        //componentManager = 
        base.OnAwake();
    }

}
