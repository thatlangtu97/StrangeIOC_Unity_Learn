using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyGame")]
[TaskDescription("Returns Success if skill is ready")]
public class CheckCoolDownAndInRange : Conditional
{
	public SharedComponentManager componentManager;
	public SharedFloat rangeToEnemy;
	public SharedSkillCheck selectedSkill;

	public override TaskStatus OnUpdate()
	{
		if (selectedSkill.Value.range >= Mathf.Abs(rangeToEnemy.Value) && selectedSkill.Value.minRange <= Mathf.Abs(rangeToEnemy.Value)) // in range
		{
			float cooldownAfterReduce = selectedSkill.Value.cooldown;
			if (selectedSkill.Value.canNotReduce == false)
			{
				cooldownAfterReduce = selectedSkill.Value.cooldown * (1f - componentManager.Value.entity.skillContainer.cooldownReduction);
			}
			if (Time.timeSinceLevelLoad - selectedSkill.Value.lastTimeUsed >= Mathf.Clamp(cooldownAfterReduce, 0.1f, Mathf.Infinity) || selectedSkill.Value.lastTimeUsed == 0)
			{
				return TaskStatus.Success;
			}
			else
				return TaskStatus.Failure;
		}
		else
        {
			return TaskStatus.Failure;
		}
		
	}
}