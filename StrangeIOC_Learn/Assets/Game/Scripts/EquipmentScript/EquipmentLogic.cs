using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EquipmentLogic 
{
    static Dictionary<int, EquipmentConfig> cacheConfig = new Dictionary<int, EquipmentConfig>();
    static List<EquipmentData> equipmentOfCraft = new List<EquipmentData>();
    public static void Cache()
    {
        //EquipmentConfig wp_cf = ScriptableObjectData.EquipmentConfigCollection.weaponCollection[0];
        //EquipmentConfig ar_cf = ScriptableObjectData.EquipmentConfigCollection.armorCollection[0];
        //EquipmentConfig rg_cf = ScriptableObjectData.EquipmentConfigCollection.ringCollection[0];
        //EquipmentConfig ch_cf = ScriptableObjectData.EquipmentConfigCollection.charmCollection[0];

        //foreach(EquipmentConfig temp in ScriptableObjectData.EquipmentConfigCollection.weaponCollection)
        //{
        //    temp.mainStatConfig.attType = wp_cf.mainStatConfig.attType;
        //    temp.mainStatConfig.valueMinRandom = wp_cf.mainStatConfig.valueMinRandom;
        //    temp.mainStatConfig.valueMaxRandom = wp_cf.mainStatConfig.valueMaxRandom;
        //}
        //foreach (EquipmentConfig temp in ScriptableObjectData.EquipmentConfigCollection.armorCollection)
        //{
        //    temp.mainStatConfig.attType = ar_cf.mainStatConfig.attType;
        //    temp.mainStatConfig.valueMinRandom = ar_cf.mainStatConfig.valueMinRandom;
        //    temp.mainStatConfig.valueMaxRandom = ar_cf.mainStatConfig.valueMaxRandom;
        //}
        //foreach (EquipmentConfig temp in ScriptableObjectData.EquipmentConfigCollection.ringCollection)
        //{
        //    temp.mainStatConfig.attType = rg_cf.mainStatConfig.attType;
        //    temp.mainStatConfig.valueMinRandom = rg_cf.mainStatConfig.valueMinRandom;
        //    temp.mainStatConfig.valueMaxRandom = rg_cf.mainStatConfig.valueMaxRandom;
        //}
        //foreach (EquipmentConfig temp in ScriptableObjectData.EquipmentConfigCollection.charmCollection)
        //{
        //    temp.mainStatConfig.attType = ch_cf.mainStatConfig.attType;
        //    temp.mainStatConfig.valueMinRandom = ch_cf.mainStatConfig.valueMinRandom;
        //    temp.mainStatConfig.valueMaxRandom = ch_cf.mainStatConfig.valueMaxRandom;
        //}

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
    public static EquipmentData CloneEquipmentData(int idConfig, Rarity rarity, GearSlot gearSlot, int idOfHero , int level)
    {
        EquipmentData newEquipment = new EquipmentData();
        newEquipment.idConfig = idConfig;
        newEquipment.gearSlot = gearSlot;
        newEquipment.rarity = rarity;
        newEquipment.idOfHero = idOfHero;
        newEquipment.level = level;
        return newEquipment;
    }
    public static StatData RandomStatEquipment(StatConfig config,Rarity rarity)
    {
        StatData newStat = new StatData();
        newStat.statType = config.attType;
        newStat.baseValue = UnityEngine.Random.Range(config.valueMinRandom[(int)rarity],config.valueMaxRandom[(int)rarity]);
        return newStat;
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
        Rarity rarityCraft= Rarity.common;
        GearSlot gearSlotCraft = GearSlot.weapon;
        foreach (EquipmentData equipmentData in equipmentOfCraft)
        {
            breakID.Add(equipmentData.id);
            rarityCraft = equipmentData.rarity;
            gearSlotCraft = equipmentData.gearSlot;
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
            if (breakID.Count != 0)
            {
                
               if (!breakID.Contains(tempItem.id) && tempItem.rarity == rarityCraft && tempItem.gearSlot == gearSlotCraft)
                        newlist.Add(tempItem);
            }
            else
            {
                if (tempItem.id != idEquiped && tempItem.idOfHero == hero)
                {
                    newlist.Add(tempItem);
                }
            }

        }
        
        return newlist;
    }
    
    public static List<EquipmentData> GetEquipmentInventory(GearSlot gearSlot, int hero)
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
            if (!breakID.Contains(tempItem.id))
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
    public static bool CanCraft()
    {
        if (equipmentOfCraft.Count == 3)
        {
            return true;
        }
        return false;
    }
    public static EquipmentData getMainEquipmentCraft()
    {
        return equipmentOfCraft[0];
    }
    public static EquipmentData CraftItem()
    {
        if (equipmentOfCraft.Count == 3)
        {
            EquipmentData mainEquipmentCraft = equipmentOfCraft[0];
            int idEquiped = DataManager.Instance.HeroDataManager.GetIdEquipmentEquiped(mainEquipmentCraft.gearSlot, mainEquipmentCraft.idOfHero);

            foreach (EquipmentData tempItem in equipmentOfCraft)
            {
                if (tempItem.id != mainEquipmentCraft.id)
                {
                    if (idEquiped == tempItem.id)
                    {
                        UnEquipGear(tempItem, tempItem.idOfHero);

                    }
                    DataManager.Instance.InventoryDataManager.RemoveItem(tempItem);
                }
            }
            DataManager.Instance.InventoryDataManager.CraftItem(mainEquipmentCraft);
            equipmentOfCraft.Clear();
            return mainEquipmentCraft;
        }
        return null;
    }
    
    public static string StatValueToString(StatType statType,float value)
    {
        string prefix = "";
        if (value > 0)
        {
            prefix ="";
        }
        switch (statType)
        {
            case StatType.Attack:
            case StatType.Health:
            case StatType.Defense:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N0}", value); 
            default:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N1}", value*100f) + " %";
        }
    }
    
    public static string StatTypeToString(StatType statType)
    {
        switch (statType)
        {
            case StatType.Attack:
                return "rst_attack";
            case StatType.Defense:
                return "rst_defense";
            case StatType.Health:
                return "rst_health_point";
            case StatType.CritChance:
                return "rst_crit_chance";
            case StatType.CritDamage:
                return "rst_crit_damage";
            case StatType.SkillCritChance:
                return "rst_skill_crit_chance";
            case StatType.SkillCritDamage:
                return "rst_skill_crit_damage";
            case StatType.BossDMGBonus:
                return "rst_boss_dmg_bonus";
            case StatType.PvPDMGBonus:
                return "rst_pvp_dmg_bonus";
            case StatType.EXPBonus:
                return "rst_exp_bonus";
            case StatType.GoldBonus:
                return "rst_gold_bonus";
            case StatType.GemBonus:
                return "rst_gem_bonus";
            case StatType.EquipmentDropRate:
                return "rst_equipment_droprate";
            case StatType.PhysicalResistance:
                return "rst_physical_resistance";
            case StatType.FireResistance:
                return "rst_fire_resistance";
            case StatType.IceResistance:
                return "rst_ice_resistance";
            case StatType.LightingResistance:
                return "rst_lighting_resistance";
            case StatType.ProjectileResistance:
                return "rst_projectile_resistance";
            case StatType.SkillCooldownReduction:
                return "rst_skill_cooldown_reduction";
            case StatType.AttackScale:
                return "rst_attack_scale";
            case StatType.DefenseScale:
                return "rst_defense_scale";
            case StatType.HealthPointScale:
                return "rst_health_point_scale";

        }
        return "Unknowns StatType";
    }
}
