using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public static TabManager Instance { get; private set; }
    private TabCommon tabCurrent;
    public Button[] tabButtons;
    public GameObject[] panel;
    public event EventHandler<ChangeValueShopEvent> OnChangeValueShop;
    public class ChangeValueShopEvent : EventArgs
    {
        public int index;
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i;
            tabButtons[index].onClick.AddListener(() => OpenTab(index));
            panel[index].SetActive(false);
        }
        int skin = DataRuntimeManager.Instance.DataRuntime.Skin();
        int indexTab = skin / 9;
        tabCurrent = tabButtons[indexTab].transform.GetComponent<TabCommon>();
        tabCurrent.Active();
        panel[indexTab].SetActive(true);
        OpenTab(indexTab);
        OnChangeValueShop?.Invoke(this, new ChangeValueShopEvent { index = indexTab });

    }
    private void OnEnable()
    {
        int skin = DataRuntimeManager.Instance.DataRuntime.Skin();
        int indexTab = skin / 9;
        tabCurrent = tabButtons[indexTab].transform.GetComponent<TabCommon>();
        tabCurrent.Active();
        panel[indexTab].SetActive(true);
        OpenTab(indexTab);
        OnChangeValueShop?.Invoke(this, new ChangeValueShopEvent { index = indexTab });
    }

    public void OpenTab(int index)
    {
        int indexTabCurrent= tabCurrent.GetIndexTab();
        if(indexTabCurrent != index) 
        {
            tabCurrent.UnActive();
            panel[indexTabCurrent].SetActive(false);
            tabCurrent = tabButtons[index].transform.GetComponent<TabCommon>();
            tabCurrent.Active();
            panel[index].SetActive(true);
            OnChangeValueShop?.Invoke(this, new ChangeValueShopEvent { index=index});
        }
    }
}