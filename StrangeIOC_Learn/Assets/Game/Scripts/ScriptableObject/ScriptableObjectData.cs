using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ScriptableObjectData
{
    private static readonly string FOLDER = "ScriptableObjectData/";
    private static readonly string EQUIPMENT_PATH_CONFIG = FOLDER + "EquipmentConfigCollection";
    private static readonly string GACHA_PATH_CONFIG = FOLDER + "GachaConfigCollection";
    private static readonly string HEOR_PATH_CONFIG = FOLDER + "HeroConfigCollection";
   

    private static EquipmentConfigCollection _equipmentConfigCollection;
    public static EquipmentConfigCollection EquipmentConfigCollection
    {
        get
        {
            if (_equipmentConfigCollection == null)
            {
                _equipmentConfigCollection = Load<EquipmentConfigCollection>(EQUIPMENT_PATH_CONFIG);
            }
            return _equipmentConfigCollection;
        }
    }
    private static GachaConfigCollection gachaConfigCollection;
    public static GachaConfigCollection GachaConfigCollection
    {
        get
        {
            if (gachaConfigCollection == null)
            {
                gachaConfigCollection = Load<GachaConfigCollection>(GACHA_PATH_CONFIG);
            }
            return gachaConfigCollection;
        }
    }

    private static HeroConfigCollection heroConfigCollection;
    public static HeroConfigCollection HeroConfigCollection
    {
        get
        {
            if (heroConfigCollection == null)
            {
                heroConfigCollection = Load<HeroConfigCollection>(HEOR_PATH_CONFIG);
            }
            return heroConfigCollection;
        }
    }
    public static T Load<T>(string path) where T : ScriptableObject
    {
        return Resources.Load<T>(path);
    }
    public static string AttributeTypeToString(StatType atributeType)
    {
        switch (atributeType)
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
        return "Unknowns Attribute";
    }
    /*
    public static string AttributeValueToString(AtributeEquipment attributeBonus,RARITY rarity)
    {
        string prefix = "";
        if (attributeBonus.value > 0)
        {
            prefix ="";
        }
        switch (attributeBonus.attType)
        {
            case AtributeType.Attack:
            case AtributeType.Health:
            case AtributeType.Defense:
            //case AtributeType.moveSpeed:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N0}", attributeBonus.value); 
            default:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N1}", attributeBonus.value*100f) + " %";
        }
    }
    public static string AttributeConfigValueToString(AtributeConfig attributeBonus, RARITY rarity)
    {
        string prefix = "";
        //if (attributeBonus.value > 0)
        //{
        //    prefix = "";
        //}
        switch (attributeBonus.attType)
        {
            case AtributeType.Attack:
            case AtributeType.HealthPoint:
            case AtributeType.Defense:
                //case AtributeType.moveSpeed:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N0}", attributeBonus.valueMinRandom[(int)rarity]) +"-"+ string.Format(CultureInfo.InvariantCulture, "{0:N0}", attributeBonus.valueMaxRandom[(int)rarity]);
            default:
                return prefix + string.Format(CultureInfo.InvariantCulture, "{0:N1}", attributeBonus.valueMinRandom[(int)rarity] * 100f) + " %" + "-" + string.Format(CultureInfo.InvariantCulture, "{0:N1}", attributeBonus.valueMaxRandom[(int)rarity] * 100f) + " %";
        }
    }
*/
    public static string RarityToString(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.common:
                return "Common";
            case Rarity.uncommon:
                return "UnCommon";
            case Rarity.rare:
                return "Rare";
            case Rarity.epic:
                return "Epic";
            case Rarity.heroic:
                return "Heroic";
        }
        return "Unknowns rarity";
    }
}
