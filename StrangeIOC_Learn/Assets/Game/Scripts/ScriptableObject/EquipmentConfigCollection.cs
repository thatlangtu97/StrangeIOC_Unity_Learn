using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentConfigCollection", menuName = "Data/EquipmentCollection")]
public class EquipmentConfigCollection : ScriptableObject
{
    public List<EquipmentConfig> weaponCollection;
    public List<EquipmentConfig> armorCollection;
    public List<EquipmentConfig> ringCollection;
    public List<ColorRarity> colorRarity;
}
[System.Serializable]
public class ColorRarity
{
    public Rarity rarity;

    public Color color;
}