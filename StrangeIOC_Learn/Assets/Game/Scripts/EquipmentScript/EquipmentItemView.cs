using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentItemView : MonoBehaviour
{
    public Image icon;
    public Image boderRarity;
    public Text level;
    EquipmentData data;
    EquipmentConfig config;

    public void Show(EquipmentData data, EquipmentConfig config)
    {
        this.data = data;
        this.config = config;
        icon.sprite = config.GearIcon;
        boderRarity.color = GachaLogic.GetColorByRarity(data.rarity);
        level.text = $"{data.level}";
    }
}
