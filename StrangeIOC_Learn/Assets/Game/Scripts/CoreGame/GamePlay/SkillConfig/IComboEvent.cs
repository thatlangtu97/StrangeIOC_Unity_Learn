
using UnityEngine;
public interface IComboEvent 
{
    int id { get; }
    float timeTrigger { get; }
    void OnEventTrigger(GameEntity entity);
}
public class CastProjectileEvent : IComboEvent
{
    public int idEvent;
    public float timeTriggerEvent;
    public GameObject Prefab;
    public Vector3 Localosition;
    public int id { get { return idEvent; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            GameObject temp = ObjectPool.Spawn(Prefab);
            Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
            temp.transform.localScale = new Vector3(temp.transform.localScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f), temp.transform.localScale.y, temp.transform.localScale.z);
            temp.transform.position = baseTransform.position + new Vector3(Localosition.x * baseTransform.localScale.x, Localosition.y * baseTransform.localScale.y, Localosition.z * baseTransform.localScale.z);
            ObjectPool.instance.Recycle(temp, 1.5f);
        }
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
}
