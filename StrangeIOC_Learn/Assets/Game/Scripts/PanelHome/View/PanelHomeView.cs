using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHomeView : AbsPanelView
{
    [Inject]public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    [Inject]public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    public Button StaminaBtn;
    public Button HeroBtn;

    protected override void Start()
    {
        base.Start();
        StaminaBtn.onClick.AddListener( ()=>{ showPopupStaminaSignal.Dispatch(); });
        HeroBtn.onClick.AddListener(() => { showPanelHeroSignal.Dispatch(); });
    }
}
