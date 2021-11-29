﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGachaView : AbsPopupView
{

    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    public EquipmentConfig config;
    public AutoPlayOpenGacha gachaEffect;
    public Image ImageGacha;
    public Gacha CurrencyGacha;
    public Button OpenGacha;
    public Animator animator;
    
    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        animator.Play("ShowGacha");
        dataGachaRandom data = global.dataGacha;
        CurrencyGacha = global.CurrenctGacha;
        Debug.Log($"GearSlot {data.GearSlot} idConfig {data.idConfig} Rarity {data.Rarity} idOfHero {data.idOfHero}");
        config = GachaLogic.getEquipmentConfig(data.GearSlot, data.idConfig, data.idOfHero);
        ImageGacha.sprite = config.GearFull;
        ImageGacha.SetNativeSize();
        gachaEffect._FillColor_Color_1 = GachaLogic.GetColorByRarity(data.Rarity);
    }
    public void Open()
    {
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen1) return;
        dataGachaRandom data = GachaLogic.GetGachaRandom(CurrencyGacha.id);
        global.dataGacha = data;        
        showPopupGachaSignal.Dispatch();
    }
}
