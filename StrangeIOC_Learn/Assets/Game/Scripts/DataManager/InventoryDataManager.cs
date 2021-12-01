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
    public void AddEquipment(int idConfig,Rarity rarity,GearSlot gearSlot,int idOfHero)
    {
        EquipmentData newEquipment = new EquipmentData();
        newEquipment.id = GenerateIdentityEquipment();
        newEquipment.idConfig = idConfig;
        newEquipment.gearSlot = gearSlot;
        newEquipment.rarity = rarity;
        newEquipment.level = 1;
        newEquipment.idOfHero = idOfHero;
        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
        {
            inventoryData.EquipmentDicBySlot[gearSlot].Add(newEquipment);
        }
        else
        {
            List<EquipmentData> tempList = new List<EquipmentData>();
            tempList.Add(newEquipment);
            inventoryData.EquipmentDicBySlot.Add(gearSlot, tempList);
        }
        SaveData();
    }
    public List<EquipmentData> GetAllEquipmentBySlot(GearSlot gearSlot)
    {
        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
        {
            return inventoryData.EquipmentDicBySlot[gearSlot];
        }
        return new List<EquipmentData>();
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

}
