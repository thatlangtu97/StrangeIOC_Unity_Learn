using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using strange.extensions.mediation.impl;

public class EquipmentItemView : View
{
    [Inject] public GlobalData global { get; set; }
    public Image icon;
    public Image boderRarity;
    public Text level;
    EquipmentData data;
    EquipmentConfig config;
    public Button button;
    
    public void Show(EquipmentData data, EquipmentConfig config)
    {
        base.CopyStart();
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=> EquipmentLogic.EquipGear(data, global.CurrentIdHero));
        this.data = data;
        this.config = config;
        icon.sprite = config.GearIcon;
        boderRarity.color = GachaLogic.GetColorByRarity(data.rarity);
        level.text = $"{data.level}";
    }

}
