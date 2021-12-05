using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class CraftEquipmentCmd : Command
{
    [Inject] public NotificationPanelCraftSignal notificationPanelCraftSignal { get; set; }
    public override void Execute()
    {
        EquipmentLogic.CraftItem();
        notificationPanelCraftSignal.Dispatch();
    }
}
