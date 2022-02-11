using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "HeroConfigCollection", menuName = "Data/HeroConfigCollection")]
public class HeroConfigCollection : ScriptableObject
{
    public List<HeroConfig> heroes;
}
[System.Serializable]
public class HeroConfig
{
    [PreviewField]
    public Sprite preview;
    public string name;
    public int id;
}

