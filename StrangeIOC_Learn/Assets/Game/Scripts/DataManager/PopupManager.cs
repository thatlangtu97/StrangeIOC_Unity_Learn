﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.signal.impl;

public class PopupManager 
{
    public PanelKey panelKey { get; set; }
    public PopupKey popupKey { get; set; }
    public Dictionary<UILayer, Transform> UIDic = new Dictionary<UILayer, Transform>();
    public Dictionary<PanelKey, GameObject> PanelDic = new Dictionary<PanelKey, GameObject>();
    public Dictionary<PopupKey, GameObject> PopupDic = new Dictionary<PopupKey, GameObject>();

    public Dictionary<PanelKey, List<GameObject>> ListPopupOfPanel = new Dictionary<PanelKey, List<GameObject>>();
    public PanelKey currentPanel;
    //new databack
    public Dictionary<PanelKey,Signal> DicSignalPanel = new Dictionary<PanelKey,Signal>();



    [Inject] public ShowPanelHeroSignal showPanelHeroSignal { get; set; }
    [Inject] public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    public PopupManager()
    {
    }

    public void AddUILayer(UILayer layer, Transform transform)
    {

        if (UIDic.ContainsKey(layer))
        {
            UIDic[layer] = transform;
        }
        else
        {
            UIDic.Add(layer, transform);

        }
    }
    public Transform GetUILayer(UILayer layer)
    {
        if (UIDic.ContainsKey(layer))
        {
            return UIDic[layer];
        }
        else
        {
            return null;
        }

    }

    #region PANEL
    public bool CheckContainPanel(PanelKey key)
    {
        if (PanelDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
    public GameObject GetPanelByPanelKey(PanelKey key)
    {
        if (PanelDic.ContainsKey(key))
        {
            return PanelDic[key];
        }
        
        return null;
    }
    public void AddPanel(PanelKey key , GameObject panel)
    {
        if (PanelDic.ContainsKey(key))
        {
            PanelDic[key] = panel;
        }
        else
        {
            PanelDic.Add(key, panel);
        }
    }
    public void AddPanel(PanelKey key, Signal signalPanel)
    {
        if (DicSignalPanel.ContainsKey(key))
        {
            DicSignalPanel[key] = signalPanel;
        }
        else
        {
            DicSignalPanel.Add(key, signalPanel);
        }
    }
    public void SetPanelAfterLoadHomeScene(PanelKey key)
    {
        panelKey = key;
    }
    public void ResetPanelAfterLoadHomeScene()
    {
        panelKey = PanelKey.PanelHome;
    }
    public PanelKey GetPanelAfterLoadHomeScene()
    {
        return panelKey;
    }
    public void ResetPanelShowAfterLoadHomeScene()
    {
        //panelKey = PanelKey.PanelHome;
    }
    public void ShowPanel(PanelKey key)
    {
        currentPanel = key;
        if (!ListPopupOfPanel.ContainsKey(key))
        {
            ListPopupOfPanel.Add(key, new List<GameObject>());
        }
        foreach (PanelKey temp in PanelDic.Keys)
        {
            if (temp != key)
            {
                //PanelDic[temp].SetActive(false);
                PanelDic[temp].GetComponent<AbsPanelView>().HidePanel();
            }
            else
            {
                PanelDic[temp].GetComponent<AbsPanelView>().ShowPanel();
            }
        }
    }
    public void BackPanel()
    {
        //Disable popup
        if (!ListPopupOfPanel.ContainsKey(currentPanel))
        {
            ListPopupOfPanel.Add(currentPanel, new List<GameObject>());
        }
        GameObject lastPopup = null;
        foreach(GameObject temp in ListPopupOfPanel[currentPanel])
        {
            if (temp.activeInHierarchy == true)
            {
                lastPopup = temp;
            }
        }
        if (lastPopup != null)
        {
            lastPopup.SetActive(false);
            return;
        }
        //disable Panel
        foreach (PanelKey temp in PanelDic.Keys)
        {
            if (temp != PanelKey.PanelHome)
            {
                if (PanelDic[temp].activeInHierarchy == true)
                {
                    PanelDic[temp].GetComponent<AbsPanelView>().HidePanel();
                }
            }
        }
        if (!PanelDic.ContainsKey(PanelKey.PanelHome)){
            showPanelHomeSignal.Dispatch();
            currentPanel = PanelKey.PanelHome;
        }
        else
        {
            showPanelHomeSignal.Dispatch();
            currentPanel = PanelKey.PanelHome;
        }
    }
    #endregion

    #region POPUP
    public bool CheckContainPopup(PopupKey key)
    {
        if (PopupDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
    public GameObject GetPopupByPopupKey(PopupKey key)
    {
        if (PopupDic.ContainsKey(key))
        {
            return PopupDic[key];
        }

        return null;
    }
    public void AddPopup(PopupKey key, GameObject panel)
    {
        if (PopupDic.ContainsKey(key))
        {
            PopupDic[key] = panel;
        }
        else
        {
            PopupDic.Add(key, panel);
        }
    }
    public void AddPopupOfPanel(PopupKey key, GameObject panel)
    {
        if (PopupDic.ContainsKey(key))
        {
            PopupDic[key] = panel;
        }
        else
        {
            PopupDic.Add(key, panel);
        }
        if (!ListPopupOfPanel.ContainsKey(currentPanel))
        {
            ListPopupOfPanel.Add(currentPanel, new List<GameObject>());
        }
        else
        {
            if (!ListPopupOfPanel[currentPanel].Contains(PopupDic[key]))
            {
                ListPopupOfPanel[currentPanel].Add(PopupDic[key]);
            }
        }
    }
    public void ResetPopup()
    {
        //popupKey = PopupKey.Node;
    }
    public void ShowPopup(PopupKey key)
    {
        
        if (!ListPopupOfPanel.ContainsKey(currentPanel))
        {
            ListPopupOfPanel.Add(currentPanel, new List<GameObject>());
        }
        else
        {
            if (!ListPopupOfPanel[currentPanel].Contains(PopupDic[key]))
            {
                ListPopupOfPanel[currentPanel].Add(PopupDic[key]);
            }
        }
    }
    public void ShowPopup(PopupKey keyPopup , PanelKey keyPanel)
    {
        if (!ListPopupOfPanel.ContainsKey(keyPanel))
        {
            ListPopupOfPanel.Add(keyPanel, new List<GameObject>());
        }
        List <GameObject> TempPopupOfPanel = ListPopupOfPanel[keyPanel];
        GameObject PopupGameObject = PopupDic[keyPopup];
        foreach (GameObject tempPopup in TempPopupOfPanel)
        {
            if(tempPopup != PopupGameObject)
            {
                tempPopup.gameObject.SetActive(false);
            }
            else
            {
                tempPopup.gameObject.SetActive(true);
            }
        }

    }
    #endregion

}
public enum PanelKey
{
    PanelHome,
    PanelHero,
    PanelCraft,
    PanelShop,
}
public enum PopupKey
{
    Node,
    StaminaPopup,
    EquipmentHeroDetailLeft,
    EquipmentHeroDetailRight,
    EquipmentCraftDetailLeft,
    EquipmentCraftDetailRight,
    ShopGoldPopup,
    ShopGemPopup,
    ShopGachaPopup,
}
public enum UILayer
{
    UI1,
    UI2,
}
