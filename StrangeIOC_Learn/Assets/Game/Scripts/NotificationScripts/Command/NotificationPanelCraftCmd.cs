using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class NotificationPanelCraftCmd : Command
{
    [Inject] public PopupManager popupManager { get; set; }
    public override void Execute()
    {
        PanelCraftView craftview = popupManager.GetPanelByPanelKey(PanelKey.PanelCraft).GetComponent<PanelCraftView>();
        craftview.NotifyShowPanel();
    }
}
