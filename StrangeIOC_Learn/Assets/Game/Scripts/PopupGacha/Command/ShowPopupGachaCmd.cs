using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        popupKey = PopupKey.GachaPopup;
        PopupGachaView popupStaminaView = GetInstance<PopupGachaView>();
        popupStaminaView.ShowPopupByCmd();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}
