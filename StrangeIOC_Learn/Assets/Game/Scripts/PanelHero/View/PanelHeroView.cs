using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(()=>popupManager.BackPanel());
        popupManager.AddPopupOfPanel(PopupKey.EquipmentHeroDetailLeft, EquipmentDetailLeft);
        popupManager.AddPopupOfPanel(PopupKey.EquipmentHeroDetailRight, EquipmentDetailFight);
    }
}
