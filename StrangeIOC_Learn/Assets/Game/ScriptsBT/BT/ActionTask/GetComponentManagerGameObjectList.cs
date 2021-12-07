using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
[TaskCategory("MyGame")]

public class GetComponentManagerGameObjectList : Action
{
    public SharedGameObject parent;
    public SharedGameObjectList gameObjectList;
    public GameObject gameParent;

    public override void OnStart()
    {
        base.OnStart();
        //gameObjectList = new SharedGameObjectList();
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log(parent.Value.GetComponentsInChildren<ComponentManager>().Length);
        gameParent = parent.Value;
        foreach(ComponentManager component in gameParent.GetComponentsInChildren<ComponentManager>())
        {
            gameObjectList.Value.Add(component.gameObject);
        }
        return TaskStatus.Success;
    }
}
