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
    public event EventHandler<EventChangeSkin> OnChangeWeapon;
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
        TabManager.Instance.OnChangeValueShop += Instance_OnChangeValueShop;
        weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
        dataRuntime = DataRuntimeManager.Instance.DataRuntime;
        //playerCoins = dataRuntime.Gold();
        playerCoins = 1000;
    }

    private void Instance_OnChangeValueShop(object sender, TabManager.ChangeValueShopEvent e)
    {
        itemSOList = new List<ItemSO>();
        int index = e.index;
        Debug.Log("TypeShop "+e.typeShop.ToString());
        if (e.typeShop == 0) 
        {
            bool[] itemPool = weaponShopRuntime.GetListItemSkin();
            if (itemPool != null && itemPool.Length > 0)
            {
                for (int i = 9 * index; i < 9 * (index + 1); i++)
                {
                    if (!itemPool[i])
                    {
                        itemSOList.Add(listItemSO.itemSOList[i]);
                    }
                }
            }
        }
        else
        {
            bool[] itemPool = weaponShopRuntime.GetListItemWeapon();
            if (itemPool != null && itemPool.Length > 0)
            {
                for (int i = 9 * index; i < 9 * (index + 1); i++)
                {
                    if (!itemPool[i])
                    {
                        itemSOList.Add(listItemSO.itemSOList[i]);
                    }
                }
            }
        }
    }


    public void BuyItem()
    {
        Debug.Log("BuyItem");
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
            weaponShopRuntime.SetValueByIndexWeapon(index);
            OnChangeSkin?.Invoke(this,new EventChangeSkin { indexSkin = index});
        }
    }
    public void BuyItemSkin()
    {
        Debug.Log("BuyItemSkin");
        if (itemSOList == null || itemSOList.Count == 0)
        {
            Debug.Log("nullnek");
            return;
        }

        int purchasePrice = 10;

        if (playerCoins >= purchasePrice)
        {
            playerCoins -= purchasePrice;

            ItemSO randomItem = GetRandomItem();
            int index = listItemSO.itemSOList.IndexOf(randomItem);
            itemSOList.Remove(randomItem);
            weaponShopRuntime.SetValueByIndexSkin(index);
            OnChangeSkin?.Invoke(this, new EventChangeSkin { indexSkin = index });
        }
    }
    private ItemSO GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemSOList.Count);

        return itemSOList[randomIndex];
    }
    
}
