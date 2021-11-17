using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHomeView : AbsPanelView
{
    [Inject]public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    [Inject]public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelCraftSignal showPanelCraftSignal { get; set; }
    public Button StaminaBtn;
    public Button HeroBtn;
    public Button CraftBtn;

    protected override void Start()
    {
        base.Start();
        StaminaBtn.onClick.AddListener( ()=>{ showPopupStaminaSignal.Dispatch(); });
        HeroBtn.onClick.AddListener(() => { showPanelHeroSignal.Dispatch(); });
        CraftBtn.onClick.AddListener(() => { showPanelCraftSignal.Dispatch(); });
    }
}
