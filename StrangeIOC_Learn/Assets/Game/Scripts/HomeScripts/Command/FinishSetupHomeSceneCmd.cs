using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetupHomeSceneCmd : Command
{
    [Inject]
    public PopupManager popupManager { get; set; }
    [Inject]
    public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject]
    public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject]
    public ShowPanelShopSignal showPanelShopSignal { get; set; }
    [Inject]
    public ShowPanelCraftSignal showPanelCraftSignal { get; set; }

    public override void Execute()
    {
        PanelKey panelKey = popupManager.GetPanelAfterLoadHomeScene();
        switch (panelKey)
        {
            case PanelKey.PanelHome:
                showPanelHomeSignal.Dispatch();
                break;
            case PanelKey.PanelHero:
                showPanelHeroSignal.Dispatch();
                break;
            case PanelKey.PanelCraft:
                showPanelCraftSignal.Dispatch();
                break;
            case PanelKey.PanelShop:
                showPanelShopSignal.Dispatch();
                break;
        }
        //popupManager.ResetPanelShowAfterLoadHomeScene();
    }
}
