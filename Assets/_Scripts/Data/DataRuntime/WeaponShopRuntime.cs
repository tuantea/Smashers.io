using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponShopRuntime
{
    [SerializeField] private bool[] itemSOArray;

    public WeaponShopRuntime(bool[] listItemSOCommon)
    {
        this.itemSOArray = listItemSOCommon;
    }
    public WeaponShopRuntime() { 
        itemSOArray = new bool[27];
        itemSOArray[0] = true;
    }
    public WeaponShopRuntime DeepCopy()
    {
        return new WeaponShopRuntime(itemSOArray);
    }
    public bool[] GetListItem()
    {
        return itemSOArray;
    }
    public int GetCount()
    {
        return itemSOArray.Length;
    }
    public void SetValueByIndex(int index)
    {
        itemSOArray[index] = true;
    }
}
public class DataShop
{
    public bool isPurchase;
}
