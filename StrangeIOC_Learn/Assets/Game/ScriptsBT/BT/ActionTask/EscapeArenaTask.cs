using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EscapeArenaTask : Action
{
	public SharedComponentManager component;
	public SharedGameObject destroyGameObject;
	public SharedFloat delayEscapeTime;
	public GameObject escapeFx;
	float startTime;
	float rangeToEnemy;
	CharacterDirection characterDirection;
	public override void OnStart()
	{
		startTime = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		if (component.Value.entity.centerPoint.centerPoint.position.x > LevelCreator.instance.map.rightAnchor.position.x +1||
			component.Value.entity.centerPoint.centerPoint.position.x < LevelCreator.instance.map.leftAnchor.position.x -1)
        {
			if (Time.time - startTime < delayEscapeTime.Value)
			{
				
				component.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputIdle();
			}
			else
			{
				
				EffectRequestManager.RequestEffect(escapeFx, null, component.Value.entity.centerPoint.centerPoint.position, 1);
				Escape();
				return TaskStatus.Success;
			}
		}
		else
        {
			if(component.Value.entity.hasMoveByDirection)
			component.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputMove();
		}

		//if (Time.time - startTime < delayEscapeTime.Value)
  //      {
		//	component.Value.entity.stateMachineContainer.stateMachine.currentState.OnInputMove();
		//}
		//else
  //      {
		//	EffectRequestManager.RequestEffect(escapeFx, null, component.Value.entity.centerPoint.centerPoint.position, 1);
		//	Escape();
		//	return TaskStatus.Success;
		//}
		return TaskStatus.Running;
	}

	void Escape()
    {
		if (GameModeManager.instance.mode != GameMode.BossRaid)
			LevelCreator.instance.OnEnemyDie();
		component.Value.entity.Destroy();

		if (CaculateRegeToEnemy.instance != null)
        {
			Detectable detectable = null;
			detectable = destroyGameObject.Value.GetComponent<StateMachineController>().softBody.GetComponent<Detectable>();

			if (detectable != null && CaculateRegeToEnemy.instance.EnemyTrans.Contains(detectable))
				CaculateRegeToEnemy.instance.EnemyDeath(detectable);
		}
		
		GameObject.Destroy(destroyGameObject.Value);
	}

	//void CheckFacing()
 //   {
	//	if (component.Value.entity.isEnabled)
	//	{
	//		if (component.Value.entity.checkEnemyInSigh.enemy != null)
	//		{
	//			float dir = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position.x - component.Value.entity.centerPoint.centerPoint.position.x;
	//			if (dir == 0) dir = 0.01f;
				
	//			rangeToEnemy = dir / Mathf.Abs(dir) * Vector2.Distance(component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position, component.Value.entity.centerPoint.centerPoint.position);						
	//		}
	//	}

	//	characterDirection = component.Value.entity.stateMachineContainer.stateMachine.characterDirection;

	//	if (rangeToEnemy > 0.1f)
	//	{

	//		if (characterDirection.isFaceRight)
	//		{

	//			characterDirection.ForceChangeDirection();
	//		}
	//	}
	//	else if (rangeToEnemy < -0.1f)
	//	{

	//		if (!characterDirection.isFaceRight)
	//		{
	//			characterDirection.ForceChangeDirection();
	//		}
	//	}
	//}
}