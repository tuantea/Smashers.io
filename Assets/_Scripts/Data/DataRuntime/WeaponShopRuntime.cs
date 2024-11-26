using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponShopRuntime
{
    [SerializeField] private bool[] itemWeaponArray;
    [SerializeField] private bool[] itemSkinArray;

    public WeaponShopRuntime(bool[] listItemSOCommon, bool[] itemSkinArray)
    {
        this.itemWeaponArray = listItemSOCommon;
        this.itemSkinArray = itemSkinArray;
    }
    public WeaponShopRuntime() { 
        itemWeaponArray = new bool[27];
        itemSkinArray = new bool[27];
        itemSkinArray[0] = true;
        itemWeaponArray[0] = true;
    }
    public WeaponShopRuntime DeepCopy()
    {
        return new WeaponShopRuntime(itemWeaponArray,itemSkinArray);
    }
    public bool[] GetListItemWeapon()
    {
        return itemWeaponArray;
    }
    public bool[] GetListItemSkin()
    {
        return itemSkinArray;
    }
    public int GetCount()
    {
        return itemWeaponArray.Length;
    }
    public void SetValueByIndexWeapon(int index)
    {
        itemWeaponArray[index] = true;
    }
    public void SetValueByIndexSkin(int index)
    {
        itemWeaponArray[index] = true;
    }
}
public class DataShop
{
    public bool isPurchase;
}
