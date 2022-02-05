using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class CheckInRangeHorizontal : Conditional
    {
        public SharedFloat rangeToEnemy;
        public SharedComponentManager componentManager;
        public float distance;
        public float spaceY;
        public override TaskStatus OnUpdate()
        {
            if (rangeToEnemy.Value < distance && (componentManager.Value.transform.position.y + spaceY) > componentManager.Value.enemy.position.y)
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
