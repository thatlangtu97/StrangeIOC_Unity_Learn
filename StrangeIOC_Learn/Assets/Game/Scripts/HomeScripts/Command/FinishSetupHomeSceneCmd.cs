using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetupHomeSceneCmd : Command
{
    [Inject]
    public PopupManager popupManager { get; set; }
    [Inject]
    public ShowPanelHomeSignal ShowMainScenePopupSignal { get; set; }
    public override void Execute()
    {
        //throw new System.NotImplementedException();
    }
}
