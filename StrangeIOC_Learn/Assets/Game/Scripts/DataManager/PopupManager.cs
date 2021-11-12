using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager 
{
    public PanelKey panelKey;
    public PopupManager()
    {
        Debug.Log("tao moi popup manager");
    }
    public void Start()
    {
        Debug.Log("start popup singleton");
    }
    public void SetPopupAfterLoadHomeScene(PanelKey key)
    {
        panelKey = key;
    }
    public void ResetPopupAfterLoadHomeScene()
    {
        panelKey = PanelKey.PanelHome;
    }
    public PanelKey GetPopupAfterLoadHomeScene()
    {
        return panelKey;
    }
}
public enum PanelKey
{
    PanelHome,
}
