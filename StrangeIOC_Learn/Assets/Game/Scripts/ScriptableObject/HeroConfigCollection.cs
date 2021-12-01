using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeroConfigCollection", menuName = "Data/HeroConfigCollection")]
public class HeroConfigCollection : ScriptableObject
{
    public List<HeroConfig> heroes;
}
[System.Serializable]
public class HeroConfig
{
    public int id;
}

