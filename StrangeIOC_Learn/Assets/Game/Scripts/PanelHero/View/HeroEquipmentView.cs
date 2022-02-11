using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public OnViewHeroSignal OnViewHeroSignal { get; set; }

    private Dictionary<GearSlot, EquipmentOfHeroView> DicEquipmentOfHeroView = new Dictionary<GearSlot, EquipmentOfHeroView>();
    [SerializeField]
    private List<EquipmentOfHeroView> listEquipmentOfHeroView = new List<EquipmentOfHeroView>();
    private List<EquipmentData> currentEquipment = new List<EquipmentData>();
    [SerializeField]
    private HeroPreViewData heroPreViewData;
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
        heroPreViewData.Show(global.CurrentIdHero);
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
    [System.Serializable]
    public struct HeroPreViewData
    {
        public Image previewImage;
        public Text previewName;

        public void Show(int id)
        {
            previewImage.sprite = HeroLogic.GetHeroConfigById(id).preview;
            previewImage.SetNativeSize();
            previewName.text= HeroLogic.GetHeroConfigById(id).name;
        }
    }
}
