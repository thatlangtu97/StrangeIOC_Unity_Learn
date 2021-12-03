using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDetailView : AbsPopupView
{

    [Inject] public EquipGearSignal equipGearSignal { get; set; }
    [Inject] public UnequipGearSignal nnEquipGearSignal { get; set; }
    public EquipmentItemView EquipmentView;
    public Text nameEquipment;
    public Text rarityEquipment;
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
    }
    public void EquipGear()
    {
        equipGearSignal.Dispatch(equipmentData);
    }
    public void UnEquipGear()
    {
        nnEquipGearSignal.Dispatch(equipmentData);
    }
    

}
