using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
public class EquipGearCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; set; }
    [Inject] public GlobalData global { get; set; }
    public override void Execute()
    {
        EquipmentLogic.EquipGear(equipmentData, global.CurrentIdHero);
    }
}
