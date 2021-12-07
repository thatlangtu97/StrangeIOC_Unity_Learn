using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopGacha : View
{
    [Inject] public ShowPopupGachaSignal showPopupGachaSignal { get; set; }
    [Inject] public GlobalData global { get; set; }
    [Inject] public ShowPopupGachaInfoSignal ShowPopupGachaInfoSignal { get; set; }
    public int idGacha;
    public Text costOpen1Text, costOpen10Text;
    public Gacha gacha;
    public Button infoGachaBtn;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        Setup();
    }
    void Setup()
    {
        gacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        costOpen1Text.text = gacha.costOpen1.ToString();
        costOpen10Text.text = gacha.costOpen10.ToString();
        infoGachaBtn.onClick.AddListener(ShowInfoGacha);
    }
    public void ShowInfoGacha()
    {
        ShowPopupGachaInfoSignal.Dispatch(gacha);
    }
    public void Open()
    {
        if (DataManager.Instance.CurrencyDataManager.gem < gacha.costOpen1) return;
       
        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen1, false);
        global.UpdateDataAllCurrencyView();

        DataGachaOpened dataGachaOpened = new DataGachaOpened();
        DataGachaRandom data= GachaLogic.GetGachaRandom(idGacha);        
        dataGachaOpened.datas.Add(data);

        showPopupGachaSignal.Dispatch(dataGachaOpened);
    }
    public void Open10()
    {        
        if (DataManager.Instance.CurrencyDataManager.gem < gacha.costOpen10) return;
        
        global.CurrenctGacha = ScriptableObjectData.GachaConfigCollection.GetGachaById(idGacha);
        DataManager.Instance.CurrencyDataManager.DownGem(gacha.costOpen10, false);
        global.UpdateDataAllCurrencyView();

        DataGachaOpened dataGachaOpened = new DataGachaOpened();
        for (int i=0;i< 10; i++)
        {           
            DataGachaRandom data = GachaLogic.GetGachaRandom(idGacha);
            dataGachaOpened.datas.Add(data);
        }
        showPopupGachaSignal.Dispatch(dataGachaOpened);
    }

}
public class DataGachaOpened
{
    public List<DataGachaRandom> datas = new List<DataGachaRandom>();
}
