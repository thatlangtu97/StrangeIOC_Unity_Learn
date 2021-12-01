using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetGameToolbar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Delete/Delete All Data")]
    private static void DeleteAll()
    {
        //DataManager.DeleteData("INVENTORY.binary");
        //DataManager.DeleteData("GameSessionData.binary");
        //DataManager.DeleteData("HeroUpgradeData.binary");
        DataManager.DeleteData("CurencyData.binary");
        DataManager.DeleteData("InventoryData.binary");
        DataManager.DeleteData("AllHeroData.binary");
        //DataManager.DeleteData("FirebaseData.binary");
        //DataManager.DeleteData("StoryTapeData.binary");
        //DataManager.DeleteData("Achievement.binary");
        //DataManager.DeleteData("NewEquipmentDataManager.binary");
        //DataManager.DeleteData("ForgeData.binary");
        //PlayerPrefs.DeleteAll();
    }
#endif
}
