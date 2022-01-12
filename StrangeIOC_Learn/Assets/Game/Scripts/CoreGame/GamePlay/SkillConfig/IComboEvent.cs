
using UnityEngine;
public interface IComboEvent 
{
    int id { get; }
    float timeTrigger { get; }
    void OnEventTrigger(GameEntity entity);
    void Recycle();
}
public class CastProjectileEvent : IComboEvent
{
    public int idEvent;
    public float timeTriggerEvent;
    public float duration;
    public GameObject Prefab;
    public Vector3 Localosition;
    
    public int id { get { return idEvent; } }
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
        if (prefabSpawned)
            ObjectPool.Recycle(prefabSpawned);
    }
}
public class CastColliderEvent : IComboEvent
{
    public int idEvent;
    public float timeTriggerEvent;
    public Vector3 position;
    public Vector3 sizeBox;
    public LayerMask layerMaskEnemy;
    public int id { get { return idEvent; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        Collider2D[] cols = null;
        Transform transform = entity.stateMachineContainer.stateMachine.transform;
        //cols = Physics2D.OverlapBoxAll(transform.position + position, sizeBox, layerMaskEnemy);
        cols = Physics2D.OverlapBoxAll(transform.position + new Vector3(position.x * transform.localScale.x, position.y, position.z), sizeBox, transform.localScale.x > 0 ? 0f:180f, layerMaskEnemy) ;
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
    }
    public void Recycle()
    {
    }
}
public class CastEnableMeshRenderer : IComboEvent
{
    public int idEvent;
    public float timeTriggerEvent;
    public bool enable;
    public int id { get { return idEvent; } }
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
    public int idEvent;
    public float timeTriggerEvent;
    public int id { get { return idEvent; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    public float duration=0.5f;
    public GameObject Prefab;
    public Vector3 Localosition;
    public Vector3 LocalRotation;
    public Vector3 LocalScale;
    public bool setParent=true;
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
        if(prefabSpawned)
        ObjectPool.Recycle(prefabSpawned);
    }
}
