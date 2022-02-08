using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class MoveToPoint : Action
    {
        public SharedComponentManager componentManager;
        public SharedVector2 pointToMove;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.vectorSpeed = ( (Vector3)pointToMove.Value - componentManager.Value.transform.position ).normalized;
        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}
