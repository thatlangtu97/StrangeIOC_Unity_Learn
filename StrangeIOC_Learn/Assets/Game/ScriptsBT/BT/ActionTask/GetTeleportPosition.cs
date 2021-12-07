using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetTeleportPosition : Action
{
	public SharedComponentManager component;
	public TeleportType type;
	public SharedFloat offSetX, offSetY;
	public SharedVector2 storedValue;
	public float randomRange;
	public SharedSkillCheck selectedSkill;

	RaycastHit2D hit; //for corner
	
	public override void OnStart()
	{
	
}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}

	void GetRandomPosition()
    {

    }

	void GetCornerPosition()
    {

    }

	void GetTargetPosition()
    {

    }
}

public enum TeleportType
{
	Random,
	Corner,
	ToEnemy
}