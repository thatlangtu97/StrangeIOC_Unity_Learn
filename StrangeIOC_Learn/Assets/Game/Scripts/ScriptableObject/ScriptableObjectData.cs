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
    public static string AttributeTypeToString(AtributeType atributeType)
    {
        switch (atributeType)
        {
            case AtributeType.Attack:
                return "rst_attack";
            case AtributeType.Defense:
                return "rst_defense";
            case AtributeType.Health:
                return "rst_health_point";
            case AtributeType.CritChance:
                return "rst_crit_chance";
            case AtributeType.CritDamage:
                return "rst_crit_damage";
            case AtributeType.SkillCritChance:
                return "rst_skill_crit_chance";
            case AtributeType.SkillCritDamage:
                return "rst_skill_crit_damage";
            case AtributeType.BossDMGBonus:
                return "rst_boss_dmg_bonus";
            case AtributeType.PvPDMGBonus:
                return "rst_pvp_dmg_bonus";
            case AtributeType.EXPBonus:
                return "rst_exp_bonus";
            case AtributeType.GoldBonus:
                return "rst_gold_bonus";
            case AtributeType.GemBonus:
                return "rst_gem_bonus";
            case AtributeType.EquipmentDropRate:
                return "rst_equipment_droprate";
            case AtributeType.PhysicalResistance:
                return "rst_physical_resistance";
            case AtributeType.FireResistance:
                return "rst_fire_resistance";
            case AtributeType.IceResistance:
                return "rst_ice_resistance";
            case AtributeType.LightingResistance:
                return "rst_lighting_resistance";
            case AtributeType.ProjectileResistance:
                return "rst_projectile_resistance";
            case AtributeType.SkillCooldownReduction:
                return "rst_skill_cooldown_reduction";
            case AtributeType.AttackScale:
                return "rst_attack_scale";
            case AtributeType.DefenseScale:
                return "rst_defense_scale";
            case AtributeType.HealthPointScale:
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
