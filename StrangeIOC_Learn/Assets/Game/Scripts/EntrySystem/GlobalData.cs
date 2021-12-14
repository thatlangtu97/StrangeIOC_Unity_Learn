using System.Collections.Generic;
using strange.extensions.context.impl;

public class GlobalData
{
    #region CURRENCY ASSET SHOWER
    public Dictionary<CurrencyType, List<CurrencyAssetShower>> dicCurrencyAssetShower = new Dictionary<CurrencyType, List<CurrencyAssetShower>>();
    public void AddCurrencyAssetShower(CurrencyType type, CurrencyAssetShower asset)
    {
        if (!dicCurrencyAssetShower.ContainsKey(type))
        {
            dicCurrencyAssetShower.Add(type, new List<CurrencyAssetShower>());
        }
        dicCurrencyAssetShower[type].Add(asset);
    }
    public void UpdateDataAllCurrencyView()
    {
        foreach (CurrencyType type in dicCurrencyAssetShower.Keys)
        {
            foreach (CurrencyAssetShower asset in dicCurrencyAssetShower[type])
            {
                if (asset != null)
                {
                    asset.Setup();
                }
            }
        }
    }
    public void UpdateDataAllCurrencyView(CurrencyType type)
    {
        if (!dicCurrencyAssetShower.ContainsKey(type)) return;
        foreach (CurrencyAssetShower asset in dicCurrencyAssetShower[type])
        {
            if (asset != null)
            {
                asset.Setup();
            }
        }
    }
    #endregion
    #region GACHA
    public Gacha CurrenctGacha;
    #endregion
    #region INVENTORY
    public GearSlot CurrentTab= GearSlot.weapon;
    public int CurrentIdHero=0;
    public EquipmentData dataLeft,dataRight;

    #endregion

}