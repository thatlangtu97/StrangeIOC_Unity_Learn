using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    

    public Dictionary<GearSlot, EquipmentItemView> DicSlotOfHero = new Dictionary<GearSlot, EquipmentItemView>();
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
        foreach (EquipmentData data in currentEquipment)
        {
            EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(data.idConfig);

            DicSlotOfHero[data.gearSlot].gameObject.SetActive(true);
            DicSlotOfHero[data.gearSlot].Show(data, config);
        }

    }
    private void InitItem()
    {
        foreach(EquipmentOfHeroView temp in listEquipmentOfHeroView)
        {
            if (!DicSlotOfHero.ContainsKey(temp.slot))
            {
                DicSlotOfHero.Add(temp.slot, temp.view);
            }
            else
            {
                DicSlotOfHero[temp.slot] = temp.view;
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
