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
        Debug.Log("spawn projectile");
        GameObject.Instantiate(Prefab, Localosition,Quaternion.identity);
    }
}