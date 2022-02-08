
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class RangeWorld : Action
    {
        public SharedComponentManager componentManager;
        public SharedFloat rangeToEnemy;
        public override void OnStart()
        {
            base.OnStart();
            float space = Vector3.Distance( componentManager.Value.enemy.position, componentManager.Value.transform.position);
            rangeToEnemy.Value = space;
        }
    }
}