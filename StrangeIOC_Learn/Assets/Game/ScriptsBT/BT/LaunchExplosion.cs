using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LaunchExplosion : Action
{
	public SharedComponentManager componentManager;
	public SharedTransform castPoint;
	public GameObject prefab;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		CleanUpBufferManager.instance.AddReactiveComponent(() => { componentManager.Value.entity.AddSpawnProjectileCommand(castPoint.Value.position , castPoint.Value.position, 1, true, Vector2.zero, false, prefab); }, () => { componentManager.Value.entity.RemoveSpawnProjectileCommand(); });
		return TaskStatus.Success;
	}
}