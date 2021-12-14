using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelCraftCmd : AbsShowPanelCmd
{
    public override void Execute()
    {
        PanelCraftView panelCraftView = GetInstance<PanelCraftView>();
        panelCraftView.ShowPanelByCmd();
    }
    //public override string GetInjectName()
    //{
    //    return typeof(PanelHeroView).ToString() ;
    //}
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_CRAFT;
    }
}
