using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Shop : MonoBehaviour
{
    public static Shop Instance {  get; private set; }
    DataRuntime dataRuntime;
    WeaponShopRuntime weaponShopRuntime;
    [SerializeField] private ListItemSO listItemSO;
    private List<ItemSO> itemSOList;
    int playerCoins;
    public event EventHandler<EventChangeSkin> OnChangeSkin;
    public class EventChangeSkin:EventArgs
    {
        public int indexSkin;
    }
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
        TabManager.Instance.OnChangeValueShop += Instance_OnChangeValueShop; ;
        weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
        dataRuntime = DataRuntimeManager.Instance.DataRuntime;
        //playerCoins = dataRuntime.Gold();
        playerCoins = 1000;
        //itemSOList=new List<ItemSO>();
        //List<int> listWeaponOwned=dataRuntime.GetListWeaponsOwned();
        //bool[] itemPool = weaponShopRuntime.GetListItem(); 
        //if (itemPool != null && itemPool.Length > 0)
        //{
        //    for (int i = 0; i < itemPool.Length; i++)
        //    {
        //        if (!itemPool[i])
        //        {
        //           itemSOList.Add(listItemSO.itemSOList[i]);
        //        }
        //    }
        //}
    }

    private void Instance_OnChangeValueShop(object sender, TabManager.ChangeValueShopEvent e)
    {
        itemSOList = new List<ItemSO>();
        int index = e.index;
        List<int> listWeaponOwned = dataRuntime.GetListWeaponsOwned();
        bool[] itemPool = weaponShopRuntime.GetListItem();
        if (itemPool != null && itemPool.Length > 0)
        {
            for (int i = 9*index; i < 9*(index+1); i++)
            {
                if (!itemPool[i])
                {
                    itemSOList.Add(listItemSO.itemSOList[i]);
                }
            }
        }
    }


    public void BuyItem()
    {
        if (itemSOList == null || itemSOList.Count == 0)
        {
            return;
        }

        int purchasePrice = 10;

        if (playerCoins >= purchasePrice)
        {
            playerCoins -= purchasePrice;

            ItemSO randomItem = GetRandomItem();
            int index = listItemSO.itemSOList.IndexOf(randomItem);
            itemSOList.Remove(randomItem);
            weaponShopRuntime.SetValueByIndex(index);
            OnChangeSkin?.Invoke(this,new EventChangeSkin { indexSkin = index});
        }
    }
    private ItemSO GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemSOList.Count);

        return itemSOList[randomIndex];
    }
    
}
