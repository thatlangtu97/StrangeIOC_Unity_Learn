using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailView : AbsPopupView
{

    [Inject] public EquipGearSignal equipGearSignal { get; set; }
    [Inject] public UnequipGearSignal unEquipGearSignal { get; set; }
    [Inject] public NotificationPanelHeroSignal NotificationPanelHeroSignal { get; set; }
    [Inject] public NotificationPanelCraftSignal NotificationPanelCraftSignal { get; set; }
    public EquipmentItemView EquipmentView;
    public Text nameEquipment;
    public Text rarityEquipment;
    public Text mainStatType,mainStatValue;
    EquipmentData equipmentData;
    EquipmentConfig equipmentConfig;
    public void SetupData(EquipmentData equipmentData, EquipmentConfig equipmentConfig)
    {
        this.equipmentData = equipmentData;
        this.equipmentConfig = equipmentConfig;
        EquipmentView.Show(equipmentData, equipmentConfig);
        nameEquipment.text = equipmentConfig.gearName;
        nameEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        rarityEquipment.text = equipmentData.rarity.ToString();
        rarityEquipment.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
        mainStatType.text = EquipmentLogic.StatTypeToString(equipmentData.mainStatData.statType);
        mainStatValue.text = EquipmentLogic.StatValueToString(equipmentData.mainStatData.statType,equipmentData.mainStatData.baseValue);
    }
    public void EquipGear()
    {
        equipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        HidePopup();

    }
    public void UnEquipGear()
    {
        unEquipGearSignal.Dispatch(equipmentData);
        NotificationPanelHeroSignal.Dispatch();
        HidePopup();
    }
    public void SelectGearCraft()
    {
        EquipmentLogic.AddEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        HidePopup();
    }
    public void UnSelectGearCraft()
    {
        EquipmentLogic.RemoveEquipmentToCraft(equipmentData);
        NotificationPanelCraftSignal.Dispatch();
        HidePopup();
    }
    

}
