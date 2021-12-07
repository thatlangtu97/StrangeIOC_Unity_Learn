using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EquipmentConfig", menuName = "Data/EquipmentConfig")]
public class EquipmentConfig : ScriptableObject
{
    [Header("idHero/Gearslot/id")]
    public int id;
    public Sprite GearIcon, GearFull;
    public GearSlot GearSlot;
    public string gearName;
    public int idOfHero;
    public float[] price;
    [Header("Main Stat")]
    public StatConfig mainStatConfig;

}
public enum GearSlot
{
    weapon=0,
    armor=1,
    ring=2,
    charm=3,
}
public enum Rarity
{
    common=0,
    uncommon=1,
    rare=2,
    epic=3,
    heroic=4,
}
[Serializable]
public class StatConfig
{
    public StatType attType;
    public float[] valueMinRandom,valueMaxRandom;

}
public enum StatType
{
    //INT
    Attack,
    Defense,
    Health,

    //FLOAT
    CritChance,
    CritDamage,
    SkillCritChance,
    SkillCritDamage,
    BossDMGBonus,
    PvPDMGBonus,
    EXPBonus,
    GoldBonus,
    GemBonus,
    EquipmentDropRate,
    PhysicalResistance,
    FireResistance,
    IceResistance,
    LightingResistance,
    ProjectileResistance,
    SkillCooldownReduction,
    AttackScale,
    DefenseScale,
    HealthPointScale,
}
public enum TypeRequires
{
    gold,
}
