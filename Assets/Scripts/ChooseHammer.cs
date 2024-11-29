using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHammer : MonoBehaviour
{
    [SerializeField] private Transform ListSkin;
    int index;
    void Start()
    {
        index = DataRuntimeManager.Instance.DataRuntime.Skin();
        ShopManager.Instance.OnUpdate += Player_OnUpdate;
        ListSkin.GetChild(index).gameObject.SetActive(true);
    }

    private void Player_OnUpdate(object sender, System.EventArgs e)
    {
        ListSkin.GetChild(index).gameObject.SetActive(false);
        index = DataRuntimeManager.Instance.DataRuntime.Weapon();
        ListSkin.GetChild(index).gameObject.SetActive(true);
    }
}
