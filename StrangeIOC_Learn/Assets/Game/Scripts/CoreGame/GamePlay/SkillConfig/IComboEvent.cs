using System.Collections;
using System.Collections.Generic;
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
        GameObject temp =  ObjectPool.Spawn(Prefab);
        temp.transform.position = entity.stateMachineContainer.stateMachine.transform.position + Localosition;
        ObjectPool.instance.Recycle(temp, 1.5f);
        //GameObject.Instantiate(Prefab, Localosition,Quaternion.identity);
    }
}