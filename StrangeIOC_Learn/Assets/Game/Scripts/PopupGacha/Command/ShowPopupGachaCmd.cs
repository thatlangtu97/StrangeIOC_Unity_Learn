using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupGachaCmd : AbsShowPopupCmd
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public DataGachaOpened dataGachaOpened { get; set; }
    public override void Execute()
    {
        popupKey = PopupKey.GachaPopup;
        PopupGachaView popupGachaView = GetInstance<PopupGachaView>();
        popupGachaView.dataGachaOpened = dataGachaOpened;
        popupGachaView.ShowPopupByCmd();

        List<EquipmentData> newItems = new List<EquipmentData>();
        foreach (DataGachaRandom tempData in dataGachaOpened.datas)
        {
            EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(tempData.idConfig);
            EquipmentData newItem = EquipmentLogic.CloneEquipmentData(tempData.idConfig, tempData.Rarity, tempData.GearSlot, tempData.idOfHero, 1);
            newItem.mainStatData = EquipmentLogic.RandomStatEquipment(config.mainStatConfig, tempData.Rarity);
            newItems.Add(newItem); 
        }
        DataManager.Instance.InventoryDataManager.AddItems(newItems);



    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}
