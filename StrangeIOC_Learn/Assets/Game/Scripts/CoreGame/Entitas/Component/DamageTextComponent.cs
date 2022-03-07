using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
[Game]
public class DamageTextComponent  : IComponent
{
    public DamageTextType damageTextType;
    public string value;
    public Vector3 position;
}
public enum DamageTextType
{
    Normal,
    Block,
}