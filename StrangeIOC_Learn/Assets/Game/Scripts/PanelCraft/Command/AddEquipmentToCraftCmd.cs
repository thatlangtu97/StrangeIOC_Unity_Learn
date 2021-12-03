using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class AddEquipmentToCraftCmd : Command
{
    [Inject] public EquipmentData equipmentData { get; }
    [Inject] public EquipmentConfig equipmentConfig { get; }
    public override void Execute()
    {
        
    }
}
