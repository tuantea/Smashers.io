using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataRuntimeManager : KBTemplate.Patterns.Singleton.Singleton<DataRuntimeManager>
{
    public static string DataPersistentDirectoryPath => Application.persistentDataPath + "/DT";
    public readonly static string DATA_RUNTIME_FILE_NAME = "DATA_RT.ngm";
    public readonly static string DATA_SHOP_FILE_NAME = "DATA_SHOP.ngm";

    [SerializeField] private SaveGameSO defaultSaveGameFile;
    [SerializeField] private ListItemSO defaultItemList;
    
    public DataRuntime DataRuntime { get; private set; }
    public WeaponShopRuntime WeaponShopRuntime { get; private set; }
    public override void OnCreatedSingleton()
    {
        base.OnCreatedSingleton();
        DontDestroyOnLoad(this);
        Init();
    }
    private void Init()
    {
        LoadDataRuntime();
        LoadDataShop();
    }
    #region data
    private void LoadDataRuntime()
    {
        DataRuntime = SimpleDataSave.LoadData<DataRuntime>(System.IO.Path.Combine(DataPersistentDirectoryPath, DATA_RUNTIME_FILE_NAME));
        if (DataRuntime == null)
        {
            if (defaultSaveGameFile)
                DataRuntime = defaultSaveGameFile.dataRuntime.DeepCopy(); 
            else
                DataRuntime = new DataRuntime();
        }
    }
    #endregion

    #region dataShop
    private void LoadDataShop()
    {
        WeaponShopRuntime = SimpleDataSave.LoadData<WeaponShopRuntime>(System.IO.Path.Combine(DataPersistentDirectoryPath, DATA_SHOP_FILE_NAME));
        if (WeaponShopRuntime == null)
        {
            if (defaultSaveGameFile)
                WeaponShopRuntime = defaultSaveGameFile.weaponShopRuntime.DeepCopy();            
            else
                WeaponShopRuntime = new WeaponShopRuntime(); 
        }
    }
    #endregion

    private void SaveDataRuntime()
    {
        SimpleDataSave.SaveData(DataRuntime, DATA_RUNTIME_FILE_NAME, DataPersistentDirectoryPath);
    }
    private void SaveDataShop()
    {
        SimpleDataSave.SaveData(WeaponShopRuntime, DATA_SHOP_FILE_NAME, DataPersistentDirectoryPath);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDataRuntime();
            SaveDataShop();
        }

    }
    private void OnApplicationQuit()
    {
        SaveDataRuntime();
        SaveDataShop();
    }
}
