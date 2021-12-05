using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopGacha : View
{
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    [Inject] public GlobalData global { get; set; }
    public int idGacha;
    public Text costOpen1Text, costOpen10Text;
    public Gacha gacha;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        Setup();
    }
    void Setup()
    {
        gacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        costOpen1Text.text = gacha.costOpen1.ToString();
        costOpen10Text.text = gacha.costOpen10.ToString();
    }
    public void Open()
    {
        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen1) return;
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen1,false);
        global.UpdateDataAllCurrencyView();

        dataGachaRandom data= GachaLogic.GetGachaRandom(idGacha);
        global.dataGacha = data;        
        showPopupGachaSignal.Dispatch();
    }

}
