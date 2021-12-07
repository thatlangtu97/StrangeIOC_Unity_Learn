using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowPopupCraftCmd : AbsShowPopupCmd
{
    [Inject] public EquipmentData equipmentData { get; set; }
    public override void Execute()
    {
        popupKey = PopupKey.CraftPopup;
        ShowPopupCraftView showPopupCraftView = GetInstance<ShowPopupCraftView>();
        showPopupCraftView.equipmentData = equipmentData;
        showPopupCraftView.ShowPopupByCmd();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_CRAFT;
    }
}
