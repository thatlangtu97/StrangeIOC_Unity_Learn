using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
[TaskCategory("MyGame")]

public class RandomPositionByLimit : Action
{
    public SharedComponentManager componentManager;
    public SharedVector2 refPoint;
    public SharedVector2 limit;
    public float minX;
    public float minY;
    bool onRight;
    bool close;
    public int randomDir = 1;

    public override TaskStatus OnUpdate()
    {
        Vector3 enemyP = componentManager.Value.entity.checkEnemyInSigh.lockedTarget.centerPoint.centerPoint.position;
        bool right;
        if (componentManager.Value.entity.centerPoint.centerPoint.position.x > enemyP.x)
        {
            right = true;
            randomDir = -1;
        }
        else
        {
            right = false;
            randomDir = 1;
        }

        close = !close;
        float offSetX = 0;
        if (close)
        {
            offSetX = Random.Range(0, limit.Value.x/2);
        }
        else
        {
            offSetX = Random.Range(limit.Value.x / 2, limit.Value.x );
        }

        float ymin = minY + LevelCreator.instance.map.leftAnchor.position.y;
         
        refPoint.Value = new Vector2(refPoint.Value.x + randomDir * offSetX, Random.Range(ymin, ymin + limit.Value.y));
        return TaskStatus.Success;
    }
}
