using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaLogic
{
    #region PUBLIC METHOC
    public static List<dataGachaRandom> GetListGachaRandom(int id, int count)
    {
        List<dataGachaRandom> tempList = new List<dataGachaRandom>();

        for (int i = 0; i < count; i++)
        {
            tempList.Add(GetGachaRandom(id));
        }
        return tempList;
    }
    public static dataGachaRandom GetGachaRandom(int idGacha){
        dataGachaRandom tempValueRandom = new dataGachaRandom();
        Gacha currentGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        //data select item
        Rarity rarity = Rarity.common;
        
        List<int> listSlotRamdom = new List<int>
        {
            /*Weapon*/   0,
            /*Armor*/    1,
            /*Ring*/     2,
        };
        List<int> listIdHeroRandom = new List<int>
        {
            0,
        };
        List<int> listIdConfig = new List<int>
        {
            1,
            2,
            3,
            4,
            5,
        };
        int gearslot = randomSlot(listSlotRamdom);
        int idConfig = randomSlot(listIdConfig);
        int idOfHero = randomSlot(listIdHeroRandom);

        //
        int maxrandom = 100;
        int randomQuality = UnityEngine.Random.Range(0, maxrandom);

        int initdrop = 0;
        int desireddrop = 0;
        for (int i = 0; i < currentGacha.DropList.Count; i++)
        {
            desireddrop += currentGacha.DropList[i].rate;
            if (randomQuality >= initdrop && randomQuality <= desireddrop)
            {
                rarity = currentGacha.DropList[i].rarity;
                break;
            }
            else
            {
                initdrop += desireddrop;
            }
        }
        tempValueRandom.GearSlot = (GearSlot)gearslot;
        tempValueRandom.idConfig = idConfig;
        tempValueRandom.Rarity = rarity;
        tempValueRandom.idOfHero = idOfHero;
        //Debug.Log($"GearSlot {tempValueRandom.GearSlot} idConfig {tempValueRandom.idConfig} Rarity {tempValueRandom.Rarity} idOfHero {tempValueRandom.idOfHero}");
        return tempValueRandom;

}

    #endregion
    static int randomSlot(List<int> listSlot)
    {
        int seed = UnityEngine.Random.Range(1000, 1000000);
        UnityEngine.Random.InitState((int)Time.time * seed);
        int random = UnityEngine.Random.Range(0, listSlot.Count);
        return listSlot[random];
    }
    public static EquipmentConfig getEquipmentConfig(GearSlot gearSlot , int idConfig, int idOfHero)
    {
        List<EquipmentConfig> listConfig = new List<EquipmentConfig>();
        switch (gearSlot)
        {
            case GearSlot.weapon:
                listConfig = ScriptableObjectData.EquipmentConfigCollection.weaponCollection;
                break;
            case GearSlot.armor:
                listConfig = ScriptableObjectData.EquipmentConfigCollection.armorCollection;
                break;
            case GearSlot.ring:
                listConfig = ScriptableObjectData.EquipmentConfigCollection.ringCollection;
                break;
        }
        foreach (EquipmentConfig temp in listConfig)
        {
            if(temp.id == idConfig && temp.idOfHero == idOfHero)
            {
                return temp;
            }
        }
        return null;
    }
    public static Color GetColorByRarity(Rarity rarity)
    {
        foreach(ColorRarity tempColor in ScriptableObjectData.EquipmentConfigCollection.colorRarity)
        {
            if(tempColor.rarity == rarity)
            {
                return tempColor.color;
            }
        }
        return Color.white;
        
    }
    
}
