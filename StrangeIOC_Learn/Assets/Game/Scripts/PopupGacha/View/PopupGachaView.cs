using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGachaView : AbsPopupView
{

    [Inject] public GlobalData globalData { get; set; }
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
        dataGachaRandom data = globalData.dataGacha;
        CurrencyGacha = globalData.CurrenctGacha;
        Debug.Log($"GearSlot {data.GearSlot} idConfig {data.idConfig} Rarity {data.Rarity} idOfHero {data.idOfHero}");
        config = GachaLogic.getEquipmentConfig(data.GearSlot, data.idConfig, data.idOfHero);
        ImageGacha.sprite = config.GearFull;
        ImageGacha.SetNativeSize();
        gachaEffect._FillColor_Color_1 = GachaLogic.GetColorByRarity(data.Rarity);
    }
    public void Open()
    {
        dataGachaRandom data = GachaLogic.GetGachaRandom(CurrencyGacha.id);
        globalData.dataGacha = data;        
        showPopupGachaSignal.Dispatch();
    }
}
