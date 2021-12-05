using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class CraftEquipmentCmd : Command
{
    [Inject] public NotificationPanelCraftSignal notificationPanelCraftSignal { get; set; }
    [Inject] public ShowPopupCraftSignal ShowPopupCraftSignal { get; set; }
    //[Inject] public ShowPopupStaminaSignal showPopupStamina { get; set; }
    public override void Execute()
    {
        EquipmentData equipmentData = EquipmentLogic.CraftItem();
        ShowPopupCraftSignal.Dispatch(equipmentData);
        notificationPanelCraftSignal.Dispatch();
        //showPopupStamina.Dispatch();
    }
}
