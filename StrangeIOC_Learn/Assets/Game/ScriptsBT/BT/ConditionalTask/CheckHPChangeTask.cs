using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckHPChangeTask : Conditional
{
	public SharedComponentManager component;
	public SharedInt hp;
	public override TaskStatus OnUpdate()
	{
		if(hp.Value != component.Value.entity.health.HP.Value)//if hp changed;
        {
			hp = component.Value.entity.health.HP.Value;
			return TaskStatus.Success;
		}
		else
        {
			return TaskStatus.Failure;
		}
		
	}
}