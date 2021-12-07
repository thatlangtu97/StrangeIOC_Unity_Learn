using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
[TaskCategory("MyGame")]

public class GetAttackPointByDistanceXY : Action
{
    public SharedComponentManager componentManager;
    public SharedFloat xDistance;
    public SharedFloat height;
    public int limit = 0;
    float y;
    public SharedVector2 AttackPoint;
    public SharedVector2 EnemyPoint;

    public override void OnStart()
    {
        y = LevelCreator.instance.map.leftAnchor.position.y + height.Value;
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log(componentManager.Value.entity.checkEnemyInSigh.enemy);
        Vector2 enemyP = componentManager.Value.entity.checkEnemyInSigh.lockedTarget.centerPoint.centerPoint.position;
        Vector2 atkPoint;
        EnemyPoint.Value = enemyP;
        limit = 0;
        if(enemyP.x - xDistance.Value - 0.5f < LevelCreator.instance.map.leftAnchor.position.x)
        {
            limit = -1;
        }
        else if(enemyP.x + xDistance.Value + 0.5f > LevelCreator.instance.map.rightAnchor.position.x)
        {
            limit = 1;
        }
        if(limit > 0)
        {
            atkPoint = new Vector2(enemyP.x - xDistance.Value,y);
        }else if (limit < 0)
        {
            atkPoint = new Vector2(enemyP.x + xDistance.Value, y);
        }
        else
        {
            if(componentManager.Value.entity.centerPoint.centerPoint.position.x > enemyP.x)
            {
                atkPoint = new Vector2(enemyP.x + xDistance.Value, y);
            }
            else
            {
                atkPoint = new Vector2(enemyP.x - xDistance.Value, y);
            }
        }
        AttackPoint.Value = atkPoint;
        //if (Vector3.Distance(atkPoint, AttackPoint.Value) > 0.5f)
        //{
            
        //}

        return TaskStatus.Success;
    }
}
