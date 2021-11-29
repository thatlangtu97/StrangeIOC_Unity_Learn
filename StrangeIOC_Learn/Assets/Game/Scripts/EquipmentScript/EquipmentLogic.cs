using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLogic 
{
    public static EquipmentConfig GetEquipmentConfigById(GearSlot gearSlot, int idConfig)
    {
        switch (gearSlot) {
            case GearSlot.weapon:
                return ScriptableObjectData.EquipmentConfigCollection.weaponCollection[idConfig];
            case GearSlot.armor:
                return ScriptableObjectData.EquipmentConfigCollection.armorCollection[idConfig];
            case GearSlot.ring:
                return ScriptableObjectData.EquipmentConfigCollection.ringCollection[idConfig];
        }
        return null;
    }
}
