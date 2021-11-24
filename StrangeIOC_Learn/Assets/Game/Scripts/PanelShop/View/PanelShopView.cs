using strange.extensions.signal.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopView : AbsPanelView
{
    public Button backBtn;
    public List<PopupShopType> ListPopup;
    public List<TabShopType> ListTab;
    protected override void Start()
    {
        base.Start();
        Setup();
        popupManager.ShowPopup(popupManager.popupKey);

    }
    public void Setup()
    {
        backBtn.onClick.AddListener(() => popupManager.BackPanel());
        foreach (PopupShopType popup in ListPopup)
        {
            popupManager.AddPopupOfPanel(popup.key, popup.Prefab);
        }
        foreach (TabShopType tab in ListTab)
        {
            //tab.btn.onClick.AddListener(() => popupManager.ShowPopup(tab.key, tab.keyPanel));
            tab.btn.onClick.AddListener(() => popupManager.ShowPopup(tab.key));
        }
    }
    protected override void OnEnable()
    {
        base.CopyStart();
        base.OnEnable();
        popupManager.ShowPopup(popupManager.popupKey);
    }



    [System.Serializable]
    public class PopupShopType
    {
        public GameObject Prefab;
        public PopupKey key;
    }
    [System.Serializable]
    public class TabShopType
    {
        public Button btn;
        public PopupKey key;
        public PanelKey keyPanel;
    }

}
