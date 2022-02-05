using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class RangeHorizontal : Action
    {
        public SharedComponentManager componentManager;
        public SharedFloat rangeToEnemy;
        public override void OnStart()
        {
            base.OnStart();
            float space = componentManager.Value.enemy.position.x - componentManager.Value.transform.position.x;
            rangeToEnemy.Value = Mathf.Abs(space);
        }
    }
}
