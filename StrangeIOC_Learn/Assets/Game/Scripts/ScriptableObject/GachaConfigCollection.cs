using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GachaConfigCollection", menuName = "Data/GachaConfigCollection")]
public class GachaConfigCollection : ScriptableObject
{
    public List<Gacha> GachaList;
    public Gacha GetGachaById(int id)
    {
        foreach(Gacha temp in GachaList)
        {
            if(id == temp.id)
            {
                return temp;
            }
        }
        return null;
    }
}
[System.Serializable]
public class DataGachaRandom
{
    public int idConfig;
    public int idOfHero;
    public GearSlot GearSlot;
    public Rarity Rarity;
}
[System.Serializable]
public class Gacha
{
    public int id;
    public int costOpen1;
    public int costOpen10;
    public List<Droprate> DropList;
}
[System.Serializable]
public class Droprate
{
    public Rarity rarity;
    public int rate;
}
