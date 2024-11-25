using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private GameObject skins;

    private void ActiveSkin(int skinIndex)
    {
        skins.transform.GetChild(skinIndex).gameObject.SetActive(true);
    }
}
