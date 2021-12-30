
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
        GameObject temp = ObjectPool.Spawn(Prefab);
        Transform baseTransform = entity.stateMachineContainer.stateMachine.transform;
        temp.transform.localScale = new Vector3(temp.transform.localScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f), temp.transform.localScale.y, temp.transform.localScale.z);
        temp.transform.position = baseTransform.position + new Vector3(Localosition.x* baseTransform.localScale.x, Localosition.y * baseTransform.localScale.y, Localosition.z * baseTransform.localScale.z);
        ObjectPool.instance.Recycle(temp, 1.5f);
    }
}