using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLogic 
{
    static Dictionary<int, EquipmentConfig> cacheConfig = new Dictionary<int, EquipmentConfig>();
    static List<EquipmentData> equipmentOfCraft = new List<EquipmentData>();
    public static void Cache()
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
    public static Color GetColorByRarity(Rarity rarity)
    {
        foreach (ColorRarity tempColor in ScriptableObjectData.EquipmentConfigCollection.colorRarity)
        {
            if (tempColor.rarity == rarity)
            {
                return tempColor.color;
            }
        }
        return Color.white;

    }
    public static EquipmentConfig GetEquipmentConfigById(int idConfig)
    {
        return cacheConfig[idConfig];
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
    public static EquipmentData GetEquipment(GearSlot gearSlot, int id)
    {
        List<EquipmentData> tempConFigList = DataManager.Instance.InventoryDataManager.GetAllEquipmentBySlot(gearSlot);
        foreach (EquipmentData data in tempConFigList)
        {
            if(id == data.id)
            {
                return data;
            }
        }
        return null;
    }
    public static List<EquipmentData> GetEquipmentOfHero(int idHero)
    {
        List<EquipmentData> tempConFigList = new List<EquipmentData>();
        HeroData data = DataManager.Instance.HeroDataManager.GetHeroById(idHero);
        foreach(GearSlot gearSlot in data.gearEquip.Keys)
        {
            tempConFigList.Add(GetEquipment(gearSlot, data.gearEquip[gearSlot]));
        }
        return tempConFigList;
    }
    public static List<EquipmentData> GetAllEquipmentBySlotOfHeroNotEquiped(GearSlot gearSlot, int hero)
    {

        List<EquipmentData> templist = DataManager.Instance.InventoryDataManager.GetAllEquipmentBySlot(gearSlot);
        List<int> breakID = new List<int>();

        int idEquiped = DataManager.Instance.HeroDataManager.GetIdEquipmentEquiped(gearSlot, hero);
        foreach (EquipmentData equipmentData in equipmentOfCraft)
        {
            breakID.Add(equipmentData.id);
        }





        List<EquipmentData> newlist = new List<EquipmentData>();
        foreach (EquipmentData tempItem in templist)
        {
            /*
            if(tempItem.id != idEquiped)
            {
                newlist.Add(tempItem);
            }
            */
            if(!breakID.Contains(tempItem.id))
                newlist.Add(tempItem);
        }
        return newlist;
    }
    public static void EquipGear(EquipmentData data ,int hero)
    {
        DataManager.Instance.HeroDataManager.EquipGear(data.gearSlot, data.id, (int)hero);
    }
    public static void UnEquipGear(EquipmentData data, int hero)
    {
        DataManager.Instance.HeroDataManager.UnEquipGear(data.gearSlot, (int)hero);
    }
    public static List<EquipmentData> GetEquipmentOfCraft()
    {
        return equipmentOfCraft;
    }
    public static void AddEquipmentToCraft(EquipmentData eqiupmentData)
    {
        equipmentOfCraft.Add(eqiupmentData);
    }
    public static void RemoveEquipmentToCraft(EquipmentData eqiupmentData)
    {
        equipmentOfCraft.Remove(eqiupmentData);
    }
    public static void RemoveAllEquipmentToCraft()
    {
        equipmentOfCraft.Clear();
    }
}
