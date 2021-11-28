using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CurrencyDataManager : IObjectDataManager
{
    const string CURRENCY_DATA_FILE = "CurencyData.binary";
    private CurrencyData currencyData = new CurrencyData();
    public void DeleteData()
    {
        DataManager.DeleteData(CURRENCY_DATA_FILE);
        currencyData = new CurrencyData();
    }

    public void LoadData()
    {
        try
        {
            currencyData = DataManager.LoadData<CurrencyData>(CURRENCY_DATA_FILE);
            if (currencyData == null)
            {
                currencyData = new CurrencyData();
                setupValueDefault();
                SaveData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("CurrencyData Error: " + e);
            currencyData = new CurrencyData();
            setupValueDefault();
            SaveData();
            return;
        }

    }

    public void SaveData()
    {
        DataManager.SaveData(currencyData, CURRENCY_DATA_FILE);
    }


    public int gold
    {
        get { return currencyData.gold; }
        set
        {
            currencyData.gold = value;
            SaveData();
        }
    }
    public int gem
    {
        get { return currencyData.gem; }
        set
        {
            currencyData.gem = value;
            SaveData();
        }
    }
    public int stamina
    {
        get { return currencyData.stamina; }
        set
        {
            currencyData.stamina = value;
            SaveData();
        }
    }
    public int maxStamina
    {
        get { return currencyData.maxStamina; }
        set
        {
            currencyData.maxStamina = value;
            SaveData();
        }
    }
    public int UpGold(int count, bool withEffect)
    {
        int initValue = currencyData.gold;
        int currentValue = initValue;
        currencyData.gold += count;
        int desiredValue = currencyData.gold;
        SaveData();
        return currencyData.gold;
    }
    public int DownGold(int count, bool withEffect)
    {
        int initValue = currencyData.gold;
        int currentValue = initValue;

        currencyData.gold -= count;
        if (currencyData.gold <= 0)
            currencyData.gold = 0;

        int desiredValue = currencyData.gold;
        SaveData();
        return currencyData.gold;
    }
    public int UpGem(int count, bool withEffect)
    {
        int initValue = currencyData.gem;
        int currentValue = initValue;
        currencyData.gem += count;
        int desiredValue = currencyData.gem;

        SaveData();
        return currencyData.gem;
    }
    public int DownGem(int count, bool withEffect)
    {
        int initValue = currencyData.gem;
        int currentValue = initValue;
        currencyData.gem -= count;
        if (currencyData.gem<= 0)
            currencyData.gem = 0;
        int desiredValue = currencyData.gem;

        SaveData();
        return currencyData.gem;
    }
    public int UpStamina(int count, bool withEffect)
    {
        int initValue = currencyData.stamina;
        int currentValue = initValue;
        currencyData.stamina += count;
        int desiredValue = currencyData.stamina;

        SaveData();
        return currencyData.stamina;
    }
    public int DownStamina(int count, bool withEffect)
    {
        int initValue = currencyData.stamina;
        int currentValue = initValue;
        currencyData.stamina -= count;
        int desiredValue = currencyData.stamina;
        if (currencyData.stamina <= 0)
            currencyData.stamina = 0;
        SaveData();
        return currencyData.stamina;
    }
    public int GetCountOpenGachaById(int id)
    {
        if (!currencyData.gachaOpened.ContainsKey(id))
        {
            currencyData.gachaOpened.Add(id, 0);
            SaveData();
        }
        return currencyData.gachaOpened[id];
    }
    public void UpCountOpenGachaById(int id ,int count)
    {
        if (!currencyData.gachaOpened.ContainsKey(id))
        {
            currencyData.gachaOpened.Add(id, count);
            
        }
        else
        {
            currencyData.gachaOpened[id] += count;
        }
        SaveData();
    }
    public void setupValueDefault() {
        currencyData.gold = 3000;
        currencyData.gem = 100;
        currencyData.stamina = 50;
        currencyData.maxStamina = 50;
        SaveData();
    }
}
[Serializable]
public class CurrencyData : DataObject
{
    public int gold;
    public int gem;
    public int stamina;
    public int maxStamina;
    public Dictionary<int, int> gachaOpened = new Dictionary<int, int>();
}
public enum CurrencyType
{
    gold,
    gem,
    stamina,
}


