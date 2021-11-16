using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroView : AbsPanelView
{
    public Button backBtn;
    protected override void Start()
    {
        base.Start();
        backBtn.onClick.AddListener(()=>popupManager.BackPanel());
    }
}
