using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class NotificationPanelHeroCmd : Command
{
    [Inject] public PopupManager popupManager { get; set; }
    public override void Execute()
    {
        PanelHeroView heroView = popupManager.GetPanelByPanelKey(PanelKey.PanelHero).GetComponent<PanelHeroView>();
        heroView.NotifyShowPanel();
    }
}
