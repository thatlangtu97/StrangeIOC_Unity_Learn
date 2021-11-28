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
    /*
    private static readonly string ENEMY_PATH_CONFIG = FOLDER + "EnemyConfig";
    private static readonly string SKILL_PATH_CONFIG = FOLDER + "SkillConfig";
    private static readonly string HERO_DATA_CONFIG_PATH = FOLDER + "HeroConfig";
    private static readonly string TALENT_TREE_CONFIG_PATH = FOLDER + "TalentTreeConfig";
    private static readonly string IAP_DATA_CONGIF_PATH = FOLDER + "ProductIAPConfig";
    private static readonly string DROP_RATE_GACHA_PATH = FOLDER + "DropRateGachaConfig";
    private static readonly string ACCOUNT_LEVEL_PATH = FOLDER + "AccountLevel";
    private static readonly string IAP_SHOP_GOLD_PATH = FOLDER + "IAPGold";
    private static readonly string IAP_SHOP_GEM_PATH = FOLDER + "IAPGem";
    private static readonly string IAP_SHOP_STAMINA_PATH = FOLDER + "IAPStamina";
    private static readonly string DAILY_LOGIN_PATH = FOLDER + "DailyLogin";
    private static readonly string LIST_SPRITES_EQUIPMENT_OF_HERO = FOLDER + "DataSpriteEquipmentOfHero";
    private static readonly string ARENA_CONFIG_PATH = FOLDER + "ArenaConfig";
    private static readonly string SHOP_EQUIPMENT_LABYRIN_PATH = FOLDER + "DataShopLabyrin";
    private static readonly string IAP_ITEM = FOLDER + "IAPItem";
    private static readonly string ACHIEVEMENT_ITEM_CONFIG_PATH = FOLDER + "AchievementItemConfig";
    private static readonly string DROP_RATE_FORGE_PACK_PATH = FOLDER + "DropRateForgePackConfig";
    private static readonly string BOSSRAID_CONFIG_PATH = FOLDER + "BossConfig";
    private static readonly string COUNTADSITEM_CONFIG_PATH = FOLDER + "CountAdsItemConfig";
    private static readonly string IAP_SHOP_DEAL_PATH = FOLDER + "IAPShopDealConfig";
    private static readonly string IAP_GAME_ITEM_CONFIG_PATH = FOLDER + "IAPGameItemConfig";
    */
    /*
    private static EnemyConfig enemyConfig;
    public static EnemyConfig EnemyConfig
    {
        get
        {
            if (enemyConfig == null)
            {
                enemyConfig = Load<EnemyConfig>(ENEMY_PATH_CONFIG);
                Debug.Assert(enemyConfig != null, "gameplayConfig  null");
            }
            return enemyConfig;
        }
    }
    private static ArenaConfig arenaConfig;
    public static ArenaConfig ArenaConfig
    {
        get
        {
            if (arenaConfig == null)
            {
                arenaConfig = Load<ArenaConfig>(ARENA_CONFIG_PATH);
            }
            return arenaConfig;
        }
    }
    private static ArenaConfig bossConfig;
    public static ArenaConfig BossConfig
    {
        get
        {
            if (bossConfig == null)
            {
                bossConfig = Load<ArenaConfig>(BOSSRAID_CONFIG_PATH);
            }
            return bossConfig;
        }
    }

    private static DataSpriteEquipmentOfHero dataSpriteEquipmentOfHero;
    public static DataSpriteEquipmentOfHero DataSpriteEquipmentOfHero
    {
        get
        {
            if (dataSpriteEquipmentOfHero == null)
            {
                dataSpriteEquipmentOfHero = Load<DataSpriteEquipmentOfHero>(LIST_SPRITES_EQUIPMENT_OF_HERO);
            }
            return dataSpriteEquipmentOfHero;
        }
    }
    private static DaiLyLoginItemConfig dailyLoginConfig;

    public static DaiLyLoginItemConfig DailyLoginConfig
    {
        get
        {
            if (dailyLoginConfig == null)
            {
                dailyLoginConfig = Load<DaiLyLoginItemConfig>(DAILY_LOGIN_PATH);
            }
            return dailyLoginConfig;
        }
    }
    private static CountAdsItemConfig countAdsItemConfig;

    public static CountAdsItemConfig CountAdsItemConfig
    {
        get
        {
            if (countAdsItemConfig == null)
            {
                countAdsItemConfig = Load<CountAdsItemConfig>(COUNTADSITEM_CONFIG_PATH);
            }
            return countAdsItemConfig;
        }
    }
    private static IAPShopDealConfig iAPShopDealConfig;

    public static IAPShopDealConfig IAPShopDealConfig
    {
        get
        {
            if (iAPShopDealConfig == null)
            {
                iAPShopDealConfig = Load<IAPShopDealConfig>(IAP_SHOP_DEAL_PATH);
            }
            return iAPShopDealConfig;
        }
    }
    private static IAPGameItemConfig iAPGameItemConfig;

    public static IAPGameItemConfig IAPGameItemConfig
    {
        get
        {
            if (iAPGameItemConfig == null)
            {
                iAPGameItemConfig = Load<IAPGameItemConfig>(IAP_GAME_ITEM_CONFIG_PATH);
            }
            return iAPGameItemConfig;
        }
    }
    private static SkillConfig skillConfig;
    public static SkillConfig SkillConfig
    {
        get
        {
            if(skillConfig ==null)
            {
                skillConfig = Load<SkillConfig>(SKILL_PATH_CONFIG);
            }
            return skillConfig;
        }
    }

    private static HeroDataConfig heroDataConfig;
    public static HeroDataConfig HeroDataConfig
    {
        get
        {
            if (heroDataConfig == null)
            {
                heroDataConfig = Load<HeroDataConfig>(HERO_DATA_CONFIG_PATH);
            }
            return heroDataConfig;
        }
    }

    private static TalentTreeConfig talentTreeConfig;
    public static TalentTreeConfig TalentTreeConfig
    {
        get
        {
            if (talentTreeConfig == null)
            {
                talentTreeConfig = Load<TalentTreeConfig>(TALENT_TREE_CONFIG_PATH);
            }
            return talentTreeConfig;
        }
    }
    */
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
    /*
    private static ProductIAPConfig _productIAPConfig;

    public static ProductIAPConfig ProductIAPConfig
    {
        get
        {
            if (_productIAPConfig == null)
            {
                _productIAPConfig = Load<ProductIAPConfig>(IAP_DATA_CONGIF_PATH);
            }

            return _productIAPConfig;
        }
    }
    private static DropRateGachaConfig _dropRateGachaConfig;

    public static DropRateGachaConfig DropRateGachaConfig
    {
        get
        {
            if (_dropRateGachaConfig == null)
            {
                _dropRateGachaConfig = Load<DropRateGachaConfig>(DROP_RATE_GACHA_PATH);
            }

            return _dropRateGachaConfig;
        }
    }
    private static DropRateForgePackConfig _dropRateForgePackConfig;

    public static DropRateForgePackConfig DropRateForgePackConfig
    {
        get
        {
            if (_dropRateForgePackConfig == null)
            {
                _dropRateForgePackConfig = Load<DropRateForgePackConfig>(DROP_RATE_FORGE_PACK_PATH);
            }

            return _dropRateForgePackConfig;
        }
    }
    private static AccountLevel _accountLevel;
    public static AccountLevel AccountLevel
    {
        get
        {
            if (_accountLevel == null)
            {
                _accountLevel = Load<AccountLevel>(ACCOUNT_LEVEL_PATH);
            }

            return _accountLevel;
        }
    }
    private static IAPGold _iapGold;
    public static IAPGold IAPGold
    {
        get
        {
            if (_iapGold == null)
            {
                _iapGold = Load<IAPGold>(IAP_SHOP_GOLD_PATH);
            }

            return _iapGold;
        }
    }
    private static IAPGem _iapGem;
    public static IAPGem IAPGem
    {
        get
        {
            if (_iapGem == null)
            {
                _iapGem = Load<IAPGem>(IAP_SHOP_GEM_PATH);
            }

            return _iapGem;
        }
    }
    private static IAPItem _iapItem;
    public static IAPItem IAPItem
    {
        get
        {
            if (_iapItem == null)
            {
                _iapItem = Load<IAPItem>(IAP_ITEM);
            }

            return _iapItem;
        }
    }
    private static AchievementItemConfig achievementItemConfig;
    public static AchievementItemConfig AchievementItemConfig
    {
        get
        {
            if (achievementItemConfig == null)
            {
                achievementItemConfig = Load<AchievementItemConfig>(ACHIEVEMENT_ITEM_CONFIG_PATH);
            }
            return achievementItemConfig;
        }
    }
    private static IAPStamina _iapStamina;
    public static IAPStamina IAPStamina
    {
        get
        {
            if (_iapStamina == null)
            {
                _iapStamina = Load<IAPStamina>(IAP_SHOP_STAMINA_PATH);
            }

            return _iapStamina;
        }
    }
    private static DataItemEquipmentShopLabyrin _dataItemEquipmentShopLabyrin;
    public static DataItemEquipmentShopLabyrin DataItemEquipmentShopLabyrin
    {
        get
        {
            if (_dataItemEquipmentShopLabyrin == null)
            {
                _dataItemEquipmentShopLabyrin = Load<DataItemEquipmentShopLabyrin>(SHOP_EQUIPMENT_LABYRIN_PATH);
            }

            return _dataItemEquipmentShopLabyrin;
        }
    }
    */
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
