using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCraftView : AbsPanelView
{
    public Button backBtn;
    public GameObject EquipmentDetailLeft, EquipmentDetailFight;
    public AbsPopupView PopupEquipmentDetailLeft, PopupEquipmentDetailFight;
    public CraftEquipmentView craftEquipmentView;
    public InventoryView inventoryView;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(() => popupManager.BackPanel());
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailLeft, PopupEquipmentDetailLeft);
        popupManager.AddPopup(PopupKey.EquipmentCraftDetailRight, PopupEquipmentDetailFight);
    }
    public override void NotifyShowPanel()
    {
        base.NotifyShowPanel();
        craftEquipmentView.Show();
        inventoryView.ReloadPage();
    }
    public override void ShowPanelByCmd()
    {
        EquipmentLogic.RemoveAllEquipmentToCraft();
        base.ShowPanelByCmd();
        
        //craftEquipmentView.Show();
        //inventoryView.ReloadPage();
        
    }
}
