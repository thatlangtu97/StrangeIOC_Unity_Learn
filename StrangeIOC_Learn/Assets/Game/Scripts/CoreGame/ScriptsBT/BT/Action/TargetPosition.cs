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
        public bool plantGround;
        public LayerMask layerCast;
        public Vector3 target;
        //public SharedVector3 targetPosition;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.transform.position = componentManager.Value.stateMachine.componentManager.enemy.position;
            if (plantGround)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(componentManager.Value.stateMachine.transform.position, Vector2.down,100f, layerCast);
                if (raycastHit2D.collider != null)
                {
                    componentManager.Value.stateMachine.transform.position = raycastHit2D.point;
                    target = raycastHit2D.point;
                }
            }
        }
    }
}
