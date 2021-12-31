using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Extension")]
public class InputSkillTask : Action
{
    public int skillId;
    public float baseTimeCooldown;
    public float timeCountCooldown;
    public SharedComponentManager componentManager;
    public override void OnStart()
    {
        base.OnStart();
        
        
    }
    public override TaskStatus OnUpdate()
    {
        if (timeCountCooldown < 0f)
        {
            componentManager.Value.stateMachine.OnInputSkill(skillId);
            timeCountCooldown = baseTimeCooldown;
            return TaskStatus.Success;
        }
        else
        {
            timeCountCooldown -= Time.deltaTime;
            return TaskStatus.Failure;
        }
    }
}
