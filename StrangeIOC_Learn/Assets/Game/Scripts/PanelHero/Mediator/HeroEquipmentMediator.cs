using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class HeroEquipmentMediator : Mediator
{
    [Inject] public  HeroEquipmentView View { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }
    public override void OnRegister()
    {
        OnViewHeroSignal.AddListener(View.Show);
    }

    public override void OnRemove()
    {
        OnViewHeroSignal.RemoveListener(View.Show);
    }

    private void OnDestroy()
    {
        OnRemove();
    }
}
