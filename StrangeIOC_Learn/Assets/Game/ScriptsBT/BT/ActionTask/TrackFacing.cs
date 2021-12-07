using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TrackFacing : Action
{
	public SharedFloat rangeToEnemy;
	public SharedComponentManager componentManager;
	public bool isUseDistance;
    CharacterDirection characterDirection;
	public override void OnStart()
	{
        if (componentManager.Value.entity.isEnabled)
        {
            if (componentManager.Value.entity.checkEnemyInSigh.enemy != null)
            {
                float dir = componentManager.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position.x - componentManager.Value.entity.centerPoint.centerPoint.position.x;
                if (dir == 0) dir = 0.01f;
                if (isUseDistance)
                {
                    rangeToEnemy.Value = dir / Mathf.Abs(dir) * Vector2.Distance(componentManager.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position, componentManager.Value.entity.centerPoint.centerPoint.position);
                }

                else
                {
                    rangeToEnemy.Value = dir;
                }
            }
        }

        characterDirection = componentManager.Value.entity.stateMachineContainer.stateMachine.characterDirection;
    }

	public override TaskStatus OnUpdate()
	{
        if (rangeToEnemy.Value > 0.1f)
        {

            if (!characterDirection.isFaceRight) 
            {
               
                characterDirection.ForceChangeDirection();
            }
        }
        else if (rangeToEnemy.Value < -0.1f)
        {

            if (characterDirection.isFaceRight)
            {         
                characterDirection.ForceChangeDirection();
            }
        }
        else
        {
            
        }
        return TaskStatus.Success;
	}
}