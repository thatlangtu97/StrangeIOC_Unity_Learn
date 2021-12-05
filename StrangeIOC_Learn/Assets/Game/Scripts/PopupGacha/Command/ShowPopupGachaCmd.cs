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

        List<int> idConfigs = new List<int>();
        List<Rarity> raritys = new List<Rarity>();
        List<GearSlot> gearSlots = new List<GearSlot>();
        List<int> idOfHeros = new List<int>();
        foreach (DataGachaRandom tempData in dataGachaOpened.datas)
        {
            idConfigs.Add(tempData.idConfig);
            raritys.Add(tempData.Rarity);
            gearSlots.Add(tempData.GearSlot);
            idOfHeros.Add(tempData.idOfHero);
        }
        DataManager.Instance.InventoryDataManager.AddEquipments(idConfigs, raritys, gearSlots, idOfHeros);



    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_GACHA;
    }
}
