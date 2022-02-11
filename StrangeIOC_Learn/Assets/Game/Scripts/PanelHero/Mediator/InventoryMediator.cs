using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class InventoryMediator : Mediator
{
    [Inject] public InventoryView View { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    public override void OnRegister()
    {
        OnViewHeroSignal.AddListener(View.ReloadPage);
    }

    public override void OnRemove()
    {
        OnViewHeroSignal.RemoveListener(View.ReloadPage);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
