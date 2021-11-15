using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetupHomeSceneCmd : Command
{
    [Inject]
    public PopupManager popupManager { get; set; }
    [Inject]
    public ShowPanelHomeSignal showPanelHomeSignal { get; set; }
    public override void Execute()
    {
        PanelKey panelKey = popupManager.GetPanelAfterLoadHomeScene();
        switch (panelKey)
        {
            case PanelKey.PanelHome:
                showPanelHomeSignal.Dispatch();
                break;
        }
        popupManager.ResetPanelShowAfterLoadHomeScene();
    }
}
