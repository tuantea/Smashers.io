using Sirenix.OdinInspector;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Edit Tools/Data Runtime", fileName = "Data Runtime", order = 0)]
public class DataRuntimeSO : ScriptableObject
{
    public DataRuntime dynamicData;

    [ButtonGroup("Data")]
    public void SaveUserData()
    {
        if (EditorUtility.DisplayDialog("Save Data", "Are you sure you wanna save this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.SaveData(dynamicData, DataRuntimeManager.DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
        }
    }
    [ButtonGroup("Data")]
    public void ReloadUserData()
    {
        dynamicData = SimpleDataSave.LoadData<DataRuntime>(Path.Combine(DataRuntimeManager.DataPersistentDirectoryPath, DataRuntimeManager.DATA_RUNTIME_FILE_NAME));
    }
    [ButtonGroup("Data")]
    public void DeleteUserData()
    {
        if (EditorUtility.DisplayDialog("Delete Shop Data", "Are you sure you wanna delete this save?", "100% Sure", "Not now"))
        {
            SimpleDataSave.DeleteData<DataRuntime>(DataRuntimeManager.DATA_RUNTIME_FILE_NAME, DataRuntimeManager.DataPersistentDirectoryPath);
            dynamicData = new DataRuntime();
        }
    }
}
#endif
