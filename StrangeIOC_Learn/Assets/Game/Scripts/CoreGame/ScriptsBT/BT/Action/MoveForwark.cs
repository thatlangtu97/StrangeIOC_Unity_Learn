using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class MoveForwark : Action
    {
        public SharedComponentManager componentManager;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.ChangeState(NameState.MoveState);
        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}
