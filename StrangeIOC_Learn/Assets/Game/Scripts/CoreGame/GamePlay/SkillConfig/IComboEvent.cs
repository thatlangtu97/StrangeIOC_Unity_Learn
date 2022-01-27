
using UnityEngine;
using Sirenix.OdinInspector;
public interface IComboEvent 
{

    int id { get; set; }
    float timeTrigger { get; }
    void OnEventTrigger(GameEntity entity);
    void Recycle();
}
public class CastProjectileEvent : IComboEvent
{
    [BoxGroup("Cast Projectile")]
    [GUIColor(0f,1f,0f)]
    public int idEvent;

    [BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [BoxGroup("Cast Projectile")]
    [Range(0f,5f)]
    public float duration;

    [BoxGroup("Cast Projectile")]
    public GameObject Prefab;

    [BoxGroup("Cast Projectile")]
    public Vector3 Localosition;

    [BoxGroup("Cast Projectile")]
    [LabelWidth(300)]
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
}
public class CastBoxColliderEvent : IComboEvent
{
    [BoxGroup("Cast Collider")]
    [GUIColor(0f, 1f, 0f)]
    public int idEvent;

    [BoxGroup("Cast Collider")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [BoxGroup("Cast Collider")]
    public Vector3 position;

    [BoxGroup("Cast Collider")]
    public Vector3 sizeBox;

    [BoxGroup("Cast Collider")]
    public LayerMask layerMaskEnemy;

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
                    DealDmgManager.DealDamage(col, entity);
                    break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box);
#endif
    }
    public void Recycle()
    {
    }
}
public class CastCircleColliderEvent : IComboEvent
{
    [BoxGroup("Cast Collider")]
    [GUIColor(0f, 1f, 0f)]
    public int idEvent;

    [BoxGroup("Cast Collider")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [BoxGroup("Cast Collider")]
    public Vector3 position;

    [BoxGroup("Cast Collider")]
    public float radius;

    [BoxGroup("Cast Collider")]
    public LayerMask layerMaskEnemy;

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
                    DealDmgManager.DealDamage(col, entity);
                    break;
                }
            }
        }
#if UNITY_EDITOR
        GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle);
#endif
    }
    public void Recycle()
    {
    }
}
public class CastEnableMeshRenderer : IComboEvent
{
    [BoxGroup("Enable Mesh Renderer")]
    [GUIColor(0f, 1f, 0f)]
    public int idEvent;

    [BoxGroup("Enable Mesh Renderer")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [BoxGroup("Enable Mesh Renderer")]
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
    public void Recycle()
    {
    }
}
public class CastImpackEvent : IComboEvent
{
    [BoxGroup("Cast Impack Event")]
    [GUIColor(0f, 1f, 0f)]
    public int idEvent;

    [BoxGroup("Cast Impack Event")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [BoxGroup("Cast Impack Event")]
    public float duration = 0.5f;

    [BoxGroup("Cast Impack Event")]
    public GameObject Prefab;

    [BoxGroup("Cast Impack Event")]
    public Vector3 Localosition;

    [BoxGroup("Cast Impack Event")]
    public Vector3 LocalRotation;

    [BoxGroup("Cast Impack Event")]
    public Vector3 LocalScale;

    [BoxGroup("Cast Impack Event")]
    public bool setParent = true;

    [BoxGroup("Cast Impack Event")]
    [LabelWidth(300)]
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
            prefabSpawned.transform.parent = null;
        }
    }
}
