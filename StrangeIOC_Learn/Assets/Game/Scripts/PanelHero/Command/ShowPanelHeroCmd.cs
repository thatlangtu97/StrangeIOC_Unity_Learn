using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHeroCmd : AbsShowPanelCmd
{
    public override void Execute()
    {
        PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
        panelHeroView.ShowPanel();
    }
    //public override string GetInjectName()
    //{
    //    return typeof(PanelHeroView).ToString() ;
    //}
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_HERO;
    }
}
