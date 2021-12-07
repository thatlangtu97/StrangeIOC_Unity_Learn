using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaInfoCmd : AbsShowPopupCmd
{
    [Inject] public Gacha gacha { get; set; }
    public override void Execute()
    {
        base.Execute();
        popupKey = PopupKey.GachaInfoPopup;
        PopupGachaInfoView popupGachaInfoView = GetInstance<PopupGachaInfoView>();
        popupGachaInfoView.gacha = gacha;
        popupGachaInfoView.ShowPopupByCmd();
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_INFO_GACHA;
    }
}
