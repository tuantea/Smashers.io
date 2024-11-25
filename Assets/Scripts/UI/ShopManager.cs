using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    
    public static ShopManager Instance { get; private set; }
    [SerializeField] private Camera cameraUI;
    [SerializeField] private GameObject shopSkin;
    [SerializeField] private GameObject shopWeapon;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Back()
    {
        shopSkin.SetActive(false);
        shopWeapon.SetActive(false);
        cameraUI.gameObject.SetActive(false);
    }
    public void ShowShopSkin()
    {
        shopSkin.SetActive(true);
        cameraUI.gameObject.SetActive(true);
    }
    public void ShowShopWeapon()
    {
        shopWeapon.SetActive(true);
        cameraUI.gameObject.SetActive(true);
    }
}
