using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLogic 
{
    
    public static EquipmentConfig GetEquipmentConfigById(GearSlot gearSlot, int idConfig)
    {
        List<EquipmentConfig> tempConFigList = new List<EquipmentConfig>();
        switch (gearSlot) {
            case GearSlot.weapon:
                tempConFigList= ScriptableObjectData.EquipmentConfigCollection.weaponCollection;
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
            if (itemConfig.id == idConfig)
            {
                return itemConfig;
            }
        }
        return null;
    }
    public static List<int> ListIdConfigBySlot(GearSlot gearSlot)
    {
        List<int> tempList = new List<int>();
        List<EquipmentConfig> tempConFigList = new List<EquipmentConfig>();
        switch (gearSlot)
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
            tempList.Add(itemConfig.id);
        }
        return tempList;
    }
    public static List<int> ListIdGearSlot()
    {
        List<int> tempList = new List<int>();
        tempList.Add((int)GearSlot.weapon);
        tempList.Add((int)GearSlot.armor);
        tempList.Add((int)GearSlot.ring);
        tempList.Add((int)GearSlot.charm);
        return tempList;
    }
}
