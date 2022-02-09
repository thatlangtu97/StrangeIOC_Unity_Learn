
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public enum PowerCollider
{
    Node,
    Small,
    Medium,
    Heavy,
    KnockDown,
}
public interface IComboEvent 
{

    int id { get; set; }
    float timeTrigger { get; }
    void OnEventTrigger(GameEntity entity);
    void OnUpdateTrigger();
    void Recycle();
}
#region CAST PROJECTILE
public class CastProjectileEvent : IComboEvent
{
    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f,5f)]
    public float duration;

    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    public GameObject Prefab;

    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    public Vector3 Localosition;

    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    public bool recycleWhenFinishDuration = false;

    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            prefabSpawned = ObjectPool.Spawn(Prefab);
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            prefabSpawned.transform.localScale = new Vector3(   prefabSpawned.transform.localScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f), 
                                                                prefabSpawned.transform.localScale.y, 
                                                                prefabSpawned.transform.localScale.z);
            prefabSpawned.transform.position = baseTransform.position + new Vector3(    Localosition.x * baseTransform.localScale.x, 
                                                                                        Localosition.y * baseTransform.localScale.y, 
                                                                                        Localosition.z * baseTransform.localScale.z);
            ObjectPool.instance.Recycle(prefabSpawned, duration);
        }
    }

    public void Recycle()
    {
        if (recycleWhenFinishDuration)
        {
            if (prefabSpawned)
                ObjectPool.Recycle(prefabSpawned);
        }
    }

    public void OnUpdateTrigger()
    {
    }
}
#endregion

#region CAST BOX COLLIDER
public class CastBoxColliderEvent : IComboEvent
{
    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider", true, true)]
    //[HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public Vector3 position;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public Vector3 sizeBox;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public LayerMask layerMaskEnemy;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public PowerCollider powerCollider;

    [FoldoutGroup("CAST BOX COLLIDER")]
    //[BoxGroup("Cast Collider")]
    public Vector2 force;
    [FoldoutGroup("CAST BOX COLLIDER")]
    public bool castByTime;

    [FoldoutGroup("CAST BOX COLLIDER")]
    [ShowIf("castByTime")]
    public int idStartCastByTime;

    [FoldoutGroup("CAST BOX COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStartCastByTime;

    [FoldoutGroup("CAST BOX COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStepCastByTime;

    [FoldoutGroup("CAST BOX COLLIDER")]
    [ShowIf("castByTime")]
    public int maxCastByTime;

    private int countCast;

    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        Collider2D[] cols = null;
        Transform transform = entity.stateMachineContainer.stateMachine.transform;
        Vector3 point = transform.position + new Vector3((position.x + (sizeBox.x / 2f)) * transform.localScale.x, position.y, position.z);
        float angle = transform.localScale.x > 0 ? 0f : 180f;
        cols = Physics2D.OverlapBoxAll(point, sizeBox, angle, layerMaskEnemy);
        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col != null)
                {
                    Action action = delegate
                    {
                        col.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(force.x * transform.localScale.x, force.y), col.transform.position);
                    };
                    DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box);
#endif
    }

    public void OnUpdateTrigger()
    {
        if (castByTime)
        {
            if (countCast < maxCastByTime)
            {
                timeTriggerEvent = timeStartCastByTime + countCast * timeStepCastByTime;
                idEvent = idStartCastByTime + countCast;
                countCast += 1;
            }
        }
    }

    public void Recycle()
    {
        if (castByTime)
        {
            timeTriggerEvent = timeStartCastByTime;
            idEvent = idStartCastByTime;
            countCast = 0;
        }
    }
}
#endregion

#region CAST CIRCLE COLLIDER
public class CastCircleColliderEvent : IComboEvent
{
    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    public Vector3 position;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    public float radius;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    public LayerMask layerMaskEnemy;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    public PowerCollider powerCollider;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    //[BoxGroup("Cast Circle Collider")]
    public Vector2 force;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    public bool castByTime;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    [ShowIf("castByTime")]
    public int idStartCastByTime;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStartCastByTime;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStepCastByTime;

    [FoldoutGroup("CAST CIRCLE COLLIDER")]
    [ShowIf("castByTime")]
    public int maxCastByTime;

    private int countCast;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        Collider2D[] cols = null;
        Transform transform = entity.stateMachineContainer.stateMachine.transform;
        Vector3 point = transform.position + new Vector3(position.x * transform.localScale.x, position.y, position.z);
        cols = Physics2D.OverlapCircleAll(point, radius, layerMaskEnemy);
        if (cols != null)
        {
            foreach (var col in cols)
            {
                if (col != null)
                {
                    Action action = delegate
                    {
                        col.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(force.x * transform.localScale.x, force.y), col.transform.position);
                    };
                    DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle);
#endif
    }

    public void OnUpdateTrigger()
    {
        if (castByTime)
        {
            if (countCast < maxCastByTime)
            {
                timeTriggerEvent = timeStartCastByTime + countCast * timeStepCastByTime;
                idEvent = idStartCastByTime + countCast;
                countCast += 1;
            }
        }
    }

    public void Recycle()
    {
        if (castByTime)
        {
            timeTriggerEvent = timeStartCastByTime;
            idEvent = idStartCastByTime;
            countCast = 0;
        }
    }
}
#endregion

#region CAST ENABLE MESH RENDERER
public class CastEnableMeshRenderer : IComboEvent
{
    [FoldoutGroup("CAST ENABLE MESH RENDERER")]
    //[BoxGroup("Enable Mesh Renderer", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST ENABLE MESH RENDERER")]
    //[BoxGroup("Enable Mesh Renderer")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST ENABLE MESH RENDERER")]
    //[BoxGroup("Enable Mesh Renderer")]
    public bool enable;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        MeshRenderer mesh = entity.stateMachineContainer.stateMachine.componentManager.meshRenderer;
        if (mesh != null)
        {
            mesh.enabled = enable;
        }
    }

    public void OnUpdateTrigger()
    {
        
    }

    public void Recycle()
    {
    }
}
#endregion

#region CAST IMPACK
public class CastImpackEvent : IComboEvent
{
    //[FoldoutGroup("$arenaName")]
    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public float duration = 0.5f;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public GameObject Prefab;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public Vector3 Localosition;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public Vector3 LocalRotation;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public Vector3 LocalScale;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public bool setParent = true;

    [FoldoutGroup("CAST IMPACK")]
    //[BoxGroup("Cast Impack Event")]
    public bool recycleWhenFinishDuration = false;

    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            prefabSpawned = ObjectPool.Spawn(Prefab);
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            
            prefabSpawned.transform.parent = baseTransform;
            prefabSpawned.transform.localPosition = new Vector3(Localosition.x , Localosition.y , Localosition.z );
            prefabSpawned.transform.localRotation = Quaternion.Euler(LocalRotation);
            prefabSpawned.transform.localScale = LocalScale;
            if (!setParent)
            {
                prefabSpawned.transform.parent = null;
            }
            ObjectPool.instance.Recycle(prefabSpawned, duration);
        }
    }
    public void Recycle()
    {
        if (recycleWhenFinishDuration)
        {
            if (prefabSpawned)
                ObjectPool.Recycle(prefabSpawned);
        }
        else
        {
            if (prefabSpawned)
                prefabSpawned.transform.parent = null;
        }
    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion

#region CAST FORWARD PROJECTILE
public class CastForwardProjectileEvent : IComboEvent
{
    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float duration;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public GameObject Prefab;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public Vector3 Localosition;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Impack Event")]
    public Vector3 LocalRotation;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public bool setPatent = false;

    [FoldoutGroup("CAST PROJECTILE FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public bool recycleWhenFinishDuration = false;


    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            prefabSpawned = ObjectPool.Spawn(Prefab);
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            prefabSpawned.transform.localScale = new Vector3(prefabSpawned.transform.localScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
                                                                prefabSpawned.transform.localScale.y,
                                                                prefabSpawned.transform.localScale.z);
            prefabSpawned.transform.position = baseTransform.position + new Vector3(Localosition.x * baseTransform.localScale.x,
                                                                                        Localosition.y * baseTransform.localScale.y,
                                                                                        Localosition.z * baseTransform.localScale.z);
            prefabSpawned.transform.parent = baseTransform;
            prefabSpawned.transform.localRotation = Quaternion.Euler(LocalRotation);
            if(setPatent==false)
                prefabSpawned.transform.parent = null;
            ObjectPool.instance.Recycle(prefabSpawned, duration);
        }
    }

    public void Recycle()
    {
        if (recycleWhenFinishDuration)
        {
            if (prefabSpawned)
                ObjectPool.Recycle(prefabSpawned);
        }
    }

    public void OnUpdateTrigger()
    {
    }
}
#endregion
#region CAST FORWARD ENEMY PROJECTILE
public class CastForwardEnemyProjectileEvent : IComboEvent
{
    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float duration;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public GameObject Prefab;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public Vector3 Localosition;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Impack Event")]
    public Vector3 LocalScale = Vector3.one;
    
    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Collider")]
    public LayerMask layerMaskEnemy;
    
    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Collider")]
    public float radiusCastEnemy;
    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public bool setPatent = false;

    [FoldoutGroup("CAST PROJECTILE ENEMY FORWARD")]
    //[BoxGroup("Cast Projectile")]
    public bool recycleWhenFinishDuration = false;


    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            prefabSpawned = ObjectPool.Spawn(Prefab);
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            prefabSpawned.transform.localScale = new Vector3(LocalScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
                LocalScale.y,
                LocalScale.z);
            prefabSpawned.transform.position = baseTransform.position + new Vector3(Localosition.x * baseTransform.localScale.x,
                                                                                        Localosition.y * baseTransform.localScale.y,
                                                                                        Localosition.z * baseTransform.localScale.z);
            prefabSpawned.transform.parent = baseTransform;
            
            Collider2D[] cols = null;
            
            cols = Physics2D.OverlapCircleAll(baseTransform.position, radiusCastEnemy, layerMaskEnemy);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col != null)
                    {
                        Debug.Log(col.gameObject.name);
                        Vector3 direction = col.transform.position - baseTransform.position;
                        prefabSpawned.transform.right = direction.normalized  * baseTransform.localScale.x ;
                        break;
                    }
                }
            }
            
            if(setPatent==false)
                prefabSpawned.transform.parent = null;
            ObjectPool.instance.Recycle(prefabSpawned, duration);
        }
    }

    public void Recycle()
    {
        if (recycleWhenFinishDuration)
        {
            if (prefabSpawned)
                ObjectPool.Recycle(prefabSpawned);
        }
    }

    public void OnUpdateTrigger()
    {
    }
}
#endregion
