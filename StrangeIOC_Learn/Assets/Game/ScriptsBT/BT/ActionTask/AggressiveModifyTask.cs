using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
//using Boo.Lang;

public class AggressiveModifyTask : Action
{
	public SharedComponentManager component;
	public SharedFloat minWait;
	public SharedFloat maxWait;
	public float increasedVale;
	public List<float> hpThreshHold;
	public override void OnStart()
	{
		for(int i = hpThreshHold.Count; i< 0; i--)
        {
			if ((float)component.Value.entity.health.HP.Value <= (float)component.Value.entity.health.MaxHP * hpThreshHold[0])
			{
				minWait.Value -= increasedVale * minWait.Value;
				maxWait.Value -= increasedVale * maxWait.Value;
			}
		}
		
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}