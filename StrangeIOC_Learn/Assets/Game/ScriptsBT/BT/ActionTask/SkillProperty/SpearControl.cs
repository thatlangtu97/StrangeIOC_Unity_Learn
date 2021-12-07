using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpearControl : Action
{
	public SharedGameObject spear;
	ProjectileDamageColliderCallBack damageCollider;
	public SharedVector2 blinkPoint;
	public override void OnStart()
	{
		damageCollider = spear.Value.GetComponent<ProjectileDamageColliderCallBack>();
	}

	public override TaskStatus OnUpdate()
	{
		if (damageCollider.isHit)
        {
			blinkPoint.Value = damageCollider.gameObject.transform.position;
			return TaskStatus.Success;
		}
		else
			return TaskStatus.Running;
	}
}