using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
public class UnequipGearCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; set; }
    [Inject] public GlobalData global { get; set; }
    public override void Execute()
    {
        EquipmentLogic.UnEquipGear(equipmentData, global.CurrentIdHero);
    }
}
