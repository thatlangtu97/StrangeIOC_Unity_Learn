using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class CheckInRangeToPoint : Conditional
    {        
        public SharedComponentManager componentManager;
        public SharedVector2 pointToMove;
        public float distance;
        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(pointToMove.Value, componentManager.Value.transform.position) < distance)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}