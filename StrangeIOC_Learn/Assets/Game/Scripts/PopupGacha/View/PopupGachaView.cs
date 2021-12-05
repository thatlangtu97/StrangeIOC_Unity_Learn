using System;
using System.Collections;
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
    public Gacha gacha;
    public Button OpenGacha;
    public Text EquipmentText;
    public Text RarityText;
    public Animator animator;
    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        dataGachaRandom data = global.dataGacha;
        gacha = global.CurrenctGacha;
        config = GachaLogic.getEquipmentConfig(data.GearSlot, data.idConfig, data.idOfHero);
        ImageGacha.sprite = config.GearFull;
        ImageGacha.SetNativeSize();
        gachaEffect._FillColor_Color_1 = EquipmentLogic.GetColorByRarity(data.Rarity);
        EquipmentText.text = config.gearName;
        RarityText.text = data.Rarity.ToString();
        RarityText.color = EquipmentLogic.GetColorByRarity(data.Rarity);
        animator.SetTrigger("Show");

    }
    
    public void Open()
    {
        
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen1) return;
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen1, false);
        global.UpdateDataAllCurrencyView();
        dataGachaRandom data = GachaLogic.GetGachaRandom(gacha.id);
        global.dataGacha = data;        
        showPopupGachaSignal.Dispatch();
    }
}
