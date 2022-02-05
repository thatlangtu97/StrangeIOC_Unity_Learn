using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class ChangeSkillState : Action
    {
        public SharedComponentManager componentManager;
        public int skillId;
        //public float baseTimeCooldown;
        //public float timeCountCooldown;
        public override void OnStart()
        {
            componentManager.Value.stateMachine.OnInputSkill(skillId);
            base.OnStart();
        }
        //public override TaskStatus OnUpdate()
        //{
        //    if (timeCountCooldown < 0f)
        //    {
        //        componentManager.Value.stateMachine.OnInputSkill(skillId);
        //        timeCountCooldown = baseTimeCooldown;
        //        return TaskStatus.Success;
        //    }
        //    else
        //    {
        //        timeCountCooldown -= Time.deltaTime;
        //        return TaskStatus.Failure;
        //    }
        //}
    }
}