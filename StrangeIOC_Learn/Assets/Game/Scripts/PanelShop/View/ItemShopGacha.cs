using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class ItemShopGacha : View
{
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    [Inject] public GlobalData global { get; set; }
    public int idGacha;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
    }
    public void Open()
    {
        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen1) return;

        dataGachaRandom data= GachaLogic.GetGachaRandom(idGacha);
        global.dataGacha = data;        
        showPopupGachaSignal.Dispatch();
    }

}
