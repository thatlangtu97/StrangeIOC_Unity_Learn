using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{


    public const string DATA_PATH = "Game/Data/";
    private static DataManager instance;
    private List<IObjectDataManager> listObjectDataManager;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject dataManager = new GameObject();
                dataManager.name = "DataManager";
                instance = dataManager.AddComponent<DataManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //if (Instance != null && instance.GetInstanceID() != Instance.GetInstanceID())
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        listObjectDataManager = new List<IObjectDataManager>();
    }
    public static void DeleteData(string fileName)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public static T LoadData<T>(string fileName) where T : DataObject
    {
        T obj = default(T);
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            obj = formatter.Deserialize(fileStream) as T;
            fileStream.Close();
        }
        return obj;
    }
    public static void SaveData(DataObject obj, string fileName)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (obj != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, File.Exists(filePath) ? FileMode.Open : FileMode.Create);
            binaryFormatter.Serialize(fileStream, obj);
            fileStream.Close();
        }
    }
    CurrencyDataManager currencyDataManager;
    public CurrencyDataManager CurrencyDataManager
    {
        get
        {
            if (currencyDataManager == null)
            {
                currencyDataManager = new CurrencyDataManager();
                currencyDataManager.LoadData();
                listObjectDataManager.Add(currencyDataManager);

            }
            return currencyDataManager;
        }
    }


    //ForgeDataManager forgeDataManager;
    //public ForgeDataManager ForgeDataManager
    //{
    //    get
    //    {
    //        if (forgeDataManager == null)
    //        {
    //            forgeDataManager = new ForgeDataManager();
    //            forgeDataManager.LoadData();
    //            listObjectDataManager.Add(forgeDataManager);

    //        }
    //        return forgeDataManager;
    //    }
    //}
    //FireBaseDataManager fireBaseDataManager;
    //public FireBaseDataManager FireBaseDataManager
    //{
    //    get
    //    {
    //        if (fireBaseDataManager == null)
    //        {
    //            fireBaseDataManager = new FireBaseDataManager();
    //            fireBaseDataManager.LoadData();
    //            listObjectDataManager.Add(fireBaseDataManager);

    //        }
    //        return fireBaseDataManager;
    //    }
    //}

    //GameSessionDataManager gameSessionDataManager;
    //public GameSessionDataManager GameSessionDataManager
    //{
    //    get
    //    {
    //        if (gameSessionDataManager == null)
    //        {
    //            gameSessionDataManager = new GameSessionDataManager();
    //            gameSessionDataManager.LoadData();
    //            listObjectDataManager.Add(gameSessionDataManager);

    //        }
    //        return gameSessionDataManager;
    //    }
    //}

    //CurrencyDataManager currencyDataManager;
    //public CurrencyDataManager CurrencyDataManager
    //{
    //    get
    //    {
    //        if (currencyDataManager == null)
    //        {
    //            currencyDataManager = new CurrencyDataManager();
    //            currencyDataManager.LoadData();
    //            listObjectDataManager.Add(currencyDataManager);

    //        }
    //        return currencyDataManager;
    //    }
    //}
    //AchievementDatamanager achievementDatamanager;
    //public AchievementDatamanager AchievementDatamanager
    //{
    //    get
    //    {
    //        if (achievementDatamanager == null)
    //        {
    //            achievementDatamanager = new AchievementDatamanager();
    //            achievementDatamanager.LoadData();
    //            listObjectDataManager.Add(achievementDatamanager);

    //        }
    //        return achievementDatamanager;
    //    }
    //}
    //HeroDataManager heroDataManager;
    //public HeroDataManager HeroDataManager
    //{
    //    get
    //    {
    //        if (heroDataManager == null)
    //        {
    //            heroDataManager = new HeroDataManager();
    //            heroDataManager.LoadData();
    //            listObjectDataManager.Add(heroDataManager);

    //        }
    //        return heroDataManager;
    //    }
    //}
    //private InventoryDataManager equipmentDataManager;

    //public InventoryDataManager EquipmentDataManager
    //{
    //    get
    //    {
    //        if (equipmentDataManager == null)
    //        {
    //            equipmentDataManager = new InventoryDataManager();
    //            equipmentDataManager.LoadData();
    //            listObjectDataManager.Add(equipmentDataManager);

    //        }
    //        return equipmentDataManager;
    //    }
    //}
    ///*
    //private NewEquipmentDataManager newEquipmentDataManager;

    //public NewEquipmentDataManager NewEquipmentDataManager
    //{
    //    get
    //    {
    //        if (newEquipmentDataManager == null)
    //        {
    //            newEquipmentDataManager = new NewEquipmentDataManager();
    //            newEquipmentDataManager.LoadData();
    //            listObjectDataManager.Add(newEquipmentDataManager);

    //        }
    //        return newEquipmentDataManager;
    //    }
    //}
    //*/
    //private TalentTreeDataManager talentTreeDataManager;

    //public TalentTreeDataManager TalentTreeDataManager
    //{
    //    get
    //    {
    //        if (talentTreeDataManager == null)
    //        {
    //            talentTreeDataManager = new TalentTreeDataManager();
    //            talentTreeDataManager.LoadData();
    //            listObjectDataManager.Add(talentTreeDataManager);

    //        }
    //        return talentTreeDataManager;
    //    }
    //}

    //private StoryTapeManager storyTapeManager;

    //public StoryTapeManager StoryTapeManager
    //{
    //    get
    //    {
    //        if (storyTapeManager == null)
    //        {
    //            storyTapeManager = new StoryTapeManager();
    //            storyTapeManager.LoadData();
    //            listObjectDataManager.Add(storyTapeManager);

    //        }
    //        return storyTapeManager;
    //    }
    //}


    //public void CheckUpdateData() {
    //    /*
    //    string cur_version = Application.version;
    //    string[] subs_cur_version = cur_version.Split('.');
    //    string[] subs_old_version;
    //    string old_version = CurrencyDataManager.versionApplication;

    //    if (old_version != null)
    //    {
    //        if (old_version != "")
    //        {
    //            subs_old_version = old_version.Split('.');
    //            int minsize = subs_cur_version.Length < subs_old_version.Length ? subs_cur_version.Length : subs_old_version.Length;
    //            if (int.Parse(subs_old_version[0]) < 1)
    //            {
    //                for (int i = 0; i < minsize; i++)
    //                {

    //                    if (int.Parse(subs_cur_version[i]) > int.Parse(subs_old_version[i]))
    //                    {
    //                        CurrencyDataManager.versionApplication = cur_version;
    //                        // random lai item
    //                        equipmentDataManager.RandomAllItemToValidateNewBuild();
    //                        fireBaseDataManager.CloneNewDataFromOldData();
    //                        currencyDataManager.CloneNewDataFromOldData();
    //                        storyTapeManager.VerifyArenaData();
    //                        Debug.Log("random lai item");
    //                    }
    //                }
    //            }

    //        }
    //        else
    //        {

    //            // random lai item
    //            CurrencyDataManager.versionApplication = cur_version;
    //            equipmentDataManager. RandomAllItemToValidateNewBuild();
    //            fireBaseDataManager.CloneNewDataFromOldData();
    //            currencyDataManager.CloneNewDataFromOldData();
    //            storyTapeManager.VerifyArenaData();
    //            Debug.Log("random lai item");
    //        }
    //    }
    //    else
    //    {
    //        // random lai item
    //        CurrencyDataManager.versionApplication = cur_version;
    //        equipmentDataManager.RandomAllItemToValidateNewBuild();
    //        fireBaseDataManager.CloneNewDataFromOldData();
    //        currencyDataManager.CloneNewDataFromOldData();
    //        storyTapeManager.VerifyArenaData();
    //        Debug.Log("random lai item");
    //    }
    //    */
    //    string cur_version = Application.version;
    //    //if(currencyDataManager!=null)
    //    //if (currencyDataManager.versionApplication == "")
    //    //{

    //        if (currencyDataManager != null)
    //        {
    //            Debug.Log(1);
    //            CurrencyData new_CurrencyData = ReplaceOldVersion(currencyDataManager.getAllData());
    //            Debug.Log(2);
    //            listObjectDataManager.Remove(currencyDataManager);
    //            currencyDataManager.DeleteData();
    //            Debug.Log(3);

    //            currencyDataManager = new CurrencyDataManager();
    //            currencyDataManager.rePlaceAllData(new_CurrencyData);
    //            currencyDataManager.LoadData();
    //            listObjectDataManager.Add(currencyDataManager);
    //            Debug.Log(4);
    //        }
    //        else
    //        {
    //            currencyDataManager = new CurrencyDataManager();
    //            currencyDataManager.setupValueDefault();
    //            currencyDataManager.LoadData();
    //            listObjectDataManager.Add(currencyDataManager);
    //            Debug.Log("null currencyDataManager");
    //        }

    //        if (equipmentDataManager != null)
    //        {
    //            Debug.Log(6);
    //            Inventory new_Inventory = ReplaceOldVersion(equipmentDataManager.getAllData());
    //            Debug.Log(7);
    //            listObjectDataManager.Remove(equipmentDataManager);
    //            equipmentDataManager.DeleteData();
    //            Debug.Log(8);
    //            equipmentDataManager = new InventoryDataManager();
    //            equipmentDataManager.rePlaceAllData(new_Inventory);
    //            equipmentDataManager.LoadData();
    //            listObjectDataManager.Add(equipmentDataManager);
    //            Debug.Log(9);
    //        }
    //        else
    //        {
    //            equipmentDataManager = new InventoryDataManager();
    //            equipmentDataManager.LoadData();
    //            listObjectDataManager.Add(equipmentDataManager);
    //            Debug.Log("null equipmentDataManager");
    //        }
    //        if (fireBaseDataManager != null)
    //        {
    //            Debug.Log(10);
    //            FireBaseData new_FireBaseData = ReplaceOldVersion(fireBaseDataManager.getAllData());
    //            Debug.Log(11);
    //            listObjectDataManager.Remove(fireBaseDataManager);
    //            fireBaseDataManager.DeleteData();
    //            Debug.Log(12);
    //            fireBaseDataManager = new FireBaseDataManager();
    //            fireBaseDataManager.rePlaceAllData(new_FireBaseData);
    //            fireBaseDataManager.LoadData();
    //            listObjectDataManager.Add(fireBaseDataManager);
    //            Debug.Log(13);
    //        }
    //        else
    //        {

    //            fireBaseDataManager = new FireBaseDataManager();
    //            fireBaseDataManager.setupValueDefault();
    //            fireBaseDataManager.LoadData();
    //            listObjectDataManager.Add(fireBaseDataManager);
    //            Debug.Log("null fireBaseDataManager");
    //        }
    //        //if (storyTapeManager != null)
    //        //{
    //        //    Debug.Log(14);
    //        //    StoryTapeData new_StoryTapeData = ReplaceOldVersion(storyTapeManager.getAllData());
    //        //    Debug.Log(15);
    //        //    listObjectDataManager.Remove(storyTapeManager);
    //        //    storyTapeManager.DeleteData();
    //        //    Debug.Log(16);
    //        //    storyTapeManager = new StoryTapeManager();
    //        //    storyTapeManager.rePlaceAllData(new_StoryTapeData);
    //        //    storyTapeManager.LoadData();
    //        //    listObjectDataManager.Add(storyTapeManager);
    //        //    Debug.Log(17);
    //        //}
    //        //else
    //        //{
    //        //    storyTapeManager = new StoryTapeManager();

    //        //    storyTapeManager.IniData();
    //        //    storyTapeManager.LoadData();
    //        //    listObjectDataManager.Add(storyTapeManager);
    //        //    Debug.Log("null storyTapeManager");
    //        //}






    //        if (achievementDatamanager == null)
    //        {
    //            achievementDatamanager = new AchievementDatamanager();
    //            achievementDatamanager.setupValueDefault();
    //            listObjectDataManager.Add(achievementDatamanager);
    //        }
    //    //}


    //}
    //CurrencyData ReplaceOldVersion(CurrencyData oldData) {
    //    CurrencyData newData = new CurrencyData();
    //    DateTime timeNow = DateTime.Now;
    //    newData.versionApplication = Application.version;

    //    newData.firstX2Gem = oldData.firstX2Gem;
    //    newData.levelAccount = oldData.levelAccount;
    //    newData.expLevelAccount = oldData.expLevelAccount;
    //    newData.expUpgradeHero = oldData.expUpgradeHero;
    //    newData.gold = oldData.gold;
    //    newData.gem = oldData.gem;
    //    newData.gemInPot = 0;
    //    newData.stamina = oldData.stamina;
    //    newData.labyrinthToken = oldData.labyrinthToken;
    //    newData.maxStamina = 100;
    //    newData.KeyFreeWoodent = 0;
    //    newData.KeyFreeSilver = oldData.KeyFreeSilver;
    //    newData.KeyFreeGoldent = oldData.KeyFreeGoldent;
    //    newData.KeyWoodent = oldData.KeyWoodent;
    //    newData.KeySilver = oldData.KeySilver;
    //    newData.KeyGoldent = oldData.KeyGoldent;
    //    newData.crystalWeapon = oldData.crystalWeapon;
    //    newData.crystalArmor = oldData.crystalArmor;
    //    newData.crystalRing = oldData.crystalRing;
    //    newData.countLibrary = oldData.countLibrary;
    //    newData.keyTutorial = oldData.keyTutorial;
    //    newData.saveStepTutorial = oldData.saveStepTutorial;
    //    newData.noticedGold = oldData.noticedGold;
    //    newData.noticedStamina = oldData.noticedStamina;
    //    newData.noticedGem = oldData.noticedGem;
    //    newData.isOpenKeyFree = oldData.isOpenKeyFree;

    //    newData.spaceTimeWatchAds = oldData.spaceTimeWatchAds;
    //    newData.countAdsWatchGem = oldData.countAdsWatchGem;
    //    newData.countAdsWatchGold = oldData.countAdsWatchGold;
    //    newData.countAdsWatchStamina = oldData.countAdsWatchStamina;
    //    newData.countBuyStamina = oldData.countBuyStamina;
    //    newData.countAdsRevive = oldData.countAdsRevive;
    //    newData.maxAdsWatchStamina = oldData.maxAdsWatchStamina;
    //    newData.maxAdsWatchGold = oldData.maxAdsWatchGold;
    //    newData.maxAdsWatchGem = oldData.maxAdsWatchGem;
    //    newData.maxBuyStamina = oldData.maxBuyStamina;
    //    newData.maxAdsRevive = oldData.maxAdsRevive;
    //    newData.spaceTimeRecieveStamina_Minute = oldData.spaceTimeRecieveStamina_Minute;
    //    newData.spaceTimeRecieveKeyFreeWoodent = oldData.spaceTimeRecieveKeyFreeWoodent;// chua cho mo ruong go
    //    newData.spaceTimeRecieveKeyFreeSilver = oldData.spaceTimeRecieveKeyFreeSilver;
    //    newData.spaceTimeRecieveKeyFreeGoldent = oldData.spaceTimeRecieveKeyFreeGoldent;
    //    newData.isSetup = oldData.isSetup;
    //    newData.adsPass = oldData.adsPass;

    //    newData.lastTimeRecieveKeyFreeWoodent = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveKeyFreeSilver = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveKeyFreeGoldent = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeReceiveStamina = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveAdsStamina = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveAdsGold = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveAdsGem = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeResetLibrary = new DateTimeReplace(timeNow.AddDays(-2));
    //    newData.lastTimeResetBuyStamina = new DateTimeReplace(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Millisecond);
    //    newData.lastTimeRecieveForgePackAds = new DateTimeReplace[10];
    //    for (int i = 0; i < 10; i++)
    //    {

    //        newData.lastTimeRecieveForgePackAds[i] = new DateTimeReplace(timeNow.AddDays(-1));
    //    }
    //    return newData;
    //}
    //Inventory ReplaceOldVersion(Inventory oldData) {
    //    Inventory newData = new Inventory();
    //    newData.equipmentDatas = new List<EquipmentData>();
    //    foreach (EquipmentData data in oldData.equipmentDatas)
    //    {
    //        newData.equipmentDatas.Add(CloneNewDataFromOldData(data.idEquipment, (int)data.rarity, (int)data.geartype, data.level, data.isEquipedByHeroID, data.idOfHero));
    //    }



    //    return newData;

    //}
    //StoryTapeData ReplaceOldVersion(StoryTapeData oldData)
    //{
    //    StoryTapeData newData = new StoryTapeData();
    //    newData.arenaClearedDic = new Dictionary<int, int>();
    //    newData.arenaShowStoryFinished = new Dictionary<int, bool>();
    //    newData.situationFinished = new Dictionary<int, bool>();
    //    newData.bossRaidClearedDic = new Dictionary<int, bool>();
    //    ArenaConfig arenaConfigData = ScriptableObjectData.ArenaConfig;
    //    foreach (ArenaData arena in arenaConfigData.arenaList)
    //    {

    //        if (!newData.arenaClearedDic.ContainsKey(arena.arenaID))
    //        {
    //            newData.arenaClearedDic.Add(arena.arenaID,(int)GameMode.Story_normal - 1);
    //        }

    //        bool newNode = false;
    //        if (arena.arenaID > 1)
    //            newNode = true;
    //    }
    //    newData.indexNewArenaUnlock = ScriptableObjectData.ArenaConfig.arenaList[0].arenaID;
    //    ArenaConfig bossConfigData = ScriptableObjectData.BossConfig;
    //    foreach (ArenaData arena in bossConfigData.arenaList)
    //    {

    //        if (!newData.bossRaidClearedDic.ContainsKey(arena.arenaID))
    //        {
    //            newData.bossRaidClearedDic.Add(arena.arenaID, false);
    //        }
    //    }
    //    return newData;
    //}
    //FireBaseData ReplaceOldVersion(FireBaseData oldData)
    //{
    //    FireBaseData newData = new FireBaseData();
    //    newData.daily_login_count = oldData.daily_login_count;
    //    newData.real_money_spent = "";
    //    newData.playing_time = oldData.playing_time;
    //    newData.match_arena_count = oldData.match_arena_count;
    //    newData.highest_wave = oldData.highest_wave;

    //    newData.rewardItemDaily = oldData.rewardItemDaily;
    //    newData.rewardX2ItemDaily = oldData.rewardX2ItemDaily;
    //    newData.try_count = oldData.try_count;
    //    //for (int i = 0; i < 30; i++)
    //    //{
    //    //    firebaseData.try_count[i, 0] = 1;
    //    //}

    //    newData.match_total_count = oldData.match_total_count;
    //    newData.highest_arena = oldData.highest_arena;
    //    newData.highest_node = oldData.highest_node;
    //    newData.issetup = oldData.issetup;
    //    newData.last_daily_login_count = oldData.last_daily_login_count;
    //    newData.last_playing_time = oldData.last_playing_time;
    //    newData.last_previous_screen_time = oldData.last_previous_screen_time;

    //    DateTime timenow = DateTime.Now;

    //    newData.new_last_daily_login_count = new DateTimeReplace(oldData.last_daily_login_count);
    //    newData.new_last_playing_time = new DateTimeReplace(oldData.last_playing_time);
    //    newData.new_previous_screen_time = new DateTimeReplace(oldData.last_previous_screen_time);

    //    newData.spin_reward_id = "";
    //    newData.spin_reward_quantity = "";
    //    newData.spin_reward_name = "";
    //    return newData;
    //}


    ///// <summary>
    ////Random Congif Equipment
    ///// <returns></returns>
    //public EquipmentConfig GetBaseEquipMentConfig(GEARSLOT slot, int idConfig, int idOFhero)
    //{
    //    List<EquipmentConfig> tempList = new List<EquipmentConfig>();
    //    switch (slot)
    //    {
    //        case GEARSLOT.Armor:
    //            //return ScriptableObjectData.EquipmentConfigCollection.armorCollection[idConfig];
    //            tempList = ScriptableObjectData.EquipmentConfigCollection.armorCollection;
    //            break;
    //        case GEARSLOT.Ring:
    //            //return ScriptableObjectData.EquipmentConfigCollection.ringCollection[idConfig];
    //            tempList = ScriptableObjectData.EquipmentConfigCollection.ringCollection;
    //            break;
    //        case GEARSLOT.Weapon:
    //            //return ScriptableObjectData.EquipmentConfigCollection.weaponCollection[idConfig];
    //            tempList = ScriptableObjectData.EquipmentConfigCollection.weaponCollection;
    //            break;
    //        case GEARSLOT.Custom:
    //            tempList = ScriptableObjectData.EquipmentConfigCollection.customCollection;
    //            break;

    //    }
    //    foreach (EquipmentConfig item in tempList)
    //    {
    //        if (item.gearSlot != GEARSLOT.Ring)
    //        {
    //            if (item.idOfHero == idOFhero & idConfig == item.id)
    //            {
    //                return item;
    //            }
    //        }
    //        else
    //        {
    //            if (idConfig == item.id)
    //            {
    //                return item;
    //            }
    //        }
    //    }

    //    return null;
    //}
    //public int randomIDConfig(GEARSLOT slot, int idOFhero)
    //{

    //    List<EquipmentConfig> tempList = new List<EquipmentConfig>();
    //    List<EquipmentConfig> tempListRandomOfHero = new List<EquipmentConfig>();
    //    switch (slot)
    //    {
    //        case GEARSLOT.Armor:

    //            tempList = ScriptableObjectData.EquipmentConfigCollection.armorCollection;
    //            break;
    //        case GEARSLOT.Ring:

    //            tempList = ScriptableObjectData.EquipmentConfigCollection.ringCollection;
    //            break;
    //        case GEARSLOT.Weapon:

    //            tempList = ScriptableObjectData.EquipmentConfigCollection.weaponCollection;
    //            break;
    //        case GEARSLOT.Custom:

    //            tempList = ScriptableObjectData.EquipmentConfigCollection.customCollection;
    //            break;
    //    }

    //    foreach (EquipmentConfig item in tempList)
    //    {
    //        if (item.gearSlot != GEARSLOT.Ring)
    //        {
    //            if (item.idOfHero == idOFhero)
    //            {
    //                tempListRandomOfHero.Add(item);
    //            }
    //        }
    //        else
    //        {
    //            tempListRandomOfHero.Add(item);
    //        }
    //    }

    //    int rd = UnityEngine.Random.Range(0, tempListRandomOfHero.Count);
    //    //Debug.Log(" random item "+ slot .ToString()+" " + rd +" " + tempListRandomOfHero.Count );

    //    return tempListRandomOfHero[rd].id;

    //}
    //public EquipmentData CloneNewDataFromOldData(int idEquipment, int rare, int slot, int level, int isEquipedByHeroID, int idOfHero)
    //{
    //    EquipmentData newItem = new EquipmentData();
    //    int GearID = randomIDConfig((GEARSLOT)slot, idOfHero);
    //    EquipmentConfig baseItemConfig = GetBaseEquipMentConfig((GEARSLOT)slot, GearID, idOfHero);
    //    newItem.rarity = (RARITY)rare;
    //    newItem.geartype = (GEARSLOT)slot;
    //    newItem.level = level;
    //    newItem.idConfig = baseItemConfig.id;
    //    newItem.isEquipedByHeroID = isEquipedByHeroID;
    //    newItem.idEquipment = idEquipment;
    //    newItem.idOfHero = baseItemConfig.idOfHero;
    //    newItem.isNewItem = true;

    //    newItem.price = baseItemConfig.price[rare];
    //    newItem.upgradePrice = 0;
    //    newItem.arrMainAtribute = new AtributeEquipment[baseItemConfig.mainAttribute.Count];
    //    newItem.arrForge = new List<int>();
    //    newItem.indexOutfit = baseItemConfig.index;
    //    newItem.indexItemEquip = -1;
    //    for (int i = 0; i < baseItemConfig.mainAttribute.Count; i++)
    //    {
    //        newItem.arrMainAtribute[i] = RandomStat(baseItemConfig.mainAttribute[i], newItem.rarity);
    //    }
    //    return newItem;

    //}
    //public AtributeEquipment RandomStat(AtributeConfig config, RARITY rarity)
    //{

    //    AtributeEquipment newAtribute = new AtributeEquipment();
    //    newAtribute.attType = config.attType;
    //    newAtribute.value = UnityEngine.Random.Range(config.valueMinRandom[(int)rarity], config.valueMaxRandom[(int)rarity]);
    //    newAtribute.baseRandomValue = newAtribute.value;
    //    newAtribute.isMainStat = config.isMainStat;
    //    return newAtribute;
    //}
    //public static void ReplaceDataJsonFromServer(DataGameToFireBaseRealtime data) {
    //    if (data != null)
    //    {
    //        Instance.CurrencyDataManager.rePlaceAllData(data.dataCurrency);
    //        Instance.EquipmentDataManager.rePlaceAllData(data.dataInventory);
    //        Instance.ForgeDataManager.rePlaceAllData(data.dataForgeListItem);
    //        Instance.HeroDataManager.rePlaceAllData(data.dataHeroDataAll);
    //        Instance.StoryTapeManager.rePlaceAllData(data.dataStoryTapeData);
    //        Instance.FireBaseDataManager.rePlaceAllData(data.dataFireBase);
    //        Instance.AchievementDatamanager.rePlaceAllData(data.dataAchievement);
    //        Debug.Log("Data Online replace");
    //    }
    //    else
    //    {
    //        Debug.Log("Data Online bi null");
    //    }
    //}
    //public void ReplaceDataJsonFromServer(string json)
    //{
    //    UserDataOnline CloudData = JsonConvert.DeserializeObject<UserDataOnline>(json);
    //    DataGameToFireBaseRealtime data = CloudData.data;
    //    if (data != null)
    //    {
    //        Debug.Log("Data Online replace");
    //        Instance.CurrencyDataManager.rePlaceAllData(data.dataCurrency);
    //        Instance.EquipmentDataManager.rePlaceAllData(data.dataInventory);
    //        Instance.ForgeDataManager.rePlaceAllData(data.dataForgeListItem);
    //        Instance.HeroDataManager.rePlaceAllData(data.dataHeroDataAll);
    //        Instance.StoryTapeManager.rePlaceAllData(data.dataStoryTapeData);
    //        Instance.FireBaseDataManager.rePlaceAllData(data.dataFireBase);
    //        Instance.AchievementDatamanager.rePlaceAllData(data.dataAchievement);

    //    }
    //    else
    //    {
    //        Debug.Log("Data Online bi null");
    //    }


    //}
}
public interface IObjectDataManager
{
    void LoadData();

    void SaveData();

    void DeleteData();
}
[System.Serializable]
public class DataObject
{

}

//[System.Serializable]
//public class DataGameToFireBaseRealtime
//{
//    public CurrencyData dataCurrency;
//    public Inventory dataInventory;
//    public ForgeListItem dataForgeListItem;
//    public HeroDataAll dataHeroDataAll;
//    public StoryTapeData dataStoryTapeData;
//    public FireBaseData dataFireBase;
//    public AchievementData dataAchievement;
//    public DataGameToFireBaseRealtime()
//    {

//    }
//    public DataGameToFireBaseRealtime(DataManager data)
//    {
//        dataCurrency = data.CurrencyDataManager.getAllData();
//        dataInventory = data.EquipmentDataManager.getAllData();
//        dataForgeListItem = data.ForgeDataManager.getAllData();
//        dataHeroDataAll = data.HeroDataManager.getAllData();
//        dataStoryTapeData = data.StoryTapeManager.getAllData();
//        dataFireBase = data.FireBaseDataManager.getAllData();
//        dataAchievement = data.AchievementDatamanager.getAllData();
//    }
//}
//public static class JsonHelper_FireBase
//{
//    public static T[] FromJson<T>(string json)
//    {
//        Wrapper_FireBase<T> wrapper = JsonUtility.FromJson<Wrapper_FireBase<T>>(json);
//        return wrapper.Items;
//    }

//    public static string ToJson<T>(T[] array)
//    {
//        Wrapper_FireBase<T> wrapper = new Wrapper_FireBase<T>();
//        wrapper.Items = array;

//        return JsonUtility.ToJson(wrapper);
//    }

//    public static string ToJson<T>(T[] array, bool prettyPrint)
//    {
//        Wrapper_FireBase<T> wrapper = new Wrapper_FireBase<T>();
//        wrapper.Items = array;
//        return JsonUtility.ToJson(wrapper, prettyPrint);
//    }

//    [Serializable]
//    private class Wrapper_FireBase<T>
//    {
//        public T[] Items;

//    }
//}
//[System.Serializable] 
//public class DataOnlineResult
//{
//    public DataGameToFireBaseRealtime dataCloud;
//    public DataGameToFireBaseRealtime dataDevice;
//}