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
    public Dictionary<PopupKey, AbsPopupView> PopupDic = new Dictionary<PopupKey, AbsPopupView>();
    public PanelKey BasePabelKey;
    public Dictionary<string, IEnumerator> listActionDelay = new Dictionary<string, IEnumerator>();
    //public Dictionary<PanelKey, List<GameObject>> ListPopupOfPanel = new Dictionary<PanelKey, List<GameObject>>();

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
        if (PanelDic.ContainsKey(key))
        {
            PanelDic[key] = panel;
        }
        else
        {
            PanelDic.Add(key, panel);
        }
    }
    public void SetPanelAfterLoadHomeScene(PanelKey key , PopupKey popupkey)
    {
        panelKey = key;
        popupKey = popupkey;
        BasePabelKey = PanelKey.PanelHome;
    }
    
    public void ResetPanelAfterLoadHomeScene()
    {
        panelKey = PanelKey.PanelHome;
        BasePabelKey = PanelKey.PanelHome;
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
        panelKey = key;
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
        AbsPopupView lastPopup = null;
        foreach (AbsPopupView temp in PopupDic.Values)
        {
            if (temp != null)
            {
                if (temp.gameObject.activeInHierarchy == true)
                {
                    lastPopup = temp;
                }
            }
        }
        if (lastPopup != null)
        {
            lastPopup/*.GetComponent<AbsPopupView>()*/.HidePopup();
            return;
        }
        //disable Panel
        foreach (PanelKey temp in PanelDic.Keys)
        {
            if (temp != BasePabelKey)
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
        if(BasePabelKey == PanelKey.PanelHome)
        {
            showPanelHomeSignal.Dispatch();
            panelKey = PanelKey.PanelHome;
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
    public AbsPopupView GetPopupByPopupKey(PopupKey key)
    {
        if (PopupDic.ContainsKey(key))
        {
            return PopupDic[key];
        }

        return null;
    }
    public void AddPopup(PopupKey key, AbsPopupView panel)
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
    public void ResetPopup()
    {
        //popupKey = PopupKey.Node;
    }
    public void ShowPopup(PopupKey key)
    {
        if (!PopupDic.ContainsKey(key))
            return;
        if (PopupDic[key] != null)
            PopupDic[key]/*.GetComponent<AbsPopupView>()*/.ShowPopup();
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
    Node=0,
    StaminaPopup = 1,
    EquipmentHeroDetailLeft = 2,
    EquipmentHeroDetailRight = 3,
    EquipmentCraftDetailLeft = 4,
    EquipmentCraftDetailRight = 5,
    ShopGoldPopup = 6,
    ShopGemPopup =7,
    ShopGachaPopup =8,
    GachaPopup =9,
    CraftPopup =10,
    GachaInfoPopup =11,
}
public enum UILayer
{
    UI1,
    UI2,
    NODE,
}
