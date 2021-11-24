using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCraftView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(() => popupManager.BackPanel());
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailLeft, EquipmentDetailLeft);
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailRight, EquipmentDetailFight);
    }
}
