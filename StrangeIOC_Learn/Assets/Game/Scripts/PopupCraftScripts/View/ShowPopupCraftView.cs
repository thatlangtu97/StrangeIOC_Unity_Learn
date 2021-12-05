using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPopupCraftView : AbsPopupView
{
    public EquipmentData equipmentData;
    public EquipmentConfig config;
    public AutoPlayOpenGacha gachaEffect;
    public Image ImageGear;
    public Text EquipmentText;
    public Text RarityText;
    public Animator animator;
    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        if (equipmentData != null)
        {
            config = GachaLogic.getEquipmentConfig(equipmentData.gearSlot, equipmentData.idConfig, equipmentData.idOfHero);
            ImageGear.sprite = config.GearFull;
            ImageGear.SetNativeSize();
            gachaEffect._FillColor_Color_1 = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
            EquipmentText.text = config.gearName;
            RarityText.text = equipmentData.rarity.ToString();
            RarityText.color = EquipmentLogic.GetColorByRarity(equipmentData.rarity);
            animator.SetTrigger("Show");
        }
    }
}
