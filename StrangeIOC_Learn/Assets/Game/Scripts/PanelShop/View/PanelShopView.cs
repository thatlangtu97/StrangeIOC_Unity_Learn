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
    protected override void Awake()
    {
        base.CopyStart();
        //Setup();
        //ShowPopupShop(popupManager.popupKey);

    }
    protected override void Start()
    {
        base.Start();
        Setup();
        ShowPopupShop(popupManager.popupKey);

    }
    public void Setup()
    {
        backBtn.onClick.AddListener(() => popupManager.BackPanel());
        foreach (TabShopType tab in ListTab)
        {
            tab.btn.onClick.AddListener(() => ShowPopupShop(tab.key));
        }
    }
    public override void ShowPanelByCmd()
    {
        base.ShowPanelByCmd();
        ShowPopupShop(popupManager.popupKey);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        ShowPopupShop(popupManager.popupKey);
    }
    protected override void OnEnable()
    {
        base.CopyStart();
        base.OnEnable();
        ShowPopupShop(popupManager.popupKey);
    }
    void ShowPopupShop(PopupKey popupKey)
    {
        foreach (PopupShopType popup in ListPopup)
        {
            if(popup.key == popupKey)
            {
                popup.Prefab.GetComponent<AbsPopupView>().ShowPopup();
            }
            else
            {
                if(popup.Prefab.activeInHierarchy)
                    popup.Prefab.GetComponent<AbsPopupView>().HidePopup();
            }
        }
        foreach (TabShopType tab in ListTab)
        {
            Color colorTemp = tab.text.color;
            if (tab.key == popupKey)
            {
                
                tab.text.color = new Vector4(colorTemp.r, colorTemp.g, colorTemp.b, 1f);
            }
            else
            {
                tab.text.color = new Vector4(colorTemp.r, colorTemp.g, colorTemp.b, 0.5f);
            }
        }
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
        public Text text;
        public PopupKey key;
    }

}
