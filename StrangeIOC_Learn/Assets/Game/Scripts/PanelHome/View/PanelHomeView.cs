﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHomeView : AbsPanelView
{
    [Inject] public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelCraftSignal showPanelCraftSignal { get; set; }
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
    public Button StaminaBtn;
    public Button HeroBtn;
    public Button CraftBtn;
    public Button ShopBtn;
    //public Button ShopGoldBtn, ShopGemBtn;

    protected override void Start()
    {
        base.Start();
        //StaminaBtn.onClick.AddListener( ()=>{ showPopupStaminaSignal.Dispatch(); });
        HeroBtn.onClick.AddListener(() => { showPanelHeroSignal.Dispatch(); });
        CraftBtn.onClick.AddListener(() => { showPanelCraftSignal.Dispatch(); });

        ShopBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
        ShopBtn.onClick.AddListener(() => { popupManager.popupKey = PopupKey.ShopGoldPopup; });
        /*
        ShopGoldBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
        ShopGoldBtn.onClick.AddListener(() => { popupManager.popupKey = PopupKey.ShopGoldPopup; });
        ShopGemBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
        ShopGemBtn.onClick.AddListener(() => { popupManager.popupKey = PopupKey.ShopGemPopup; });
        */
    }
}
