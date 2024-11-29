using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemThumbnailSkin : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject setSelected;
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject markItem;
    private int index;
    public event EventHandler<ChooseEventArgs> ItemClick;

    public class ChooseEventArgs : EventArgs
    {
        public int indexChoose;
    }
    private void Start()
    {
    }
    private void Instance(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void ToggleMarkItem(bool status)
    {
        markItem.SetActive(status);
    }
    public void Selected()
    {
        if (toggle.enabled)
        {
            toggle.isOn = true;
        }
    }
    public void SetToggle()
    {
        toggle.isOn = true;
    }
    public Toggle GetToggle()
    {
        return toggle;
    }
    public void EnableToggle(bool status)
    {
        toggle.enabled = status;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
    public void ChangeValue()
    {
        toggle.enabled = true;
        toggle.isOn = true;
        markItem.SetActive(false);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        setSelected.SetActive(isOn);
        Debug.Log(isOn);
        if (isOn)
        {
            ItemClick?.Invoke(this, new ChooseEventArgs { indexChoose = index });
        }
    }
}
