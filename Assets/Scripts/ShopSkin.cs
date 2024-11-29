using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkin : Shop
{   
    public static ShopSkin Instance { get; protected set; }
    private void Awake()
    {
        if(Instance == null)
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
        tabManager.OnChangeValueShop += Instance_OnChangeValueShop;
        weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
        dataRuntime = DataRuntimeManager.Instance.DataRuntime;
        playerCoins = 1000;
    }
    public override void BuyItem()
    {
        tabManager.Test();
        base.BuyItem();
    }

}
