using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEquipmentDetailCmd : Command
{
    [Inject] public ParameterEquipmentDetail Parameter { get; set; }
    [Inject] public PopupManager popupManager { get; set; }
    public override void Execute()
    {
        
        EquipmentDetailView detailview = popupManager.GetPopupByPopupKey(Parameter.popupkey) as EquipmentDetailView;
        detailview.SetupData(Parameter.equipmentData, Parameter.equipmentConfig);
        popupManager.ShowPopup(Parameter.popupkey);
        
    }
}
