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
        dataGachaRandom data= GachaLogic.GetGachaRandom(idGacha);
        global.dataGacha = data;
        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        showPopupGachaSignal.Dispatch();
    }

}
