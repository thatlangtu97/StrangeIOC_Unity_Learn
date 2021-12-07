using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchStaticProjectile : Action
{
	public SharedComponentManager component;
	public GameObject prefab;
	//public GameObject warningFx; 
	public bool followCaster;
	public float followSpeed;
	//public float launchSpeed;
	public SharedTransformList launchPoint;
	GameObject p;
	ComponentManager c;
	public bool launchAfterTime;
	//public float waitTime;
	//public float timeBetweenAttack;
	//public float duration;
	public SharedGameEntityList entityProjectile;
	public override void OnStart()
	{
		
		for(int i = 0; i< launchPoint.Value.Count; i++)
        {
			p = ObjectPool.Spawn(prefab, launchPoint.Value[i].position);
			c = p.GetComponent<ComponentManager>();
			if (followCaster)
				c.entity.AddMoveToTarget(followSpeed, true, p.transform, launchPoint.Value[i]);

			DamageCollider[] projectilecollider = p.GetComponentsInChildren<DamageCollider>();
			//col = spawn.GetComponent<DamageCollider>();
			foreach (var collider in projectilecollider)
			{
				collider.attackInfo = component.Value.entity.projectTileLauncher.attackInfo;
				collider.source = component.Value.entity;
			}
			entityProjectile.Value.Add(c.entity);
		}
	}

	public override TaskStatus OnUpdate()
	{
		//StartCoroutine(Launch());
		return TaskStatus.Success;
	}
	
//	void Laucnh()
//    {
//		StartCoroutine(Launch());
//	}

//	IEnumerator Launch()
//    {
//		yield return new WaitForSeconds(waitTime);
//		for (int i = 0; i < entityProjectile.Value.Count; i++)
//		{
//			if (entityProjectile.Value[i].hasMoveByDestination)
//				entityProjectile.Value[i].RemoveMoveByDestination();
//			if (entityProjectile.Value[i].hasMoveToTarget)
//				entityProjectile.Value[i].RemoveMoveToTarget();
//			Vector3 direction = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position - launchPoint.Value[i].position;
//			ObjectPool.Spawn(warningFx, launchPoint.Value[i].position);
//			yield return new WaitForSeconds(timeBetweenAttack);
//			entityProjectile.Value[i].AddMoveAndDestroy(true, launchSpeed, duration, duration, direction.normalized, entityProjectile.Value[i].centerPoint.centerPoint.gameObject, true);
//		}
//		entityProjectile.Value.Clear();
//	}
}