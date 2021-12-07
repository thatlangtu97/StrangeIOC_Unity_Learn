using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFxOnAnimation : Action
{
    public GameObject prefab;
    public SharedComponentManager manager;
    public Vector3 offSet;
    public SharedVector2 pos;
    public SharedFloat duration;
    public bool followTransform;
    public SharedTransform rootTransform;
    public bool notUsePool;
    public bool followFacing;
    public bool notSetParent;
    public bool spawnByEvent;
    GameObject fx;
    public override void OnStart()
    {
        if(spawnByEvent)
        {
            manager.Value.entity.stateMachineContainer.stateMachine.eventOnAnimation.offset = offSet;
            manager.Value.entity.stateMachineContainer.stateMachine.eventOnAnimation.EarthStompFx = prefab;
            return;
        }

        if(notSetParent)
        {
            
            if(manager.Value.entity.stateMachineContainer.stateMachine.characterDirection.isFaceRight)
                ObjectPool.Spawn(prefab, manager.Value.entity.centerPoint.centerPoint.position + offSet, Quaternion.Euler(0, 180, 0f));
            else
                ObjectPool.Spawn(prefab, manager.Value.entity.centerPoint.centerPoint.position + offSet, Quaternion.Euler(0, 0, 0f));
            return;
        }

        Transform tf = null;
        if (notUsePool)
        {
            GameObject effect = GameObject.Instantiate(prefab);
            if (followTransform)
            {
                if (rootTransform.Value != null)
                {
                    tf = rootTransform.Value;
                }
                else
                {
                    tf = manager.Value.entity.animatorContainer.animator.transform;
                }
                if (pos.Value != Vector2.zero)
                {
                    effect.transform.position = pos.Value + (Vector2)offSet;
                    GameObject.Destroy(effect, duration.Value);
                }
                else
                {
                    effect.transform.SetParent(tf);
                    effect.transform.localPosition = pos.Value + (Vector2)offSet;
                    GameObject.Destroy(effect, duration.Value);
                }
            }
            else
            {
                effect.transform.position = (Vector2)transform.position + (Vector2)offSet;
                GameObject.Destroy(effect, duration.Value);
            }

            if (followFacing)
            {
                effect.transform.right = transform.right;
            }
           
        }
        else
        {
            if (followTransform)
            {
                if (rootTransform.Value != null)
                {
                    tf = rootTransform.Value;
                }
                else
                {
                    tf = manager.Value.entity.animatorContainer.animator.transform;
                }
            }

            if (pos.Value != Vector2.zero)
            {
                EffectRequestManager.RequestEffect(prefab, null, pos.Value + (Vector2)offSet, duration.Value);
            }
            else
            {
                EffectRequestManager.RequestEffect(prefab, tf, manager.Value.entity.centerPoint.centerPoint.position + offSet, duration.Value);
            }

        }
       


    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}

