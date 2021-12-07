using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class CraftEquipmentView : View
{
    [Inject] public GlobalData global { get; set; }
    [Inject] public CraftEquipmentSignal CraftEquipmentSignal { get; set; }
    [SerializeField]
    List<EquipmentToCraftView> listEquipmentOfHeroView = new List<EquipmentToCraftView>();
    List<EquipmentData> currentEquipment = new List<EquipmentData>();

    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
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
        currentEquipment = EquipmentLogic.GetEquipmentOfCraft();
        foreach (EquipmentToCraftView equipmentOfCraft in listEquipmentOfHeroView)
        {
            equipmentOfCraft.view.gameObject.SetActive(false);
            equipmentOfCraft.backItem.SetActive(true);
        }
        int index = 0;
        foreach (EquipmentData data in currentEquipment)
        {
            if (index > listEquipmentOfHeroView.Count) continue;
            EquipmentConfig config = EquipmentLogic.GetEquipmentConfigById(data.idConfig);
            listEquipmentOfHeroView[index].backItem.SetActive(false);
            listEquipmentOfHeroView[index].view.gameObject.SetActive(true);
            listEquipmentOfHeroView[index].view.Show(data, config);
            index += 1;
        }
    }
    public void CraftItem()
    {
        if (EquipmentLogic.CanCraft())
        {
            CraftEquipmentSignal.Dispatch();
        }

    }
    [System.Serializable]
    public struct EquipmentToCraftView
    {
        public EquipmentItemView view;
        public GameObject backItem;
    }
}

