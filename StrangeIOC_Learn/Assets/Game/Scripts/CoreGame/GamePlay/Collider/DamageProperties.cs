using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProperties : MonoBehaviour
{
    public float baseDamage;

    public DamageProperties(DamageProperties clone)
    {
        this.baseDamage = clone.baseDamage;
    }
}
