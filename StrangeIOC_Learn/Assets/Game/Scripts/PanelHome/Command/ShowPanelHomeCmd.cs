using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelHomeCmd : Command
{
    public override void Execute()
    {
        
    }
    public string GetResourcePath()
    {
        return GameResourcePath.PANEL_HOME;
    }
}
