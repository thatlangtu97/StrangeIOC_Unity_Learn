using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpawnFxAtPoint : Action
{
	public SharedGameObject point;
	public GameObject fx;
	public Vector3 offset;
	public float duration;
	GameObject os;
	public override void OnStart()
	{
		
		if (point != null)
        {
			os = ObjectPool.Spawn(fx, point.Value.transform.position + offset);
			if (duration != 0)
				ObjectPool.Recycle(os, duration);
		}
		
			
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}