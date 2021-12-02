using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLogic 
{
    static Dictionary<int, EquipmentConfig> cacheConfig = new Dictionary<int, EquipmentConfig>();
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
    public static EquipmentConfig GetEquipmentConfigById(int idConfig)
    {
        return cacheConfig[idConfig];
    }
    public static EquipmentData CloneEquipmentData (EquipmentData dataBase)
    {
        EquipmentData dataClone = new EquipmentData();
        dataClone.id = dataBase.id;
        dataClone.idConfig = dataBase.idConfig;
        dataClone.gearSlot = dataBase.gearSlot;
        dataClone.rarity = dataBase.rarity;
        dataClone.level = dataBase.level;
        dataClone.idOfHero = dataBase.idOfHero;
        return dataClone;
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
        int idEquiped = DataManager.Instance.HeroDataManager.GetIdEquipmentEquiped(gearSlot, hero);
        List<EquipmentData> newlist = new List<EquipmentData>();
        foreach (EquipmentData tempItem in templist)
        {
            if(tempItem.id != idEquiped)
            {
                newlist.Add(tempItem);
            }
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
}
