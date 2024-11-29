using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : Shop
{
    public static ShopWeapon Instance { get; protected set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
        Debug.Log("ShopWeaponStart");
        tabManager.OnChangeValueShop += Instance_OnChangeValueShop;
        weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
        dataRuntime = DataRuntimeManager.Instance.DataRuntime;
        playerCoins = 1000;
    }
    public override void BuyItem()
    {
        TabManager.Instance.Test();
        base.BuyItem();
    }
}
