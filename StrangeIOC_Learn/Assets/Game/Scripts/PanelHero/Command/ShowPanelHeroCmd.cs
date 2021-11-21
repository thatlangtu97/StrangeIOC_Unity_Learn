using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHeroCmd : AbsShowPanelCmd
{
    public override void Execute()
    {
        PanelHeroView panelHeroView = GetInstance<PanelHeroView>();
        panelHeroView.ShowPanelByCmd();
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_HERO;
    }
}
