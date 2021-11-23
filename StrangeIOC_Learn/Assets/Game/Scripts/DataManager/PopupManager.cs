using System.Collections;
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
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
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
    public void AddPanel(PanelKey key, GameObject panel)
    {
        Debug.Log(key + " "+ panel);
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
    public void SetPanelAfterLoadHomeScene(PanelKey key , PopupKey popupkey)
    {
        panelKey = key;
        popupKey = popupkey;
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
                if(PanelDic[temp]!=null)
                    PanelDic[temp].GetComponent<AbsPanelView>().HidePanel();
            }
            else
            {
                if (PanelDic[temp] != null)
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
        if (currentPanel != PanelKey.PanelShop)
        {
            foreach (GameObject temp in ListPopupOfPanel[currentPanel]) 
            { 
                if (temp != null)
                {
                    if (temp.activeInHierarchy == true)
                    {
                        lastPopup = temp;
                    }
                }
            }
            if (lastPopup != null)
            {
                lastPopup.GetComponent<AbsPopupView>().HidePopup();
                return;
            }
        }
        //disable Panel
        foreach (PanelKey temp in PanelDic.Keys)
        {
            if (temp != PanelKey.PanelHome)
            {
                if (PanelDic[temp] != null)
                {
                    if (PanelDic[temp].activeInHierarchy == true)
                    {
                        PanelDic[temp].GetComponent<AbsPanelView>().HidePanel();
                    }
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
        if (!PopupDic.ContainsKey(key))
            return;

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

            //PopupDic[key].GetComponent<AbsPopupView>().ShowPopup();
            List<GameObject> TempPopupOfPanel = ListPopupOfPanel[currentPanel];
            foreach (GameObject tempPopup in TempPopupOfPanel)
            {
                if (tempPopup != PopupDic[key])
                {
                    if (tempPopup != null)
                        tempPopup.GetComponent<AbsPopupView>().HidePopup();
                }
                else
                {
                    if (tempPopup != null)
                        tempPopup.GetComponent<AbsPopupView>().ShowPopup();
                }
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
        if (!PopupDic.ContainsKey(keyPopup)) return;

        GameObject PopupGameObject = PopupDic[keyPopup];
        foreach (GameObject tempPopup in TempPopupOfPanel)
        {
            if(tempPopup != PopupGameObject)
            {
                if (tempPopup != null)
                    tempPopup.GetComponent<AbsPopupView>().HidePopup();
            }
            else
            {
                if (tempPopup != null)
                    tempPopup.GetComponent<AbsPopupView>().ShowPopup();
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
    NODE,
}
