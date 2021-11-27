using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupDataManagerCmd : Command
{
    public override void Execute()
    {
        DataManager.Instance.CurrencyDataManager.LoadData();
    }
}
