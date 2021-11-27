﻿using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUtils :View
{
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    [Inject] public PopupManager popupManager { get; set; }
    [Inject] public GlobalData globalData { get; set; }
    public PanelKey panelKey;
    public PopupKey popupKey;
    public CurrencyType currencyType;
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("testSpawnObject");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("HomeScene");
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            popupManager.SetPanelAfterLoadHomeScene(panelKey, popupKey);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            globalData.UpdateDataAllCurrencyView();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            switch (currencyType)
            {
                case CurrencyType.gold:
                    DataManager.Instance.CurrencyDataManager.UpGold(100,false);
                    break;
                case CurrencyType.gem:
                    DataManager.Instance.CurrencyDataManager.UpGem(100, false);
                    break;
                case CurrencyType.stamina:
                    DataManager.Instance.CurrencyDataManager.UpStamina(1,false);
                    break;
            }
            globalData.UpdateDataAllCurrencyView();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            switch (currencyType)
            {
                case CurrencyType.gold:
                    DataManager.Instance.CurrencyDataManager.DownGold(100, false);
                    break;
                case CurrencyType.gem:
                    DataManager.Instance.CurrencyDataManager.DownGem(100, false);
                    break;
                case CurrencyType.stamina:
                    DataManager.Instance.CurrencyDataManager.DownStamina(1, false);
                    break;
            }
            globalData.UpdateDataAllCurrencyView();
        }
    }
}
