using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupDataManagerCmd : Command
{
    public override void Execute()
    {
        DataManager.Instance.CurrencyDataManager.LoadData();
        EquipmentLogic.Cache();
        HeroLogic.Cache();
        PrefabUtils.LoadPrefab(GameResourcePath.PANEL_CRAFT);
        PrefabUtils.LoadPrefab(GameResourcePath.PANEL_HERO);
        PrefabUtils.LoadPrefab(GameResourcePath.PANEL_SHOP);
        PrefabUtils.LoadPrefab(GameResourcePath.PANEL_HOME);
    }
}
