using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleportProjectileTask : Action
{
	//public AttackInfo info;
	public SharedComponentManager component;
	public SharedGameObject prefab;
	public int numberOfProjectile;
	public List<int> direction;

	public int range;
	public float timeBetweenAttack;

	public LayerMask enemy;
	public float angle;
	float angleToRotate;

	Collider2D hit; //half range
	Collider2D hit2;//full range
	GameObject projectile;
	Transform player;
	int randomDirection;
	Vector3 eulerAngle;

	public Vector3 offset;
	public override void OnStart()
	{
		//IniDirection();	
		randomDirection = 0;
		player = component.Value.entity.stateMachineContainer.stateMachine.centerPoint;
		component.Value.entity.projectTileLauncher.type = SpawnType.SingleIndependently;
		component.Value.entity.worldLaunchPoint.worldLaunchPoint.position = component.Value.entity.centerPoint.centerPoint.position;
		StartCoroutine(Cast());
		//CleanUpBufferManager.instance.AddReactiveComponent(() => { component.Value.entity.AddSpawnProjectileCommand(component.Value.entity.worldLaunchPoint.worldLaunchPoint.position, component.Value.entity.worldLaunchPoint.worldLaunchPoint.position, 1, true, Vector2.zero, false, prefab.Value); }, () => { component.Value.entity.RemoveSpawnProjectileCommand(); });
		
	}

	IEnumerator Cast()
    {
		for(int i = 0; i< numberOfProjectile; i++)
        {
			hit = Physics2D.OverlapCircle(player.position + offset, range / 2, enemy);
			hit2 = Physics2D.OverlapCircle(player.position + offset, range , enemy);
			
			if (hit)
			{
				component.Value.entity.worldLaunchPoint.worldLaunchPoint.position = hit.transform.position;
			}
			else if (hit2)
			{
				component.Value.entity.worldLaunchPoint.worldLaunchPoint.position = hit2.transform.position;
			}
			else
			{
				component.Value.entity.worldLaunchPoint.worldLaunchPoint.position = player.transform.position;
			}
			RandomSpin();//spin launch Point
			CleanUpBufferManager.instance.AddReactiveComponent(() => { component.Value.entity.AddSpawnProjectileCommand(component.Value.entity.worldLaunchPoint.worldLaunchPoint.position, component.Value.entity.worldLaunchPoint.worldLaunchPoint.position, 1, true, Vector2.zero, false, prefab.Value); }, () => { component.Value.entity.RemoveSpawnProjectileCommand(); });
			yield return new WaitForSeconds(timeBetweenAttack);
		}	
	}

	void RandomSpin()
    {
		if (randomDirection == 0)
			randomDirection = 1;
		else
			randomDirection = 0;

		//randomDirection = Random.Range(0, direction.Count);
		
		angleToRotate = angle * direction[randomDirection];
		eulerAngle.z = angleToRotate;
		component.Value.entity.worldLaunchPoint.worldLaunchPoint.eulerAngles = eulerAngle;

		//direction.Remove(direction[randomDirection]);
		if (direction.Count == 0)
			ReFill();
	}

	void ReFill()
	{

		for (int i = 0; i < numberOfProjectile; i++)
		{
			if (i != 1 && i != 5)
				direction.Add(i + 1);
		}
	}
	
}