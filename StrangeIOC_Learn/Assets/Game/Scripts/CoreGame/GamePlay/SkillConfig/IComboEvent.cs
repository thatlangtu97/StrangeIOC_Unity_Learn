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
public enum TypeComponent
{
    MeshRenderer,
    
}
public enum TypeSpawn
{
    Transform,
    RigidBody2D,
    Forward,
    
}
public enum ColliderCast
{
    Box,
    Circle,
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
    
    [FoldoutGroup("CAST PROJECTILE")]
    //[BoxGroup("Cast Projectile")]
    public LayerMask LayerMask ;

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
            RaycastHit2D hit = Physics2D.Raycast(baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Localosition.x, LayerMask);
            if (hit.collider != null)
            {
                prefabSpawned.transform.position = new Vector3(hit.point.x,prefabSpawned.transform.position.y,prefabSpawned.transform.position.z); 
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
                    //break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box,angle);
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
                    //break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle,0);
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
    [ReadOnly()]
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

#region CAST ADD FORCE
public class CastAddForce : IComboEvent
{
    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile")]
    public Vector3 force;



    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        Rigidbody2D baseRigidbody = entity.stateMachineContainer.stateMachine.componentManager.rgbody2D;
        Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
        
        Vector3 CalculateForce = new Vector3(force.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
            force.y,
            force.z);
        baseRigidbody.AddForce(CalculateForce);
    }

    public void Recycle()
    {
    }

    public void OnUpdateTrigger()
    {
    }
}
#endregion







#region CAST VFX
public class CastVfxEvent : IComboEvent
{
    [FoldoutGroup("CAST VFX")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("CAST VFX")]
    public float timeTriggerEvent;
    
    [FoldoutGroup("CAST VFX")]
    public float duration = 0.5f;

    [FoldoutGroup("CAST VFX")]
    public GameObject Prefab;

    [FoldoutGroup("CAST VFX")]
    public Vector3 Localosition;

    [FoldoutGroup("CAST VFX")]
    public Vector3 LocalRotation;

    [FoldoutGroup("CAST VFX")]
    public Vector3 LocalScale;

    [FoldoutGroup("CAST VFX")]
    public bool setParent = true;

    [FoldoutGroup("CAST VFX")]
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



#region CAST ENEMY VFX
public class CastEnemyEvent : IComboEvent
{
    [FoldoutGroup("CAST ENEMY VFX")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("CAST ENEMY VFX")]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST ENEMY VFX")]
    public bool notUseDuration ;
    
    [FoldoutGroup("CAST ENEMY VFX")]
    public float duration = 0.5f;

    [FoldoutGroup("CAST ENEMY VFX")]
    public GameObject Prefab;

    [FoldoutGroup("CAST ENEMY VFX")]
    public Vector3 Localosition;
    

    [FoldoutGroup("CAST ENEMY VFX")]
    public Vector3 LocalScale;

    
    [FoldoutGroup("CAST ENEMY VFX")]
    public Vector2 ForceSpawn ;

    
    [FoldoutGroup("CAST ENEMY VFX")]
    public bool setParent = true;

    [FoldoutGroup("CAST ENEMY VFX")]
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
            prefabSpawned.transform.localScale = LocalScale;
            
            if (!setParent)
            {
                prefabSpawned.transform.parent = null;
            }
            Rigidbody2D rg = prefabSpawned.GetComponent<Rigidbody2D>();
            if (rg)
            {
                rg.AddForce(new Vector2(ForceSpawn.x*prefabSpawned.transform.localScale.x,ForceSpawn.y *prefabSpawned.transform.localScale.y));
            }
            if(!notUseDuration)
                ObjectPool.instance.Recycle(prefabSpawned, duration);
            
        }
    }
    public void Recycle()
    {
        if (!notUseDuration)
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
    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion
/// <summary>
/// //////////////////////
/// </summary>

#region ENABLE COMPONENT
public class EnableComponent : IComboEvent
{
    [FoldoutGroup("ENABLE COMPONENT")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("ENABLE COMPONENT")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("ENABLE COMPONENT")]
    public bool enable;

    [FoldoutGroup("ENABLE COMPONENT")] 
    public TypeComponent Component;
    
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        switch (Component)
        {
            case TypeComponent.MeshRenderer:
                MeshRenderer meshRenderer  = entity.stateMachineContainer.stateMachine.componentManager.meshRenderer ;
                if (meshRenderer != null) meshRenderer.enabled = enable;
                break;
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
#region SPAWN GAMEOBJECT
public class SpawnGameObject : IComboEvent
{
    [FoldoutGroup("SPAWN GAMEOBJECT")][ReadOnly] public int idEvent;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public float timeTriggerEvent;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public float duration = 0.5f;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public GameObject Prefab;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 localPosition;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 LocalRotation;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 LocalScale = Vector3.one;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]    [ShowIf("typeSpawn", TypeSpawn.RigidBody2D)]     public Vector2 ForceSpawn ;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool setParent ;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool UseDuration;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool forceWhenFinishEvent ;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public TypeSpawn typeSpawn;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public LayerMask LayerMask ;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        
        if (Prefab)
        {
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            switch (typeSpawn)
            {
                case TypeSpawn.Transform:
                    prefabSpawned = ObjectPool.Spawn(Prefab);
                    prefabSpawned.transform.parent = baseTransform;
                    prefabSpawned.transform.localPosition = new Vector3(localPosition.x , localPosition.y , localPosition.z );
                    prefabSpawned.transform.localRotation = Quaternion.Euler(LocalRotation);
                    prefabSpawned.transform.localScale = LocalScale;
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMask,prefabSpawned.transform,baseTransform);
                    break;
                case TypeSpawn.RigidBody2D:
                    prefabSpawned = ObjectPool.Spawn(Prefab);
                    prefabSpawned.transform.parent = baseTransform;
                    prefabSpawned.transform.localPosition = new Vector3(localPosition.x , localPosition.y , localPosition.z );
                    prefabSpawned.transform.localScale = LocalScale;
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMask,prefabSpawned.transform,baseTransform);
                    Rigidbody2D rg = prefabSpawned.GetComponent<Rigidbody2D>();
                    if (rg)
                    {
                        rg.AddForce(new Vector2(ForceSpawn.x*prefabSpawned.transform.localScale.x,ForceSpawn.y *prefabSpawned.transform.localScale.y));
                    }
                    break;
                    ;
//                case TypeSpawn.Forward:
//                    prefabSpawned = ObjectPool.Spawn(Prefab);
//                    prefabSpawned.transform.localScale = new Vector3(LocalScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
//                        LocalScale.y,
//                        LocalScale.z);
//                    prefabSpawned.transform.position = baseTransform.position + new Vector3(localPosition.x * baseTransform.localScale.x,
//                                                           localPosition.y * baseTransform.localScale.y,
//                                                           localPosition.z * baseTransform.localScale.z);
//                    prefabSpawned.transform.parent = baseTransform;
//            
//                    Collider2D[] cols = null;
//            
//                    cols = Physics2D.OverlapCircleAll(baseTransform.position, radiusCastEnemy, layerMaskEnemy);
//                    if (cols != null)
//                    {
//                        foreach (var col in cols)
//                        {
//                            if (col != null)
//                            {
//                                Vector3 direction = col.transform.position - baseTransform.position;
//                                prefabSpawned.transform.right = direction.normalized  * baseTransform.localScale.x ;
//                                break;
//                            }
//                        }
//                    }
//                    if (!setParent)
//                    {
//                        prefabSpawned.transform.parent = null;
//                    }
//                    ObjectPool.instance.Recycle(prefabSpawned, duration);
//                    
//                    break;
            }
            if(UseDuration)
                ObjectPool.instance.Recycle(prefabSpawned, duration);
        }
    }
    public void Recycle()
    {
        if (UseDuration)
        {
            if (forceWhenFinishEvent)
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
    }

    public void OnUpdateTrigger()
    {
        
    }

    void UseRayCast(Vector2 fromPosition,Vector2 direction,float distance,int layerMask,Transform transform ,Transform parent)
    {
        RaycastHit2D hit = Physics2D.Raycast(parent.position, new Vector2(1,0)* parent.localScale.x, Mathf.Abs(localPosition.x), LayerMask);
        if (hit.collider != null)
        {
            transform.position = new Vector3(hit.point.x,transform.position.y,transform.position.z); 
        }
    }
}
#endregion
#region COLLIDER
public class CastColliderEvent : IComboEvent
{
    [FoldoutGroup("COLLIDER")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("COLLIDER")]
    public float timeTriggerEvent;
    
    [FoldoutGroup("COLLIDER")]
    public LayerMask layerMaskEnemy;
    
    [FoldoutGroup("COLLIDER")]
    public ColliderCast typeCast;
    
    [FoldoutGroup("COLLIDER")]
    public Vector3 localPosition;
    
    [FoldoutGroup("COLLIDER")]
    [ShowIf("typeCast", ColliderCast.Box)]
    public Vector3 sizeBox;
    
    [FoldoutGroup("COLLIDER")]
    [ShowIf("typeCast", ColliderCast.Circle)]
    public float radius;
    
    [FoldoutGroup("COLLIDER")]
    public bool useAngle;
    
    [FoldoutGroup("COLLIDER")]
    [ShowIf("useAngle")]
    public float angleCollider;
    
    [FoldoutGroup("COLLIDER")]
    public PowerCollider powerCollider;

    [FoldoutGroup("COLLIDER")]
    public Vector2 forcePower;
    
    [FoldoutGroup("COLLIDER")]
    public bool castByTime;

    [FoldoutGroup("COLLIDER")]
    [ShowIf("castByTime")]
    public int idStartCastByTime;

    [FoldoutGroup("COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStartCastByTime;

    [FoldoutGroup("COLLIDER")]
    [ShowIf("castByTime")]
    public float timeStepCastByTime;

    [FoldoutGroup("COLLIDER")]
    [ShowIf("castByTime")]
    public int maxCastByTime;

    
    private int countCast;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        ////    CAST BOX    ////////////////////////////////////////////
        if (typeCast == ColliderCast.Box)
        {
            Collider2D[] cols = null;
            Transform transform = entity.stateMachineContainer.stateMachine.transform;
            Vector3 point = transform.position + new Vector3((localPosition.x + (sizeBox.x / 2f)) * transform.localScale.x,
                                localPosition.y, localPosition.z);
            float angle = 0;
            if (!useAngle)
            {
                angle  = transform.localScale.x > 0 ? 0f : 180f;
            }
            else
            {
                if (transform.localScale.x > 0)
                {
                    angle = angleCollider;
                }
                else
                {
                    angle = 180f - angleCollider;
                }
            }
            
            cols = Physics2D.OverlapBoxAll(point, sizeBox, angle, layerMaskEnemy);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col != null)
                    {
                        Action action = delegate
                        {
                            col.GetComponent<Rigidbody2D>().AddForceAtPosition(
                                new Vector2(forcePower.x * transform.localScale.x, forcePower.y), col.transform.position);
                        };
                        DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    }
                }
            }
#if UNITY_EDITOR
            GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box,angle);
#endif
        }
        ////    CAST CIRCLE    ////////////////////////////////////////////
        else if(typeCast == ColliderCast.Circle)
        {
            Collider2D[] cols = null;
            Transform transform = entity.stateMachineContainer.stateMachine.transform;
            Vector3 point = transform.position + new Vector3(localPosition.x * transform.localScale.x, localPosition.y, localPosition.z);
            cols = Physics2D.OverlapCircleAll(point, radius, layerMaskEnemy);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col != null)
                    {
                        Action action = delegate
                        {
                            col.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(forcePower.x * transform.localScale.x, forcePower.y), col.transform.position);
                        };
                        DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    }
                }
            }
#if UNITY_EDITOR
            GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle,0);
#endif
        }
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
#region COLLIDER EVENT
public class ColliderEvent : IComboEvent
{
    [FoldoutGroup("COLLIDER EVENT")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("COLLIDER EVENT")]
    public float timeTriggerEvent;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public LayerMask layerMaskEnemy;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public ColliderCast typeCast;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public Vector3 localPosition;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("typeCast", ColliderCast.Box)]
    public Vector3 sizeBox;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("typeCast", ColliderCast.Circle)]
    public float radius;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public bool useAngle;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("useAngle")]
    public float angleCollider;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public PowerCollider powerCollider;

    [FoldoutGroup("COLLIDER EVENT")]
    public Vector2 forcePower;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public bool castByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public int idStartCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public float timeStartCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public float timeStepCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public int maxCastByTime;

    
    private int countCast;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        ////    CAST BOX    ////////////////////////////////////////////
        if (typeCast == ColliderCast.Box)
        {
            Collider2D[] cols = null;
            Transform transform = entity.stateMachineContainer.stateMachine.transform;
            Vector3 point = transform.position + new Vector3((localPosition.x + (sizeBox.x / 2f)) * transform.localScale.x,
                                localPosition.y, localPosition.z);
            float angle = 0;
            if (!useAngle)
            {
                angle  = transform.localScale.x > 0 ? 0f : 180f;
            }
            else
            {
                if (transform.localScale.x > 0)
                {
                    angle = angleCollider;
                }
                else
                {
                    angle = 180f - angleCollider;
                }
            }
            
            cols = Physics2D.OverlapBoxAll(point, sizeBox, angle, layerMaskEnemy);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col != null)
                    {
                        Action action = delegate
                        {
                            col.GetComponent<Rigidbody2D>().AddForceAtPosition(
                                new Vector2(forcePower.x * transform.localScale.x, forcePower.y), col.transform.position);
                        };
                        DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    }
                }
            }
#if UNITY_EDITOR
            GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box,angle);
#endif
        }
        ////    CAST CIRCLE    ////////////////////////////////////////////
        else if(typeCast == ColliderCast.Circle)
        {
            Collider2D[] cols = null;
            Transform transform = entity.stateMachineContainer.stateMachine.transform;
            Vector3 point = transform.position + new Vector3(localPosition.x * transform.localScale.x, localPosition.y, localPosition.z);
            cols = Physics2D.OverlapCircleAll(point, radius, layerMaskEnemy);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    if (col != null)
                    {
                        Action action = delegate
                        {
                            col.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(forcePower.x * transform.localScale.x, forcePower.y), col.transform.position);
                        };
                        DealDmgManager.DealDamage(col, entity, powerCollider, action);
                    }
                }
            }
#if UNITY_EDITOR
            GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle,0);
#endif
        }
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