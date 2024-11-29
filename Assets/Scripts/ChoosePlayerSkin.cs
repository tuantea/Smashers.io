using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayerSkin : MonoBehaviour
{
    [SerializeField] private List<Skin> ListSkin;

    [SerializeField] private GameObject _permanant;
    int index;
    void Start()
    {
        index = DataRuntimeManager.Instance.DataRuntime.Skin();
        ShopManager.Instance.OnUpdate += Play_OnUpdate;
        ListSkin[index].gameObject.SetActive(true);
        _permanant.SetActive(ListSkin[index].GetComponent<Skin>().IsHeroA());
    }

    private void Play_OnUpdate(object sender, System.EventArgs e)
    {

        ListSkin[index].gameObject.SetActive(false);
        index = DataRuntimeManager.Instance.DataRuntime.Skin();
        ListSkin[index].gameObject.SetActive(true);
        _permanant.SetActive(ListSkin[index].GetComponent<Skin>().IsHeroA());
    }
}
