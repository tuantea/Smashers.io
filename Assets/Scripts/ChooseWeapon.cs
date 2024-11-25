using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public static ChooseWeapon Instance { get; private set; }
    [SerializeField] private Transform ListSkin;
    int index;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        index = DataRuntimeManager.Instance.DataRuntime.Skin();
        Shop.Instance.OnChangeSkin += Shop_OnChangeSkin;
        ListSkin.GetChild(index).gameObject.SetActive(true);
    }

    private void Shop_OnChangeSkin(object sender, Shop.EventChangeSkin e)
    {
        ListSkin.GetChild(index).gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetSkin(e.indexSkin);
        index=e.indexSkin;
        ListSkin.GetChild(e.indexSkin).gameObject.SetActive(true);
    }

    public void OnChangeSkin(int indexChoose)
    {
        ListSkin.GetChild(index).gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetSkin(indexChoose);
        index = indexChoose;
        ListSkin.GetChild(indexChoose).gameObject.SetActive(true);
    }
}
