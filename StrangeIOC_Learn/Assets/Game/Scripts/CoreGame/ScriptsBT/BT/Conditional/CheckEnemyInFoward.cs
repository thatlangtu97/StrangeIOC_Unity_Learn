using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Extension")]
public class CheckEnemyInFoward : Conditional
{
    // Start is called before the first frame update
    public SharedComponentManager componentManager;
    public SharedFloat rangeToEnemy;
    //CharacterDirection characterDirection;
    public override void OnStart()
    {
        base.OnStart();
        //if (characterDirection == null)
        //    characterDirection = componentManager.Value.GetComponent<CharacterDirection>();
    }
    public override TaskStatus OnUpdate()
    {

        if (rangeToEnemy.Value > 0.1f)
        {
            if (!componentManager.Value.isFaceRight) // neu k dang quay mat ben phai thi change direction
            {
                return TaskStatus.Success;
            }
            else
            {

                return TaskStatus.Failure;
            }
        }
        else if (rangeToEnemy.Value < -0.1f)
        {
            if (componentManager.Value.isFaceRight)

            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
        else
        {
            return TaskStatus.Failure;
        }
      
    }
}
