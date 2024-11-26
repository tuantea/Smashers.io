using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeShop
{
    Skin,
    Weapon
}
public class TabManager : MonoBehaviour
{
    public static TabManager Instance { get; private set; }
    [SerializeField] TypeShop typeShop;
    private TabCommon tabCurrent;
    public Button[] tabButtons;
    public GameObject[] panel;
    public event EventHandler<ChangeValueShopEvent> OnChangeValueShop;
    public class ChangeValueShopEvent : EventArgs
    {
        public int index;
        public TypeShop typeShop;
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
        int skin=0, indexTab = 0;
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i;
            tabButtons[index].onClick.AddListener(() => OpenTab(index));
            panel[index].SetActive(false);
        }
        if (typeShop == TypeShop.Skin)
        {
            skin = DataRuntimeManager.Instance.DataRuntime.Skin();
        }
        else
        {
            skin = DataRuntimeManager.Instance.DataRuntime.Weapon();
        }
        indexTab = skin / 9;
        tabCurrent = tabButtons[indexTab].transform.GetComponent<TabCommon>();
       // tabCurrent.Active();
        panel[indexTab].SetActive(true);
       // OpenTab(indexTab);
        StartCoroutine(AwaitEvent(indexTab));
    }
    private void OnEnable()
    {
        int index, indexTab;
        if (typeShop == TypeShop.Skin)
        {
            index = DataRuntimeManager.Instance.DataRuntime.Skin();
            Debug.Log("SkinIndex "+index);
            indexTab = index / 9;
            tabCurrent = tabButtons[indexTab].transform.GetComponent<TabCommon>();
            tabCurrent.ActiveSkin();
        }
        else
        {
            index = DataRuntimeManager.Instance.DataRuntime.Weapon();
            indexTab = index / 9;
            tabCurrent = tabButtons[indexTab].transform.GetComponent<TabCommon>();
            Debug.Log("WeaponIndex " + index);
            Debug.Log("indexTab "+indexTab);
            tabCurrent.Active();
        }
        panel[indexTab].SetActive(true);
        Debug.Log("name"+panel[indexTab].transform.name);
        Debug.Log("IndexTab1 " + indexTab);
        OpenTab(indexTab);
    }

    public void OpenTab(int index)
    {
        int indexTabCurrent= tabCurrent.GetIndexTab();
        Debug.Log("indexTabcurrent " + indexTabCurrent);
        if(indexTabCurrent != index) 
        {
            Debug.Log("kkkkdavaoday");
            tabCurrent.UnActive();
            panel[indexTabCurrent].SetActive(false);
            tabCurrent = tabButtons[index].transform.GetComponent<TabCommon>();
            if (typeShop == TypeShop.Skin)
            {
                tabCurrent.ActiveSkin();
            }
            else
            {
                tabCurrent.Active();
                Debug.Log("Active111");
            }
            panel[index].SetActive(true);
            Debug.Log(OnChangeValueShop != null);
            OnChangeValueShop?.Invoke(this, new ChangeValueShopEvent
            {
                index = index,
                typeShop = this.typeShop,
            });
        }
    }
    IEnumerator AwaitEvent(int indexTab)
    {
        if (OnChangeValueShop == null)
            yield return null;
        OnChangeValueShop?.Invoke(this, new ChangeValueShopEvent
        {
            index = indexTab,
            typeShop = typeShop,
        });
    }
}