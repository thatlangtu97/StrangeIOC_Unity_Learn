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
        public SharedVector2 pointToMove;
//        public Vector3 startRangeDistance,endRangeDistance;
        public Vector2 rangeDistance;
        public float deltaRangeX, deltaRangeY;
        bool hasFind = false;
        int count;
        public bool forceFind;
        public override void OnStart()
        {           
            base.OnStart();
            if (!forceFind)
            {
                if (!hasFind)
                {
                    count = Random.Range(1, 10);
                    FindEndPosition();
                    hasFind = true;
                }
                else
                {
                    if (Vector2.Distance(pointToMove.Value, componentManager.Value.transform.position) < 0.1f)
                    {
                        componentManager.Value.vectorSpeed =
                            (componentManager.Value.enemy.position - componentManager.Value.transform.position)
                            .normalized;
                        hasFind = false;
                    }
                }
            }
            else
            {
                count = Random.Range(1, 10);
                FindEndPosition();
            }

        }
        public override void OnEnd()
        {
            base.OnEnd();
        }
        private Vector3 RandomDistance()
        {
            Vector3 temp = Vector3.zero;

            if (count % 2 == 0)
            {
                temp = new Vector3(
                    Random.Range(rangeDistance.x, rangeDistance.x + deltaRangeX),
                    Random.Range(rangeDistance.y, rangeDistance.y + deltaRangeY),
                    0
                );
                
            }
            else
            {
                temp = new Vector3(
                    Random.Range(-rangeDistance.x, -rangeDistance.x - deltaRangeX),
                    Random.Range(rangeDistance.y, rangeDistance.y + deltaRangeY),
                    0
                );
            }
            return temp;
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
