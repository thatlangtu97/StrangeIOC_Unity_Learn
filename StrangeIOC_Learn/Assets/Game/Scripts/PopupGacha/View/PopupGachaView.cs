using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGachaView : AbsPopupView
{

    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    public DataGachaOpened dataGachaOpened;
    public EquipmentConfig config;
    public AutoPlayOpenGacha gachaEffect;
    public Image ImageGacha;
    public Gacha gacha;
    public Button OpenGacha;
    public Text EquipmentText;
    public Text RarityText;
    public Text costOpen1Text, costOpen10Text;
    public Animator animator;
    public override void ShowPopupByCmd()
    {
        base.ShowPopupByCmd();
        DataGachaRandom data = dataGachaOpened.datas[0];
        foreach(DataGachaRandom tempData in dataGachaOpened.datas)
        {
            if(tempData.Rarity> data.Rarity)
            {
                data = tempData;
            }
        }        
        SetupValue();
        config = GachaLogic.getEquipmentConfig(data.GearSlot, data.idConfig, data.idOfHero);
        ImageGacha.sprite = config.GearFull;
        ImageGacha.SetNativeSize();
        gachaEffect._FillColor_Color_1 = EquipmentLogic.GetColorByRarity(data.Rarity);
        EquipmentText.text = config.gearName;
        RarityText.text = data.Rarity.ToString();
        RarityText.color = EquipmentLogic.GetColorByRarity(data.Rarity);
        animator.SetTrigger("Show");
    }
    public void SetupValue()
    {
        gacha = global.CurrenctGacha;
        costOpen1Text.text = gacha.costOpen1.ToString();
        costOpen10Text.text = gacha.costOpen10.ToString();
    }
    public void Open()
    {        
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen1) return;

        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(gacha.id);
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen1, false);
        global.UpdateDataAllCurrencyView();

        DataGachaOpened dataGachaOpened = new DataGachaOpened();
        DataGachaRandom data = GachaLogic.GetGachaRandom(gacha.id);
        dataGachaOpened.datas.Add(data);

        showPopupGachaSignal.Dispatch(dataGachaOpened);
    }
    public void Open10()
    {
        if (DataManager.Instance.CurrencyDataManager.gem < global.CurrenctGacha.costOpen10) return;

        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(gacha.id);
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen10, false);
        global.UpdateDataAllCurrencyView();

        DataGachaOpened dataGachaOpened = new DataGachaOpened();
        for (int i = 0; i < 10; i++)
        {
            DataGachaRandom data = GachaLogic.GetGachaRandom(gacha.id);
            dataGachaOpened.datas.Add(data);
        }
        showPopupGachaSignal.Dispatch(dataGachaOpened);
    }
}
