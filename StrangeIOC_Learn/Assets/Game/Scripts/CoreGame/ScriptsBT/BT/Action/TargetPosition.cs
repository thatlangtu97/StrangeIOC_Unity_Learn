using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class TargetPosition : Action
    {
        public SharedComponentManager componentManager;
        //public SharedVector3 targetPosition;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.transform.position = componentManager.Value.stateMachine.componentManager.enemy.position;
        }
    }
}
