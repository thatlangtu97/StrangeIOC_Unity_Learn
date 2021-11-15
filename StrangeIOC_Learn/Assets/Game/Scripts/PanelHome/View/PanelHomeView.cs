using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHomeView : AbsPanelView
{
    [Inject]public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    public Button StaminaBtn;
    protected override void Start()
    {
        base.Start();
        StaminaBtn.onClick.AddListener( ()=>{ showPopupStaminaSignal.Dispatch(); });
    }
}
