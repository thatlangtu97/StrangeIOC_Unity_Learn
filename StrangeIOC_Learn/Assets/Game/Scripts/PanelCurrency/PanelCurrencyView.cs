using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class PanelCurrencyView : View
{
    [Inject] public PopupManager popupManager { get; set; }
    [Inject] public ShowPanelShopSignal showPanelShopSignal { get; set; }
    [Inject] public ShowPopupStaminaSignal showPopupStaminaSignal { get; set; }
    public Button ShopStaminaBtn, ShopGoldBtn, ShopGemBtn;

    public Doozy.Engine.UI.UIButton UIBtnShopStamina, UIBtnShopGold, UIBtnShopGem
        ;
    protected override void Awake()
    {
        base.CopyStart();
        /*
        if(ShopStaminaBtn!=null)
            ShopStaminaBtn.onClick.AddListener(() => { showPopupStaminaSignal.Dispatch(); });
        if (ShopGoldBtn != null)
        {
            ShopGoldBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
            ShopGoldBtn.onClick.AddListener(() => { ShowShop(PopupKey.ShopGoldPopup, PanelKey.PanelShop); });
        }
        if (ShopGemBtn != null)
        {
            ShopGemBtn.onClick.AddListener(() => { showPanelShopSignal.Dispatch(); });
            ShopGemBtn.onClick.AddListener(() => { ShowShop(PopupKey.ShopGemPopup, PanelKey.PanelShop); });
        }
        */
        ////
        if (UIBtnShopStamina != null)
            UIBtnShopStamina.OnClick.OnTrigger.Event.AddListener(() => { showPopupStaminaSignal.Dispatch(); });
        if (UIBtnShopGold != null)
        {
            UIBtnShopGold.OnClick.OnTrigger.Event.AddListener(() => { showPanelShopSignal.Dispatch(); });
            UIBtnShopGold.OnClick.OnTrigger.Event.AddListener(() => { ShowShop(PopupKey.ShopGoldPopup, PanelKey.PanelShop); });
        }
        if (UIBtnShopGem != null)
        {
            UIBtnShopGem.OnClick.OnTrigger.Event.AddListener(() => { showPanelShopSignal.Dispatch(); });
            UIBtnShopGem.OnClick.OnTrigger.Event.AddListener(() => { ShowShop(PopupKey.ShopGemPopup, PanelKey.PanelShop); });
        }

    }
    void ShowShop(PopupKey keyPopup , PanelKey panelKey)
    {
        popupManager.popupKey = keyPopup;
        popupManager.ShowPanel(panelKey);
    }

}
