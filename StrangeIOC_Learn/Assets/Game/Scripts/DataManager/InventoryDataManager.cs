using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : IObjectDataManager
{
    const string INVENTORY_DATA_FILE = "InventoryData.binary";
    private InventoryData inventoryData = new InventoryData();
    private const string identityKey = "IdentityKeyEquipment";
    public void DeleteData()
    {
        DataManager.DeleteData(INVENTORY_DATA_FILE);
        inventoryData = new InventoryData();
    }

    public void LoadData()
    {
        try
        {
            inventoryData = DataManager.LoadData<InventoryData>(INVENTORY_DATA_FILE);
            if (inventoryData == null)
            {
                inventoryData = new InventoryData();
                SaveData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("CurrencyData Error: " + e);
            inventoryData = new InventoryData();
            SaveData();
            return;
        }
    }


    public void SaveData()
    {
        DataManager.SaveData(inventoryData, INVENTORY_DATA_FILE);
    }
    public int GenerateIdentityEquipment()
    {
        int returnValue = PlayerPrefs.GetInt(identityKey, 0);
        returnValue++;
        PlayerPrefs.SetInt(identityKey, returnValue);
        PlayerPrefs.Save();
        return returnValue;
    }
    //public void AddEquipments(List<int> idConfigs, List<Rarity> raritys,List<GearSlot> gearSlots,List<int> idOfHeros)
    //{
    //    for (int i=0;i< idConfigs.Count; i++)
    //    {
    //        EquipmentData newEquipment = new EquipmentData();
    //        newEquipment.id = GenerateIdentityEquipment();
    //        newEquipment.idConfig = idConfigs[i];
    //        newEquipment.gearSlot = gearSlots[i];
    //        newEquipment.rarity = raritys[i];
    //        newEquipment.level = 1;
    //        newEquipment.idOfHero = idOfHeros[i];
    //        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlots[i]))
    //        {
    //            inventoryData.EquipmentDicBySlot[gearSlots[i]].Add(newEquipment);
    //        }
    //        else
    //        {
    //            List<EquipmentData> tempList = new List<EquipmentData>();
    //            tempList.Add(newEquipment);
    //            inventoryData.EquipmentDicBySlot.Add(gearSlots[i], tempList);
    //        }
    //    }
    //    SaveData();
    //}
    //public void AddEquipment(int idConfig,Rarity rarity,GearSlot gearSlot,int idOfHero)
    //{
    //    EquipmentData newEquipment = new EquipmentData();
    //    newEquipment.id = GenerateIdentityEquipment();
    //    newEquipment.idConfig = idConfig;
    //    newEquipment.gearSlot = gearSlot;
    //    newEquipment.rarity = rarity;
    //    newEquipment.level = 1;
    //    newEquipment.idOfHero = idOfHero;
    //    if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
    //    {
    //        inventoryData.EquipmentDicBySlot[gearSlot].Add(newEquipment);
    //    }
    //    else
    //    {
    //        List<EquipmentData> tempList = new List<EquipmentData>();
    //        tempList.Add(newEquipment);
    //        inventoryData.EquipmentDicBySlot.Add(gearSlot, tempList);
    //    }
    //    SaveData();
    //}
    public List<EquipmentData> GetAllEquipmentBySlot(GearSlot gearSlot)
    {
        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
        {
            return inventoryData.EquipmentDicBySlot[gearSlot];
        }
        return new List<EquipmentData>();
    }
    public void CraftItem(EquipmentData equipmentData)
    {
        List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[equipmentData.gearSlot];
        foreach (EquipmentData tempData in tempList)
        {
            if (tempData.id == equipmentData.id)
            {
                tempData.rarity = (Rarity)Mathf.Clamp((int)tempData.rarity + 1, (int)Rarity.common, (int)Rarity.heroic);
                SaveData();
                return;
            }
        }
    }
    //public void CraftItem(GearSlot gearSlot, int idItem)
    //{
    //    List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[gearSlot];
    //    foreach(EquipmentData equipmentData in tempList)
    //    {
    //        if(equipmentData.id == idItem)
    //        {
    //            equipmentData.rarity = (Rarity)Mathf.Clamp( (int)equipmentData.rarity + 1,(int)Rarity.common,(int)Rarity.heroic);
    //            SaveData();
    //            return;
    //        }
    //    }
    //}
    public void RemoveItem(EquipmentData equipmentData)
    {
        List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[equipmentData.gearSlot];
        foreach (EquipmentData tempData in tempList)
        {
            if (tempData.id == equipmentData.id)
            {
                inventoryData.EquipmentDicBySlot[tempData.gearSlot].Remove(tempData);
                SaveData();
                return;
            }
        }
    }
    //public void RemoveItem(GearSlot gearSlot, int idItem)
    //{
    //    List<EquipmentData> tempList = inventoryData.EquipmentDicBySlot[gearSlot];
    //    foreach (EquipmentData equipmentData in tempList)
    //    {
    //        if (equipmentData.id == idItem)
    //        {
    //            inventoryData.EquipmentDicBySlot[gearSlot].Remove(equipmentData);
    //            SaveData();
    //            return;
    //        }
    //    }
    //}
    public void AddItems(List<EquipmentData> equipmentDatas)
    {
        for (int i = 0; i < equipmentDatas.Count; i++)
        {
            EquipmentData newEquipment = equipmentDatas[i];
            newEquipment.id = GenerateIdentityEquipment();
            if (inventoryData.EquipmentDicBySlot.ContainsKey(newEquipment.gearSlot))
            {
                inventoryData.EquipmentDicBySlot[newEquipment.gearSlot].Add(newEquipment);
            }
            else
            {
                List<EquipmentData> tempList = new List<EquipmentData>();
                tempList.Add(newEquipment);
                inventoryData.EquipmentDicBySlot.Add(newEquipment.gearSlot, tempList);
            }
        }
        SaveData();
    }
    public void AddItem(EquipmentData equipmentData)
    {
        equipmentData.id = GenerateIdentityEquipment();
        if (inventoryData.EquipmentDicBySlot.ContainsKey(equipmentData.gearSlot))
        {
            inventoryData.EquipmentDicBySlot[equipmentData.gearSlot].Add(equipmentData);
        }
        else
        {
            List<EquipmentData> tempList = new List<EquipmentData>();
            tempList.Add(equipmentData);
            inventoryData.EquipmentDicBySlot.Add(equipmentData.gearSlot, tempList);
        }
        SaveData();
    }
}
[Serializable]
public class InventoryData : DataObject
{
    public Dictionary<GearSlot, List<EquipmentData>> EquipmentDicBySlot = new Dictionary<GearSlot, List<EquipmentData>>();
}
[Serializable]
public class EquipmentData
{
    public int id;
    public int idConfig;
    public int idOfHero;
    public GearSlot gearSlot;
    public Rarity rarity;
    public int level;
    public StatData mainStatData;
}
[Serializable]
public class StatData
{
    public StatType statType;
    public float baseValue;
}

