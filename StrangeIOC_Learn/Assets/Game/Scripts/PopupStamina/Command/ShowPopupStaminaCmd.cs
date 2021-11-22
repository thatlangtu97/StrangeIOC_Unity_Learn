using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowPopupStaminaCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        popupKey = PopupKey.StaminaPopup;
        PopupStaminaView popupStaminaView = GetInstance<PopupStaminaView>();
        popupStaminaView.ShowPopupByCmd();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_STAMINA;
    }
}
