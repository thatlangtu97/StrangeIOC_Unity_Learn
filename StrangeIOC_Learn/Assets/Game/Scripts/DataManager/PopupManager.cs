using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager 
{
    public PanelKey panelKey { get; set; }
    public Dictionary<UILayer, Transform> UIDic = new Dictionary<UILayer, Transform>();
    public Dictionary<PanelKey, GameObject> GameObjectDic = new Dictionary<PanelKey, GameObject>(); 
    public PopupManager()
    {
    }
    public bool CheckContainPanel(PanelKey key)
    {
        if (GameObjectDic.ContainsKey(key))
        {
            return true;
        }
        return false;
    }
    public GameObject GetPanelByPanelKey(PanelKey key)
    {
        if (GameObjectDic.ContainsKey(key))
        {
            return GameObjectDic[key];
        }
        
        return null;
    }
    public void AddPanel(PanelKey key , GameObject panel)
    {
        if (GameObjectDic.ContainsKey(key))
        {
            GameObjectDic[key] = panel;
        }
        else
        {
            GameObjectDic.Add(key, panel);
        }
    }
    public void AddUILayer(UILayer layer,Transform transform)
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
    public void SetPopupAfterLoadHomeScene(PanelKey key)
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
        panelKey = PanelKey.PanelHome;
    }
}
public enum PanelKey
{
    PanelHome,
}
public enum UILayer
{
    UI1,
    UI2,
}
