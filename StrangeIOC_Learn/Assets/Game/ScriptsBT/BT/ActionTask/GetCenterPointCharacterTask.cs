using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MyGame")]


public class GetCenterPointCharacterTask : Action
{
    public SharedVector2 firePoint;
         public SharedComponentManager componentManager;
         public override void OnStart()
         {
     
     
         }
         public override TaskStatus OnUpdate()
         {
             firePoint.Value = componentManager.Value.entity.centerPoint.centerPoint.position;
             return TaskStatus.Success;
         }
}
