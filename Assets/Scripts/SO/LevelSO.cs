using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;

    [Header("GameLevel")]
    public string scene;
    public int amountCoin;
    public GameObject gameLevel1;

    //[Header("Sounds")]
    //public AudioClip music;
    //[Range(0.0f, 1.0f)]
    //public float musicVolume;
}
