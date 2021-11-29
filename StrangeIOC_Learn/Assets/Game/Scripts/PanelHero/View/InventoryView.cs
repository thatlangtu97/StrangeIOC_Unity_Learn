using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : View
{
    [Inject] public GlobalData global{ get; set; }
    public List<TabType> tabTypes = new List<TabType>();
    public List<EquipmentItemView> equipmentItemViews = new List<EquipmentItemView>();
    public int currentPage = 1;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        currentPage = 1;
        foreach (TabType temp in tabTypes)
        {
            temp.button.onClick.AddListener(() => Open(temp.slot));
        }
       
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Open(global.CurrentTab);
        Open(GearSlot.armor);
    }
    public void Open(GearSlot gearSlot)
    {
        List<EquipmentData> ListEquipment = DataManager.Instance.InventoryDataManager.GetAllEquipmentBySlot((int)gearSlot);

        int countEquipment = ListEquipment.Count;
        int indexStart = 0/*ListEquipment.Count % (equipmentItemViews.Count * currentPage)*/;
        for(int i= indexStart;i< equipmentItemViews.Count; i++)
        {
            if (i < countEquipment)
            {
                equipmentItemViews[i].gameObject.SetActive(true);
                equipmentItemViews[i].Show(ListEquipment[i], EquipmentLogic.GetEquipmentConfigById(ListEquipment[i].gearSlot, ListEquipment[i].idConfig));
            }
            else
            {
                equipmentItemViews[i].gameObject.SetActive(false);
            }
        }
        //Todo show ListEquipment;

    }
    public struct TabType 
    {
        public GearSlot slot;
        public Button button;
    }

}

