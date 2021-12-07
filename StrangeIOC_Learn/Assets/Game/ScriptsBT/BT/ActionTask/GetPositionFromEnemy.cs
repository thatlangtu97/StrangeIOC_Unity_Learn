using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPositionFromEnemy : Action
{
    public SharedFloat offSetX, offSetY;
    public SharedVector2 storedValue;
    public bool useRandomX, useRandomY;
    public SharedSkillCheck selectedSkill;
    public SharedComponentManager component;
    public bool isNotuseGround;
    public bool isRandomDirection;
    int randDomNumber;
    
    public override void OnStart()
    {
        var tempValue = new SharedVector2();
        int breakCount = 0;
        var enemyPos = component.Value.entity.checkEnemyInSigh.lockedTarget.centerPoint.centerPoint;

        Vector2 newPos = Vector2.zero;
        do
        {
            if (selectedSkill != null)
            {
                offSetX = Random.Range(selectedSkill.Value.minRange, selectedSkill.Value.range);
            }

            breakCount++;
            if (breakCount > 10)
            {
                break;
            }

            float directionX = (Random.Range(0, 100) > 50 && useRandomX) ? -1f : 1f;
            float directionY = (Random.Range(0, 100) > 50 && useRandomY) ? -1f : 1f;
            newPos = new Vector2(enemyPos.position.x + directionX * offSetX.Value,
                enemyPos.position.y + directionY * offSetY.Value);
            if ((newPos.x > LevelCreator.instance.map.rightAnchor.position.x - 1f) ||
                (newPos.x < LevelCreator.instance.map.leftAnchor.position.x + 1f))
            {
                newPos = new Vector2(enemyPos.position.x - directionX * offSetX.Value,
                    enemyPos.position.y + directionY * offSetY.Value);
            }

            if (!isNotuseGround) // use ground
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(newPos, Vector2.down, 10f, GameSceneConfig.instance.groundCastMask);
                if (hit.collider != null)
                {
                    newPos = hit.point;
                }
            }
        } while (newPos.x < LevelCreator.instance.map.leftAnchor.position.x ||
                 newPos.x > LevelCreator.instance.map.rightAnchor.position.x);

        tempValue.Value = newPos;
        storedValue.SetValue(tempValue.Value);        
    }
}