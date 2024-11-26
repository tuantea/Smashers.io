using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabCommon : MonoBehaviour, ITab
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform prefabItemThumbnail;

    [SerializeField] private Image tabCommon;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Color color;
    [SerializeField] private Color colorWhite;

    [SerializeField] private Sprite sprite1;
    [SerializeField] private ListItemSO listItemSO;
    [SerializeField] private int indexTab;
    [SerializeField] private List<ItemThumbnail> itemThumbnailList=new List<ItemThumbnail>();
    private WeaponShopRuntime weaponShopRuntime;
    DataRuntime dataRuntime;
    int index,skin;
    private List<Button> listButton;
    [SerializeField] private bool isStart = false;
    [SerializeField] private ToggleGroup toggleGroup;
    private void Start()
    {
        dataRuntime = DataRuntimeManager.Instance.DataRuntime;
        listButton = new List<Button>();
        skin = dataRuntime.Skin();
        index = skin / 9;
        //if (skin / 9 == indexTab)
        //{
        //    Active();
        //}
        Shop.Instance.OnChangeSkin += ItemThumbnail_OnChangeSkin;
    }

    private void ItemThumbnail_OnChangeSkin(object sender, Shop.EventChangeSkin e)
    {
        Debug.Log(e.indexSkin);
        Debug.Log(e.indexSkin - 9 * indexTab);
        if (e.indexSkin >= 9 * indexTab && e.indexSkin < 9 * (indexTab + 1))
        {
            itemThumbnailList[e.indexSkin-9*indexTab].ChangeValue();
        }
    }

    public int GetIndexTab()
    {
        return indexTab;
    }
    public void ActiveSkin()
    {
        tabCommon.color = colorWhite;
        rarityText.color = color;
        skin = DataRuntimeManager.Instance.DataRuntime.Skin();
        if (!isStart && skin >= 9 * indexTab && skin < 9 * (indexTab + 1))
        {
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            toggles[skin - 9 * indexTab].isOn = true;
        }
        if (isStart)
        {
            isStart = false;
            weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
            bool[] isPurchase = weaponShopRuntime.GetListItemSkin();
            for (int i = 9 * indexTab; i < 9 * (indexTab + 1); i++)
            {
                if (i >= listItemSO.itemSOList.Count) break;
                CreateItemThumbnail(listItemSO.itemSOList[i], i, isPurchase[i], i == skin);
            }
        }
    }
    public void Active()
    {
        Debug.Log("Active");
        Debug.Log(isStart);
        tabCommon.color = colorWhite;
        rarityText.color = color;
        skin=DataRuntimeManager.Instance.DataRuntime.Skin();
        if (!isStart&&skin >= 9 * indexTab && skin < 9 * (indexTab + 1))
        {
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            toggles[skin-9*indexTab].isOn = true;
        }
        if (isStart)
        {
            isStart = false;
            weaponShopRuntime = DataRuntimeManager.Instance.WeaponShopRuntime;
            bool[] isPurchase = weaponShopRuntime.GetListItemWeapon();
            for (int i = 9 * indexTab; i < 9 * (indexTab + 1); i++)
            {
                if (i >= listItemSO.itemSOList.Count) break;
                CreateItemThumbnail(listItemSO.itemSOList[i],i, isPurchase[i], i == skin);
            }
        }
    }
    public void UnActive()
    {
        tabCommon.color = color;
        rarityText.color = colorWhite;
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }
    }
    private void OnDisable()
    {
        UnActive();
    }
    public void CreateItemThumbnail(ItemSO itemSO,int index,bool isPurchase,bool toggle)
    {
        Transform item = Instantiate(prefabItemThumbnail, container);
        Transform itemSprite = item.GetChild(0).Find("_Item_Sprite");
        Transform itemNotOwned = item.GetChild(0).Find("_ItemNotOwned");
        Transform button = item.Find("Button");
        ItemThumbnail thumbnail = item.GetComponent<ItemThumbnail>();
        itemThumbnailList.Add(thumbnail);
        if (toggle)
        {
            thumbnail.SetToggle();
        }
        thumbnail.GetToggle().group= toggleGroup;
        thumbnail.ToggleMarkItem(!isPurchase);
        thumbnail.EnableToggle(isPurchase);
        thumbnail.SetIndex(index);
        thumbnail.ItemClick += Thumbnail_ItemClick;
        if (itemSprite != null)
        {
            Image image = itemSprite.GetComponent<Image>();
            image.sprite = itemSO.sprite;
        }
        if(itemNotOwned != null)
        {
            itemNotOwned.gameObject.SetActive(itemSO.isPurchase);
        }
    }

    private void Thumbnail_ItemClick(object sender, ItemThumbnail.ChooseEventArgs e)
    {
        ChooseWeapon.Instance?.OnChangeSkin(e.indexChoose);
        ChooseSkin.Instance?.OnChangeSkin(e.indexChoose);
        //DataRuntimeManager.Instance.DataRuntime.SetSkin(e.indexChoose);
    }

}
