using System;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class DamageInfoSend
{
    public DamageInfoEvent damageInfoEvent;
    public DamageProperties damageProperties;
    public Action action;
    public DamageInfoSend(){}
    public DamageInfoSend(DamageInfoEvent damageInfoEvent , DamageProperties damageProperties, Action action)
    {
        this.damageInfoEvent = damageInfoEvent;
        this.damageProperties = damageProperties;
        this.action = action;
    }
}
