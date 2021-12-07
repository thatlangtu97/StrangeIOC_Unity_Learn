using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyForDurationTask : Action
{
    public SharedComponentManager component;
    public bool useGround;
    float currDuration;
    public Vector3 offSet;
    public SharedFloat duration;
    public override void OnStart()
    {
        currDuration = 0f;
    }
    public override TaskStatus OnUpdate()
    {
        var targetPos = component.Value.entity.checkEnemyInSigh.enemy.centerPoint.centerPoint.position;
        if(useGround)
        {
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.down, 10f, GameSceneConfig.instance.groundCastMask);
            if (hit.collider != null)
            {
                targetPos = hit.point;
            }
        }
        currDuration += Time.deltaTime;
        var newPos = targetPos+new Vector3( transform.right.x* offSet.x, offSet.y, 0f);
        if (!(newPos.x > LevelCreator.instance.map.rightAnchor.position.x - 1f) &&
            !(newPos.x < LevelCreator.instance.map.leftAnchor.position.x + 1f))
        {
            transform.position = newPos;
        }
       
        if(currDuration<duration.Value)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }

}
