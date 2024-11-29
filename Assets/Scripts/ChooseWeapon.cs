using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public static ChooseWeapon Instance { get; private set; }
    [SerializeField] private Transform ListSkin;
    public event EventHandler<EventArgs> OnChangeHammer;
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
        ShopWeapon.Instance.OnChangeWeapon += Shop_OnChangeWeapon;
        ListSkin.GetChild(index).gameObject.SetActive(true);
    }

    private void Shop_OnChangeWeapon(object sender, Shop.EventChangeSkin e)
    {
        ListSkin.GetChild(index).gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetWeapon(e.indexSkin);
        index=e.indexSkin;
        Debug.Log("ChangeWeapon" + e.indexSkin);
        ListSkin.GetChild(e.indexSkin).gameObject.SetActive(true);
    }

    public void OnChangeSkin(int indexChoose)
    {
        Debug.Log("OnchageSkinWeapon");
        ListSkin.GetChild(index).gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetWeapon(indexChoose);
        index = indexChoose;
        ListSkin.GetChild(indexChoose).gameObject.SetActive(true);
        OnChangeHammer?.Invoke(this, EventArgs.Empty);
    }
}
