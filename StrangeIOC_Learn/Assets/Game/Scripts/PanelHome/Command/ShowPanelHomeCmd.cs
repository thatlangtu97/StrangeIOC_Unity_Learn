using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHomeCmd : AbsShowPanelCmd
{

    public override void Execute()
    {
        PanelHomeView panelHomeView = GetInstance<PanelHomeView>();
        panelHomeView.ShowPanel();
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_HOME;
    }
}
