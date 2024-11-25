using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class DataConfigManager : KBTemplate.Patterns.Singleton.Singleton<DataConfigManager>
{
    //[Header("Jsons")]
    //public TextAsset levelDataConfigJson;
    public override void OnCreatedSingleton()
    {
        base.OnCreatedSingleton();
        DontDestroyOnLoad(this);
        Init();
    }
    private void Init()
    {
    }
}
