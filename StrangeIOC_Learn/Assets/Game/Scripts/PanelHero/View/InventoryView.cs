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
    List<EquipmentData> ListEquipment = new List<EquipmentData>();
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
        currentPage = 1;
        Open(global.CurrentTab);
    }
    public void Open(GearSlot gearSlot)
    {
        //Todo show ListEquipment;
        currentPage = 1;
        global.CurrentTab = gearSlot;
        ReloadPage();
        ShowButton();

    }
    public void NextPage()
    {
        if (equipmentItemViews.Count * (currentPage - 1) > ListEquipment.Count) return;
        currentPage += 1;

        ReloadPage();
    }
    public void PreviousPage()
    {
        currentPage -= 1;
        if (currentPage < 1) { currentPage = 1; }
        ReloadPage();
    }
    void ReloadPage()
    {
        ListEquipment = DataManager.Instance.InventoryDataManager.GetAllEquipmentBySlot((int)global.CurrentTab);
        
        int countEquipment = ListEquipment.Count;

        int indexStart = equipmentItemViews.Count * (currentPage - 1);
        Debug.Log(indexStart + " " + ListEquipment.Count);
        for (int i = 0; i < equipmentItemViews.Count; i++)
        {
            if (indexStart < countEquipment)
            {
                equipmentItemViews[i].gameObject.SetActive(true);
                equipmentItemViews[i].Show(ListEquipment[indexStart], EquipmentLogic.GetEquipmentConfigById(ListEquipment[indexStart].gearSlot, ListEquipment[indexStart].idConfig));
            }
            else
            {
                equipmentItemViews[i].gameObject.SetActive(false);
            }
            indexStart += 1;
        }
    }
    public void ShowButton()
    {
        foreach (TabType temp in tabTypes)
        {
            Color newColor = Color.white;
            if (global.CurrentTab == temp.slot)
            {                
                temp.text.color = new Vector4(newColor.r, newColor.g, newColor.b, 1f);
            }
            else
            {
                temp.text.color = new Vector4(newColor.r, newColor.g, newColor.b, .5f);
            }
        }
    }
    [System.Serializable]
    public struct TabType 
    {
        public GearSlot slot;
        public Button button;
        public Text text;
    }

}

