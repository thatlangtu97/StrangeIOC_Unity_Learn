using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Flee : Action
{	
	//public float safeDistance;

	public SharedComponentManager componentManager;
    public SharedFloat runAwayDuration;
	public SharedFloat chargeSpeedMultiple;
	public SharedFloat rangeToEnemy;
	public SharedBool isRunAwayFromWall;
	float speedScaleOriginal;
	public LayerMask stopMask;
	public float rangeCheck = 1f;
	public float animationSpeedScale;
	public bool notRandomSpeed;
	public int minRandomFleeDuration;
	public int maxRandomFleeDuration;
	public bool randomDuration;
	bool onSpeedUp = false;
    float startTime;
	public bool stopWhenHitMask;
    float duration;
	RaycastHit2D hit;
	public override void OnStart()
	{

		base.OnStart();
		speedScaleOriginal = componentManager.Value.entity.moveByDirection.speedScale;
		componentManager.Value.entity.isStopMove = false;
		if (!notRandomSpeed)
        {
			onSpeedUp = !onSpeedUp;
			if (onSpeedUp)
			{
				componentManager.Value.entity.stateMachineContainer.stateMachine.animator.SetFloat("SpeedScale", animationSpeedScale);
				componentManager.Value.entity.moveByDirection.speedScale = chargeSpeedMultiple.Value;
			}
			else
				componentManager.Value.entity.moveByDirection.speedScale = 1f;
		}
		else
        {
			componentManager.Value.entity.moveByDirection.speedScale = chargeSpeedMultiple.Value;
		}
        
        startTime = Time.time;
		if (randomDuration)
        {
			duration = Random.Range(minRandomFleeDuration, maxRandomFleeDuration);
		}	
		else
			duration = runAwayDuration.Value;
		
		componentManager.Value.entity.moveByDirection.direction = transform.right;
	}


    public override TaskStatus OnUpdate()
	{
		if(stopWhenHitMask)
        {
			hit = Physics2D.Raycast(componentManager.Value.entity.centerPoint.centerPoint.position, transform.right, rangeCheck, stopMask);
			if (hit)
			{
				componentManager.Value.entity.stateMachineContainer.stateMachine.animator.SetFloat("SpeedScale", 1f);
				return TaskStatus.Success;
			}
		}

		if (componentManager.Value.entity.isAttackComplete || componentManager.Value.entity.isSkillComplete)
		{
			
			componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
			componentManager.Value.entity.isStopMove = true;
			return TaskStatus.Success;
		}

		if (Time.time - startTime >= duration)
        {
            componentManager.Value.entity.stateMachineContainer.stateMachine.animator.SetFloat("SpeedScale", 1f);
            componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
            return TaskStatus.Success;
        }

        if (componentManager.Value.entity.checkWall.isHitWall)
		{
			isRunAwayFromWall.Value = true;
			componentManager.Value.entity.stateMachineContainer.stateMachine.characterDirection.ChangeDirection();
			if(componentManager.Value.entity.moveByDirection.direction.x < 0)
				componentManager.Value.entity.moveByDirection.direction = Vector2.right;
			else
				componentManager.Value.entity.moveByDirection.direction = Vector2.left;
			return TaskStatus.Running;
		}
		
        /*
		if(Mathf.Abs(rangeToEnemy.Value) < 0.5)
        {
			return TaskStatus.Running;
		}
        */

        

        /*
		RaycastHit2D hit = Physics2D.Raycast(componentManager.Value.entity.centerPoint.centerPoint.position, transform.right, rangeCheck, stopMask);
		if (hit.collider != null && isRunAwayFromWall.Value== false)
		{
            componentManager.Value.entity.stateMachineContainer.stateMachine.animator.SetFloat("SpeedScale", 1f);
            return TaskStatus.Success;
		}
        */
        /*
		if (Mathf.Abs(rangeToEnemy.Value) >= safeDistance )
        {
			componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
			isRunAwayFromWall.Value = false;
			safeDistance = storedSafeDistance;
            componentManager.Value.entity.stateMachineContainer.stateMachine.animator.SetFloat("SpeedScale", 1f);
            return TaskStatus.Success;
        }
        */


		return TaskStatus.Running;
	}

	public override void OnEnd()
	{

		base.OnEnd();
		if(componentManager.Value.entity.hasMoveByDirection)
		componentManager.Value.entity.moveByDirection.speedScale = speedScaleOriginal;
		componentManager.Value.entity.isStopMove = true;
	}
}