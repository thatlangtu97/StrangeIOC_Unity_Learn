using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : IObjectDataManager
{
    const string HERO_DATA_FILE = "AllHeroData.binary";
    private AllHeroData allHeroData = new AllHeroData();
    public void DeleteData()
    {
        DataManager.DeleteData(HERO_DATA_FILE);
        allHeroData = new AllHeroData();
    }

    public void LoadData()
    {
        try
        {
            allHeroData = DataManager.LoadData<AllHeroData>(HERO_DATA_FILE);
            if (allHeroData == null)
            {
                allHeroData = new AllHeroData();
                InitAllHero();
                SaveData();
            }
            
        }
        catch (Exception e)
        {
            Debug.LogError("CurrencyData Error: " + e);
            allHeroData = new AllHeroData();
            InitAllHero();
            SaveData();
            return;
        }
    }

    public void SaveData()
    {
        DataManager.SaveData(allHeroData, HERO_DATA_FILE);
    }
    public void AddHero(int idConfig)
    {
        HeroData temp = new HeroData();
        temp.id = idConfig;
        temp.idConfig = idConfig;
        temp.unlock = true;
        temp.selected = false;
        temp.gearEquip = new Dictionary<GearSlot, int>();
        if (!allHeroData.HeroDataDic.ContainsKey(idConfig))
        {           
            allHeroData.HeroDataDic.Add(idConfig, temp);
        }
        SaveData();
    }
    public HeroData GetHeroById(int idHero)
    {
        return allHeroData.HeroDataDic[idHero];
    }
    public void InitAllHero()
    {
        foreach(HeroConfig temp in ScriptableObjectData.HeroConfigCollection.heroes)
        {
            AddHero((int)temp.id);
        }
    }
    public bool HasEquiped(GearSlot slot, int idHero)
    {
        return allHeroData.HeroDataDic[idHero].gearEquip.ContainsKey(slot);
    }
    public void EquipGear(GearSlot slot, int id , int idHero)
    {
        if (allHeroData.HeroDataDic[idHero].gearEquip.ContainsKey(slot))
        {
            allHeroData.HeroDataDic[idHero].gearEquip[slot] = id;
        }
        else
        {
            allHeroData.HeroDataDic[idHero].gearEquip.Add(slot, id);
        }
        SaveData();
    }
    public void UnEquipGear(GearSlot slot, int idHero)
    {
        if (allHeroData.HeroDataDic[idHero].gearEquip.ContainsKey(slot))
        {
            allHeroData.HeroDataDic[idHero].gearEquip.Remove(slot);
        }
        SaveData();
    }
    public int GetIdEquipmentEquiped(GearSlot slot, int idHero)
    {
        if (allHeroData.HeroDataDic[idHero].gearEquip.ContainsKey(slot))
        {
            return allHeroData.HeroDataDic[idHero].gearEquip[slot];
        }
        return -1;
    }
}
[Serializable]
public class AllHeroData : DataObject
{
    public Dictionary<int, HeroData> HeroDataDic = new Dictionary<int, HeroData>();
}
[Serializable]
public class HeroData
{
    public int id;
    public int idConfig;
    public bool unlock;
    public bool selected;
    public Dictionary<GearSlot, int> gearEquip = new Dictionary<GearSlot, int>();
}

