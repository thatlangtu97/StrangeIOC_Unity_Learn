using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelShopCmd : AbsShowPanelCmd
{
    public override void Execute()
    {
        PanelShopView panelShopView = GetInstance<PanelShopView>();
        panelShopView.ShowPanelByCmd();
    }
    public override string GetResourcePath()
    {
        return GameResourcePath.PANEL_SHOP;
    }
}
