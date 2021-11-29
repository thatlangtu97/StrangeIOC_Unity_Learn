using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaCmd : AbsShowPopupCmd
{
    [Inject] public GlobalData global { get; set; }
    public override void Execute()
    {
        popupKey = PopupKey.GachaPopup;
        PopupGachaView popupStaminaView = GetInstance<PopupGachaView>();
        popupStaminaView.ShowPopupByCmd();
        DataManager.Instance.InventoryDataManager.AddEquipment(global.dataGacha.idConfig, global.dataGacha.Rarity, global.dataGacha.GearSlot);
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}
