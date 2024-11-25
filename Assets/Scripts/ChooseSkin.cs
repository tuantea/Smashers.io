using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSkin : MonoBehaviour
{
    public static ChooseSkin Instance { get; private set; }
    [SerializeField] private List<Skin> ListSkin;
    int index;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        index = DataRuntimeManager.Instance.DataRuntime.Skin();
        Shop.Instance.OnChangeSkin += Shop_OnChangeSkin;
        ListSkin[index].gameObject.SetActive(true);
    }

    private void Shop_OnChangeSkin(object sender, Shop.EventChangeSkin e)
    {
        ListSkin[index].gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetSkin(e.indexSkin);
        index = e.indexSkin;
        Debug.Log("e.indexSkin"+e.indexSkin);
        ListSkin[e.indexSkin].gameObject.SetActive(true);
    }

    public void OnChangeSkin(int indexChoose)
    {
        ListSkin[index].gameObject.SetActive(false);
        DataRuntimeManager.Instance.DataRuntime.SetSkin(indexChoose);
        index = indexChoose;
        Debug.Log("e.indexSkin" + indexChoose);
        ListSkin[indexChoose].gameObject.SetActive(true);
    }
}
