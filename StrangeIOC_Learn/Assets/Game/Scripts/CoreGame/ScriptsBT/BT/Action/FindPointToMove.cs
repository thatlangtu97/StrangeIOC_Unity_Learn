using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class FindPointToMove : Action
    {
        public SharedComponentManager componentManager;
        public SharedVector3 pointToMove;
        public Vector3 startRangeDistance,endRangeDistance; 
        bool hasFind = false;
        public override void OnStart()
        {           
            base.OnStart();
            if (!hasFind)
            {
                FindEndPosition();
                hasFind = true;
            }
            else
            {
                if (pointToMove.Value == componentManager.Value.transform.position)
                {
                    componentManager.Value.vectorSpeed =
                        (componentManager.Value.enemy.position - componentManager.Value.transform.position).normalized;
                    hasFind = false;
                }
            }

        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
        private Vector3 RandomDistance()
        {
            return new Vector3(    
                Random.Range(startRangeDistance.x, endRangeDistance.x),
                Random.Range(startRangeDistance.y, endRangeDistance.y),
                Random.Range(startRangeDistance.z, endRangeDistance.z)
            );
        }
        private void FindEndPosition()
        {
            if (componentManager.Value.enemy)
            {
                pointToMove.Value = componentManager.Value.enemy.position + RandomDistance();
            }
            else
            {
                pointToMove.Value = componentManager.Value.transform.position + RandomDistance();
                
            }
        }
    }
}
