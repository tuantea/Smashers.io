using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Weapon Shop Runtime", fileName = "Weapon Shop Runtime", order = 0)]
public class WeaponShopRuntimeEditor : ScriptableObject
{
    public WeaponShopRuntime weaponShopData;

    [ButtonGroup("Data")]
    public void SaveUserData()
    {
        if (EditorUtility.DisplayDialog("Save Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(weaponShopData, DataRuntimeManager.DATA_SHOP_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
        }
    }
    [ButtonGroup("Data")]
    public void ReloadUserData()
    {
        weaponShopData = SimpleDataSave.LoadData<WeaponShopRuntime>(Path.Combine(DataRuntimeManager.DataPersistentDirectoryPath, DataRuntimeManager.DATA_SHOP_FILE_NAME));
    }
    [ButtonGroup("Data")]
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Shop Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<WeaponShopRuntime>(DataRuntimeManager.DATA_SHOP_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            weaponShopData = new WeaponShopRuntime();
        }
    }
}
#endif
