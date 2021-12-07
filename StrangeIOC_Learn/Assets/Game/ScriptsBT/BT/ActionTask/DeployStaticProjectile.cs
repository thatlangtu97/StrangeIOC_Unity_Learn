using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployStaticProjectile : Action
{
	public SharedComponentManager component;
	public SharedGameEntityList entityProjectile;
	public GameObject warningFx;
	public float waitTime;
	public float timeBetweenAttack;
	public SharedTransformList launchPoint;
	public float launchSpeed;
	public float duration;
	public SharedBool lauch;
	public override void OnStart()
	{
		Laucnh();
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}

	void Laucnh()
	{
		StartCoroutine(Launch());
	}

	IEnumerator Launch()
	{
		yield return new WaitForSeconds(waitTime);
		for (int i = 0; i < entityProjectile.Value.Count; i++)
		{
			if (entityProjectile.Value[i].hasMoveByDestination)
				entityProjectile.Value[i].RemoveMoveByDestination();
			if (entityProjectile.Value[i].hasMoveToTarget)
				entityProjectile.Value[i].RemoveMoveToTarget();
			Vector3 direction = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position - launchPoint.Value[i].position;
			ObjectPool.Spawn(warningFx, launchPoint.Value[i].position);
			yield return new WaitForSeconds(timeBetweenAttack);
			entityProjectile.Value[i].AddMoveAndDestroy(true, launchSpeed, duration, duration, direction.normalized, entityProjectile.Value[i].centerPoint.centerPoint.gameObject, true);
		}
		entityProjectile.Value.Clear();
		lauch.Value = true;
	}
}