using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    public AbsPopupView PopupEquipmentDetailLeft, PopupEquipmentDetailFight;
    //public InventoryView inventoryView;
    //public HeroEquipmentView heroEquipmentView;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(()=>popupManager.BackPanel());
        popupManager.AddPopup(PopupKey.EquipmentHeroDetailLeft, PopupEquipmentDetailLeft);
        popupManager.AddPopup(PopupKey.EquipmentHeroDetailRight, PopupEquipmentDetailFight);
    }
}
