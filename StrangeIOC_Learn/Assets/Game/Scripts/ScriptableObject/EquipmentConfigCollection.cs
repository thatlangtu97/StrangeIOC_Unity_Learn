using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentConfigCollection", menuName = "Data/EquipmentCollection")]
public class EquipmentConfigCollection : ScriptableObject
{
    public List<EquipmentConfig> weaponCollection;
    public List<EquipmentConfig> armorCollection;
    public List<EquipmentConfig> ringCollection;
    public List<EquipmentConfig> charmCollection;
    public List<ColorRarity> colorRarity;
    public Dictionary<int, EquipmentConfig> cacheConfig = new Dictionary<int, EquipmentConfig>();
    public void Cache()
    {
        cacheConfig = new Dictionary<int, EquipmentConfig>();
        GearSlot[] gearSlots = new[]
            {
                GearSlot.weapon,GearSlot.armor,GearSlot.ring,GearSlot.charm
            };
        foreach (GearSlot tempGearSlot in gearSlots)
        {
            List<EquipmentConfig> tempConFigList = new List<EquipmentConfig>();
            switch (tempGearSlot)
            {
                case GearSlot.weapon:
                    tempConFigList = ScriptableObjectData.EquipmentConfigCollection.weaponCollection;
                    break;
                case GearSlot.armor:
                    tempConFigList = ScriptableObjectData.EquipmentConfigCollection.armorCollection;
                    break;
                case GearSlot.ring:
                    tempConFigList = ScriptableObjectData.EquipmentConfigCollection.ringCollection;
                    break;
                case GearSlot.charm:
                    tempConFigList = ScriptableObjectData.EquipmentConfigCollection.charmCollection;
                    break;
            }
            foreach (EquipmentConfig itemConfig in tempConFigList)
            {
                cacheConfig.Add(itemConfig.id, itemConfig);

            }
        }
    }
}
[System.Serializable]
public class ColorRarity
{
    public Rarity rarity;

    public Color color;
}