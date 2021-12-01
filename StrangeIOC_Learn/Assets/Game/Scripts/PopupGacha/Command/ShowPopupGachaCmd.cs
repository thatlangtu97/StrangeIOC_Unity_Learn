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
        dataGachaRandom dataGacha = global.dataGacha;
        DataManager.Instance.InventoryDataManager.AddEquipment(dataGacha.idConfig, dataGacha.Rarity, dataGacha.GearSlot, dataGacha.idOfHero);
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}
