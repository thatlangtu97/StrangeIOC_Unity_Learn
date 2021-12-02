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
        
        EquipmentDetailView detailview = popupManager.GetPopupByPopupKey(Parameter.popupkey).GetComponent<EquipmentDetailView>();
        Debug.Log(detailview);
        detailview.SetupData(Parameter.equipmentData);
        popupManager.ShowPopup(Parameter.popupkey);
        
    }
}
