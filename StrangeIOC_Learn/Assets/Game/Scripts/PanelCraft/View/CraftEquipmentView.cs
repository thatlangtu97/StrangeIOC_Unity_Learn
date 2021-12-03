using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class CraftEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    
    [SerializeField]
    List<EquipmentToCraftView> listEquipmentOfHeroView = new List<EquipmentToCraftView>();
    protected override void Awake()
    {
        base.Awake();
    }

    [System.Serializable]
    public struct EquipmentToCraftView
    {
        public EquipmentItemView view;
        public GameObject backItem;
    }
}

