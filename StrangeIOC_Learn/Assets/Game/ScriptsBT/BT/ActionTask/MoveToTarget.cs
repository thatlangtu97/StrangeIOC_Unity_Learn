using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToTarget : Action
{
	public SharedComponentManager component;
	//public bool useGround;
	float currDuration;
	public Vector2 velocity;
	public Vector3 minOffSet;
	public Vector3 maxOffSet;
	public float duration;
	public Vector2 target;
	//move over time if time runs out then stop
	public override void OnStart()
	{
		currDuration = duration;
	}

	public override TaskStatus OnUpdate()
	{
		target = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position;


		currDuration -= Time.deltaTime;

		if(component.Value.entity.stateMachineContainer.stateMachine.characterDirection.isFaceRight)
		component.Value.entity.rigidbodyContainer.rigidbody.MovePosition(component.Value.entity.rigidbodyContainer.rigidbody.position + velocity * Time.deltaTime);
		else
		component.Value.entity.rigidbodyContainer.rigidbody.MovePosition(component.Value.entity.rigidbodyContainer.rigidbody.position - velocity * Time.deltaTime);

		if(component.Value.entity.rigidbodyContainer.rigidbody.position.x <= (target.x +maxOffSet.x) && component.Value.entity.rigidbodyContainer.rigidbody.position.x >= (target.x - minOffSet.x))
        {

			return TaskStatus.Success;
		}
			
		if (currDuration > 0)
        {
			return TaskStatus.Running;
		}
		return TaskStatus.Success;
			//rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
		
	}
}