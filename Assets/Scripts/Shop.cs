using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Shop : MonoBehaviour
{
    protected DataRuntime dataRuntime;
    protected WeaponShopRuntime weaponShopRuntime;
    [SerializeField] protected ListItemSO listItemSO;
    [SerializeField] protected TabManager tabManager;
    private List<ItemSO> itemSOList;
    protected int playerCoins;
    public event EventHandler<EventChangeSkin> OnChangeSkin;
    public event EventHandler<EventChangeSkin> OnChangeWeapon;
    public class EventChangeSkin:EventArgs
    {
        public int indexSkin;
    }


    protected void Instance_OnChangeValueShop(object sender, TabManager.ChangeValueShopEvent e)
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


    public virtual void BuyItem()
    {
        Debug.Log("BuyItem");
        if (itemSOList == null || itemSOList.Count == 0)
        {
            Debug.Log("null");
            Debug.Log(itemSOList == null);
            return;
        }

        int purchasePrice = 10;

        if (playerCoins >= purchasePrice)
        {
            Debug.Log("damua");
            playerCoins -= purchasePrice;

            ItemSO randomItem = GetRandomItem();
            int index = listItemSO.itemSOList.IndexOf(randomItem);
            itemSOList.Remove(randomItem);
            if (tabManager.GetTypeShop() == 0)
            {
                Debug.Log("SSSS");
                weaponShopRuntime.SetValueByIndexSkin(index);
                OnChangeSkin?.Invoke(this, new EventChangeSkin { indexSkin = index });
            }
            else
            {
                Debug.Log("wwww");
                weaponShopRuntime.SetValueByIndexWeapon(index);
                OnChangeWeapon?.Invoke(this, new EventChangeSkin { indexSkin = index });
            }
            //OnChangeWeapon?.Invoke(this, new EventChangeSkin { indexSkin = index });
            //OnChangeSkin?.Invoke(this, new EventChangeSkin { indexSkin = index });
        }
    }
    public ItemSO GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemSOList.Count);

        return itemSOList[randomIndex];
    }
    
}
