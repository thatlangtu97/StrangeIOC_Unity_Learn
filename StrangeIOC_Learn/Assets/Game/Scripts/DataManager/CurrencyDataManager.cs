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
        }
        catch (Exception e)
        {
            Debug.LogError("CurrencyData Error: " + e);
            currencyData = new CurrencyData();
            SaveData();
            return;
        }
        if (currencyData == null)
        {
            currencyData = new CurrencyData();
            SaveData();
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
    public void setupValueDefault() {
        currencyData.gold = 3000;
        currencyData.gem = 0;
        SaveData();
    }
}
[Serializable]
public class CurrencyData : DataObject
{
    public int gold;
    public int gem;
}


