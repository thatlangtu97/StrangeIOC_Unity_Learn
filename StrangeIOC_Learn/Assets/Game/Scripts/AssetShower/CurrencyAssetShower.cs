using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyAssetShower : View
{
    // Start is called before the first frame update
    [Inject] public GlobalData globalData { get; set; }
    public CurrencyType currencyType;
    public Text ValueText;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        Setup();
        globalData.AddCurrencyAssetShower(currencyType, this);

    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup()
    {
        switch (currencyType) {
            case CurrencyType.gold:
                ValueText.text = $"{DataManager.Instance.CurrencyDataManager.gold}";
                break;
            case CurrencyType.gem:
                ValueText.text = $"{DataManager.Instance.CurrencyDataManager.gem}";
                break;
            case CurrencyType.stamina:
                ValueText.text = $"{DataManager.Instance.CurrencyDataManager.stamina}/{DataManager.Instance.CurrencyDataManager.maxStamina}";
                break;
        }
    }
}
