using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    //public InventoryView inventoryView;
    //public HeroEquipmentView heroEquipmentView;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(()=>popupManager.BackPanel());
        popupManager.AddPopup(PopupKey.EquipmentHeroDetailLeft, EquipmentDetailLeft);
        popupManager.AddPopup(PopupKey.EquipmentHeroDetailRight, EquipmentDetailFight);
    }
}
