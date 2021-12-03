using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCraftView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    public AbsPopupView PopupEquipmentDetailLeft, PopupEquipmentDetailFight;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(() => popupManager.BackPanel());
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailLeft, PopupEquipmentDetailLeft);
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailRight, PopupEquipmentDetailFight);
    }
}
