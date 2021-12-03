using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    Dictionary<GearSlot, EquipmentOfHeroView> DicEquipmentOfHeroView = new Dictionary<GearSlot, EquipmentOfHeroView>();
    [SerializeField]
    List<EquipmentOfHeroView> listEquipmentOfHeroView = new List<EquipmentOfHeroView>();
    List<EquipmentData> currentEquipment = new List<EquipmentData>();
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        InitItem();
    }
    protected override void Start()
    {
        base.Start();
        Show();
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Show();

    }
    public void Show()
    {
        currentEquipment = EquipmentLogic.GetEquipmentOfHero(global.CurrentIdHero);
        foreach (EquipmentOfHeroView equipmentOfHero in DicEquipmentOfHeroView.Values) {
            equipmentOfHero.view.gameObject.SetActive(false);
            equipmentOfHero.backItem.SetActive(true);
        }
        foreach (EquipmentData data in currentEquipment)
        {
            EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(data.idConfig);
            DicEquipmentOfHeroView[data.gearSlot].backItem.SetActive(false);
            DicEquipmentOfHeroView[data.gearSlot].view.gameObject.SetActive(true);
            DicEquipmentOfHeroView[data.gearSlot].view.Show(data, config);
            
            
        }
    }
    private void InitItem()
    {
        foreach(EquipmentOfHeroView temp in listEquipmentOfHeroView)
        {
            if (!DicEquipmentOfHeroView.ContainsKey(temp.slot))
            {
                DicEquipmentOfHeroView.Add(temp.slot, temp);
            }
            else
            {
                DicEquipmentOfHeroView[temp.slot] = temp;
            }
        }
    }
    [System.Serializable]
    public struct EquipmentOfHeroView
    {
        public GearSlot slot;
        public EquipmentItemView view;
        public GameObject backItem;
    }
}
