using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using strange.extensions.mediation.impl;

public class EquipmentItemView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowEquipmentDetailSignal showEquipmentDetailSignal { get; set; }

    public Image icon;
    public Image boderRarity;
    public Text level;
    EquipmentData data;
    EquipmentConfig config;
    
    protected override void Awake()
    {
        base.Awake();
    }
    public void Show(EquipmentData data, EquipmentConfig config )
    {
        base.CopyStart();
        this.data = data;
        this.config = config;
        icon.sprite = config.GearIcon;
        boderRarity.color = EquipmentLogic.GetColorByRarity(data.rarity);
        if(level!=null) level.text = $"{data.level}";
    }
    public void ShowDetail(int valuePopup)
    {
        ParameterEquipmentDetail temp = new ParameterEquipmentDetail();
        temp.equipmentData = data;
        temp.equipmentConfig = config;
        temp.popupkey = (PopupKey)valuePopup;
        showEquipmentDetailSignal.Dispatch(temp);
    }
    


}
public class ParameterEquipmentDetail
{
    public EquipmentData equipmentData;
    public EquipmentConfig equipmentConfig;
    public PopupKey popupkey;
}
