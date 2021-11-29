using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : IObjectDataManager
{
    const string INVENTORY_DATA_FILE = "InventoryData.binary";
    private InventoryData inventoryData = new InventoryData();
    private const string identityKey = "IdentityKey";
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
    public void AddEquipment(int idConfig,Rarity rarity,GearSlot gearSlot)
    {
        EquipmentData newEquipment = new EquipmentData();
        newEquipment.id = GenerateIdentityEquipment();
        newEquipment.idConfig = idConfig;
        newEquipment.gearSlot = gearSlot;
        newEquipment.rarity = rarity;
        newEquipment.level = 1;
        if (inventoryData.EquipmentDicBySlot.ContainsKey((int)gearSlot))
        {
            inventoryData.EquipmentDicBySlot[(int)gearSlot].Add(newEquipment);
        }
        else
        {
            List<EquipmentData> tempList = new List<EquipmentData>();
            tempList.Add(newEquipment);
            inventoryData.EquipmentDicBySlot.Add((int)gearSlot, tempList);
        }
        SaveData();
    }
    public List<EquipmentData> GetAllEquipmentBySlot(int gearSlot)
    {
        if (inventoryData.EquipmentDicBySlot.ContainsKey(gearSlot))
        {
            return inventoryData.EquipmentDicBySlot[(int)gearSlot];
        }
        return new List<EquipmentData>();
    }
}
[Serializable]
public class InventoryData : DataObject
{
    public Dictionary<int, List<EquipmentData>> EquipmentDicBySlot = new Dictionary<int, List<EquipmentData>>();
}
[Serializable]
public class EquipmentData
{
    public int id;
    public int idConfig;
    public GearSlot gearSlot;
    public Rarity rarity;
    public int level;

}
