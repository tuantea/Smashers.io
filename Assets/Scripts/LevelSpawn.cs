using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    [SerializeField] private List<LevelSO> listLevelSO;
    public Transform levelParent;
    public void Start() 
    {
        int index = DataRuntimeManager.Instance.DataRuntime.Level() - 1;
       // Instantiate(listLevelSO[index].gameLevel1);
        Instantiate(listLevelSO[index]._levelPrefab, levelParent);
    }
}
